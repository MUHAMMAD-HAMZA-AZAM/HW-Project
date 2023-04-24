using System;
using System.Collections.Generic;

namespace HW.CommunicationModels
{
    public partial class CallRequest
    {
        public long RequestId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
