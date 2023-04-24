using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.EmailViewModel
{
    public class ContactEmailVm
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SecKey { get; set; }
        public Email Email { get; set; }
    }
}
