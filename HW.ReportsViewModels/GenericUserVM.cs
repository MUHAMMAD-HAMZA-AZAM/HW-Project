using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ReportsViewModels
{
    public class GenericUserVM
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string dataOrderBy { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string userName { get; set; }
        public string categories { get; set; }
        public string city { get; set; }
        public string skills { get; set; }
        public string location { get; set; }
        public string mobile { get; set; }
        public string usertype { get; set; }
        public string emailtype { get; set; }
        public string mobileType {get;set;}
        public string jobsType { get; set; }
        public string activityType { get; set; }
        public string usertypeid { get; set; }
        public bool isOrganisation { get; set; }
        public string cnic { get; set; }
        public string sourceOfReg { get; set; }
        public string email { get; set; }
        public long? id { get; set; }
        public string SalesmanId { get; set; }
        public string customerId { get; set; }
        public string tradesmanId { get; set; }
    }
}
