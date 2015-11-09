﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.Rest.Generator.ClientModel;

namespace Microsoft.Rest.Generator.CSharp.Azure
{
    public class AzureParameterTemplateModel : ParameterTemplateModel
    {
        public AzureParameterTemplateModel(Parameter source) : base(source)
        {
        }

        /// <summary>
        /// Gets declaration for the parameter.
        /// </summary>
        public override string DeclarationExpression
        {
            get
            {
                if (IsODataFilterExpression)
                {
                    return string.Format(CultureInfo.InvariantCulture, 
                        "Expression<Func<{0}, bool>>", Type.Name);
                }

                return base.DeclarationExpression;
            }
        }

        /// <summary>
        /// Gets True if parameter can call .Validate method
        /// </summary>
        public override bool CanBeValidated
        {
            get
            {
                return !IsODataFilterExpression;
            }
        }

        /// <summary>
        /// Gets True if parameter is OData $filter expression
        /// </summary>
        public virtual bool IsODataFilterExpression
        {
            get
            {
                return SerializedName.Equals("$filter", StringComparison.OrdinalIgnoreCase) &&
                       Location == ParameterLocation.Query &&
                       Type is CompositeType;
            }
        }
    }
}