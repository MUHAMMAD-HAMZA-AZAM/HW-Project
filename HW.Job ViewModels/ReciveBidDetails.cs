using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
    public class ReciveBidDetails
    {
        public long TradesmanId { get; set; }
        public string Title { get; set; }
        public decimal Budget { get; set; }
        public decimal TradesmanBudget { get; set; }
        public string Description { get; set; }
        public string address { get; set; }

    }
}
