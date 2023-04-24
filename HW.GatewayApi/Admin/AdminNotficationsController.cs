using HW.CustomerModels;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.NotificationModels;
using HW.NotificationViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Admin
{
  [Produces("application/json")]
  public class AdminNotficationsController : AdminBaseController
  {
    private readonly IAdminNotificationService adminNotificationService;

    public AdminNotficationsController(IAdminNotificationService adminNotificationService_, IUserManagementService userManagementService) : base(userManagementService)
    {
      this.adminNotificationService = adminNotificationService_;
    }

    [HttpGet]
    public async Task<List<PostNotificationVM>> GetAdminNotifications(int pageSize, int pageNumber,string userId)
    {
      return await adminNotificationService.GetAdminNotifications(pageSize,pageNumber,userId);
    }
    [HttpGet]
    public async Task<List<PostNotificationVM>> GetNotificationsByUserId(int pageSize, int pageNumber,string userId)
    {
      return await adminNotificationService.GetNotificationsByUserId(pageSize,pageNumber,userId);
    }    
    public async Task<Response> MarkNotificationAsRead(int notificationId)
    {
      return await adminNotificationService.MarkNotificationAsRead(notificationId);
    }

    public async Task<List<PostNotificationVM>> GetNotifications(int pageNumber)
    {
      UserRegisterVM user = DecodeTokenForUser();
      return await adminNotificationService.GetNotifications(user.Id, user.Role, pageNumber);
    }

    [HttpGet]
    public async Task<Customer> GetCustomerById(long customerId)
    {
      return await adminNotificationService.GetCustomerById(customerId);
    }
  }
}
