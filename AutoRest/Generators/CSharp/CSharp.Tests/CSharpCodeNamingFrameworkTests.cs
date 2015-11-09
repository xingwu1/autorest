﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Globalization;
using System.Linq;
using Microsoft.Rest.Generator.ClientModel;
using Xunit;

namespace Microsoft.Rest.Generator.CSharp.Tests
{
    [Collection("AutoRest Tests")]
    public class CSharpCodeNamingFrameworkTests
    {
        [Fact]
        public void TypeNormalizationTest()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";
            serviceClient.Properties.Add(new Property
            {
                Name = "&%$ i rock too!",
                Type = PrimaryType.Int
            });
            serviceClient.Properties.Add(new Property
            {
                Name = "some-other-stream",
                Type = PrimaryType.Stream
            });

            var customObjectType = new CompositeType();
            customObjectType.Name = "!@#$%^&*()abc";
            customObjectType.SerializedName = "!@#$%^&*()abc";
            customObjectType.Properties.Add(new Property
            {
                Name = "boolean-property",
                Type = PrimaryType.Boolean
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "some^dateTime_sequence",
                Type = new SequenceType
                {
                    ElementType = PrimaryType.DateTime
                }
            });

            var baseType = new CompositeType();
            baseType.Name = "baseType";
            baseType.SerializedName = "baseType";
            baseType.Properties.Add(new Property
            {
                Name = "boolean-property",
                SerializedName = "boolean-property",
                Type = PrimaryType.Boolean
            });
            baseType.BaseModelType = baseType;
            baseType.Properties.Add(new Property
            {
                Name = "self-property",
                SerializedName = "self-property",
                Type = baseType
            });

            customObjectType.BaseModelType = baseType;

            serviceClient.ModelTypes.Add(customObjectType);
            serviceClient.ModelTypes.Add(baseType);

            var framework = new CSharpCodeNamer();
            framework.NormalizeClientModel(serviceClient);

            Assert.Equal("Azurealwaysrocks", serviceClient.Name);
            Assert.Equal("Abc", serviceClient.ModelTypes[0].Name);
            Assert.Equal("!@#$%^&*()abc", serviceClient.ModelTypes[0].SerializedName);
            Assert.Equal("BooleanProperty", serviceClient.ModelTypes[0].Properties[0].Name);
            Assert.Equal("SomedateTimeSequence", serviceClient.ModelTypes[0].Properties[1].Name);
            Assert.Equal("BaseType", serviceClient.ModelTypes[1].Name);
            Assert.Equal("baseType", serviceClient.ModelTypes[1].SerializedName);
            Assert.Equal("BooleanProperty", serviceClient.ModelTypes[1].Properties[0].Name);
            Assert.Equal("boolean-property", serviceClient.ModelTypes[1].Properties[0].SerializedName);
            Assert.Equal("SelfProperty", serviceClient.ModelTypes[1].Properties[1].Name);
            Assert.Equal("self-property", serviceClient.ModelTypes[1].Properties[1].SerializedName);
        }

        [Fact]
        public void TypeNormalizationWithComplexTypesTest()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";

            var childObject = new CompositeType();
            childObject.Name = "child";
            childObject.Properties.Add(new Property
            {
                Name = "childProperty",
                Type = PrimaryType.String
            });

            var customObjectType = new CompositeType();
            customObjectType.Name = "sample";
            customObjectType.Properties.Add(new Property
            {
                Name = "child",
                Type = childObject
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "childList",
                Type = new SequenceType
                {
                    ElementType = childObject
                }
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "childDict",
                Type = new DictionaryType
                {
                    ValueType = childObject
                }
            });

            serviceClient.ModelTypes.Add(customObjectType);
            serviceClient.ModelTypes.Add(childObject);

            var framework = new CSharpCodeNamer();
            framework.NormalizeClientModel(serviceClient);
            framework.ResolveNameCollisions(serviceClient, "SampleNs", "SampleNs.Models");

            Assert.Equal("Sample", serviceClient.ModelTypes[0].Name);
            Assert.Equal("Child", serviceClient.ModelTypes[0].Properties[0].Name);
            Assert.Equal("Child", serviceClient.ModelTypes[0].Properties[0].Type.Name);
            Assert.Equal("ChildList", serviceClient.ModelTypes[0].Properties[1].Name);
            Assert.Equal("IList<Child>", serviceClient.ModelTypes[0].Properties[1].Type.Name);
            Assert.Equal("ChildDict", serviceClient.ModelTypes[0].Properties[2].Name);
            Assert.Equal("IDictionary<string, Child>", serviceClient.ModelTypes[0].Properties[2].Type.Name);
            Assert.Equal("Child", serviceClient.ModelTypes[1].Name);
            Assert.Equal("string", serviceClient.ModelTypes[1].Properties[0].Type.Name);
        }

        [Fact]
        public void VerifyMethodRenaming()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";

            var customObjectType = new CompositeType();
            customObjectType.Name = "!@#$%^&*()abc";
            customObjectType.Properties.Add(new Property
            {
                Name = "boolean-property",
                Type = PrimaryType.Boolean
            });
            serviceClient.Methods.Add(new Method
            {
                Name = " method name with lots of spaces",
                Group = "#$% group with lots of-weird-characters",
                ReturnType = customObjectType
            });

            var framework = new CSharpCodeNamer();
            framework.NormalizeClientModel(serviceClient);

            Assert.Equal("Methodnamewithlotsofspaces", serviceClient.Methods[0].Name);
            Assert.Equal("GroupwithlotsofWeirdCharacters", serviceClient.Methods[0].Group);
            Assert.Equal("Abc", serviceClient.Methods[0].ReturnType.Name);
        }

        [Fact]
        public void NameCollisionTestWithoutNamespace()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";

            var customObjectType = new CompositeType();
            customObjectType.Name = "azure always rocks!";

            var baseType = new CompositeType();
            baseType.Name = "azure always rocks!";

            serviceClient.Methods.Add(new Method
            {
                Name = "azure always rocks!",
                Group = "azure always rocks!",
                ReturnType = customObjectType
            });

            serviceClient.ModelTypes.Add(customObjectType);
            serviceClient.ModelTypes.Add(baseType);

            var framework = new CSharpCodeNamer();
            framework.ResolveNameCollisions(serviceClient, null, null);

            Assert.Equal("azure always rocks!Client", serviceClient.Name);
            Assert.Equal("azure always rocks!Operations", serviceClient.MethodGroups.First());
            Assert.Equal("azure always rocks!", serviceClient.Methods[0].Name);
            Assert.Equal("azure always rocks!", serviceClient.ModelTypes[0].Name);
            Assert.Equal("azure always rocks!Model", serviceClient.ModelTypes[1].Name);
        }

        [Fact]
        public void NameCollisionTestWithNamespace()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";

            var customObjectType = new CompositeType();
            customObjectType.Name = "azure always rocks!";

            var baseType = new CompositeType();
            baseType.Name = "azure always rocks!";

            serviceClient.Methods.Add(new Method
            {
                Name = "azure always rocks!",
                Group = "azure always rocks!",
                ReturnType = customObjectType
            });

            serviceClient.ModelTypes.Add(customObjectType);
            serviceClient.ModelTypes.Add(baseType);

            var framework = new CSharpCodeNamer();
            framework.ResolveNameCollisions(serviceClient, "azure always rocks!", "azure always rocks!.Models");

            Assert.Equal("azure always rocks!Client", serviceClient.Name);
            Assert.Equal("azure always rocks!Operations", serviceClient.MethodGroups.First());
            Assert.Equal("azure always rocks!", serviceClient.Methods[0].Name);
            Assert.Equal("azure always rocks!Model", serviceClient.ModelTypes[0].Name);
            Assert.Equal("azure always rocks!ModelModel", serviceClient.ModelTypes[1].Name);
        }

        [Fact]
        public void SequenceWithRenamedComplexType()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "azure always rocks!";

            var complexType = new CompositeType();
            complexType.Name = "Greetings";

            serviceClient.Methods.Add(new Method
            {
                Name = "List",
                ReturnType = new SequenceType {ElementType = complexType }
            });

            serviceClient.Methods.Add(new Method
            {
                Name = "List2",
                ReturnType = new DictionaryType { ValueType = complexType }
            });

            serviceClient.ModelTypes.Add(complexType);

            var codeGenerator = new CSharpCodeGenerator(new Settings { Namespace = "Polar.Greetings" });
            codeGenerator.NormalizeClientModel(serviceClient);

            Assert.Equal("GreetingsModel", complexType.Name);
            Assert.Equal("IList<GreetingsModel>", serviceClient.Methods[0].ReturnType.Name);
            Assert.Equal("IDictionary<string, GreetingsModel>", serviceClient.Methods[1].ReturnType.Name);
        }

        [Fact]
        public void VerifyInputMappingsForFlattening()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "test service client";

            var customObjectType = new CompositeType();
            customObjectType.Name = "Foo";
            customObjectType.Properties.Add(new Property
            {
                Name = "A",
                Type = PrimaryType.Boolean
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "B",
                Type = PrimaryType.String
            });
            var method = new Method
            {
                Name = "method1",
                Group = "mGroup",
                ReturnType = customObjectType
            };
            var outputParameter = new Parameter { Name = "body", Type = customObjectType };
            serviceClient.Methods.Add(method);
            method.Parameters.Add(new Parameter { Name = "paramA", Type = PrimaryType.Boolean, SerializedName = "paramA" });
            method.Parameters.Add(new Parameter { Name = "paramB", Type = PrimaryType.String, SerializedName = "paramB" });
            method.InputParameterTransformation.Add(new ParameterTransformation
            {
                OutputParameter = outputParameter
            });
            method.InputParameterTransformation[0].ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = method.Parameters[0],
                OutputParameterProperty = "A"
            });
            method.InputParameterTransformation[0].ParameterMappings.Add(new ParameterMapping 
            { 
                InputParameter = method.Parameters[1],
                OutputParameterProperty = "B"
            });

            MethodTemplateModel templateModel = new MethodTemplateModel(method, serviceClient);
            var output = templateModel.BuildInputMappings();
            string expected =
          @"Foo body = null;
            if (paramA != null || paramB != null)
            {
                body = new Foo();
                body.A = paramA;
                body.B = paramB;
            }";

            MultilineAreEqual(expected, output.Trim());
        }

        [Fact]
        public void VerifyInputMappingsForGrouping()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "test service client";

            var customObjectType = new CompositeType();
            customObjectType.Name = "Foo";
            customObjectType.Properties.Add(new Property
            {
                Name = "A",
                Type = PrimaryType.Boolean
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "B",
                Type = PrimaryType.String
            });
            var method = new Method
            {
                Name = "method1",
                Group = "mGroup",
                ReturnType = customObjectType
            };
            var inputParameter = new Parameter { Name = "body", Type = customObjectType };
            serviceClient.Methods.Add(method);
            method.Parameters.Add(inputParameter);
            method.InputParameterTransformation.Add(new ParameterTransformation
            {
                OutputParameter = new Parameter { Name = "paramA", Type = PrimaryType.String, SerializedName = "paramA" }
            });
            method.InputParameterTransformation.Last().ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = inputParameter,
                InputParameterProperty = "A"
            });
            method.InputParameterTransformation.Add(new ParameterTransformation
            {
                OutputParameter = new Parameter { Name = "paramB", Type = PrimaryType.String, SerializedName = "paramB" }
            });
            method.InputParameterTransformation.Last().ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = inputParameter,
                InputParameterProperty = "B"
            });

            MethodTemplateModel templateModel = new MethodTemplateModel(method, serviceClient);
            var output = templateModel.BuildInputMappings();
            string expected =
          @"String paramA = null;
            if (body != null)
            {
                paramA = body.A;
            }
            String paramB = null;
            if (body != null)
            {
                paramB = body.B;
            }";

            MultilineAreEqual(expected, output.Trim());
        }

        [Fact (Skip = "TODO: Implement more robust mapping for resource transformation")]
        public void VerifyInputMappingsForResources()
        {
            var serviceClient = new ServiceClient();
            serviceClient.Name = "test service client";

            var flattenedPropertyType = new CompositeType();
            flattenedPropertyType.Name = "FooFlattened";
            flattenedPropertyType.Properties.Add(new Property
            {
                Name = "Sku",
                Type = PrimaryType.String
            });
            flattenedPropertyType.Properties.Add(new Property
            {
                Name = "ProvState",
                Type = PrimaryType.String
            });
            flattenedPropertyType.Properties.Add(new Property
            {
                Name = "Id",
                Type = PrimaryType.Int
            });

            var customObjectPropertyType = new CompositeType();
            customObjectPropertyType.Name = "FooProperty";
            customObjectPropertyType.Properties.Add(new Property
            {
                Name = "Sku",
                Type = PrimaryType.String
            });
            customObjectPropertyType.Properties.Add(new Property
            {
                Name = "ProvState",
                Type = PrimaryType.String
            });

            var customObjectType = new CompositeType();
            customObjectType.Name = "Foo";
            customObjectType.Properties.Add(new Property
            {
                Name = "Property",
                Type = customObjectPropertyType
            });
            customObjectType.Properties.Add(new Property
            {
                Name = "Id",
                Type = PrimaryType.Int
            });

            var method = new Method
            {
                Name = "method1",
                Group = "mGroup",
                ReturnType = flattenedPropertyType
            };
            var inputParameter = new Parameter { Name = "prop", Type = flattenedPropertyType };
            serviceClient.Methods.Add(method);
            method.Parameters.Add(inputParameter);
            method.InputParameterTransformation.Add(new ParameterTransformation
            {
                OutputParameter = new Parameter { Name = "body", Type = customObjectType, SerializedName = "body" }
            });
            method.InputParameterTransformation.Last().ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = inputParameter,
                InputParameterProperty = "Id",
                OutputParameterProperty = "Id"
            });
            method.InputParameterTransformation.Last().ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = inputParameter,
                InputParameterProperty = "Sku",
                OutputParameterProperty = "Property.Sku"
            });
            method.InputParameterTransformation.Last().ParameterMappings.Add(new ParameterMapping
            {
                InputParameter = inputParameter,
                InputParameterProperty = "ProvState",
                OutputParameterProperty = "Property.ProvState"
            });            

            MethodTemplateModel templateModel = new MethodTemplateModel(method, serviceClient);
            var output = templateModel.BuildInputMappings();
            string expected =
          @"String paramA = null;
            if (body != null)
            {
                paramA = body.A;
            }
            String paramB = null;
            if (body != null)
            {
                paramB = body.B;
            }";

            MultilineAreEqual(expected, output.Trim());
        }

        private static void MultilineAreEqual(string expectedText, string actualText)
        {
            string[] expectedLines = expectedText.Split('\n').Select(p => p.TrimEnd('\r')).ToArray();
            string[] actualLines = actualText.Split('\n').Select(p => p.TrimEnd('\r')).ToArray();

            Assert.Equal(expectedLines.Length, actualLines.Length);

            for (int i = 0; i < expectedLines.Length; i++)
            {
                Assert.True(expectedLines[i].Trim().Equals(actualLines[i].Trim(), System.StringComparison.OrdinalIgnoreCase),
                    string.Format(CultureInfo.InvariantCulture, "Difference on line {0}.\r\nExpected: {1}\r\nActual: {2}", i, expectedLines[i], actualLines[i]));
            }
        }
    }
}