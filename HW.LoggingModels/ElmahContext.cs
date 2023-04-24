using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.LoggingModels
{
    public partial class ElmahContext : DbContext
    {
        public ElmahContext()
        {

        }

        public ElmahContext(DbContextOptions<ElmahContext> options)
            : base(options)
        {

        }
      

    }
}
