using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplirCommissionVM
    {
        public int Id { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int? NoOfRecords { get; set; }
        public string UserId { get; set; }
        public string SupplierName { get; set; }
        public long SupplierId { get; set; }
        public decimal? SupplierCommision { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
