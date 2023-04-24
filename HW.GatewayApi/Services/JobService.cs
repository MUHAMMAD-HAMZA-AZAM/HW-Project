using HW.AudioModels;
using HW.CallModels;
using HW.CommunicationViewModels;
using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.NotificationViewModels;
using HW.PackagesAndPaymentsModels;
using HW.PromotionsModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.Utility;
using HW.VideoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;
using BidStatus = HW.Utility.BidStatus;
using Dispute = HW.JobModels.Dispute;
using DisputeStatusDB = HW.JobModels.DisputeStatus;
using PromotionRedemptions = HW.PackagesAndPaymentsModels.PromotionRedemptions;
using Redemptions = HW.PackagesAndPaymentsModels.Redemptions;
using ReferalCode = HW.PackagesAndPaymentsModels.ReferalCode;

namespace HW.GatewayApi.Services
{
    public interface IJobService
    {
        Task<string> GetAllJobs();
        Task<string> GetJobById(long id);
        Task<TradesmanProfileImage> GetTradesmanImageById(long tradesmanId);
        Task<CustomerProfileImage> GetImageByCustomerId(long customerId);
        Task<string> GetAllBids();
        Task<List<QuotationBidsVM>> GetQuotationBids(long quotationId, int sortId, string userId);
        Task<bool> UpdateSelectedBid(long BidId, bool IsSelected);
        Task<Response> UpdateBidStatus(long BidId, int statusId);
        Task<Response> AddJobDetails(long bidId, int paymentMethod, int statusId);
        Task<BidDetailsVM> GetBidDetails(long bidId);
        Task<List<MyQuotationsVM>> GetPostedJobsByCustomerId(long customerId);
        Task<JobQuotationDetailVM> GetJobQuotationByJobQuotationId(long jobQuotationId);
        Task<List<FinishedJobVM>> GetFinishedJobList(long customerId, int statusId);
        Task<FinishedJobDetailsVM> GetFinishedJobDetails(long jobDetailId);
        Task<Response> PostTradesmanFeedback(TradesmanRatingsVM tradesmanRatingsVM);
        Task<bool> MarkAsFinishedJob(TradesmanViewModels.MarkAsFinishJobVM finishedJobVM);
        Task<Response> AddEscalateIssue(DisputeVM disputeVM);
        Task<List<DisputeVM>> GetCompletedJobDetailsByCustomerAndStatusIds(long customerId, long statusId);
        Task<List<DispVM>> GetDisputeRecord(long customerid);
        Task<Response> updateStatuse(long disputeId, int disputeStatusId);
        Task<Response> SetSupplierRating(RateSupplierVM rateSupplierVM);
        Task<List<InprogressVM>> GetAlljobDetails(long customerId, long statusId);
        Task<InprogressJobDetailVM> GetInprogressJobDetail(long jobQuotationId);
        Task<bool> UpdateJobDetailStatus(long jobDetailId, int statusId);
        Task<Response> UpdateJobCost(long jobDetailId, decimal jobCost);
        Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(long tradesmanId, int pageNumber, int pageSize);
        Task<List<JobLeadsWebVM>> GetJobLeadsWebByTradesmanId(long tradesmanId, int pageNumber, int pageSize);
        Task<JobQuotationDetailVM> GetJobQuotationDataByQuotationId(long jobQuotationId);
        Task<MediaVM> GetJobQuotationMediaByQuotationId(long jobQuotationId);
        Task<List<BidVM>> GetActiveBidsDetails(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId);
        Task<List<BidVM>> GetDeclinedBidsDetails(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId);
        Task<List<BidWebVM>> GetActiveBidsDetailsWeb(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId);
        Task<List<BidWebVM>> GetDeclinedBidsDetailsWeb(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId);
        Task<List<MyQuotationsVM>> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, long customerId, int statusId,bool bidStatus);
        Task<List<MyQuotationsVM>> SpGetJobsByCustomerId(long customerId);
        Task<List<MyQuotationsVM>> GetPostedJobs(int pageNumber, int pageSize, long customerId);
        Task<Response> GetJobDetail(long tradesmanId, int pageNumber, int pageSize, int jobStatusId);
        Task<string> GetJobDetailWeb(long tradesmanId, int pageNumber, int pageSize, int jobStatusId);

        Task<UserViewModels.VideoVM> GetQuoteVideoByJobQuotationId(long jobQuotationId);
        Task<UserViewModels.AudioVM> GetQuoteAudioByJobQuotationId(long jobQuotationId);
        Task<UserViewModels.ImageVM> GetQuoteImageById(long jobImageId);
        Task<List<InprogressVM>> GetInprogressJob(int pageNumber, int pageSize, long customerId, int statusId);
        Task<List<FinishedJobVM>> GetFinishedJob(long customerId, int statusId, int pageNumber, int pageSize);
        Task<List<UserViewModels.ImageVM>> GetQuoteImagesByJobQuotationIdWeb(long jobQuotationId);
        Task<List<WebLiveLeadsVM>> WebLiveLeads(long jobQuotationId);
        Task<List<WebLiveLeadsVM>> WebLiveLeadsPanel(long TradesmanId,int statusId, int pageNumber, int pageSize);
        Task<List<IdValueVM>> WebLiveLeadsLatLong();
        Task<List<CompletedJobListVm>> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, string startDate, string endDate, string city, string location, string fromDate, string toDate, string dataOrderBy = "");
        Task<List<JobDetail>> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status);
        Task<Response> PostJobContactInfo(PersonalDetailsVM personalDetailsVM);
        Task<List<GetJobsCountVM>> GetJobsCount(long tradesmanId);
        Task<bool> GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId);
        Task<string> GetBidCountsOnJob(long jobQuotationId);
        Task<List<JobImages>> GetJobImagesListByJobQuotationIds(List<long> jobQuotationIds);
        Task<List<EsclateOption>> getEscalateOptions(int userRole);
        Task<Response> submitIssue(EsclateRequest esclateRequest);
        Task<Response> getEscalateIssueByJQID(long jobQuotationId,int userRole,int status);
        Task<Response> UpdateBidByStatusId(BidDetailsVM bidDetailsVM);
        Task<string> GetAcceptedBidsList(BidDetailsVM bidDetailsVM);
        Task<string> GetInprogressJobsMobile(InProgressJobsVM inProgressJobsVM);
        Task<Response> JobStartNotification(long tradesmanId, long? customerId, long bidId, string jobTitle, long jobQuotationId);
        Task<Response> JobStartNotificationForTradesman(long tradesmanId, long? customerId, long bidId, string jobTitle, long jobQuotationId);
        Task<Response> StartOrFinishJob(BidDetailsVM bidDetailsVM);
        Task<Response> JobFifnishedNotification(long JobQuotationId);
        Task<Response> UpdateJobAdditionalCharges(BidDetailsVM bidDetailsVM);
        Task<Response> JobPostByFacebookLeads(string data);
        Task<Response> GetUserFromFacebookLeads(string data);
    }

    public class JobService : IJobService
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public JobService(IHttpClientService httpClient, ClientCredentials clientCred, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            _apiConfig = apiConfig;
            this.httpClient = httpClient;
            this.Exc = Exc;
        }

        public async Task<string> GetAllJobs()
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetAllJobs}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<string> GetJobById(long id)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetByCustomerId}?customerId={id}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }
        public async Task<CustomerProfileImage> GetImageByCustomerId(long customerId)
        {
          try
          {
            return JsonConvert.DeserializeObject<CustomerProfileImage>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customerId}", ""));
          }
          catch (Exception ex)
          {
            Exc.AddErrorLog(ex);
            return new CustomerProfileImage();
          }
        }
        public async  Task<TradesmanProfileImage> GetTradesmanImageById(long tradesmanId)
        {
          try
          {
            return JsonConvert.DeserializeObject<TradesmanProfileImage>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Image.GetTradesmanImageById}?tradesmanId={tradesmanId}", ""));
          }
          catch (Exception ex)
          {
            Exc.AddErrorLog(ex);
            return new TradesmanProfileImage();
          }
        }
        public async Task<string> GetAllBids()
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetAllBids}");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<List<QuotationBidsVM>> GetQuotationBids(long quotationId, int sortId, string userId)
        {
            List<QuotationBidsVM> quotationBids = new List<QuotationBidsVM>();
            List<QuotationBidsVM> trademanfb = new List<QuotationBidsVM>();
            int totalRating = 0;
            try
            {
                JobQuotation jobQuotation = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={quotationId}", ""));
                List<Bids> bids = JsonConvert.DeserializeObject<List<Bids>>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetQuotationBids}?quotationId={quotationId}", ""));
                List<Tradesman> tradesmanList = JsonConvert.DeserializeObject<List<Tradesman>>(await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", bids.Select(c => c.TradesmanId).ToList()));
                List<TradesmanProfileImage> tradesmanProfileImages = JsonConvert.DeserializeObject<List<TradesmanProfileImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImages}", bids.Select(c => c.TradesmanId).ToList()));
                List<JobDetail> tradesmanJobs = JsonConvert.DeserializeObject<List<JobDetail>>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsByTradesmanIds}", bids.Select(c => c.TradesmanId).ToList()));


                foreach (var item in bids)
                {
                    QuotationBidsVM ratevm = new QuotationBidsVM();

                    List<TradesmanFeedback> tradesmanFeedbacks = JsonConvert.DeserializeObject<List<TradesmanFeedback>>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanFeedBack}?tradesmanId={item.TradesmanId}", ""));
                    if (tradesmanFeedbacks?.Count() > 0)
                    {
                        totalRating += tradesmanFeedbacks?.Select(x => x?.OverallRating)?.Sum() ?? 0;
                        totalRating += tradesmanFeedbacks?.Select(x => x?.CommunicationRating)?.Sum() ?? 0;
                        totalRating += tradesmanFeedbacks?.Select(x => x?.QualityRating)?.Sum() ?? 0;

                        if (totalRating > 0)
                        {
                            totalRating = totalRating / (tradesmanFeedbacks.Count() * 3);
                        }
                        else
                        {
                            totalRating = 0;
                        }

                    }
                    ratevm.Rating = totalRating;
                    ratevm.TradesmanId = item.TradesmanId;
                    trademanfb.Add(ratevm);

                }

                List<string> userIds = new List<string>();
                userIds.AddRange(tradesmanList.Select(x => x.UserId));
                userIds.Add(userId);

                List<IdentityUserTypeVM> identityuserTypes = JsonConvert.DeserializeObject<List<IdentityUserTypeVM>>(
                                await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUsersTypeByUserIds}", userIds));

                List<string> tradesmanUserIds = identityuserTypes.Where
                    (x => x.UserId != userId && x.IsTestUser == identityuserTypes.FirstOrDefault(t => t.UserId == userId).IsTestUser).Select(s => s.UserId).ToList();

                List<Bids> filteredBids = bids.Where(x => tradesmanList.Where(s => tradesmanUserIds.Contains(s.UserId)).Select(a => a.TradesmanId).Contains(x.TradesmanId)).ToList();
                quotationBids = filteredBids?.Select(b => new QuotationBidsVM
                {
                    BidId = b.BidsId,
                    TradesmanImage = tradesmanProfileImages?.FirstOrDefault(x => x?.TradesmanId == b.TradesmanId)?.ProfileImage,
                    TradesmanId = b.TradesmanId,
                    MemberSince = tradesmanList.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).CreatedOn,
                    TradesmanAddress = tradesmanList?.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).Area + ", " + tradesmanList.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).City,
                    BidBy =  tradesmanList?.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).FirstName + " " + tradesmanList.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).LastName,
                    BidOn = b.CreatedOn,
                    BidAmount = Convert.ToInt64(b.Amount),
                    IsSelected = b.IsSelected,
                    Rating = trademanfb?.FirstOrDefault(x => x?.TradesmanId == b.TradesmanId)?.Rating,
                    TotalDoneJob = tradesmanJobs?.Where(x => x?.TradesmanId == b.TradesmanId)?.Count() ?? 0,
                    JobQuotationTitle = jobQuotation?.WorkTitle,
                    JobPostedOn = jobQuotation.CreatedOn,
                    VisitCharges = jobQuotation?.VisitCharges ?? 0,
                    ServiceCharges = jobQuotation?.ServiceCharges ?? 0,
                    OtherCharges = jobQuotation?.OtherCharges ?? 0,
                    IsOrganization = tradesmanList?.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).IsOrganization,
                    CompanyName = tradesmanList?.FirstOrDefault(x => x.TradesmanId == b.TradesmanId).CompanyName

                }).ToList();

                if (sortId > 0)
                {
                    switch (sortId)
                    {
                        case 1: //Latest
                            quotationBids = quotationBids?.OrderByDescending(o => o.BidOn).ToList();
                            break;
                        case 2: //Old
                            quotationBids = quotationBids?.OrderBy(o => o.BidOn).ToList();
                            break;
                        case 3: //Shortlisted
                            quotationBids = quotationBids?.OrderByDescending(o => o.IsSelected).ToList();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }


            return quotationBids;
        }

        public async Task<bool> UpdateSelectedBid(long BidId, bool IsSelected)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateSelectedBid}?BidId={BidId}&IsSelected={IsSelected}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }
        //Gateway JobService
        public async Task<Response> UpdateBidStatus(long BidId, int statusId)
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateBidStatus}?BidId={BidId}&statusId={statusId}", ""));

        }

        public async Task<bool> UpdateJobDetailStatus(long jobDetailId, int statusId)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobDetailStatus}?jobDetailId={jobDetailId}&statusId={statusId}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public async Task<Response> AddJobDetails(long bidId, int paymentMethod, int statusId)
        {
            Response response = new Response();

            try
            {
                Response bidStatusUpdated = JsonConvert.DeserializeObject<Response>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateBidStatus}?BidId={bidId}&statusId={statusId}", "")
                    );

                Response addJobDetailsResponse = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobDetails}?bidId={bidId}&paymentMethod={paymentMethod}", "")
                );

                if (addJobDetailsResponse?.Status == ResponseStatus.OK)
                {
                    JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={(long)addJobDetailsResponse.ResultData}", "")
                    );

                    if (bidStatusUpdated?.Status == ResponseStatus.OK)
                    {
                        List<long> tradesmanIds = new List<long>();

                        tradesmanIds = JsonConvert.DeserializeObject<List<long>>(bidStatusUpdated.ResultData?.ToString());

                        if (tradesmanIds?.Count > 0)
                        {
                            List<Tradesman> tradesmanList = JsonConvert.DeserializeObject<List<Tradesman>>
                                (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradmanDetails}", tradesmanIds));

                            List<string> tradesmanUserIds = tradesmanList.Select(x => x.UserId).ToList();

                            List<IdentityIdsValueVM> identityIdsValueVM = JsonConvert.DeserializeObject<List<IdentityIdsValueVM>>(
                                await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserListByUserIds}", tradesmanUserIds)
                            );
                            if (identityIdsValueVM != null)
                            {
                                foreach (IdentityIdsValueVM item in identityIdsValueVM)
                                {
                                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                                    {
                                        Body = $"Your Bid for Job {jobDetail?.Title} has been declined",
                                        Title = NotificationTitles.BidDeclined,
                                        To = item.FirebaseId,
                                        SenderEntityId = jobDetail?.JobQuotationId.ToString(),
                                        TargetActivity = "JobDetail_DeclinedBid",
                                        SenderUserId = jobDetail?.CreatedBy,
                                        TargetDatabase = TargetDatabase.Tradesman,
                                        TragetUserId = item.UserId,
                                        IsRead = false
                                    };

                                    JsonConvert.DeserializeObject<bool>
                                        (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                                    );
                                }

                            }
                        }
                        
                        // Send SMS to Authorizer when Bid has been accepted
                        
                        var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                        List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                        SmsVM numList = new SmsVM();

                        authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                        numList.Message = $"Bid has been accepted against '{jobDetail.Title}' with '{jobDetail.JobQuotationId}'";

                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);


                    }
                    else
                    {
                        JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateBidStatus}?BidId={bidId}&statusId={(int)BidStatus.Active}", ""));
                        response.Status = ResponseStatus.Error;
                        response.Message = "Payment services aren't working yet please try later";
                        return response;
                    }
                    response.Status = ResponseStatus.OK;
                    response.Message = "Bid accepted succesfully";
                    response.ResultData = jobDetail?.JobQuotationId;
                    return response;
                }
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }

            response.Status = ResponseStatus.Error;
            response.Message = "Payment services aren't working yet please try later";
            return response;
        }

        public async Task<BidDetailsVM> GetBidDetails(long bidId)
        {
            try
            {
                Bids bid = JsonConvert.DeserializeObject<Bids>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidById}?id={bidId}"));
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={bid.TradesmanId}"));
                TradesmanProfileImage tradesmanImage = JsonConvert.DeserializeObject<TradesmanProfileImage>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanImageById}?tradesmanId={bid.TradesmanId}", ""));
                BidAudio bidAudio = JsonConvert.DeserializeObject<BidAudio>(await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetAudioById}?bidId={bidId}", ""));
                JobQuotation jobQuotation = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={bid.JobQuotationId}", ""));
                JobDetail jobDeatils = JsonConvert.DeserializeObject<JobDetail>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={bid.JobQuotationId}", ""));
                Customer customer = JsonConvert.DeserializeObject<Customer>
                            (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={jobQuotation.CustomerId}", "")
                        );
                BidDetailsVM bidDetails = new BidDetailsVM()
                {
                    BidAudioMessage = bidAudio?.Audio,
                    BidAudioFileName = bidAudio?.FileName,
                    CustomerName = customer.FirstName + " " + customer.LastName,
                    TradesmanProfileImage = tradesmanImage?.ProfileImage,
                    JobTitle = jobQuotation?.WorkTitle,
                    CustomerBudget = Math.Truncate(jobQuotation.WorkBudget.HasValue ? jobQuotation.WorkBudget.Value : 0),
                    TradesmanOffer = Math.Truncate(bid?.Amount ?? 0),
                    BidId = bid?.BidsId ?? 0,
                    JobDescription = bid?.Comments,
                    TradesmanId = tradesman?.TradesmanId ?? 0,
                    TradesmanUserId = tradesman?.UserId,
                    TradesmanName = tradesman?.FirstName + " " + tradesman?.LastName,
                    MobileNumber = tradesman?.MobileNumber,
                    TradesmanAddress = $"{tradesman?.ShopAddress.Trim()} {tradesman?.Area.Trim()}, {tradesman?.City}",
                    BidStatusId = bid?.StatusId ?? 0,
                    Email = tradesman?.EmailAddress,
                    BidPostedOn = bid.CreatedOn,
                    JobQuotationId = jobQuotation.JobQuotationId,
                    JobDetailsId = jobDeatils?.JobDetailId ?? 0,
                    VisitCharges = jobQuotation?.VisitCharges ?? 0,
                    ServiceCharges = jobQuotation?.ServiceCharges ?? 0,
                    OtherCharges = jobQuotation?.OtherCharges ?? 0,
                    SkillId = jobQuotation.SkillId
                };

                return bidDetails;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new BidDetailsVM();
            }
        }

        public async Task<List<MyQuotationsVM>> GetPostedJobsByCustomerId(long customerId)
        {
            try
            {
                List<JobImages> jobImages = new List<JobImages>();
                List<JobQuotation> jobQuotations = JsonConvert.DeserializeObject<List<JobQuotation>>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetPostedJobsByCustomerId}?customerId={customerId}", "")
                    );

                //List<long> customerIds = jobQuotations.Select(customer => customer.CustomerId).Distinct().ToList();

                //List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(
                //    await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customerIds)
                //);

                List<long> jobQuotationIds = jobQuotations?.Select(id => id.JobQuotationId).Distinct().ToList();

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationIds}", jobQuotationIds));


                List<Bids> bidCounts = JsonConvert.DeserializeObject<List<Bids>>(
                    await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidCounts}", jobQuotationIds)
                );

                //List<CallCount> callCounts = JsonConvert.DeserializeObject<List<CallCount>>(
                //    await httpClient.PostAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetTradesmanCallLogByJobQuotationId}", jobQuotationIds)
                //);

                List<MyQuotationsVM> myQuotationsVMs = jobQuotations?.Select(x => new MyQuotationsVM
                {
                    JobQuotationId = x.JobQuotationId,
                    WorkTitle = x.WorkTitle,
                    PostedDate = x.CreatedOn,
                    BidCount = bidCounts.Where(b => b.JobQuotationId == x.JobQuotationId)?.ToList().Count(),
                    WorkStartDate = x.WorkStartDate,
                    JobImage = jobImages?.FirstOrDefault(c => c.JobQuotationId == x.JobQuotationId && c.IsMain == true)?.BidImage
                    // CallCount = callCounts.FirstOrDefault(c => c.JobQuotationId == x.JobQuotationId)?.Calls

                }).ToList();

                return myQuotationsVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }

        public async Task<JobQuotationDetailVM> GetJobQuotationByJobQuotationId(long jobQuotationId)
        {
            try
            {
                JobQuotationDetailVM jobQuotationDetailVM;

                JobQuotation jobQuotation = new JobQuotation();
                Skill jobSkill = new Skill();
                JobAddress jobAddress = new JobAddress();
                List<IdValueVM> allCities = new List<IdValueVM>();
                List<FavoriteTradesman> favoriteTradesmanList = new List<FavoriteTradesman>();
                List<Tradesman> tradesmenList = new List<Tradesman>();
                List<JobImages> jobImages = new List<JobImages>();
                JobQuotationVideo jobVideo = new JobQuotationVideo();

                List<SubSkill> subCatagoryList = new List<SubSkill>();

                
                 var aa= await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationByJobQuotationId}?jobQuotationId={jobQuotationId}", "");
                jobQuotation = JsonConvert.DeserializeObject<JobQuotation>(aa);
               jobSkill = JsonConvert.DeserializeObject<Skill>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillBySkillId}?skillId={jobQuotation.SkillId}", ""));

                subCatagoryList = JsonConvert.DeserializeObject<List<SubSkill>>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillBySkillId}?skillId={jobSkill.SkillId}", ""));

                jobAddress = JsonConvert.DeserializeObject<JobAddress>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationId}?JobQuotationId={jobQuotation.JobQuotationId}", ""));

                favoriteTradesmanList = JsonConvert.DeserializeObject<List<FavoriteTradesman>>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFavoriteTradesmenByJobQuotationId}?jobQuotationId={jobQuotation.JobQuotationId}", ""));

                List<long> tradesmanIdsList = favoriteTradesmanList.Select(x => x.CustomerFavoritesId).ToList();

                tradesmenList = JsonConvert.DeserializeObject<List<Tradesman>>
                    (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", tradesmanIdsList));

                allCities = JsonConvert.DeserializeObject<List<IdValueVM>>
                    (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                //jobVideo = JsonConvert.DeserializeObject<JobQuotationVideo>
                //    (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                List<IdValueVM> subCatagories = subCatagoryList.Select(s => new IdValueVM { Id = s.SubSkillId, Value = s.Name }).ToList();
                // IdValueVM subCatagories = subCatagoryList.Where(x => x.SkillId== jobQuotation.SkillId).Select(s => new IdValueVM { Id = s.SubSkillId, Value = s.Name }).FirstOrDefault();

                List<IdValueVM> tradesman = tradesmenList.Select(s => new IdValueVM { Id = s.TradesmanId, Value = $"{s.FirstName} {s.LastName}" }).ToList();

                List<UserViewModels.ImageVM> images = jobImages.Select(i => new UserViewModels.ImageVM { Id = i.BidImageId, ImageContent = i.BidImage, IsMain = i.IsMain, FilePath = i.FileName }).ToList();

                jobQuotationDetailVM = new JobQuotationDetailVM()
                {
                    Address = jobAddress?.StreetAddress,
                    Budget = jobQuotation.WorkBudget.HasValue ? jobQuotation.WorkBudget.Value : 0,
                    Catagory = jobSkill?.Name,
                    CategoryId = jobSkill?.SkillId ?? 0,
                    CityId = jobAddress?.CityId ?? 0,
                    JobDescription = jobQuotation?.WorkDescription,
                    JobQuotationId = jobQuotation.JobQuotationId,
                    QuotesQuantity = jobQuotation?.DesiredBids ?? 0,
                    Title = jobQuotation?.WorkTitle,
                    StartingDateTime = jobQuotation.WorkStartDate.Value,
                    JobStartingTime = jobQuotation.WorkStartTime.ToString(),
                    Area = jobAddress?.Area,
                    SubCatagory = subCatagories,
                    SubSkillId = jobQuotation.SubSkillId.HasValue ? jobQuotation.SubSkillId.Value : 0,
                    SelectiveTradesman = jobQuotation.SelectiveTradesman.Value,
                    TradesmanList = tradesman,
                    CitiesList = allCities,
                    Images = images,
                    video = jobVideo?.Video,
                    VideoFileName = jobVideo?.FileName
                };

                return jobQuotationDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotationDetailVM();
            }
        }

        public async Task<JobQuotationDetailVM> GetJobQuotationDataByQuotationId(long jobQuotationId)
        {
            try
            {
                JobQuotationDetailVM jobQuotationDetailVM;

                JobQuotation jobQuotation = new JobQuotation();
                Skill jobSkill = new Skill();
                JobAddress jobAddress = new JobAddress();
                List<IdValueVM> allCities = new List<IdValueVM>();
                List<FavoriteTradesman> favoriteTradesmanList = new List<FavoriteTradesman>();
                List<Tradesman> tradesmenList = new List<Tradesman>();
                List<JobImages> jobImages = new List<JobImages>();
                JobQuotationVideo jobVideo = new JobQuotationVideo();

                List<SubSkill> subCatagoryList = new List<SubSkill>();

                jobQuotation = JsonConvert.DeserializeObject<JobQuotation>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                JobAddress jobAddres = JsonConvert.DeserializeObject<JobAddress>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={jobQuotation.JobQuotationId}", ""));

                jobSkill = JsonConvert.DeserializeObject<Skill>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillBySkillId}?skillId={jobQuotation.SkillId}", ""));

                subCatagoryList = JsonConvert.DeserializeObject<List<SubSkill>>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillBySkillId}?skillId={jobSkill.SkillId}", ""));

                jobAddress = JsonConvert.DeserializeObject<JobAddress>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationId}?JobQuotationId={jobQuotation.JobQuotationId}", ""));

                favoriteTradesmanList = JsonConvert.DeserializeObject<List<FavoriteTradesman>>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFavoriteTradesmenByJobQuotationId}?jobQuotationId={jobQuotation.JobQuotationId}", ""));

                List<long> tradesmanIdsList = favoriteTradesmanList.Select(x => x.CustomerFavoritesId).ToList();

                tradesmenList = JsonConvert.DeserializeObject<List<Tradesman>>
                    (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", tradesmanIdsList));

                allCities = JsonConvert.DeserializeObject<List<IdValueVM>>
                    (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                jobVideo = JsonConvert.DeserializeObject<JobQuotationVideo>
                    (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                List<IdValueVM> subCatagories = subCatagoryList.Select(s => new IdValueVM { Id = s.SubSkillId, Value = s.Name }).ToList();
                // IdValueVM subCatagories = subCatagoryList.Where(x => x.SkillId == jobQuotation.SkillId).Select(s => new IdValueVM { Id = s.SubSkillId, Value = s.Name }).FirstOrDefault();

                List<IdValueVM> tradesman = tradesmenList.Select(s => new IdValueVM { Id = s.TradesmanId, Value = $"{s.FirstName} {s.LastName}" }).ToList();

                List<UserViewModels.ImageVM> images = jobImages.Select(i => new UserViewModels.ImageVM { Id = i.BidImageId, IsMain = i.IsMain, FilePath = i.FileName }).ToList();

                jobQuotationDetailVM = new JobQuotationDetailVM()
                {
                    Address = jobAddress?.StreetAddress,
                    Budget = jobQuotation.WorkBudget.HasValue ? jobQuotation.WorkBudget.Value : 0,
                    Catagory = jobSkill?.Name,
                    CityId = jobAddress?.CityId ?? 0,
                    CategoryId = jobSkill?.SkillId ?? 0,
                    JobDescription = jobQuotation?.WorkDescription,
                    JobQuotationId = jobQuotation.JobQuotationId,
                    QuotesQuantity = jobQuotation?.DesiredBids ?? 0,
                    Title = jobQuotation?.WorkTitle,
                    StartingDateTime = jobQuotation.WorkStartDate.Value,
                    Area = jobAddress?.Area,
                    SubCatagory = subCatagories,
                    SubSkillId = jobQuotation.SubSkillId.HasValue ? jobQuotation.SubSkillId.Value : 0,
                    SelectiveTradesman = jobQuotation.SelectiveTradesman.Value,
                    TradesmanList = tradesman,
                    CitiesList = allCities,
                    Images = images,
                    //video = jobVideo?.Video,
                    VideoFileName = jobVideo?.FileName,
                    WorkStartTime = jobQuotation.WorkStartTime,
                    StatusId = jobQuotation.StatusId ?? 0

                };

                return jobQuotationDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotationDetailVM();
            }
        }

        public async Task<MediaVM> GetJobQuotationMediaByQuotationId(long jobQuotationId)
        {
            try
            {
                List<JobImages> jobImages = new List<JobImages>();
                JobQuotationVideo jobVideo = new JobQuotationVideo();



                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                jobVideo = JsonConvert.DeserializeObject<JobQuotationVideo>
                    (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                MediaVM mediaVM = new MediaVM()
                {
                    Images = jobImages?.Select(i => new UserViewModels.ImageVM { Id = i?.BidImageId ?? 0, ImageContent = i?.BidImage, IsMain = i?.IsMain ?? false, FilePath = i?.FileName }).ToList(),
                    Video = new UserViewModels.VideoVM() { FilePath = jobVideo?.FileName, VideoContent = jobVideo?.Video },

                };

                return mediaVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new MediaVM();
            }
        }

        public async Task<UserViewModels.VideoVM> GetQuoteVideoByJobQuotationId(long jobQuotationId)
        {
            try
            {
                JobQuotationVideo jobVideo = new JobQuotationVideo();

                jobVideo = JsonConvert.DeserializeObject<JobQuotationVideo>
                    (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                UserViewModels.VideoVM videoVM = new UserViewModels.VideoVM()
                {
                    FilePath = jobVideo?.FileName,
                    VideoContent = jobVideo?.Video
                };

                return videoVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.VideoVM();
            }
        }

        public async Task<UserViewModels.AudioVM> GetQuoteAudioByJobQuotationId(long jobQuotationId)
        {
            try
            {
                BidAudio audio = new BidAudio();

                audio = JsonConvert.DeserializeObject<BidAudio>
                    (await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                UserViewModels.AudioVM audioVM = new UserViewModels.AudioVM()
                {
                    FileName = audio?.FileName,
                    AudioContent = audio?.Audio
                };

                return audioVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.AudioVM();
            }
        }


        public async Task<UserViewModels.ImageVM> GetQuoteImageById(long jobImageId)
        {
            try
            {
                JobImages jobImage = new JobImages();

                jobImage = JsonConvert.DeserializeObject<JobImages>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImageById}?jobImageId={jobImageId}", ""));

                UserViewModels.ImageVM imageVM = new UserViewModels.ImageVM() { Id = jobImage?.BidImageId ?? 0, ImageContent = jobImage?.BidImage, FilePath = jobImage?.FileName };

                return imageVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.ImageVM();
            }
        }

        public async Task<List<UserViewModels.ImageVM>> GetQuoteImagesByJobQuotationIdWeb(long jobQuotationId)
        {
            try
            {
                List<UserViewModels.ImageVM> jobImages = new List<UserViewModels.ImageVM>();

                List<JobImages> images = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                jobImages = images.Select(i => new UserViewModels.ImageVM() { ImageContent = i.BidImage }).ToList();

                return jobImages;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<UserViewModels.ImageVM>();
            }
        }


        public async Task<List<FinishedJobVM>> GetFinishedJobList(long customerId, int statusId)
        {
            List<FinishedJobVM> finishedJobVMs = new List<FinishedJobVM>();
            try
            {
                List<JobDetail> jobDetail = new List<JobDetail>();
                List<Tradesman> tradesmen = new List<Tradesman>();
                List<JobAddress> jobAddresses = new List<JobAddress>();
                List<City> cities = new List<City>();
                List<TradesmanFeedback> tradesmanFeedbacks = new List<TradesmanFeedback>();
                List<JobImages> jobImages = new List<JobImages>();

                jobDetail = JsonConvert.DeserializeObject<List<JobDetail>>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobStatusByCustomerId}?customerId={customerId}&statusId={statusId}", ""));

                List<long> tradesmanIds = jobDetail.Select(c => c.TradesmanId).ToList();

                tradesmen = JsonConvert.DeserializeObject<List<Tradesman>>
                    (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", tradesmanIds));

                List<long> jobQuotationIds = jobDetail.Select(x => x.JobQuotationId).ToList();

                jobAddresses = JsonConvert.DeserializeObject<List<JobAddress>>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationIds}", jobQuotationIds));

                cities = JsonConvert.DeserializeObject<List<City>>
                   (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                List<long> customerIds = jobDetail.Select(x => x.CustomerId).ToList();

                tradesmanFeedbacks = JsonConvert.DeserializeObject<List<TradesmanFeedback>>
                   (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanFeedbackByCustomerIds}", customerIds));

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationIds}", jobQuotationIds));

                finishedJobVMs = jobDetail.Select(s => new FinishedJobVM
                {

                    JobDetailId = s?.JobDetailId ?? 0,
                    JobEndTime = s?.EndDate ?? DateTime.Now,
                    JobTitle = s?.Title,
                    TradesmanId = tradesmen?.FirstOrDefault(x => x.TradesmanId == s.TradesmanId)?.TradesmanId ?? 0,
                    TradesmanName = $"{tradesmen?.FirstOrDefault(x => x.TradesmanId == s.TradesmanId)?.FirstName} {tradesmen?.FirstOrDefault(x => x.TradesmanId == s.TradesmanId)?.LastName}",
                    StreetAddress = jobAddresses?.FirstOrDefault(c => c.JobQuotationId == s.JobQuotationId)?.StreetAddress,
                    Town = jobAddresses?.FirstOrDefault(c => c.JobQuotationId == s.JobQuotationId)?.Area,
                    City = cities?.FirstOrDefault(c => c.CityId == jobAddresses?.FirstOrDefault(v => v.JobQuotationId == s.JobQuotationId)?.CityId)?.Name,
                    Rating = tradesmanFeedbacks?.FirstOrDefault(t => t.JobDetailId == s.JobDetailId)?.OverallRating != null ? tradesmanFeedbacks?.FirstOrDefault(t => t.JobDetailId == s.JobDetailId)?.OverallRating : 0,
                    JobImage = jobImages?.FirstOrDefault(c => c.JobQuotationId == s.JobQuotationId && c.IsMain == true)?.BidImage,
                    JobQuotationId = s?.JobQuotationId ?? 0

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return finishedJobVMs;
        }


        public async Task<FinishedJobDetailsVM> GetFinishedJobDetails(long jobDetailId)
        {
            try
            {
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>
                        (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={jobDetailId}", ""));

                Tradesman tradesmanDetail = JsonConvert.DeserializeObject<Tradesman>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByTradesmanId}?tradesmanId={jobDetail.TradesmanId}", ""));

                TradesmanProfileImage tradesmanProfileImage = JsonConvert.DeserializeObject<TradesmanProfileImage>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageByTradesmanId}?tradesmanId={tradesmanDetail.TradesmanId}", ""));

                TradesmanJobReceipts tradesmanJobReceipts = JsonConvert.DeserializeObject<TradesmanJobReceipts>
                    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetTradesmanJobReceiptsByTradesmanId}?tradesmanId={tradesmanDetail.TradesmanId}&jobDetailId={jobDetail.JobDetailId}", ""));

                TradesmanFeedback tradesmanFeedback = JsonConvert.DeserializeObject<TradesmanFeedback>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFeedBack}?JobDetailId={jobDetailId}&tradesmanId={tradesmanDetail.TradesmanId}", ""));

                JobAddress jobAddressDetails = JsonConvert.DeserializeObject<JobAddress>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationId}?JobQuotationId={jobDetail.JobQuotationId}", ""));

                List<City> cities = JsonConvert.DeserializeObject<List<City>>
                   (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityList}", ""));

                bool tradesmanIsFavorite = JsonConvert.DeserializeObject<bool>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetTradesmanIsFavorite}?customerId={jobDetail.CustomerId}&tradesmanId={tradesmanDetail.TradesmanId}", ""));

                var FavoriteTradesmanJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.CheckSavedTradesmanById}?tradesmanId={tradesmanDetail.TradesmanId}&customerId={jobDetail.CustomerId}", "");
                bool savedtradesman = JsonConvert.DeserializeObject<bool>(FavoriteTradesmanJson);

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={jobDetail.CustomerId}", "")
                );

                FinishedJobDetailsVM finishedJobDetailsVM = new FinishedJobDetailsVM()
                {
                    CustomerId = jobDetail?.CustomerId ?? 0,
                    JobDetailId = jobDetail?.JobDetailId ?? 0,
                    JobTitle = jobDetail?.Title,
                    JobQuotationId = jobDetail?.JobQuotationId ?? 0,
                    JobStartingDateTime = jobDetail.StartDate,
                    JobEndingDateTime = jobDetail.EndDate,
                    TradesmanName = $"{tradesmanDetail?.FirstName} {tradesmanDetail?.LastName}",
                    TradesmanId = tradesmanDetail?.TradesmanId ?? 0,
                    TradesmanProfileImage = tradesmanProfileImage?.ProfileImage,
                    DirectPayment = tradesmanJobReceipts?.DirectPayment ?? false,
                    FeedbackComments = tradesmanFeedback?.Comments,
                    OverallRating = tradesmanFeedback?.OverallRating ?? 0,
                    LatLng = jobAddressDetails?.GpsCoordinates,
                    JobAddress = $"{jobAddressDetails?.StreetAddress}, {jobAddressDetails?.Area} {cities.FirstOrDefault(x => x.CityId == jobAddressDetails.CityId)?.Name}",
                    IsFavorite = tradesmanIsFavorite,
                    Payment = tradesmanJobReceipts?.Amount ?? 0,
                    IsFavoriteTradesman = savedtradesman,
                    CustomerName = $"{customer?.FirstName} {customer?.LastName}",


                };

                return finishedJobDetailsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new FinishedJobDetailsVM();
            }
        }

        public async Task<Response> AddEscalateIssue(DisputeVM disputeVM)
        {
            Response response = new Response();
            try
            {
                Dispute UserEntity = new Dispute()
                {
                    Subject = disputeVM.Subject,
                    Message = disputeVM.Message,
                    CreatedOn = disputeVM.CreatedOn,
                    JobDetailId = disputeVM.JobDetailId,
                    CreatedBy = disputeVM.CreatedBy,
                    JobStatusId = disputeVM.JobStatusId,
                    CustomerId = disputeVM.CustomerId,
                    DisputeStatusId = disputeVM.DisputeStatusId
                };

                Response disputeResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddEscalateIssue}", UserEntity));

                if (disputeResponse.Status == ResponseStatus.OK)
                {

                    if (disputeVM?.ImageVMs?.Count > 0)
                    {
                        List<DisputeImages> disputeImagesList = new List<DisputeImages>();

                        foreach (UserViewModels.ImageVM item in disputeVM?.ImageVMs)
                        {
                            DisputeImages disputeImages = new DisputeImages
                            {
                                BidImage = item.ImageContent,
                                DisputeId = Convert.ToInt32(disputeResponse?.ResultData),
                                FileName = item.FilePath,
                                IsMain = item.IsMain,
                                CreatedOn = DateTime.Now,
                                CreatedBy = disputeVM.CreatedBy
                            };
                            disputeImagesList.Add(disputeImages);
                        }

                        bool imagesAdded = await AddImages(disputeImagesList);

                        if (!imagesAdded)
                        {
                            await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.DeleteEscalateIssue}?disputeId={ Convert.ToInt32(disputeResponse.ResultData)}", "");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(disputeVM.AudioVM?.FileName) && disputeVM.AudioVM?.AudioContent.Length > 0)
                    {
                        DisputeAudio disputeAudio = new DisputeAudio
                        {
                            FileName = disputeVM.AudioVM?.FileName,
                            DisputeId = Convert.ToInt32(disputeResponse.ResultData),
                            Audio = disputeVM.AudioVM.AudioContent
                        };

                        bool audioAdded = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.SaveDisputeAudio}", disputeAudio));

                        if (!audioAdded)
                        {
                            await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.DeleteEscalateIssue}?disputeId={ Convert.ToInt32(disputeResponse.ResultData)}", "");
                            response.Status = ResponseStatus.Error;
                            response.Message = "Task Failed";
                            return response;
                        }
                    }



                    response.Status = ResponseStatus.OK;
                    response.Message = "Your dispute issue has been registered";
                }

                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Task Failed";
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        private async Task<bool> AddImages(List<DisputeImages> imageVMs)
        {
            try
            {
                bool result = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.SaveDisputeImages}", imageVMs));
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<List<DisputeVM>> GetCompletedJobDetailsByCustomerAndStatusIds(long customerId, long statusId)
        {
            List<DisputeVM> disputeVMs = new List<DisputeVM>();

            try
            {
                List<JobDetail> jobDetail = JsonConvert.DeserializeObject<List<JobDetail>>
                       (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetCompletedJobDetailsByCustomerAndStatusIds}?customerId={customerId}&statusId={statusId}", ""));

                disputeVMs = jobDetail?.Select(x => new DisputeVM { Title = x?.Title, JobDetailId = x?.JobDetailId ?? 0 }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return disputeVMs;
        }

        public async Task<List<DispVM>> GetDisputeRecord(long customerid)
        {
            List<DispVM> dispVMS = new List<DispVM>();
            try
            {
                List<Dispute> disputes = new List<Dispute>();
                List<DisputeStatusDB> disputeStatuses = new List<DisputeStatusDB>();

                string disputeJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetDisputeRecord}?customerid={customerid}", "");

                disputes = JsonConvert.DeserializeObject<List<Dispute>>(disputeJson);

                List<long> disputeIds = disputes?.Select(x => x.DisputeStatusId).ToList();

                disputeStatuses = JsonConvert.DeserializeObject<List<DisputeStatusDB>>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetDisputeStatusByStatusIds}", disputeIds));

                dispVMS = disputes?.Select(d => new DispVM
                {
                    DisputeId = d?.DisputeId ?? 0,
                    CreatedOn = d.CreatedOn,
                    DisputeStatusID = d?.DisputeStatusId ?? 0,
                    disputeStatus = disputeStatuses?.FirstOrDefault(x => x.DisputeStatusId == d.DisputeStatusId)?.Name,

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return dispVMS;
        }

        public async Task<Response> updateStatuse(long disputeId, int disputeStatusId)
        {
            try
            {
                Response response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.updateStatuse}?disputeId={disputeId}&disputeStatusId={disputeStatusId}", ""));
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> PostTradesmanFeedback(TradesmanRatingsVM tradesmanRatingsVM)
        {
            try
            {
                TradesmanFeedback tradesmanFeedback = new TradesmanFeedback()
                {
                    CustomerId = tradesmanRatingsVM.CustomerId,
                    TradesmanId = tradesmanRatingsVM.TradesmanId,
                    Comments = tradesmanRatingsVM.Comments,
                    OverallRating = tradesmanRatingsVM.OverallRating,
                    CommunicationRating = tradesmanRatingsVM.CommunicationRating,
                    QualityRating = tradesmanRatingsVM.QualityRating,
                    JobDetailId = tradesmanRatingsVM.JobDetailId,
                    CreatedBy = tradesmanRatingsVM.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                return JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.PostTradesmanFeedback}", tradesmanFeedback));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> SetSupplierRating(RateSupplierVM rateSupplierVM)
        {
            try
            {
                SupplierFeedback supplierFeedback = new SupplierFeedback()
                {
                    CustomerId = rateSupplierVM.CustomerId,
                    SupplierId = rateSupplierVM.SupplierId,
                    Comments = rateSupplierVM.Comments,
                    OverallRating = rateSupplierVM.OverallRating,
                    CommunicationRating = rateSupplierVM.SupplierCommunicatonRating,
                    QualityRating = rateSupplierVM.SupplierServiceQualityRating,
                    CreatedBy = rateSupplierVM?.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                string jsonResponse = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SetSupplierRating}", supplierFeedback);
                Response response = JsonConvert.DeserializeObject<Response>(jsonResponse);

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> UpdateJobCost(long jobDetailId, decimal jobCost)
        {
            try
            {
                Response response = new Response();

                Response jobResponse = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobCost}?jobDetailId={jobDetailId}&jobCost={jobCost}")
                );

                if (jobResponse.Status == ResponseStatus.OK)
                {
                    response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.UpdateJobReceiptCost}?jobDetailId={jobDetailId}&jobCost={jobCost}"));
                }

                return jobResponse;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<InprogressVM>> GetAlljobDetails(long customerId, long statusId)
        {
            List<InprogressVM> inprogressVMs = new List<InprogressVM>();
            try
            {
                List<JobImages> jobImages = new List<JobImages>();

                List<CallCount> callCounts = new List<CallCount>();

                List<JobDetail> jobDetails = JsonConvert.DeserializeObject<List<JobDetail>>
                    (await httpClient.GetAsync($"{ _apiConfig.JobApiUrl}{ApiRoutes.Job.GetAlljobDetails}?customerId={customerId}&statusId={statusId}", ""));

                List<Bids> bidsList = JsonConvert.DeserializeObject<List<Bids>>
                (await httpClient.GetAsync($"{ _apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidListByCustomerId}?customerId={customerId}&statusId={(int)BidStatus.Accepted}", ""));

                List<long> tradesmanIds = jobDetails.Select(x => x.TradesmanId).ToList();

                List<Tradesman> tradesmenDetail = JsonConvert.DeserializeObject<List<Tradesman>>
                    (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradmanDetails}", tradesmanIds));

                List<long> jobQuotationIds = jobDetails.Select(x => x.JobQuotationId).ToList();

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                  (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationIds}", jobQuotationIds));

                List<CallCount> tradesmanCallLogs = callCounts;// = JsonConvert.DeserializeObject<List<CallCount>>
                                                               //(await httpClient.PostAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetTradesmanCallLogByJobQuotationId}", jobQuotationIds));

                foreach (JobDetail job in jobDetails)
                {
                    Tradesman _tradesman = tradesmenDetail.FirstOrDefault(x => x.TradesmanId == job.TradesmanId);
                    Bids bid = bidsList.FirstOrDefault(x => x.JobQuotationId == job.JobQuotationId);
                    inprogressVMs.Add(new InprogressVM()
                    {
                        Title = job?.Title,
                        CreatedOn = job.CreatedOn,
                        Address = $"{_tradesman?.Area},{_tradesman?.City}",
                        TradesmanName = $"{_tradesman?.FirstName} {_tradesman?.LastName}",
                        Getcount = tradesmanCallLogs.FirstOrDefault(x => x.JobQuotationId == job.JobQuotationId)?.Calls ?? 0,
                        JobImage = jobImages?.FirstOrDefault(c => c.JobQuotationId == job.JobQuotationId && c.IsMain == true)?.BidImage,
                        JobQuotationId = job?.JobQuotationId ?? 0,
                        TradesmanId = job?.TradesmanId,
                        IsFinished = job?.IsFinished,
                        TradesmanOffer = job.TradesmanBudget,
                        BidsId = bid.BidsId,
                        JobDetailId = job.JobDetailId
                    });
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return inprogressVMs;
        }

        public async Task<InprogressJobDetailVM> GetInprogressJobDetail(long JobQuotationId)
        {
            try
            {
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={JobQuotationId}", ""));
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={jobDetail.TradesmanId}", ""));
                Skill skill = JsonConvert.DeserializeObject<Skill>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillBySkillId}?skillId={jobDetail.SkillId}", ""));

                SubSkill subSkill = new SubSkill();
                if (jobDetail.SubSkillId.HasValue)
                {
                    subSkill = JsonConvert.DeserializeObject<SubSkill>
                        (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillById}?SubSkillId={jobDetail.SubSkillId.Value}", ""));
                }

                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationId}?JobQuotationId={JobQuotationId}", ""));

                City city = JsonConvert.DeserializeObject<City>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}", ""));

                TradesmanJobReceipts tradesmanJobReceipts = JsonConvert.DeserializeObject<TradesmanJobReceipts>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetTradesmanJobReceiptsByTradesmanId}?tradesmanId={jobDetail.TradesmanId}&jobDetailId={jobDetail.JobDetailId}", ""));

                List<JobImages> jobImages = JsonConvert.DeserializeObject<List<JobImages>>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationId}?jobQuotationId={JobQuotationId}", ""));

                //JobQuotationVideo jobQuotationVideo = JsonConvert.DeserializeObject<JobQuotationVideo>(await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={JobQuotationId}", ""));

                InprogressJobDetailVM inprogressJobDetailVM = new InprogressJobDetailVM
                {
                    JobQuotationId = jobDetail?.JobQuotationId ?? 0,
                    TradesmanId = jobDetail?.TradesmanId ?? 0,
                    TradesmanName = $"{tradesman.FirstName} {tradesman.LastName}",
                    CatagoryName = skill?.Name,
                    SubCatagoryName = subSkill?.Name,
                    WorkTitle = jobDetail?.Title,
                    WorkDescription = jobDetail?.Description,
                    WorkBudget = Math.Round(jobDetail?.TradesmanBudget ?? 0, 2),
                    WorkStartDate = jobDetail?.StartDate,
                    DirectPayment = tradesmanJobReceipts?.DirectPayment ?? false,
                    CityName = city?.Name,
                    CustomerId = jobDetail.CustomerId,
                    JobDetailId = jobDetail.JobDetailId,
                    Town = jobAddress?.Area,
                    StreetAddress = jobAddress?.StreetAddress,
                    PaymentStatus = jobDetail?.PaymentStatus,
                    ImageList = jobImages?.Select(x => new UserViewModels.ImageVM
                    {
                        Id = x?.BidImageId ?? 0,
                        FilePath = x?.FileName,
                        //ImageContent = x?.BidImage,
                        IsMain = x?.IsMain ?? false

                    }).ToList(),
                    //Videos = new UserViewModels.VideoVM()
                    //{
                    //    FilePath = jobQuotationVideo?.FileName,
                    //    //VideoContent = jobQuotationVideo?.Video
                    //},
                    IsFinished = jobDetail?.IsFinished
                };

                return inprogressJobDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new InprogressJobDetailVM();
            }
        }

        //======================== Old MarkAsFinshJob Function ///////////////////////////////////////////
        //public async Task<bool> MarkAsFinishedJob(MarkAsFinishJobVM finishedJobVM)
        //{
        //    bool isMarkFinish = false;
        //    try
        //    {
        //        Response response = new Response();

        //        JobDetail jobDetail = JsonConvert.DeserializeObinject<JobDetail>(
        //        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={finishedJobVM.JobQuotationId}", "")
        //        );

        //        TradesmanJobReceipts tradesmanJobReceipts = new TradesmanJobReceipts()
        //        {
        //            TradesmanId = jobDetail.TradesmanId,
        //            PaymentDate = DateTime.Now.Date,
        //            JobDetailId = jobDetail.JobDetailId,
        //            Amount = (decimal)jobDetail.TradesmanBudget,
        //            DirectPayment = finishedJobVM.isPaymentDirect,
        //            CreatedOn = jobDetail.CreatedOn,
        //            CreatedBy = jobDetail.CreatedBy
        //        };

        //        response = JsonConvert.DeserializeObject<Response>(
        //            await httpClient.PostAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.AddEditTradesmanJobReceipts}", tradesmanJobReceipts)
        //        );


        //        // JobDetail job = JsonConvert.DeserializeObject<JobDetail>(
        //        //     await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={finishedJobVM.JobDetailId}", ""));

        //        if (jobDetail != null)
        //        {
        //            jobDetail.StatusId = (int)BidStatus.Completed;
        //            jobDetail.EndDate = DateTime.Now;

        //            isMarkFinish = JsonConvert.DeserializeObject<bool>(
        //                await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.MarkAsFinishedJob}", jobDetail)
        //            );
        //        }

        //        // Notificatoin Request part

        //        try
        //        {
        //            if (isMarkFinish)
        //            {
        //                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
        //                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByTradesmanId}?tradesmanId={finishedJobVM.TradesmanId}", "")
        //                );

        //                Customer customer = JsonConvert.DeserializeObject<Customer>
        //                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={finishedJobVM.CustomerId}", "")
        //                );

        //                response = JsonConvert.DeserializeObject<Response>
        //                   (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
        //               );

        //                UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

        //                PostNotificationVM postNotificationVM = new PostNotificationVM()
        //                {
        //                    Title = NotificationTitles.JobIsFinished,
        //                    Body = $"Tradesman {tradesman.FirstName} {tradesman.LastName} has marked job {jobDetail.Title} as finished",
        //                    SenderEntityId = $"{finishedJobVM.JobDetailId},True",
        //                    TargetActivity = "RateTradesman",
        //                    To = userVM.FirebaseClientId,
        //                    SenderUserId = tradesman.UserId,
        //                    TargetDatabase = TargetDatabase.Customer,
        //                    TragetUserId = userVM.Id,
        //                    IsRead = false
        //                };

        //                JsonConvert.DeserializeObject<bool>(
        //                   await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
        //               );
        //            }
        //        }
        //        catch (Exception notificationExp)
        //        {
        //            Exc.AddErrorLog(notificationExp);
        //        }

        //        return isMarkFinish;
        //    }

        //    catch (Exception ex)
        //    {
        //        Exc.AddErrorLog(ex);
        //        return false;
        //    }
        //}

        //======================== New MarkAsFinshJob Function ///////////////////////////////////////////

        public async Task<bool> MarkAsFinishedJob(MarkAsFinishJobVM finishedJobVM)
        {
            bool isMarkFinish = false;
            try
            {
                Response response = new Response();
                decimal payableAmount;
                decimal discountAmount;

                isMarkFinish = JsonConvert.DeserializeObject<bool>(
                await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.LeadgerTransectionEntries}?jobQuotationId={finishedJobVM.JobQuotationId}", "")
                );
                
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(
                await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={finishedJobVM.JobQuotationId}", "")
                );



                //==========Voucher and Promotion Process =================

                //PromotionRedemptions getRecordPromotion = new PromotionRedemptions();
                //getRecordPromotion = JsonConvert.DeserializeObject<PromotionRedemptions>
                //                    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetProRecordByJQID}?redeemPromotionByJQID={jobDetail.JobQuotationId}"));


                //Redemptions getRecordVoucher = new Redemptions();
                //getRecordVoucher = JsonConvert.DeserializeObject<Redemptions>
                //                    (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetRedeemRecordByJQID}?redeemVoucherByJQID={jobDetail.JobQuotationId}"));

                //if (getRecordVoucher != null)
                //{
                //    discountAmount = getRecordVoucher.TotalDiscount;
                //    payableAmount = (decimal)jobDetail.TradesmanBudget - discountAmount;
                //}
                //else if (getRecordPromotion != null)
                //{
                //    discountAmount = getRecordPromotion.TotalDiscount;
                //    payableAmount = (decimal)jobDetail.TradesmanBudget - discountAmount;
                //}
                //else
                //{
                //    discountAmount = 0;
                //    payableAmount = (decimal)jobDetail.TradesmanBudget;
                //}


                ////==========Commission Process =================

                //List<ApplicationSettingVM> applicationSettingVMList = new List<ApplicationSettingVM>();
                //ApplicationSettingVM applicationSettingVM = new ApplicationSettingVM();
                //TradesmanCommissionOverride tradesmanCommission = new TradesmanCommissionOverride();
                //CategoryCommissionSetup categoryCommissionSetup = new CategoryCommissionSetup();

                //decimal netpayment;
                //decimal commission;
                //string jsonresponse = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSettingList}");
                //applicationSettingVMList = JsonConvert.DeserializeObject<List<ApplicationSettingVM>>(jsonresponse);
                //if(applicationSettingVMList != null)
                //{
                //    applicationSettingVM = applicationSettingVMList.Where(a => a.SettingName == "Commission" && a.IsActive == true).FirstOrDefault();
                //    if(applicationSettingVM != null)
                //    {
                //        tradesmanCommission = JsonConvert.DeserializeObject<TradesmanCommissionOverride>
                //            (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getTradesmanCommissionOverride}?trademanId={jobDetail.TradesmanId}&categoryId={jobDetail.SkillId}"));

                //        categoryCommissionSetup = JsonConvert.DeserializeObject<CategoryCommissionSetup>
                //            (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getCommissionByCategory}?categoryId={jobDetail.SkillId}"));


                //        if (tradesmanCommission != null)
                //        {
                //            if (tradesmanCommission.CommissionOverridePercentage > 0)
                //            {
                //                commission = (decimal)tradesmanCommission.CommissionOverridePercentage * ((decimal)jobDetail.TradesmanBudget / 100);
                //                netpayment = (decimal)(discountAmount - commission);
                //            }
                //            else
                //            {
                //                netpayment = (decimal)(discountAmount - tradesmanCommission.CommissionOverrideAmount);
                //                commission = (decimal)tradesmanCommission.CommissionOverrideAmount;
                //            }

                //        }
                //        else if (categoryCommissionSetup != null)
                //        {
                //            if (categoryCommissionSetup.CommissionPercentage > 0)
                //            {
                //                commission = (decimal)categoryCommissionSetup.CommissionPercentage * ((decimal)jobDetail.TradesmanBudget / 100);
                //                netpayment = (decimal)(discountAmount - commission);
                //            }
                //            else
                //            {
                //                netpayment = (decimal)(discountAmount - categoryCommissionSetup.CommisionAmount);
                //                commission = (decimal)categoryCommissionSetup.CommisionAmount;
                //            }

                //        }
                //        else
                //        {
                //            netpayment = discountAmount;
                //            commission = 0;
                //        }
                //    }
                //    else
                //    {
                //        netpayment = discountAmount;
                //        commission = 0;
                //    }
                //}
                //else
                //{
                //    netpayment = discountAmount;
                //    commission = 0;
                //}




                //if (payableAmount <= 0)
                //{
                //    payableAmount = 0;
                //    netpayment = (decimal)jobDetail.TradesmanBudget - commission;
                //}

                ////////  getting wallet response //////
                //Response walletResponse = new Response();
                //var responseJson = await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetWalletValueByBidId}?customerId={finishedJobVM.CustomerId}&bidId={finishedJobVM.BidId}", "");
                //walletResponse = JsonConvert.DeserializeObject<Response>(responseJson);
                //var walletValue = walletResponse.ResultData?.ToString();
                //decimal wValue = Convert.ToInt64(walletValue);
                //TradesmanJobReceipts tradesmanJobReceipts = new TradesmanJobReceipts()
                //{
                //    TradesmanId = jobDetail.TradesmanId,
                //    PaymentDate = DateTime.Now.Date,
                //    JobDetailId = jobDetail.JobDetailId,
                //    Amount = (decimal)jobDetail.TradesmanBudget,
                //    DirectPayment = finishedJobVM.isPaymentDirect,
                //    CreatedOn = jobDetail.CreatedOn,
                //    CreatedBy = jobDetail.CreatedBy,
                //    PayableAmount = payableAmount,
                //    DiscountedAmount = discountAmount,
                //    CustomerId = jobDetail.CustomerId,
                //    ServiceCharges = jobDetail.ServiceCharges,
                //    OtherCharges = jobDetail.AdditionalCharges,
                //    Commission = commission,
                //    NetPayableToTradesman = netpayment,
                //    PaymentStatus = jobDetail.PaymentStatus,
                //    PaidViaWallet = wValue

                //};

                //response = JsonConvert.DeserializeObject<Response>(
                //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddEditTradesmanJobReceipts}", tradesmanJobReceipts)
                //);

                //JobDetail job = JsonConvert.DeserializeObject<JobDetail>(
                //    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={finishedJobVM.JobDetailId}", ""));


                //if (jobDetail != null)
                //{
                //    jobDetail.StatusId = (int)BidStatus.Completed;
                //    jobDetail.EndDate = DateTime.Now;

                //    isMarkFinish = JsonConvert.DeserializeObject<bool>(
                //        await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.MarkAsFinishedJob}", jobDetail)
                //    );
                //}

                //////////////// wallet code ////////////////////


                //////////////// Tradesman Transaction /////////////////////
                //HW.PackagesAndPaymentsModels.SubAccount subAccount = new SubAccount();
                //subAccount = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                //    await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccountByTradesmanId}?tradesmanId={finishedJobVM.TradesmanId}", ""));

                //PackagesAndPaymentsModels.LeadgerTransection leadgerTransection = new PackagesAndPaymentsModels.LeadgerTransection()
                //{
                //    AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                //    SubAccountId = subAccount.SubAccountId,
                //    Debit = jobDetail.TradesmanBudget,
                //    Credit = 0,
                //    Active = true,
                //    RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //    RefTradesmanSubAccountId = subAccount.TradesmanId,
                //    ReffrenceDocumentNo = job.JobQuotationId,
                //    ReffrenceDocumentId = job.JobDetailId,
                //    ReffrenceDocumentType = "Job",
                //    CreatedOn = DateTime.Now,
                //    CreatedBy = finishedJobVM.UserId
                //};
                //if (payableAmount == 0)
                //{
                //    leadgerTransection.Debit = netpayment;
                //}
                //Response transactionresponse = new Response();

                //transactionresponse = JsonConvert.DeserializeObject<Response>(
                //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransection)
                //);
                //////////////// Customer And HoomWork Transaction /////////////////////
                //HW.PackagesAndPaymentsModels.SubAccount subAccountCustomer = new SubAccount();
                //SubAccount sub = new SubAccount();

                //if (tradesmanJobReceipts.PayableAmount > 0)
                //{

                //    subAccountCustomer = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                //    await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={finishedJobVM.CustomerId}", ""));


                //    PackagesAndPaymentsModels.LeadgerTransection leadgerTransction = new PackagesAndPaymentsModels.LeadgerTransection()
                //    {
                //        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                //        SubAccountId = subAccountCustomer.SubAccountId,
                //        Debit = 0,
                //        Credit = tradesmanJobReceipts?.PayableAmount ?? 0,
                //        Active = true,
                //        RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //        RefTradesmanSubAccountId = finishedJobVM.TradesmanId,
                //        ReffrenceDocumentNo = job.JobQuotationId,
                //        ReffrenceDocumentId = job.JobDetailId,
                //        ReffrenceDocumentType = "Job",
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = finishedJobVM.UserId
                //    };


                //    transactionresponse = JsonConvert.DeserializeObject<Response>(
                //        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransction)
                //    );



                //    sub = JsonConvert.DeserializeObject<SubAccount>(
                //        await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkSubAccount}", "")
                //    );

                //    LeadgerTransection leadgerTransctn = new LeadgerTransection()
                //    {
                //        AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                //        SubAccountId = sub.SubAccountId,
                //        Debit = 0,
                //        Credit = tradesmanJobReceipts?.DiscountedAmount ?? 0,
                //        Active = true,
                //        RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //        RefTradesmanSubAccountId = finishedJobVM.TradesmanId,
                //        ReffrenceDocumentNo = job.JobQuotationId,
                //        ReffrenceDocumentId = job.JobDetailId,
                //        ReffrenceDocumentType = "Job",
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = finishedJobVM.UserId
                //    };

                //    transactionresponse = JsonConvert.DeserializeObject<Response>(
                //        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn)
                //    );
                //    ///// FRom Wallet Amount ///////

                //    decimal walletamount = 0;

                //        if (walletResponse.Status == ResponseStatus.OK)
                //        {
                //            var aa = walletResponse.ResultData?.ToString();
                //        walletamount = Convert.ToInt64(aa);


                //            PackagesAndPaymentsModels.LeadgerTransection leadgerTransctionTrades = new PackagesAndPaymentsModels.LeadgerTransection()
                //            {
                //                AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                //                SubAccountId = subAccount.SubAccountId,
                //                Debit = walletamount,
                //                Credit = 0,
                //                Active = true,
                //                RefTradesmanSubAccountId = finishedJobVM.TradesmanId,
                //                ReffrenceDocumentNo = job.JobQuotationId,
                //                ReffrenceDocumentId = job.JobDetailId,
                //                ReffrenceDocumentType = "Job",
                //                CreatedOn = DateTime.Now,
                //                CreatedBy = finishedJobVM.UserId
                //            };

                //            transactionresponse = JsonConvert.DeserializeObject<Response>(
                //                await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctionTrades)
                //            );

                //        }
                //    ////////// Customer Self Deposit ////////////////////
                //    ///
                //    //if (jobDetail.PaymentStatus == (int)Utility.PaymentStatus.DirectPayment)
                //    //{
                //        PackagesAndPaymentsModels.LeadgerTransection SelfleadgerTransction = new PackagesAndPaymentsModels.LeadgerTransection()
                //        {
                //            AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                //            SubAccountId = subAccountCustomer.SubAccountId,
                //            Debit = tradesmanJobReceipts?.PayableAmount ?? 0,
                //            Credit = 0,
                //            Active = true,
                //            RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //            ReffrenceDocumentNo = job.JobQuotationId,
                //            ReffrenceDocumentType = "Self Deposit",
                //            CreatedOn = DateTime.Now,
                //            CreatedBy = finishedJobVM.UserId
                //        };
                //        if (walletamount > 0 && jobDetail.PaymentStatus == (int)Utility.PaymentStatus.JazzCash)
                //        {
                //            decimal remainingamount = payableAmount - walletamount;
                //            SelfleadgerTransction.Debit = remainingamount;
                //        }
                //        transactionresponse = JsonConvert.DeserializeObject<Response>(
                //            await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", SelfleadgerTransction)
                //        );

                //   // }
                //    if (jobDetail.PaymentStatus == (int)Utility.PaymentStatus.JazzCash)
                //    {
                //        ////////// JazzCash Tradesman Transaction////////////////////
                //        ///


                //        PackagesAndPaymentsModels.LeadgerTransection leadgerTransection1 = new PackagesAndPaymentsModels.LeadgerTransection()
                //        {
                //            AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                //            SubAccountId = subAccount.SubAccountId,
                //            Debit = payableAmount,
                //            Credit = 0,
                //            Active = true,
                //            RefTradesmanSubAccountId = subAccount.TradesmanId,
                //            ReffrenceDocumentNo = job.JobQuotationId,
                //            ReffrenceDocumentId = job.JobDetailId,
                //            ReffrenceDocumentType = "Job",
                //            CreatedOn = DateTime.Now,
                //            CreatedBy = finishedJobVM.UserId
                //        };
                //        if(walletamount > 0)
                //        {
                //            decimal remainingamount = payableAmount - walletamount;
                //            leadgerTransection1.Debit = remainingamount;
                //        }
                //        transactionresponse = JsonConvert.DeserializeObject<Response>(
                //            await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransection1)
                //        );


                //        /////////////////// Jazz Cash Payment and use of wallet//////////////

                //        if(walletamount > 0)
                //        {
                //            PackagesAndPaymentsModels.LeadgerTransection leadgerTransctionuser = new PackagesAndPaymentsModels.LeadgerTransection()
                //            {
                //                AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                //                SubAccountId = subAccountCustomer.SubAccountId,
                //                Debit = walletamount,
                //                Credit = 0,
                //                Active = true,
                //                RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //                ReffrenceDocumentNo = job.JobQuotationId,
                //                ReffrenceDocumentId = job.JobDetailId,
                //                ReffrenceDocumentType = "Job",
                //                CreatedOn = DateTime.Now,
                //                CreatedBy = finishedJobVM.UserId
                //            };

                //            transactionresponse = JsonConvert.DeserializeObject<Response>(
                //                await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctionuser)
                //            );
                //        }



                //        ////////// JazzCash Hoomwork Transaction////////////////////

                //        //LeadgerTransection leadgerTransctn2 = new LeadgerTransection()
                //        //{
                //        //    AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                //        //    SubAccountId = sub.SubAccountId,
                //        //    Debit = 0,
                //        //    Credit = jobDetail.TradesmanBudget,
                //        //    Active = true,
                //        //    RefTradesmanSubAccountId = finishedJobVM.TradesmanId,
                //        //    ReffrenceDocumentNo = job.JobQuotationId,
                //        //    ReffrenceDocumentId = job.JobDetailId,
                //        //    ReffrenceDocumentType = "Job",
                //        //    CreatedOn = DateTime.Now,
                //        //    CreatedBy = finishedJobVM.UserId
                //        //};

                //        //transactionresponse = JsonConvert.DeserializeObject<Response>(
                //        //    await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn2)
                //        //);
                //    }


                //}

                //////////// Commission Transaction////////////////////
                /////
                //if (tradesmanCommission != null || categoryCommissionSetup != null)
                //{
                //    PackagesAndPaymentsModels.LeadgerTransection commsionTransactionTradesman = new PackagesAndPaymentsModels.LeadgerTransection()
                //    {
                //        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountPayables),
                //        SubAccountId = subAccount.SubAccountId,
                //        Debit = 0,
                //        Credit = commission,
                //        Active = true,
                //        RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //        RefTradesmanSubAccountId = subAccount.TradesmanId,
                //        ReffrenceDocumentNo = job.JobQuotationId,
                //        ReffrenceDocumentId = job.JobDetailId,
                //        ReffrenceDocumentType = "Commission on Job",
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = finishedJobVM.UserId
                //    };
                //    transactionresponse = JsonConvert.DeserializeObject<Response>(
                //        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", commsionTransactionTradesman)
                //    );

                //    LeadgerTransection commisionTransactionHoomwork = new LeadgerTransection()
                //    {
                //        AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                //        SubAccountId = sub.SubAccountId,
                //        Debit = commission,
                //        Credit = 0,
                //        Active = true,
                //        RefCustomerSubAccountId = finishedJobVM.CustomerId,
                //        RefTradesmanSubAccountId = finishedJobVM.TradesmanId,
                //        ReffrenceDocumentNo = job.JobQuotationId,
                //        ReffrenceDocumentId = job.JobDetailId,
                //        ReffrenceDocumentType = "Commission on Job",
                //        CreatedOn = DateTime.Now,
                //        CreatedBy = finishedJobVM.UserId
                //    };

                //    transactionresponse = JsonConvert.DeserializeObject<Response>(
                //        await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", commisionTransactionHoomwork)
                //    );
                //}

                /////////////////        Refferal code   //////////////////////
                ReferalCode referalCode = new ReferalCode();
                referalCode = JsonConvert.DeserializeObject<ReferalCode>(
                await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetReferalCodeByUserId}?jobQuotationId={finishedJobVM.JobQuotationId}", "")
                );
                if (referalCode != null)
                {
                    Response responseuser = JsonConvert.DeserializeObject<Response>(
                                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={referalCode.UserId}")
                                    );
                    if (responseuser.Status == ResponseStatus.OK)
                    {
                        string sharedRefferalUserID = JsonConvert.DeserializeObject<string>
                        (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByPublicId}?publicID={referalCode.ReferralCode}"));

                        Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={sharedRefferalUserID}"));

                        //        subAccountCustomer = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                        //            await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={customer.CustomerId}", ""));



                        //        PackagesAndPaymentsModels.LeadgerTransection leadgerTransction = new PackagesAndPaymentsModels.LeadgerTransection()
                        //        {
                        //            AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                        //            SubAccountId = subAccountCustomer.SubAccountId,
                        //            Debit = referalCode.RefferalAmount ?? 0,
                        //            Credit = 0,
                        //            Active = true,
                        //            RefCustomerSubAccountId = subAccountCustomer.CustomerId,
                        //            RefTradesmanSubAccountId = 0,
                        //            ReffrenceDocumentNo = referalCode.JobQuotationId,
                        //            ReffrenceDocumentId = 0,
                        //            ReffrenceDocumentType = "Referral",
                        //            CreatedOn = DateTime.Now,
                        //            CreatedBy = referalCode.ReferredUser
                        //        };

                        //        Response refferalTransactionResponse = JsonConvert.DeserializeObject<Response>(
                        //            await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransction)
                        //        );

                        //        sub = JsonConvert.DeserializeObject<SubAccount>(
                        //        await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkSubAccount}", "")
                        //        );

                        //        LeadgerTransection leadgerTransctn = new LeadgerTransection()
                        //        {
                        //            AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                        //            SubAccountId = sub.SubAccountId,
                        //            Debit = 0,
                        //            Credit = referalCode.RefferalAmount ?? 0,
                        //            Active = true,
                        //            RefCustomerSubAccountId = finishedJobVM.CustomerId,
                        //            ReffrenceDocumentNo = job.JobQuotationId,
                        //            ReffrenceDocumentId = job.JobDetailId,
                        //            ReffrenceDocumentType = "Referral",
                        //            CreatedOn = DateTime.Now,
                        //            CreatedBy = finishedJobVM.UserId
                        //        };

                        //        Response refferalHoomworkTransactionResponse = JsonConvert.DeserializeObject<Response>(
                        //            await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn)
                        //        );
                        int ReferralAmount = 0;
                        ReferralAmount = Convert.ToInt32(referalCode.RefferalAmount);
                        Response identityResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}"));
                        UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(identityResponse?.ResultData?.ToString());
                        PostNotificationVM postNotificationVM = new PostNotificationVM()
                        {
                            Title = ClientsConstants.NotificationTitles.Credit,
                            Body = $"You have credited {ReferralAmount} Rs with reffer",
                            To = $"{userVM?.FirebaseClientId}",
                            TargetActivity = "Home",
                            SenderUserId = customer?.UserId,
                            SenderEntityId = referalCode.ReferredUser,
                            TargetDatabase = TargetDatabase.Customer,
                            TragetUserId = userVM?.Id,
                            IsRead = false
                        };

                        bool notificationResult = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));

                    }


                }






                // Notificatoin Request part

                try
                {
                    if (isMarkFinish)
                    {


                        Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByTradesmanId}?tradesmanId={finishedJobVM.TradesmanId}", "")
                        );

                        Customer customer = JsonConvert.DeserializeObject<Customer>
                            (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={finishedJobVM.CustomerId}", "")
                        );

                        response = JsonConvert.DeserializeObject<Response>
                           (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
                       );

                        UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                        PostNotificationVM postNotificationVM = new PostNotificationVM()
                        {
                            Title = NotificationTitles.JobIsFinished,
                            Body = $"Customer {customer.FirstName} {customer.LastName} has marked job {jobDetail.Title} as finished",
                            SenderEntityId = $"{finishedJobVM.JobDetailId},True",
                            TargetActivity = "RateTradesman",
                            To = userVM.FirebaseClientId,
                            SenderUserId = userVM.Id,
                            TargetDatabase = TargetDatabase.Tradesman,
                            TragetUserId = tradesman.UserId,
                            IsRead = false
                        };


                        var NoticationStatus = JsonConvert.DeserializeObject<bool>(
                           await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                        );

                        //var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                        //List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                        //SmsVM numList = new SmsVM();

                        //authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                        //numList.Message = $"Tradesman {tradesman.FirstName} {tradesman.LastName} has marked job '{jobDetail.Title}' with '{jobDetail.JobQuotationId}' as finished";

                        //await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);



                    }
                }
                catch (Exception notificationExp)
                {
                    Exc.AddErrorLog(notificationExp);
                }

                return isMarkFinish;
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public async Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(long tradesmanId, int pageNumber, int pageSize)
        {
            List<JobLeadsVM> jobLeadsVM = new List<JobLeadsVM>();
            List<JobLeadsVM> unexpiredjobLeadsVM = new List<JobLeadsVM>();
            try
            {
                string jobLeads = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsBySkill}?tradesmanId={tradesmanId}&pageNUmber={pageNumber}&pageSize={pageSize }");
                jobLeadsVM = JsonConvert.DeserializeObject<List<JobLeadsVM>>(jobLeads);
                if (jobLeadsVM != null)
                {
                    if (jobLeadsVM.Count > 0)
                    {
                        foreach (var item in jobLeadsVM)
                        {
                            if (item.BidCount == 0 && item.PostedOn.AddDays(15) > DateTime.Now || item.BidCount > 0)
                            {
                                unexpiredjobLeadsVM.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return unexpiredjobLeadsVM;
        }

        public async Task<List<JobLeadsWebVM>> GetJobLeadsWebByTradesmanId(long tradesmanId, int pageNumber, int pageSize)
        {
            List<JobLeadsWebVM> jobLeadsVM = new List<JobLeadsWebVM>();

            try
            {
                string jobLeads = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsWebBySkill}?tradesmanId={tradesmanId}&pageNUmber={pageNumber}&pageSize={pageSize }");
                jobLeadsVM = JsonConvert.DeserializeObject<List<JobLeadsWebVM>>(jobLeads);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return jobLeadsVM;
        }

        public async Task<List<BidVM>> GetActiveBidsDetails(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId)
        {
            try
            {
                string getActiveBids = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetActiveBidsDetails}?tradesmanId={tradesmanId}&pageNumber={pageNumber}&pageSize={pageSize}&bidsStatusId={bidsStatusId}");
                List<BidVM> bidVM = JsonConvert.DeserializeObject<List<BidVM>>(getActiveBids);
                return bidVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidVM>();
            }
        }

        public async Task<List<BidVM>> GetDeclinedBidsDetails(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId)
        {
            try
            {
                string getDeclinedBids = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetDeclinedBidsDetails}?tradesmanId={tradesmanId}&pageNumber={pageNumber}&pageSize={pageSize}&bidsStatusId={bidsStatusId}");
                List<BidVM> bidVM = JsonConvert.DeserializeObject<List<BidVM>>(getDeclinedBids);
                return bidVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidVM>();
            }
        }

        public async Task<List<BidWebVM>> GetActiveBidsDetailsWeb(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId)
        {
            try
            {
                string getActiveBids = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetActiveBidsDetailsWeb}?tradesmanId={tradesmanId}&pageNumber={pageNumber}&pageSize={pageSize}&bidsStatusId={bidsStatusId}");
                List<BidWebVM> bidVM = JsonConvert.DeserializeObject<List<BidWebVM>>(getActiveBids);
                return bidVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidWebVM>();
            }
        }

        public async Task<List<BidWebVM>> GetDeclinedBidsDetailsWeb(long tradesmanId, int pageNumber, int pageSize, int bidsStatusId)
        {
            try
            {
                string getDeclinedBids = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetDeclinedBidsDetailsWeb}?tradesmanId={tradesmanId}&pageNumber={pageNumber}&pageSize={pageSize}&bidsStatusId={bidsStatusId}");
                List<BidWebVM> bidVM = JsonConvert.DeserializeObject<List<BidWebVM>>(getDeclinedBids);
                return bidVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<BidWebVM>();
            }
        }

        public async Task<List<MyQuotationsVM>> SpGetPostedJobsByCustomerId(int pageNumber, int pageSize, long customerId, int statusId, bool bidStatus)
        {
            try
            {
                var jsonRequestTest = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SpGetPostedJobsByCustomerId}?pageNumber={pageNumber}&pageSize={pageSize}&customerId={customerId}&statusId={statusId}&bidStatus={bidStatus}");
                return JsonConvert.DeserializeObject<List<MyQuotationsVM>>(jsonRequestTest);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }
        public async Task<List<MyQuotationsVM>> SpGetJobsByCustomerId(long customerId)
        {
          try
          {
            var jsonRequestTest = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SpGetJobsByCustomerId}?customerId={customerId}");
            return JsonConvert.DeserializeObject<List<MyQuotationsVM>>(jsonRequestTest);
          }
          catch (Exception ex)
          {
            Exc.AddErrorLog(ex);
            return new List<MyQuotationsVM>();
          }
        }

        public async Task<List<MyQuotationsVM>> GetPostedJobs(int pageNumber, int pageSize, long customerId)
        {
            try
            {
                var jsonRequestTest = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetPostedJobs}?pageNumber={pageNumber}&pageSize={pageSize}&customerId={customerId}");
                return JsonConvert.DeserializeObject<List<MyQuotationsVM>>(jsonRequestTest);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MyQuotationsVM>();
            }
        }

        public async Task<Response> GetJobDetail(long tradesmanId, int pageNumber, int pageSize, int jobStatusId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                       await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetail}?pageNumber={pageNumber}&pageSize={pageSize}&tradesmanId={tradesmanId}&jobStatusId={jobStatusId}")
                   );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<string> GetJobDetailWeb(long tradesmanId, int pageNumber, int pageSize, int jobStatusId)
        {
            return await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailWeb}?pageNumber={pageNumber}&pageSize={pageSize}&tradesmanId={tradesmanId}&jobStatusId={jobStatusId}");
        }

        public async Task<List<InprogressVM>> GetInprogressJob(int pageNumber, int pageSize, long customerId, int statusId)
        {
            try
            {
                string inprogressJob = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetInprogressJob}?customerId={customerId}&statusId={statusId}&pageNumber={pageNumber}&pageSize={pageSize}");
                List<InprogressVM> inprogressVMs = JsonConvert.DeserializeObject<List<InprogressVM>>(inprogressJob);
                return inprogressVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InprogressVM>();
            }
        }

        public async Task<List<FinishedJobVM>> GetFinishedJob(long customerId, int statusId, int pageNumber, int pageSize)
        {
            try
            {
                string finishedJob = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFinishedJob}?customerId={customerId}&statusId={statusId}&pageNumber={pageNumber}&pageSize={pageSize}");
                List<FinishedJobVM> finishedJobvm = JsonConvert.DeserializeObject<List<FinishedJobVM>>(finishedJob);
                return finishedJobvm;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<FinishedJobVM>();
            }
        }

        public async Task<List<WebLiveLeadsVM>> WebLiveLeads(long jobQuotationId)
        {
            List<WebLiveLeadsVM> WebLiveLeadsVM = new List<WebLiveLeadsVM>();
            var jsonResponse = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.WebLiveLeads}?jobQuotationId={jobQuotationId}");
            WebLiveLeadsVM = JsonConvert.DeserializeObject<List<WebLiveLeadsVM>>(jsonResponse);
            return WebLiveLeadsVM;
        }

        public async Task<List<IdValueVM>> WebLiveLeadsLatLong()
        {
            List<IdValueVM> idValues = new List<IdValueVM>();
            var jsonResponse = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.WebLiveLeadsLatLong}");
            idValues = JsonConvert.DeserializeObject<List<IdValueVM>>(jsonResponse);
            return idValues;
        }

        public async Task<List<CompletedJobListVm>> CompletedJobListAdmin(long pageNumber, long pageSize, string customerName, string jobId, string jobDetailId, string startDate, string endDate, string city, string location, string fromDate, string toDate, string dataOrderBy = "")
        {
            List<CompletedJobListVm> completedJobListVms;

            try
            {
                string completedJobsListJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.CompletedJobListAdmin}?pageNumber={pageNumber}&pageSize={pageSize}&customerName={customerName}&jobId={jobId}&jobDetailId={jobDetailId}&startDate={startDate}&endDate={endDate}&city={city}&location={location}&fromDate={fromDate}&toDate={toDate}&dataOrderBy={dataOrderBy}");
                completedJobListVms = JsonConvert.DeserializeObject<List<CompletedJobListVm>>(completedJobsListJson);

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CompletedJobListVm>();
            }

            return completedJobListVms;
        }

        public async Task<List<JobDetail>> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsForReport}?startDate={startDate}&endDate={endDate}&customers={customer}&status={status}");
            return JsonConvert.DeserializeObject<List<JobDetail>>(response);
        }

        public async Task<Response> PostJobContactInfo(PersonalDetailsVM personalDetailsVM)
        {
            Response response = new Response();
            var aa = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.PostJobContactInfo}", personalDetailsVM);
            response = JsonConvert.DeserializeObject<Response>(aa);
            return response;
        }

        public async Task<List<GetJobsCountVM>> GetJobsCount(long tradesmanId)
        {
            List<GetJobsCountVM> getJobsCounts = new List<GetJobsCountVM>();
            var aa = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsCount}?TradesmanId={tradesmanId}");
            getJobsCounts = JsonConvert.DeserializeObject<List<GetJobsCountVM>>(aa);
            return getJobsCounts;
        }

        public async Task<List<WebLiveLeadsVM>> WebLiveLeadsPanel(long TradesmanId,int statusId, int pageNumber, int pageSize)
        {
            List<WebLiveLeadsVM> webLiveLeadsVMs = new List<WebLiveLeadsVM>();
            try
            {
                webLiveLeadsVMs = JsonConvert.DeserializeObject<List<WebLiveLeadsVM>>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.WebLiveLeadsPanel}?TradesmanId={TradesmanId}&statusId={statusId}&pageNumber={pageNumber}&pageSize={pageSize}"));
               
            }
            catch ( Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<WebLiveLeadsVM>();
            }
            return webLiveLeadsVMs;
        }

        public async Task<bool> GetBidStatusForTradesmanId(long jobQuotationId, long tradesmanId, int statusId)
        {
            try
            {
                var jobDeclinedForTradesman = JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidStatusForTradesmanId}?jobQuotationId={jobQuotationId}&tradesmanId={tradesmanId}&statusId={statusId}"));
                return jobDeclinedForTradesman;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<List<JobImages>> GetJobImagesListByJobQuotationIds(List<long> jobQuotationIds)
        {
            try
            {
                List<JobImages> jobImages = new List<JobImages>();

                jobImages = JsonConvert.DeserializeObject<List<JobImages>>
                               (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesListByJobQuotationIds}", jobQuotationIds));
                return jobImages;
            }
            catch (Exception  ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobImages>();
            }

        }

        public async Task<string> GetBidCountsOnJob(long jobQuotationId)
        {
            var bidCount = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidCountsOnJob}?jobQuotationId={jobQuotationId}");
            return bidCount;
        }

        public async Task<List<EsclateOption>> getEscalateOptions(int userRole)
        {
            List<EsclateOption> escalateOptions = new List<EsclateOption>();
            string jsonResponce = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.getEscalateOptions}?userRole={userRole}", "");
            escalateOptions = JsonConvert.DeserializeObject<List<EsclateOption>>(jsonResponce);
            return escalateOptions;
        }

        public async Task<Response> submitIssue(EsclateRequest esclateRequest)
        {
            Response response = new Response();
            var jsonRes = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.submitIssue}", esclateRequest);
            response = JsonConvert.DeserializeObject<Response>(jsonRes);
            return response;
        }

        public async Task<Response> getEscalateIssueByJQID(long jobQuotationId, int userRole, int status )
        {
            Response response = new Response();
            var jsonRes = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.getEscalateIssueByJQID}?jobQuotationId={jobQuotationId}&userRole={userRole}&status={status}", "");
            response = JsonConvert.DeserializeObject<Response>(jsonRes);
            return response;
        }        
        public async Task<Response> UpdateBidByStatusId(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            var jsonRes = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateBidByStatusId}",bidDetailsVM);
            response = JsonConvert.DeserializeObject<Response>(jsonRes);
            return response;
        }        
        public async Task<string> GetAcceptedBidsList(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            return await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetAcceptedBidsList}",bidDetailsVM);
        }    
        public async Task<string> GetInprogressJobsMobile(InProgressJobsVM inProgressJobsVM)
        {
            Response response = new Response();
            return await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetInprogressJobsMobile}", inProgressJobsVM);
        }       
        public async Task<Response> StartOrFinishJob(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            var jsonRes = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.StartOrFinishJob}",bidDetailsVM);
            response = JsonConvert.DeserializeObject<Response>(jsonRes);
            return response;
        }

        public async Task<Response> JobStartNotification(long tradesmanId, long? customerId, long bidId, string jobTitle, long jobQuotationId)
        {
            Response response = new Response();

            Customer customer = JsonConvert.DeserializeObject<Customer>
                           (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}")
                       );

            Response identityResponse = JsonConvert.DeserializeObject<Response>
                (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
            );


            try
            {
                if (!string.IsNullOrEmpty(identityResponse?.ResultData?.ToString()))
                {
                    UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(identityResponse?.ResultData?.ToString());

                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        Title = "Start Job",
                        Body = $"Job '{jobTitle}' has been started '{jobQuotationId}'",
                        To = $"{userVM?.FirebaseClientId}",
                        TargetActivity = "JobStarted",
                        SenderUserId = customer?.UserId,
                        SenderEntityId = Convert.ToString(jobQuotationId),
                        TargetDatabase = TargetDatabase.Customer,
                        TragetUserId = userVM?.Id,
                        IsRead = false
                    };

                    bool notificationResult = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                    );

                    if (notificationResult)
                    {
                        response.Status = ResponseStatus.OK;
                        response.Message = "Send Successful";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Unable to send Notification";
                    }
                }
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
                response.Status = ResponseStatus.Error;
                response.Message = "Unable to post Notification";
            }
            return response;
        }
        public async Task<Response> JobStartNotificationForTradesman(long tradesmanId, long? customerId, long bidId, string jobTitle, long jobQuotationId)
        {
            Response response = new Response();

            Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>
                           (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradesmanId}")
                       );

            Response identityResponse = JsonConvert.DeserializeObject<Response>
                (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={tradesman.UserId}")
            );


            try
            {
                if (!string.IsNullOrEmpty(identityResponse?.ResultData?.ToString()))
                {
                    UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(identityResponse?.ResultData?.ToString());

                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        Title = "Start Job",
                        Body = $"Job '{jobTitle}' has been started '{jobQuotationId}'",
                        To = $"{userVM?.FirebaseClientId}",
                        TargetActivity = "JobStarted",
                        SenderUserId = tradesman?.UserId,
                        SenderEntityId = Convert.ToString(jobQuotationId),
                        TargetDatabase = TargetDatabase.Tradesman,
                        TragetUserId = userVM?.Id,
                        IsRead = false
                    };

                    bool notificationResult = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                    );

                    if (notificationResult)
                    {
                        response.Status = ResponseStatus.OK;
                        response.Message = "Send Successful";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Unable to send Notification";
                    }
                }
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
                response.Status = ResponseStatus.Error;
                response.Message = "Unable to post Notification";
            }
            return response;
        }
        public async Task<Response> JobFifnishedNotification(long JobQuotationId)
        {
            Response response = new Response();

            try
            {
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(
                await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={JobQuotationId}", "")
                );
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByTradesmanId}?tradesmanId={jobDetail.TradesmanId}", "")
                        );

                Customer customer = JsonConvert.DeserializeObject<Customer>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={jobDetail.CustomerId}", "")
                );

                response = JsonConvert.DeserializeObject<Response>
                   (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
               );

                UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    Title = NotificationTitles.JobIsFinished,
                    Body = $"Tradesman {tradesman.FirstName} {tradesman.LastName} has marked job {jobDetail.Title} as finished",
                    SenderEntityId = $"{jobDetail.JobDetailId},True",
                    TargetActivity = "FinishedJobTradesman",
                    To = userVM.FirebaseClientId,
                    SenderUserId = tradesman.UserId,
                    TargetDatabase = TargetDatabase.Customer,
                    TragetUserId = userVM.Id,
                    IsRead = false
                };


                bool notificationResult = JsonConvert.DeserializeObject<bool>(
                   await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                );
                if (notificationResult)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Send Successful";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Unable to send Notification";
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Unable to send Notification";
            }
            return response;
        }     
        public async Task<Response> UpdateJobAdditionalCharges(BidDetailsVM bidDetailsVM)
        {
            Response response = new Response();
            var jsonRes = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobAdditionalCharges}",bidDetailsVM);
            response = JsonConvert.DeserializeObject<Response>(jsonRes);
            return response;
        }

        public async Task<Response> JobPostByFacebookLeads(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobPostByFacebookLeads}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> GetUserFromFacebookLeads(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetUserFromFacebookLeads}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
    }
}
