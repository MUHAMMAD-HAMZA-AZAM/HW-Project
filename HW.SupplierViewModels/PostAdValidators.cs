using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.SupplierViewModels
{
    public class PostAdValidators
    {
        public long Category { get; set; }

        [Display(Name = "Sub Category")]
        public long SubCategory { get; set; }

        [Display(Name = "Ad Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Ad Description")]
        public string JobDescription { get; set; }

        public decimal Price { get; set; }

        public long City { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }
        public bool collectionAvailabels { get; set; }
    }
}
