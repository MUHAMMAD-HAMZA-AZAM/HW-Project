using HW.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HW.IdentityServer.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return Json("Identity Server Started");
        }
    }
}
