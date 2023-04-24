using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class JournalEntryHeader
    {
        public int JournalEntryHeaderId { get; set; }
        public string ReferenceNo { get; set; }
        public string Narration { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Notes { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? PostedDate { get; set; }
    }
}
