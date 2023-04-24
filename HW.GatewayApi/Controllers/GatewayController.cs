using HW.GatewayApi.Services;

namespace HW.GatewayApi.Controllers
{
    public class GatewayController : BaseController
    {
        public GatewayController(IUserManagementService userManagementService) : base(userManagementService) { }

        public string Start()
        {
            return "Gateway API is started.";
        }
    }
}