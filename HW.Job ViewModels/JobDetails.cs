using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public class JobDetails
    {
        public string Skill { get; set; }
        public string subSkill { get; set; }
        public string WorkTitle { get; set; }
        public string WorkDescription { get; set; }
        public decimal WorkBudget { get; set; }
        public DateTime WorkStartDate { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public long TradesmanId { get; set; }
        public decimal TradesmanBudget { get; set; }
        public DateTime EndDate { get; set; }

    }
}
