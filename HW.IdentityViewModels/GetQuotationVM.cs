using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.IdentityViewModels
{
    public class GetQuotationVM
    {
        public long Category { get; set; }

        [Display(Name = "Sub Category")]
        public long SubCategory { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        public string Location { get; set; }

        public long City { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }
    }
}
