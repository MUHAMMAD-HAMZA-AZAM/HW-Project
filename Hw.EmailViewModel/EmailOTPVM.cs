using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.EmailViewModel
{
    public class EmailOTPVM
    {
        public string UserEmail { get; set; }
        public string OtpCode { get; set; }
        public string Role{ get; set; }
        public string FAQs { get; set; }
        public string Terms { get; set; }
        public string MyAccount { get; set; }
        public string GooglePlayStoreUrl { get; set; }
        public Email Email { get; set; }
    }
}
