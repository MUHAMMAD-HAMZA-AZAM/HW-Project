using System;
using System.Collections.Generic;

namespace HW.AnalyticsModels
{
    public partial class VersionControl
    {
        public long Id { get; set; }
        public string VersionCode { get; set; }
        public bool Flexable { get; set; }
        public int Application { get; set; }
        public int Os { get; set; }
    }
}
