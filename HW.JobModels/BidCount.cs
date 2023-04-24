namespace HW.JobModels
{
    public class BidCount
    {
        public long JobQuotationId { get; set; }
        public long? TradesmanId { get; set; }
        public int Bids { get; set; }
    }
}
