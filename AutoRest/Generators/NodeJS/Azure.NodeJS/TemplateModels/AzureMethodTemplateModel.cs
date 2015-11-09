﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net;
using Microsoft.Rest.Generator.ClientModel;
using Microsoft.Rest.Generator.NodeJS;
using Microsoft.Rest.Generator.Utilities;

namespace Microsoft.Rest.Generator.Azure.NodeJS
{
    public class AzureMethodTemplateModel : MethodTemplateModel
    {
        public AzureMethodTemplateModel(Method source, ServiceClient serviceClient)
            : base(source, serviceClient)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            
            this.ClientRequestIdString = AzureCodeGenerator.GetClientRequestIdString(source);
            this.RequestIdString = AzureCodeGenerator.GetRequestIdString(source);
        }
        
        public string ClientRequestIdString { get; private set; }

        public string RequestIdString { get; private set; }

        /// <summary>
        /// Returns true if method has x-ms-long-running-operation extension.
        /// </summary>
        public bool IsLongRunningOperation
        {
            get { return Extensions.ContainsKey(AzureCodeGenerator.LongRunningExtension); }
        }

        /// <summary>
        /// If this is a relative uri, we will add api-version query, so add this condition to the check
        /// </summary>
        /// <returns>true if there are any query parameters in the uri, otherwise false</returns>
        protected override bool HasQueryParameters()
        {
            return base.HasQueryParameters() || !IsAbsoluteUrl;
        }


        public override string InitializeResult
        {
            get
            {
                var sb = new IndentedStringBuilder();
                if (this.HttpMethod == HttpMethod.Head &&
                    this.ReturnType != null)
                {
                    HttpStatusCode code = this.Responses.Keys.FirstOrDefault(AzureCodeGenerator.HttpHeadStatusCodeSuccessFunc);
                    sb.AppendFormat("result = (statusCode === {0});", (int)code).AppendLine();
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the expression for default header setting. 
        /// </summary>
        public override string SetDefaultHeaders
        {
            get
            {
                var sb = new IndentedStringBuilder();
                sb.AppendLine("httpRequest.headers['{0}'] = msRestAzure.generateUuid();", this.ClientRequestIdString)
                  .AppendLine(base.SetDefaultHeaders);
                return sb.ToString();
            }
        }

        public string LongRunningOperationMethodNameInRuntime
        {
            get
            {
                string result = null;
                if (this.IsLongRunningOperation)
                {
                    if (HttpMethod == HttpMethod.Post || HttpMethod == HttpMethod.Delete)
                    {
                        result = "getPostOrDeleteOperationResult";
                    }
                    else if (HttpMethod == HttpMethod.Put || HttpMethod == HttpMethod.Patch)
                    {
                        result = "getPutOrPatchOperationResult";
                    }
                }
                return result;
            }
        }
    }
}