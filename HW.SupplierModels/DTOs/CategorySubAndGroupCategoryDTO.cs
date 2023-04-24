using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
    public class CategorySubAndGroupCategoryDTO
    {
        public CategorySubAndGroupCategoryDTO()
        {
            Categories = new List<IdValueDTO>();
            SubCategories = new List<IdValueDTO>();
            GroupCategories = new List<IdValueDTO>();
            ProductVariants = new List<IdValueDTO>();
        }
        public List<IdValueDTO> Categories { get; set; }
        public List<IdValueDTO> SubCategories { get; set; }
        public List<IdValueDTO> GroupCategories { get; set; }
        public List<IdValueDTO> ProductVariants { get; set; }
    }
}
