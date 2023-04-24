using HW.Http;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.UserManagmentModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.CustomerModels;
using HW.NotificationViewModels;
using static HW.Utility.ClientsConstants;
using HW.TradesmanModels;
using System.Text.RegularExpressions;
using HW.IdentityViewModels;
using HW.PackagesAndPaymentsModels;
using HW.UserViewModels;
using HW.GatewayApi.AdminService;

namespace HW.GatewayApi.AdminService
{
    public interface IAdminJobServices
    {
        Task<JobsVm> GetAllJobsCount();

        Task<List<ActiveJobListVM>> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string startDate, string endDate, string city, string status, string jobdetailid,
            string fromDate, string toDate, string tradesmanName, string usertype, string location, bool athorize, string cSJobStatusId, string townId, string town);
        Task<List<DeletedJobListVM>> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string startDate, string endDate, string city, string status, string jobdetailid,
           string fromDate, string toDate, string tradesmanName, string usertype, string location, string cSJobStatusId, string town);
        Task<List<ActiveJobListVM>> GetActiveJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string startDate, string endDate, string city, string location);
        Task<List<InprogressJobList>> GetInprogressJobList(long pageNumber, long pageSize,
            string dataOrderBy, string customerName, string jobId, string jobDetailId, string startDate, string endDate, string city, string location);
        Task<JobQuotationDTO> GetJobDetailsList(long customerId);
        //Task<List<BidsDTO>> GetBidsDetailsByQuaId(long customerId);
        Task<List<ReciveBidsList>> GetReciveBids(long jobQoutationId);
        Task<ReciveBidDetails> ReciveBidDetails(long jobQoutationId);
        Task<List<Status>> JobStatusForDropdown();
        Task<List<Status>> GetCsJobStatusDropdown();
        Task<List<JobReportWithStatusCustomerVM>> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location);
        Task<List<JobQuotationDTO>> getPostedJobsWithDateLastDay(string startDate, string endDate);
        Task<List<BidsDTO>> getActiveBidsLastDay(string startDate, string endDate);


        Task<List<JobQuotationDTO>> GetPostedJobsForDynamicReport(string postedstartDate, string postedendDate, string endfromDate, string endtoDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId, string jobDetId, string userType);
        Task<List<BidsDTO>> GetBisForDynamicReport(string postedstartDate, string postedendDate, string endfromDate, string endtoDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId);
        Task<List<JobQuotation>> GetPostedJobsAddressList();
        Task<Response> DeleteJobWithJobQuotationId(long JobQuotationId, string actionPageName);
        Task<Response> ApproveJob(long jobQuotationId);
        Task<Response> JobAuthorizer(JobAuthorizerVM jobAuthorizerVM);
        Task<List<JobAuthorizerVM>> JobAuthorizerList();
        Task<List<JobAuthorizerVM>> AdminJobAuthorizerList();
        Task<Response> UpdateJobBudget(UpdateJobBudgetVM budgetVM);
        Task<Response> UpdateJobExtraCharges(UpdateJobBudgetVM updateJob);
        Task<Response> AssignJobToTradesman(AssignJobVM assignJobVM);
        Task<Response> ChangeJobStatus(int jqId, long bidId);

        Task<Response> GetBidCountByJobId(long tradesmanId, long jobQuotationId);
        Task<List<InprogressJobList>> GetUrgentJobsList();
        Task<List<EsclateRequestVM>> EscalateIssuesRequestList(EsclateRequestVM esclateRequestVM);
        Task<Response> AuthorizeEscalateIssueRequest(long escalateIssueId);
        Task<List<EsclateRequestVM>> AuthorizeEscalateIssuesList(EsclateRequestVM esclateRequestVM);
        Task<List<EscalateOptionVM>> GetEscalateOptionsList();
        Task<Response> InsertAndUpdateEscalateOption(EscalateOptionVM escalateOption);
        Task<List<InprogressJobList>> GetJobsListByCategory(InprogressJobList inprogressJobList);
        Task<List<MyQuotationsVM>> SpGetJobsByCustomerId(GetJobsParams getJobsParams);
    }
}



public class AdminJobServices : IAdminJobServices
{
    private readonly IHttpClientService httpClient;
    private readonly IExceptionService exc;
    private readonly ApiConfig apiConfig;

    public AdminJobServices(IHttpClientService httpClientService, IExceptionService exceptionService, ApiConfig apiConfig)
    {
        this.apiConfig = apiConfig;
        exc = exceptionService;
        httpClient = httpClientService;
    }


    public async Task<List<ActiveJobListVM>> GetActiveJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string startDate, string endDate, string city, string location)
    {
        List<ActiveJobListVM> ActiveJobList = new List<ActiveJobListVM>();

        try
        {
            string jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetActiveJobList}?pageNumber={pageNumber}&pageSize={pageSize}&dataOrderBy={dataOrderBy}&customerName={customerName}&jobId={jobId}&startDate={startDate}&endDate={endDate}&city={city}&location={location}");
            ActiveJobList = JsonConvert.DeserializeObject<List<ActiveJobListVM>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return ActiveJobList;
    }

    public async Task<List<MyQuotationsVM>> SpGetJobsByCustomerId(GetJobsParams getJobsParams)
    {
        try
        {
            var jsonRequestTest = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.SpGetJobsByCustomerId}", getJobsParams);
            return JsonConvert.DeserializeObject<List<MyQuotationsVM>>(jsonRequestTest);
        }
        catch (Exception ex)
        {
            return new List<MyQuotationsVM>();
        }
    }
    public async Task<List<ActiveJobListVM>> GetPendingJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId,
        string startDate, string endDate, string city, string status, string jobdetailid, string fromDate, string toDate, string tradesmanName, string usertype,
        string location, bool athorize, string cSJobStatusId, string townId, string town)
    {
        List<ActiveJobListVM> ActiveJobList = new List<ActiveJobListVM>();

        try
        {
            string jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetPendingJobList}?pageNumber={pageNumber}&pageSize={pageSize}&dataOrderBy={dataOrderBy}&customerName={customerName}&jobId={jobId}&startDate={startDate}&endDate={endDate}&city={city}&status={status}&jobdetailid={jobdetailid}&fromDate={fromDate}&toDate={toDate}&tradesmanName={tradesmanName}&usertype={usertype}&location={location}&athorize={athorize}&cSJobStatusId={cSJobStatusId}&townId={townId}&town={town}");
            ActiveJobList = JsonConvert.DeserializeObject<List<ActiveJobListVM>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return ActiveJobList;
    }

    public async Task<JobsVm> GetAllJobsCount()
    {
        JobsVm jobsVm = new JobsVm();

        try
        {
            string jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetAllJobsCount}");
            jobsVm = JsonConvert.DeserializeObject<JobsVm>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return jobsVm;
    }

    public async Task<List<InprogressJobList>> GetInprogressJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string jobDetailId, string startDate, string endDate, string city, string location)
    {
        List<InprogressJobList> jobsVm = new List<InprogressJobList>();

        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetInprogressJobList}?pageNumber={pageNumber}&pageSize={pageSize}&dataOrderBy={dataOrderBy}&customerName={customerName}&jobId={jobId}&jobDetailId={jobDetailId}&startDate={startDate}&endDate={endDate}&city={city}&location={location}");
            jobsVm = JsonConvert.DeserializeObject<List<InprogressJobList>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return jobsVm;
    }

    public async Task<JobQuotationDTO> GetJobDetailsList(long customerId)
    {
        JobQuotationDTO jobsVm = new JobQuotationDTO();

        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsList}?customerId={customerId}");
            jobsVm = JsonConvert.DeserializeObject<JobQuotationDTO>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return jobsVm;
    }
    //public async Task<List<BidsDTO>> GetBidsDetailsByQuaId(long customerId)
    //{
    //    List<BidsDTO> jobsVm = new List<BidsDTO>();

    //    try
    //    {
    //        var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidsDetailsByQuaId}?customerId={customerId}");
    //        jobsVm = JsonConvert.DeserializeObject<BidsDTO>(jobsJson);
    //    }
    //    catch (Exception ex)
    //    {
    //        exc.AddErrorLog(ex);
    //    }

    //    return jobsVm;
    //}

    public async Task<List<ReciveBidsList>> GetReciveBids(long jobQoutationId)
    {
        List<ReciveBidsList> jobsVm = new List<ReciveBidsList>();

        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetReciveBids}?jobQoutationId={jobQoutationId}");
            jobsVm = JsonConvert.DeserializeObject<List<ReciveBidsList>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return jobsVm;
    }

    public async Task<ReciveBidDetails> ReciveBidDetails(long jobQoutationId)
    {
        ReciveBidDetails jobsVm = new ReciveBidDetails();

        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetReciveBidDetails}?jobQoutationId={jobQoutationId}");
            jobsVm = JsonConvert.DeserializeObject<ReciveBidDetails>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return jobsVm;
    }
    public async Task<List<Status>> JobStatusForDropdown()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobStatusDropdown}");
            return JsonConvert.DeserializeObject<List<Status>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }


    }
    public async Task<List<Status>> GetCsJobStatusDropdown()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetCsJobStatusDropdown}");
            return JsonConvert.DeserializeObject<List<Status>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }


    }

    public async Task<List<JobReportWithStatusCustomerVM>> GetJobsForReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location)
    {
        string response = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsForReport}?startDate={startDate}&endDate={endDate}&customers={customer}&status={status}&city={city}&lastactive={lastactive}&location={location}");

        return JsonConvert.DeserializeObject<List<JobReportWithStatusCustomerVM>>(response);
    }
    public async Task<List<JobQuotationDTO>> getPostedJobsWithDateLastDay(string startDate, string endDate)
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GEtlatestJobsForOneDayReport}?StartDate={startDate}&EndDate={endDate}");
            return JsonConvert.DeserializeObject<List<JobQuotationDTO>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<BidsDTO>> getActiveBidsLastDay(string startDate, string endDate)
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetlatestBidsForOneDayReport}?StartDate={startDate}&EndDate={endDate}");
            return JsonConvert.DeserializeObject<List<BidsDTO>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<JobQuotationDTO>> GetPostedJobsForDynamicReport(string postedstartDate, string postedendDate, string endfromDate, string endtoDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId, string jobDetId, string userType)
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetPostedJobsForDynamicReport}?StartDate={postedstartDate}&EndDate={postedendDate}&FromDate={endfromDate}&ToDate={endtoDate}&customer={customer}&tradesman={tradesman}&status={status}&city={city}&lastActive={lastActive}&location={location}&jobId={jobId}&jobDetId={jobDetId}&userType={userType}");
            return JsonConvert.DeserializeObject<List<JobQuotationDTO>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }

    public async Task<List<BidsDTO>> GetBisForDynamicReport(string postedstartDate, string postedendDate, string endfromDate, string endtoDate, string customer, string tradesman, string status, string city, bool lastActive, string location, string jobId,string customerId)
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidsForDynamicReport}?StartDate={postedstartDate}&EndDate={postedendDate}&FromDate={endfromDate}&ToDate={endtoDate}&customer={customer}&tradesman={tradesman}&status={status}&city={city}&lastActive={lastActive}&location={location}&jobId={jobId}&customerId={customerId}");
            return JsonConvert.DeserializeObject<List<BidsDTO>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<JobQuotation>> GetPostedJobsAddressList()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetPostedJobsAddressList}");
            return JsonConvert.DeserializeObject<List<JobQuotation>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }

    public async Task<Response> DeleteJobWithJobQuotationId(long JobQuotationId, string actionPageName)
    {
        try
        {
            var response = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.DeleteJobByQuotationId}?JobQuotationId={JobQuotationId}&actionPageName={actionPageName}");
            return JsonConvert.DeserializeObject<Response>(response);

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }

    }

    public async Task<Response> ApproveJob(long JobQuotationId)
    {
        var res = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.ApproveJob}?JobQuotationId={JobQuotationId}");
        var response = JsonConvert.DeserializeObject<Response>(res);
        if (response.Status == ResponseStatus.OK)
        {
            JobQuotation jobDetails = JsonConvert.DeserializeObject<JobQuotation>(
                await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.JobDetailsByJQID}?jobQuotationId={JobQuotationId}")
                );
            if (jobDetails.CsjobStatusId == null || jobDetails.CsjobStatusId == 0)
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                            await httpClient.GetAsync($"{apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={jobDetails?.CreatedBy}")
                        );

                City city = JsonConvert.DeserializeObject<City>(
                    await httpClient.GetAsync($"{apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={customer.CityId}")
                );
                Skill skill = JsonConvert.DeserializeObject<Skill>(
                  await httpClient.GetAsync($"{apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillBySkillId}?skillId={jobDetails.SkillId}")
              );
                skill.Name = Regex.Replace(skill?.Name, CustomRegularExpressions.specialCharsPattern, "");

                string cityName = city?.Name;

                List<string> tradesmenList = JsonConvert.DeserializeObject<List<string>>(
                    await httpClient.GetAsync($"{apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmenListBySkillIdAndCityId}?_skillId={jobDetails.SkillId}&_cityName={cityName}")
                );
                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    SenderUserId = jobDetails?.CreatedBy,
                    Body = $"{customer.FirstName} {customer.LastName} posted a new Job {jobDetails?.WorkTitle}",
                    Title = NotificationTitles.NewJobPost,
                    TargetActivity = "Home",
                    To = $"'{skill?.Name}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                    TargetDatabase = TargetDatabase.Tradesman,
                    TradesmenList = tradesmenList,
                    IsRead = false
                };

                bool result = JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                );
            }


        }
        return response;
    }
    public async Task<Response> JobAuthorizer(JobAuthorizerVM jobAuthorizerVM)
    {

        try
        {
            var jobsJson = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizer}", jobAuthorizerVM);
            return JsonConvert.DeserializeObject<Response>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<JobAuthorizerVM>> JobAuthorizerList()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
            return JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<JobAuthorizerVM>> AdminJobAuthorizerList()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.AdminJobAuthorizerList}");
            return JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }

    public async Task<Response> UpdateJobBudget(UpdateJobBudgetVM budgetVM)
    {
        try
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobBudget}", budgetVM));
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<Response> UpdateJobExtraCharges(UpdateJobBudgetVM updateJob)
    {
        try
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobExtraCharges}", updateJob));
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<Response> AssignJobToTradesman(AssignJobVM assignJobVM)
    {
        Response response = new Response();
        try
        {
            response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.AssignJobToTradesman}", assignJobVM));
            if (response.Status == ResponseStatus.OK)
            {
                // Notification to customer 

                JobQuotation jobQuotation = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={assignJobVM.JobQuotationId}", ""));
                Tradesman tradesmanDetail = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByTradesmanId}?tradesmanId={assignJobVM.TradesmanId}", ""));
                Customer customer = JsonConvert.DeserializeObject<Customer>(await httpClient.GetAsync($"{apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={assignJobVM.CustomerId}", ""));
                Bids bids = JsonConvert.DeserializeObject<Bids>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidByJQID}?id={assignJobVM.JobQuotationId}", ""));
                Response identityResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}"));
                UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(identityResponse?.ResultData?.ToString());
                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    Title = ClientsConstants.NotificationTitles.NewBid,
                    Body = $"You have received new bid on {jobQuotation.WorkTitle}",
                    To = $"{userVM?.FirebaseClientId}",
                    TargetActivity = "BidDetails",
                    SenderUserId = customer?.UserId,
                    //SenderEntityId = Convert.ToString(bids.BidsId),
                    SenderEntityId = assignJobVM.JobQuotationId.ToString(),
                    TargetDatabase = TargetDatabase.Customer,
                    TragetUserId = userVM?.Id,
                    IsRead = false
                };

                bool notificationResult = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));

                // Notification to Tradesman 

                Response tradesmanResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={tradesmanDetail.UserId}"));
                if (tradesmanResponse.Status == ResponseStatus.OK)
                {
                    UserRegisterVM TradesmanUserVM = JsonConvert.DeserializeObject<UserRegisterVM>(tradesmanResponse.ResultData.ToString());

                    //long jobDetailId = JsonConvert.DeserializeObject<long>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailIdByQuotationId}?quotationId={assignJobVM.JobQuotationId}", ""));

                    PostNotificationVM postNotificationTradesmanVM = new PostNotificationVM()
                    {
                        Body = $"New job is assigned to you. Title : {jobQuotation.WorkTitle}",
                        Title = NotificationTitles.NewMessage,
                        To = $"{TradesmanUserVM?.FirebaseClientId}",
                        SenderEntityId = assignJobVM.JobQuotationId.ToString(),
                        TargetActivity = "Home",
                        SenderUserId = customer.UserId,
                        TargetDatabase = TargetDatabase.Tradesman,
                        TragetUserId = TradesmanUserVM.Id,
                        IsRead = false
                    };

                    bool tradesmanNotification = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationTradesmanVM));
                }


            }
            return response;

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return response;
        }
    }
    public async Task<Response> ChangeJobStatus(int jqId, long bidId)
    {
        Response response = new Response();
        try
        {
            if (jqId > 0)
            {
                Response res = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.ChangeJobStatus}", jqId));

                /////// Payment return transactions ///////////
                JobQuotation jobQuotation = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={jqId}", ""));

                ///// check and get wallet value
                Response walletResponse = new Response();
                var responseJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetWalletValueByBidId}?customerId={jobQuotation.CustomerId}&bidId={bidId}", "");
                walletResponse = JsonConvert.DeserializeObject<Response>(responseJson);

                var walletValue = walletResponse.ResultData?.ToString();
                decimal wValue = Convert.ToInt64(walletValue);

                ///// check and get jazzcash value
                Response jazzCashResponse = new Response();
                var jazzCashResponseJson = await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetJazzCashByBidId}?customerId={jobQuotation.CustomerId}&bidId={bidId}", "");
                jazzCashResponse = JsonConvert.DeserializeObject<Response>(jazzCashResponseJson);

                var jazzcashValue = jazzCashResponse.ResultData?.ToString();
                decimal jcValue = Convert.ToInt64(jazzcashValue);

                ////// check and get promotions
                Response promotionResponce = new Response();
                PromotionRedemptions getRecordPromotion = new PromotionRedemptions();
                getRecordPromotion = JsonConvert.DeserializeObject<PromotionRedemptions>
                                    (await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetProRecordByJQID}?redeemPromotionByJQID={jqId}"));

                Redemptions getRecordVoucher = new Redemptions();
                getRecordVoucher = JsonConvert.DeserializeObject<Redemptions>
                                    (await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetRedeemRecordByJQID}?redeemVoucherByJQID={jqId}"));

                if (getRecordPromotion != null || getRecordVoucher != null)
                {
                    promotionResponce = JsonConvert.DeserializeObject<Response>
                                    (await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.DeletePromotionEntry}?jqId={jqId}"));

                }
                decimal totalAmount = wValue + jcValue;
                if (totalAmount > 0)
                {
                    ///// Customer Return Entry ///////////////
                    Response leagerresponse = new Response();
                    SubAccount subAccountCustomer = JsonConvert.DeserializeObject<HW.PackagesAndPaymentsModels.SubAccount>(
                            await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetSubAccount}?customerId={jobQuotation.CustomerId}", ""));
                    HW.PackagesAndPaymentsModels.LeadgerTransection SelfleadgerTransction = new HW.PackagesAndPaymentsModels.LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.AccountReceiveables),
                        SubAccountId = subAccountCustomer.SubAccountId,
                        Debit = totalAmount,
                        Credit = 0,
                        Active = true,
                        RefCustomerSubAccountId = jobQuotation.CustomerId,
                        ReffrenceDocumentNo = jqId,
                        ReffrenceDocumentType = "Return Payment",
                        CreatedOn = DateTime.Now,
                        CreatedBy = subAccountCustomer.CreatedBy
                    };

                    leagerresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", SelfleadgerTransction)
                    );

                    ///////// Hoomwork debited entry ////////////////

                    SubAccount sub = JsonConvert.DeserializeObject<SubAccount>(
                                await httpClient.GetAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetHoomWorkSubAccount}", "")
                            );

                    LeadgerTransection leadgerTransctn = new LeadgerTransection()
                    {
                        AccountId = Convert.ToInt64(HW.Utility.AccountType.Assets),
                        SubAccountId = sub.SubAccountId,
                        Debit = 0,
                        Credit = totalAmount,
                        Active = true,
                        RefCustomerSubAccountId = jobQuotation.CustomerId,
                        ReffrenceDocumentNo = jqId,
                        ReffrenceDocumentType = "Return Payment",
                        CreatedOn = DateTime.Now,
                        CreatedBy = subAccountCustomer.CreatedBy
                    };

                    leagerresponse = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.AddLeadgerTransection}", leadgerTransctn)
                    );

                }

            }
            return response;

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return response;
        }
    }

    public async Task<Response> GetBidCountByJobId(long tradesmanId, long jobQuotationId)
    {
        try
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidCountByJobId}?tradesmanId={tradesmanId}&jobQuotationId={jobQuotationId}"));

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return new Response();
        }
    }
    public async Task<List<InprogressJobList>> GetUrgentJobsList()
    {
        try
        {
            var jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetUrgentJobsList}");
            return JsonConvert.DeserializeObject<List<InprogressJobList>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<InprogressJobList>> GetJobsListByCategory(InprogressJobList inprogressJobList)
    {
        try
        {
            var jobsJson = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsListByCategory}", inprogressJobList);
            return JsonConvert.DeserializeObject<List<InprogressJobList>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }

    public async Task<List<DeletedJobListVM>> GetDeletedJobList(long pageNumber, long pageSize, string dataOrderBy, string customerName, string jobId, string startDate, string endDate, string city, string status, string jobdetailid, string fromDate, string toDate, string tradesmanName, string usertype, string location, string cSJobStatusId, string town)
    {
        List<DeletedJobListVM> ActiveJobList = new List<DeletedJobListVM>();

        try
        {
            string jobsJson = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetDeletedJobList}?pageNumber={pageNumber}&pageSize={pageSize}&dataOrderBy={dataOrderBy}&customerName={customerName}&jobId={jobId}&startDate={startDate}&endDate={endDate}&city={city}&status={status}&jobdetailid={jobdetailid}&fromDate={fromDate}&toDate={toDate}&tradesmanName={tradesmanName}&usertype={usertype}&location={location}&cSJobStatusId={cSJobStatusId}&town={town}");
            ActiveJobList = JsonConvert.DeserializeObject<List<DeletedJobListVM>>(jobsJson);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }

        return ActiveJobList;
    }

    public async Task<List<EsclateRequestVM>> EscalateIssuesRequestList(EsclateRequestVM esclateRequestVM)
    {

        try
        {
            string response = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.EscalateIssuesRequestList}", esclateRequestVM);
            return JsonConvert.DeserializeObject<List<EsclateRequestVM>>(response);

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }

    public async Task<Response> AuthorizeEscalateIssueRequest(long escalateIssueId)
    {
        Response response = new Response();
        try
        {
            response = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.AuthorizeEscalateIssueRequest}?escalateIssueId={escalateIssueId}"));

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
        }
        return response;
    }

    public async Task<List<EsclateRequestVM>> AuthorizeEscalateIssuesList(EsclateRequestVM esclateRequestVM)
    {
        try
        {
            string response = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.AuthorizeEscalateIssuesList}", esclateRequestVM);
            return JsonConvert.DeserializeObject<List<EsclateRequestVM>>(response);

        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return null;
        }
    }
    public async Task<List<EscalateOptionVM>> GetEscalateOptionsList()
    {
        try 
        {

            string response = await httpClient.GetAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.GetEscalateOptionsList}");
            return JsonConvert.DeserializeObject<List<EscalateOptionVM>>(response);
        }
        catch(Exception ex)
        {
            exc.AddErrorLog(ex);
            return new List<EscalateOptionVM>();
        }
    }

    public async Task<Response> InsertAndUpdateEscalateOption(EscalateOptionVM escalateOption)
    {
        Response response = new Response();
        try
        {
             string result = await httpClient.PostAsync($"{apiConfig.JobApiUrl}{ApiRoutes.Job.InsertAndUpdateEscalateOption}",escalateOption);
            return JsonConvert.DeserializeObject<Response>(result);
        }
        catch (Exception ex)
        {
            exc.AddErrorLog(ex);
            return response;
        }
    }
}
