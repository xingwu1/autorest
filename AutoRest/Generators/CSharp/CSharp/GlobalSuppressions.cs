﻿// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.MethodGroupTemplateModel.#MethodTemplateModels", 
    Justification = "Generic list is the best type that provides the needed OM (RemoveAll and AddRange).  Moving to a type like Collection would add unnecessary complexity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#ParameterTemplateModels", 
    Justification = "Generic list is the best type that provides the needed OM (RemoveAll and AddRange).  Moving to a type like Collection would add unnecessary complexity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.ModelTemplateModel.#PropertyTemplateModels", 
    Justification = "Generic list is the best type that provides the needed OM (RemoveAll and AddRange).  Moving to a type like Collection would add unnecessary complexity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.ServiceClientTemplateModel.#MethodTemplateModels", 
    Justification = "Generic list is the best type that provides the needed OM (RemoveAll and AddRange).  Moving to a type like Collection would add unnecessary complexity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.ExtensionsTemplateModel.#MethodTemplateModels", 
    Justification = "Generic list is the best type that provides the needed OM (RemoveAll and AddRange).  Moving to a type like Collection would add unnecessary complexity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", 
    Scope = "namespace", Target = "Microsoft.Rest.Generator.CSharp.TemplateModels", Justification="Keep parallelism in namespaces between generators")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", 
    MessageId = "0#", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#RemoveDuplicateForwardSlashes(System.String)", Justification="This is a Uri that may not pass Uri validation rules")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", 
    Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#BuildUrl(System.String)", Justification="This is a Uri that may not pass Uri validation rules")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", 
    "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#RemoveDuplicateForwardSlashes(System.String)", 
    Justification="The string is generated code, it is much more readable and maintainable if this is a literal rather than a string resource, " + 
    "and there are no globalization concerns for source code.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", 
    "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#BuildUrl(System.String)", 
    Justification="The string is generated code, it is much more readable and maintainable if this is a literal rather than a string resource, " + 
    "and there are no globalization concerns for source code.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", 
    "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", 
    Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.TemplateModels.ClientModelExtensions.#CheckNull(System.String,System.String)",
    Justification="The string is generated code, it is much more readable and maintainable if this is a literal rather than a string resource, " + 
    "and there are no globalization concerns for source code.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", 
    "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", 
    Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.TemplateModels.ClientModelExtensions.#ValidateType(Microsoft.Rest.Generator.ClientModel.IType,Microsoft.Rest.Generator.CSharp.IScopeProvider,System.String)",
    Justification = "The string is generated code, it is much more readable and maintainable if this is a literal rather than a string resource, " +
    "and there are no globalization concerns for source code.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", 
    Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.TemplateModels.ClientModelExtensions.#ValidateType(Microsoft.Rest.Generator.ClientModel.IType,Microsoft.Rest.Generator.CSharp.IScopeProvider,System.String,System.Collections.Generic.Dictionary`2<Microsoft.Rest.Generator.ClientModel.Constraint,System.String>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", 
    MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", 
    Scope = "member", 
    Target = "Microsoft.Rest.Generator.CSharp.TemplateModels.ClientModelExtensions.#AppendConstraintValidations(System.String,System.Collections.Generic.Dictionary`2<Microsoft.Rest.Generator.ClientModel.Constraint,System.String>,Microsoft.Rest.Generator.Utilities.IndentedStringBuilder)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#.ctor(Microsoft.Rest.Generator.ClientModel.Method,Microsoft.Rest.Generator.ClientModel.ServiceClient)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#.ctor(Microsoft.Rest.Generator.ClientModel.Method,Microsoft.Rest.Generator.ClientModel.ServiceClient)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.ModelTemplateModel.#.ctor(Microsoft.Rest.Generator.ClientModel.CompositeType)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#GroupedParameterTemplateModels")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#LogicalParameterTemplateModels")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Rest.Generator.Utilities.IndentedStringBuilder.AppendLine(System.String)", Scope = "member", Target = "Microsoft.Rest.Generator.CSharp.MethodTemplateModel.#BuildInputMappings()")]

