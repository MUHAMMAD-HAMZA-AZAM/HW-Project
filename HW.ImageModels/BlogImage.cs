using System;
using System.Collections.Generic;

namespace HW.ImageModels
{
    public partial class BlogImage
    {
        public long BlogImageId { get; set; }
        public string FileName { get; set; }
        public byte[] BlogImage1 { get; set; }
        public long BlogId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
