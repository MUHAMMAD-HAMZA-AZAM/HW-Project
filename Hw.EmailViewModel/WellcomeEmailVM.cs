using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.EmailViewModel
{
    public class WellcomeEmailVM
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Email Email { get; set; }
    }
}
