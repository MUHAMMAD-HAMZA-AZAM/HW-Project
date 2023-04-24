using HW.AnalyticsModels;
using HW.Http;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{
    public interface IAnalyticsService
    {
        Task<Response> SaveAnalytics(Analytics analytics);

        Task<List<AnalyticsVM>> GetUserAnalytics(AnalyticsVM analyticsVM);


    }
    public class AnalyticsService: IAnalyticsService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;
        public AnalyticsService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }
        public async Task<Response> SaveAnalytics(Analytics analytics)
        {
            Response response = new Response();
            try
            {
                var res = await httpClient.PostAsync($"{_apiConfig.AnalyticsApiUrl}{ApiRoutes.Analytics.SaveAnalytics}", analytics);
                Response resp = JsonConvert.DeserializeObject<Response>(res);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }


        public async Task<List<AnalyticsVM>> GetUserAnalytics(AnalyticsVM analyticsVM)
        {
            try
            {
                string response = await httpClient.PostAsync($"{_apiConfig.AnalyticsApiUrl}{ApiRoutes.Analytics.GetUserAnalytics}", analyticsVM);
                return JsonConvert.DeserializeObject<List<AnalyticsVM>>(response);
         
            }
            catch(Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AnalyticsVM>();
            }
        }
    }
}
