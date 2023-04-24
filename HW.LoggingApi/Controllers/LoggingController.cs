using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.Events;
using HW.LoggingApi.DbLogProvider;
using HW.LoggingApi.Services;
using HW.LoggingViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HW.LoggingApi.Controllers
{
    [Produces("application/json")]
    public class LoggingController : Controller
    {
        private readonly ILoggingService loggingService;

        public LoggingController(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        [HttpGet]
        public string Start()
        {
            return "Log service is started.";
        }

        [HttpPost]
        public void Log([FromBody]LoggingEvent model)
        {
            loggingService.Log(model);
        }

        [HttpPost]
        public async Task<bool> LogException([FromBody] ExceptionVM exceptionVM)
        {
            return await loggingService.LogException(exceptionVM);
        }
    }
}