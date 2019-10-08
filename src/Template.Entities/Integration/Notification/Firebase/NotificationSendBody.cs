using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Template.Entities.Integration.Notification.Firebase
{
    public class NotificationSendBody
    {
        [JsonProperty(PropertyName = "to")]
        [DeserializeAs(Name = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "notification")]
        [DeserializeAs(Name = "notification")]
        public FirebaseNotification FirebaseNotification { get; set; }

        public NotificationSendBody(string to, FirebaseNotification firebaseNotification)
        {
            To = to;
            FirebaseNotification = firebaseNotification;
        }
    }

    public class FirebaseNotification
    {
        [JsonProperty(PropertyName = "title")]
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "body")]
        [DeserializeAs(Name = "body")]
        public string Body { get; set; }

        public FirebaseNotification(string body, string title)
        {
            Body = body;
            Title = title;
        }
    }
}
