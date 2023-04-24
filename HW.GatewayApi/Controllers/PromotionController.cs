using HW.GatewayApi.Services;
using HW.PackagesAndPaymentsModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionService promotiontService;

        public PromotionController(IPromotionService promotiontService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.promotiontService = promotiontService;
        }

        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddEditPromotionReferalCode([FromBody]ReferalCode referalCode)
        {
            Response response = new Response();
            referalCode.ReferredUser = DecodeTokenForUser().Id;
            referalCode.CreatedBy = DecodeTokenForUser().Id;
            response = await promotiontService.AddEditPromotionReferalCode(referalCode);
            return response;
        }
        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddPromotionRedemptions([FromBody]PromotionRedemptions proRedemption)
        {
            Response response = new Response();
            proRedemption.RedeemBy = DecodeTokenForUser().Id;
            proRedemption.CustomerId = await GetEntityIdByUserId();
            response = await promotiontService.AddPromotionRedemptions(proRedemption);
            return response;
        }
        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> AddEditRedemptions([FromBody]Redemptions redemptions)
        {
            Response response = new Response();
            redemptions.RedeemBy = DecodeTokenForUser().Id;
            response = await promotiontService.AddEditRedemptions(redemptions);
            return response;
        }

        [HttpGet]
        public async Task<string> GetReferalByRoleId()
        {
            return await promotiontService.GetReferalByRoleId(DecodeTokenForUser().Id);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode)
        {
            return await promotiontService.GetReferalRecordByreferalCode(referalCode);
        }

        [HttpGet]
        public async Task<string> GetRedemptionById(string userId)
        {
            return await promotiontService.GetRedemptionById(userId);
        }
    }
}
