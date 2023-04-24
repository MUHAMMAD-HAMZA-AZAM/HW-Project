using HW.Events;
using HW.LoggingModels;
using HW.LoggingViewModels;
using HW.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionLogs = HW.LoggingModels.ExceptionLogs;

namespace HW.LoggingApi.Services
{
    public interface ILoggingService
    {
        void Log(LoggingEvent log);
        Task<bool> LogException(ExceptionVM exceptionVM);
    }

    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public LoggingService(ILogger<LoggingService> logger, IUnitOfWork unitOfWork, IExceptionService Exc)
        {
            _logger = logger;
            uow = unitOfWork;
            this.Exc = Exc;
        }

        public void Log(LoggingEvent log)
        {
            try
            {
                switch ((LogLevel)log.LogLevel)
                {
                    case LogLevel.Error:
                        _logger.LogError(log.EventID, null, log.Message);
                        break;
                    case LogLevel.Information:
                    default:
                        _logger.LogInformation(log.EventID, log.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task<bool> LogException(ExceptionVM exceptionVM)
        {
            try
            {
                ExceptionLogs exceptionLogs = new ExceptionLogs()
                {
                    CpuType = exceptionVM.CpuType,
                    ErrorMessage = exceptionVM.ErrorMessage,
                    ErrorType = exceptionVM.ErrorType,
                    MobleModel = exceptionVM.MobileModel,
                    OsVersion = exceptionVM.OsVersion,
                    CreatedOn = DateTime.UtcNow,
                    Application = (int)exceptionVM.Activity
                };

                Exc.AddErrorLog(new Exception(JsonConvert.SerializeObject(exceptionVM)));
                uow.Repository<ExceptionLogs>().Add(exceptionLogs);
                await uow.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }

        }
    }
}
