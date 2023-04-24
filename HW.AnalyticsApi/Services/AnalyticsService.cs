using HW.AnalyticsModels;
using HW.UserViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HW.AnalyticsApi.Services
{
    public interface IAnalyticsService
    {
        Task<Response> SaveAnalytics(Analytics analytics);

        List<AnalyticsVM> GetUserAnalytics(AnalyticsVM analyticsVM);
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;


        public AnalyticsService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public async Task<Response> SaveAnalytics(Analytics analytics)
        {
            Response response = new Response();
            try
            {
                Analytics Analyticsuser = uow.Repository<Analytics>().Get().Where(x => x.CreatedBy == analytics.CreatedBy).FirstOrDefault();
                if (Analyticsuser != null)
                {
                    Analyticsuser.Ip = analytics.Ip;
                    Analyticsuser.Device = analytics.Device;
                    Analyticsuser.Platform = analytics.Platform;
                    Analyticsuser.Os = analytics.Os;
                    Analyticsuser.OsVersion = analytics.OsVersion;
                    Analyticsuser.IpLocation = analytics.IpLocation;
                    Analyticsuser.ApplicationType = analytics.ApplicationType;
                    Analyticsuser.ApplicaitonVersion = analytics.ApplicaitonVersion;
                    Analyticsuser.CreatedBy = analytics.CreatedBy;
                    Analyticsuser.CreatedOn = analytics.CreatedOn;
                    Analyticsuser.MobileDevice = analytics.MobileDevice;
                    Analyticsuser.TabletDevice = analytics.TabletDevice;
                    Analyticsuser.DesktopDevice = analytics.DesktopDevice;
                    Analyticsuser.Browser = analytics.Browser;
                    Analyticsuser.BrowserVersion = analytics.BrowserVersion;
                    Analyticsuser.Country = analytics.Country;
                    Analyticsuser.CountryCapital = analytics.CountryCapital;
                    Analyticsuser.City = analytics.City;
                    Analyticsuser.District = analytics.District;
                    uow.Repository<Analytics>().Update(Analyticsuser);
                    await uow.SaveAsync();

                    response.Message = "Successfully Updated.";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    await uow.Repository<Analytics>().AddAsync(analytics);
                    await uow.SaveAsync();

                    response.Message = "Successfully Added.";
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;

        }


        public List<AnalyticsVM> GetUserAnalytics(AnalyticsVM analyticsVM)
        {
            List<AnalyticsVM> analytics = new List<AnalyticsVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@name",analyticsVM.Name),
                    new SqlParameter("@userTypeId",analyticsVM.userId),
                   new SqlParameter("@userRole",analyticsVM.userRole),
                   new SqlParameter("@FromDate",analyticsVM.FromDate),
                     new SqlParameter("@ToDate",analyticsVM.ToDate),
                   new SqlParameter("@pageNumber",analyticsVM.pageNumber),
                   new SqlParameter("@pageSize",analyticsVM.pageSize)

                };
                analytics = uow.ExecuteReaderSingleDS<AnalyticsVM>("Sp_GetUserAnalytics", sqlParameters).ToList();

                return analytics;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AnalyticsVM>();
            }

        }
    }
}
