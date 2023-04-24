using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ViewModels.Tradesman
{
    public class DeclinedBidVM
    {
        public long JobQuotationid { get; set; }
        public string WorkTitle { get; set; }
        public string JobPostedDate { get; set; }
        public string CustomerName { get; set; }
        public string WorkAddress { get; set; }
    }
}
