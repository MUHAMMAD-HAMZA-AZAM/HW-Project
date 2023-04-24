using HW.CallModels;
using HW.CustomerModels;
using HW.Http;
using HW.IdentityViewModels;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.SupplierModels;
using HW.TradesmanModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;

namespace HW.GatewayApi.Services
{
    public interface INotificationService
    {
        Task<int> NotificationCallRequest(long userId, UserRegisterVM userRegisterVM, long jobQuotationId, long customerId);
        Task<bool> PostBidAcceptanceNotification(long userId, long tradesmanId, long bidId, string jobTitle);
        Task<bool> RequestFeedbackNotification(long jobDetailId);
        Task<bool> PromoteAdNotification(long supplierId, string adTitle);
        Task<bool> NotificationRateSupplier(long userId, long supplierId, int overallRating);
        Task<bool> NotificationRatingTradesman(long userId, long tradesmanId, int overallRating, long jobDetailId);
        Task<List<PostNotificationVM>> GetNotifications(string userId, string role, int pageNumber);
        Task<Response> GetNotificationsCount(string userId, string role);
        Task<Response> GetHWMallNotificationsCount(string userId, string role);
        Task<bool> NotificationJobCostUpdate(long jobDetailId);
        Task<Response> NotificationBidDecline(long bidId, string jobTitle,long CustomerId, string userid);
        Task<Response> NotificationJobUpdate(long jobQuotationId, long userId);
        Task<NotificationLogging> GetNotificationLogByNotificationId(long notificationId);
        Task<bool> UpdateNotificationLogByNotificationId(NotificationLogging notificationLogging);
        Task<List<PostNotificationVM>> GetNotificationsForOrders(string userId);
    }

    public class NotificationService : INotificationService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;


        public NotificationService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<int> NotificationCallRequest(long userId, UserRegisterVM userRegisterVM, long jobQuotationId, long customerId)
        {
            int returnCode = (int)ReturnNotificationCallRequest.ErrorOccure;

            try
            {
                Response callRequestCount = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetCallRequestLogs}?tradesmanId={userId}&jobQuotationId={jobQuotationId}")
                );

                int requestCount = JsonConvert.DeserializeObject<int>(callRequestCount.ResultData.ToString());

                if (requestCount <= Retries.Three) // if user have requested for call request three times a day
                {
                    Tradesman tradesman = new Tradesman();
                    string workerName = string.Empty;
                    object entityId = null;

                    if (userRegisterVM.Role == "Tradesman" || userRegisterVM.Role == "Organization")
                    {
                        tradesman = JsonConvert.DeserializeObject<Tradesman>
                            (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={userId}", "")
                        );
                    }

                    Customer customer = JsonConvert.DeserializeObject<Customer>
                        (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", "")
                    );

                    if (customer != null)
                    {
                        Response response = JsonConvert.DeserializeObject<Response>
                            (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer?.UserId}"));

                        UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                        if (tradesman != null)
                        {
                            workerName = $"{tradesman.FirstName} {tradesman.LastName}";
                            entityId = tradesman.TradesmanId;

                            // post notification

                            PostNotificationVM postNotificationVM = new PostNotificationVM()
                            {
                                Body = $"New call request from {workerName}",
                                Title = NotificationTitles.NewCallRequest,
                                To = userVM.FirebaseClientId,
                                SenderEntityId = entityId.ToString(),
                                TargetActivity = "TradesmanProfile",
                                SenderUserId = tradesman.UserId,
                                TargetDatabase = TargetDatabase.Customer,
                                JobReferenceId = jobQuotationId,
                                TragetUserId = userVM.Id,
                                IsRead = false
                            };

                            bool isNotificationSent = JsonConvert.DeserializeObject<bool>
                                (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                            );

                            if (isNotificationSent)
                            {
                                TradesmanCallLog tradesmanCallLog = new TradesmanCallLog()
                                {
                                    CustomerId = customerId,
                                    JobQuotationId = jobQuotationId,
                                    TradesmanId = userId,
                                    CallType = (int)CallTypeEnum.CallRequest,
                                    CreatedBy = tradesman.UserId,
                                    CreatedOn = DateTime.Now
                                };

                                Response callRequestLog = JsonConvert.DeserializeObject<Response>(
                                    await httpClient.PostAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.PostCallRequestLog}", tradesmanCallLog)
                                );

                                returnCode = (int)ReturnNotificationCallRequest.Success;
                            }
                        }
                        else
                        {
                            returnCode = (int)ReturnNotificationCallRequest.TradesmanDontExist;
                            // orgainzation implementaiton
                        }
                    }
                    else
                    {
                        returnCode = (int)ReturnNotificationCallRequest.CustomerDontExist;
                    }

                    // add data to database
                }
                else
                {
                    returnCode = (int)ReturnNotificationCallRequest.RetriesLimitReached;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(new Exception($"Customer id: {customerId}, user id : {userId}, job quotation id : {jobQuotationId}, exception message : {ex.Message}"));
                returnCode = (int)ReturnNotificationCallRequest.ExceptionOccur;
            }

            return returnCode;
        }

        public async Task<bool> PostBidAcceptanceNotification(long userId, long tradesmanId, long bidId, string jobTitle)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={userId}", "")
                    );

                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>
                        (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradesmanId}", "")
                );

                Response response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={tradesman.UserId}")
                );

                UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                long jobQuatationId = JsonConvert.DeserializeObject<long>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetQuotationIdByBidId}?bidId={bidId}", "")
                );

                //long jobDetailId = JsonConvert.DeserializeObject<long>(
                //    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailIdByQuotationId}?quotationId={jobQuatationId}", "")
                //);

                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    Body = $"{customer.FirstName} {customer.LastName} has accept your bid for the job {jobTitle}",
                    Title = NotificationTitles.BidAccepted,
                    To = $"{userVM?.FirebaseClientId}",
                    SenderEntityId = jobQuatationId.ToString(),
                    TargetActivity = "JobDetailActive",
                    SenderUserId = customer.UserId,
                    TargetDatabase = TargetDatabase.Tradesman,
                    TragetUserId = userVM.Id,
                    IsRead = false
                };

                bool IsSend = false;
                IsSend = JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                );
                return IsSend;
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;

            }

        }

        public async Task<bool> RequestFeedbackNotification(long jobDetailId)
        {
            try
            {
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={jobDetailId}")
                    );

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={jobDetail.CustomerId}")
                );

                Response response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
                );

                UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>
                        (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={jobDetail.TradesmanId}", "")
                );

                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    Title = NotificationTitles.FeedbackRequest,
                    Body = $"{tradesman.FirstName} {tradesman.LastName} has Request Feedback for {jobDetail.Title}",
                    To = userVM.FirebaseClientId,
                    SenderEntityId = jobDetailId.ToString(),
                    TargetActivity = "RateTradesman",
                    TargetDatabase = TargetDatabase.Customer,
                    SenderUserId = tradesman.UserId,
                    TragetUserId = userVM.Id,
                    IsRead = false

                };

                return JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
               );
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;

            }
        }

        public async Task<bool> PromoteAdNotification(long supplierId, string adTitle)
        {
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierId}")
                  );


                Response response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={supplier.UserId}")
                );

                if (response?.ResultData != null)
                {
                    UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());
                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        Title = NotificationTitles.YourAdIsPosted,
                        Body = $"Your ad. {adTitle} is posted successfully",
                        SenderUserId = supplier.UserId,
                        TargetActivity = "MyAds",
                        To = userVM.FirebaseClientId,
                        TargetDatabase = TargetDatabase.Supplier,
                        TragetUserId = supplierId.ToString(),
                        IsRead = false
                    };
                    var result = JsonConvert.DeserializeObject<bool>(
                   await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));
                    return result;
                }
                else
                {
                    return false;
                }


            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;

            }

        }

        public async Task<bool> NotificationRateSupplier(long userId, long supplierId, int overallRating)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={userId}")
                    );

                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(
                    await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierId}")
               );

                Response response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={supplier.UserId}")
                );

                UserRegisterVM userRegisterVM = JsonConvert.DeserializeObject<UserRegisterVM>(response?.ResultData?.ToString());


                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    To = userRegisterVM.FirebaseClientId,
                    Title = NotificationTitles.NewFeedbackReceived,
                    Body = $"{customer.FirstName} {customer.LastName} have rated you with {overallRating} stars",
                    TargetDatabase = TargetDatabase.Supplier,
                    SenderUserId = customer.UserId,
                    TragetUserId = supplier.UserId,
                    TargetActivity = "Ratings",
                    IsRead =false
                };

                return JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;

            }
        }

        public async Task<bool> NotificationRatingTradesman(long userId, long tradesmanId, int overallRating, long jobDetailId)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={userId}")
                    );

                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradesmanId}")
                );

                Response response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={tradesman.UserId}")
                );

                UserRegisterVM userRegisterVM = JsonConvert.DeserializeObject<UserRegisterVM>(response?.ResultData?.ToString());


                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    To = userRegisterVM.FirebaseClientId,
                    Title = NotificationTitles.NewFeedbackReceived,
                    Body = $"{customer.FirstName} {customer.LastName} have rated you with {overallRating} stars",
                    TargetDatabase = TargetDatabase.Tradesman,
                    SenderUserId = customer.UserId,
                    TargetActivity = "JobDetail_CompletedJob",
                    SenderEntityId = jobDetailId.ToString(),
                    TragetUserId = tradesman.UserId,
                    IsRead = false
                };

                return JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;

            }
        }

        public async Task<bool> NotificationJobCostUpdate(long jobDetailId)
        {
            bool result = false;

            try
            {
                JobDetail jobDetail = JsonConvert.DeserializeObject<JobDetail>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={jobDetailId}")
                    );

                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(
                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={jobDetail.TradesmanId}")
                );

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={jobDetail.CustomerId}")
                );

                Response response = JsonConvert.DeserializeObject<Response>(
                   await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}")
               );

                UserRegisterVM userRegisterVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    To = userRegisterVM.FirebaseClientId,
                    Title = NotificationTitles.BidCostUpdated,
                    Body = $"Tradesman {tradesman.FirstName} {tradesman.LastName} have updated the bid cost of job {jobDetail.Title}",
                    TargetDatabase = TargetDatabase.Customer,
                    SenderUserId = tradesman.UserId,
                    TragetUserId = customer.UserId,
                    TargetActivity = "InProgressJobDetail",
                    SenderEntityId = jobDetail.JobQuotationId.ToString(),
                    IsRead = false
                };

                result = JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));

            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return result;
        }

        public async Task<Response> NotificationBidDecline(long bidId, string jobTitle, long CustomerId, string userid)
        {
            Response response = new Response();

            try
            {
                UserRegisterVM userRegisterVM = new UserRegisterVM();

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                   await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={CustomerId}")
               );

                response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanByBidId}?bidId={bidId}", "")
                );


                response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={response.ResultData.ToString()}", "")
                );

                userRegisterVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    To = userRegisterVM.FirebaseClientId,
                    Title = NotificationTitles.BidDeclined,
                    Body = $"{customer.FirstName} {customer.LastName} has declined your bid for the job {jobTitle} but new job is on your way",
                    //Body = $"Job has been declined but new job is on your way",
                    TargetDatabase = TargetDatabase.Tradesman,
                    SenderUserId = userid,
                    TragetUserId = userRegisterVM.Id,
                    TargetActivity = "MyBids",
                    SenderEntityId = bidId.ToString(),
                    IsRead = false
                };

                bool result = JsonConvert.DeserializeObject<bool>(
                    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> NotificationJobUpdate(long jobQuotationId, long userId)
        {
            Response response = new Response();

            bool result = false;

            List<long> bidIds = JsonConvert.DeserializeObject<List<long>>(
                await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidCountByJobQuotationId}?jobQuotationId={jobQuotationId}", "")
            );

            try
            {
                if (bidIds.Count > 0)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={userId}")
                    );

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.Sp_GetTradesmanFirebaseClientId}?jobQuotationId={jobQuotationId}", "")
                    );
                   
                    List<TradesmanFirebaseIdVM> tradesmanFirebaseIdVMs = JsonConvert.DeserializeObject<List<TradesmanFirebaseIdVM>>(response.ResultData.ToString());

                    foreach (TradesmanFirebaseIdVM tradesmanFirebase in tradesmanFirebaseIdVMs)
                    {
                        PostNotificationVM postNotificationVM = new PostNotificationVM()
                        {
                            To = tradesmanFirebase.FirebaseClientId,
                            Title = NotificationTitles.JobUpdted,
                            Body = $"User have update the job",
                            TargetDatabase = TargetDatabase.Tradesman,
                            SenderUserId = customer.UserId,
                            TragetUserId = tradesmanFirebase.Id,
                            TargetActivity = "JobDetail_EditBid",
                            SenderEntityId = jobQuotationId.ToString(),
                            IsRead = false
                        };

                        result = JsonConvert.DeserializeObject<bool>(
                            await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM));
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            if (result)
            {
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.ResultData = result;
            }

            return response;
        }

        public async Task<Response> GetNotificationsCount(string userId, string role)
        {
            Response response = new Response();

            double notificationcount = JsonConvert.DeserializeObject<double>(
                await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetNotificationsCount}?userId={userId}&role={role}")
            ); 

            if (notificationcount > 0)
            {
                response.Status = ResponseStatus.OK;
                response.ResultData = notificationcount;
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.ResultData = null;
            }

            return response;
        }
        public async Task<Response> GetHWMallNotificationsCount(string userId, string role)
        {
            Response response = new Response();

            double notificationcount = JsonConvert.DeserializeObject<double>(
                await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetHWMallNotificationsCount}?userId={userId}&role={role}")
            ); 

            if (notificationcount > 0)
            {
                response.Status = ResponseStatus.OK;
                response.ResultData = notificationcount;
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.ResultData = null;
            }

            return response;
        }

        public async Task<List<PostNotificationVM>> GetNotifications(string userId, string role, int pageNumber)
        {
            List<PostNotificationVM> postNotificationVMs = new List<PostNotificationVM>();

            try
            {
                List<NotificationLogging> notificationLogging = JsonConvert.DeserializeObject<List<NotificationLogging>>
                        (await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetNotifications}?userId={userId}&role={role}&pageNumber={pageNumber}"));


                if (notificationLogging != null)
                {
                    foreach (NotificationLogging notification in notificationLogging)
                    {
                        if(notification.PayLoad !=null)
                        {
                            DataNotificationPayload payload = JsonConvert.DeserializeObject<DataNotificationPayload>(notification?.PayLoad);
                            if(payload != null)
                            {
                                PostNotificationVM model = new PostNotificationVM
                                {
                                    Title = payload.notification.title,
                                    SenderEntityId = payload.data.senderEntityId,
                                    Body = payload.notification.body,
                                    IsRead = notification.IsRead,
                                    TargetActivity = payload.data.targetActivity,
                                    NotificationId = notification.NotificationLoggingId,
                                    CreatedOn = notification.CreatedOn
                                };

                                postNotificationVMs.Add(model);
                            }
                            
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return postNotificationVMs;

        }

        public async Task<NotificationLogging> GetNotificationLogByNotificationId(long notificationId)
        {
            string notificationLoggingJson = await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetNotificationLogByNotificationId}?_notificationId={notificationId}");
            return JsonConvert.DeserializeObject<NotificationLogging>(notificationLoggingJson);
        }

        public async Task<bool> UpdateNotificationLogByNotificationId(NotificationLogging notificationLogging)
        {
            string notificationLoggingJson = await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.UpdateNotificationLogByNotificationId}", notificationLogging);
            return JsonConvert.DeserializeObject<bool>(notificationLoggingJson);
        }

        public async Task<List<PostNotificationVM>> GetNotificationsForOrders(string userId)
        {
            List<PostNotificationVM> postNotificationVMs = new List<PostNotificationVM>();

            try
            {
                List<NotificationLogging> notificationLogging = JsonConvert.DeserializeObject<List<NotificationLogging>>
                        (await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetNotificationsForOrders}?userId={userId}"));


                if (notificationLogging != null)
                {
                    foreach (NotificationLogging notification in notificationLogging)
                    {
                        if (notification.PayLoad != null)
                        {
                            DataNotificationPayload payload = JsonConvert.DeserializeObject<DataNotificationPayload>(notification?.PayLoad);
                            if (payload != null)
                            {
                                PostNotificationVM model = new PostNotificationVM
                                {
                                    Title = payload.notification.title,
                                    SenderEntityId = payload.data.senderEntityId,
                                    Body = payload.notification.body,
                                    IsRead = notification.IsRead,
                                    TargetActivity = payload.data.targetActivity,
                                    NotificationId = notification.NotificationLoggingId,
                                    CreatedOn = notification.CreatedOn
                                };

                                postNotificationVMs.Add(model);
                            }

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return postNotificationVMs;
        }
    }
}