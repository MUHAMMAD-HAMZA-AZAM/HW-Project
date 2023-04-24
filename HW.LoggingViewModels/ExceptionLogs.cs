using System;
using System.Collections.Generic;

namespace HW.LoggingViewModels
{
    public partial class ExceptionLogs
    {
        public long ExceptionId { get; set; }
        public string CpuType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorScreen { get; set; }
        public string ErrorType { get; set; }
        public string MobleModel { get; set; }
        public string OsVersion { get; set; }
        public int? Application { get; set; }
    }
}
