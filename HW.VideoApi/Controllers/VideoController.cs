using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HW.VideoModels;
using HW.VideoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using HW.Utility;

namespace HW.VideoApi.Controllers
{
    [Produces("application/json")]
    public class VideoController : Controller
    {
        // GET api/values
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpGet]
        public string Start()
        {
            return "Video service is started.";
        }

        [HttpGet]
        //[Authorize(Roles = "Trademan")]
        public List<JobQuotationVideo> GetAllVideo()
        {
            return videoService.GetAllVideo().ToList();
        }

        [HttpGet]
        public JobQuotationVideo GetByJobQuotationId(long jobQuotationId)
        {
            return videoService.GetByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        public SupplierAdVideos GetSupplierAdVideoByAdId(long supplierAdId)
        {
            return videoService.GetSupplierAdVideoByAdId(supplierAdId);
        }

        [HttpPost]
        public async Task AddVideo([FromBody]JobQuotationVideo jobQuotationVideo)
        {
            await videoService.AddVideo(jobQuotationVideo);
        }

        [HttpPost]
        public async Task<Response> AddJobVideo([FromBody]JobQuotationVideo jobVideo)
        {
            return await videoService.AddJobVideo(jobVideo);
        }

        [HttpGet]
        public async Task DeleteJobQuotationVideo(long jobQuotationId)
        {
            await videoService.DeleteJobQuotationVideo(jobQuotationId);
        }

        [HttpGet]
        public async Task<Response> UpdateJobVideoStatus(long jobQuotationId, bool isActive)
        {
            return await videoService.UpdateJobVideoStatus(jobQuotationId, isActive);
        }

        
        [HttpPost]
        public async Task DeleteAdVideo(long supplierAdsId)
        {
            await videoService.DeleteAdVideo(supplierAdsId);
        }

        [HttpPost]
        public void UpdateSuplierAdVideo([FromBody]SupplierAdVideos supplierAdVideos)
        {
            videoService.UpdateSuplierAdVideo(supplierAdVideos);
        }
        [HttpPost]
        public void SubmitAndUpdateAdVideo([FromBody]SupplierAdVideos supplierAdVideos)
        {
            videoService.SubmitAndUpdateAdVideo(supplierAdVideos);
        }
        public string GetSupplierAdVideoNameByAdId(long supplierAdId)
        {
            return videoService.GetSupplierAdVideoNameByAdId(supplierAdId);
        }

    }
}
