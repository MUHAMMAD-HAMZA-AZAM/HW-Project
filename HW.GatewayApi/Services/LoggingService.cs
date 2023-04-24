using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.LoggingViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{
    public interface ILoggingService
    {
        Task<bool> LogException(ExceptionVM exceptionVM, UserRegisterVM userRegisterVM);
    }

    public class LoggingService : ILoggingService
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCredentials;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public LoggingService(IHttpClientService httpClientService, ClientCredentials clientCredentials, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClientService;
            this.clientCredentials = clientCredentials;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<bool> LogException(ExceptionVM exceptionVM, UserRegisterVM userRegisterVM)
        {
            try
            {
                if (userRegisterVM?.Role == "Customer")
                {
                    exceptionVM.Activity = TargetDatabase.Customer;
                }
                else if (userRegisterVM?.Role == "Tradesman")
                {
                    exceptionVM.Activity = TargetDatabase.Tradesman;
                }
                else if (userRegisterVM?.Role == "Supplier")
                {
                    exceptionVM.Activity = TargetDatabase.Supplier;
                }

                return JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.LoggingApiUrl}{ApiRoutes.Logging.LogException}", exceptionVM, "")
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
    }
}
