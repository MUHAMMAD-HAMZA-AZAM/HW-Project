using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CommunicationViewModels
{
    public class EmailVM
    {
        public List<string> EmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public List<string> Bcc { get; set; }
    }
}