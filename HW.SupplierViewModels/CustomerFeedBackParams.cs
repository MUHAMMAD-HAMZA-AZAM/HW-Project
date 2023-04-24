using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public class CustomerFeedBackParams
  {
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
    public int PagesNumber { get; set; }
    public int PageSize { get; set; }
  }
}
