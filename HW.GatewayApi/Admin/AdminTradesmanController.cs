using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW.ReportsViewModels;
using HW.Utility;
using HW.GatewayApi.AuthO;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminTradesmanController : AdminBaseController
    {
        private readonly IAdminTradesmanService adminTradesmanService;
        public AdminTradesmanController(IAdminTradesmanService adnTradesmanService_, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminTradesmanService = adnTradesmanService_;
        }

        [HttpPost]
        public async Task<List<AdminTradesmanListVM>> SpGetTradesmanList([FromBody] GenericUserVM genericUserVM)
        {
            return await adminTradesmanService.SpGetTradesmanList(genericUserVM); 
        }

        [HttpGet]

        public async Task<SpTradesmanStats> SpGetTradesmanStats()
        {
            return await adminTradesmanService.SpGetTradesmanStats();
        }

        [HttpGet]
        public async Task<SpBusinessProfileVM> SpGetBusinessDetails(long userId,string role)
        {
            return await adminTradesmanService.SpGetBusinessDetails(userId, role);
        }

        [HttpGet]

        public async Task<List<Tradesman>> GetAllTradesman()
        {
            return await adminTradesmanService.GetLAllTradesman();
        }

        [HttpGet]

        public async Task<List<Tradesman>> GetAllActiveTradesman(bool isOrganisation)
        {
            return await adminTradesmanService.GetAllActiveTradesman(isOrganisation);
        }

        [HttpGet]

        public async Task<List<Tradesman>> GetLAllTradesmanYearlyReport()
        {
            return await adminTradesmanService.GetLAllTradesmanYearlyReport();
        }

        [HttpGet]

        public async Task<List<Tradesman>> GetLAllTradesmanFromToReport([FromQuery] DateTime StartDate, DateTime EndDate)
        {
            return await adminTradesmanService.GetLAllTradesmanFromToReport(StartDate, EndDate);
        }

        [HttpGet]

        public async Task<List<HW.Utility.IdValueVM>> GetSkillsForDropDown()
        {

            return await adminTradesmanService.GetSkillsForDropDown();
        }

        [HttpGet]

        public async Task<List<Skill>> GetSkillList(long skillId)
        {

            return await adminTradesmanService.GetSkillList(skillId);
        }
        [HttpGet]

        public async Task<List<SkillAndSubSkillVM>> GetSkillListAdmin()
        {

            return await adminTradesmanService.GetSkillListAdmin();
        }

        [HttpGet]

        public async Task<List<TradesmanDTO>> GetTradesmanByCategoryReport(string StartDate, string EndDate, string skills ,string tradesman , string city , bool lastActive , string location, string mobile, string cnic , string emailtype,  string mobileType , string activityType, string userType)
        {
            return await adminTradesmanService.GetLAllTradesmanbyCategoryReport(StartDate, EndDate, skills , tradesman ,city , lastActive , location, mobile, cnic ,emailtype , mobileType, activityType, userType);
        }

        [HttpGet]

        public async Task<List<GetInActiveUserVM>> getAllInActiveFromToReport( int pageNumber, int pageSize, string dataOrderBy , string fromDate , string toDate , string city , string selectedUser)
        {
            return await adminTradesmanService.getAllInActiveFromToReport(pageNumber,pageSize, dataOrderBy , fromDate, toDate, city, selectedUser);
        }

        [HttpGet]

        public async Task<List<SecurityRoleItemVM>> GetSecurityRoleItem()
        {
            return await adminTradesmanService.GetSecurityRoleItem();
        }

        [HttpGet]

        public async Task<List<SecurityRoleVM>> GetSecurityRoles()
        {
            return await adminTradesmanService.GetSecurityRoles();
        }

        [HttpGet]

        public async Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId)
        {
            return await adminTradesmanService.GetSecurityRoleDetails(roleId);
        }

        [HttpPost]

        public async Task<bool> AddSecurityRoleDetails([FromBody] List<GetSecurityRoleDetailsVM> detailsVMs )
        {
            return await adminTradesmanService.AddSecurityRoleDetails(detailsVMs);
        }

        [HttpGet]

        public async Task<List<GetAdminUserDetails>> GetAdminUserDetails(int roleId)
        {
            return await adminTradesmanService.GetAdminUserDetails(roleId);
        }

        [HttpPost]

        public async Task<bool> UpdateAdminUserdetails([FromBody] GetAdminUserDetails detailsVMs)
        {
            return await adminTradesmanService.UpdateAdminUserdetails(detailsVMs);
        }

        [HttpGet]

        public async Task<bool> DeleteAdminUser(string userId)
        {
            return await adminTradesmanService.DeleteAdminUser(userId);
        }

        [HttpGet]

        public async Task<List<TradesmanDTO>> GetTradesmanWithDatesReport(string startDate , string endDate)
        {
            return await adminTradesmanService.GetTradesmanLast24HourReport(startDate , endDate);
        }

        [HttpGet]

        public async Task<List<Tradesman>> GetTradesmanAddressList()
        {
            return await adminTradesmanService.GetTradesmanAddressList();
        }

        [HttpPost]

        public async Task<List<TradesmanDTO>> TradesmanByCategory([FromBody] TradesmanByCatVMcs tradesmanByCatVMcs)
        {
            return await adminTradesmanService.TradesmanByCategory(tradesmanByCatVMcs);
        }

        [HttpGet]

        public async Task<Response> CheckSkillAvailability(string skillName, string subSkillName, int skillId , int orderBy)
        {
            return await adminTradesmanService.CheckSkillAvailability(skillName, subSkillName, skillId , orderBy);
        }

        [HttpPost]

        public async Task<Response> CheckOrderAvailability([FromBody] TradesmanCommonVM tradesmanCommonVM)
        {
            return await adminTradesmanService.CheckOrderAvailability(tradesmanCommonVM);
        }

        [HttpPost]

        public async Task<Response> AddNewSkill([FromBody] SkillAndSubSkillVM skill)
        {
            return await adminTradesmanService.AddNewSkill(skill);
        }

        [HttpPost]

        public async Task<Response> AddOrUpdateSubSkill([FromBody] SkillAndSubSkillVM subSkill)
        {
            return await adminTradesmanService.AddOrUpdateSubSkill(subSkill);
        }

        [HttpPost]

        public async Task<Response> UpdateSkill([FromBody] UpdateSkillVM skill)
        {
            return await adminTradesmanService.UpdateSkill(skill);
        }

        [HttpGet]

        public async Task<Response> DeleteSkill(int skillId , int subSkillId)
        {
            return await adminTradesmanService.DeleteSkill(skillId , subSkillId);
        }

        [HttpGet]

        public async Task<List<SkillAndSubSkillVM>> GetSubSkillList()
        {

            return await adminTradesmanService.GetSubSkillList();
        }

        [HttpGet]

        public async Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb(long tradesmanId)
        {
            return await adminTradesmanService.GetBusinessAndPersnalProfileWeb(tradesmanId);
        }

        [HttpPost]

        public async Task<bool> UpdatePersonalDetails([FromBody]PersonalDetailVM personalDetailVM)
        {

            return await adminTradesmanService.UpdatePersonalDetails(personalDetailVM.TradesmanId , personalDetailVM);
        }

        [HttpGet]

        public async Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            return await adminTradesmanService.AddLinkedSalesman(SalesmanId, CustomerId);
        }

        [HttpGet]

        public async  Task<List<PersonalDetailVM>> GetTradesmanByName(string tradesmanName,long tradesmanId,string tradesmanPhoneNo,long jobQuotationId)
        {
            return await adminTradesmanService.GetTradesmanByName(tradesmanName,tradesmanId,tradesmanPhoneNo, jobQuotationId);
        }

        [HttpGet]

        public async Task<Response> BlockTradesman(string tradesmanId, string userId, bool status)
        {
            return await adminTradesmanService.BlockTradesman(tradesmanId, userId, status);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<InvoiceVM>> GetTradesmanPaymentReceipts(long tradesmanId)
        {
            return await adminTradesmanService.GetTradesmanPaymentReceipts(tradesmanId);
        }

    }
}