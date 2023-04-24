using HW.AnalyticsApi.Services;
using HW.AnalyticsModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HW.AnalyticsApi.Controllers
{
    [Produces("application/json")]
    public class AnalyticsController : BaseController
    {
        private readonly IAnalyticsService analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        [HttpGet]
        public string Start()
        {
            return "Analytics service is started.";
        }

        [HttpPost]
        public async Task<Response> SaveAnalytics([FromBody] Analytics analytics)
        {
            return await analyticsService.SaveAnalytics(analytics);
        }

        [HttpPost]
        public List<AnalyticsVM> GetUserAnalytics([FromBody] AnalyticsVM analyticsVM)
        {
            return analyticsService.GetUserAnalytics(analyticsVM);
        }

    }
}
