using Hangfire;
using HW.NotificationApi.Code;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;

namespace HW.NotificationApi.Services
{
    public interface INotificationService
    {
        Task<bool> PostTopicNotification(PostNotificationVM postNotificationVM);
        Task<bool> PostDataNotification(PostNotificationVM postNotificationVM);
        Task SaveNotificationDataWeb(PostNotificationVM postNotificationVM);
        double GetNotificationsCount(string userId, string role);
        double GetHWMallNotificationsCount(string userId, string role);
        IQueryable<NotificationLogging> GetNotifications(string userId, string role, int pageNumber);
        List<NotificationLogging> GetAdminNotifications(int pageSize, int pageNumber, string userId);
        List<PostNotificationVM> GetNotificationsByUserId(int pageSize, int pageNumber, string userId);
        Response MarkNotificationAsRead(int notificationId);
        NotificationLogging GetNotificationLogByNotificationId(long notificationId);
        bool UpdateNotificationLogByNotificationId(NotificationLogging notificationLogging);
        List<NotificationLogging> GetNotificationsForOrders(string userId);
    }

    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork uow;

        private readonly NotificationKeys genericFirebaseKeys;

        private HttpClient httpClient;
        private readonly IExceptionService exceptionService;

        private NotificationLogging notificationLogging;

        public NotificationService(IUnitOfWork _uow, NotificationKeys genericFirebaseKey, IExceptionService Exc)
        {
            uow = _uow;
            genericFirebaseKeys = genericFirebaseKey;
            httpClient = new HttpClient();
            exceptionService = Exc;
        }

        public async Task<bool> PostTopicNotification(PostNotificationVM postNotificationVM)
        {
            bool result = false;

            #region Database

            try
            {
                await TopicNotificationProcess(postNotificationVM);
                // BackgroundJob.Enqueue(() => TopicNotificationProcess(postNotificationVM));
                result = true;
            }
            catch (Exception ex)
            {
                var error = new
                {
                    Data = postNotificationVM,
                    Error = ex
                };

                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                result = false;
            }

            #endregion

            return result;
        }

        public async Task TopicNotificationProcess(PostNotificationVM _postNotificationVM)
        {
            try
            {
                List<long> _notificationIds = new List<long>();

                if (_postNotificationVM?.TradesmenList.Count > 0)
                {
                    foreach (var _tradesman in _postNotificationVM?.TradesmenList)
                    {
                        notificationLogging = new NotificationLogging()
                        {
                            SenderId = Guid.Parse(_postNotificationVM.SenderUserId),
                            ReceiverId = Guid.Parse(_tradesman),
                            Type = (int)NotificationType.NewJobPost,
                            CreatedBy = Guid.Parse(_postNotificationVM.SenderUserId),
                            CreatedOn = DateTime.Now,
                            IsRead = false
                        };

                        uow.Repository<NotificationLogging>().Add(notificationLogging);
                        await uow.SaveAsync();

                        _notificationIds.Add(notificationLogging.NotificationLoggingId);
                    }
                }

                TopicNotificationPayload payload = new TopicNotificationPayload()
                {
                    condition = _postNotificationVM.To,
                    notification = new TopicPayload
                    {
                        title = _postNotificationVM.Title,
                        body = _postNotificationVM.Body,
                        sound = "default"
                    },
                    data = new Data
                    {
                        targetActivity = _postNotificationVM.TargetActivity,
                        senderEntityId = _postNotificationVM.SenderEntityId,
                        priority = "high"
                    }
                };

                string payloadJson = JsonConvert.SerializeObject(payload);
                await UpdateDatabaseOnSentNotification(payloadJson, _postNotificationVM, _notificationIds);
                //  BackgroundJob.Enqueue(() => UpdateDatabaseOnSentNotification(payloadJson, _postNotificationVM, _notificationIds));
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(ex)));
            }
        }

        public async Task SaveNotificationDataWeb(PostNotificationVM postNotificationVM)
        {
            bool result = false;
            string payloadJson = string.Empty;
            byte _type = 0;


            try
            {
                switch (postNotificationVM.Title)
                {
                    case NotificationTitles.BidAccepted:
                        _type = (int)NotificationType.BidAccepted;
                        break;

                    case NotificationTitles.BidCostUpdated:
                        _type = (int)NotificationType.BidCostUpdated;
                        break;

                    case NotificationTitles.BidDeclined:
                        _type = (int)NotificationType.BidDeclined;
                        break;

                    case NotificationTitles.BidUpdated:
                        _type = (int)NotificationType.BidUpdated;
                        break;

                    case NotificationTitles.FeedbackRequest:
                        _type = (int)NotificationType.FeedbackRequest;
                        break;

                    case NotificationTitles.JobIsFinished:
                        _type = (int)NotificationType.JobIsFinished;
                        break;

                    case NotificationTitles.JobUpdted:
                        _type = (int)NotificationType.JobUpdted;
                        break;

                    case NotificationTitles.NewBid:
                        _type = (int)NotificationType.NewBid;
                        break;

                    case NotificationTitles.NewCallRequest:
                        _type = (int)NotificationType.NewCallRequest;
                        break;

                    case NotificationTitles.NewFeedbackReceived:
                        _type = (int)NotificationType.NewFeedbackReceived;
                        break;

                    case NotificationTitles.NewJobPost:
                        _type = (int)NotificationType.NewJobPost;
                        break;

                    case NotificationTitles.NewMessage:
                        _type = (int)NotificationType.NewMessage;
                        break;

                    case NotificationTitles.PromoteYourAd:
                        _type = (int)NotificationType.PromoteYourAd;
                        break;

                    case NotificationTitles.YourAdIsPosted:
                        _type = (int)NotificationType.YourAdIsPosted;
                        break;
                    case NotificationTitles.ExpiredAd:
                        _type = (int)NotificationType.ExpiredAd;
                        break;
                    case NotificationTitles.NewOrderPlace:
                        _type = (int)NotificationType.NewOrderPlace;
                        break;
                    case NotificationTitles.OrderStatus:
                        _type = (int)NotificationType.OrderStatus;
                        break;
                }
                NotificationLogging notificationLogging = new NotificationLogging()
                {
                    SenderId = Guid.Parse(postNotificationVM.SenderUserId), //customerId
                    ReceiverId = Guid.Parse(postNotificationVM.TragetUserId),//supplierId
                    CreatedBy = Guid.Parse(postNotificationVM.SenderUserId),//customerUserId
                    CreatedOn = DateTime.Now,
                    IsAborted = false,
                    IsRead = false,
                    IsRecived = false,
                    IsSent = false,
                    Type = _type
                };
                if (postNotificationVM.isFromWeb == true)
                {

                    DataNotificationPayload payload = new DataNotificationPayload()
                    {
                        to = postNotificationVM.To,
                        notification = new DataPayload
                        {
                            notificationId = postNotificationVM.NotificationId + "",
                            title = postNotificationVM.Title,
                            body = postNotificationVM.Body,
                            sound = "default"
                        },
                        data = new Data
                        {
                            senderEntityId = postNotificationVM.SenderEntityId,
                            targetActivity = postNotificationVM.TargetActivity,
                            priority = "high"
                        }
                    };
                    payloadJson = JsonConvert.SerializeObject(payload);
                    notificationLogging.PayLoad = payloadJson;
                    notificationLogging.IsRecived = true;
                    notificationLogging.IsSent = true;

                }
                uow.Repository<NotificationLogging>().Add(notificationLogging);
                await uow.SaveAsync();
                postNotificationVM.NotificationId = notificationLogging.NotificationLoggingId;
            }
            catch (Exception ex)
            {
                var error = new
                {
                    Data = postNotificationVM,
                    Error = ex
                };

                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                result = false;
            }
        }
        public async Task<bool> PostDataNotification(PostNotificationVM postNotificationVM)
        {
            bool result = false;
            string payloadJson = string.Empty;
            byte _type = 0;

            #region Database

            try
            {
                switch (postNotificationVM.Title)
                {
                    case NotificationTitles.BidAccepted:
                        _type = (int)NotificationType.BidAccepted;
                        break;

                    case NotificationTitles.BidCostUpdated:
                        _type = (int)NotificationType.BidCostUpdated;
                        break;

                    case NotificationTitles.BidDeclined:
                        _type = (int)NotificationType.BidDeclined;
                        break;

                    case NotificationTitles.BidUpdated:
                        _type = (int)NotificationType.BidUpdated;
                        break;

                    case NotificationTitles.FeedbackRequest:
                        _type = (int)NotificationType.FeedbackRequest;
                        break;

                    case NotificationTitles.JobIsFinished:
                        _type = (int)NotificationType.JobIsFinished;
                        break;

                    case NotificationTitles.JobUpdted:
                        _type = (int)NotificationType.JobUpdted;
                        break;

                    case NotificationTitles.NewBid:
                        _type = (int)NotificationType.NewBid;
                        break;

                    case NotificationTitles.NewCallRequest:
                        _type = (int)NotificationType.NewCallRequest;
                        break;

                    case NotificationTitles.NewFeedbackReceived:
                        _type = (int)NotificationType.NewFeedbackReceived;
                        break;

                    case NotificationTitles.NewJobPost:
                        _type = (int)NotificationType.NewJobPost;
                        break;

                    case NotificationTitles.NewMessage:
                        _type = (int)NotificationType.NewMessage;
                        break;

                    case NotificationTitles.PromoteYourAd:
                        _type = (int)NotificationType.PromoteYourAd;
                        break;

                    case NotificationTitles.YourAdIsPosted:
                        _type = (int)NotificationType.YourAdIsPosted;
                        break;
                    case NotificationTitles.ExpiredAd:
                        _type = (int)NotificationType.ExpiredAd;
                        break;
                    case NotificationTitles.NewOrderPlace:
                        _type = (int)NotificationType.NewOrderPlace;
                        break;
                    case NotificationTitles.OrderStatus:
                        _type = (int)NotificationType.OrderStatus;
                        break;
                }
                NotificationLogging notificationLogging = new NotificationLogging()
                {
                    SenderId = Guid.Parse(postNotificationVM.SenderUserId), //customerId
                    ReceiverId = Guid.Parse(postNotificationVM.TragetUserId),//supplierId
                    CreatedBy = Guid.Parse(postNotificationVM.SenderUserId),//customerUserId
                    CreatedOn = DateTime.Now,
                    IsAborted = false,
                    IsRead = false,
                    IsRecived = false,
                    IsSent = false,
                    Type = _type
                };
                if (postNotificationVM.isFromWeb == true)
                {

                    DataNotificationPayload payload = new DataNotificationPayload()
                    {
                        to = postNotificationVM.To,
                        notification = new DataPayload
                        {
                            notificationId = postNotificationVM.NotificationId + "",
                            title = postNotificationVM.Title,
                            body = postNotificationVM.Body,
                            sound = "default"
                        },
                        data = new Data
                        {
                            senderEntityId = postNotificationVM.SenderEntityId,
                            targetActivity = postNotificationVM.TargetActivity,
                            priority = "high"
                        }
                    };
                    payloadJson = JsonConvert.SerializeObject(payload);
                    notificationLogging.PayLoad = payloadJson;
                    notificationLogging.IsRecived = true;
                    notificationLogging.IsSent = true;

                }
                uow.Repository<NotificationLogging>().Add(notificationLogging);
                await uow.SaveAsync();
                postNotificationVM.NotificationId = notificationLogging.NotificationLoggingId;
            }
            catch (Exception ex)
            {
                var error = new
                {
                    Data = postNotificationVM,
                    Error = ex
                };

                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                result = false;
            }

            #endregion

            #region Firebase Request
            //if (postNotificationVM.isFromWeb == false)
            //{
            try
            {
                if (postNotificationVM.NotificationId != 0)
                {
                    if (string.IsNullOrWhiteSpace(postNotificationVM.To))
                    {
                        var error = new
                        {
                            Message = "Firebase id is null",
                            Data = postNotificationVM
                        };

                        exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                    }
                    else
                    {
                        DataNotificationPayload payload = new DataNotificationPayload()
                        {
                            to = postNotificationVM.To,
                            notification = new DataPayload
                            {
                                notificationId = postNotificationVM.NotificationId + "",
                                title = postNotificationVM.Title,
                                body = postNotificationVM.Body,
                                sound = "default"
                            },
                            data = new Data
                            {
                                senderEntityId = postNotificationVM.SenderEntityId,
                                targetActivity = postNotificationVM.TargetActivity,
                                priority = "high"
                            }
                        };

                        payloadJson = JsonConvert.SerializeObject(payload);
                        await UpdateDatabaseOnSentNotification(payloadJson, postNotificationVM, null);
                        // BackgroundJob.Enqueue(() => UpdateDatabaseOnSentNotification(payloadJson, postNotificationVM, null));
                        result = true;
                    }
                }
                else
                {
                    var error = new
                    {
                        Message = "Unable to save notification in Database",
                        Data = postNotificationVM
                    };

                    exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                    result = false;
                }
            }
            catch (Exception ex)
            {
                var error = new
                {
                    PayloadString = payloadJson,
                    Data = postNotificationVM,
                    Error = ex
                };

                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                result = false;
            }
            // }

            #endregion

            return result;
        }
        public async Task UpdateDatabaseOnSentNotification(string _payloadJson, PostNotificationVM postNotificationVM, List<long> _notificationIds)
        {
            try
            {
                bool _isNotificationSent = await SendNotification(_payloadJson, postNotificationVM);

                if (_isNotificationSent)
                {
                    if (postNotificationVM.NotificationId > 0)
                    {
                        notificationLogging = uow.Repository<NotificationLogging>().GetById(postNotificationVM.NotificationId);
                        notificationLogging.CreatedOn = DateTime.Now;
                        notificationLogging.PayLoad = _payloadJson;
                        notificationLogging.IsSent = _isNotificationSent;
                        notificationLogging.IsAborted = !_isNotificationSent;
                        notificationLogging.ReasonToAbort = string.IsNullOrWhiteSpace(postNotificationVM.JobAbortReason) ? AbortReason.Null : postNotificationVM.JobAbortReason;
                        notificationLogging.Retries = postNotificationVM.JobRetries;
                        notificationLogging.ModifiedOn = DateTime.Now;
                        notificationLogging.ModifiedBy = Guid.Parse(postNotificationVM.SenderUserId);
                        notificationLogging.IsRead = false;

                        uow.Repository<NotificationLogging>().Update(notificationLogging);
                        uow.Save();
                    }
                    else if (_notificationIds?.Count > 0)
                    {
                        foreach (var _notification in _notificationIds)
                        {
                            notificationLogging = uow.Repository<NotificationLogging>().GetById(_notification);

                            notificationLogging.CreatedOn = DateTime.Now;
                            notificationLogging.PayLoad = _payloadJson;
                            notificationLogging.IsSent = _isNotificationSent;
                            notificationLogging.IsAborted = !_isNotificationSent;
                            notificationLogging.ReasonToAbort = string.IsNullOrWhiteSpace(postNotificationVM.JobAbortReason) ? AbortReason.Null : postNotificationVM.JobAbortReason;
                            notificationLogging.Retries = postNotificationVM.JobRetries;
                            notificationLogging.ModifiedOn = DateTime.Now;
                            notificationLogging.ModifiedBy = Guid.Parse(postNotificationVM.SenderUserId);

                            uow.Repository<NotificationLogging>().Update(notificationLogging);
                            uow.Save();
                        }
                    }
                }
                else
                {
                    //BackgroundJob.Enqueue(() => GetFailedNotifications());
                }
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(ex)));
            }
        }

        public async Task<bool> SendNotification(string _payloadJson, PostNotificationVM postNotificationVM)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(_payloadJson))
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, genericFirebaseKeys.Url)
                {
                    Content = new StringContent(_payloadJson, Encoding.UTF8, "application/json")
                };

                requestMessage.Headers.TryAddWithoutValidation("Authorization", $"key={genericFirebaseKeys.ServerKey}");
                requestMessage.Headers.TryAddWithoutValidation("Sender", $"id={genericFirebaseKeys.SenderId}");

                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                if (responseMessage.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    var error = new
                    {
                        FirebaseResponce = responseMessage,
                        Data = postNotificationVM
                    };

                    exceptionService.AddErrorLog(new Exception(JsonConvert.SerializeObject(error)));
                    result = false;
                }
            }
            else
            {
                return result;
            }

            return result;
        }

        private async void GetFailedNotifications()
        {
            List<NotificationLogging> notificationLoggings = new List<NotificationLogging>();
            notificationLoggings = uow.Repository<NotificationLogging>().GetAll().Where(x => x.IsSent == false && x.Retries < Retries.Three).ToList();

            PostNotificationVM postNotificationVM;

            foreach (var notification in notificationLoggings)
            {
                if (!string.IsNullOrEmpty(notification.PayLoad))
                {
                    postNotificationVM = new PostNotificationVM()
                    {
                        NotificationId = notification.NotificationLoggingId,
                        TargetDatabase = TargetDatabase.Customer,
                        JobRetries = Retries.Zero + 1,
                        JobAbortReason = AbortReason.Null
                    };
                }
                else
                {
                    postNotificationVM = new PostNotificationVM()
                    {
                        NotificationId = notification.NotificationLoggingId,
                        TargetDatabase = TargetDatabase.Customer,
                        JobRetries = Retries.Three,
                        JobAbortReason = AbortReason.LimitReached
                    };
                }
                await UpdateDatabaseOnSentNotification(notification.PayLoad, postNotificationVM, null);
                // BackgroundJob.Enqueue(() => UpdateDatabaseOnSentNotification(notification.PayLoad, postNotificationVM, null));
            }
        }

        public bool UpdateNotificationLogByNotificationId(NotificationLogging notificationLogging)
        {
            NotificationLogging notificationLog = new NotificationLogging()
            {
                CreatedBy = notificationLogging.CreatedBy,
                CreatedOn = notificationLogging.CreatedOn,
                IsAborted = notificationLogging.IsAborted,
                IsRead = notificationLogging.IsRead,
                IsRecived = notificationLogging.IsRecived,
                IsSent = notificationLogging.IsSent,
                ModifiedBy = notificationLogging.ModifiedBy,
                ModifiedOn = notificationLogging.ModifiedOn,
                NotificationLoggingId = notificationLogging.NotificationLoggingId,
                PayLoad = notificationLogging.PayLoad,
                ReadAt = notificationLogging.ReadAt,
                ReasonToAbort = notificationLogging.ReasonToAbort,
                ReceivedAt = notificationLogging.ReceivedAt,
                ReceiverId = notificationLogging.ReceiverId,
                Retries = notificationLogging.Retries,
                SenderId = notificationLogging.SenderId,
                Type = notificationLogging.Type
            };

            uow.Repository<NotificationLogging>().Update(notificationLog);
            uow.Save();
            return true;
        }


        public IQueryable<NotificationLogging> GetNotifications(string userId, string role, int pageNumber)
        {
            try
            {
                IQueryable<NotificationLogging> notifications = uow.Repository<NotificationLogging>().Get(x => x.ReceiverId == Guid.Parse(userId) && !x.PayLoad.Contains("order status")).OrderByDescending(o => o.CreatedOn);
                return notifications;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new List<NotificationLogging>().AsQueryable();
            }
        }

        public List<NotificationLogging> GetAdminNotifications(int pageSize, int pageNumber, string userId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                             new SqlParameter("@pageSize", pageSize),
                             new SqlParameter("@pageNumber",pageNumber),
                             new SqlParameter("@userId",userId)
                             };

                List<NotificationLogging> result = uow.ExecuteReaderSingleDS<NotificationLogging>("Sp_GetNotifications", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new List<NotificationLogging>();
            }
        }
        public List<PostNotificationVM> GetNotificationsByUserId(int pageSize, int pageNumber, string userId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                             new SqlParameter("@pageSize", pageSize),
                             new SqlParameter("@pageNumber",pageNumber),
                             new SqlParameter("@userId",userId)
                             };

                List<PostNotificationVM> result = uow.ExecuteReaderSingleDS<PostNotificationVM>("Sp_GetNotificationsByUserId", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new List<PostNotificationVM>();
            }
        }
        public Response MarkNotificationAsRead(int notificationId)
        {
            try
            {

                Response response = new Response();
                var getById = uow.Repository<NotificationLogging>().GetById((long)notificationId);
                if (getById != null)
                {
                    getById.IsRead = true;
                    uow.Repository<NotificationLogging>().Update(getById);
                    uow.Save();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Data Updated";
                return response;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new Response();
            }
        }

        public double GetNotificationsCount(string userId, string role)
        {
            try
            {
                IQueryable<NotificationLogging> notification = uow.Repository<NotificationLogging>().Get(x => x.ReceiverId == Guid.Parse(userId) && !x.PayLoad.Contains("order status")).OrderByDescending(o => o.CreatedOn);
                double notificationCount = notification.Where(s => s.IsRead == false && s.PayLoad != null).Count();
                //uow.Repository<NotificationLogging>().Get(x => x.ReceiverId == Guid.Parse(userId)).Where(s => s.IsRead == false && s.PayLoad != null).Count();
                return notificationCount;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return 0;
            }
        }
        public double GetHWMallNotificationsCount(string userId, string role)
        {
            try
            {
                IQueryable<NotificationLogging> notification = uow.Repository<NotificationLogging>().Get(x => x.ReceiverId == Guid.Parse(userId) && x.PayLoad.Contains("order status"));
                double notificationCount = notification.Where(s => s.IsRead == false && s.PayLoad != null).Count();
                //uow.Repository<NotificationLogging>().Get(x => x.ReceiverId == Guid.Parse(userId)).Where(s => s.IsRead == false && s.PayLoad != null).Count();
                return notificationCount;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return 0;
            }
        }

        public NotificationLogging GetNotificationLogByNotificationId(long notificationId)
        {
            try
            {
                return uow.Repository<NotificationLogging>().GetById(notificationId);
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new NotificationLogging();
            }
        }

        public List<NotificationLogging> GetNotificationsForOrders(string userId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                   new SqlParameter("@userId",userId)
                };

                List<NotificationLogging> result = uow.ExecuteReaderSingleDS<NotificationLogging>("Sp_GetNotificationsForOrderByUserId", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                exceptionService.AddErrorLog(ex);
                return new List<NotificationLogging>();
            }
        }

    }
}
