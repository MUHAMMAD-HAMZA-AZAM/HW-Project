using HW.Events;
using HW.LoggingApi.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.LoggingApi.Messaging.Consumers
{
    public class LogEventConsumer : IConsumer<LoggingEvent>
    {
        private readonly ILoggingService loggingService;

        public LogEventConsumer(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public Task Consume(ConsumeContext<LoggingEvent> context)
        {
            Task t = Task.Factory.StartNew(() => {
                loggingService.Log(context.Message);
            });
            t.Wait();
            return t;
        }
    }
}
