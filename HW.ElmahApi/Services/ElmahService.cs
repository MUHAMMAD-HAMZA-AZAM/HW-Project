using HW.ElmahModels;
using HW.Http;
using HW.LoggingViewModels;
using HW.Utility;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW.ElmahApi.Services
{
    public interface IElmahService
    {
        List<ElmahErrorsLogListVM> ElmahErrorlogList();
        ElmahErrorsLogListVM ElmahErrorDetailsById(string errorID);

        string GetClientIpAddress(string clientIPAddress);
    }


    public class ElmahService : IElmahService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        private readonly IHttpClientService httpClient;
        public ElmahService(IHttpClientService httpClient, IUnitOfWork uow, IExceptionService Exc)
        {
            this.httpClient = httpClient;
            this.uow = uow;
            this.Exc = Exc;
        }

        public List<ElmahErrorsLogListVM> ElmahErrorlogList()
        {
            List<ElmahErrorsLogListVM> elmaherrorsList = new List<ElmahErrorsLogListVM>();
            try
            {
                SqlParameter[] sqlParameters =
               {
                   
               };

                elmaherrorsList = uow.ExecuteReaderSingleDS<ElmahErrorsLogListVM>("ELMAH_GetLogErrors", sqlParameters);
                return elmaherrorsList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ElmahErrorsLogListVM>();
            }
            
        }

        public ElmahErrorsLogListVM ElmahErrorDetailsById(string errorID)
        {
            ElmahErrorsLogListVM elmahErrorsLogListVM;
            try
            {
                SqlParameter[] sqlParameters =
              {
                    new SqlParameter("@ErrorId",errorID),
            };

                elmahErrorsLogListVM = uow.ExecuteReaderSingleDS<ElmahErrorsLogListVM>("ELMAH_GetErrorDetailsById", sqlParameters).FirstOrDefault();
               
                return elmahErrorsLogListVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ElmahErrorsLogListVM();
            }
        }


        public string GetClientIpAddress(string clientIPAddress)
        {
            try
            {
                IPAddress[] adresses = Dns.GetHostAddresses(Dns.GetHostName());
                clientIPAddress = adresses[2].ToString();
              
        
                return clientIPAddress;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return "";
        }

        protected T FromXml<T>(String xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        // String passed is not XML, simply return defaultXmlClass
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return returnedXmlClass;
        }
    }
}
