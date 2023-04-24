using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.EmailViewModel
{
    public class FeedBackEmailVM
    {
      public string EmailAddress { get; set; }
      public string Subject { get; set; }
      public string Message { get; set; }
      public string FullName { get; set; }
      public Email Email { get; set; }
    }
}
