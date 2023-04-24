﻿using System;
using System.Collections.Generic;

namespace HW.JobModels
{
    public partial class CsjobRemarks
    {
        public long RemarksId { get; set; }
        public long? JobQuotationId { get; set; }
        public string RemarksDescription { get; set; }
        public int? CallTries { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
