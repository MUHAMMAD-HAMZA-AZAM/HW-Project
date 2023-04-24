using HW.Job_ViewModels;
using HW.JobApi.Services;
using HW.JobModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using System;
using HW.IdentityViewModels;

namespace HW.JobApi.Controllers
{
    [Produces("application/json")]
    public class JobController : BaseController
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpGet]
        public string Start()
        {
            return "Job service is started";
        }

        [HttpGet]
        public async Task<List<Bids>> GetAllJobs()
        {
            List<Bids> bids = new List<Bids>();
            return bids;
        }

        [HttpGet]
        public JobDetail GetJobDetailsById(long jobDetailId)
        {
            return  jobService.GetJobDetailsById(jobDetailId);
        }

        [HttpGet]
        public JobAddress GetJobAddress(long jobQuotationId)
        {
            return jobService.GetJobAddress(jobQuotationId);
        }
        [HttpGet]
        public async Task<JobQuotation> GetJobQuotationById(long id)
        {
            return await jobService.GetJobQuotationById(id);
        }

        [HttpGet]
        public async Task<Bids> GetBidById(long id)
        {
            return await jobService.GetBidById(id);
        }

        [HttpGet]
        public Bids GetBidJobQuotaionId(long jobQuotationId, long tradesmanId)
        {
            return jobService.GetBidJobQuotaionId(jobQuotationId, tradesmanId);
        }

        public JobQuotation GetByCustomerId(long customerId)
        {
            return jobService.GetByCustomerId(customerId);
        }

        [HttpGet]
        public List<Bids> GetAllBids()
        {
            return jobService.GetAllBids().ToList();
        }       
        [HttpGet]
        public List<Bids> GetBidListByCustomerId(long customerId , int statusId)
        {
            return jobService.GetBidListByCustomerId(customerId , statusId);
        }

        public IQueryable<Bids> GetActiveBids([FromQuery]long tradesmanId, int bidsStatusId)
        {
            return jobService.GetActiveBids(tradesmanId, bidsStatusId);
        }

        [HttpPost]
        public async Task<List<JobQuotation>> GetJobQuotationsByIds([FromBody]List<long> jobQuotationIds)
        {
            return await jobService.GetJobQuotationsByIds(jobQuotationIds);
        }

        public async Task<List<JobDetail>> GetJobsDetail([FromQuery]long tradesmanId, int jobStatusId)
        {
            return await jobService.GetJobsDetail(tradesmanId, jobStatusId);
        }

        public List<JobQuotation> GetJobQuotationsBySkillId([FromBody]SkillproductIdVM skillproductIdVM)
        {
            return jobService.GetJobQuotationsBySkillId(skillproductIdVM.skillIds, skillproductIdVM.cityId, skillproductIdVM.pageNumber);
        }

        public async Task<JobDetail> GetCompletedJobById([FromQuery]long jobDetailid, long tradesmanId)
        {
            return await jobService.GetAJobById(jobDetailid, tradesmanId);
        }
        public async Task<JobDetail> GetAJobByJobDeatilId([FromQuery]long jobDetailid, long tradesmanId)
        {
            return await jobService.GetAJobByJobDeatilId(jobDetailid, tradesmanId);
        }

        [HttpGet]
        public TradesmanFeedback GetFeedBack(long jobDetailid, long tradesmanId)
        {
            return jobService.GetFeedBack(jobDetailid, tradesmanId);
        }

        public List<TradesmanFeedback> GetTradesmanFeedBack(long tradesmanId)
        {
            return jobService.GetTradesmanFeedBack(tradesmanId);
        }

        public Response SubmitBid([FromBody]Bids bids)
        {
            return jobService.SubmitBids(bids);
        }

        [HttpPost]
        public IQueryable<Bids> GetBidCounts([FromBody]List<long> jobQuotationIds)
        {
            return jobService.GetBidCounts(jobQuotationIds);
        }
        [HttpGet]
        public long GetBidCountsOnJob(long jobQuotationId)
        {
            return jobService.GetBidCountsOnJob(jobQuotationId);
        }

        public IQueryable<Bids> GetJobQuotationBidsByJobQuotatationIds([FromBody]List<long> jobQuotationIds)
        {
            return jobService.GetJobQuotationBidsByJobQuotatationIds(jobQuotationIds);
        }

        public void DeleteJobDetailsByJobQuotationId(long jobQuotationId)
        {
            jobService.DeleteJobDetailsByJobQuotationId(jobQuotationId);
        }

        public Response UpdateJobCost(long jobDetailId, decimal jobCost)
        {
            return jobService.UpdateJobCost(jobDetailId, jobCost);
        }

        public Response UpdateJobQuotationStatus(long jobQuotationId, int statusId)
        {
            return jobService.UpdateJobQuotationStatus(jobQuotationId, statusId);
        }

        [HttpPost]
        public Response AddEscalateIssue([FromBody] Dispute dispute)
        {
            return jobService.AddEscalateIssue(dispute);
        }

        [HttpGet]
        public List<JobDetail> GetCompletedJobDetailsByCustomerAndStatusIds(long customerId, long statusId)
        {
            return jobService.GetCompletedJobDetailsByCustomerAndStatusIds(customerId, statusId);
        }

        [HttpGet]
        public List<Dispute> GetDisputeRecord(long customerid)
        {

            return jobService.GetDisputeRecord(customerid);
        }

        [HttpPost]
        public List<DisputeStatus> GetDisputeStatusByStatusIds([FromBody]List<long> statusIds)
            {
            return jobService.GetDisputeStatusByStatusIds(statusIds);
        }

        public Response updateStatuse(long disputeId, int disputeStatusId)
        {
            return jobService.updateStatuse(disputeId, disputeStatusId);
        }

        [HttpGet]
        public List<JobQuotation> GetPostedJobsByCustomerId(long customerId)
        {
            return jobService.GetPostedJobsByCustomerId(customerId);
        }

        [HttpGet]
        public Task<JobQuotation> GetJobQuotationByJobQuotationId(long jobQuotationId)
        {
            return jobService.GetJobQuotationByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        public JobAddress GetJobAddressByJobQuotationId(long JobQuotationId)
        {
            return jobService.GetJobAddressByJobQuotationId(JobQuotationId);
        }

        [HttpPost]
        public List<JobAddress> GetJobAddressByJobQuotationIds([FromBody]List<long> JobQuotationIds)
        {
            return jobService.GetJobAddressByJobQuotationIds(JobQuotationIds);
        }

        [HttpGet]
        public List<FavoriteTradesman> GetFavoriteTradesmenByJobQuotationId(long jobQuotationId)
        {
            return jobService.GetFavoriteTradesmenByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        public List<JobDetail> GetJobStatusByCustomerId(long customerId, int statusId)
        {
            return jobService.GetJobStatusByCustomerId(customerId, statusId);
        }

        [HttpPost]
        public List<TradesmanFeedback> GetTradesmanFeedbackByCustomerIds([FromBody]List<long> customerIds)
        {
            return jobService.GetTradesmanFeedbackByCustomerIds(customerIds);
        }

        [HttpPost]
        public Response PostTradesmanFeedback([FromBody]TradesmanFeedback tradesmanFeedback)
        {
            return jobService.PostTradesmanFeedback(tradesmanFeedback);
        }

        [HttpGet]
        public List<Bids> GetQuotationBids(long quotationId)
        {
            return jobService.GetQuotationBids(quotationId);
        }

        [HttpPost]
        public IQueryable<JobDetail> GetJobsByTradesmanIds([FromBody]List<long> tradesmanIds)
        {
            return jobService.GetJobsByTradesmanIds(tradesmanIds);
        }
        [HttpGet]
        public async Task<bool> UpdateSelectedBid(long BidId, bool IsSelected)
        {
            return await jobService.UpdateSelectedBid(BidId, IsSelected);
        }

        [HttpGet]
        public async Task<Response> UpdateBidStatus(long BidId, int statusId)
        {
            return await jobService.UpdateBidStatus(BidId, statusId);
        }


        [HttpGet]
        public bool UpdateJobDetailStatus(long jobDetailId, int statusId)
        {
            return jobService.UpdateJobDetailStatus(jobDetailId, statusId);
        }

        [HttpGet]
        public async Task<Response> AddJobDetails(long bidId,int paymentMethod)
        {
            return await jobService.AddJobDetails(bidId, paymentMethod);
        }

        [HttpGet]
        public JobDetail GetJobDetailsByJobQuotationId(long jobQuotationId)
        {
            return jobService.GetJobDetailsByJobQuotationId(jobQuotationId);
        }
        [HttpGet]
        public async Task<Bids> GetBidByJQID(long id)
        {
            return await jobService.GetBidByJQID(id);
        }

        [HttpGet]
        public List<SupplierFeedback> GetSupplierFeedbackBySupplierId(long supplierId)
        {
            return jobService.GetSupplierFeedbackBySupplierId(supplierId);
        }

        [HttpPost]
        public Response SetSupplierRating([FromBody]SupplierFeedback supplierFeedback)
        {
            return jobService.SetSupplierRating(supplierFeedback);
        }


        [HttpGet]
        public List<long> GetJobDetails([FromQuery]long customer)
        {
            return jobService.GetJobDetails(customer);
        }

        [HttpGet]
        public List<JobDetail> GetAlljobDetails(long customerId, long statusId)
        {
            return jobService.GetAlljobDetails(customerId, statusId);
        }

        [HttpPost]
        public Task<Response> UpdateJobQuotation([FromBody]JobQuotation jobQuotation)
        {
            return jobService.UpdateJobQuotation(jobQuotation);
        }

        [HttpGet]
        public async Task<Response> DeleteJobQuotation(long jobQuotationId)
        {
            return await jobService.DeleteJobQuotation(jobQuotationId);
        }

        [HttpGet]
        public async Task<Response> DeleteJobAddressByJobQuotationId(long jobQuotationId)
        {
            return await jobService.DeleteJobAddressByJobQuotationId(jobQuotationId);
        }

        [HttpPost]
        public long JobQuotation([FromBody] JobQuotation jobQuotationVM)
        {
            return jobService.JobQuotations(jobQuotationVM);
        }
        [HttpPost]
        public Response SaveJobAddress([FromBody] JobAddress jobQuotationVM)
        {
            return jobService.SaveJobAddress(jobQuotationVM);
        }
        [HttpPost]
        public bool MarkAsFinishedJob([FromBody] JobDetail jobDetail)
        {
            return jobService.MarkAsFinishedJob(jobDetail);
        }

        [HttpPost]
        public void AddJobQuotationFavoriteTradesmen([FromBody] List<FavoriteTradesman> favoriteTradesman)
        {
            jobService.AddJobQuotationFavoriteTradesmen(favoriteTradesman);
        }

        public void DeleteEscalateIssue(long disputeId)
        {
            jobService.DeleteEscalateIssue(disputeId);
        }

        [HttpGet]
        public long GetQuotationIdByBidId(long bidId)
        {
            return jobService.GetQuotationIdByBidId(bidId);
        }

        [HttpGet]
        public List<long> GetBidCountByJobQuotationId(long jobQuotationId)
        {
            return jobService.GetBidCountByJobQuotationId(jobQuotationId);
        }
        [HttpGet]
        public bool CheckFeedBackStatus(long jobDetailId)
        {
            return jobService.CheckFeedBackStatus(jobDetailId);
        }
        [HttpGet]
        public List<JobQuotation> GetPostedJobsAddressList()
        {
            return jobService.GetPostedJobsAddressList();
        }
        [HttpGet]
        public List<JobTown> GetTownsList()
        {
            return jobService.GetTownsList();
        }
        [HttpPost]
        public Response PostJobContactInfo([FromBody] PersonalDetailsVM personalDetailsVM)
        {
            return jobService.PostJobContactInfo(personalDetailsVM);
        }
        [HttpPost]
        public Response submitIssue([FromBody] EsclateRequest esclateRequest)
        {
            return jobService.submitIssue(esclateRequest);
        }
        [HttpGet]
        public Response getEscalateIssueByJQID(long jobQuotationId, int userRole, int status)
        {
            return jobService.getEscalateIssueByJQID(jobQuotationId, userRole, status);
        }
        [HttpPost]
        public Response JobAuthorizer([FromBody] JobAuthorizerVM jobAuthorizerVM)
        {
            return jobService.JobAuthorizer(jobAuthorizerVM);
        }        
        [HttpPost]
        public Response ChangeJobStatus([FromBody] int jqId)
        {
            return jobService.ChangeJobStatus(jqId);
        }
        [HttpGet]
        public List<JobAuthorizerVM> JobAuthorizerList()
        {
            return jobService.JobAuthorizerList();
        }        
        [HttpGet]
        public List<JobAuthorizerVM> AdminJobAuthorizerList()
        {
            return jobService.AdminJobAuthorizerList();
        }
        [HttpGet]
        public List<InprogressJobList> GetUrgentJobsList()
        {
            return jobService.GetUrgentJobsList();
        }        
        [HttpPost]
        public List<InprogressJobList> GetJobsListByCategory([FromBody] InprogressJobList inprogressJobList)
        {
            return jobService.GetJobsListByCategory(inprogressJobList);
        }
        [HttpGet]
        public List<EsclateOption> getEscalateOptions(int userRole)
        {
            return jobService.getEscalateOptions(userRole);
        }

        [HttpPost]
        public List<EsclateRequestVM> EscalateIssuesRequestList( [FromBody] EsclateRequestVM esclateRequestVM)
        {
            return jobService.EscalateIssuesRequestList(esclateRequestVM);
        }

        [HttpGet]
        public Response AuthorizeEscalateIssueRequest(long escalateIssueId)
        {
            return jobService.AuthorizeEscalateIssueRequest( escalateIssueId);
        }

        [HttpPost]
        public List<EsclateRequestVM> AuthorizeEscalateIssuesList([FromBody] EsclateRequestVM esclateRequestVM)
        {
            return jobService.AuthorizeEscalateIssuesList(esclateRequestVM);
        }        
        [HttpPost]
        public Task<Response> UpdateBidByStatusId([FromBody] BidDetailsVM bidDetailsVM)
        {
            return jobService.UpdateBidByStatusId(bidDetailsVM);
        }       
        [HttpPost]
        public Response GetAcceptedBidsList([FromBody] BidDetailsVM bidDetailsVM)
        {
            return jobService.GetAcceptedBidsList(bidDetailsVM);
        }    
        [HttpPost]
        public Response GetInprogressJobsMobile([FromBody] InProgressJobsVM inProgressJobsVM)
        {
            return jobService.GetInprogressJobsMobile(inProgressJobsVM);
        }        
        [HttpPost]
        public Response StartOrFinishJob([FromBody] BidDetailsVM bidDetailsVM)
        {
            return jobService.StartOrFinishJob(bidDetailsVM);
        }        
        [HttpPost]
        public Response UpdateJobAdditionalCharges([FromBody] BidDetailsVM bidDetailsVM)
        {
            return jobService.UpdateJobAdditionalCharges(bidDetailsVM);
        }

        #region Store Procedures

        [HttpGet]
        public long GetJobDetailIdByQuotationId(long quotationId)
        {
            return jobService.GetJobDetailIdByQuotationId(quotationId);
        }
        [HttpGet]
        public JobQuotation JobDetailsByJQID(long jobQuotationId)
        {
            return jobService.JobDetailsByJQID(jobQuotationId);
        }

        [HttpGet]
        public List<JobLeadsVM> GetJobQuotationsBySkill(long tradesmanId, int pageNumber, int pageSize)
        {
            return jobService.GetJobQuotationsBySkill(tradesmanId, pageNumber, pageSize);
        }

        [HttpGet]
        public List<JobLeadsWebVM> GetJobQuotationsWebBySkill(long tradesmanId, int pageNumber, int pageSize)
        {
            return jobService.GetJobQuotationsWebBySkill(tradesmanId, pageNumber, pageSize);
        }

       
        [HttpGet]
        public List<BidVM> GetActiveBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            return jobService.GetActiveBidsDetails(pageNumber, pageSize, tradesmanId, bidsStatusId);
        }
       



        [HttpGet]
        public List<BidVM> GetDeclinedBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            return jobService.GetDeclinedBidsDetails(pageNumber, pageSize, tradesmanId, bidsStatusId);
        }
        [HttpGet]
        public List<BidWebVM> GetActiveBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            return jobService.GetActiveBidsDetailsWeb(pageNumber, pageSize, tradesmanId, bidsStatusId);
        }
        public List<BidWebVM> GetDeclinedBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            return jobService.GetDeclinedBidsDetailsWeb(pageNumber, pageSize, tradesmanId, bidsStatusId);
        }

        [HttpGet]
        public List<MyQuotationsVM> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, long customerId, int statusId,bool bidStatus)
        {
            return jobService.SpGetPostedJobsByCustomerId(pageNumber, pageSize, customerId, statusId, bidStatus);
        }
        [HttpGet]
        public List<MyQuotationsVM> GetPostedJobs(int pageNumber, int pageSize, long customerId)
        {
            return jobService.GetPostedJobs(pageNumber, pageSize, customerId);
        }

        [HttpGet]
        public List<InprogressVM> GetInprogressJob(long customerId, int statusId, int pageNumber, int pageSize)
        {
            return jobService.GetInprogressJob(customerId, statusId, pageNumber, pageSize);
        }

        [HttpGet]
        public Response GetJobDetail(int pageNumber, int pageSize, long tradesmanId, int jobStatusId)
        {
            return jobService.GetJobDetail(pageNumber, pageSize, tradesmanId, jobStatusId);
        }
        [HttpGet]
        public Response GetJobDetailWeb(int pageNumber, int pageSize, long tradesmanId, int jobStatusId)
        {
            return jobService.GetJobDetailWeb(pageNumber, pageSize, tradesmanId, jobStatusId);
        }

        [HttpGet]
        public List<FinishedJobVM> GetFinishedJob(long customerId, int statusId, int pageNumber, int pageSize)
        {
            return jobService.GetFinishedJob(customerId, statusId, pageNumber, pageSize);
        }

        [HttpGet]
        public Response GetTradesmanByBidId(long bidId)
        {
            return jobService.GetTradesmanByBidId(bidId);
        }

        [HttpGet]
        public Response Sp_GetTradesmanFirebaseClientId(long jobQuotationId)
        {
            return jobService.Sp_GetTradesmanFirebaseClientId(jobQuotationId);
        }

        [HttpGet]
        public List<WebLiveLeadsVM> WebLiveLeads(long jobQuotationId)
        {
            return jobService.WebLiveLeads(jobQuotationId);
        }

        [HttpGet]
        public List<WebLiveLeadsVM> WebLiveLeadsPanel(long TradesmanId,int statusId ,int pageNumber, int pageSize)
        {
            return jobService.WebLiveLeadsPanel(TradesmanId,statusId, pageNumber, pageSize);
        }
        [HttpGet]
        public List<IdValueVM> WebLiveLeadsLatLong()
        {
            return jobService.WebLiveLeadsLatLong();
        }
       public List<GetJobsCountVM> GetJobsCount(long tradesmanId)
        {
            return jobService.GetJobsCount(tradesmanId);
        }
        #endregion

        #region Admin

       [HttpGet]
        public JobsVm GetAllJobsCount()
        {
            return jobService.GetAllJobsCount();
        }

        [HttpGet]
        public List<ActiveJobListVM> GetActiveJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city, string location)
        {
            return jobService.GetActiveJobList(pageNumber, pageSize, dataOrderBy, customerName, jobId, startDate, endDate, city, location);
        }

        [HttpPost]
        public List<MyQuotationsVM> SpGetJobsByCustomerId([FromBody]GetJobsParams getJobsParams)
        {
          return jobService.SpGetJobsByCustomerId(getJobsParams);
        }
        [HttpGet]
        public List<ActiveJobListVM> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city,string status ,string jobdetailid, DateTime? fromDate, DateTime? toDate, string tradesmanName, string usertype, string location ,bool athorize,string cSJobStatusId,string townId,string town)
        {
            return jobService.GetPendingJobList(pageNumber, pageSize, dataOrderBy, customerName, jobId, startDate, endDate, city, status, jobdetailid,fromDate,toDate 
                ,tradesmanName , usertype, location , athorize,cSJobStatusId,townId,town);
        }
        [HttpGet]
        public List<DeletedJobListVM> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy,
           string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city, string status, string jobdetailid, DateTime? fromDate, DateTime? toDate, string tradesmanName, string usertype, string location,  string cSJobStatusId, string town)
        {
            return jobService.GetDeletedJobList(pageNumber, pageSize, dataOrderBy, customerName, jobId, startDate, endDate, city, status, jobdetailid, fromDate, toDate
                , tradesmanName, usertype, location,  cSJobStatusId,  town);
        }
        [HttpGet]
        public List<InprogressJobList> GetInprogressJobList(long pageNumber, long pageSize , string dataOrderBy, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location)
        {
            return jobService.GetInprogressJobList(pageNumber, pageSize , dataOrderBy, customerName, jobId, jobDetailId, startDate, endDate, city, location);
        }
        [HttpGet]
        public JobQuotationDTO GetJobDetailsList(long customerId)
        {
            return jobService.GetJobDetailsList(customerId);
        }

        [HttpGet]
        public List<CompletedJobListVm> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location, DateTime? fromDate, DateTime? toDate, string dataOrderBy = "")
        {
            return jobService.CompletedJobListAdmin(pageNumber, pageSize, customerName, jobId, jobDetailId, startDate, endDate, city, location, fromDate, toDate, dataOrderBy);
        }

        [HttpGet]
        public List<ReciveBidsList> GetReciveBids(long jobQoutationId)
        {
            return jobService.GetReciveBids(jobQoutationId);
        }
        [HttpGet]
        public ReciveBidDetails GetReciveBidDetails(long jobQoutationId)
        {
            return jobService.GetReciveBidDetails(jobQoutationId);
        }
        [HttpGet]
        public List<Status> JobStatusForDropDown()
        {
            return  jobService.JobStatusForDropdown();
        }        
        [HttpGet]
        public List<Status> GetCsJobStatusDropdown()
        {
            return  jobService.GetCsJobStatusDropdown();
        }
        [HttpGet]
        public List<JobReportWithStatusCustomerVM> GetJobsForReport(System.DateTime?  startDate, System.DateTime?  endDate, string customers, string status , string city , bool lastactive , string location)
        { 
            return jobService.GetJobsForReport(startDate, endDate,customers,status , city , lastactive , location);
        }
        [HttpGet]
        public List<JobQuotationDTO> GEtlatestJobsForOneDayReport(System.DateTime StartDate , System.DateTime EndDate)
        {
            return jobService.GEtlatestJobsForOneDayReport(StartDate , EndDate);
        }
        [HttpGet]
        public List<BidsDTO> latestBidsForOneDayReport(System.DateTime StartDate, System.DateTime EndDate)
        {
            return jobService.latestJobsForOneDayReport(StartDate, EndDate);
        }
        [HttpGet]
        public List<JobQuotationDTO> GetPostedJobsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId , string jobDetId, string userType)
        {
            return jobService.GetPostedJobsForDynamicReport(StartDate, EndDate, FromDate, ToDate, customer, tradesman, status, city, lastActive, location, jobId, jobDetId, userType);
        }
        [HttpGet]
        public List<BidsDTO> GetBidsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId)
        {
            return jobService.GetBidsForDynamicReport(StartDate, EndDate, FromDate, ToDate, customer, tradesman, status, city, lastActive, location, jobId, customerId);
        }

        [HttpGet]
        public Response DeleteJobByQuotationId(long jobQuotationId, string actionPageName)
        {
             return  jobService.DeleteJobByQuotationId(jobQuotationId, actionPageName);
        } 
        [HttpGet]
        public Response ApproveJob(long jobQuotationId)
        {
              return jobService.ApproveJob(jobQuotationId);
        }
        [HttpPost]
        public async Task<Response> UpdateJobBudget([FromBody] UpdateJobBudgetVM budgetVM)
        {
            return await jobService.UpdateJobBudget(budgetVM);
        }        
        [HttpPost]
        public async Task<Response> UpdateJobExtraCharges([FromBody] UpdateJobBudgetVM updateJob)
        {
            return await jobService.UpdateJobExtraCharges(updateJob);
        }
        [HttpPost]
        public Response AssignJobToTradesman([FromBody] AssignJobVM assignJobVM)
        {
            return jobService.AssignJobToTradesman(assignJobVM);
        }
        [HttpGet]
        public Response GetBidCountByJobId(long tradesmanId, long jobQuotationId)
        {
            return jobService.GetBidCountByJobId(tradesmanId,jobQuotationId);
        }
        [HttpGet]
        public List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo,long jobQuotationId)
        {
            return jobService.GetTradesmanByName(tradesmanName, tradesmanId, tradesmanPhoneNo, jobQuotationId);
        }

        public bool GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId)
        {

            return jobService.GetBidStatusForTradesmanId(jobQuotationId, tradesmanId, statusId);
        }

        [HttpGet]
        public List<EscalateOptionVM> GetEscalateOptionsList()
        {
            return jobService.GetEscalateOptionsList();
        }

        public Response InsertAndUpdateEscalateOption([FromBody] EscalateOptionVM escalateOptionVM)
        {
            return jobService.InsertAndUpdateEscalateOption(escalateOptionVM);
        }
        #endregion

        [HttpPost]
        public async Task<Response> JobPostByFacebookLeads([FromBody] string data)
        {
            return await jobService.JobPostByFacebookLeads(data);
        }

        [HttpPost]
        public async Task<Response> GetUserFromFacebookLeads([FromBody] string data)
        {
            return await jobService.GetUserFromFacebookLeads(data);
        }

    }
}
