
namespace Petstore.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// The CheckNameAvailability operation response.
    /// </summary>
    public partial class CheckNameAvailabilityResult
    {
        /// <summary>
        /// Initializes a new instance of the CheckNameAvailabilityResult
        /// class.
        /// </summary>
        public CheckNameAvailabilityResult() { }

        /// <summary>
        /// Initializes a new instance of the CheckNameAvailabilityResult
        /// class.
        /// </summary>
        public CheckNameAvailabilityResult(bool? nameAvailable = default(bool?), Reason? reason = default(Reason?), string message = default(string))
        {
            NameAvailable = nameAvailable;
            Reason = reason;
            Message = message;
        }

        /// <summary>
        /// Gets or sets gets a boolean value that indicates whether the name
        /// is available for you to use. If true, the name is available. If
        /// false, the name has already been taken or invalid and cannot be
        /// used.
        /// </summary>
        [JsonProperty(PropertyName = "nameAvailable")]
        public bool? NameAvailable { get; set; }

        /// <summary>
        /// Gets or sets gets the reason that a storage account name could not
        /// be used. The Reason element is only returned if NameAvailable is
        /// false. Possible values include: 'AccountNameInvalid',
        /// 'AlreadyExists'
        /// </summary>
        [JsonProperty(PropertyName = "reason")]
        public Reason? Reason { get; set; }

        /// <summary>
        /// Gets or sets gets an error message explaining the Reason value in
        /// more detail.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

    }
}
