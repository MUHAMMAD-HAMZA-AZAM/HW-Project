using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.AdminServices;
using HW.LoggingViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.AdminControllers
{
   [Produces("application/json")]
    public class AdminElmahController : ControllerBase
    {
        private readonly IAdminElmahService  _elmahService;
        public AdminElmahController(IAdminElmahService elmahService)
        {
            this._elmahService = elmahService;
        }

        [HttpGet]
        public async  Task<List<ElmahErrorsLogListVM>> ElmahErrorlogList()
        {
            return await _elmahService.ElmahErrorlogList();
        }

        [HttpGet]
        public async Task<ElmahErrorsLogListVM> ElmahErrorDetailsById(string errorID)
        {
            return await _elmahService.ElmahErrorDetailsById(errorID);
        }
        [HttpGet]
        public async Task<string> GetClientIpAddress(string clientIPAddress)
        {
            return await _elmahService.GetClientIpAddress(clientIPAddress);
        }
    }
}