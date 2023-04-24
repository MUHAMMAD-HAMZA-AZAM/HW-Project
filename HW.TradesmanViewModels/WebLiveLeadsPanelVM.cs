using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class WebLiveLeadsPanelVM
    {
        public List<WebLiveLeadsVM> ActiveLiveLeads = new List<WebLiveLeadsVM>();
        public List<WebLiveLeadsVM> InProgressLiveLeads = new List<WebLiveLeadsVM>();
        public List<WebLiveLeadsVM> CompletedLiveLeads = new List<WebLiveLeadsVM>();
        public long ActiveLiveLeadsCount { get; set; }
        public long InProgressLiveLeadsCount { get; set; }
        public long CompletedLiveLeadsCount { get; set; }
    }
}
