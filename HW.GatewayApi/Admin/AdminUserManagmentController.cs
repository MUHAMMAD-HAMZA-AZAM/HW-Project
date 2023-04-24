using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.SupplierViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminUserManagmentController : AdminBaseController
    {
        private readonly IAdminUserManagmentService adminUserManagmentService;
        public AdminUserManagmentController(IAdminUserManagmentService adminUserManagmentService_, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminUserManagmentService = adminUserManagmentService_;
        }

        [HttpGet]

        public async Task<string> AdminForgotPasswordAuthentication([FromQuery]string email)
        {
            return await adminUserManagmentService.AdminForgotPasswordAuthentication(email);
        }

        [HttpPost]

        public async Task<Response> InsertAndUpDateFAQs([FromBody] Faqs fAQs )
        {
            return await adminUserManagmentService.InsertAndUpDateFAQs(fAQs);

        }

        [HttpGet]

        public async Task<List<Faqs>> GetFAQsList()
        {
            return await adminUserManagmentService.GetFAQsList();

        }

        [HttpPost]

        public async Task<Response> InsertAndUpDateAgreement([FromBody] Agreements agreements )
        {
            return await adminUserManagmentService.InsertAndUpDateAgreement(agreements);
        }        
        [HttpPost]

        public async Task<Response> AddAndUpdateCampaigns([FromBody] CampaignVM campaignsVM )
        {
            return await adminUserManagmentService.AddAndUpdateCampaigns(campaignsVM);
        }

        [HttpGet]

        public async Task<List<Agreements>> GetAgreementsList()
        {
            return await adminUserManagmentService.GetAgreementsList();
        }
        [HttpGet]
        public async Task<List<CampaignVM>> GetCampaignsList()
        {
            return await adminUserManagmentService.GetCampaignsList();
        }

        [HttpPost]
        public async Task<Response> UpdatePersonalDetails([FromBody]IdentityViewModels.PersonalDetailsVM model)
        {
            string userRole = model.UserRole;
            //model.EntityId = model.SupplierId
            //model.UserId = model.UserId
            return await adminUserManagmentService.UpdatePersonalDetails(model, userRole);
        }
        public async Task<Response> AddSupplierBusinessDetails([FromBody]SupplierBusinessDetailVM model)
        {
            var useriD = model.SupplierId; ;
            return await adminUserManagmentService.AddSupplierBusinessDetails(model, useriD);
        }

        [HttpPost]
        public async Task<Response> AddEditTradesmanWithSkills([FromBody]TmBusinessDetailVM model)
        {
           
            return await adminUserManagmentService.AddEditTradesmanWithSkills(model);
        }

        [HttpPost]

        public async Task<Response> AddSalesman([FromBody] SalesmanVM salesmanVM)
        {
            return await adminUserManagmentService.AddSalesman(salesmanVM);
        }

        [HttpGet]
        public async Task<List<SalesmanVM>> GetAllSalesman()
        {
            return await adminUserManagmentService.GetAllSalesman();
        }        
        [HttpPost]
        public async Task<List<SalesmanVM>> LinkedSalesManList([FromBody] SalesmanVM salesmanVM)
        {
            return await adminUserManagmentService.LinkedSalesManList(salesmanVM);
        }
        [HttpGet]
        public async Task<List<TownListVM>> GetTownsByCityId(long cityId)
        {
            return await adminUserManagmentService.GetTownsByCityId(cityId);
        }
        [HttpPost]
        public async Task<Response> InsertAndUpDateActivePromotion([FromBody] ActivePromotionVM activePromotionVM)
        {
            return await adminUserManagmentService.InsertAndUpDateActivePromotion(activePromotionVM);

        }        
        [HttpPost]
        public async Task<Response> AddUpdateToolTipForm([FromBody] TooltipVM tooltipVM)
        {
            return await adminUserManagmentService.AddUpdateToolTipForm(tooltipVM);

        }
        [HttpPost]
        public async Task<Response> AddUpdateToolTipFormDetails([FromBody] TooltipVM tooltipVM)
        {
            return await adminUserManagmentService.AddUpdateToolTipFormDetails(tooltipVM);

        }        
        [HttpPost]
        public async Task<Response> AddAndUpdateApplicationSetting([FromBody] ApplicationSettingVM applicationSettingVM )
        {
            return await adminUserManagmentService.AddAndUpdateApplicationSetting(applicationSettingVM);

        }
        [HttpGet]
        public async Task<List<ApplicationSettingVM>> GetSettingList()
        {
            return await adminUserManagmentService.GetSettingList();
        }
        [HttpPost]
        public async Task<Response> AddAndUpdateApplicationSettingDetails([FromBody] ApplicationSettingVM applicationSettingVM )
        {
            return await adminUserManagmentService.AddAndUpdateApplicationSettingDetails(applicationSettingVM);

        }
        [HttpPost]
        public async Task<Response> UnlinkSalesman([FromBody] SalesmanVM salesmanVM)
        {
            return await adminUserManagmentService.UnlinkSalesman(salesmanVM);

        }
        [HttpGet]
        public async Task<List<ApplicationSettingVM>> GetSettingDetailsList()
        {
            return await adminUserManagmentService.GetSettingDetailsList();
        }
        [HttpGet]
        public async Task<List<ActivePromotionVM>> GetPromotionList()
        {
            return await adminUserManagmentService.GetPromotionList();
        }        
        [HttpGet]
        public async Task<List<TooltipVM>> GetToolTipFormsList()
        {
            return await adminUserManagmentService.GetToolTipFormsList();

        }        
        [HttpGet]
        public async Task<List<TooltipVM>> GetSingleFormDetails(long id)
        {
            return await adminUserManagmentService.GetSingleFormDetails(id);

        }
        [HttpGet]
        public async Task<List<TooltipVM>> GetToolTipFormsDetailsList()
        {
            return await adminUserManagmentService.GetToolTipFormsDetailsList();

        }

        [HttpPost]
        public async Task<Response> CheckUserExist([FromBody] string data)
        {
            return await adminUserManagmentService.CheckUserExist(data);
        }

        [HttpGet]
        public async Task<Response> GetCompaignsTypesListForAdmin()
        {
            return await adminUserManagmentService.GetCompaignsTypesListForAdmin();
        }
        [HttpGet]
        public async Task<Response> GetTimonialsTypesListForAdmin()
        {
            return await adminUserManagmentService.GetTimonialsTypesListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateTestinomials([FromBody] string data)
        {
            return await adminUserManagmentService.AddUpdateTestinomials(data);
        }
        [HttpGet]
        public async Task<Response> GetTestimonialsListForAdmin()
        {
            return await adminUserManagmentService.GetTestimonialsListForAdmin();
        }
        [HttpPost]
        public async Task<Response> DeleteTesimoaialsStatus([FromBody] string data)
        {
            return await adminUserManagmentService.DeleteTesimoaialsStatus(data);
        }
    
    }
}