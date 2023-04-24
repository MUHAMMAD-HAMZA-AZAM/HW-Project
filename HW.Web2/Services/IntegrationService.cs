using HW.Http;
using HW.PackagesAndPaymentsViewModels;
using HW.Utility;
using HW.Web2.Code;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HW.Web2.Services
{
    public interface IIntegrationService {
        Task<string> ProceedToJazzCash(JazzCashPaymentDetailVM objPayment, string token);
        Task<string> JazzCashCallBack(JazzCashPaymentDetailVM detailVM);
        Task<string> GetJazzCashMerchantDetails(string key);
    }
    public class IntegrationService :IIntegrationService
    {
        private readonly IHttpClientService _httpClient;
        //private readonly WebApiConfig _apiConfig;
        public IntegrationService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
            //_apiConfig = apiConfig;
        }
        public async Task<string> ProceedToJazzCash(JazzCashPaymentDetailVM objPayment, string token)
        {
            var gtwyUrl = "https://www.hoomwork.com/GatewayNet5/";
            return await _httpClient.PostAsync($"{gtwyUrl}{ApiRoutes.Payment.ProceedToJazzCash}", objPayment, token);
        }
        public async Task<string> JazzCashCallBack(JazzCashPaymentDetailVM detailVM)
        {
            var gtwyUrl = "http://172.16.1.33:15790/";
            return await _httpClient.PostAsync($"{gtwyUrl}{ApiRoutes.Payment.JazzCashCallBack}", detailVM);
        }
        public async Task<string> GetJazzCashMerchantDetails(string key)
        {
            var gtwyUrl = "https://www.hoomwork.com/GatewayNet5/";
            return await _httpClient.GetAsync($"{gtwyUrl}{ApiRoutes.Payment.GetJazzCashMerchantDetails}?key="+key, "");
        }
    }
}
