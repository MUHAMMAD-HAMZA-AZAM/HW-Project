using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.CustomerModels;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.Services;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW.ReportsViewModels;
using Microsoft.EntityFrameworkCore;
using HW.UserManagmentModels;
using System.Runtime.CompilerServices;
using HW.PackagesAndPaymentsViewModels;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.GatewayApi.AuthO;
using HW.SupplierViewModels;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminCustomerController : AdminBaseController
    {
        private readonly IAdminCustomerServices adminCustomerServices;

        public AdminCustomerController(IAdminCustomerServices adnCustomerServices_, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminCustomerServices = adnCustomerServices_;
        }
        public string User()
        {
            return adminCustomerServices.GetUser();
        }

        [HttpGet]

        public async Task<List<AdminDashboardVM>> SpGetAdminDashBoard()
        {
            return await adminCustomerServices.SpGetAdminDashBoard();
        }

        [HttpGet]
        public async Task<Customer> GetCustomerById(long customerId)
        {
          return await adminCustomerServices.GetCustomerById(customerId);
        }

        [HttpPost]
        public async Task<List<SpPrimaryUserVM>> SpGetPrimaryUsersList([FromBody] GenericUserVM genericUserVM)
        {
            return await adminCustomerServices.SpGetPrimaryUsersList(genericUserVM);
        }

        [HttpGet]

        public async Task<AdminWebUserProfileVM> SpGetUserProfile(string role, long userId)
        {
            string aspUserId = "0";
            if (role == "Admin")
            {
                aspUserId = DecodeTokenForUser().Id;
            }

            return await adminCustomerServices.SpGetUserProfile(role, userId, aspUserId);
        }

        [HttpGet]
        public async Task<List<CustomersDTO>> GetAllCustomers(string startDate , string endDate)
        {
            return await adminCustomerServices.GetAllCustomers(startDate , endDate);
        }

        [HttpGet]
        public async Task<List<Customer>> GetAllCustomersYearlyReport()
        {
            return await adminCustomerServices.GetAllCustomersYearlyReport();
        }

        [HttpGet]

        public async Task<List<Customer>> GetAllCustomersFromToReport([FromQuery] DateTime StartDate, DateTime EndDate)
        {
            return await adminCustomerServices.GetAllCustomersFromToReport(StartDate, EndDate);
        }

        [HttpGet]

        public async Task<List<IdValueVM>> GetAllCustomersDropdown()
        {
            return await adminCustomerServices.GetAllCustomersDropdown();
        }


        [HttpGet]
        public async Task<List<IdValueVM>> GetCityList()
        {
            return await adminCustomerServices.GetCityList();
        }        
        [HttpGet]
        public async Task<string> GetCityListWithTraxCityId()
        {
            return await adminCustomerServices.GetCityListWithTraxCityId();
        }

        [HttpGet]

        public async Task<List<CityAndStateVM>> GetCitiesList()
        {
            return await adminCustomerServices.GetCitiesList();
        }

        [HttpGet]

        public async Task<List<State>> GetStatesList()
        {
            return await adminCustomerServices.GetStatesList();
        }

        [HttpGet]

        public async Task<List<CustomersDTO>> GetCustomersFordaynamicReport(string startDate, string endDate, string customer, string status, string city, 
            bool lastActive, string location , string mobile , string cnic , string emailtype , string mobileType , string userType , string jobsType)
        {
            return await adminCustomerServices.GetRegitredCustomerDaynamicReport(startDate, endDate, customer, status, city, lastActive, location , mobile , cnic , emailtype , mobileType,userType , jobsType);
        }

        [HttpGet]

        public async Task<List<Customer>> GetCustomerAddressList()
        {
            return await adminCustomerServices.GetCustomerAddressList();
        }

        [HttpGet]

        public async Task<Response> BlockCustomer(string customerId,string userId , bool status)
        {
            return await adminCustomerServices.BlockCustomer(customerId, userId,status);
        }

        [HttpGet]

        public async Task<City> CheckCityAvailability(string cityName)
        {
            return await adminCustomerServices.CheckCityAvailability(cityName);
        }

        [HttpPost]

        public async Task<Response> UpdateCity([FromBody] City city)
        {
            return await adminCustomerServices.UpdateCity(city);
        }

        [HttpGet]

        public async Task<Response> DeleteCity(string cityId)
        {
            return await adminCustomerServices.DeleteCity(cityId);
        }

        [HttpPost]

        public async Task<Response> AddNewCity([FromBody] City city)
        {
            return await adminCustomerServices.AddNewCity(city);
        }

        [HttpGet]

        public async Task<List<AllUsersVM>> GetAll()
        {
            return await adminCustomerServices.GetAll();
        }

        [HttpGet]
        public async Task<IdentityViewModels.PersonalDetailsVM> GetPersonalDetails(long customerId)
        {
            return await adminCustomerServices.GetPersonalDetails(customerId);
        }

        [HttpPost]
        public async Task<Response> AddUpdateTown([FromBody] TownVM town)
        {
            return await adminCustomerServices.AddUpdateTown(town);
        }

        [HttpGet]

        public async Task<List<TownVM>> GetAllTown()
        {
            return await adminCustomerServices.GetAllTown();
        }

        [HttpPost]
        public async Task<Response> AddLinkedSalesman([FromBody] SalesmanVM salesmanVM)
        {
            return await adminCustomerServices.AddLinkedSalesman(salesmanVM);
        }
        public async Task<List<JobImages>> GetJobImages(long quotationId)
        {
            return await adminCustomerServices.GetJobImages(quotationId);
        }
        [HttpPost]
        public async Task<Response> AddUpdateSecurityRoleItem([FromBody] SecurityRoleItemVM securityRoleItem)
        {
            return await adminCustomerServices.AddUpdateSecurityRoleItem(securityRoleItem);
        }
        [HttpGet]
        public async Task<Response> DeleteSecurityRoleItem(int securityRoleItemId)
        {
            return await adminCustomerServices.DeleteSecurityRoleItem(securityRoleItemId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<GetPaymentRecordVM>> GetUserPaymentReceipts(long customerId)
        {

            return await adminCustomerServices.GetUserPaymentReceipts(customerId);
        }
    }
}
