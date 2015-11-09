﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Rest.Generator.ClientModel
{
    /// <summary>
    /// Defines model data type.
    /// </summary>
    public class CompositeType : IType
    {
        /// <summary>
        /// Initializes a new instance of CompositeType class.
        /// </summary>
        public CompositeType()
        {
            Extensions = new Dictionary<string, object>();
            Properties = new List<Property>();
        }

        /// <summary>
        /// Gets or sets the model type name used in on the wire.
        /// </summary>
        public string SerializedName { get; set; }

        /// <summary>
        /// Gets or sets the list of CompositeType properties.
        /// </summary>
        public IList<Property> Properties { get; private set; }

        /// <summary>
        /// Gets the union of Parent and current type properties
        /// </summary>
        public IEnumerable<Property> ComposedProperties
        {
            get
            {
                if (BaseModelType != null)
                {
                    return BaseModelType.ComposedProperties.Union(Properties);
                }

                return this.Properties;
            }
        }

        /// <summary>
        /// Gets or sets the base model type.
        /// </summary>
        public CompositeType BaseModelType { get; set; }

        /// <summary>
        /// Gets or sets the discriminator property for polymorphic types.
        /// </summary>
        public string PolymorphicDiscriminator { get; set; }

        /// <summary>
        /// Gets or sets the CompositeType documentation.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets a dictionary of x-vendor extensions defined for the CompositeType.
        /// </summary>
        public Dictionary<string, object> Extensions { get; private set; }

        /// <summary>
        /// Gets or sets the CompositeType name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns a string representation of the CompositeType object.
        /// </summary>
        /// <returns>
        /// A string representation of the CompositeType object.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this object based on the Name.
        /// </summary>
        /// <param name="obj">The object to compare with this object.</param>
        /// <returns>true if the specified object is equal to this object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var modelType = obj as CompositeType;

            if (modelType != null)
            {
                return modelType.Name == Name;
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function based on Name.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            return Name == null ? 0 : Name.GetHashCode();
        }
    }
}