﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class ImageVM
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public bool IsMain { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageBase64 { get; set; }
        public string ThumbImage { get; set; }
        public byte[] ThumbImageContent { get; set; }
    }
}
