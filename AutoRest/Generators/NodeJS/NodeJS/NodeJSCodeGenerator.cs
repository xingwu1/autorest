// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Rest.Generator.ClientModel;
using Microsoft.Rest.Generator.NodeJS.Properties;
using Microsoft.Rest.Generator.NodeJS.Templates;
using Microsoft.Rest.Generator.Utilities;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Rest.Generator.NodeJS
{
    public class NodeJSCodeGenerator : CodeGenerator
    {
        private const string ClientRuntimePackage = "ms-rest version 1.2.0";
        private const bool DisableTypeScriptGeneration = false;    // Change to true if you want to no longer generate the 3 d.ts files, for some reason

        public NodeJsCodeNamer Namer { get; private set; }

        public NodeJSCodeGenerator(Settings settings) : base(settings)
        {
            Namer = new NodeJsCodeNamer();
        }

        public override string Name
        {
            get { return "NodeJS"; }
        }

        public override string Description
        {
            // TODO resource string.
            get { return "NodeJS for Http Client Libraries"; }
        }

        public override string UsageInstructions
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    Resources.UsageInformation, ClientRuntimePackage);
            }
        }

        public override string ImplementationFileExtension
        {
            get { return ".js"; }
        }

        /// <summary>
        /// Normalizes client model by updating names and types to be language specific.
        /// </summary>
        /// <param name="serviceClient"></param>
        public override void NormalizeClientModel(ServiceClient serviceClient)
        {
            PopulateAdditionalProperties(serviceClient);
            Namer.NormalizeClientModel(serviceClient);
            Namer.ResolveNameCollisions(serviceClient, Settings.Namespace,
                Settings.Namespace + ".Models");
        }

        private void PopulateAdditionalProperties(ServiceClient serviceClient)
        {
            if (Settings.AddCredentials)
            {
                if (!serviceClient.Properties.Any(p => p.Type == PrimaryType.Credentials))
                {
                    serviceClient.Properties.Add(new Property
                    {
                        Name = "credentials",
                        SerializedName = "credentials",
                        Type = PrimaryType.Credentials,
                        IsRequired = true,
                        Documentation = "Subscription credentials which uniquely identify client subscription."
                    });
                }
                
            }
        }

        /// <summary>
        /// Generate NodeJS client code for given ServiceClient.
        /// </summary>
        /// <param name="serviceClient"></param>
        /// <returns></returns>
        public override async Task Generate(ServiceClient serviceClient)
        {
            var serviceClientTemplateModel = new ServiceClientTemplateModel(serviceClient);
            // Service client
            var serviceClientTemplate = new ServiceClientTemplate
            {
                Model = serviceClientTemplateModel,
            };
            await Write(serviceClientTemplate, serviceClient.Name.ToCamelCase() + ".js");

            if (!DisableTypeScriptGeneration)
            {
                var serviceClientTemplateTS = new ServiceClientTemplateTS
                {
                    Model = serviceClientTemplateModel,
                };
                await Write(serviceClientTemplateTS, serviceClient.Name.ToCamelCase() + ".d.ts");
            }

            //Models
            if (serviceClient.ModelTypes.Any())
            {
                var modelIndexTemplate = new ModelIndexTemplate
                {
                    Model = serviceClientTemplateModel
                };
                await Write(modelIndexTemplate, Path.Combine("models", "index.js"));
                if (!DisableTypeScriptGeneration)
                {
                    var modelIndexTemplateTS = new ModelIndexTemplateTS
                    {
                        Model = serviceClientTemplateModel
                    };
                    await Write(modelIndexTemplateTS, Path.Combine("models", "index.d.ts"));
                }

                foreach (var modelType in serviceClientTemplateModel.ModelTemplateModels)
                {
                    var modelTemplate = new ModelTemplate
                    {
                        Model = modelType
                    };
                    await Write(modelTemplate, Path.Combine("models", modelType.Name.ToCamelCase() + ".js"));
                }
            }

            //MethodGroups
            if (serviceClientTemplateModel.MethodGroupModels.Any())
            {
                var methodGroupIndexTemplate = new MethodGroupIndexTemplate
                {
                    Model = serviceClientTemplateModel
                };
                await Write(methodGroupIndexTemplate, Path.Combine("operations", "index.js"));

                if (!DisableTypeScriptGeneration)
                {
                    var methodGroupIndexTemplateTS = new MethodGroupIndexTemplateTS
                    {
                        Model = serviceClientTemplateModel
                    };
                    await Write(methodGroupIndexTemplateTS, Path.Combine("operations", "index.d.ts"));
                }

                foreach (var methodGroupModel in serviceClientTemplateModel.MethodGroupModels)
                {
                    var methodGroupTemplate = new MethodGroupTemplate
                    {
                        Model = methodGroupModel
                    };
                    await Write(methodGroupTemplate, Path.Combine("operations", methodGroupModel.MethodGroupType.ToCamelCase() + ".js"));
                }
            }
        }
    }
}
