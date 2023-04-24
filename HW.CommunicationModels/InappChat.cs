using System;
using System.Collections.Generic;

namespace HW.CommunicationModels
{
    public partial class InappChat
    {
        public long InappChatId { get; set; }
        public string ChatRoom { get; set; }
        public string ChatKey { get; set; }
        public int? UserRoleId { get; set; }
        public string Message { get; set; }
        public int? StatusId { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
