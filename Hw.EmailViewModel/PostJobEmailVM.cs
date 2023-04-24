using HW.CommunicationModels;
using System;

namespace Hw.EmailViewModel
{
    public class PostJobEmailVM
    {
        public string name { get; set; }
        public string email_ { get; set; }
        public string jobTitle { get; set; }
        public Email Email { get; set; }
    }
}
