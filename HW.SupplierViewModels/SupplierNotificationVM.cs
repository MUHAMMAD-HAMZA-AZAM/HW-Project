using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierNotificationVM
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string SenderEntityId { get; set; }
        public string TargetActivity { get; set; }
        public string SenderUserId { get; set; }
        public string TragetUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public TargetDatabase TargetDatabase { get; set; }
    }
}
