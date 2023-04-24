using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public partial class SendFeedBackEmail
  {
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
    public string EmailAddress { get; set; }
    public string FullName { get; set; }
    public int NoOfEmails { get; set; }
  }
}
