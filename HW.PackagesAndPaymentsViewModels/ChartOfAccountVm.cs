using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels
{
    public class ChartOfAccountVm
    {
        public long? id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int parentLevel { get; set; }
        public bool? isControlAccount { get; set; }
        public long? parentIdLevel1 { get; set; }
        public long? parentIdLevel2 { get; set; }
        public long? parentIdLevel3 { get; set; }
    }
}
