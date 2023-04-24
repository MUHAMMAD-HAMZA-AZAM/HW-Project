using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CommunicationViewModels
{
    public class InappChatVM
    {
        public long InappChatId { get; set; }
        public string ChatRoom { get; set; }
        public string ChatKey { get; set; }
        public int UserRoleId { get; set; }
        public string Message { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateSent { get; set; }
    }
}
