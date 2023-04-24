using System;
using System.Collections.Generic;
using System.Text;

namespace HW.LoggingViewModels
{
    public class ElmahVM
    {
        public string errorId { get; set; }
        public string application { get; set; }
        public string host { get; set; }
        public string type { get; set; }
        public string source { get; set; }
        public string user { get; set; }
        public string statusCode { get; set; }
        public string timeUTC { get; set; }
        public string sequence { get; set; }
        public string allXml { get; set; }
        public string message { get; set; }
    }
}
