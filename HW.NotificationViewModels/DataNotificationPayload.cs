namespace HW.NotificationViewModels
{
    public class DataNotificationPayload
    {
        public string to { get; set; }
        public DataPayload notification { get; set; }
        public Data data { get; set; }
    }

    public class DataPayload
    {
        public string title { get; set; }
        public string body { get; set; }
        public string sound { get; set; }
        public string notificationId { get; set; }
    }
}
