using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public class OrderDetailDTO
  {
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public string CityName { get; set; }
  }
}
