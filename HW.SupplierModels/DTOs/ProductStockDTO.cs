using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs
{
  public class ProductStockDTO
  {
    public long ProductId { get; set; }
    public long VarientId { get; set; }
    public int Quantity { get; set; }
  }
}
