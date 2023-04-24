using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.CallApi.Services;
using HW.CallModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.CallApi.Controllers
{

    public class HomeController : BaseController
    {
        public string Index()
        {
            return "Call Api is Started";
        }
    }
}
