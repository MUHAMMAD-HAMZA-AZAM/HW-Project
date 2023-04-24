using System;

namespace HW.TradesmanViewModels
{
    public class CompletedJobDetailVM
    {
        public long JobDetailId { get; set; }
        public string CustomerName { get; set; }
        public long customerId { get; set; }
        public long jobQuotationId { get; set; }
        public string JobTitle { get; set; }
        public DateTime JobStartedDate { get; set; }
        public DateTime JobFinishDate { get; set; }
        public decimal Payment { get; set; }
        public string LatLong { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string JobAddress { get; set; }
        public string MapAddress { get; set; }
    }
}
