﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net.Http;
using Newtonsoft.Json;

namespace Microsoft.Rest.Azure
{
    /// <summary>
    /// Interface for all Microsoft Azure clients.
    /// </summary>
    public interface IAzureClient
    {
        /// <summary>
        /// Gets subscription credentials which uniquely identify Microsoft
        /// Azure subscription. The subscription ID forms part of the URI for
        /// every service call.
        /// </summary>
        ServiceClientCredentials Credentials { get; }

        /// <summary>
        /// Gets the HttpClient used for making HTTP requests.
        /// </summary>
        HttpClient HttpClient { get; }
        
        /// <summary>
        /// Gets the retry timeout for Long Running Operations.
        /// </summary>
        int? LongRunningOperationRetryTimeout { get; }
        
        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }        
    }
}
