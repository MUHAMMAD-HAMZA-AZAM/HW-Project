using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public class PayementHistoryDetailDTO
  {
    public string SupplierName { get; set; }
    public long ProductId { get; set; }
    public string Title { get; set; }
    public string PaymentStatusName { get; set; }
    public int NoOfRecords { get; set; }
    public decimal ItemCost { get; set; }
    public decimal TotalItemCost { get; set; }
    public long SupplierId { get; set; }
    public long OrderId { get; set; }
    public string CustomerName { get; set; }
  }
}
