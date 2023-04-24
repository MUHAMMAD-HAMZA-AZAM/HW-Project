using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.EmailViewModel
{
    public class AdminForgotEmail
    {
        public string RecivereEmail { get; set; }
        public string UserId { get; set; }
        public Email Email { get; set; }
    }
}
