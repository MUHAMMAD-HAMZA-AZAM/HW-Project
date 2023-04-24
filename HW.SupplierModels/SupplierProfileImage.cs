﻿using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class SupplierProfileImage
    {
        public long ProfileImageId { get; set; }
        public long SupplierId { get; set; }
        public byte[] ProfileImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
