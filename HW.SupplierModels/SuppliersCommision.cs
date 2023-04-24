using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels
{
   public partial class SuppliersCommision
    {
        public int Id { get; set; }
        public long SupplierId { get; set; }
        public decimal? SupplierCommision { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
