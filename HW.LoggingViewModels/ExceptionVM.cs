using HW.Utility;

namespace HW.LoggingViewModels
{
    public class ExceptionVM
    {
        public string ErrorType { get; set; }
        public string MobileModel { get; set; }
        public string OsVersion { get; set; }
        public string CpuType { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorScreen { get; set; }
        public TargetDatabase Activity { get; set; }
    }
}
