using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.AudioApi.Services;
using HW.AudioModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using HW.Utility;

namespace HW.AudioApi.Controllers
{
    public class HomeController : BaseController
    {
        // GET api/values
        public string Index()
        {
            return "Audio Api is Starteed.";
        }
    }
}
