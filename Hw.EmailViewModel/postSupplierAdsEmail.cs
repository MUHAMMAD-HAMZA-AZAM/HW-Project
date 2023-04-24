using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hw.EmailViewModel
{
    public class PostSupplierAdsEmail
    {
        public string supplierName { get; set; }
        public string adTitle { get; set; }
        public string category { get; set; }
        public string address { get; set; }
        public string adRefrenceNumber { get; set; }
        public string email_ { get; set; }
        public Email Email { get; set; }
    }
}
