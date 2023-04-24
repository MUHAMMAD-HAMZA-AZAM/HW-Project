using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class ProductCategoryVM
    {
        public long ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public byte[] ProductCategoryImage { get; set; }
        public int StaticImage { get; set; }
    }
}
