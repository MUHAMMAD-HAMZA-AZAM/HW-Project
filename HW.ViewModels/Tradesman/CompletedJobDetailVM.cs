using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ViewModels.Tradesman
{
    public class CompletedJobDetailVM
    {
        public long JobDetailId { get; set; }
        public string CustomerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime JobStartedDate { get; set; }
        public DateTime JobFinishDate { get; set; }
        public decimal LatiTude { get; set; }
        public decimal Lngitude { get; set; }
        public int Feedback { get; set; }
    }
}
