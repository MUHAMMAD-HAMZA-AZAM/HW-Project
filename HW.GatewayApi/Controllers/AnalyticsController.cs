using HW.AnalyticsModels;
using HW.GatewayApi.Services;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class AnalyticsController : BaseController
    {
        private readonly IAnalyticsService analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.analyticsService = analyticsService;
        }
        public string Start()
        {
            return "Gateway analytics API is started.";
        }

        [HttpPost]
        public async Task<Response> SaveAnalytics([FromBody] Analytics model)
        {
            if (DecodeTokenForUser() != null)
            {
               // model.CreatedBy = DecodeTokenForUser().Id;
                return await analyticsService.SaveAnalytics(model);
            }
            else
            {
                return await analyticsService.SaveAnalytics(model);
            }
           

        }

        [HttpPost]
        public async Task<List<AnalyticsVM>> GetUserAnalytics([FromBody] AnalyticsVM analyticsVM)
        {
            return await analyticsService.GetUserAnalytics(analyticsVM);
        }

    }
}
