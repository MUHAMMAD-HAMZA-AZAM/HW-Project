using System.Collections.Generic;
using HW.ElmahApi.Services;
using HW.LoggingViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HW.ElmahApi.Controllers
{
    [Produces("application/json")]
    public class ElmahController : BaseController
    {
        private readonly IElmahService _elmahService;
        public ElmahController(IElmahService elmahService)
        {
            this._elmahService = elmahService;
        }

        [HttpGet]
        public List<ElmahErrorsLogListVM> ElmahErrorlogList()
        {
            return _elmahService.ElmahErrorlogList();
        }

        [HttpGet]
        public ElmahErrorsLogListVM ElmahErrorDetailsById(string errorID)
        {
            return _elmahService.ElmahErrorDetailsById(errorID);
        }

        [HttpGet]
        public string GetClientIpAddress(string clientIPAddress)
        {
            return _elmahService.GetClientIpAddress(clientIPAddress);
        }

    }
}