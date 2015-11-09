﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Rest.Generator.ClientModel;
using Microsoft.Rest.Generator.Utilities;

namespace Microsoft.Rest.Generator.Ruby
{
    /// <summary>
    /// The model for the enum template.
    /// </summary>
    public class EnumTemplateModel : EnumType
    {
        /// <summary>
        /// Initializes a new instance of the class EnumTemplateModel.
        /// </summary>
        /// <param name="source">The source object.</param>
        public EnumTemplateModel(EnumType source)
        {
            this.LoadFrom(source);
        }

        /// <summary>
        /// Gets the trimmed name so that '?'
        /// does not occur in the type definition
        /// </summary>
        public string TypeDefinitionName
        {
            get
            {
                return this.Name.TrimEnd('?');
            }
        }
    }
}