using HW.CallModels;
using HW.CommunicationViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HW.CallApi.Services
{
    public interface ICallService
    {
        IQueryable<TradesmanCallLog> GetTradesmanCallLogs(long tradesmanId, long customerId = 0, bool todaysRecordOnly = false);
        CallType GetCallTypeById(long callTypeId);
        IQueryable<CallType> GetAllCallType();
        Task<bool> DeleteCallLogs(List<long> selectedCallLogIds);
        List<TradesmanCallLog> GetJobQuotationCallLogs(long jobQuotationId);
        IQueryable<TradesmanCallLog> GetCallLogByBidId(long tradesmanId, long bidId, long customerId);
        IQueryable<CallCount> GetTradesmanCallLogByJobQuotationId(List<long> quotationIdList);
        List<SupplierCallLog> GetSuppliersCallLog(long supplierId);
        Task<bool> DeleteSuppliersCallLogs(List<long> selectedCallLogIds);
        List<SupplierCallLog> GetSuppliersCallLogs(long supplierId, long customerId, bool todaysRecordOnly);
        Response PostCallRequestLog(TradesmanCallLog tradesmanCallLog);
        Response GetCallRequestLogs(long tradesmanId, long jobQuotationId);
    }

    public class CallService : ICallService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public CallService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public IQueryable<CallType> GetAllCallType()
        {
            try
            {
                return uow.Repository<CallType>().GetAll();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return new List<CallType>().AsQueryable();
            }
        }

        public CallType GetCallTypeById(long callTypeId)
        {
            try
            {
                return uow.Repository<CallType>().Get(c => c.CallTypeId == callTypeId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return new CallType();
            }
        }

        public IQueryable<TradesmanCallLog> GetTradesmanCallLogs(long tradesmanId, long customerId = 0, bool todaysRecordOnly = false)
        {
            try
            {
                IQueryable<TradesmanCallLog> callLogs = uow.Repository<TradesmanCallLog>().GetAll().Where(x => x.TradesmanId == tradesmanId);
                if (customerId > 0) callLogs = callLogs.Where(x => x.CustomerId == customerId);
                if (todaysRecordOnly) callLogs = callLogs.Where(x => x.CreatedOn.Date == DateTime.Now.Date);
                callLogs = callLogs.OrderByDescending(x => x.CreatedOn);
                return callLogs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<TradesmanCallLog>().AsQueryable();
            }
        }

        public IQueryable<TradesmanCallLog> GetCallLogByBidId(long tradesmanId, long bidId, long customerId)
        {
            try
            {
                return uow.Repository<TradesmanCallLog>().GetAll().Where(x => x.TradesmanId == tradesmanId && x.CustomerId == customerId && x.JobQuotationId == bidId && x.CreatedOn.Date == DateTime.Now.Date).OrderByDescending(x => x.CreatedOn);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<TradesmanCallLog>().AsQueryable();
            }
        }

        public List<TradesmanCallLog> GetJobQuotationCallLogs(long jobQuotationId)
        {
            try
            {
                return uow.Repository<TradesmanCallLog>().GetAll().Where(x => x.JobQuotationId == jobQuotationId).ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return new List<TradesmanCallLog>();
            }
        }

        public async Task<bool> DeleteCallLogs(List<long> selectedCallLogIds)
        {
            try
            {
                if (selectedCallLogIds.Count == 0)
                {
                    return false;
                }
                else
                {
                    IQueryable<TradesmanCallLog> query = uow.Repository<TradesmanCallLog>().Get(x => selectedCallLogIds.Contains(x.TradesmanCallLogId));
                    await uow.Repository<TradesmanCallLog>().DeleteAllAsync(query);
                    await uow.SaveAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public IQueryable<CallCount> GetTradesmanCallLogByJobQuotationId(List<long> quotationIdList)
        {
            try
            {
                IQueryable<IGrouping<long?, TradesmanCallLog>> callGroups = uow.Repository<TradesmanCallLog>().GetAll().GroupBy(x => x.JobQuotationId);
                var test = callGroups.Where(callGroup => quotationIdList.Contains(callGroup.Key.Value)).Select(bidGroup => new CallCount { Calls = bidGroup.Count(), JobQuotationId = bidGroup.Key.Value });
                return test;
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return new List<CallCount>().AsQueryable();
            }
        }

        public List<SupplierCallLog> GetSuppliersCallLog(long supplierId)
        {
            try
            {
                return uow.Repository<SupplierCallLog>().GetAll().Where(s => s.SupplierId == supplierId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierCallLog>();

            }
        }

        public async Task<bool> DeleteSuppliersCallLogs(List<long> selectedCallLogIds)
        {
            try
            {
                if (selectedCallLogIds.Count == 0)
                {
                    return false;
                }
                else
                {
                    var repository = uow.Repository<SupplierCallLog>();
                    await repository.DeleteAllAsync(repository.Get(x => selectedCallLogIds.Contains(x.SupplierCallLogId)));
                    await uow.SaveAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public List<SupplierCallLog> GetSuppliersCallLogs(long supplierId, long customerId, bool todaysRecordOnly)
        {
            try
            {
                IQueryable<SupplierCallLog> callLogs = uow.Repository<SupplierCallLog>().GetAll().Where(x => x.SupplierId == supplierId);
                if (customerId > 0) callLogs = callLogs.Where(x => x.CustomerId == customerId);
                if (todaysRecordOnly) callLogs = callLogs.Where(x => x.CreatedOn.Date == DateTime.Now.Date);
                callLogs = callLogs.OrderByDescending(x => x.CreatedOn);
                return callLogs.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);

                return new List<SupplierCallLog>();
            }
        }

        public Response PostCallRequestLog(TradesmanCallLog tradesmanCallLog)
        {
            Response response;

            try
            {
                uow.Repository<TradesmanCallLog>().Add(tradesmanCallLog);
                uow.Save();

                response = new Response()
                {
                    Message = "Data saved successfully",
                    ResultData = null,
                    Status = ResponseStatus.OK
                };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }
            return response;
        }

        public Response GetCallRequestLogs(long tradesmanId, long jobQuotationId)
        {
            Response response;

            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@TradesmanId", tradesmanId),
                    new SqlParameter("@JobQuotationId",jobQuotationId),
                    new SqlParameter("@CreatedOn",DateTime.Now)
                };

                int callCount = uow.ExecuteReaderSingleDS<CallRequestVM>("GetCallRequestLogs", sqlParameters).Select(x => x.CallRequest).SingleOrDefault();

                response = new Response()
                {
                    Message = "Successfull executed",
                    Status = ResponseStatus.OK,
                    ResultData = callCount
                };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }
    }
}


