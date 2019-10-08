using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Template.Entities.Integration.Notification.Firebase
{
    public class NotificationResponse
    {
        [JsonProperty(PropertyName = "message_id")]
        [DeserializeAs(Name = "message_id")]
        public string MessageId { get; set; }

        [JsonProperty(PropertyName = "failure")]
        [DeserializeAs(Name = "failure")]
        public int FailureAmount { get; set; }  

        [JsonProperty(PropertyName = "success")]
        [DeserializeAs(Name = "success")]
        public int SuccessAmount { get; set; }

        [JsonProperty(PropertyName = "multicast_id")]
        [DeserializeAs(Name = "multicast_id")]
        public string MulticastId { get; set; }

        [JsonProperty(PropertyName = "results")]
        [DeserializeAs(Name = "results")]
        public IEnumerable<Result> Results { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "error")]
        [DeserializeAs(Name = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "message_id")]
        [DeserializeAs(Name = "message_id")]
        public string MessageId { get; set; }

        [JsonProperty(PropertyName = "registration_id")]
        [DeserializeAs(Name = "registration_id")]
        public string RegistrationId { get; set; }
    }
}
