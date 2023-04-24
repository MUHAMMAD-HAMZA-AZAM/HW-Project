using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Job_ViewModels
{
   public class ReciveBidsList
    {
        public long JobQuotationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
