﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Rest.Modeler.Swagger.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Rest.Modeler.Swagger.JsonConverters
{
    public class PathItemRefConverter : SwaggerJsonConverter
    {
        public PathItemRefConverter(string json)
        {
            Document = JObject.Parse(json);
        }

        public override bool CanConvert(System.Type objectType)
        {
            // Type of a path item object
            return objectType == typeof (Dictionary<string, Operation>);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JObject jobject = JObject.Load(reader);
            // Unwrap if it's a reference object.
            while (jobject.First.Path == "$ref")
            {
                jobject =
                    Document.SelectToken(jobject.GetValue("$ref", StringComparison.Ordinal).ToString().
                    Replace("#/", "").Replace("/", ".")) as
                        JObject;
            }
            return JsonConvert.DeserializeObject<Dictionary<string, Operation>>(jobject.ToString(),
                GetSettings(serializer));
        }
    }
}