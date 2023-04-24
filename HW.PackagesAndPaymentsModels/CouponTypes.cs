using System;
using System.Collections.Generic;

namespace HW.PackagesAndPaymentsModels
{
    public partial class CouponTypes
    {
        public long CouponTypeId { get; set; }
        public string CouponTypeCode { get; set; }
        public string CouponTypeName { get; set; }
        public string EntityStatus { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
