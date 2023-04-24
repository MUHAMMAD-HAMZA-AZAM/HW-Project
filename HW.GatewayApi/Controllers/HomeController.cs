using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Gateway API is started.";
        }
    }
}