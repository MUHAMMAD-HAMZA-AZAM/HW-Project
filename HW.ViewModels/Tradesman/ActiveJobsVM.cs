using HW.CustomerModels;
using HW.ImageModels;
using HW.JobModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ViewModels.Tradesman
{
    public class BidVM
    {
        public long JobQuotationid { get; set; }
        public byte[] JobImage { get; set; }
        public string WorkTitle { get; set; }
        public string JobDate { get; set; }
        public string CustomerName { get; set; }
        public string WorkAddress { get; set; }
    }
}


