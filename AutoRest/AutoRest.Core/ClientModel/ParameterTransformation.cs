﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Rest.Generator.ClientModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines a parameter transformation.
    /// </summary>
    public class ParameterTransformation : ICloneable
    {
        public ParameterTransformation()
        {
            ParameterMappings = new List<ParameterMapping>();
        }
        /// <summary>
        /// Gets or sets the output parameter.
        /// </summary>
        public Parameter OutputParameter { get; set; }

        /// <summary>
        /// Gets the list of Parameter Mappings
        /// </summary>
        public IList<ParameterMapping> ParameterMappings { get; private set; }

        /// <summary>
        /// Returns a string representation of the ParameterMapping object.
        /// </summary>
        /// <returns>
        /// A string representation of the ParameterMapping object.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (OutputParameter != null)
            {
                sb.AppendLine("var " + OutputParameter.Name);
                foreach (var mapping in ParameterMappings)
                {
                    sb.AppendLine(OutputParameter.Name + mapping.ToString());
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Performs a deep clone of a parameter transformation.
        /// </summary>
        /// <returns>A deep clone of current object.</returns>
        public object Clone()
        {
            ParameterTransformation paramTransformation = (ParameterTransformation)this.MemberwiseClone();
            return paramTransformation;
        }
    }
}