using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class CallInfoVM
    {
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public byte[] CustomerImage { get; set; }
        public List<CallLogVM> CallLogs { get; set; }
    }
}
