﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.JobApi.Controllers
{
    public class HomeController : BaseController
    {
        public string Index()
        {
            return "Job Api is Started.";
        }
    }
}