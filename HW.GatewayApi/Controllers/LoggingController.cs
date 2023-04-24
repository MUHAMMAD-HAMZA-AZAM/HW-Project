using HW.GatewayApi.Services;
using HW.LoggingViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class LoggingController : BaseController
    {
        private readonly ILoggingService loggingService;

        public LoggingController(ILoggingService loggingService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.loggingService = loggingService;
        }

        [HttpPost]
        public async Task<bool> LogException([FromBody]ExceptionVM exceptionVM)
        {
            return await loggingService.LogException(exceptionVM, DecodeTokenForUser());
        }
    }
}