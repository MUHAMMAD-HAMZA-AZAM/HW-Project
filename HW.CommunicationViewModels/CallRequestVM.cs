using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CommunicationViewModels
{
    public class CallRequestVM
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public long RequestId { get; set; }

        public int CallRequest { get; set; }

    }
}
