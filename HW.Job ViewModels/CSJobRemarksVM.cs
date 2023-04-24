using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class CSJobRemarksVM
    {
        public long RemarksId { get; set; }
        public long? JobQuotationId { get; set; }
        public string RemarksDescription { get; set; }
        public int? CallTries { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
