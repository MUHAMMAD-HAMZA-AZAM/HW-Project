using HW.Http;
using HW.LoggingViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminElmahService
    {
        Task<List<ElmahErrorsLogListVM>> ElmahErrorlogList();
        Task<ElmahErrorsLogListVM> ElmahErrorDetailsById(string errorID);
        Task<string> GetClientIpAddress(string clientIPAddress);
    }
    public class AdminElmahService : IAdminElmahService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public AdminElmahService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.Exc = Exc;
            this._apiConfig = apiConfig;
        }

        public async Task<ElmahErrorsLogListVM> ElmahErrorDetailsById(string errorID)
        {
            try
            {
                return JsonConvert.DeserializeObject<ElmahErrorsLogListVM>
               (await httpClient.GetAsync($"{_apiConfig.ElmahApiUrl}{ApiRoutes.Elmah.ElmahErrorDetailsById}?errorID={errorID}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return new ElmahErrorsLogListVM();
        }

        public async Task<List<ElmahErrorsLogListVM>> ElmahErrorlogList()
        {
            try
            {
                var elmahlogs = await httpClient.GetAsync($"{_apiConfig.ElmahApiUrl}{ApiRoutes.Elmah.ElmahErrorlogList}");
                return JsonConvert.DeserializeObject<List<ElmahErrorsLogListVM>>(elmahlogs);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ElmahErrorsLogListVM>();
            }
        }

        public async Task<string> GetClientIpAddress(string clientIPAddress)
        {
            try
            {
                return JsonConvert.DeserializeObject<string>
             (await httpClient.GetAsync($"{_apiConfig.ElmahApiUrl}{ApiRoutes.Elmah.GetClientIpAddress}?clientIPAddress={clientIPAddress}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "Some thing went Wrong !!";
            }
        }
    }
}
