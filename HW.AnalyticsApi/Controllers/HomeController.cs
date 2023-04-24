using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.AnalyticsApi.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Analytics api start successfully.";
        }
    }
}
