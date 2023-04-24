using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.IdentityViewModels
{
    public class GetQuotation2VM
    {
        public long Bids { get; set; }

        [Display(Name = "Job Start Date")]
        public string JobStartDate { get; set; }

        [Display(Name = "Job Start Time")]
        public string JobStartTime { get; set; }

        public decimal Budget { get; set; }
    }
}
