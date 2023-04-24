using HW.SupplierModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class FeaturedSupplier
    {
            public long? supplierId { get; set; }
            public string action { get; set; }
            public bool featuredSupplier { get; set; }
            public bool? imageStatus1 { get; set; }
            public bool? imageStatus2 { get; set; }
            public bool? imageStatus3 { get; set; }
            public int imageId1 { get; set; }
            public int imageId2 { get; set; }
            public int imageId3 { get; set; }
            //public byte[] croppedImage1 { get; set; }
            //public byte[] croppedImage2 { get; set; }
            //public byte[] croppedImage3 { get; set; }
            //public string base64Image1 { get; set; }
            //public string base64Image2 { get; set; }
            //public string base64Image3 { get; set; }
            public string[] base64ImageArray { get; set; }
            //public List<FeaturedSupplierImageVM> featuredSupplierImages { get; set;}

    }
}
