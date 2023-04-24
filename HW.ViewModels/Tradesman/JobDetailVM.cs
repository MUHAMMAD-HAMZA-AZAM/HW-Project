using System;
using System.Collections.Generic;
using System.Text;

namespace HW.ViewModels.Tradesman
{
    public class JobDetailVM
    {
        public long JobDetailId { get; set; }
        public byte[] JobImage { get; set; }
        public string WorkTitle { get; set; }
        public string WorkAddress { get; set; }
        public string JobStartedDate { get; set; }
        public string CustomerName { get; set; }
        public string Rating { get; set; }
    }
}
