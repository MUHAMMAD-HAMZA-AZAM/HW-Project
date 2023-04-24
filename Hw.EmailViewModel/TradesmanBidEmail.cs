using HW.CommunicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hw.EmailViewModel
{
    public class TradesmanBidEmail
    {
        public string tradesmanName { get; set; }
        public string jobTitle { get; set; }
        public string customerName { get; set; }
        public string tradesmanCategory { get; set; }
        //public string distance { get; set; }
        public string jobBudget { get; set; }
        public string bidAmount { get; set; }
        public string emailTradesman { get; set; }
        public Email Email { get; set; }
    }
}
 