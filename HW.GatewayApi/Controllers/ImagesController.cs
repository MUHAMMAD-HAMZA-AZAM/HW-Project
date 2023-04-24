using HW.Gateway.Services;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.ImageModels;
using HW.SupplierViewModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageVM = HW.TradesmanViewModels.ImageVM;

namespace HW.GatewayApi.Controllers
{
    [Produces("text/plain")]
    public class ImagesController : BaseController
    {
        private readonly IImagesService imagesService;
        public ImagesController(IImagesService imagesService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.imagesService = imagesService;
        }

        
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Customer, UserRoles.Tradesman, UserRoles.Admin, UserRoles.Supplier, UserRoles.Organization })]

        public async Task<JobImages> GetJobMainImages(long quotationId)
        {
            return await imagesService.GetJobMainImages(quotationId);
        }

        //[Permission(new string[] { UserRoles.Customer, UserRoles.Tradesman, UserRoles.Admin, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<JobImages>> GetJobImages(long quotationId)
        {
            return await imagesService.GetJobImages(quotationId);
        }

        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> AddUpdateTradesmanProfileImage([FromBody]UpdateTradesmanProfileImageVM updateTradesmanProfileImageVM)
        {
            updateTradesmanProfileImageVM.CreatedBy = DecodeTokenForUser().Id;
            updateTradesmanProfileImageVM.TradesmanId = await GetEntityIdByUserId();

            return await imagesService.AddUpdateTradesmanProfileImage(updateTradesmanProfileImageVM);
        }

        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> AddUpdateUserProfileImage([FromBody]UpdateCustomerProfileImageVM updateCustomerProfileImageVM)
        {
            updateCustomerProfileImageVM.CreatedBy = DecodeTokenForUser().Id;
            updateCustomerProfileImageVM.UserId = await GetEntityIdByUserId();
           
            return await imagesService.AddUpdateUserProfileImage(updateCustomerProfileImageVM);
        }

        
        [HttpPost]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> AddUpdateSupplierProfileImage([FromBody]UpdateSupplierProfileImageVM updateSupplierProfileImageVM)
        {
            updateSupplierProfileImageVM.CreatedBy = DecodeTokenForUser().Id;
            updateSupplierProfileImageVM.SupplierId= await GetEntityIdByUserId();
            return await imagesService.AddUpdateSupplierProfileImage(updateSupplierProfileImageVM);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization , UserRoles.Customer })]
        public async Task<ImageVM> GetJobImageByUrl(string imageUrl)
        {
            return await imagesService.GetJobImageByUrl(imageUrl);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Supplier })]
        public async Task<ImageVM> GetSupplierAdImageByUrl(string imageUrl)
        {
            return await imagesService.GetSupplierAdImageByUrl(imageUrl);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<ImageVM> GetTradesmanProfileImageByTradesmanId(long profileImageId)
        {
            return await imagesService.GetTradesmanProfileImageByTradesmanId(profileImageId);
        }
       
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<ImageVM> GetImageByCustomerId(long customerId)
        {
            return await imagesService.GetImageByCustomerId(customerId);
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<ImageVM> GetMarkeetPlaceProductsImages(long adImageId)
        {
            return await imagesService.GetMarkeetPlaceProductsImages(adImageId);
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<Response> AddSupplierProductImages([FromBody] AddProductVM addProductVM)
        {
            return await imagesService.AddSupplierProductImages(addProductVM);
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<Response> MarkProductImageAsMain([FromBody] ProductImageDTO productImageDTO)
        {
            return await imagesService.MarkProductImageAsMain(productImageDTO);
        }
    }
}