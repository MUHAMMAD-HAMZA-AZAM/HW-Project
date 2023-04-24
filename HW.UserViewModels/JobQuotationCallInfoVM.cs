using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class JobQuotationCallInfoVM
    {
        public long TradesmanID { get; set; }
        public string TradesmanName { get; set; }
        public byte[] TradesmanImage { get; set; }
        public List<CallLogVM> CallLogs { get; set; }
    }

    public class CallLogVM
    {
        public string CallType { get; set; }
        public int? CallDuration { get; set; }
    }
}
