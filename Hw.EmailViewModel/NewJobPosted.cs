using HW.CommunicationModels;
using HW.TradesmanModels;
using System.Collections.Generic;

namespace HW.EmailViewModel
{
    public class NewJobPosted
    {
        public string UserName { get; set; }
        public string TradeCategory { get; set; }
        public string  JobDescription { get; set; }
        public string Distance { get; set; }
        public string  JobCity { get; set; }
        public List<string> TradesmanEmail { get; set; }
        public Email Email { get; set; }


    }
}
