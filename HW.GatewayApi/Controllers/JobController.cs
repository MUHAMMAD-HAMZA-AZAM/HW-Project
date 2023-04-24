using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    //[Authorize(Policy = "RolePolicy")]
    [Produces("application/json")]
    public class JobController : BaseController
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.jobService = jobService;
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<string> GetAllJobs()
        {
            return await jobService.GetAllJobs();
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<string> GetById(long id)
        {
            return await jobService.GetJobById(id);
        }

        [HttpGet]
        public Task<TradesmanProfileImage> GetTradesmanImageById(long tradesmanId)
        {
          return jobService.GetTradesmanImageById(tradesmanId);
        }

        [HttpGet]
        public Task<CustomerProfileImage> GetImageByCustomerId(long customerId)
        {
           return jobService.GetImageByCustomerId(customerId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<string> GetAllBids()
        {
            return await jobService.GetAllBids();
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddEscalateIssue([FromBody] DisputeVM disputeVM)
        {
            disputeVM.CreatedBy = DecodeTokenForUser().Id;
            disputeVM.CustomerId = await GetEntityIdByUserId();
            return await jobService.AddEscalateIssue(disputeVM);

        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<DisputeVM>> GetCompletedJobDetailsByCustomerAndStatusIds(long statusId)
        {
            return await jobService.GetCompletedJobDetailsByCustomerAndStatusIds(await GetEntityIdByUserId(), statusId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<DispVM>> GetDisputeRecord()
        {
            return await jobService.GetDisputeRecord(await GetEntityIdByUserId());
        }

        public async Task<Response> updateStatuse(long disputeId, int disputeStatusId)
        {
            return await jobService.updateStatuse(disputeId, disputeStatusId);

        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<MyQuotationsVM>> GetPostedJobsByCustomerId()
        {
            return await jobService.GetPostedJobsByCustomerId(await GetEntityIdByUserId());
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<JobQuotationDetailVM> GetJobQuotationByJobQuotationId(long jobQuotationId)
        {
            return await jobService.GetJobQuotationByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<FinishedJobVM>> GetFinishedJobList(int statusId)
        {

            return await jobService.GetFinishedJobList(await GetEntityIdByUserId(), statusId);
        }
        //[HttpPost]
        //public async Task<List<FinishedJobVM>> FinishedJobsList(int statusId)
        //{

        //    return await jobService.GetFinishedJobList(await GetEntityIdByUserId(),statusId);
        //}

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<FinishedJobDetailsVM> GetFinishedJobDetails(long jobDetailId)
        {
            long id = await GetEntityIdByUserId();
            return await jobService.GetFinishedJobDetails(jobDetailId);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> PostTradesmanFeedback([FromBody] TradesmanRatingsVM tradesmanRatingsVM)
        {
            tradesmanRatingsVM.CustomerId = await GetEntityIdByUserId();
            tradesmanRatingsVM.CreatedBy = DecodeTokenForUser().Id;

            return await jobService.PostTradesmanFeedback(tradesmanRatingsVM);
        }
        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> MarkAsFinishedJob([FromBody] MarkAsFinishJobVM finishedJobVM)
        {
           // finishedJobVM.TradesmanId = await GetEntityIdByUserId();
            finishedJobVM.UserId = DecodeTokenForUser().Id;
            return await jobService.MarkAsFinishedJob(finishedJobVM);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<QuotationBidsVM>> GetQuotationBids(long quotationId, int sortId = 0)
        {
            string userId = DecodeTokenForUser().Id;
            return await jobService.GetQuotationBids(quotationId, sortId, userId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> UpdateSelectedBid(long BidId, bool IsSelected)
        {
            return await jobService.UpdateSelectedBid(BidId, IsSelected);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> UpdateBidStatus(long BidId, int statusId)
        {
            return await jobService.UpdateBidStatus(BidId, statusId);
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> UpdateJobDetailStatus(long jobDetailId, int statusId)
        {
            return await jobService.UpdateJobDetailStatus(jobDetailId, statusId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddJobDetails(long bidId, int paymentMethod, int statusId)
        {
            return await jobService.AddJobDetails(bidId, paymentMethod, statusId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<BidDetailsVM> GetBidDetails(long bidId)
        {
            return await jobService.GetBidDetails(bidId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<InprogressVM>> GetAlljobDetails(long statusId)
        {
            return await jobService.GetAlljobDetails(await GetEntityIdByUserId(), statusId);

        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<InprogressJobDetailVM> GetInprogressJobDetail(long JobQuotationId)
        {
            return await jobService.GetInprogressJobDetail(JobQuotationId);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> SetSupplierRating([FromBody] RateSupplierVM rateSupplierVM)
        {
            rateSupplierVM.CustomerId = await GetEntityIdByUserId();
            rateSupplierVM.CreatedBy = DecodeTokenForUser().Id;
            return await jobService.SetSupplierRating(rateSupplierVM);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> UpdateJobCost(long jobDetailId, decimal jobCost)
        {
            return await jobService.UpdateJobCost(jobDetailId, jobCost);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(int pageNumber, int pageSize)
        {
            return await jobService.GetJobLeadsByTradesmanId(await GetEntityIdByUserId(), pageNumber, pageSize);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<JobLeadsWebVM>> GetJobLeadsWebByTradesmanId(int pageNumber, int pageSize)
        {
            return await jobService.GetJobLeadsWebByTradesmanId(await GetEntityIdByUserId(), pageNumber, pageSize);
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<JobQuotationDetailVM> GetJobQuotationDataByQuotationId(long jobQuotationId)
        {
            return await jobService.GetJobQuotationDataByQuotationId(jobQuotationId);
        }

        [HttpGet]
        public async Task<List<UserViewModels.ImageVM>> GetQuoteImagesByJobQuotationIdWeb(long jobQuotationId)
        {
            return await jobService.GetQuoteImagesByJobQuotationIdWeb(jobQuotationId);
        }

        [HttpGet]
        public async Task<MediaVM> GetJobQuotationMediaByQuotationId(long jobQuotationId)
        {
            return await jobService.GetJobQuotationMediaByQuotationId(jobQuotationId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<List<BidVM>> GetActiveBidsDetails(int pageNumber, int pageSize, int bidsStatusId)
        {

            return await jobService.GetActiveBidsDetails(await GetEntityIdByUserId(), pageNumber, pageSize, bidsStatusId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<List<BidVM>> GetDeclinedBidsDetails(int pageNumber, int pageSize, int bidsStatusId)
        {

            return await jobService.GetDeclinedBidsDetails(await GetEntityIdByUserId(), pageNumber, pageSize, bidsStatusId);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<List<BidWebVM>> GetActiveBidsDetailsWeb(int pageNumber, int pageSize, int bidsStatusId)
        {

            return await jobService.GetActiveBidsDetailsWeb(await GetEntityIdByUserId(), pageNumber, pageSize, bidsStatusId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<List<BidWebVM>> GetDeclinedBidsDetailsWeb(int pageNumber, int pageSize, int bidsStatusId)
        {

            return await jobService.GetDeclinedBidsDetailsWeb(await GetEntityIdByUserId(), pageNumber, pageSize, bidsStatusId);
        }



        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<Response> GetJobDetail(int jobStatusId, int pageNumber, int pageSize)
        {
            return await jobService.GetJobDetail(await GetEntityIdByUserId(), pageNumber, pageSize, jobStatusId);
        }

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<string> GetJobDetailWeb(int jobStatusId, int pageNumber, int pageSize)
        {
            return await jobService.GetJobDetailWeb(await GetEntityIdByUserId(), pageNumber, pageSize, jobStatusId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<MyQuotationsVM>> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, int statusId,bool bidStatus)
        {
            return await jobService.SpGetPostedJobsByCustomerId(pageNumber, pageSize, await GetEntityIdByUserId(), statusId, bidStatus);
        }
        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<MyQuotationsVM>> SpGetJobsByCustomerId()
        {
          return await jobService.SpGetJobsByCustomerId(await GetEntityIdByUserId());
        }
        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<MyQuotationsVM>> GetPostedJobs(int pageNumber, int pageSize)
        {
            return await jobService.GetPostedJobs(pageNumber, pageSize, await GetEntityIdByUserId());
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<InprogressVM>> GetInprogressJob(int pageNumber, int pageSize, int statusId)
        {
            return await jobService.GetInprogressJob(pageNumber, pageSize, await GetEntityIdByUserId(), statusId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<UserViewModels.VideoVM> GetQuoteVideoByJobQuotationId(long jobQuotationId)
        {
            return await jobService.GetQuoteVideoByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<UserViewModels.ImageVM> GetQuoteImageById(long jobImageId)
        {
            return await jobService.GetQuoteImageById(jobImageId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<UserViewModels.AudioVM> GetQuoteAudioByJobQuotationId(long jobQuotationId)
        {
            return await jobService.GetQuoteAudioByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<FinishedJobVM>> GetFinishedJob(int statusId, int pageNumber, int pageSize)
        {
            return await jobService.GetFinishedJob(await GetEntityIdByUserId(), statusId, pageNumber, pageSize);
        }

        [HttpGet]
        public Task<List<WebLiveLeadsVM>> WebLiveLeads(long jobQuotationId)
        {
            return jobService.WebLiveLeads(jobQuotationId);
        }

        [HttpGet]
        public Task<List<IdValueVM>> WebLiveLeadsLatLong()
        {
            return jobService.WebLiveLeadsLatLong();
        }

        [HttpGet]
        public async  Task<List<WebLiveLeadsVM>> WebLiveLeadsPanel(int statusId, int pageNumber, int pageSize)
        {
            var TradesmanId = await GetEntityIdByUserId();
            return await jobService.WebLiveLeadsPanel(TradesmanId,statusId, pageNumber, pageSize);
        }

        [HttpGet]
        public async Task<List<CompletedJobListVm>> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, string startDate, string endDate, string city, string location, string fromDate, string toDate, string dataOrderBy = "")
        {
            return await jobService.CompletedJobListAdmin(pageNumber, pageSize, customerName, jobId, jobDetailId, startDate, endDate, city, location, fromDate, toDate, dataOrderBy);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> PostJobContactInfo([FromBody] PersonalDetailsVM personalDetailsVM)
        {
            personalDetailsVM.EntityId = await GetEntityIdByUserId();
            //personalDetailsVM.CreatedBy = DecodeTokenForUser().Id;
            return await jobService.PostJobContactInfo(personalDetailsVM);
        }
        [HttpPost]
        public async Task<Response> submitIssue([FromBody] EsclateRequest esclateRequest)
        {
            if(esclateRequest.CustomerId == 0)
            {
                esclateRequest.CustomerId = await GetEntityIdByUserId();
                esclateRequest.CreatedBy = DecodeTokenForUser().Id;
            }
            else if(esclateRequest.TradesmanId == 0)
            {
                esclateRequest.TradesmanId = await GetEntityIdByUserId();
                esclateRequest.CreatedBy = DecodeTokenForUser().Id;
            }
            
            esclateRequest.CreatedOn = DateTime.Now;
            return await jobService.submitIssue(esclateRequest);
        }
        [HttpGet]
        public async Task<Response> getEscalateIssueByJQID(long jobQuotationId , int userRole, int status)
        {
            return await jobService.getEscalateIssueByJQID(jobQuotationId, userRole, status);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<List<GetJobsCountVM>> GetJobsCount()
        {
            var aa = await GetEntityIdByUserId();
            return await jobService.GetJobsCount(aa);
        }

        [HttpGet]
        public async Task<bool> GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId)
        {
            return await jobService.GetBidStatusForTradesmanId(jobQuotationId, tradesmanId, statusId);
        }
        [HttpGet]
        public async Task<string> GetBidCountsOnJob(long jobQuotationId)
        {
            return await jobService.GetBidCountsOnJob(jobQuotationId);
        }
        [HttpPost]
        public async Task<List<JobImages>> GetJobImagesListByJobQuotationIds([FromBody] List<long> jobQuotationIds)
        {
            return await jobService.GetJobImagesListByJobQuotationIds(jobQuotationIds);
        }        
        [HttpGet]
        public async Task<List<EsclateOption>> getEscalateOptions(int userRole)
        {
            return await jobService.getEscalateOptions(userRole);
        }
        [HttpPost]
        public async Task<Response> UpdateBidByStatusId([FromBody] BidDetailsVM bidDetailsVM)
        {
            return await jobService.UpdateBidByStatusId(bidDetailsVM);
        }        
        [HttpPost]
        public async Task<string> GetAcceptedBidsList([FromBody] BidDetailsVM bidDetailsVM)
        {
            return await jobService.GetAcceptedBidsList(bidDetailsVM);
        }        
        [HttpPost]
        public async Task<Response> StartOrFinishJob([FromBody] BidDetailsVM bidDetailsVM)
        {
            return await jobService.StartOrFinishJob(bidDetailsVM);
        }       
        [HttpPost]
        public async Task<Response> UpdateJobAdditionalCharges([FromBody] BidDetailsVM bidDetailsVM)
        {
            return await jobService.UpdateJobAdditionalCharges(bidDetailsVM);
        }
        public async Task<Response> JobStartNotification(long tradesmanId, long? customerId, long bidId, string jobTitle,long jobQuotationId)
        {
            return await jobService.JobStartNotification(tradesmanId,customerId,bidId,jobTitle,jobQuotationId);
            
        }
        public async Task<Response> JobStartNotificationForTradesman(long tradesmanId, long? customerId, long bidId, string jobTitle,long jobQuotationId)
        {
            return await jobService.JobStartNotificationForTradesman(tradesmanId,customerId,bidId,jobTitle,jobQuotationId);
            
        }
        public async Task<Response> JobFifnishedNotification(long JobQuotationId)
        {
            return await jobService.JobFifnishedNotification(JobQuotationId);
            
        }

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
        [HttpPost]
        public async Task<string> GetInprogressJobsMobile([FromBody] InProgressJobsVM inProgressJobsVM)
        {
            return await jobService.GetInprogressJobsMobile(inProgressJobsVM);
        }
    }
}
