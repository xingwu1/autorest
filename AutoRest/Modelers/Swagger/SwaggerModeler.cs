﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Rest.Generator;
using Microsoft.Rest.Generator.ClientModel;
using Microsoft.Rest.Generator.Logging;
using Microsoft.Rest.Generator.Utilities;
using Microsoft.Rest.Modeler.Swagger.Model;
using ParameterLocation = Microsoft.Rest.Modeler.Swagger.Model.ParameterLocation;
using Resources = Microsoft.Rest.Modeler.Swagger.Properties.Resources;

namespace Microsoft.Rest.Modeler.Swagger
{
    public class SwaggerModeler : Generator.Modeler
    {
        private const string BaseUriParameterName = "BaseUri";

        internal Dictionary<string, string> ExtendedTypes = new Dictionary<string, string>();
        internal Dictionary<string, CompositeType> GeneratedTypes = new Dictionary<string, CompositeType>();

        public SwaggerModeler(Settings settings) : base(settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            DefaultProtocol = TransferProtocolScheme.Http;
        }

        public override string Name
        {
            get { return "Swagger"; }
        }

        /// <summary>
        /// Swagger service model.
        /// </summary>
        public ServiceDefinition ServiceDefinition { get; set; }

        /// <summary>
        /// Client model.
        /// </summary>
        public ServiceClient ServiceClient { get; set; }

        /// <summary>
        /// Default protocol when no protocol is specified in the schema
        /// </summary>
        public TransferProtocolScheme DefaultProtocol { get; set; }

        /// <summary>
        /// Builds service model from swagger file.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public override ServiceClient Build()
        {
            PrimaryType.Reset();
            Logger.LogInfo(Resources.ParsingSwagger);
            ServiceDefinition = SwaggerParser.Load(Settings.Input, Settings.FileSystem);
            Logger.LogInfo(Resources.GeneratingClient);
            InitializeClientModel();
            BuildCompositeTypes();

            // Build client parameters
            foreach (var swaggerParameter in ServiceDefinition.Parameters.Values)
            {
                var parameter = ((ParameterBuilder)swaggerParameter.GetBuilder(this)).Build();

                var clientProperty = new Property();
                clientProperty.LoadFrom(parameter);

                ServiceClient.Properties.Add(clientProperty);
            }

            // Build methods
            foreach (var path in ServiceDefinition.Paths)
            {
                foreach (var verb in path.Value.Keys)
                {
                    var operation = path.Value[verb];
                    if (string.IsNullOrWhiteSpace(operation.OperationId))
                    {
                        throw ErrorManager.CreateError(
                            string.Format(CultureInfo.InvariantCulture,
                                Resources.OperationIdMissing,
                                verb,
                                path.Key));
                    }
                    var methodName = GetMethodName(operation);
                    var methodGroup = GetMethodGroup(operation);

                    if (verb.ToHttpMethod() != HttpMethod.Options)
                    {
                        var method = BuildMethod(verb.ToHttpMethod(), path.Key, methodName, operation);
                        method.Group = methodGroup;
                        ServiceClient.Methods.Add(method);
                    }
                    else
                    {
                        Logger.LogWarning(Resources.OptionsNotSupported);
                    }
                }
            }

            // Set base type
            foreach (var typeName in GeneratedTypes.Keys)
            {
                var objectType = GeneratedTypes[typeName];
                if (ExtendedTypes.ContainsKey(typeName))
                {
                    objectType.BaseModelType = GeneratedTypes[ExtendedTypes[typeName]];
                }

                ServiceClient.ModelTypes.Add(objectType);
            }

            // Flatten request payload
            foreach (var method in ServiceClient.Methods)
            {
                FlattenRequestPayload(method);
            }

            return ServiceClient;
        }

        /// <summary>
        /// Flattens the request payload if the number of properties of the 
        /// payload is less than or equal to the PayloadFlatteningThreshold.
        /// </summary>
        /// <param name="method">Method to process</param>
        private void FlattenRequestPayload(Method method)
        {
            var bodyParameter = method.Parameters.FirstOrDefault(
                p => p.Location == Generator.ClientModel.ParameterLocation.Body);

            if (bodyParameter != null)
            {
                var bodyParameterType = bodyParameter.Type as CompositeType;
                if (bodyParameterType != null && bodyParameterType.ComposedProperties.Count() <= Settings.PayloadFlatteningThreshold)
                {
                    var parameterTransformation = new ParameterTransformation
                    {
                        OutputParameter = bodyParameter
                    };
                    method.InputParameterTransformation.Add(parameterTransformation);

                    foreach (var property in bodyParameterType.ComposedProperties)
                    {
                        var newMethodParameter = new Parameter();
                        newMethodParameter.LoadFrom(property);
                        method.Parameters.Add(newMethodParameter);

                        parameterTransformation.ParameterMappings.Add(new ParameterMapping
                        {
                            InputParameter = newMethodParameter,
                            OutputParameterProperty = property.Name
                        });                        
                    }

                    method.Parameters.Remove(bodyParameter);
                }
            }
        }

        /// <summary>
        /// Initialize the base service and populate global service properties
        /// </summary>
        /// <returns>The base ServiceModel Service</returns>
        public virtual void InitializeClientModel()
        {
            ServiceClient = new ServiceClient();
            if (string.IsNullOrEmpty(ServiceDefinition.Swagger))
            {
                throw ErrorManager.CreateError(Resources.UnknownSwaggerVersion);
            }

            if (ServiceDefinition.Info == null)
            {
                throw ErrorManager.CreateError(Resources.InfoSectionMissing);
            }

            if (string.IsNullOrWhiteSpace(Settings.ClientName))
            {
                if (ServiceDefinition.Info.Title == null)
                {
                    throw ErrorManager.CreateError(Resources.TitleMissing);
                }

                ServiceClient.Name = ServiceDefinition.Info.Title.Replace(" ", "");
            }
            else
            {
                ServiceClient.Name = Settings.ClientName;
            }
            ServiceClient.Namespace = Settings.Namespace;
            ServiceClient.ApiVersion = ServiceDefinition.Info.Version;
            ServiceClient.Documentation = ServiceDefinition.Info.Description;
            if (ServiceDefinition.Schemes == null || ServiceDefinition.Schemes.Count != 1)
            {
                ServiceDefinition.Schemes = new List<TransferProtocolScheme> {DefaultProtocol};
            }
            if (string.IsNullOrEmpty(ServiceDefinition.Host))
            {
                ServiceDefinition.Host = "localhost";
            }
            ServiceClient.BaseUrl = string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}",
                ServiceDefinition.Schemes[0].ToString().ToLower(CultureInfo.InvariantCulture),
                ServiceDefinition.Host, ServiceDefinition.BasePath);
        }

        /// <summary>
        /// Create the BaseURi for a method from metadata
        /// </summary>
        /// <param name="serviceClient">The service client that will contain the method </param>
        /// <param name="path">The relative path for this operation</param>
        /// <returns>A string representing the full http (parameterized) path for the operation</returns>
        public virtual string BuildMethodBaseUrl(ServiceClient serviceClient, string path)
        {
            return string.Format(CultureInfo.InvariantCulture, "{{{0}}}{1}", BaseUriParameterName, path);
        }

        /// <summary>
        /// Build composite types from definitions
        /// </summary>
        public virtual void BuildCompositeTypes()
        {
            // Load any external references
            foreach (var reference in ServiceDefinition.ExternalReferences)
            {
                string[] splitReference = reference.Split(new[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
                Debug.Assert(splitReference.Length == 2);
                string filePath = splitReference[0];
                string externalDefinition = Settings.FileSystem.ReadFileAsText(filePath);
                ServiceDefinition external = SwaggerParser.Parse(externalDefinition);
                external.Definitions.ForEach(d => ServiceDefinition.Definitions[d.Key] = d.Value);
            }

            // Build service types and validate allOf
            if (ServiceDefinition.Definitions != null)
            {
                foreach (var schemaName in ServiceDefinition.Definitions.Keys.ToArray())
                {
                    var schema = ServiceDefinition.Definitions[schemaName];
                    schema.GetBuilder(this).BuildServiceType(schemaName);

                    Resolver.ExpandAllOf(schema);
                    var parent = string.IsNullOrEmpty(schema.Extends.StripDefinitionPath())
                        ? null
                        : ServiceDefinition.Definitions[schema.Extends.StripDefinitionPath()];

                    if (parent != null &&
                        !AncestorsHaveProperties(parent.Properties, parent.Extends.StripDefinitionPath()))
                    {
                        throw ErrorManager.CreateError(Resources.InvalidAncestors, schemaName);
                    }
                }
            }
        }

        /// <summary>
        /// Recursively traverse the schema's extends to verify that it or one of it's parents
        /// has at least one property
        /// </summary>
        /// <param name="properties">The schema's properties</param>
        /// <param name="extends">The schema's extends</param>
        /// <returns>True if one or more properties found in this schema or in it's ancestors. False otherwise</returns>
        private bool AncestorsHaveProperties(Dictionary<string, Schema> properties, string extends)
        {
            if (properties.IsNullOrEmpty() && string.IsNullOrEmpty(extends))
            {
                return false;
            }

            if (!properties.IsNullOrEmpty())
            {
                return true;
            }

            Debug.Assert(!string.IsNullOrEmpty(extends) && ServiceDefinition.Definitions.ContainsKey(extends));
            return AncestorsHaveProperties(ServiceDefinition.Definitions[extends].Properties,
                ServiceDefinition.Definitions[extends].Extends);
        }

        /// <summary>
        /// Builds method from swagger operation.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public Method BuildMethod(HttpMethod httpMethod, string url, string name,
            Operation operation)
        {
            string methodGroup = GetMethodGroup(operation);
            var operationBuilder = new OperationBuilder(operation, this);
            Method method = operationBuilder.BuildMethod(httpMethod, url, name, methodGroup);
            return method;
        }

        /// <summary>
        /// Extracts method group from operation ID.
        /// </summary>
        /// <param name="operation">The swagger operation.</param>
        /// <returns>Method group name or null.</returns>
        private static string GetMethodGroup(Operation operation)
        {
            if (operation.OperationId.IndexOf('_') == -1)
            {
                return null;
            }

            var parts = operation.OperationId.Split('_');
            return parts[0];
        }

        /// <summary>
        /// Extracts method name from operation ID.
        /// </summary>
        /// <param name="operation">The swagger operation.</param>
        /// <returns>Method name.</returns>
        private static string GetMethodName(Operation operation)
        {
            if (operation.OperationId.IndexOf('_') == -1)
            {
                return operation.OperationId;
            }

            var parts = operation.OperationId.Split('_');
            return parts[1];
        }

        public SwaggerParameter Unwrap(SwaggerParameter swaggerParameter)
        {
            if (swaggerParameter == null)
            {
                throw new ArgumentNullException("swaggerParameter");
            }

            // If referencing global parameters serializationProperty
            if (swaggerParameter.Reference != null)
            {
                string referenceKey = swaggerParameter.Reference.StripParameterPath();
                if (!ServiceDefinition.Parameters.ContainsKey(referenceKey))
                {
                    throw new ArgumentException(
                        string.Format(CultureInfo.InvariantCulture,
                        Resources.DefinitionDoesNotExist, referenceKey));
                }

                swaggerParameter = ServiceDefinition.Parameters[referenceKey];
            }

            // Unwrap the schema if in "body"
            if (swaggerParameter.Schema != null && swaggerParameter.In == ParameterLocation.Body)
            {
                swaggerParameter.Schema = Resolver.Unwrap(swaggerParameter.Schema);
            }

            return swaggerParameter;
        }

        public SchemaResolver Resolver
        {
            get { return new SchemaResolver(this); }
        }
    }
}
