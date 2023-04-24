using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierSliderDTO
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string LocalImageName { get; set; }
        public string ImagePath { get; set; }
        public bool? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
