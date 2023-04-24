using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class ProductImageDTO
    {
        public long? ProductId { get; set; }
        public long? FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string YoutubeURL { get; set; }
        public decimal Weight { get; set; }
        public bool? IsActive { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryGroupName { get; set; }



    }
}
