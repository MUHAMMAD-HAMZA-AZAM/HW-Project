using HW.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using ElmahCore;
using Microsoft.AspNetCore.Http;

namespace HW.Utility
{
    public interface IExceptionService
    {
       
        string AddErrorLog(Exception ex);

        string AddErrorLog(string ex);
    }

    public class ExceptionService : IExceptionService
    {
        private HttpContext _httpContext;
      
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientService _httpClient;

    public ExceptionService( IServiceProvider serviceProvider, IHttpClientService httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
            _httpContext = httpContextAccessor.HttpContext; 
        }

       

        public void AddLog()
        {

        }

        public string AddErrorLog(Exception ex)
        {
            try
            {
                _httpContext.RiseError(ex);
            }
            catch (Exception)
            {

            }
            return "Messages.Error";
        }

        public string AddErrorLog(string ex)
        {
            try
            {
                _httpContext.RiseError(new Exception(ex));
            }
            catch (Exception)
            {

            }
            return "Messages.Error";
        }
    }
}