using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class AnalyticsVM
    {
        public long? Id { get; set; }
        public string Ip { get; set; }
        public string Device { get; set; }
        public string Platform { get; set; }
        public string Os { get; set; }
        public string OsVersion { get; set; }
        public string IpLocation { get; set; }
        public string ApplicationType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ApplicaitonVersion { get; set; }
        public bool? MobileDevice { get; set; }
        public bool? TabletDevice { get; set; }
        public bool? DesktopDevice { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string Country { get; set; }
        public string CountryCapital { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Name { get; set; }
        public string userRole { get; set; }
        public int? roleId { get; set; }
        public long? userId { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? noOfRecoards { get; set; }
    }
}
