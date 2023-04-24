using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierListVM
    {
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public long ImageId { get; set; }
        public string LastName { get; set; }
        public long SupplierId { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
