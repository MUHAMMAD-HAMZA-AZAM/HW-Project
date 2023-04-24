using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public class ProductCategoryGroupVM
  {
    public long Id { get; set; }
    public long ProductCategoryId { get; set; }
    public long ProductCategoryGroupId { get; set; }
    public string Name { get; set; }
    public string SubCategoryName { get; set; }
    public string CategoryName { get; set; }
    public long SubCategoryId { get; set; }
    public bool? Active { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public string SeoTitle { get; set; }
    public string SeoDescription { get; set; }
     public string Slug { get; set; }
     public string OgTitle { get; set; }
    public string OgDescription { get; set; }
     public string Canonical { get; set; }
    }
}
