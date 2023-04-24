namespace HW.NotificationViewModels
{
    public class TopicNotificationPayload
    {
        public string condition { get; set; }
        public TopicPayload notification { get; set; }
        public Data data { get; set; }
    }

    public class TopicPayload
    {
        public string title { get; set; }
        public string body { get; set; }
        public string sound { get; set; }
    }
}
