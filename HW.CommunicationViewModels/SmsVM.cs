using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CommunicationViewModels
{
    public class SmsVM
    {
        public SmsVM()
        {
            MobileNumberList = new List<string>();
        }

        public string MobileNumber { get; set; }
        public List<string> MobileNumberList { get; set; }
        public string Message { get; set; }
      
       
    }
    public class SmsUsersVM
    {

        public string MobileNumber { get; set; }
        public string Message { get; set; }
        public string[] MobileNumberList { get; set; }
    }
}
