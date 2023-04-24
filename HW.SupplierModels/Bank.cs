using System;
using System.Collections.Generic;

#nullable disable

namespace HW.SupplierModels
{
    public partial class Bank
    {
        public long BankId { get; set; }
        public string BankName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
