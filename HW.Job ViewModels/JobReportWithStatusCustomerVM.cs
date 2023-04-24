using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public class JobReportWithStatusCustomerVM
    {
        public string Customer { get; set; }
        public string Status { get; set; }
        public string TradesmanCompany { get; set; }
        public decimal TradesmanBudget  { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public DateTime  CreatedOn { get; set; }
        public long JobDetailId { get; set; }


    }
}
