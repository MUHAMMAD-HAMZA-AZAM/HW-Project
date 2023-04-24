using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class BankDTO
    {
        public long BankId { get; set; }
        public string BankName { get; set; }
        public bool? Active { get; set; }
    }
}
