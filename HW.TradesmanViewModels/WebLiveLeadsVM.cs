using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class WebLiveLeadsVM
    {
        public string WorkDescription { get; set; }
        public string WorkTitle { get; set; }
        public decimal WorkBudget { get; set; }
        public string CityName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Area { get; set; }
        public string GpsCoordinates { get; set; }
        public byte[] JobImage { get; set; }
        public long JobQuotationId { get; set; }
        public long TotalJobs { get; set; }

    }
}
