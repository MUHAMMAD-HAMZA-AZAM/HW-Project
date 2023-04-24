using HW.Http;
using HW.UserViewModels;
using HW.Utility;
using HW.VideoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{

    public interface IVideoService
    {
        Task<Response> AddJobVideo(VideoVM videoVM, string UserId);
    }

    public class VideoService : IVideoService
    {
        private readonly IExceptionService Exc;
        private readonly IHttpClientService httpClient;
        private readonly ApiConfig apiConfig;

        public VideoService(IExceptionService _Exc, IHttpClientService _httpClient, ApiConfig _apiConfig)
        {
            Exc = _Exc;
            httpClient = _httpClient;
            apiConfig = _apiConfig;
        }

        public async Task<Response> AddJobVideo(VideoVM videoVM, string UserId)
        {
            Response response = new Response();
            try
            {
                JobQuotationVideo jobVideo = new JobQuotationVideo()
                {
                    FileName = videoVM.FilePath,
                    Video = videoVM.VideoContent,
                    JobQuotationId = videoVM.JobQuotationId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = UserId,
                    IsActive = videoVM.IsActive
                };

                response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{apiConfig.VideoApiUrl}{ApiRoutes.Video.AddJobVideo}", jobVideo));

                
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Could not upload the video";
            }

            return response;
        }
    }
}
