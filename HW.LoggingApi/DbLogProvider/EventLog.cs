using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.LoggingApi.DbLogProvider
{
    public class EventLog
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
