namespace Template.Entities.Integration.Notification
{
    public class NotificationBody
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }

        public NotificationBody(string title, string body)
        {
            Title = title;
            Body = body;
        }
    }
}
