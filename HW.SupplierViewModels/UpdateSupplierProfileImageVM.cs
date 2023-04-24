using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW.SupplierViewModels
{
    public class UpdateSupplierProfileImageVM
    {
        public long SupplierId { get; set; }
        public byte[] ProfileImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [NotMapped]
        public string ImageBase64 { get; set; }
    }
}
