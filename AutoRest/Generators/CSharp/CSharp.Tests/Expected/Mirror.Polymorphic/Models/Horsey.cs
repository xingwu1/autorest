// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Fixtures.MirrorPolymorphic.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Horsey : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Horsey class.
        /// </summary>
        public Horsey() { }

        /// <summary>
        /// Initializes a new instance of the Horsey class.
        /// </summary>
        public Horsey(string id = default(string), string description = default(string), string breed = default(string))
            : base(id, description)
        {
            Breed = breed;
        }

        /// <summary>
        /// Gets or sets horse breed
        /// </summary>
        [JsonProperty(PropertyName = "breed")]
        public string Breed { get; set; }

    }
}
