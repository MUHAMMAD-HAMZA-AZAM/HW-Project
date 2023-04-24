using Hw.EmailViewModel;
using HW.AudioModels;
using HW.CallModels;
using HW.CommunicationModels;
using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.JobModels;
using HW.NotificationViewModels;
using HW.PackagesAndPaymentsModels;
using HW.PackagesAndPaymentsViewModels;
using HW.PaymentModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.Utility;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;
using PaymentMethod = HW.PackagesAndPaymentsModels.PaymentMethod;
using Response = HW.Utility.Response;
using SkillSet = HW.TradesmanModels.SkillSet;
using TradesmanJobReceipts = HW.PackagesAndPaymentsModels.TradesmanJobReceipts;

namespace HW.GatewayApi.Services
{
    public interface ITradesmanService
    {
        Task<string> GetAllSkills();
        Task<List<BidVM>> GetActiveBids(long tradesmanId, int bidsStatusId);
        Task<List<BidVM>> GetDeclinedBids(long tradesmanId, int bidsStatusId);
        Task<List<MediaImagesVM>> GetJobBidImages(long jobQuotaionId);
        Task<Response> GetJobsDetail(long tradesmanId, int jobStatusId);
        Task<JobQuotationEditBidVM> GetJobQuotationsEditBid(long jobQuotationId);
        Task<CallInfoVM> GetCallInfo(long tradesmanId, long customerId, bool todaysRecordOnly = false);
        Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(long tradesmanid, int pageNumber);
        Task<List<InvoiceVM>> GetInvoiceMembership(long tradesmanId);
        Task<List<InvoiceVM>> GetInvoiceJobReceipts(long tradesmanId);
        Task<List<IdValueVM>> GetTradesmanSkillsByParentId(long parentId);
        Task<CompletedJobDetailVM> GetCompletedJob(long jobDetailId, long tradesmanId);
        Task<Response> SubmitBid(EditBidVM editBidVM);
        Task<List<ExpiryJobNotificationVM>> JobEndReminderNotification(List<ExpiryJobNotificationVM> postAdVM);
        Task<Response> SubmitBidsVoice(EditBidVM editBidVM, string resultData);
        Task<List<UserViewModels.CallHistoryVM>> GetTradesmanCallLogs(long tradesmanId);
        Task<bool> DeleteCallLogs(List<long> selectedCallLogIds);
        Task<PersonalDetailsVM> GetPersonalDetails(long tradesmanId);
        Task<bool> UpdatePersonalDetails(long id, PersonalDetailVM personalDetailVM);
        Task<TradesmanProfileVM> GetTradesmanProfile(long tradesmanIds, bool isActive);
        Task<List<LocalProfessionVM>> GetLocalProfessionalImages();
        Task<RateTradesmanVM> RateTradesmanByJobQuotationId(long jobDetailId);
        Task<List<IdValueVM>> GetTradesmanSkills();
        Task<TmBusinessDetailVM> GetBusinessDetail(long tradsmanId);
        Task<UserProfileVM> GetTradesmanByUserId(string userId);
        Task<List<IdValueVM>> GetSubSkillBySkillId(long tradesmanSkillId);
        Task<List<IdValuePriceVM>> GetSubSkillsBySkillId(long tradesmanSkillId);
        Task<IdValuePriceVM> GetSubSkillbySubSkillId(long subSkillId);
        Task<List<IdValueVM>> GetSubSkill();
        Task<bool> CheckFeedBackStatus(long jobDetailId);
        Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb(long tradsmanId);
        Task<Response> Login(LoginVM model);
        Task<List<TradesmanReportbySkillVM>> GetLAllTradesmanbyCategoryReport(DateTime StartDate, DateTime EndDate, string[] skills);
        Task<List<TradesmanReportbySkillVM>> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId);
        Task<List<Tradesman>> GetTradesmanCompletedJobsFeeback();
        Task<Response> GetBusinessDetailsStatus(string id);
        Task<List<Skill>> GetSkillList(long skill);
        Task<Skill> GetSkillTagsBySkillId(long skillId);
        Task<List<TradesmanViewModels.MetaTagsVM>> GetCommonMetaTags();
        Task<SubSkill> GetSubSkillTagsById(long subSkillId);
        Task<SubSkill> GetSubSkillById(long subSkillId);
        Task<List<SubSkill>> GetSubSkillTagsBySkillId(long SkillId);
        Task<bool> UpdateTradesmanPublicId(long tradesmanId, string publicId);
        Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city);
        Task<List<SubSkillWithSkillVM>> GetSubSkillsWithSkill();
    }

    public class TradesmanService : ITradesmanService
    {
        private readonly IHttpClientService httpClient;
        private readonly ApiConfig _apiConfig;
        private readonly ClientCredentials clientCred;
        private readonly IExceptionService Exc;
        public TradesmanService(IHttpClientService httpClient, ClientCredentials clientCred, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.clientCred = clientCred;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<string> GetAllSkills()
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetAllJobs}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
        }

        public async Task<List<ExpiryJobNotificationVM>> JobEndReminderNotification(List<ExpiryJobNotificationVM> postAdVM)
        {
            if (postAdVM != null)
            {
                foreach (var item in postAdVM)
                {
                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        SenderUserId = item.UserId,
                        Body = $"Your Job of {item.WorkTitle} near to end time let it finished to get more job",
                        Title = NotificationTitles.ExpiredAd,
                        TargetActivity = "Tradesman",
                        To = item.FirebaseClientId,
                        TragetUserId = item.UserId,
                        IsRead = false,
                        TargetDatabase = TargetDatabase.Supplier
                    };

                    bool result = JsonConvert.DeserializeObject<bool>
                        (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                    );

                }
                return postAdVM;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<BidVM>> GetActiveBids(long tradesmanId, int bidsdStatusId)
        {

            List<BidVM> activeBidsVM = new List<BidVM>();
            try
            {
                List<JobImages> bidImages = new List<JobImages>();

                List<Bids> activeBids = JsonConvert.DeserializeObject<List<Bids>>
                    (await httpClient.PostAsync<string>($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetActiveBids}?tradesmanId={tradesmanId}&bidsStatusId={bidsdStatusId}", ""));

                List<JobQuotation> jobs = JsonConvert.DeserializeObject<List<JobQuotation>>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsByIds}", activeBids.Select(j => j.JobQuotationId).ToList()));

                bidImages = JsonConvert.DeserializeObject<List<JobImages>>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesByIds}", jobs.Select(x => x.JobQuotationId).ToList()));

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>
                    (await httpClient.PostAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Customer.GetCustomerByIdList}", activeBids.Select(j => j.CustomerId).ToList()));

                activeBidsVM = activeBids?.Select(x => new BidVM
                {
                    JobQuotationid = x?.JobQuotationId ?? 0,
                    Budget = System.Math.Round(x.Amount, 2),
                    JobDate = x?.CreatedOn.ToString("dd MMM yyyy"),
                    // JobImage = bidImages.FirstOrDefault(img => img.JobQuotationId == x.JobQuotationId && img.IsMain)?.BidImage,
                    WorkTitle = jobs.FirstOrDefault(j => j.JobQuotationId == x.JobQuotationId).WorkTitle,
                    CustomerName = $"{ customers.FirstOrDefault(c => c.CustomerId == x.CustomerId)?.FirstName} { customers.FirstOrDefault(c => c.CustomerId == x.CustomerId)?.LastName}"

                }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return activeBidsVM;
        }

        public async Task<List<BidVM>> GetDeclinedBids(long tradesmanId, int bidsdStatusId)
        {
            List<BidVM> DeclinedBidsVM = new List<BidVM>();
            try
            {
                List<Bids> activeBids = JsonConvert.DeserializeObject<List<Bids>>
                (await httpClient.PostAsync<string>($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetActiveBids}?tradesmanId={tradesmanId}&bidsStatusId={bidsdStatusId}", ""));

                List<JobQuotation> jobs = JsonConvert.DeserializeObject<List<JobQuotation>>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsByIds}", activeBids.Select(j => j.JobQuotationId).ToList()));

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>
                    (await httpClient.PostAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Customer.GetCustomerByIdList}", activeBids.Select(j => j.CustomerId).ToList()));


                DeclinedBidsVM = activeBids?.Select(x => new BidVM
                {
                    JobQuotationid = x?.JobQuotationId ?? 0,
                    Budget = System.Math.Round(x.Amount, 2),
                    JobDate = x?.CreatedOn.ToString("dd MMM yyyy"),
                    WorkTitle = jobs.FirstOrDefault(j => j.JobQuotationId == x.JobQuotationId).WorkTitle,
                    CustomerName = $"{ customers.FirstOrDefault(c => c.CustomerId == x.CustomerId)?.FirstName} { customers.FirstOrDefault(c => c.CustomerId == x.CustomerId)?.LastName}"

                }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return DeclinedBidsVM;
        }

        public async Task<Response> GetJobsDetail(long tradesmanId, int jobStatusId)
        {
            Response response = new Response();
            try
            {
                List<JobDetailVM> jobDetailVM = new List<JobDetailVM>();
                List<TradesmanFeedback> tradesmanFeedback = new List<TradesmanFeedback>();
                List<JobImages> bidImages = new List<JobImages>();
                string jobJosn = await httpClient.PostAsync<string>($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsDetail}?tradesmanId={tradesmanId}&jobStatusId={jobStatusId}", "");
                List<JobDetail> jobsList = JsonConvert.DeserializeObject<List<JobDetail>>(jobJosn);

                List<long> jobQuotationsIds = jobsList.Select(j => j.JobQuotationId).ToList();
                string activeJobsJson = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsByIds}", jobQuotationsIds);
                List<JobQuotation> jobs = JsonConvert.DeserializeObject<List<JobQuotation>>(activeJobsJson);


                string imagesJson = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImagesByIds}", jobQuotationsIds);
                bidImages = JsonConvert.DeserializeObject<List<JobImages>>(imagesJson);

                if (jobStatusId == 3)
                {
                    string feedbackjson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanFeedBack}?tradesmanId={tradesmanId}&jobDetailId={jobsList.FirstOrDefault(f => f.TradesmanId == tradesmanId).JobDetailId}");
                    tradesmanFeedback = JsonConvert.DeserializeObject<List<TradesmanFeedback>>(feedbackjson);
                }
                List<long> customerIds = jobsList.Select(j => j.CustomerId).ToList();
                string customersJson = await httpClient.PostAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Customer.GetByIds}", customerIds);
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);
                if (jobStatusId == 1)
                {
                    foreach (JobDetail jobList in jobsList)
                    {
                        JobDetailVM jobDetailsVM = new JobDetailVM()
                        {
                            JobDetailId = jobList?.JobDetailId ?? 0,
                            JobQuotationId = jobList?.JobQuotationId ?? 0,

                            JobStartedDate = jobList?.CreatedOn.ToString("dd MMM yyyy"),

                            // JobImage = bidImages?.FirstOrDefault(img => img.JobQuotationId == jobList.JobQuotationId)?.BidImage,
                        };

                        Customer customer = customers?.FirstOrDefault(c => c.CustomerId == jobList.CustomerId);
                        jobDetailsVM.WorkTitle = jobsList?.FirstOrDefault(j => j.JobQuotationId == jobList.JobQuotationId)?.Title;
                        jobDetailsVM.CustomerName = $"{customer?.FirstName} {customer?.LastName}";

                        jobDetailVM.Add(jobDetailsVM);
                    }
                }
                else
                {
                    foreach (JobDetail jobList in jobsList)
                    {
                        JobDetailVM jobDetailsVM = new JobDetailVM()
                        {
                            JobDetailId = jobList?.JobDetailId ?? 0,
                            JobStartedDate = jobList?.EndDate.ToString("dd MMM yyyy"),
                            //JobImage = bidImages?.FirstOrDefault(img => img.JobQuotationId == jobList.JobQuotationId)?.BidImage,
                        };

                        jobDetailsVM.Rating = tradesmanFeedback?.FirstOrDefault(r => r.JobDetailId == jobList.JobDetailId)?.OverallRating ?? 0;
                        Customer customer = customers?.FirstOrDefault(c => c.CustomerId == jobList.CustomerId);

                        jobDetailsVM.WorkTitle = jobsList?.FirstOrDefault(j => j.JobQuotationId == jobList.JobQuotationId)?.Title;
                        jobDetailsVM.CustomerName = $"{customer?.FirstName} {customer?.LastName}";

                        jobDetailVM.Add(jobDetailsVM);
                    }
                }
                if (jobDetailVM.Count > 0)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully";
                    response.ResultData = jobDetailVM;

                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error";
                    response.ResultData = jobDetailVM;
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<CallInfoVM> GetCallInfo(long tradesmanId, long customerId, bool todaysRecordOnly = false)
        {
            try
            {
                string customerJson = await httpClient.GetAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", "");
                string callLogsJson = await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetTradesmanCallLogs}?tradesmanId={tradesmanId}&customerId={customerId}&todaysRecordOnly={todaysRecordOnly}", "");
                string customerImageJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customerId}", "");
                string allCallTypesJson = await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}", "");

                Customer customer = JsonConvert.DeserializeObject<Customer>(customerJson);
                List<TradesmanCallLog> callLogs = JsonConvert.DeserializeObject<List<TradesmanCallLog>>(callLogsJson);
                CustomerProfileImage customerImage = JsonConvert.DeserializeObject<CustomerProfileImage>(customerImageJson);
                List<CallType> allCallTypes = JsonConvert.DeserializeObject<List<CallType>>(allCallTypesJson);

                CallInfoVM callInfoVM = new CallInfoVM()
                {
                    CustomerID = customer.CustomerId,
                    CustomerImage = customerImage?.ProfileImage,
                    CustomerName = customer.FirstName + " " + customer.LastName,
                    //Lat = customer.Lat.HasValue ? customer.Lat.Value : 0,
                    //Long = customer.Long.HasValue ? customer.Long.Value : 0,

                    CallLogs = callLogs.Select(c => new HW.TradesmanViewModels.CallLogVM
                    {
                        CallDuration = c.Duration,
                        CallType = allCallTypes.Where(x => x.CallTypeId == c.CallType).FirstOrDefault().Name,
                    }).ToList()
                };
                return callInfoVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new CallInfoVM();
            }

        }

        public async Task<List<InvoiceVM>> GetInvoiceJobReceipts(long tradesmanId)
        {
            try
            {
                List<TradesmanJobReceiptsVM> tradesmanJobReceipts = JsonConvert.DeserializeObject<List<TradesmanJobReceiptsVM>>
                (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetJobReceiptsByTradesmanId}?tradesmanId={tradesmanId}", ""));

                List<InvoiceVM> Invoice_JobReceipts = new List<InvoiceVM>();
                Invoice_JobReceipts = tradesmanJobReceipts?.Select(m => new InvoiceVM
                {
                    JobQuotationId = m.JobQuotationId,
                    PaymentMonth = m.PaymentDate.ToString("dd MMM yyyy"),
                    Amount = m.Amount,
                    PaymentStatus = m.PaymentStatus ?? 0,
                    ServiceCharges = m.ServiceCharges ?? 0,
                    OtherCharges = m.OtherCharges ?? 0,
                    AdditionalCharges = m.AdditionalCharges ?? 0,
                    DiscountedAmount = m.DiscountedAmount ?? 0,
                    Commission = m.Commission ?? 0,
                    NetPayableToTradesman = m.NetPayableToTradesman ?? 0,
                    PayableAmount = m.PayableAmount ?? 0,
                    PaidViaWallet = m.PaidViaWallet ?? 0,
                    TradesmanInitialBudget = m.TradesmanInitialBudget
                    
                }).ToList();

                return Invoice_JobReceipts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InvoiceVM>();
            }

        }

        public async Task<List<InvoiceVM>> GetInvoiceMembership(long tradesmanId)
        {
            try
            {
                List<TradesmanMembership> tradesmanMembership = JsonConvert.DeserializeObject<List<TradesmanMembership>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetMembershipListById}?tradesmanId={tradesmanId}", ""));
                List<PaymentMethod> paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(await httpClient.GetAsync($"{_apiConfig.PaymentApiUrl}{ApiRoutes.Payment.GetAllPaymentMethods}", ""));

                List<InvoiceVM> Invoice_MemberShip = new List<InvoiceVM>();
                Invoice_MemberShip = tradesmanMembership.Select(m => new InvoiceVM
                {
                    PaymentMonth = m.PaymentMonth.ToString("MMM yyyy"),
                    Amount = m.Amount,
                    PaymentMode = paymentMethods.Where(x => x.PaymentMethodId == m.PaymentMethodId).FirstOrDefault().Name,
                    //PaymentStatus = m.IsPaid ? "Paid" : "Due Now"
                }).ToList();

                return Invoice_MemberShip;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InvoiceVM>();
            }

        }

        public async Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(long tradesmanId, int pageNumber)
        {
            List<JobLeadsVM> jobLeadsVMs = new List<JobLeadsVM>();
            try
            {
                List<long> tradesmanSkillId = JsonConvert.DeserializeObject<List<long>>(
                await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillIds}?tradesmanId={tradesmanId}", ""));

                string getTradesmanCity = await httpClient.GetAsync($"{ _apiConfig.TradesmanApiUrl}{ ApiRoutes.Tradesman.GetPersonalDetails}?tradesmanId={tradesmanId}", "");
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(getTradesmanCity);

                string tradesmanCityID = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityIdByName}?cityName={tradesman.City}", "");
                long cityId = JsonConvert.DeserializeObject<long>(tradesmanCityID);

                SkillproductIdVM skillproductIdVM = new SkillproductIdVM() { skillIds = tradesmanSkillId, cityId = cityId, pageNumber = pageNumber };

                string jobQuotationsJson = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsBySkillId}", skillproductIdVM);
                List<JobQuotation> jobQuotations = JsonConvert.DeserializeObject<List<JobQuotation>>(jobQuotationsJson);

                List<long> jobQuotationIds = jobQuotations.Select(j => j.JobQuotationId).ToList();

                string bidCountJson = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationBidsByJobQuotatationIds}", jobQuotationIds);
                List<Bids> bids = JsonConvert.DeserializeObject<List<Bids>>(bidCountJson);

                string jobImagesJson = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobQuotationImages}", jobQuotationIds);
                List<JobImages> jobImages = JsonConvert.DeserializeObject<List<JobImages>>(jobImagesJson);

                List<JobAddress> jobAddresses = JsonConvert.DeserializeObject<List<JobAddress>>
                    (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddressByJobQuotationIds}", jobQuotationIds));

                List<City> Cities = JsonConvert.DeserializeObject<List<City>>
                   (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityList}"));

                foreach (JobQuotation jobQuotation in jobQuotations)
                {
                    JobAddress address = jobAddresses.FirstOrDefault(j => j.JobQuotationId == jobQuotation.JobQuotationId);
                    JobLeadsVM jobLead = new JobLeadsVM()
                    {
                        Budget = jobQuotation.WorkBudget.HasValue ? Math.Round(jobQuotation.WorkBudget.Value, 2) : 0,
                        JobTitle = jobQuotation.WorkTitle,
                        PostedOn = jobQuotation.CreatedOn,
                        JobQuotesId = jobQuotation.JobQuotationId,
                        //JobImage = jobImages.FirstOrDefault(i => i.JobQuotationId == jobQuotation.JobQuotationId && i.IsMain)?.BidImage,
                        Address = $"{address?.Area}, {Cities?.FirstOrDefault(x => x.CityId == address.CityId)?.Name}"
                    };

                    int bidCountObj = bids.Where(b => b.JobQuotationId == jobQuotation.JobQuotationId).ToList().Count();
                    jobLead.BidCount = bidCountObj;

                    bool isBidExists = bids.FirstOrDefault(x => x.JobQuotationId == jobQuotation.JobQuotationId && x.TradesmanId == tradesmanId) != null ? true : false;

                    if (jobLead.BidCount <= jobQuotation.DesiredBids && !isBidExists)
                        jobLeadsVMs.Add(jobLead);
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return jobLeadsVMs;
        }

        public async Task<CompletedJobDetailVM> GetCompletedJob(long jobDetailId, long tradesmanId)
        {
            try
            {
                string jobJosn;
                jobJosn = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetCompletedJobById}?tradesmanId={tradesmanId}&jobDetailId={jobDetailId}", "");
                if (string.IsNullOrEmpty(jobJosn))
                {
                    jobJosn = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetAJobByJobDeatilId}?tradesmanId={tradesmanId}&jobDetailId={jobDetailId}", "");

                }
                JobDetail completedJob = JsonConvert.DeserializeObject<JobDetail>(jobJosn);
                string feedbackjson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFeedBack}?tradesmanId={tradesmanId}&jobDetailId={completedJob.JobDetailId}");
                TradesmanFeedback tradesmanFeedback = JsonConvert.DeserializeObject<TradesmanFeedback>(feedbackjson);
                long customerId = completedJob.CustomerId;
                string customersJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", "");
                Customer customer = JsonConvert.DeserializeObject<Customer>(customersJson);
                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={completedJob.JobQuotationId}", ""));
                City city = JsonConvert.DeserializeObject<City>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}"));

                CompletedJobDetailVM completedJobDetailVM = new CompletedJobDetailVM
                {
                    LatLong = jobAddress?.GpsCoordinates,
                    JobDetailId = completedJob?.JobDetailId ?? 0,
                    CustomerName = $"{customer?.FirstName} {customer?.LastName}",
                    customerId = customer.CustomerId,
                    jobQuotationId = completedJob.JobQuotationId,
                    JobTitle = completedJob?.Title,
                    JobStartedDate = completedJob.StartDate,
                    JobFinishDate = completedJob.EndDate,
                    JobAddress = $"{jobAddress.StreetAddress.Trim()} {jobAddress.Area.Trim()}, {city.Name}",
                    MapAddress = $"{jobAddress.StreetAddress.Trim()} {jobAddress.Area.Trim()}, {city.Name}",
                    Rating = tradesmanFeedback?.OverallRating ?? 0,
                    Comment = tradesmanFeedback?.Comments,
                    Payment = completedJob?.TradesmanBudget ?? 0
                };

                return completedJobDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new CompletedJobDetailVM();
            }

        }

        public async Task<JobQuotationEditBidVM> GetJobQuotationsEditBid(long jobQuotationId)
        {
            JobQuotationEditBidVM jobs = new JobQuotationEditBidVM();
            try
            {
                string bidsjosn = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotations}?jobQuotationId={jobQuotationId}");
                Bids job = JsonConvert.DeserializeObject<Bids>(bidsjosn);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            //var customerjosn = await httpClient.GetAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Job.GetById}?id={job.CustomerId}");
            //Customer customer = JsonConvert.DeserializeObject<Customer>(customerjosn);
            return jobs;
        }

        public async Task<Response> SubmitBid(EditBidVM editBidVM)
        {
            //int bidCount = 0;
            Response response = new Response();
            try
            {

                var bidCount = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidCountsOnJob}?jobQuotationId={editBidVM.JobQuotationId}");
                //List<JobLeadsVM> jobLeadsVM = new List<JobLeadsVM>(); 
                //string jobLeads = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationsBySkill}?tradesmanId={editBidVM.TradesmanId}&pageNUmber={1}&pageSize={20}");
                //jobLeadsVM = JsonConvert.DeserializeObject<List<JobLeadsVM>>(jobLeads);
                //if (!string.IsNullOrEmpty(jobLeads))
                //{
                //    bidCount = jobLeadsVM.Where(a => a.JobQuotesId == editBidVM.JobQuotationId).Select(a => a.BidCount).FirstOrDefault();
                //}
                string bidsNo = "";
                if (bidCount == "1") { bidsNo = "2nd"; }
                else if (bidCount == "2") { bidsNo = "3rd"; }
                else { bidsNo = "1st"; }

                //if (!string.IsNullOrWhiteSpace(editBidVM?.Audio?.Base64String))
                //{
                //    var checkFormatList = editBidVM.Audio.Base64String.Split(',');

                //    if (checkFormatList[0] == "data:audio/wav;base64")
                //    {
                //        string convert = editBidVM.Audio.Base64String.Replace("data:audio/wav;base64,", String.Empty);
                //        editBidVM.Audio.AudioContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                //    }
                //}



                Bids bids = new Bids();
                if (editBidVM.TradesmanId > 0 && editBidVM.CustomerId > 0)
                {
                    if (editBidVM.BidsId > 0)
                    {
                        bids = JsonConvert.DeserializeObject<Bids>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidById}?id={editBidVM.BidsId}"));
                        bids.Amount = editBidVM.Amount;
                        bids.Comments = editBidVM.Comments;
                        bids.ModifiedOn = DateTime.Now;
                        bids.ModifiedBy = editBidVM.CreatedBy;
                    }
                    else
                    {
                        bids = new Bids()
                        {
                            JobQuotationId = editBidVM.JobQuotationId,
                            TradesmanId = editBidVM.TradesmanId,
                            CustomerId = editBidVM.CustomerId,
                            Comments = editBidVM.Comments,
                            Amount = Math.Round(editBidVM.Amount, 2),
                            IsSelected = false,
                            StatusId = editBidVM.StatusId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = editBidVM.CreatedBy,
                        };
                    }

                    Response bidResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SubmitBid}", bids));

                    if (bidResponse.Status == ResponseStatus.OK)
                    {
                        //if (!string.IsNullOrWhiteSpace(editBidVM?.Audio?.FileName) && editBidVM?.Audio?.AudioContent != null)
                        //{
                        //    BidAudio audio = new BidAudio()
                        //    {
                        //        Audio = editBidVM?.Audio?.AudioContent,
                        //        FileName = editBidVM.Audio.FileName,
                        //        BidId = Convert.ToInt64(bidResponse?.ResultData),
                        //        CreatedOn = Convert.ToDateTime(DateTime.Now),
                        //        CreatedBy = editBidVM.CreatedBy
                        //    };

                        //    string audios = await httpClient.PostAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.SaveBidAudio}", audio);
                        //    object json = JsonConvert.DeserializeObject(audios);
                        //}

                        Customer customer = JsonConvert.DeserializeObject<Customer>
                           (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={editBidVM.CustomerId}")
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
                                    Title = (editBidVM?.BidsId > 0) ? ClientsConstants.NotificationTitles.BidUpdated : ClientsConstants.NotificationTitles.NewBid,
                                    Body = (editBidVM?.BidsId > 0) ? $"Bid has been updated for job {editBidVM.JobTitle}" : $"You have received {bidsNo} bid on {editBidVM.JobTitle}",
                                    To = $"{userVM?.FirebaseClientId}",
                                    TargetActivity = "BidDetails",
                                    SenderUserId = customer?.UserId,
                                    //SenderEntityId = Convert.ToString(bidResponse?.ResultData),
                                    SenderEntityId = Convert.ToString(editBidVM.JobQuotationId),
                                    TargetDatabase = TargetDatabase.Customer,
                                    TragetUserId = userVM?.Id,
                                    IsRead = false
                                };

                                bool notificationResult = JsonConvert.DeserializeObject<bool>(
                                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                                );

                                if (notificationResult)
                                {
                                    response.Status = bidResponse.Status;
                                    response.Message = bidResponse.Message;
                                    response.ResultData = bidResponse.ResultData;
                                }
                                else
                                {
                                    response.Status = ResponseStatus.OK;
                                    response.Message = "Unable to send Notification";
                                    response.ResultData = bidResponse.ResultData;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Exc.AddErrorLog(e);
                            response.Status = ResponseStatus.OK;
                            response.Message = "Unable to post Notification";
                            response.ResultData = bidResponse.ResultData;
                        }

                        #region Email to Tradesman

                        PersonalDetailsVM tradesmanPersonalDetail = await GetPersonalDetails(editBidVM.TradesmanId);
                        TmBusinessDetailVM tradesmanBussinessDetail = await GetBusinessDetail(editBidVM.TradesmanId);

                        JobQuotation jobDetail = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={editBidVM.JobQuotationId}", ""));

                        string tradesmanSkill = JsonConvert.DeserializeObject<string>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={tradesmanBussinessDetail.SkillIds[0]}", ""));

                        if (response?.Status == ResponseStatus.OK)
                        {
                            TradesmanBidEmail tradesmanBidEmail = new TradesmanBidEmail
                            {
                                bidAmount = Math.Round(editBidVM.Amount, 2).ToString("N0"),
                                customerName = customer.FirstName + " " + customer.LastName,
                                emailTradesman = tradesmanPersonalDetail.Email,
                                jobBudget = jobDetail.WorkBudget.Value.ToString("N0"),
                                jobTitle = jobDetail.WorkTitle,
                                tradesmanName = tradesmanPersonalDetail.FirstName + " " + tradesmanPersonalDetail.LastName,
                                tradesmanCategory = tradesmanSkill ?? "",
                                Email = new Email
                                {
                                    CreatedBy = tradesmanPersonalDetail.UserId,
                                    Retries = 0,
                                    IsSend = false
                                }
                            };

                            bool result = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.TradesmanBidEmail}", tradesmanBidEmail));
                        }
                        #endregion
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = bidResponse.Message;
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Inavlid User or Tradesman.";
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response> SubmitBidsVoice(EditBidVM editBidVM, string resultData = "")
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrWhiteSpace(editBidVM?.Audio?.Base64String))
                {
                    var checkFormatList = editBidVM.Audio.Base64String.Split(',');

                    if (checkFormatList[0] == "data:audio/wav;base64")
                    {
                        string convert = editBidVM.Audio.Base64String.Replace("data:audio/wav;base64,", String.Empty);
                        editBidVM.Audio.AudioContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                }
                if (!string.IsNullOrWhiteSpace(editBidVM?.Audio?.FileName) && editBidVM?.Audio?.AudioContent != null)
                {
                    //long bidId = 0;
                    //if (string.IsNullOrWhiteSpace(resultData))
                    //    bidId = editBidVM.BidsId;
                    //else
                    //    bidId = Convert.ToInt64(resultData);
                    long bidId = string.IsNullOrWhiteSpace(resultData) ? editBidVM.BidsId : Convert.ToInt64(resultData);
                    BidAudio audio = new BidAudio()
                    {
                        Audio = editBidVM?.Audio?.AudioContent,
                        FileName = editBidVM.Audio.FileName,
                        BidId = bidId,
                        CreatedOn = Convert.ToDateTime(DateTime.Now),
                        CreatedBy = editBidVM.CreatedBy
                    };
                    string audios = await httpClient.PostAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.SaveBidAudio}", audio);
                    //   object json = JsonConvert.DeserializeObject(audios);
                    Response h = JsonConvert.DeserializeObject<Response>(audios);
                    response.Status = h.Status;
                    response.Message = h.Message;
                    response.ResultData = h.ResultData;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<UserViewModels.CallHistoryVM>> GetTradesmanCallLogs(long tradesmanId)
        {
            List<UserViewModels.CallHistoryVM> callHistoryVMs = new List<UserViewModels.CallHistoryVM>();
            try
            {
                string callHistoryJson = await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetTradesmanCallLogs}?tradesmanId={tradesmanId}", "");
                List<TradesmanCallLog> callHistoryList = JsonConvert.DeserializeObject<List<TradesmanCallLog>>(callHistoryJson);

                List<long> customersIds = callHistoryList.Select(c => c.CustomerId).Distinct().ToList();

                string customerListJson = await httpClient.PostAsync($"{ApiRoutes.Customer.BaseUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customersIds);
                List<Customer> customerList = JsonConvert.DeserializeObject<List<Customer>>(customerListJson);

                string customerImageJson = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetCustomerProfileImageList}", customersIds);
                List<CustomerProfileImage> customerImageList = JsonConvert.DeserializeObject<List<CustomerProfileImage>>(customerImageJson);

                List<int> callTypeList = callHistoryList.Select(c => c.CallType.HasValue ? c.CallType.Value : 0).Distinct().ToList();

                string calltypeJson = await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}");
                List<CallType> calltypeList = JsonConvert.DeserializeObject<List<CallType>>(calltypeJson);

                foreach (TradesmanCallLog callhistory in callHistoryList)
                {
                    Customer customer = customerList.FirstOrDefault(x => x.CustomerId == callhistory.CustomerId);

                    UserViewModels.CallHistoryVM history = new UserViewModels.CallHistoryVM()
                    {
                        CallerImage = customerImageList.FirstOrDefault(x => x.CustomerId == callhistory.CustomerId)?.ProfileImage,
                        CallerName = $"{customer?.FirstName} {customer?.LastName}",
                        CallTime = callhistory.CreatedOn,
                        CallDuration = callhistory.Duration,
                        CallType = calltypeList.FirstOrDefault(c => c.CallTypeId == callhistory.CallType)?.Name,
                        CallLogId = callhistory.TradesmanCallLogId,
                        CustomerId = callhistory.CustomerId
                    };
                    callHistoryVMs.Add(history);
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return callHistoryVMs;
        }

        public async Task<bool> DeleteCallLogs(List<long> selectedCallLogIds)
        {
            try
            {
                string deleteConformationJson = await httpClient.PostAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.DeleteCallLogs}", selectedCallLogIds);
                return JsonConvert.DeserializeObject<bool>(deleteConformationJson);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public async Task<PersonalDetailsVM> GetPersonalDetails(long tradesmanId)
        {
            try
            {
                string tradesmanjson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetPersonalDetails}?tradesmanId={tradesmanId}", "");
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(tradesmanjson);

                Response identityUser = JsonConvert.DeserializeObject<Response>
                      (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={tradesman.UserId}", ""));

                UserRegisterVM userRegister = JsonConvert.DeserializeObject<UserRegisterVM>(identityUser?.ResultData?.ToString());

                TradesmanProfileImage tradesmanProfileImage = JsonConvert.DeserializeObject<TradesmanProfileImage>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageByTradesmanId}?tradesmanId={tradesmanId}", ""));

                PersonalDetailsVM personalDetailVM = new PersonalDetailsVM()
                {
                    EntityId = tradesman.TradesmanId,
                    FirstName = tradesman.FirstName,
                    LastName = tradesman.LastName,
                    Cnic = tradesman.Cnic,
                    Email = tradesman.EmailAddress,
                    Gender = (int)tradesman.Gender,
                    DateOfBirth = (DateTime)tradesman.Dob,
                    MobileNumber = tradesman.MobileNumber,
                    UserId = tradesman.UserId,
                    ProfileImage = tradesmanProfileImage?.ProfileImage,
                    IsEmailConfirmed = userRegister.IsEmailConfirmed,
                    IsNumberConfirmed = userRegister.IsNumberConfirmed,
                    City = tradesman.City,
                    FirebaseClientId = userRegister.FirebaseClientId
                };

                return personalDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new PersonalDetailsVM();
            }

        }

        public async Task<bool> UpdatePersonalDetails(long id, PersonalDetailVM personalDetailVM)
        {
            try
            {
                Tradesman tradesmanEntity = new Tradesman()
                {
                    TradesmanId = id,
                    LastName = personalDetailVM.LastName,
                    Dob = personalDetailVM.DateOfBirth,
                    FirstName = personalDetailVM.FirstName,
                    Cnic = personalDetailVM.Cnic,
                    EmailAddress = personalDetailVM.Email,
                    Gender = personalDetailVM.Gender,
                    ModifiedOn = DateTime.Now
                };
                string resultJson = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdatePersonalDetails}", tradesmanEntity);
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public async Task<List<MediaImagesVM>> GetJobBidImages(long jobQuotationId)
        {
            List<JobImages> jobImages = new List<JobImages>();
            List<MediaImagesVM> mediaImagesVMs = new List<MediaImagesVM>();
            try
            {
                string bidImagesList = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobBidImages}?jobQuotaionId={jobQuotationId}", "");
                jobImages = JsonConvert.DeserializeObject<List<JobImages>>(bidImagesList);
                foreach (JobImages bidimages in jobImages)
                {
                    MediaImagesVM mediaImages = new MediaImagesVM()
                    {
                        FileName = bidimages.FileName,
                        Images = bidimages.BidImage,
                        IsMain = bidimages.IsMain,
                        JobQuotationId = bidimages.JobQuotationId,
                        BidImageId = bidimages.BidImageId
                    };
                    mediaImagesVMs.Add(mediaImages);
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return mediaImagesVMs;
        }

        public async Task<List<IdValueVM>> GetTradesmanSkillsByParentId(long parentId)
        {
            try
            {

                List<Skill> idValues = new List<Skill>();
                List<SubSkill> idValu = new List<SubSkill>();
                List<IdValueVM> idVal = new List<IdValueVM>();
                if (parentId == 0)
                {
                    string idvaluesjson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkills}", "");
                    return JsonConvert.DeserializeObject<List<IdValueVM>>(idvaluesjson);
                    //foreach (var item in idValues)
                    //{
                    //    var idValue = new IdValueVM()
                    //    {
                    //        Value = item.Name,
                    //        Id = item.SkillId
                    //    };
                    //    idVal.Add(idValue);
                    //}
                }
                else if (parentId != 0)
                {
                    string idvaluesjson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSubSkills}?parentId={parentId}", "");
                    idValu = JsonConvert.DeserializeObject<List<SubSkill>>(idvaluesjson);
                    foreach (SubSkill item in idValu)
                    {
                        IdValueVM idValue = new IdValueVM()
                        {
                            Value = item.Name,
                            Id = item.SubSkillId
                        };
                        idVal.Add(idValue);
                    }
                }
                return idVal;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }


        }

        public async Task<List<LocalProfessionVM>> GetLocalProfessionalImages()
        {
            List<LocalProfessionVM> localProfessionVMs = new List<LocalProfessionVM>();
            try
            {
                List<Skill> skills = JsonConvert.DeserializeObject<List<Skill>>
                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLocalProfessionalImages}", ""));

                //List<long> skillIds = skills.Select(s => s.SkillId).Distinct().ToList();

                //List<TradesmanSkillImage> tradesmanSkillImages = JsonConvert.DeserializeObject<List<TradesmanSkillImage>>
                //    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageBySkillIds}", skillIds));

                localProfessionVMs = skills.Select(x => new LocalProfessionVM() { SkillId = x.SkillId, SkillName = x.Name }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return localProfessionVMs;
        }

        public async Task<RateTradesmanVM> RateTradesmanByJobQuotationId(long jobDetailId)
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
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetFeedBack}?JobDetailId={jobDetail.JobDetailId}&tradesmanId={tradesmanDetail.TradesmanId}", ""));

                RateTradesmanVM rateTradesmanVM = new RateTradesmanVM()
                {
                    TradesmanImage = tradesmanProfileImage?.ProfileImage,
                    JobQuotationId = jobDetail?.JobQuotationId ?? 0,
                    TradesmanName = $"{tradesmanDetail?.FirstName} {tradesmanDetail?.LastName}",
                    TradesmanId = tradesmanDetail?.TradesmanId ?? 0,
                    CustomerId = jobDetail?.CustomerId ?? 0,
                    JobTitle = jobDetail?.Title,
                    JobStartingDateTime = jobDetail.StartDate,
                    JobEndingDateTime = jobDetail.EndDate,
                    DirectPayment = tradesmanJobReceipts?.DirectPayment ?? false,
                    JobBudget = jobDetail?.TradesmanBudget ?? 0,
                    TradesmanFeedbackId = tradesmanFeedback?.TradesmanFeedbackId ?? 0
                };

                return rateTradesmanVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new RateTradesmanVM();
            }

        }

        public async Task<TradesmanProfileVM> GetTradesmanProfile(long tradesmanId, bool isActive)
        {
            try
            {
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradesmanId}", ""));
                TradesmanProfileImage tradesmanImage = JsonConvert.DeserializeObject<TradesmanProfileImage>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanImageById}?tradesmanId={tradesmanId}", ""));
                List<SkillSet> skillSets = JsonConvert.DeserializeObject<List<SkillSet>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillSetByTradesmanId}?tradesmanId={tradesmanId}&isActive={isActive}", ""));
                List<Skill> skills = JsonConvert.DeserializeObject<List<Skill>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetAllSkills}", ""));
                List<TradesmanFeedback> tradesmanFeedbacks = JsonConvert.DeserializeObject<List<TradesmanFeedback>>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanFeedBack}?tradesmanId={tradesmanId}", ""));

                List<JobDetail> tradesmanJobs = JsonConvert.DeserializeObject<List<JobDetail>>(
                    await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobsByTradesmanIds}", new List<long>() { tradesmanId })
                );
                List<long> customersIds = tradesmanFeedbacks.Select(c => c.CustomerId).Distinct().ToList();
                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customersIds));

                int totalRating = 0;
                int ratingValue = 0;
                int ratingReliability = 0;

                if (tradesmanFeedbacks?.Count() > 0)
                {
                    totalRating += Convert.ToInt32(tradesmanFeedbacks?.Select(x => x?.OverallRating)?.Average() ?? 0);
                    ratingValue = Convert.ToInt32(tradesmanFeedbacks?.Select(x => x?.CommunicationRating)?.Average() ?? 0);
                    ratingReliability = Convert.ToInt32(tradesmanFeedbacks?.Select(x => x?.QualityRating)?.Average() ?? 0);

                    totalRating += ratingValue + ratingReliability;

                    if (totalRating > 0)
                    {
                        totalRating = totalRating / 3;
                    }
                }



                TradesmanProfileVM tradesmanProfile = new TradesmanProfileVM()
                {
                    TradesmanId = tradesmanId,
                    TradesmanName = tradesman?.FirstName + " " + tradesman?.LastName,
                    TradesmanProfileImg = tradesmanImage?.ProfileImage,
                    SkillsSet = skills?.Where(x => skillSets.Select(c => c.SkillId).Contains(x.SkillId)).Select(s => s.Name).ToList(),
                    TradesmanUserId = tradesman.UserId,
                    GpsCoordinates = tradesman?.GpsCoordinates,
                    MobileNumber = tradesman?.MobileNumber,
                    Email = tradesman?.EmailAddress,
                    TradesmanAddress = $"{tradesman?.ShopAddress}",
                    // TradesmanAddress = $"{tradesman?.ShopAddress} {tradesman?.Area.TrimEnd()}, {tradesman?.City}",
                    MarkerOptionsAddress = $"{tradesman?.Area.TrimEnd()}, {tradesman?.City}",
                    Rating = totalRating,
                    Value = ratingValue,
                    Relaiability = ratingReliability,
                    TotalDoneJob = tradesmanJobs?.Count() ?? 0,
                    MemberSince = tradesman.CreatedOn,
                    Feedbacks = tradesmanFeedbacks?.Select(c => new Feedback
                    {
                        CustomerName = customers?.Where(x => x.CustomerId == c.CustomerId).Select(s => s.FirstName + " " + s.LastName).FirstOrDefault(),
                        //CustomerAddress = customers?.Where(x => x.CustomerId == c.CustomerId).Select(s => s.StreetAddress + ", " + s.City).FirstOrDefault(),
                        CustomerComment = c?.Comments,
                        Rating = c?.OverallRating
                    }).ToList()
                };
                return tradesmanProfile;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TradesmanProfileVM();
            }


        }

        public async Task<List<IdValueVM>> GetTradesmanSkills()
        {
            try
            {
                List<IdValueVM> trademanSkills = JsonConvert.DeserializeObject<List<IdValueVM>>
                                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkills}", ""));
                return trademanSkills;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }

        }

        public async Task<TmBusinessDetailVM> GetBusinessDetail(long tradsmanId)
        {
            try
            {
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradsmanId}", ""));
                List<long> skillIds = JsonConvert.DeserializeObject<List<long>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillIds}?tradesmanId={tradesman.TradesmanId}"));
                long cityId = JsonConvert.DeserializeObject<long>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityIdByName}?cityName={tradesman.City}", ""));
                List<Skill> tradesmanSkills = JsonConvert.DeserializeObject<List<Skill>>(await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillsByIds}", skillIds));
                List<IdValueVM> idValueVM = new List<IdValueVM>();
                idValueVM = tradesmanSkills.Select(x => new IdValueVM { Id = x.SkillId, Value = x.Name }).ToList();
                TmBusinessDetailVM businessDetailVM = new TmBusinessDetailVM()
                {
                    Town = tradesman.Area,
                    BusinessAddress = tradesman.ShopAddress,
                    SkillIds = skillIds,
                    CityId = cityId,
                    City = tradesman.City,
                    CompanyName = tradesman.CompanyName,
                    CompanyRegNo = tradesman.CompanyRegNo,
                    TravelingDistance = tradesman.TravellingDistance ?? 0,
                    TradesmanId = tradesman.TradesmanId,
                    tradesmanSkills = idValueVM,
                    LatLng = tradesman.GpsCoordinates,
                    AddressLine = tradesman.AddressLine,
                    IsOrganization = (bool)tradesman.IsOrganization

                };
                return businessDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TmBusinessDetailVM();
            }

        }

        public async Task<UserProfileVM> GetTradesmanByUserId(string userId)
        {
            UserProfileVM userProfileVM = null;
            try
            {
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
               await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanByUserId}?userId={userId}")
           );

                if (tradesman != null)
                {
                    userProfileVM = new UserProfileVM()
                    {
                        EntityId = tradesman.TradesmanId,
                        UserName = $"{tradesman.FirstName} {tradesman.LastName}",
                        City = tradesman.City,
                        UserId = userId,
                        tradesmanId = tradesman.TradesmanId,
                        PublicId = tradesman.PublicId,
                        Email = tradesman.EmailAddress
                    };
                    if ((bool)tradesman.IsOrganization)
                    {
                        userProfileVM.UserName = tradesman.CompanyName;
                    }
                    List<long> skillIds = JsonConvert.DeserializeObject<List<long>>(
                        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillIds}?tradesmanId={tradesman.TradesmanId}")
                    );

                    if (skillIds != null && skillIds.Count > 0)
                    {
                        List<Skill> skills = JsonConvert.DeserializeObject<List<Skill>>(
                            await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillsByIds}", skillIds)
                        );

                        userProfileVM.Skills = skills?.Select(s => s.Name).ToList();
                    }

                    TradesmanProfileImage tradesmanProfileImage = JsonConvert.DeserializeObject<TradesmanProfileImage>(
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageByTradesmanId}?tradesmanId={tradesman.TradesmanId}")
                    );

                    userProfileVM.ProfileImage = tradesmanProfileImage?.ProfileImage;
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return userProfileVM;
        }

        public async Task<List<IdValueVM>> GetSubSkillBySkillId(long tradesmanSkillId)
        {
            List<IdValueVM> subSkills = new List<IdValueVM>();
            try
            {
                List<SubSkill> subSkillsList = JsonConvert.DeserializeObject<List<SubSkill>>
               (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillBySkillId}?skillId={tradesmanSkillId}", ""));

                foreach (SubSkill item in subSkillsList)
                {
                    subSkills.Add(new IdValueVM() { Id = item.SkillId, Value = item.Name });
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return subSkills;
        }

        public async Task<List<IdValuePriceVM>> GetSubSkillsBySkillId(long tradesmanSkillId)
        {
            List<IdValuePriceVM> subSkills = new List<IdValuePriceVM>();
            try
            {
                List<SubSkill> subSkillsList = JsonConvert.DeserializeObject<List<SubSkill>>
               (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillsBySkillId}?skillId={tradesmanSkillId}", ""));

                if (subSkillsList?.Count > 0)
                {
                    foreach (SubSkill item in subSkillsList)
                    {
                        subSkills.Add(new IdValuePriceVM() { Id = item.SubSkillId,SkillId=item.SkillId, Value = item.Name, ExpectedPrice = item.SubSkillPrice, VisitCharges = item.VisitCharges });
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return subSkills;
        }
        public async Task<IdValuePriceVM> GetSubSkillbySubSkillId(long subSkillId)
        {
            IdValuePriceVM subSkill = new IdValuePriceVM();

            try
            {
                SubSkill subSkillsList = JsonConvert.DeserializeObject<SubSkill>
                    (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillbySubSkillId}?subSkillId={subSkillId}", ""));

                if (subSkillsList != null)
                {
                    subSkill.Id = subSkillsList.SubSkillId;
                    subSkill.Value = subSkillsList.Name;
                    subSkill.ExpectedPrice = subSkillsList?.SubSkillPrice ?? 0;
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return subSkill;
        }
        public async Task<List<IdValueVM>> GetSubSkill()
        {
            List<IdValueVM> subSkills = new List<IdValueVM>();
            try
            {
                List<SubSkill> subSkillsList = JsonConvert.DeserializeObject<List<SubSkill>>
                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkill}", ""));


                foreach (SubSkill item in subSkillsList)
                {
                    subSkills.Add(new IdValueVM() { Id = item.SkillId, Value = item.Name });
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return subSkills;

        }

        public async Task<bool> CheckFeedBackStatus(long jobDetailId)
        {
            bool checkfeedback = JsonConvert.DeserializeObject<bool>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.CheckFeedBackStatus}?jobDetailId={jobDetailId}")

                        );
            return checkfeedback;


        }

        public async Task<Response> Login(LoginVM model)
        {
            System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();

            var response = new Response();

            try
            {
                //var disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl);   only supports .net core 2.2

                var disco = await _httpClient.GetDiscoveryDocumentAsync(_apiConfig.IdentityServerApiUrl); // supports .net core 5.0

                if (disco.IsError)
                {
                    Exc.AddErrorLog(new Exception(disco.Error));
                    response.Message = "LoginFailed";
                    response.ResultData = disco.IsError;
                }
                else
                {
                    //   only supports .net core 2.2   //
                    //var extra = new Dictionary<string, string> { { "firebaseClientId", model.FirebaseClientId } };
                    //var tokenClient = new TokenClient(disco.TokenEndpoint, clientCred.ClientId, clientCred.Secret, null, AuthenticationStyle.PostValues);
                    //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync($"{model.EmailOrPhoneNumber}", model.Password, "api1", extra);

                    var tokenResponse = await HttpClientTokenRequestExtensions.RequestPasswordTokenAsync(_httpClient, new PasswordTokenRequest
                    {
                        Scope = "api1",
                        Address = disco.TokenEndpoint,
                        ClientSecret = clientCred.Secret,
                        ClientId = clientCred.ClientId,
                        UserName = model.EmailOrPhoneNumber,
                        Password = model.Password,
                        Parameters =
                        {
                            { "firebaseClientId", model.FirebaseClientId }
                        }
                    });

                    if (!tokenResponse.IsError)
                    {
                        response.ResultData = tokenResponse.AccessToken;
                        response.Status = ResponseStatus.OK;
                        response.Message = "Login successful.";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Login failed.";
                    }
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb(long tradsmanId)
        {
            TradesManProfileDetailsVM tradesmanProfileDetailVM = new TradesManProfileDetailsVM();
            try
            {
                tradesmanProfileDetailVM.PersnalDetails = await GetPersonalDetails(tradsmanId);
                tradesmanProfileDetailVM.BusinessDetails = await GetBusinessDetail(tradsmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }


            return tradesmanProfileDetailVM;
        }
        public async Task<List<TradesmanReportbySkillVM>> GetLAllTradesmanbyCategoryReport(DateTime StartDate, DateTime EndDate, string[] skills)
        {
            List<TradesmanReportbySkillVM> subSkillsList = JsonConvert.DeserializeObject<List<TradesmanReportbySkillVM>>(
                           await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillReport}?startDate={StartDate}&EndDate={EndDate}&skills={skills}"));
            return subSkillsList;

        }

        public async Task<List<TradesmanReportbySkillVM>> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId)
        {
            List<TradesmanReportbySkillVM> subSkillsList = JsonConvert.DeserializeObject<List<TradesmanReportbySkillVM>>(
                          await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllTradesmanbySkillTown}?skills={skills}&town={town}&tradesmanId={ tradesmanId}"));
            return subSkillsList;
        }
        public Task<List<Tradesman>> GetTradesmanCompletedJobsFeeback()
        {
            return null;
        }

        public async Task<Response> GetBusinessDetailsStatus(string id)
        {
            var aa = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetBusinessDetailsStatus}?id={id}");
            Response response = JsonConvert.DeserializeObject<Response>(aa);
            return response;
        }
        public async Task<List<Skill>> GetSkillList(long skillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Skill>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetAllSkills}?skillId={skillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Skill>();
            }
        }
        public async Task<Skill> GetSkillTagsBySkillId(long skillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Skill>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillTagsBySkill}?skillId={skillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Skill();
            }
        }
        public async Task<List<TradesmanViewModels.MetaTagsVM>> GetCommonMetaTags()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<TradesmanViewModels.MetaTagsVM>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetMetaTags}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanViewModels.MetaTagsVM>();
            }
        }
        public async Task<SubSkill> GetSubSkillTagsById(long subSkillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<SubSkill>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillTagsById}?subSkillId={subSkillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubSkill();
            }
        }
        public async Task<SubSkill> GetSubSkillById(long subSkillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<SubSkill>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillById}?subSkillId={subSkillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubSkill();
            }
        }
        public async Task<List<SubSkill>> GetSubSkillTagsBySkillId(long SkillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SubSkill>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSubSkills}?parentId={SkillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SubSkill>();
            }
        }

        public async Task<bool> UpdateTradesmanPublicId(long tradesmanId, string publicId)
        {

            var result = JsonConvert.DeserializeObject<bool>(
                                   await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdateTradesmanPublicId}?tradesmanId={tradesmanId}&publicId={publicId}")
                               );
            return result;
        }        
        public async Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city)
        {

            var result = JsonConvert.DeserializeObject<Response>(
                await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanFirebaseIdListBySkillAndCity}?categoryId={categoryId}&city={city}"));
            return result;
        }

        public async Task<List<SubSkillWithSkillVM>> GetSubSkillsWithSkill()
        {
            List<SubSkillWithSkillVM> subSkills = new List<SubSkillWithSkillVM>();
            try
            {
                subSkills = JsonConvert.DeserializeObject<List<SubSkillWithSkillVM>>
               (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillsWithSkill}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return subSkills;
        }
    }
}
