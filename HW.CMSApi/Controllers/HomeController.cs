using Microsoft.AspNetCore.Mvc;

namespace HW.CMSApi
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "CMS Api is Started.";
        }
    }
}
