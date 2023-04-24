using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Events
{
    public class LoggingEvent
    {
        public int LogLevel { get; set; }
        public int EventID { get; set; }
        public string Message { get; set; }
    }
}
