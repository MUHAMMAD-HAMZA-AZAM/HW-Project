using HW.CustomerModels;
using HW.Http;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Admin
{
  public interface IAdminNotificationService
  {
    Task<List<PostNotificationVM>> GetAdminNotifications(int pageSize, int pageNumber,string userId);
    Task<List<PostNotificationVM>> GetNotificationsByUserId(int pageSize, int pageNumber,string userId);
    Task<Response> MarkNotificationAsRead(int notificationId);
    Task<List<PostNotificationVM>> GetNotifications(string userId, string role, int pageNumber);
    Task<Customer> GetCustomerById(long customerId);
  }
  public class AdminNotificationService : IAdminNotificationService
  {
    private readonly IHttpClientService httpClient;
    private readonly IExceptionService Exc;
    private readonly ApiConfig _apiConfig;
    public AdminNotificationService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
    {
      this.httpClient = httpClient;
      _apiConfig = apiConfig;
      this.Exc = Exc;
    }

    public async Task<Customer> GetCustomerById(long customerId)
    {
      try
      {
        Customer customer = JsonConvert.DeserializeObject<Customer>
               (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", ""));


        return customer;
      }
      catch (Exception ex)
      {
        Exc.AddErrorLog(ex);
        return new Customer();
      }
    }
    public async Task<List<PostNotificationVM>> GetAdminNotifications(int pageSize, int pageNumber,string userId)
    {
      List<PostNotificationVM> postNotificationVMs = new List<PostNotificationVM>();

      try
      {
        List<NotificationLogging> notificationLogging = JsonConvert.DeserializeObject<List<NotificationLogging>>
                (await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetAdminNotifications}?pageSize={pageSize}&pageNumber={pageNumber}&userId={userId}"));


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
                  CreatedOn = notification.CreatedOn,
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
    public async Task<List<PostNotificationVM>> GetNotificationsByUserId(int pageSize, int pageNumber,string userId)
    {
      //List<PostNotificationVM> postNotificationVMs = new List<PostNotificationVM>();

      try
      {
        List<PostNotificationVM> postNotificationVMs = JsonConvert.DeserializeObject<List<PostNotificationVM>>
                (await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.GetNotificationsByUserId}?pageSize={pageSize}&pageNumber={pageNumber}&userId={userId}"));
        return postNotificationVMs;
      }
      catch (System.Exception ex)
      {
        Exc.AddErrorLog(ex);
        return new List<PostNotificationVM>();
      }

    }    
    public async Task<Response> MarkNotificationAsRead(int notificationId)
    {
      //List<PostNotificationVM> postNotificationVMs = new List<PostNotificationVM>();

      try
      {
        Response response = JsonConvert.DeserializeObject<Response>
                (await httpClient.GetAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.MarkNotificationAsRead}?notificationId={notificationId}"));
        return response;
      }
      catch (System.Exception ex)
      {
        Exc.AddErrorLog(ex);
        return new Response();
      }

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
