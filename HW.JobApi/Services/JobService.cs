using HW.Job_ViewModels;
using HW.JobModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BidStatus = HW.Utility.BidStatus;
using HW.ReportsViewModels;
using System.Security.Cryptography.X509Certificates;
using HW.IdentityViewModels;
using HW.JobModels.DTOs;

namespace HW.JobApi.Services
{
    public interface IJobService
    {

        Task<JobDetail> GetAJobById(long id);
        Task<JobDetail> GetAJobById(long id, long tradesmanId);
        Task<JobDetail> GetAJobByJobDeatilId(long id, long tradesmanId);
        TradesmanFeedback GetFeedBack(long JobDetailId, long tradesmanId);
        //IQueryable<TradesmanFeedback> GetTradesmanFeedBack(long id, long tradesmanId);
        List<TradesmanFeedback> GetTradesmanFeedBack(long tradesmanId);
        IQueryable<Bids> GetActiveBids(long tradesmanId, int fragmentId);
        Task<List<JobDetail>> GetJobsDetail(long tradesmanId, int jobStatusId);
        Task<List<JobQuotation>> GetJobQuotationsByIds(List<long> jobQuotationIds);
        List<JobQuotation> GetJobQuotationsBySkillId(List<long> skillId, long tradesmanCity, int pageNumber);
        IQueryable<Bids> GetAllBids();
        List<Bids> GetBidListByCustomerId(long customerIds, int statusId);
        //List<JobQuotation> GetJobQuotationsBySkillId(long skillId);
        IQueryable<JobQuotation> GetAllJob();
        JobQuotation GetByCustomerId(long customerId);
        JobDetail GetJobDetailsById(long jobDetailId);
        JobAddress GetJobAddress(long jobQuotationId);
        JobDetail GetJobDetailsByJobQuotationId(long jobQuotationId);
        Task<JobQuotation> GetJobQuotationById(long id);
        List<Bids> GetQuotationBids(long quotationId);
        IQueryable<JobDetail> GetJobsByTradesmanIds(List<long> tradesmanIds);
        Response SubmitBids(Bids bids);
        Task<bool> UpdateSelectedBid(long BidId, bool IsSelected);
        Response AddEscalateIssue(Dispute dispute);
        IQueryable<Bids> GetBidCounts(List<long> jobQuotationIds);
        long GetBidCountsOnJob(long jobQuotationId);
        List<JobDetail> GetCompletedJobDetailsByCustomerAndStatusIds(long customerId, long statusId);
        List<Dispute> GetDisputeRecord(long customerid);
        void AddJobQuotationFavoriteTradesmen(List<FavoriteTradesman> favoriteTradesman);
        List<DisputeStatus> GetDisputeStatusByStatusIds(List<long> statusIds);
        Response updateStatuse(long disputeId, int disputeStatusId);
        List<JobQuotation> GetPostedJobsByCustomerId(long customerId);
        Task<JobQuotation> GetJobQuotationByJobQuotationId(long jobQuotationId);
        JobAddress GetJobAddressByJobQuotationId(long JobQuotationId);
        List<FavoriteTradesman> GetFavoriteTradesmenByJobQuotationId(long jobQuotationId);
        List<JobDetail> GetJobStatusByCustomerId(long customerId, int statusId);
        List<JobAddress> GetJobAddressByJobQuotationIds(List<long> jobQuotationIds);
        List<TradesmanFeedback> GetTradesmanFeedbackByCustomerIds(List<long> customerIds);
        Response PostTradesmanFeedback(TradesmanFeedback tradesmanFeedback);
        Task<Bids> GetBidById(long id);
        Task<Response> UpdateBidStatus(long BidId, int statusId);
        bool UpdateJobDetailStatus(long jobDetailId, int statusId);
        Task<Response> AddJobDetails(long bidId, int paymentMethod);
        List<SupplierFeedback> GetSupplierFeedbackBySupplierId(long supplierId);
        Response SetSupplierRating(SupplierFeedback supplierFeedback);
        List<long> GetJobDetails(long customerid);
        long JobQuotations(JobQuotation quotationVM);
        Bids GetBidJobQuotaionId(long jobQuotationId, long tradesmanId);
        Response SaveJobAddress(JobAddress jobAddress);
        bool MarkAsFinishedJob(JobDetail jobDetail);
        List<JobDetail> GetAlljobDetails(long customerId, long statusId);
        Task<Response> UpdateJobQuotation(JobQuotation jobQuotation);
        Task<Response> DeleteJobAddressByJobQuotationId(long jobQuotationId);
        Task<Response> DeleteJobQuotation(long jobQuotationId);
        void DeleteEscalateIssue(long disputeId);
        long GetQuotationIdByBidId(long bidId);
        long GetJobDetailIdByQuotationId(long quotationId);
        IQueryable<Bids> GetJobQuotationBidsByJobQuotatationIds(List<long> jobQuotationIds);
        void DeleteJobDetailsByJobQuotationId(long jobQuotationId);
        Response UpdateJobCost(long jobDetailId, decimal jobCost);
        List<JobLeadsVM> GetJobQuotationsBySkill(long tradesmanId, int pageNumber, int pageSize);
        List<JobLeadsWebVM> GetJobQuotationsWebBySkill(long tradesmanId, int pageNumber, int pageSize);
        List<BidVM> GetActiveBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId);
        List<BidVM> GetDeclinedBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId);

        List<BidWebVM> GetActiveBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId);
        List<BidWebVM> GetDeclinedBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId);

        List<MyQuotationsVM> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, long customerId, int statusId, bool bidStatus);
        List<MyQuotationsVM> SpGetJobsByCustomerId(GetJobsParams getJobsParams);
        List<MyQuotationsVM> GetPostedJobs(int pageNumber, int pageSize, long customerId);
        List<InprogressVM> GetInprogressJob(long customerId, int statusId, int pageNumber, int pageSize);
        Response GetJobDetail(int pageNumber, int pageSize, long tradesmanId, int jobStatusId);
        Response GetJobDetailWeb(int pageNumber, int pageSize, long tradesmanId, int jobStatusId);
        List<FinishedJobVM> GetFinishedJob(long customerId, int statusId, int pageNumber, int pageSize);
        Response GetTradesmanByBidId(long bidId);
        Response Sp_GetTradesmanFirebaseClientId(long jobQuotationId);
        List<long> GetBidCountByJobQuotationId(long jobQuotationId);
        bool CheckFeedBackStatus(long jobDetailId);
        Response UpdateJobQuotationStatus(long jobQuotationId, int statusId);
        List<WebLiveLeadsVM> WebLiveLeads(long jobQuotationId);
        List<IdValueVM> WebLiveLeadsLatLong();
        List<WebLiveLeadsVM> WebLiveLeadsPanel(long TradesmanId, int statusId, int pageNumber, int pageSize);
        JobsVm GetAllJobsCount();
        JobQuotation JobDetailsByJQID(long jobQuotationId);
        List<ActiveJobListVM> GetActiveJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city, string location);
        List<ActiveJobListVM> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, DateTime?
            startDate, DateTime? endDate, string city, string status, string jobdetailid, DateTime? fromDate, DateTime? toDate,
            string tradesmanName, string usertype, string location, bool athorize, string cSJobStatusId,string townId,string area);
        List<DeletedJobListVM> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, DateTime?
            startDate, DateTime? endDate, string city, string status, string jobdetailid, DateTime? fromDate, DateTime? toDate,
            string tradesmanName, string usertype, string location, string cSJobStatusId, string town);
        List<CompletedJobListVm> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location, DateTime? fromDate, DateTime? toDate, string dataOrderBy = "");
        List<InprogressJobList> GetInprogressJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location);
        JobQuotationDTO GetJobDetailsList(long customerId);
        List<ReciveBidsList> GetReciveBids(long jobQoutationId);
        ReciveBidDetails GetReciveBidDetails(long jobQoutationId);
        List<Status> JobStatusForDropdown();
        List<Status> GetCsJobStatusDropdown();
        List<JobReportWithStatusCustomerVM> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location);
        List<JobQuotationDTO> GEtlatestJobsForOneDayReport(DateTime StartDate, DateTime EndDate);
        List<BidsDTO> latestJobsForOneDayReport(DateTime StartDate, DateTime EndDate);
        List<JobQuotationDTO> GetPostedJobsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId, string jobDetId, string userType);
        List<BidsDTO> GetBidsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId);
        List<JobQuotation> GetPostedJobsAddressList();
        Response DeleteJobByQuotationId(long jobQuotationId, string actionPageName);
        Response ApproveJob(long jobQuotationId);
        List<JobTown> GetTownsList();
        Response PostJobContactInfo(PersonalDetailsVM personalDetailsVM);
        List<GetJobsCountVM> GetJobsCount(long tradesmanId);
        Response JobAuthorizer(JobAuthorizerVM jobAuthorizerVM);
        List<JobAuthorizerVM> JobAuthorizerList();
        List<JobAuthorizerVM> AdminJobAuthorizerList();
        Task<Response> UpdateJobBudget(UpdateJobBudgetVM budgetVM);
        Task<Response> UpdateJobExtraCharges(UpdateJobBudgetVM updateJob);
        Response AssignJobToTradesman(AssignJobVM assignJobVM);
        Response GetBidCountByJobId(long tradesmanId, long jobQuotationId);
        Task<Bids> GetBidByJQID(long id);
        List<InprogressJobList> GetUrgentJobsList();
        List<InprogressJobList> GetJobsListByCategory(InprogressJobList inprogressJobList);
        List<EsclateOption> getEscalateOptions(int userRole);
        List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo, long jobQuotationId);
        Response ChangeJobStatus(int jqId);
        bool GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId);
        Response submitIssue(EsclateRequest esclateRequest);
        List<EsclateRequestVM> EscalateIssuesRequestList(EsclateRequestVM esclateRequestVM);
        List<EsclateRequestVM> AuthorizeEscalateIssuesList(EsclateRequestVM esclateRequestVM);
        List<EscalateOptionVM> GetEscalateOptionsList();
        Response InsertAndUpdateEscalateOption(EscalateOptionVM escalateOptionVM);
        Response AuthorizeEscalateIssueRequest(long escalateIssueId);
        Response getEscalateIssueByJQID(long jobQuotationId,int userRole,int status);
        Task<Response> UpdateBidByStatusId(BidDetailsVM bidDetailsVM);
        Response GetAcceptedBidsList(BidDetailsVM bidDetailsVM);
        Response GetInprogressJobsMobile(InProgressJobsVM inProgressJobsVM);
        Response StartOrFinishJob(BidDetailsVM bidDetailsVM);
        Response UpdateJobAdditionalCharges(BidDetailsVM bidDetailsVM);
        Task<Response> JobPostByFacebookLeads(string data);
        Task<Response> GetUserFromFacebookLeads(string data);

    }
    public class JobService : IJobService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public JobService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public IQueryable<JobQuotation> GetAllJob()
        {
            try
            {
                return uow.Repository<JobQuotation>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotation>().AsQueryable();
            }
        }

        public async Task<JobDetail> GetAJobById(long id)
        {
            try
            {
                return await uow.Repository<JobDetail>().GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobDetail();
            }
        }
        //public JobDetail GetJobDetailsByCustomerId(long id)
        //{
        //    return uow.Repository<JobDetail>().GetAll().FirstOrDefault(x => x.CustomerId == id);
        //}

        public JobDetail GetJobDetailsById(long jobDetailId)
        {
            try
            {
                return uow.Repository<JobDetail>().Get(x => x.JobDetailId == jobDetailId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobDetail();
            }
        }

        public JobDetail GetJobDetailsByJobQuotationId(long jobQuotationId)
        {
            try
            {
                return uow.Repository<JobDetail>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobDetail();
            }
        }

        public async Task<Bids> GetBidById(long id)
        {
            try
            {
                return await uow.Repository<Bids>().GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Bids();
            }
        }

        public Bids GetBidJobQuotaionId(long jobQuotationId, long tradesmanId)
        {
            try
            {
                return uow.Repository<Bids>().Get().Where(b => b.JobQuotationId == jobQuotationId && b.TradesmanId == tradesmanId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Bids();
            }
        }

        public List<Bids> GetQuotationBids(long quotationId)
        {
            return uow.Repository<Bids>().GetAll().Where(x => x.JobQuotationId == quotationId && x.StatusId == (int)BidStatus.Active).ToList();
        }

        public IQueryable<JobDetail> GetJobsByTradesmanIds(List<long> tradesmanIds)
        {
            try
            {
                IQueryable<JobDetail> totalDoneJob = uow.Repository<JobDetail>().GetAll().Where(x => x.StatusId == (int)BidStatus.Completed && tradesmanIds.Contains(x.TradesmanId));
                return totalDoneJob;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobDetail>().AsQueryable();
            }
        }

        public async Task<bool> UpdateSelectedBid(long BidId, bool IsSelected)
        {
            try
            {
                if (BidId >= 0)
                {
                    Bids bid = await uow.Repository<Bids>().GetByIdAsync(BidId);
                    bid.IsSelected = IsSelected;
                    uow.Repository<Bids>().Update(bid);
                    await uow.SaveAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<Response> UpdateJobQuotation(JobQuotation jobQuotation)
        {
            Response response = new Response();
            try
            {
                if (jobQuotation != null)
                {
                    JobQuotation existingData = await uow.Repository<JobQuotation>().GetByIdAsync(jobQuotation.JobQuotationId);
                    if (existingData != null)
                    {
                        jobQuotation.CreatedOn = existingData.CreatedOn;
                        jobQuotation.SkillId = existingData.SkillId;
                        jobQuotation.CustomerId = existingData.CustomerId;

                        JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        string jsonValues = JsonConvert.SerializeObject(jobQuotation, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<JobQuotation>().Update(existingData);
                        await uow.SaveAsync();

                        response.Message = "Quotation Updated Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Quotation could not be Updated";
                        response.Status = ResponseStatus.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public Response UpdateJobQuotationStatus(long jobQuotationId, int statusId)
        {
            Response response = new Response();

            try
            {
                JobQuotation jobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();

                if (jobQuotation != null)
                {
                    jobQuotation.StatusId = statusId;

                    uow.Repository<JobQuotation>().Update(jobQuotation);
                    uow.Save();

                    response.Message = "Job status updated successfully";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Job not found";
                    response.Status = ResponseStatus.Error;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> DeleteJobQuotation(long jobQuotationId)
        {
            Response response = new Response();
            try
            {
                if (jobQuotationId > 0)
                {
                    await uow.Repository<JobQuotation>().DeleteAsync(jobQuotationId);
                    await uow.SaveAsync();

                    response.Message = "Quotation Removed Successfully!";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Quotation could not be Removed";
                    response.Status = ResponseStatus.Error;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> UpdateBidStatus(long BidId, int statusId)
        {
            Response response = new Response();
            try
            {
                if (BidId >= 0)
                {
                    Bids bid = await uow.Repository<Bids>().GetByIdAsync(BidId);
                    bid.StatusId = statusId;
                    uow.Repository<Bids>().Update(bid);

                    if (statusId == (int)BidStatus.Accepted)
                    {
                        List<Bids> bids = uow.Repository<Bids>().Get(x => x.BidsId != bid.BidsId && x.JobQuotationId == bid.JobQuotationId).ToList();
                        if (bids != null)
                        {
                            foreach (Bids item in bids)
                            {
                                item.StatusId = (int)BidStatus.Declined;
                                uow.Repository<Bids>().Update(item);
                            }
                        }
                        response.ResultData = bids?.Select(x => x.TradesmanId).ToList();

                        JobQuotation jobQuotation = uow.Repository<JobQuotation>().GetById(bid.JobQuotationId);
                        jobQuotation.StatusId = (int)BidStatus.Completed;
                        uow.Repository<JobQuotation>().Update(jobQuotation);
                    }
                    else if (statusId == (int)BidStatus.Active)
                    {
                        JobQuotation jobQuotation = await uow.Repository<JobQuotation>().GetByIdAsync(bid.JobQuotationId);
                        jobQuotation.StatusId = statusId;
                        uow.Repository<JobQuotation>().Update(jobQuotation);
                    }

                    await uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Task Completed";

                    return response;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            response.Status = ResponseStatus.Error;
            response.Message = "Task Failed";
            return response;
        }

        public bool UpdateJobDetailStatus(long jobDetailId, int statusId)
        {
            try
            {
                if (jobDetailId >= 0)
                {
                    JobDetail jobDetail = uow.Repository<JobDetail>().Get(x => x.JobQuotationId == jobDetailId).FirstOrDefault();
                    jobDetail.StatusId = statusId;
                    if (statusId == (int)BidStatus.Completed)
                    {
                        jobDetail.EndDate = DateTime.Now;
                    }
                    uow.Repository<JobDetail>().Update(jobDetail);
                    uow.Save();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return false;
        }

        public async Task<Response> AddJobDetails(long bidId, int paymentMethod)
        {

            Response response = new Response();
            try
            {
                if (bidId >= 0)
                {
                    Bids bid = await uow.Repository<Bids>().GetByIdAsync(bidId);
                    JobQuotation jobQuotation = await uow.Repository<JobQuotation>().GetByIdAsync(bid.JobQuotationId);

                    JobDetail jobDetail = new JobDetail()
                    {
                        Title = jobQuotation.WorkTitle,
                        Description = jobQuotation.WorkDescription,
                        CustomerId = jobQuotation.CustomerId,
                        JobQuotationId = jobQuotation.JobQuotationId,
                        TradesmanId = bid.TradesmanId,
                        StatusId = Convert.ToInt32(BidStatus.Active),
                        TradesmanBudget = bid.Amount,
                        Budget = (decimal)jobQuotation.WorkBudget,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        CreatedOn = DateTime.Now,
                        CreatedBy = jobQuotation.CustomerId.ToString(),
                        SkillId = jobQuotation.SkillId,
                        SubSkillId = jobQuotation.SubSkillId,
                        ServiceCharges = jobQuotation.ServiceCharges,
                        OtherCharges = jobQuotation.OtherCharges,
                        PaymentStatus = paymentMethod
                    };

                    await uow.Repository<JobDetail>().AddAsync(jobDetail);
                    await uow.SaveAsync();


                    response.Message = "Successfull Created.";
                    response.Status = ResponseStatus.OK;
                    response.ResultData = jobQuotation.JobQuotationId;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public void DeleteJobDetailsByJobQuotationId(long jobQuotationId)
        {
            try
            {
                JobDetail jobDetail = uow.Repository<JobDetail>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();
                uow.Repository<JobDetail>().Delete(jobDetail);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task<JobDetail> GetAJobById(long id, long tradesmanId)
        {
            try
            {
                JobDetail jobDetail = new JobDetail();
                jobDetail = uow.Repository<JobDetail>().Get((j => j.TradesmanId == tradesmanId && j.JobQuotationId == id)).FirstOrDefault();
                return jobDetail;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobDetail();
            }
        }
        public async Task<JobDetail> GetAJobByJobDeatilId(long id, long tradesmanId)
        {
            try
            {
                JobDetail jobDetail = new JobDetail();
                jobDetail = uow.Repository<JobDetail>().Get((j => j.TradesmanId == tradesmanId && j.JobDetailId == id)).FirstOrDefault();
                return jobDetail;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobDetail();
            }
        }

        public JobQuotation GetByCustomerId(long customerId)
        {
            try
            {
                return uow.Repository<JobQuotation>().Get(x => x.CustomerId == customerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotation();
            }
        }

        public IQueryable<Bids> GetAllBids()
        {
            try
            {
                return uow.Repository<Bids>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Bids>().AsQueryable();
            }
        }

        public IQueryable<Bids> GetActiveBids(long tradesmanId, int bidsStatusId)
        {
            try
            {
                IQueryable<Bids> query = uow.Repository<Bids>().Get(c => c.TradesmanId == tradesmanId && c.StatusId == bidsStatusId).OrderByDescending(o => o.CreatedOn);
                return query;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Bids>().AsQueryable();
            }
        }
        public List<Bids> GetBidListByCustomerId(long customerId, int statusId)
        {
            try
            {
                List<Bids> bids = uow.Repository<Bids>().Get(x => x.CustomerId == customerId && x.StatusId == statusId).OrderByDescending(x => x.CreatedOn).ToList();
                return bids;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Bids>();
            }
        }

        public async Task<List<JobQuotation>> GetJobQuotationsByIds(List<long> jobQuotationIds)
        {
            try
            {
                List<JobQuotation> activeJobs = new List<JobQuotation>();
                IQueryable<JobQuotation> query = uow.Repository<JobQuotation>().GetAll();
                activeJobs = query.Where(c => jobQuotationIds.Any(id => c.JobQuotationId == id)).ToList();
                return activeJobs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotation>();
            }
        }

        public async Task<List<JobDetail>> GetJobsDetail(long tradesmanId, int jobStatusId)
        {
            try
            {
                List<JobDetail> activeBids = new List<JobDetail>();
                IQueryable<JobDetail> query = uow.Repository<JobDetail>().GetAll();
                if (jobStatusId == (int)BidStatus.Active)
                {
                    activeBids = query.Where(c => c.TradesmanId == tradesmanId && c.StatusId == jobStatusId).OrderByDescending(o => o.CreatedOn).ToList();
                }
                else
                {
                    activeBids = query.Where(c => c.TradesmanId == tradesmanId && c.StatusId == jobStatusId).OrderByDescending(o => o.EndDate).ToList();
                }
                return activeBids;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobDetail>();
            }
        }

        public List<JobQuotation> GetJobQuotationsBySkillId(List<long> skillId, long tradesmanCity, int pageNumber)
        {

            List<JobQuotation> liveleade = new List<JobQuotation>();
            try
            {
                List<long> jobaddres = uow.Repository<JobAddress>().GetAll().Where(s => s.CityId == tradesmanCity).Select(x => x.JobQuotationId).ToList();
                //check valid page number
                List<JobQuotation> joblist = uow.Repository<JobQuotation>().GetAll().Where(c => jobaddres.Any(id => id == c.JobQuotationId) &&
                skillId.Contains(c.SkillId) && c.StatusId == (int)BidStatus.Active).ToList();
                int validpagenumber = (joblist.Count / 10);


                if (pageNumber < validpagenumber + 1)
                {
                    liveleade = uow.Repository<JobQuotation>().GetAll().Where(c => jobaddres.Any(id => id == c.JobQuotationId) &&
                    skillId.Contains(c.SkillId) && c.StatusId == (int)BidStatus.Active).
                    OrderByDescending(x => x.CreatedOn).Page(pageNumber).ToList();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }

            return liveleade;
        }

        public List<JobQuotation> GetJobQuotationsByOrganizationSkillIds(List<long> skillIds, long organizationCity)
        {
            try
            {
                List<long> jobaddres = uow.Repository<JobAddress>().GetAll().Where(s => s.CityId == organizationCity).Select(x => x.JobQuotationId).ToList();
                List<JobQuotation> liveleade = uow.Repository<JobQuotation>().GetAll().Where(c => jobaddres.Any(id => id == c.JobQuotationId) && skillIds.Contains(c.SkillId) && c.StatusId == (int)BidStatus.Active).OrderByDescending(x => x.CreatedOn).ToList();
                return liveleade;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotation>();
            }
        }


        public TradesmanFeedback GetFeedBack(long JobDetailId, long tradesmanId)
        {
            try
            {
                return uow.Repository<TradesmanFeedback>().Get().FirstOrDefault(r => r.JobDetailId == JobDetailId && r.TradesmanId == tradesmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TradesmanFeedback();
            }
        }

        public List<TradesmanFeedback> GetTradesmanFeedBack(long tradesmanId)
        {
            try
            {
                return uow.Repository<TradesmanFeedback>().GetAll().Where(c => c.TradesmanId == tradesmanId).OrderByDescending(o => o.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanFeedback>();
            }
        }

        public Response SubmitBids(Bids bids)
        {
            Response response = new Response();
            try
            {
                if (bids.BidsId > 0)
                {
                    uow.Repository<Bids>().Update(bids);
                }
                else
                {
                    uow.Repository<Bids>().Add(bids);
                }
                uow.Save();

                response.ResultData = bids.BidsId;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
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

        public Response UpdateJobCost(long jobDetailId, decimal jobCost)
        {
            Response response = new Response();
            try
            {
                JobDetail jobDetail = uow.Repository<JobDetail>().GetById(jobDetailId);
                jobDetail.Budget = jobCost;
                uow.Repository<JobDetail>().Update(jobDetail);
                uow.Save();

                response.Status = ResponseStatus.OK;
                response.Message = "Job cost updated successfully";
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

        public async Task<JobQuotation> GetJobQuotationById(long id)
        {
            return await uow.Repository<JobQuotation>().GetByIdAsync(id);
        }

        public IQueryable<Bids> GetBidCounts(List<long> jobQuotationIds)
        {
            try
            {
                IQueryable<Bids> bidGroups = uow.Repository<Bids>().Get(x => x.StatusId == (int)BidStatus.Active && jobQuotationIds.Contains(x.JobQuotationId));
                return bidGroups;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Bids>().AsQueryable();
            }
        }

        public IQueryable<Bids> GetJobQuotationBidsByJobQuotatationIds(List<long> jobQuotationIds)
        {
            try
            {
                IQueryable<Bids> bidGroups = uow.Repository<Bids>().Get(x => jobQuotationIds.Contains(x.JobQuotationId));
                return bidGroups;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Bids>().AsQueryable();
            }
        }

        public Response AddEscalateIssue(Dispute dispute)
        {
            Response response = new Response();

            try
            {
                uow.Repository<Dispute>().Add(dispute);
                uow.Save();

                response.Status = ResponseStatus.OK;
                response.Message = "Dispute has been added successfully";
                response.ResultData = dispute.DisputeId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public void DeleteEscalateIssue(long disputeId)
        {
            try
            {
                uow.Repository<Dispute>().Delete((int)disputeId);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public void AddJobQuotationFavoriteTradesmen(List<FavoriteTradesman> favoriteTradesman)
        {
            try
            {
                if (favoriteTradesman.Count > 0)
                {
                    foreach (FavoriteTradesman fav in favoriteTradesman)
                    {
                        uow.Repository<FavoriteTradesman>().Add(fav);
                        uow.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public List<JobDetail> GetCompletedJobDetailsByCustomerAndStatusIds(long customerId, long statusId)
        {
            try
            {
                return uow.Repository<JobDetail>().GetAll().Where(cId => cId.CustomerId == customerId && cId.StatusId == statusId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobDetail>();
            }
        }

        public List<Dispute> GetDisputeRecord(long customerid)
        {
            try
            {
                return uow.Repository<Dispute>().Get(dcid => dcid.CustomerId == customerid).OrderByDescending(o => o.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Dispute>();
            }
        }

        public List<DisputeStatus> GetDisputeStatusByStatusIds(List<long> statusIds)
        {
            try
            {
                return uow.Repository<DisputeStatus>().GetAll().Where(disputeStatusTable => statusIds.Contains(disputeStatusTable.DisputeStatusId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<DisputeStatus>();
            }
        }

        public Response updateStatuse(long disputeId, int disputeStatusId)
        {
            Response response = new Response();
            try
            {
                Dispute dispute = uow.Repository<Dispute>().GetById(disputeId);

                dispute.DisputeStatusId = disputeStatusId;

                uow.Repository<Dispute>().Update(dispute);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Update Successfully";
            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public List<JobQuotation> GetPostedJobsByCustomerId(long customerId)
        {
            try
            {
                var jobQuotation = uow.Repository<JobQuotation>().GetAll().Where(x => customerId > 0 && x.CustomerId == customerId && x.StatusId == (int)BidStatus.Active).OrderByDescending(o => o.CreatedOn).ToList();
                return jobQuotation;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotation>();
            }
        }

        public async Task<JobQuotation> GetJobQuotationByJobQuotationId(long jobQuotationId)
        {
            try
            {
                var quoteDetails = await uow.Repository<JobQuotation>().GetByIdAsync(jobQuotationId);
                return quoteDetails;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotation();
            }
        }

        public JobAddress GetJobAddressByJobQuotationId(long JobQuotationId)
        {
            try
            {
                return uow.Repository<JobAddress>().GetAll().FirstOrDefault(x => x.JobQuotationId == JobQuotationId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobAddress();
            }
        }

        public List<FavoriteTradesman> GetFavoriteTradesmenByJobQuotationId(long jobQuotationId)
        {
            try
            {
                return uow.Repository<FavoriteTradesman>().GetAll().Where(x => x.JobQuotationId == jobQuotationId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<FavoriteTradesman>();
            }
        }

        public List<JobDetail> GetJobStatusByCustomerId(long customerId, int statusId)
        {
            try
            {
                return uow.Repository<JobDetail>().GetAll().Where(jobDetail => jobDetail.CustomerId == customerId && jobDetail.StatusId == statusId).OrderByDescending(o => o.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobDetail>();
            }
        }

        public List<JobAddress> GetJobAddressByJobQuotationIds(List<long> jobQuotationIds)
        {
            try
            {
                return uow.Repository<JobAddress>().GetAll().Where(x => jobQuotationIds.Contains(x.JobQuotationId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobAddress>();
            }
        }

        public List<TradesmanFeedback> GetTradesmanFeedbackByCustomerIds(List<long> customerIds)
        {
            try
            {
                return uow.Repository<TradesmanFeedback>().GetAll().Where(x => customerIds.Contains(x.CustomerId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanFeedback>();
            }
        }

        public Response PostTradesmanFeedback(TradesmanFeedback tradesmanFeedback)
        {
            Response response = new Response();
            try
            {
                if (tradesmanFeedback.TradesmanFeedbackId > 0)
                {
                    TradesmanFeedback existingData = uow.Repository<TradesmanFeedback>().GetById(tradesmanFeedback.TradesmanFeedbackId);

                    if (existingData != null)
                    {
                        uow.Repository<TradesmanFeedback>().Update(tradesmanFeedback);
                        uow.SaveAsync();
                        response.Message = "Successfully Updated TradesmanFeedback";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "A network error occure. Try again late";
                        response.Status = ResponseStatus.Error;
                    }
                }
                else
                {
                    TradesmanFeedback existing = uow.Repository<TradesmanFeedback>().GetAll().Where(a => a.JobDetailId == tradesmanFeedback.JobDetailId).FirstOrDefault();
                    if (tradesmanFeedback != null && existing == null)
                    {
                        uow.Repository<TradesmanFeedback>().Add(tradesmanFeedback);
                        uow.Save();
                        response.Message = "Successfully Added TradesmanFeedback";
                        response.Status = ResponseStatus.OK;
                    }
                    else if(tradesmanFeedback != null  && existing != null)
                    {
                        existing.QualityRating = tradesmanFeedback.QualityRating;
                        existing.OverallRating = tradesmanFeedback.OverallRating;
                        existing.CommunicationRating = tradesmanFeedback.CommunicationRating;
                        existing.Comments = tradesmanFeedback.Comments;
                        existing.ModifiedOn = DateTime.Now;
                        uow.Repository<TradesmanFeedback>().Update(existing);
                        uow.SaveAsync();
                        response.Message = "Successfully Updated TradesmanFeedback";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "A network error occure. Try again late";
                        response.Status = ResponseStatus.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public List<SupplierFeedback> GetSupplierFeedbackBySupplierId(long supplierId)
        {
            List<SupplierFeedback> supplierFeedbacks = new List<SupplierFeedback>();

            try
            {
                supplierFeedbacks = uow.Repository<SupplierFeedback>().GetAll().Where(x => x.SupplierId == supplierId).OrderByDescending(s => s.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return supplierFeedbacks;
        }

        public Response SetSupplierRating(SupplierFeedback supplierFeedback)
        {
            Response response = new Response();

            try
            {
                if (supplierFeedback != null)
                {
                    SupplierFeedback feedback = uow.Repository<SupplierFeedback>().Get().FirstOrDefault(x => x.CustomerId == supplierFeedback.CustomerId && x.SupplierId == supplierFeedback.SupplierId);

                    if (feedback == null)
                    {
                        uow.Repository<SupplierFeedback>().Add(supplierFeedback);
                    }
                    else
                    {
                        feedback.Comments = supplierFeedback.Comments;
                        feedback.OverallRating = supplierFeedback.OverallRating;
                        feedback.CommunicationRating = supplierFeedback.CommunicationRating;
                        feedback.QualityRating = supplierFeedback.QualityRating;
                        feedback.ModifiedOn = DateTime.Now;
                        feedback.ModifiedBy = supplierFeedback?.CreatedBy;

                        uow.Repository<SupplierFeedback>().Update(feedback);
                    }
                    uow.SaveAsync();

                    response.Message = "Successfully Updated";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "A network error occure. Try again late";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public List<long> GetJobDetails(long customerid)
        {

            // GetPaymentRecord getPaymentRecord = new GetPaymentRecord();
            //   List<GetPaymentRecord> getPaymentRecords = new List<GetPaymentRecord>();
            List<long> selectedColumn = new List<long>();

            try
            {
                List<JobDetail> jobDetails = uow.Repository<JobDetail>().Get().Where(cid => cid.CustomerId == customerid).ToList();

                selectedColumn = jobDetails.AsEnumerable().Select(s => s.JobDetailId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }

            //foreach (long s in selectedColumn)
            //{
            //    TradesmanJobReceipts PaymentData = uow.Repository<TradesmanJobReceipts>().Get(did => did.JobDetailId == s).FirstOrDefault();
            //    getPaymentRecord.Amount = PaymentData.Amount;
            //    getPaymentRecord.CreatedOn = PaymentData.CreatedOn;
            //    getPaymentRecord.DirectPayment = PaymentData.DirectPayment;
            //    getPaymentRecords.Add(getPaymentRecord);

            //}

            return selectedColumn;


        }

        public long JobQuotations(JobQuotation quotationVM)
        {
            try
            {
                if (quotationVM.JobQuotationId > 0)
                {
                    //quotationVM.WorkStartDate = DateTime.Now;
                    JobQuotation existingJob = uow.Repository<JobQuotation>().GetById(quotationVM.JobQuotationId);
                    existingJob.WorkBudget = quotationVM.WorkBudget <= 0 ? existingJob.WorkBudget : quotationVM.WorkBudget;
                    existingJob.VisitCharges = quotationVM.VisitCharges <= 0 ? existingJob.VisitCharges : quotationVM.VisitCharges;
                    existingJob.WorkStartDate = quotationVM.WorkStartDate == null ? existingJob.WorkStartDate : quotationVM.WorkStartDate;
                    existingJob.WorkStartTime = quotationVM.WorkStartTime == null ? existingJob.WorkStartTime : quotationVM.WorkStartTime;
                    existingJob.ModifiedOn = quotationVM.ModifiedOn == null ? existingJob.ModifiedOn : quotationVM.ModifiedOn;
                    existingJob.JobAddress = string.IsNullOrWhiteSpace(quotationVM.JobAddress) ? existingJob.JobAddress : quotationVM.JobAddress;
                    existingJob.DesiredBids = quotationVM.DesiredBids <= 0 ? existingJob.DesiredBids : quotationVM.DesiredBids;
                    existingJob.StatusId = quotationVM.StatusId <= 0 ? existingJob.StatusId : quotationVM.StatusId;
                    existingJob.SkillId = quotationVM.SkillId <= 0 ? existingJob.SkillId : quotationVM.SkillId;
                    existingJob.SubSkillId = quotationVM.SubSkillId <= 0 ? existingJob.SubSkillId : quotationVM.SubSkillId;
                    existingJob.WorkTitle = string.IsNullOrWhiteSpace(quotationVM.WorkTitle) ? existingJob.WorkTitle : quotationVM.WorkTitle;
                    existingJob.WorkDescription = string.IsNullOrWhiteSpace(quotationVM.WorkDescription) ? existingJob.WorkDescription : quotationVM.WorkDescription;
                    existingJob.ModifiedBy = string.IsNullOrWhiteSpace(quotationVM.ModifiedBy) ? existingJob.ModifiedBy : quotationVM.ModifiedBy;
                    existingJob.VisitCharges = quotationVM.VisitCharges <= 0 ? existingJob.VisitCharges : quotationVM.VisitCharges;
                    existingJob.ServiceCharges = quotationVM.ServiceCharges <= 0 ? existingJob.ServiceCharges : quotationVM.ServiceCharges;
                    //existingJob.WorkStartTime = quotationVM.WorkStartTime == null ? existingJob.WorkStartTime : quotationVM.WorkStartTime;
                    //JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    //string jsonValues = JsonConvert.SerializeObject(quotationVM, settings);
                    //JsonConvert.PopulateObject(jsonValues, existingJob);


                    uow.Repository<JobQuotation>().Update(existingJob);
                }
                else
                {
                    //quotationVM.CreatedOn = DateTime.Now;

                    uow.Repository<JobQuotation>().Add(quotationVM);
                }

                uow.Save();
                return quotationVM.JobQuotationId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public Response SaveJobAddress(JobAddress jobAddress)
        {
            Response response = new Response();
            try
            {
                if (jobAddress != null)
                {
                    JobAddress existingData = uow.Repository<JobAddress>().Get(x => x.JobQuotationId == jobAddress.JobQuotationId).FirstOrDefault();
                    if (existingData != null)
                    {
                        jobAddress.CreatedOn = existingData.CreatedOn;
                        jobAddress.JobAddressId = existingData.JobAddressId;

                        JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        string jsonValues = JsonConvert.SerializeObject(jobAddress, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<JobAddress>().Update(existingData);

                        response.Message = "Job Quotation Address Updated Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        uow.Repository<JobAddress>().Add(jobAddress);
                        response.Message = "Successfully Added Record";
                        response.Status = ResponseStatus.OK;
                    }
                    uow.Save();
                }
                else
                {
                    response.Message = "Job Quotation Address could not be Updated";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public async Task<Response> DeleteJobAddressByJobQuotationId(long jobQuotationId)
        {
            Response response = new Response();
            try
            {

                if (jobQuotationId > 0)
                {
                    JobAddress existingData = uow.Repository<JobAddress>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();
                    if (existingData != null)
                    {
                        await uow.Repository<JobAddress>().DeleteAsync(existingData.JobAddressId);
                        await uow.SaveAsync();

                        response.Message = "Job Quotation Address has been removed Successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Could not be removed.";
                        response.Status = ResponseStatus.Error;
                    }

                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public List<JobDetail> GetAlljobDetails(long customerId, long statusId)
        {
            List<JobDetail> jobDetails = new List<JobDetail>();
            try
            {
                jobDetails = uow.Repository<JobDetail>().Get(x => x.CustomerId == customerId && (x.StatusId == statusId || x.StatusId == (int)BidStatus.Active)).OrderByDescending(o => o.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return jobDetails;
        }

        public JobAddress GetJobAddress(long jobQuotationId)
        {
            try
            {
                return uow.Repository<JobAddress>().GetAll().Where(a => a.JobQuotationId == jobQuotationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobAddress();
            }
        }

        public bool MarkAsFinishedJob(JobDetail jobDetail)
        {
            try
            {
                uow.Repository<JobDetail>().Update(jobDetail);
                uow.Save();
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public long GetQuotationIdByBidId(long bidId)
        {
            try
            {
                return uow.Repository<Bids>().GetById(bidId).JobQuotationId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public long GetJobDetailIdByQuotationId(long quotationId)
        {
            try
            {
                var jobDetailsId = uow.Repository<JobDetail>().GetAll().Where(x => x.JobQuotationId == quotationId).FirstOrDefault().JobDetailId;
                return jobDetailsId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public List<JobLeadsVM> GetJobQuotationsBySkill(long tradesmanId, int pageNumber, int pageSize)
        {
            try
            {

                SqlParameter[] isTestUserParameters =
                {
                  new SqlParameter("@tradesmanId", tradesmanId)
                };

                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByTradesmanId", isTestUserParameters);

                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0]?.IsTestUser ?? false;
                else isTestUser = false;

                SqlParameter[] sqlParameters =
                {
                 new SqlParameter("@pageSize", pageSize),
                 new SqlParameter("@pageNumber",pageNumber),
                 new SqlParameter("@tradesmanId",tradesmanId),
                 new SqlParameter("@StatusId",(int)BidStatus.Active),
                 new SqlParameter("@IsTestUser", isTestUser)
                 };

                List<JobLeadsVM> result = uow.ExecuteReaderSingleDS<JobLeadsVM>("sp_JobQuotation", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobLeadsVM>();
            }
        }
        public List<JobLeadsWebVM> GetJobQuotationsWebBySkill(long tradesmanId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] isTestUserParameters =
                  {
                                new SqlParameter("@tradesmanId", tradesmanId)
                                 };

                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByTradesmanId", isTestUserParameters);

                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0].IsTestUser;
                else isTestUser = false;

                SqlParameter[] sqlParameters =
                {
                 new SqlParameter("@pageSize", pageSize),
                 new SqlParameter("@pageNumber",pageNumber),
                 new SqlParameter("@tradesmanId",tradesmanId),
                 new SqlParameter("@StatusId",(int)BidStatus.Active),
                 new SqlParameter("@IsTestUser", isTestUser)
                 };

                List<JobLeadsWebVM> result = uow.ExecuteReaderSingleDS<JobLeadsWebVM>("sp_JobQuotation_Web", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobLeadsWebVM>();
            }
        }

        public List<BidVM> GetActiveBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@bidsStatusId",bidsStatusId)
            };

                List<BidVM> bidVMs = uow.ExecuteReaderSingleDS<BidVM>("SP_ActiveBids", sqlParameters);
                return bidVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidVM>();
            }

        }

        public List<BidVM> GetDeclinedBidsDetails(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@bidsStatusId",bidsStatusId)
            };

                List<BidVM> bidVMs = uow.ExecuteReaderSingleDS<BidVM>("SP_DeclinedBids", sqlParameters);
                return bidVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidVM>();
            }
        }

        public List<BidWebVM> GetActiveBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@bidsStatusId",bidsStatusId)
            };


                List<BidWebVM> bidVMs = uow.ExecuteReaderSingleDS<BidWebVM>("SP_ActiveBidsWeb", sqlParameters);
                return bidVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidWebVM>();
            }

        }

        public List<BidWebVM> GetDeclinedBidsDetailsWeb(int pageNumber, int pageSize, long tradesmanId, int bidsStatusId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@bidsStatusId",bidsStatusId)
            };

                List<BidWebVM> bidVMs = uow.ExecuteReaderSingleDS<BidWebVM>("SP_DeclinedBidsWeb", sqlParameters);
                return bidVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidWebVM>();
            }
        }

        public Response GetJobDetail(int pageNumber, int pageSize, long tradesmanId, int jobStatusId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@jobStatusId",jobStatusId)
            };

                List<JobDetailVM> bidVMs = uow.ExecuteReaderSingleDS<JobDetailVM>("SP_myJobs", sqlParameters);
                if (bidVMs?.Count > 0)
                {
                    response.Message = "Successfully";
                    response.ResultData = bidVMs;
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Error";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public Response GetJobDetailWeb(int pageNumber, int pageSize, long tradesmanId, int jobStatusId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@jobStatusId",jobStatusId)
            };

                List<JobDetailWebVM> bidVMs = uow.ExecuteReaderSingleDS<JobDetailWebVM>("SP_myJobs_Web", sqlParameters);
                if (bidVMs?.Count > 0)
                {
                    response.Message = "Successfully";
                    response.ResultData = bidVMs;
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Error";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public List<FinishedJobVM> GetFinishedJob(long customerId, int statusId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@customerId",customerId),
                new SqlParameter("@statusId",statusId)
            };

                List<FinishedJobVM> finishedJobVMs = uow.ExecuteReaderSingleDS<FinishedJobVM>("SP_GetFinishedJobList", sqlParameters);
                return finishedJobVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<FinishedJobVM>();

            }
        }

        public List<InprogressVM> GetInprogressJob(long customerId, int statusId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@customerId",customerId),
                new SqlParameter("@statusId",statusId),
                new SqlParameter("@status",BidStatus.Accepted),
            };

                List<InprogressVM> inprogressVMs = uow.ExecuteReaderSingleDS<InprogressVM>("Sp_InprogressJob", sqlParameters);
                return inprogressVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InprogressVM>();
            }
        }

        public List<MyQuotationsVM> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, long customerId, int statusId, bool bidStatus)
        {
            try
            {
                SqlParameter[] sqlParameters =
                    {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageNumber",pageNumber),
                new SqlParameter("@CustomerId",customerId),
                new SqlParameter("@StatusId",statusId),
                new SqlParameter("@bidStatus",bidStatus)
            };
                List<MyQuotationsVM> myQuotationsVM = uow.ExecuteReaderSingleDS<MyQuotationsVM>("Sp_PostedJobList", sqlParameters);
                return myQuotationsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }

        public List<MyQuotationsVM> SpGetJobsByCustomerId(GetJobsParams getJobsParams)
        {
            try
            {
                List<MyQuotationsVM> myQuotationsVMs = new List<MyQuotationsVM>();
                SqlParameter[] sqlParameters =
                    {
                    new SqlParameter("@PageSize", getJobsParams.PageSize),
                    new SqlParameter("@dataOrderBy",getJobsParams.DataOrderBy),
                    new SqlParameter("@PageNumber",getJobsParams.PageNumber),
                    new SqlParameter("@customerId",getJobsParams.CustomerId)
                };

                myQuotationsVMs = uow.ExecuteReaderSingleDS<MyQuotationsVM>("Sp_GetjobsBycustomerId", sqlParameters);
                return myQuotationsVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }
        public List<MyQuotationsVM> GetPostedJobs(int pageNumber, int pageSize, long customerId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                    {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageNumber",pageNumber),
                new SqlParameter("@CustomerId",customerId)
            };

                return uow.ExecuteReaderSingleDS<MyQuotationsVM>("Sp_GetPostedJobs", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }

        public Response GetTradesmanByBidId(long bidId)
        {
            Response response = new Response();

            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@BidId",bidId)
                };

                var result = uow.ExecuteReaderSingleDS<TradesmanUserIdVM>("SpGetTradesmanUserIdByBidId", sqlParameters);
                if (result?.Count > 0)
                {
                    response.ResultData = result.Select(x => x.Id).FirstOrDefault();
                    response.Message = "Success";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Fail";
                    response.Status = ResponseStatus.Error;
                    response.ResultData = null;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public Response Sp_GetTradesmanFirebaseClientId(long jobQuotationId)
        {
            Response response = new Response();

            SqlParameter[] sqlParameters = { new SqlParameter("@JobQuotationId", jobQuotationId) };

            var result = uow.ExecuteReaderSingleDS<TradesmanFirebaseIdVM>("Sp_GetTradesmanFirebaseClientId", sqlParameters);

            if (result?.Count > 0)
            {
                response.ResultData = result;
                response.Message = "Success";
                response.Status = ResponseStatus.OK;
            }
            else
            {
                response.Message = "Fail";
                response.Status = ResponseStatus.Error;
                response.ResultData = null;
            }
            return response;
        }

        public List<long> GetBidCountByJobQuotationId(long jobQuotationId)
        {
            return uow.Repository<Bids>().Get().Where(x => x.JobQuotationId == jobQuotationId).Select(x => x.BidsId).ToList();
        }

        public bool CheckFeedBackStatus(long jobDetailId)
        {
            try
            {
                var tradesmanFeedbackId = uow.Repository<TradesmanFeedback>().Get().Where(x => x.JobDetailId == jobDetailId).Select(x => x.TradesmanFeedbackId).FirstOrDefault();

                if (tradesmanFeedbackId > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public List<WebLiveLeadsVM> WebLiveLeads(long jobQuotationId)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@jobQuotationId",jobQuotationId)
            };

            return uow.ExecuteReaderSingleDS<WebLiveLeadsVM>("WebLiveLeads", sqlParameters);
        }

        public List<WebLiveLeadsVM> WebLiveLeadsPanel(long TradesmanId, int statusId, int pageNumber, int pageSize)
        {
            List<WebLiveLeadsVM> webLiveLeadsVMs = new List<WebLiveLeadsVM>();
            try
            {

                SqlParameter[] isTestUserParameters =
                                 {
                                new SqlParameter("@tradesmanId", TradesmanId)
                                 };

                var result = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByTradesmanId", isTestUserParameters);

                bool isTestUser;

                if (result != null)
                    isTestUser = result[0].IsTestUser;
                else isTestUser = false;

                SqlParameter[] sqlParameters =
                {
                     new SqlParameter("@pageSize", pageSize),
                     new SqlParameter("@pageNumber",pageNumber),
                     new SqlParameter("@statusId",statusId),
                     new SqlParameter("@IsTestUser",isTestUser)
                 };

                webLiveLeadsVMs = uow.ExecuteReaderSingleDS<WebLiveLeadsVM>("Sp_WebLiveLeadsPanel", sqlParameters).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<WebLiveLeadsVM>();

            }

            return webLiveLeadsVMs;
        }


        public List<IdValueVM> WebLiveLeadsLatLong()
        {
            List<IdValueVM> idValues = new List<IdValueVM>();
            var respo = uow.Repository<JobAddress>().GetAll().OrderByDescending(x => x.CreatedOn).Where(x => x.CityId == 64).Take(20).ToList();
            foreach (var item in respo)
            {
                IdValueVM idValueVM = new IdValueVM
                {
                    Id = item.JobQuotationId,
                    Value = item.GpsCoordinates
                };
                idValues.Add(idValueVM);
            }
            return idValues;
        }

        public JobsVm GetAllJobsCount()
        {
            List<JobsVm> jobsVms;

            try
            {
                SqlParameter[] sqlParameters = { };
                jobsVms = uow.ExecuteReaderSingleDS<JobsVm>("JobStats", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                jobsVms = new List<JobsVm>();
            }

            return jobsVms[0];
        }

        public List<ActiveJobListVM> GetActiveJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city, string location)
        {
            List<ActiveJobListVM> ActivejobsVms;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageNumber", pageNumber),
                    new SqlParameter("@PageSize" , pageSize),
                    new SqlParameter("@dataOrderBy" , dataOrderBy),
                    new SqlParameter("@customerName" , customerName),
                    new SqlParameter("@jobId" , jobId),
                    new SqlParameter("@startDate" , startDate),
                    new SqlParameter("@endDate" , endDate),
                    new SqlParameter("@city" , city),
                    new SqlParameter("@location" , location),

                };
                ActivejobsVms = uow.ExecuteReaderSingleDS<ActiveJobListVM>("sp_activejobList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                ActivejobsVms = new List<ActiveJobListVM>();
            }

            return ActivejobsVms;
        }
        public List<ActiveJobListVM> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city,
            string status, string jobdetailid, DateTime? fromDate, DateTime? toDate, string tradesmanName, string usertype, string location, bool athorize, string cSJobStatusId, string townId,string town)
        {
            List<ActiveJobListVM> ActivejobsVms;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageNumber", pageNumber),
                    new SqlParameter("@PageSize" , pageSize),
                    new SqlParameter("@dataOrderBy" , dataOrderBy),
                    new SqlParameter("@customerName" , customerName),
                    new SqlParameter("@jobId" , jobId),
                    new SqlParameter("@startDate" , startDate),
                    new SqlParameter("@endDate" , endDate),
                    new SqlParameter("@city" , city),
                    new SqlParameter("@status" , status),
                    new SqlParameter("@jobdetailid" , jobdetailid),
                    new SqlParameter("@fromDate" , fromDate),
                    new SqlParameter("@toDate" , toDate),
                    new SqlParameter("@tradesmanName" , tradesmanName),
                    new SqlParameter("@usertype" , usertype),
                    new SqlParameter("@location" , location),
                    new SqlParameter("@athorize" , athorize),
                    new SqlParameter("@cSJobStatusId" , cSJobStatusId),
                    new SqlParameter("@townId" , townId),
                    new SqlParameter("@town",town)

                };
                ActivejobsVms = uow.ExecuteReaderSingleDS<ActiveJobListVM>("sp_pendingobList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                ActivejobsVms = new List<ActiveJobListVM>();
            }

            return ActivejobsVms;
        }

        public List<DeletedJobListVM> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy,
            string customerName, string jobId, DateTime? startDate, DateTime? endDate, string city,
            string status, string jobdetailid, DateTime? fromDate, DateTime? toDate, string tradesmanName, string usertype, string location, string cSJobStatusId, string town)
        {
            List<DeletedJobListVM> DeletedjobsVms;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageNumber", pageNumber),
                    new SqlParameter("@PageSize" , pageSize),
                    new SqlParameter("@dataOrderBy" , dataOrderBy),
                    new SqlParameter("@customerName" , customerName),
                    new SqlParameter("@jobId" , jobId),
                    new SqlParameter("@startDate" , startDate),
                    new SqlParameter("@endDate" , endDate),
                    new SqlParameter("@city" , city),
                    new SqlParameter("@status" , status),
                    new SqlParameter("@jobdetailid" , jobdetailid),
                    new SqlParameter("@fromDate" , fromDate),
                    new SqlParameter("@toDate" , toDate),
                    new SqlParameter("@tradesmanName" , tradesmanName),
                    new SqlParameter("@usertype" , usertype),
                    new SqlParameter("@location" , location),
                    new SqlParameter("@cSJobStatusId" , cSJobStatusId),
                    new SqlParameter("@town",town)

                };
                DeletedjobsVms = uow.ExecuteReaderSingleDS<DeletedJobListVM>("sp_DeletdJobsList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                DeletedjobsVms = new List<DeletedJobListVM>();
            }

            return DeletedjobsVms;
        }
        public List<InprogressJobList> GetInprogressJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location)
        {
            List<InprogressJobList> InprogressjobsVms;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageNumber", pageNumber),
                    new SqlParameter("@PageSize" , pageSize),
                    new SqlParameter("@dataOrderBy" , dataOrderBy),
                    new SqlParameter("@customerName" , customerName),
                    new SqlParameter("@jobId" , jobId),
                    new SqlParameter("@jobDetailId" , jobDetailId),
                    new SqlParameter("@startDate" , startDate),
                    new SqlParameter("@endDate" , endDate),
                    new SqlParameter("@city" , city),
                    new SqlParameter("@location" , location),


                };
                InprogressjobsVms = uow.ExecuteReaderSingleDS<InprogressJobList>("sp_InprogressJobList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                InprogressjobsVms = new List<InprogressJobList>();
            }

            return InprogressjobsVms;
        }

        public JobQuotationDTO GetJobDetailsList(long customerId)
        {
            List<JobQuotationDTO> jobDetails = new List<JobQuotationDTO>();
            List<BidsDTO> bidslist = new List<BidsDTO>();
            List<JobActivity> jobActivity = new List<JobActivity>();
            List<NotificationDTO> notificationDTO = new List<NotificationDTO>();
            List<CSJobRemarksVM> csjobRemarks = new List<CSJobRemarksVM>();
            JobQuotationDTO JobDetailsList;


            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@jobQuotation" , customerId)

                };

                jobDetails = uow.ExecuteReaderSingleDS<JobQuotationDTO>("sp_activejobListDetails", sqlParameters);

                SqlParameter[] sqlParameters1 = {
                new SqlParameter("@jobQuotation" , customerId)

                };
                bidslist = uow.ExecuteReaderSingleDS<BidsDTO>("Sp_GetBidsByQuotationId", sqlParameters1);
                if (jobDetails != null)
                    jobDetails[0].BidsList = bidslist;

                SqlParameter[] sqlParameters2 = {
                new SqlParameter("@jobQuotation" , customerId)

                };
                jobActivity = uow.ExecuteReaderSingleDS<JobActivity>("Sp_GetJobsActivityByQuotationId", sqlParameters2);
                if (jobDetails != null)
                    jobDetails[0].jobActivity = jobActivity;

                //SqlParameter[] sqlParameters3 = {
                //new SqlParameter("@jobQuotation" , customerId)

                //};
                //notificationDTO = uow.ExecuteReaderSingleDS<NotificationDTO>("Sp_GetNotificationByJobId", sqlParameters3);
                //if (jobDetails != null)
                //    jobDetails[0].notificationDTO = notificationDTO;

                SqlParameter[] sqlParameters4 = {
                new SqlParameter("@jobQuotation" , customerId)

                };
                csjobRemarks = uow.ExecuteReaderSingleDS<CSJobRemarksVM>("Sp_GetJobRemarksByJobId", sqlParameters4);
                if (jobDetails != null)
                    jobDetails[0].cSJobRemarksVM = csjobRemarks;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                JobDetailsList = new JobQuotationDTO();
            }
            if (jobDetails != null)
                return jobDetails[0];
            else
                return JobDetailsList = new JobQuotationDTO();
        }

        public List<CompletedJobListVm> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, DateTime? startDate, DateTime? endDate, string city, string location, DateTime? fromDate, DateTime? toDate, string dataOrderBy = "")
        {
            List<CompletedJobListVm> completedJobListVms;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageNumber", pageNumber),
                    new SqlParameter("@PageSize" , pageSize),
                    new SqlParameter("@customerName" , customerName),
                    new SqlParameter("@jobId" , jobId),
                    new SqlParameter("@jobDetailId" , jobDetailId),
                    new SqlParameter("@startDate" , startDate),
                    new SqlParameter("@endDate" , endDate),
                    new SqlParameter("@city" , city),
                    new SqlParameter("@location" , location),
                    new SqlParameter("@fromDate" , fromDate),
                    new SqlParameter("@toDate" , toDate),
                    new SqlParameter("@dataOrderBy" , dataOrderBy)

                };
                completedJobListVms = uow.ExecuteReaderSingleDS<CompletedJobListVm>("CompletedJobListAdmin", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                completedJobListVms = new List<CompletedJobListVm>();
            }

            return completedJobListVms;
        }

        public List<ReciveBidsList> GetReciveBids(long jobQoutationId)
        {
            List<ReciveBidsList> getReciveBids;

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@jobQoutationId",jobQoutationId)


                };
                getReciveBids = uow.ExecuteReaderSingleDS<ReciveBidsList>("sp_reciveBidsList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getReciveBids = new List<ReciveBidsList>();
            }

            return getReciveBids;
        }

        public ReciveBidDetails GetReciveBidDetails(long jobQoutationId)
        {
            ReciveBidDetails getReciveBidDetails = new ReciveBidDetails();

            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@jobQoutation",jobQoutationId)


                };
                getReciveBidDetails = uow.ExecuteReaderSingleDS<ReciveBidDetails>("sp_reciveBidDetails", sqlParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return getReciveBidDetails;
        }
        public List<Status> JobStatusForDropdown()
        {
            try
            {
                return uow.Repository<Status>().GetAll().ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }
        public List<Status> GetCsJobStatusDropdown()
        {
            try
            {
                SqlParameter[] sqlParameter =
                {

                };

                var csJobStatus = uow.ExecuteReaderSingleDS<Status>("SP_GetCsJobStatusDropdown", sqlParameter).ToList();
                return csJobStatus;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public List<JobReportWithStatusCustomerVM> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location)
        {
            try
            {
                SqlParameter[] sqlParameters =
                    {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@Customer",customer),
                new SqlParameter("@Status",status),
                new SqlParameter("@City",city),
                new SqlParameter("@LastActive",lastactive),
                new SqlParameter("@Location",location),

            };
                var result = uow.ExecuteReaderSingleDS<JobReportWithStatusCustomerVM>("Sp_CustomerRegistretion_rpt", sqlParameters);

                return result;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }


        }
        public List<JobQuotationDTO> GEtlatestJobsForOneDayReport(DateTime StartDate, DateTime EndDate)
        {
            List<JobQuotationDTO> getJobs;
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@FromDate" ,StartDate),
                    new SqlParameter("@ToDate" ,EndDate),
                };
                getJobs = uow.ExecuteReaderSingleDS<JobQuotationDTO>("Sp_PrimaryQuotation_Report", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getJobs = new List<JobQuotationDTO>();
            }

            return getJobs;
        }
        public List<BidsDTO> latestJobsForOneDayReport(DateTime StartDate, DateTime EndDate)
        {
            List<BidsDTO> getJobs;
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@FromDate" ,StartDate),
                    new SqlParameter("@ToDate" ,EndDate),
                };
                getJobs = uow.ExecuteReaderSingleDS<BidsDTO>("Sp_PrimaryBids_Report", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getJobs = new List<BidsDTO>();
            }

            return getJobs;
        }

        public List<JobQuotationDTO> GetPostedJobsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId, string jobDetId, string userType)
        {
            List<JobQuotationDTO> getJobs;
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@StartDate" ,StartDate),
                    new SqlParameter("@EndDate" ,EndDate),
                    new SqlParameter("@FromDate" ,FromDate),
                    new SqlParameter("@ToDate" ,ToDate),
                    new SqlParameter("@customer" ,customer),
                    new SqlParameter("@tradesman" ,tradesman),
                    new SqlParameter("@status" ,status),
                    new SqlParameter("@city" ,city),
                    new SqlParameter("@lastActive" ,lastActive),
                    new SqlParameter("@location" ,location),
                    new SqlParameter("@jobId" ,jobId),
                    new SqlParameter("@jobDetId" ,jobDetId),
                    new SqlParameter("@userType" ,userType),
                };
                getJobs = uow.ExecuteReaderSingleDS<JobQuotationDTO>("Sp_PostedJobsDynamic_Report", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getJobs = new List<JobQuotationDTO>();
            }

            return getJobs;
        }

        public List<BidsDTO> GetBidsForDynamicReport(System.DateTime? StartDate, System.DateTime? EndDate, System.DateTime? FromDate, System.DateTime? ToDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId)
        {
            List<BidsDTO> getJobs;
            try
            {
        SqlParameter[] sqlParameters = {
                    new SqlParameter("@StartDate" ,StartDate),
                    new SqlParameter("@EndDate" ,EndDate),
                    new SqlParameter("@FromDate" ,FromDate),
                    new SqlParameter("@ToDate" ,ToDate),
                    new SqlParameter("@customer" ,customer),
                    new SqlParameter("@status" ,status),
                    new SqlParameter("@city" ,city),
                    new SqlParameter("@lastActive" ,lastActive),
                    new SqlParameter("@location" ,location),
                    new SqlParameter("@jobId" ,jobId),
                    new SqlParameter("@customerId",customerId)
                };
                getJobs = uow.ExecuteReaderSingleDS<BidsDTO>("Sp_BidsDynamic_Report", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getJobs = new List<BidsDTO>();
            }

            return getJobs;
        }
        public List<JobQuotation> GetPostedJobsAddressList()
        {
            try
            {
                List<JobQuotation> customersAddressList = new List<JobQuotation>();
                customersAddressList = uow.Repository<JobQuotation>().GetAll().Select(x => new JobQuotation { JobAddress = x.JobAddress }).Where(x => x.JobAddress != null && x.JobAddress != "").ToList();
                return customersAddressList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotation>();
            }
        }


        public Response DeleteJobByQuotationId(long jobQuotationId, string actionPageName)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@jobQuotationId" ,jobQuotationId),
                    new SqlParameter("@actionPageName",actionPageName)
                };
                uow.ExecuteNonQuery<Response>("Sp_DeleteJobByQuotationId", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Job Deleted Succssfully !! ";

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public Response ApproveJob(long jobQuotationId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@jobQuotationId" ,jobQuotationId),
                };
                uow.ExecuteNonQuery<Response>("Sp_ApproveJob", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Approved successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public List<JobTown> GetTownsList()
        {
            List<JobTown> towns = new List<JobTown>();
            try
            {
                towns = uow.Repository<JobTown>().GetAll().ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
            return towns;

        }

        public Response PostJobContactInfo(PersonalDetailsVM personalDetailsVM)
        {
            Response response = new Response();
            try
            {
                JobContactInfo jobContactInfo = new JobContactInfo();
                jobContactInfo.Name = personalDetailsVM.FirstName;
                jobContactInfo.Email = personalDetailsVM.Email;
                jobContactInfo.City = personalDetailsVM.CityId;
                jobContactInfo.TownId = personalDetailsVM.TownId;
                jobContactInfo.Address = personalDetailsVM.Address;
                jobContactInfo.PhoneNumber = personalDetailsVM.MobileNumber;
                jobContactInfo.PropertyRelationship = personalDetailsVM.Relationship;
                jobContactInfo.CustomerId = personalDetailsVM.EntityId;
                jobContactInfo.JobQuotationId = personalDetailsVM.JobQuotationId;
                jobContactInfo.CreatedOn = DateTime.Now;

                uow.Repository<JobContactInfo>().AddAsync(jobContactInfo);
                uow.SaveAsync();

                response.Status = ResponseStatus.OK;
                response.Message = "Post Successfully";

            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        public List<GetJobsCountVM> GetJobsCount(long tradesmanId)
        {
            List<GetJobsCountVM> getJobs;
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@tradesmanId" ,tradesmanId)
                };
                getJobs = uow.ExecuteReaderSingleDS<GetJobsCountVM>("Sp_GetJobsCount", sqlParameters).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                getJobs = new List<GetJobsCountVM>();
            }

            return getJobs;
        }
        public Response JobAuthorizer(JobAuthorizerVM jobAuthorizerVM)
        {
            Response response = new Response();
            try
            {
                JobAuthorizer jobAuthorizer = new JobAuthorizer();
                if (jobAuthorizerVM.id <= 0 && jobAuthorizerVM.action == "added")
                {
                    jobAuthorizer.UserName = jobAuthorizerVM.userName;
                    jobAuthorizer.PhoneNumber = jobAuthorizerVM.phoneNumber;
                    jobAuthorizer.IsActive = jobAuthorizerVM.isActive;
                    jobAuthorizer.CreatedBy = jobAuthorizerVM.createdBy;
                    jobAuthorizer.CreatedOn = DateTime.Now;
                    uow.Repository<JobAuthorizer>().Add(jobAuthorizer);
                    uow.Save();
                    response.Message = "added";
                    response.Status = ResponseStatus.OK;
                }
                else if (jobAuthorizerVM.id > 0 && jobAuthorizerVM.action == "updated")
                {
                    var findById = uow.Repository<JobAuthorizer>().Get(x => x.UserId == jobAuthorizerVM.id).FirstOrDefault();
                    findById.UserName = jobAuthorizerVM.userName;
                    findById.PhoneNumber = jobAuthorizerVM.phoneNumber;
                    findById.IsActive = jobAuthorizerVM.isActive;
                    findById.ModifiedBy = jobAuthorizerVM.modifiedBy;
                    findById.ModifiedOn = DateTime.Now;
                    uow.Repository<JobAuthorizer>().Update(findById);
                    uow.Save();
                    response.Message = "updated";
                    response.Status = ResponseStatus.OK;
                }
                else if (jobAuthorizerVM.id > 0 && jobAuthorizerVM.action == "deleted")
                {
                    var findById = uow.Repository<JobAuthorizer>().Get(x => x.UserId == jobAuthorizerVM.id).FirstOrDefault();
                    findById.IsActive = findById.IsActive == true ? !findById.IsActive : findById.IsActive = true;
                    uow.Repository<JobAuthorizer>().Update(findById);
                    uow.Save();
                    response.Message = "deleted";
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }

            return response;
        }
        public Response ChangeJobStatus(int jqId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@jobQuotationId",jqId)
                };
                uow.ExecuteReaderSingleDS<Response>("Sp_ChangeJobStatus", sqlParameter);
                response.Message = "Status Changed Successfully!";
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }

            return response;
        }
        public List<JobAuthorizerVM> JobAuthorizerList()
        {
            List<JobAuthorizerVM> authorizers = new List<JobAuthorizerVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                };
                authorizers = uow.ExecuteReaderSingleDS<JobAuthorizerVM>("SP_JobAuthorizer", sqlParameters).ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
            return authorizers;

        }
        public List<JobAuthorizerVM> AdminJobAuthorizerList()
        {
            List<JobAuthorizerVM> authorizers = new List<JobAuthorizerVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                };
                authorizers = uow.ExecuteReaderSingleDS<JobAuthorizerVM>("SP_JobAuthorizerAdmin", sqlParameters).ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
            return authorizers;

        }

        public JobQuotation JobDetailsByJQID(long jobQuotationId)
        {
            try
            {
                JobQuotation quotationVM = new JobQuotation();
                return quotationVM = uow.Repository<JobQuotation>().GetAll().Where(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public long GetBidCountsOnJob(long jobQuotationId)
        {
            SqlParameter[] SqlParameters = {
                new SqlParameter("jobQuotationId",jobQuotationId)
            };
            var count = uow.ExecuteReaderSingleDS<BidCountOnJobVM>("GetBidCountOnJob", SqlParameters);

            return count[0].bidCount;
        }

        public async Task<Response> UpdateJobBudget(UpdateJobBudgetVM budgetVM)
        {
            try
            {
                JobQuotation existingJob = new JobQuotation();
                Response response = new Response();
                if (budgetVM.JobQuotationId > 0)
                {
                    existingJob = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == budgetVM.JobQuotationId).FirstOrDefault();
                }

                if (existingJob != null)
                {
                    if (budgetVM.ChangeStatus != 1)
                    {

                        CsjobRemarks csjobRemarks = new CsjobRemarks();
                        if (budgetVM.JobStatus == (int)CSJobStatus.Callnotpicked || budgetVM.JobStatus == (int)CSJobStatus.Outofstation || budgetVM.JobStatus == (int)CSJobStatus.Notinterested)
                        {
                            existingJob.StatusId = (int)BidStatus.Pending;
                        }
                        else
                        {
                            existingJob.StatusId = (int)BidStatus.Active;
                        }
                        existingJob.WorkBudget = budgetVM.WorkBudget;
                        existingJob.VisitCharges = budgetVM.VisitCharges;
                        existingJob.ServiceCharges = budgetVM.ServiceCharges;
                        existingJob.OtherCharges = budgetVM.OtherCharges;
                        existingJob.CsjobStatusId = budgetVM.JobStatus;
                        existingJob.Quantity = budgetVM.Quantity;
                        existingJob.EstimatedCommission = budgetVM.EstimatedCommission;
                        if (!string.IsNullOrWhiteSpace(budgetVM.RemarksDescription))
                        {
                            csjobRemarks.JobQuotationId = budgetVM.JobQuotationId;
                            csjobRemarks.RemarksDescription = budgetVM.RemarksDescription;
                            csjobRemarks.CreatedBy = budgetVM.CreatedBy;
                            csjobRemarks.CreatedOn = DateTime.Now;
                            await uow.Repository<CsjobRemarks>().AddAsync(csjobRemarks);
                        }
                    }
                    else
                    {
                        existingJob.StatusId = (int)BidStatus.Declined;
                        existingJob.AuthorizeJob = true;
                    }
                    uow.Repository<JobQuotation>().Update(existingJob);
                    await uow.SaveAsync();
                }
                response.Message = "Successfully Updated";
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> UpdateJobExtraCharges(UpdateJobBudgetVM budgetVM)
        {
            try
            {
                JobQuotation existingJobQuotation = new JobQuotation();
                JobDetail existingJob = new JobDetail();
                Response response = new Response();
                if (budgetVM.ChangeStatus != 1)
                {
                    if (budgetVM.JobQuotationId > 0)
                    {
                        existingJob = uow.Repository<JobDetail>().Get(x => x.JobQuotationId == budgetVM.JobQuotationId).FirstOrDefault();
                        if (existingJob == null)
                        {
                            existingJobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == budgetVM.JobQuotationId).FirstOrDefault();
                        }
                    }
                    if (existingJob != null || existingJobQuotation != null)
                    {
                        CsjobRemarks csjobRemarks = new CsjobRemarks();
                        if (!string.IsNullOrWhiteSpace(budgetVM.RemarksDescription) && budgetVM.CsJobStatus > 0)
                        {
                            csjobRemarks.JobQuotationId = budgetVM.JobQuotationId;
                            csjobRemarks.RemarksDescription = budgetVM.RemarksDescription;
                            csjobRemarks.CreatedBy = budgetVM.CreatedBy;
                            csjobRemarks.CreatedOn = DateTime.Now;
                            await uow.Repository<CsjobRemarks>().AddAsync(csjobRemarks);
                            if (existingJobQuotation.JobQuotationId <= 0)
                                existingJobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == budgetVM.JobQuotationId).FirstOrDefault();
                            existingJobQuotation.CsjobStatusId = budgetVM.CsJobStatus;
                            if (budgetVM.CsJobStatus == (int)CSJobStatus.Callnotpicked || budgetVM.CsJobStatus == (int)CSJobStatus.Outofstation || budgetVM.CsJobStatus == (int)CSJobStatus.Notinterested)
                            {
                               existingJobQuotation.StatusId = (int)BidStatus.Pending;
                            }
                            else
                            {
                               existingJobQuotation.StatusId = (int)BidStatus.Active;
                            }
                            uow.Repository<JobQuotation>().Update(existingJobQuotation);
                        }
                        else
                        {
                            if (existingJob != null)
                            {
                                existingJob.OtherCharges = budgetVM.OtherCharges;
                                existingJob.MaterialCharges = budgetVM.MaterialCharges;
                                existingJob.AdditionalCharges = budgetVM.AdditionalCharges;
                                existingJob.EstimatedCommission = budgetVM.EstimatedCommission;
                                existingJob.TotalJobValue = budgetVM.TotalJobValue;
                                existingJob.ChargesDescription = budgetVM.ChargesDescription;
                                uow.Repository<JobDetail>().Update(existingJob);
                            }
                        }
                        await uow.SaveAsync();
                        response.Message = "Successfully Updated";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Something went wrong!";
                        response.Status = ResponseStatus.Error;
                    }
                }
                else
                {
                    existingJobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == budgetVM.JobQuotationId).FirstOrDefault();
                    existingJobQuotation.StatusId = (int)BidStatus.Declined;
                    await uow.Repository<JobDetail>().DeleteAsync(budgetVM.JobQuotationId);
                    await uow.Repository<Bids>().DeleteAsync(budgetVM.JobQuotationId);
                    await uow.SaveAsync();
                    response.Message = "Job status changed successfully!";
                    response.Status = ResponseStatus.OK;
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AssignJobToTradesman(AssignJobVM assignJobVM)
        {
            try
            {
                Response response = new Response();
                if (assignJobVM.JobQuotationId > 0)
                {
                    JobQuotation getJobQuote = new JobQuotation();
                    getJobQuote = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == assignJobVM.JobQuotationId).FirstOrDefault();
                    getJobQuote.DesiredBids = 1;
                    getJobQuote.AuthorizeJob = true;
                    uow.Repository<JobQuotation>().Update(getJobQuote);
                    uow.Save();
                    Bids addNewBid = new Bids();
                    addNewBid.Amount = assignJobVM.Amount;
                    addNewBid.Comments = assignJobVM.Comments;
                    addNewBid.TradesmanId = assignJobVM.TradesmanId;
                    addNewBid.CustomerId = assignJobVM.CustomerId;
                    addNewBid.JobQuotationId = assignJobVM.JobQuotationId;
                    addNewBid.StatusId = 1;
                    addNewBid.IsSelected = false;
                    addNewBid.CreatedOn = DateTime.Now;
                    addNewBid.CreatedBy = assignJobVM.CreatedBy;
                    uow.Repository<Bids>().Add(addNewBid);
                    uow.Save();
                    response.Message = "Data saved successfully!";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Something went worng!";
                    response.Status = ResponseStatus.Error;

                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<InprogressJobList> GetUrgentJobsList()
        {
            List<InprogressJobList> urgentJobsList = new List<InprogressJobList>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };
                urgentJobsList = uow.ExecuteReaderSingleDS<InprogressJobList>("SP_GetUrgentJobList", sqlParameters).ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
            return urgentJobsList;

        }
        public List<InprogressJobList> GetJobsListByCategory(InprogressJobList inprogressJobList)
        {
            List<InprogressJobList> jobsListByCategory = new List<InprogressJobList>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@startDate",inprogressJobList.startDate),
                    new SqlParameter("@endDate",inprogressJobList.endDate),
                    new SqlParameter("@userType",inprogressJobList.userType)
                };
                jobsListByCategory = uow.ExecuteReaderSingleDS<InprogressJobList>("SP_GetJobListByCategory", sqlParameters);
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }
            return jobsListByCategory;

        }

        public Response GetBidCountByJobId(long tradesmanId, long jobQuotationId)
        {
            try
            {
                Response response = new Response();
                var tradesmanid = uow.Repository<Bids>().Get(x => x.JobQuotationId == jobQuotationId).Select(x => x.TradesmanId).FirstOrDefault();

                if (tradesmanid >= 0)
                {
                    response.ResultData = tradesmanid;
                    response.Status = ResponseStatus.OK;
                    response.Message = "Bid Count greater then Zero";
                }
                else
                {
                    response.ResultData = 0;
                    response.Status = ResponseStatus.Error;
                    response.Message = "Bid Count has Zero";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Bids> GetBidByJQID(long id)
        {

            Bids bid = uow.Repository<Bids>().GetAll().Where(a => a.JobQuotationId == id).FirstOrDefault();
            return bid;
        }
        public List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo, long jobQuotationId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@tradesmanName",tradesmanName),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@tradesmanPhoneNo",tradesmanPhoneNo),
                new SqlParameter("@jobQuotationId",jobQuotationId),
                };
                var respone = uow.ExecuteReaderSingleDS<PersonalDetailVM>("SP_GetTradesmanByName", sqlParameters).ToList();
                return respone;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PersonalDetailVM>();
            }
        }

        public bool GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId)
        {

            try
            {

                Bids bid = uow.Repository<Bids>().Get(a => a.JobQuotationId == jobQuotationId && a.TradesmanId == tradesmanId && a.StatusId == statusId).FirstOrDefault();
                if (bid != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public List<EsclateOption> getEscalateOptions(int userRole)
        {
            List<EsclateOption> esclateOptions = new List<EsclateOption>();
            esclateOptions = uow.Repository<EsclateOption>().GetAll().Where(a => a.UserRole == userRole).ToList();
            return esclateOptions;
        }

        public Response submitIssue(EsclateRequest esclateRequest)
        {
            Response response = new Response();
            try
            {
                uow.Repository<EsclateRequest>().AddAsync(esclateRequest);
                uow.SaveAsync();
                response.Status = ResponseStatus.OK;
                response.Message = "Request submitted successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }

        public List<EsclateRequestVM> EscalateIssuesRequestList(EsclateRequestVM esclateRequestVM)
        {
            List<EsclateRequestVM> esclateRequests = new List<EsclateRequestVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@customerId",esclateRequestVM.CustomerId),
                    new SqlParameter("@jobQuotationId",esclateRequestVM.JobQuotationId),
                    new SqlParameter("@tradesmanId",esclateRequestVM.TradesmanId)
                };
                esclateRequests = uow.ExecuteReaderSingleDS<EsclateRequestVM>("Sp_GetEscalteIssuesRequestList", sqlParameters).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return esclateRequests;
        }
        public Response AuthorizeEscalateIssueRequest(long escalateIssueId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@escalateIssueId" ,escalateIssueId),
                };
                uow.ExecuteNonQuery<Response>("Sp_AuthorizeEscalateRequest", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Approved successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public List<EsclateRequestVM> AuthorizeEscalateIssuesList(EsclateRequestVM esclateRequestVM)
        {
            List<EsclateRequestVM> esclateRequests = new List<EsclateRequestVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@customerId",esclateRequestVM.CustomerId),
                    new SqlParameter("@jobQuotationId",esclateRequestVM.JobQuotationId),
                    new SqlParameter("@tradesmanId",esclateRequestVM.TradesmanId)
                };
                esclateRequests = uow.ExecuteReaderSingleDS<EsclateRequestVM>("Sp_GetAuthorizeEscalateIssuesList", sqlParameters).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return esclateRequests;
        }

        public Response getEscalateIssueByJQID(long jobQuotationId, int userRole, int status)
        {
            Response response = new Response();
            try
            {
                EsclateRequest esclateRequest = new EsclateRequest();

                esclateRequest = uow.Repository<EsclateRequest>().GetAll().Where(a => a.JobQuotationId == jobQuotationId && a.UserRole == userRole && a.Status == status).FirstOrDefault();
                if (esclateRequest != null)
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Request Exist";
                }
                else
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "You can send request.";
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public async Task<Response> UpdateBidByStatusId(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@jobQuotationId",bidDetailsVM.JobQuotationId),
                    new SqlParameter("@bidId",bidDetailsVM.BidId)
                };
                var res = uow.ExecuteReaderSingleDS<Response>("SP_UpdateBidByStatusId", sqlParameters).FirstOrDefault();
                if(res.Message == "Success")
                {
                    response.Message = "Successfully Updated";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Something went Wrong!";
                }
                return response;
            }
            //try
            //{
            //    JobQuotation jobQuotation = new JobQuotation();
            //    Bids bids = new Bids();

            //    bids = uow.Repository<Bids>().Get(x => x.BidsId == bidDetailsVM.BidId).FirstOrDefault();
            //    if (bids != null)
            //    {
            //        bids.StatusId = (int)BidStatus.StandBy;
            //        uow.Repository<Bids>().Update(bids);
            //        await uow.SaveAsync();
            //        jobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == bidDetailsVM.JobQuotationId).FirstOrDefault();
            //        jobQuotation.StatusId = (int)BidStatus.StandBy;
            //        uow.Repository<JobQuotation>().Update(jobQuotation);
            //        await uow.SaveAsync();
            //        response.Status = ResponseStatus.OK;
            //        response.Message = "Success";
            //    }
            //    else
            //    {
            //        response.Status = ResponseStatus.Error;
            //        response.Message = "Something went wrong!";
            //    }

            //}
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public Response GetAcceptedBidsList(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            List<BidDetailsVM> myQuotationsVMs;
            try
            {
                if (bidDetailsVM.CustomerId > 0)
                {
                    SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@CustomerId" , bidDetailsVM.CustomerId),
                        new SqlParameter("@StatusId" , BidStatus.StandBy),
                        new SqlParameter("@PageNumber" , bidDetailsVM.PageNumber),
                        new SqlParameter("@PageSize" , bidDetailsVM.PageSize),
                    };
                    myQuotationsVMs = uow.ExecuteReaderSingleDS<BidDetailsVM>("SP_GetAcceptedBidsList", sqlParameters);
                }
                else
                {
                    SqlParameter[] sqlParameters1 =
                    {
                        new SqlParameter("@TradesmanId" , bidDetailsVM.TradesmanId),
                        new SqlParameter("@StatusId" , BidStatus.StandBy),
                    };
                    myQuotationsVMs = uow.ExecuteReaderSingleDS<BidDetailsVM>("Sp_TradesmanAcceptedBids", sqlParameters1);
                }
                response.ResultData = myQuotationsVMs;
                if (myQuotationsVMs?.Count > 0)
                    response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public Response GetInprogressJobsMobile(InProgressJobsVM inProgressJobsVM)
        {
            Response response = new Response();
            List<InProgressJobsVM> myQuotationsVMs;
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@CustomerId" , inProgressJobsVM.CustomerId),
                    new SqlParameter("@PageNumber" , inProgressJobsVM.PageNumber),
                    new SqlParameter("@PageSize" , inProgressJobsVM.PageSize),
                };
                myQuotationsVMs = uow.ExecuteReaderSingleDS<InProgressJobsVM>("SP_GetInprogressListMobile", sqlParameters);
               
                response.ResultData = myQuotationsVMs;
                if (myQuotationsVMs?.Count > 0)
                    response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public Response StartOrFinishJob(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            try
            {
                if (bidDetailsVM.Action == "start")
                {
                    int status = (int)BidStatus.StandBy;
                    Bids bids = uow.Repository<Bids>().Get(x => x.JobQuotationId == bidDetailsVM.JobQuotationId && x.StatusId == status && x.TradesmanId == bidDetailsVM.TradesmanId).FirstOrDefault();
                    if (bids != null)
                    {
                        bids.IsStarted = true;
                        uow.Repository<Bids>().Update(bids);
                        uow.SaveAsync();
                        response.Message = "Success";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }
                }
                else if (bidDetailsVM.Action == "finish")
                {
                    JobDetail jobDetail = uow.Repository<JobDetail>().Get(x => x.JobDetailId == bidDetailsVM.JobDetailsId).FirstOrDefault();
                    if (jobDetail != null)
                    {
                        jobDetail.IsFinished = true;
                        uow.Repository<JobDetail>().Update(jobDetail);
                        uow.SaveAsync();
                        response.Message = "Success";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public Response UpdateJobAdditionalCharges(BidDetailsVM bidDetailsVM)
        {
            Exc.AddErrorLog($"Start {JsonConvert.SerializeObject(bidDetailsVM)}");
            Response response = new Response();
            try
            {
                JobQuotation jobQuotation = uow.Repository<JobQuotation>().Get(x => x.JobQuotationId == bidDetailsVM.JobQuotationId).FirstOrDefault();
                Bids bids = uow.Repository<Bids>().Get(x => x.BidsId == bidDetailsVM.BidId).FirstOrDefault();
                JobDetail jobDetail = uow.Repository<JobDetail>().Get(x => x.JobQuotationId == bidDetailsVM.JobQuotationId).FirstOrDefault();
                if (bidDetailsVM.Action == "beforePayment")
                {

                    if (bids != null)
                    {
                        //bids.StatusId = (int)BidStatus.Completed;
                        bids.Amount = bidDetailsVM.TradesmanOffer + bidDetailsVM.OtherCharges ?? 0;
                        uow.Repository<Bids>().Update(bids);
                    }
                    if (jobDetail != null)
                    {
                        jobDetail.TradesmanBudget = bidDetailsVM.TradesmanOffer + bidDetailsVM.OtherCharges ?? 0;
                        jobDetail.AdditionalCharges = bidDetailsVM.OtherCharges ?? 0;
                        uow.Repository<JobDetail>().Update(jobDetail);
                    }
                    if (jobQuotation != null)
                    {
                        jobQuotation.OtherCharges = bidDetailsVM.OtherCharges;
                        uow.Repository<JobQuotation>().Update(jobQuotation);
                    }
                }
                else if(bidDetailsVM.Action == "afterPayment")
                {
                    if (jobDetail != null)
                    {
                        jobDetail.PaymentStatus = bidDetailsVM.PaymentMethod;
                        uow.Repository<JobDetail>().Update(jobDetail);
                    }
                    if (bids != null)
                    {
                        bids.StatusId = (int)BidStatus.Completed;
                        uow.Repository<Bids>().Update(bids);
                    }
                }
                else
                {
                    if (bids != null)
                    {
                        bids.StatusId = (int)BidStatus.Completed;
                        Exc.AddErrorLog($"2nd step  {JsonConvert.SerializeObject(bidDetailsVM.TradesmanOffer)} {JsonConvert.SerializeObject(bidDetailsVM.OtherCharges)}");
                        bids.Amount = bidDetailsVM.TradesmanOffer  + (bidDetailsVM.OtherCharges.HasValue ? bidDetailsVM.OtherCharges.Value : 0);
                        uow.Repository<Bids>().Update(bids);
                    }
                    if (jobDetail != null)
                    {
                        Exc.AddErrorLog($"3rd step  {JsonConvert.SerializeObject(bidDetailsVM.TradesmanOffer)} {JsonConvert.SerializeObject(bidDetailsVM.OtherCharges)}");
                        jobDetail.TradesmanBudget = bidDetailsVM.TradesmanOffer + (bidDetailsVM.OtherCharges !=null ? bidDetailsVM?.OtherCharges : 0);
                        jobDetail.PaymentStatus = bidDetailsVM.PaymentMethod;
                        jobDetail.AdditionalCharges = bidDetailsVM.OtherCharges ?? 0;
                        uow.Repository<JobDetail>().Update(jobDetail);
                    }
                    if (jobQuotation != null)
                    {
                        jobQuotation.OtherCharges = bidDetailsVM.OtherCharges;
                        uow.Repository<JobQuotation>().Update(jobQuotation);
                    }
                }
                uow.SaveAsync();
                response.Status = ResponseStatus.OK;
                response.Message = "success";

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response();
            }
            return response;
        }
        public List<EscalateOptionVM> GetEscalateOptionsList()
        {
            List<EscalateOptionVM> escalateOptions = new List<EscalateOptionVM>();
            try
            {
                SqlParameter[] sqlParameters = { 
               
                };
                escalateOptions = uow.ExecuteReaderSingleDS<EscalateOptionVM>("sp_GetEscalateOptionsList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<EscalateOptionVM>();
            }
            return escalateOptions;
        }

        public Response InsertAndUpdateEscalateOption( EscalateOptionVM escalateOptionVM)
        {
            Response response = new Response();
            EsclateOption esclateOption = new EsclateOption(); 
            try
            {
                if (escalateOptionVM.Id <= 0 &&  escalateOptionVM.Action == "add")
                {
                    esclateOption.Name = escalateOptionVM.Name;
                    esclateOption.Active = escalateOptionVM.Active;
                    esclateOption.CreatedOn = DateTime.Now;
                    esclateOption.CreatedBy = escalateOptionVM.UserId;
                    esclateOption.UserRole = escalateOptionVM.UserRole;
                    uow.Repository<EsclateOption>().Add(esclateOption);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Escalate Option Has Been Added Successfully !!";
                }
                else if (escalateOptionVM.Id > 0 && escalateOptionVM.Action == "update")
                {
                    esclateOption = uow.Repository<EsclateOption>().Get(o => o.Id == escalateOptionVM.Id).FirstOrDefault();
                    esclateOption.Name = escalateOptionVM.Name;
                    esclateOption.Active = escalateOptionVM.Active;
                    esclateOption.ModifiedOn = DateTime.Now;
                    esclateOption.ModifiedBy = escalateOptionVM.UserId;
                    esclateOption.UserRole = escalateOptionVM.UserRole;
                    uow.Repository<EsclateOption>().Update(esclateOption);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = " Escalate Option Has Been Updated Successfully !!";
                }
                else if (escalateOptionVM.Id > 0 && escalateOptionVM.Action == "delete")
                {
                    esclateOption = uow.Repository<EsclateOption>().Get(o => o.Id == escalateOptionVM.Id).FirstOrDefault();
                    esclateOption.Active = esclateOption.Active == true ? false : true;
                    esclateOption.ModifiedOn = DateTime.Now;
                    esclateOption.ModifiedBy = escalateOptionVM.UserId;
                    uow.Repository<EsclateOption>().Update(esclateOption);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Escalate Option Has Been Deleted Successfully";
                }
                else
                {

                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        public async Task<Response> JobPostByFacebookLeads(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<FacebookLeadsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@FullName",entity.FullName),
                    new SqlParameter("@PhoneNumber",entity.PhoneNumber),
                    new SqlParameter("@Budget",entity.Budget),
                    new SqlParameter("@SkillId",entity.SkillId),
                    new SqlParameter("@SubSkillId",entity.SubSkillId),
                    new SqlParameter("@StartedDate",entity.StartedDate),
                };
                var result = uow.ExecuteReaderSingleDS<FacebookLeadsDTO>("Sp_JobPostByFacebookLeads", sqlParameter);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Job Post by Facebook Leads.";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetUserFromFacebookLeads(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<FacebookLeadsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@PhoneNumber",entity.PhoneNumber),
                };
                var result = uow.ExecuteReaderSingleDS<FacebookLeadsDTO>("Sp_GetUserFromFacebookLeads", sqlParameter);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Job Post by Facebook Leads.";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

    }
    //
}
