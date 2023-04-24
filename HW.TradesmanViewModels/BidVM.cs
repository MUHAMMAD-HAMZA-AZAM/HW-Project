using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class BidVM
    {
        public long JobQuotationid { get; set; }
        public long BidId { get; set; }
        //public byte[] JobImage { get; set; }
        public string FileName { get; set; }
        public string WorkTitle { get; set; }
        public string JobDate { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string LastName { get; set; }
        public string WorkAddress { get; set; }
        public decimal Budget { get; set; }
        public string Town { get; set; }
        public string City { get; set; }

        /// <summary>
        /// Already use jobDate in string now i need to get job date in datatime 
        /// </summary>
        public DateTime Date { get; set; }

    }
    public class BidWebVM
    {
        public long JobQuotationid { get; set; }
        public byte[] BidImage { get; set; }
        public string FileName { get; set; }
        public string WorkTitle { get; set; }
        public string JobDate { get; set; }
        public string CustomerName { get; set; }
        public string LastName { get; set; }
        public string WorkAddress { get; set; }
        public decimal Budget { get; set; }

        /// <summary>
        /// Already use jobDate in string now i need to get job date in datatime 
        /// </summary>
        public DateTime Date { get; set; }
        public decimal CustomerBudget { get; set; }

    }
}
