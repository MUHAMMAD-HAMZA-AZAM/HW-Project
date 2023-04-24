using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.OrganizationApi.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Organization Api is Started.";
        }
    }
}