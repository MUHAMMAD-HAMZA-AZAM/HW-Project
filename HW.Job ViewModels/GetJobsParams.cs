using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
  public class GetJobsParams
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string DataOrderBy { get; set; }
    public long CustomerId { get; set; }
  }
}
