using HW.Job_ViewModels;
using HW.JobModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.UserManagmentModels;
using HW.GatewayApi.AuthO;
using HW.Utility;
using HW.UserViewModels;

namespace HW.GatewayApi.AdminService
{
    [Produces("application/json")]
    public class AdminJobController : ControllerBase
    {
        private readonly IAdminJobServices adminJobServices;

        public AdminJobController(IAdminJobServices adminJobServices)
        {
            this.adminJobServices = adminJobServices;
        }

        [HttpGet]

        public async Task<JobsVm> GetAllJobsCount()
        {
            return await adminJobServices.GetAllJobsCount();
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Admin })]
        public async Task<List<ActiveJobListVM>> GetActiveJobList(long pageNumber, long pageSize , string dataOrderBy , 
            string customerName , string jobId , string startDate , string endDate , string city , string location)
        {
            return await adminJobServices.GetActiveJobList(pageNumber, pageSize , dataOrderBy , customerName , jobId , startDate , endDate , city , location);
        }

        [HttpGet]
        public async Task<List<ActiveJobListVM>> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, string startDate, string endDate, string city,string status , string jobdetailid ,string fromDate , string toDate , string tradesmanName,
            string usertype , string location ,bool athorize,string cSJobStatusId,string townId,string town)
        {
            return await adminJobServices.GetPendingJobList(pageNumber, pageSize, dataOrderBy, customerName, jobId, startDate, endDate, city, status, jobdetailid, fromDate,toDate,tradesmanName, usertype, location , athorize, cSJobStatusId,townId, town);
        }
        [HttpGet]
        public async Task<List<DeletedJobListVM>> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy,
           string customerName, string jobId, string startDate, string endDate, string city, string status, string jobdetailid, string fromDate, string toDate, string tradesmanName,
           string usertype, string location, string cSJobStatusId,string town)
        {
            return await adminJobServices.GetDeletedJobList(pageNumber, pageSize, dataOrderBy, customerName, jobId, startDate, endDate, city, status, jobdetailid, fromDate, toDate, tradesmanName, usertype, location, cSJobStatusId, town);
        }
        [HttpGet]

        public async Task<List<InprogressJobList>> GetInprogressJobList(long pageNumber, long pageSize , string dataOrderBy , string customerName, string jobId, string jobDetailId , string startDate, string endDate, string city, string location)
        {
            return await adminJobServices.GetInprogressJobList(pageNumber, pageSize , dataOrderBy, customerName, jobId, jobDetailId, startDate, endDate, city, location);
        }

        [HttpGet]

        public async Task<JobQuotationDTO> GetJobDetailsList(long customerId)
        {
            return await adminJobServices.GetJobDetailsList(customerId);
        }

        [HttpGet]

        public async Task<List<ReciveBidsList>> GetReciveBids(long jobQoutationId)
        {
            return await adminJobServices.GetReciveBids(jobQoutationId);
        }

        [HttpGet]

        public async Task<ReciveBidDetails> ReciveBidDetails(long jobQoutationId)
        {
            return await adminJobServices.ReciveBidDetails(jobQoutationId);
        }

        [HttpGet]

        public async Task<List<Status>> JobStatusDropdown()
        {
            return await adminJobServices.JobStatusForDropdown();
        }
        [HttpGet]

        public async Task<List<Status>> GetCsJobStatusDropdown()
        {
            return await adminJobServices.GetCsJobStatusDropdown();
        }

        [HttpGet]

        public async Task<List<JobReportWithStatusCustomerVM>> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastActive, string location)
        {
            return await adminJobServices.GetJobsForReport(startDate, endDate, customer, status, city, lastActive, location);
        }

        [HttpGet]

        public async Task<List<JobQuotationDTO>> getPostedJobsWithDateLastDay(string startDate , string endDate)
        {
            return await adminJobServices.getPostedJobsWithDateLastDay(startDate , endDate);
        }

        [HttpGet]

        public async Task<List<BidsDTO>> GetActiveBidsLastDay(string startDate, string endDate)
        {
            return await adminJobServices.getActiveBidsLastDay(startDate, endDate);
        }

        [HttpGet]

        public async Task<List<JobQuotationDTO>> GetJobsForDynamicReport(string postedstartDate, string postedendDate , string endfromDate, string endtoDate ,string customer, string tradesman ,string status ,string city, bool lastActive , string location ,string jobId , string jobDetId , string userType)
        {
            return await adminJobServices.GetPostedJobsForDynamicReport(postedstartDate, postedendDate ,endfromDate ,endtoDate ,customer,tradesman,status,city,lastActive,location,jobId, jobDetId, userType);
        }

        [HttpGet]

        public async Task<List<BidsDTO>> GetBidsForDynamicReport(string postedstartDate, string postedendDate, string endfromDate, string endtoDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId)
        {
            return await adminJobServices.GetBisForDynamicReport(postedstartDate, postedendDate, endfromDate, endtoDate, customer, tradesman, status, city, lastActive, location, jobId, customerId);
        }

        [HttpGet]
        public async Task<List<JobQuotation>> GetPostedJobsAddressList()
        {
            return await adminJobServices.GetPostedJobsAddressList();
        }
        public async Task<List<JobAuthorizerVM>> JobAuthorizerList()
        {
            return await adminJobServices.JobAuthorizerList();
        }        
        public async Task<List<JobAuthorizerVM>> AdminJobAuthorizerList()
        {
            return await adminJobServices.AdminJobAuthorizerList();
        }

        public async Task<Response> DeleteJobWithJobQuotationId(long jobQuotationId, string actionPageName)
        {
           return await  adminJobServices.DeleteJobWithJobQuotationId(jobQuotationId, actionPageName);

        }
        public async Task<Response> ApproveJob(long jobQuotationId)
        {
            return await adminJobServices.ApproveJob(jobQuotationId);

        }
        [HttpPost]
        public async Task<Response> JobAuthorizer([FromBody] JobAuthorizerVM jobAuthorizerVM)
        {
            return await adminJobServices.JobAuthorizer(jobAuthorizerVM);
        }
        [HttpPost]
        public async Task<Response> UpdateJobBudget([FromBody] UpdateJobBudgetVM budgetVM)
        {
            return await adminJobServices.UpdateJobBudget(budgetVM);
        }        
        [HttpPost]
        public async Task<Response> UpdateJobExtraCharges([FromBody] UpdateJobBudgetVM updateJob)
        {
            return await adminJobServices.UpdateJobExtraCharges(updateJob);
        }
        [HttpPost]
        public async Task<Response> AssignJobToTradesman([FromBody] AssignJobVM assignJobVM)
        {
            return await adminJobServices.AssignJobToTradesman(assignJobVM);
        }
        [HttpGet]
        public async Task<Response> ChangeJobStatus(int jqId, long bidId)
        {
            return await adminJobServices.ChangeJobStatus(jqId, bidId);
        }
        [HttpGet]
        public async Task<Response> GetBidCountByJobId(long jobQuotationId,long tradesmanId=0 )
        {
            return await adminJobServices.GetBidCountByJobId(tradesmanId,jobQuotationId);
        }
        [HttpPost]
        public async Task<List<MyQuotationsVM>> SpGetJobsByCustomerId([FromBody]GetJobsParams getJobsParams)
        {
          return await adminJobServices.SpGetJobsByCustomerId(getJobsParams);
        }

        [HttpGet]
        public async Task<List<InprogressJobList>> GetUrgentJobsList()
        {
            return await adminJobServices.GetUrgentJobsList();
        }
        [HttpPost]
        public async Task<List<InprogressJobList>> GetJobsListByCategory([FromBody] InprogressJobList inprogressJobList)
        {
            return await adminJobServices.GetJobsListByCategory(inprogressJobList);
        }

        [HttpPost]
        public async Task<List<EsclateRequestVM>> EscalateIssuesRequestList([FromBody] EsclateRequestVM esclateRequestVM )
        {
            return await adminJobServices.EscalateIssuesRequestList(esclateRequestVM);
        }

        [HttpGet]
        public async Task<Response> AuthorizeEscalateIssueRequest(long escalateIssueId)
        {
            return await adminJobServices.AuthorizeEscalateIssueRequest(escalateIssueId);

        }
        [HttpPost]
        public async Task<List<EsclateRequestVM>> AuthorizeEscalateIssuesList([FromBody] EsclateRequestVM esclateRequestVM)
        {
            return await adminJobServices.AuthorizeEscalateIssuesList(esclateRequestVM);
        }

        [HttpGet]
        public async Task<List<EscalateOptionVM>> GetEscalateOptionsList()
        {
            return await adminJobServices.GetEscalateOptionsList();
        }

        [HttpPost]
        public async Task<Response> InsertAndUpdateEscalateOption( [FromBody] EscalateOptionVM escalateOption)
        {
            return await adminJobServices.InsertAndUpdateEscalateOption(escalateOption);
        }
    }
}
