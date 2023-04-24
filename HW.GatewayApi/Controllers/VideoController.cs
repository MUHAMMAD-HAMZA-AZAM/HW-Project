using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Services;
using HW.UserViewModels;
using HW.Utility;
using HW.VideoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class VideoController : BaseController
    {

        private readonly IVideoService videoService;

        public VideoController(IVideoService _videoService, IUserManagementService userManagementService) : base(userManagementService)
        {
            videoService = _videoService;
        }

        [HttpPost]
        public async Task<Response> AddJobVideo([FromBody] VideoVM videoVM)
        {
            string userId = DecodeTokenForUser().Id;

            return await videoService.AddJobVideo(videoVM, userId);
        }
    }
}