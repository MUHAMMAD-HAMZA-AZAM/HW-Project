using System;
using System.Collections.Generic;

#nullable disable

namespace HW.SupplierModels
{
    public partial class BankDetail
    {
        public long Id { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }
        public long BankId { get; set; }
        public long SupplierId { get; set; }
        public string BranchCode { get; set; }
        public string Iban { get; set; }
        public byte[] ChequeImage { get; set; }
        public string ChequeImageName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
