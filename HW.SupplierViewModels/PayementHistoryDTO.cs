using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public class PayementHistoryDTO
  {
    public long CustomerId { get; set; }
    public int NoOfRecords { get; set; }
    public long? OrderId { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal? TotalOrderCost { get; set; }
    public string PaymentMethod { get; set; }
    public bool? IsPaymentReceived { get; set; }
    public decimal TotalDeliveryCost { get; set; }
    public string PaymentStatusName { get; set; }
    public int PaymentStatusId { get; set; }
  }
}
