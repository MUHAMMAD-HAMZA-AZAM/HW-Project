using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Utility
{
    public interface IApiConfig
    {
        string GatewayApiUrl { get; set; }
        string AudioApiUrl { get; set; }
        string CallApiUrl { get; set; }
        string CustomerApiUrl { get; set; }
        string PromotionApiUrl { get; set; }
        string EstateAgentApiUrl { get; set; }
        string IdentityServerApiUrl { get; set; }
        string ImageApiUrl { get; set; }
        string JobApiUrl { get; set; }
        string AnalyticsApiUrl { get; set; }
        string UserManagementApiUrl { get; set; }
        string LoggingApiUrl { get; set; }
        string NotificationApiUrl { get; set; }
        string PaymentApiUrl { get; set; }
        string PackagesAndPaymentsApi { get; set; }
        string  CMSApiUrl { get; set; }
        string PurchaseApiUrl { get; set; }
        string SupplierApiUrl { get; set; }
        string TradesmanApiUrl { get; set; }
        string CommunicationApiUrl { get; set; }
        string VideoApiUrl { get; set; }
        string AdminApiUrl { get; set; }
        string ElmahApiUrl { get; set; }
        string BaseUrl { get; set; }
    }
    public class ApiConfig : IApiConfig
    {
        public string GatewayApiUrl { get; set; }
        public string AudioApiUrl { get; set; }
        public string PromotionApiUrl { get; set; }
        public string CallApiUrl { get; set; }
        public string AnalyticsApiUrl { get; set; }
        public string CustomerApiUrl { get; set; }
        public string EstateAgentApiUrl { get; set; }
        public string IdentityServerApiUrl { get; set; }
        public string ImageApiUrl { get; set; }
        public string JobApiUrl { get; set; }
        public string UserManagementApiUrl { get; set; }
        public string LoggingApiUrl { get; set; }
        public string NotificationApiUrl { get; set; }
        public string PaymentApiUrl { get; set; }
        public string PackagesAndPaymentsApi { get; set; }
        public string CMSApiUrl { get; set; }
        public string PurchaseApiUrl { get; set; }
        public string SupplierApiUrl { get; set; }
        public string TradesmanApiUrl { get; set; }
        public string CommunicationApiUrl { get; set; }
        public string VideoApiUrl { get; set; }
        public string AdminApiUrl { get; set; }
        public string ElmahApiUrl { get; set; }
        public string BaseUrl { get; set; }
    }
}
