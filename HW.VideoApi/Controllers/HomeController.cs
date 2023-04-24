using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.VideoApi.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Video Api is Started.";
        }
    }
}