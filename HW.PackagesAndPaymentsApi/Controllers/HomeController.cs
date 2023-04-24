using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HW.PackagesAndPaymentsApi.Controllers
{
    public class HomeController : BaseController
    {
        public string Index()
        {
            return "PackagesAndPaymentsApi start";
        }
    }
}