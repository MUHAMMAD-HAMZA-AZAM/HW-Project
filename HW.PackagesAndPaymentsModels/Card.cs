using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class Card
    {
        public long CardId { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedOn { get; set; }
        public int CustomerRole { get; set; }
        public long? BookId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
