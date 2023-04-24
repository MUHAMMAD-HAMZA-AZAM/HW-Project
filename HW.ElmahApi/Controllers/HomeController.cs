using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.ElmahApi.Controllers
{
    [Produces("application/json")]
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "Elmah Api is Started.";
        }
    }
}