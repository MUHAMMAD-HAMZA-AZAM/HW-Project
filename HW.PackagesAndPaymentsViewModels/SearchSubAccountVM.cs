using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
  public class SearchSubAccountVM
  {
    public int? pageSize { get; set; }
    public int? pageNumber { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public long? UserId { get; set; }
    public string UserName { get; set; }
    public string SubAccountNo { get; set; }
    public string SubAccountName { get; set; }
  }
}
