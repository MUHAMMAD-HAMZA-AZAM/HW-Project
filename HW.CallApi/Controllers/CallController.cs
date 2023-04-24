using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.CallApi.Services;
using HW.CallModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.CallApi.Controllers
{
    [Produces("application/json")]

    public class CallController : BaseController
    {
        private readonly ICallService callService;

        public CallController(ICallService callService)
        {
            this.callService = callService;
        }

        public string Start()
        {
            return "Call api has been started";
        }

        [HttpGet]
        public List<TradesmanCallLog> GetTradesmanCallLogs(long tradesmanId, long customerId = 0, bool todaysRecordOnly = false)
        {
            return callService.GetTradesmanCallLogs(tradesmanId, customerId, todaysRecordOnly).ToList();
        }


        [HttpGet]
        public List<TradesmanCallLog> GetCallLogByjobQuotationId(long tradesmanId, long jobQuotationId, long customerId = 0, bool todaysRecordOnly = false)
        {
            return callService.GetTradesmanCallLogs(tradesmanId, customerId, todaysRecordOnly).ToList();
        }

        [HttpGet]
        public List<TradesmanCallLog> GetJobQuotationCallLogs(long jobQuotationId)
        {
            return callService.GetJobQuotationCallLogs(jobQuotationId);
        }

        [HttpGet]
        public CallType GetCallTypeById(long callTypeId)
        {
            return callService.GetCallTypeById(callTypeId);
        }

        [HttpGet]
        //[Authorize(Roles = "Trademan")]
        public List<CallType> GetAllCallType()
        {
            return callService.GetAllCallType().ToList();
        }

        [HttpPost]
        public async Task<bool> DeleteCallLogs([FromBody] List<long> selectedCallLogIds)
        {
            return await callService.DeleteCallLogs(selectedCallLogIds);
        }

        [HttpGet]
        public IQueryable<TradesmanCallLog> GetCallLogByBidId(long tradesmanId, long bidId, long customerId)
        {
            return callService.GetCallLogByBidId(tradesmanId, bidId, customerId);
        }

        [HttpPost]
        public List<CallCount> GetTradesmanCallLogByJobQuotationId([FromBody] List<long> quotationIdList)
        {
            return callService.GetTradesmanCallLogByJobQuotationId(quotationIdList).ToList();
        }

        [HttpGet]
        public List<SupplierCallLog> GetSuppliersCallLog(long supplierId)
        {
            return callService.GetSuppliersCallLog(supplierId);
        }

        [HttpPost]
        public async Task<bool> DeleteSuppliersCallLogs([FromBody] List<long> selectedCallLogIds)
        {
            return await callService.DeleteSuppliersCallLogs(selectedCallLogIds);
        }

        [HttpGet]
        public List<SupplierCallLog> GetSuppliersCallLogs(long supplierId, long customerId, bool todaysRecordOnly)
        {
            return callService.GetSuppliersCallLogs(supplierId, customerId, todaysRecordOnly);
        }

        [HttpPost]
        public Response PostCallRequestLog([FromBody] TradesmanCallLog tradesmanCallLog)
        {
            return callService.PostCallRequestLog(tradesmanCallLog);
        }

        [HttpGet]
        public Response GetCallRequestLogs(long tradesmanId, long jobQuotationId)
        {
            return callService.GetCallRequestLogs(tradesmanId, jobQuotationId);
        }
    }
}
