using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class BidsVM
    {
        public long JobQuotationid { get; set; }
        public byte[] JobImage { get; set; }
        public string WorkTitle { get; set; }
        public string JobDate { get; set; }
        public string CustomerName { get; set; }
        public string WorkAddress { get; set; }
        public decimal Budget { get; set; }
    }
}
