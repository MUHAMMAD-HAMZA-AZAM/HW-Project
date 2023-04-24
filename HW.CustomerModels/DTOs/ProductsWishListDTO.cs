using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CustomerModels.DTOs
{
    public class ProductsWishListDTO
    {
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? SupplierId { get; set; }
        public long? ProductId { get; set; }
        public bool? IsFavorite { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int? noOfRecords { get; set; }

        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int? DiscountInPercentage { get; set; }

        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsMain { get; set; }
        public string Action { get; set; }

    }

}
