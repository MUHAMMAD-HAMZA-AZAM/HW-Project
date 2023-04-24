using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class PostAdPromoteVM
    {
        public byte[] AdImage { get; set; }
        public long PostAdId { get; set; }
        public string SubCategory { get; set; }
        public string AdTitle { get; set; }
    }
}
