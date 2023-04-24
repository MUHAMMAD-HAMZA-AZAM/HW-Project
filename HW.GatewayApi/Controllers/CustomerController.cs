using Amazon.S3;
using Amazon.S3.Model;
using HW.CustomerModels;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallHistoryVM = HW.TradesmanViewModels.CallHistoryVM;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IAWSS3FileService aWSS3FileService;

        public CustomerController(IAWSS3FileService aWSS3FileService, ICustomerService customerService, IUserManagementService userManagementService, ICommunicationService communicationService) : base(userManagementService)
        {
            this.customerService = customerService;
            this.aWSS3FileService = aWSS3FileService;
        }

    [HttpGet]
    public async Task<AwsImage> GetFile(string Key)
    {
      return await aWSS3FileService.GetFile(Key);
    }


    //[HttpGet]
    //    public async Task<string> GetAll()
    //    {
    //        return await customerService.GetAll();
    //    }

        [HttpGet]
        public async Task<string> GetById(long id)
        {
            return await customerService.GetById(id);
        }

        [HttpPost]
        public async Task<string> Add([FromBody] Customer data)
        {
            return await customerService.Add(data);
        }

        [HttpDelete]
        public async Task<string> Delete(long id)
        {
            long customerId = await GetEntityIdByUserId();
            return await customerService.Delete(customerId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<BidDetailVM> GetCustomerDetailsById(long id)
        {
            var tradesmanId = await GetEntityIdByUserId();
            return await customerService.GetCustomerDetailsById(tradesmanId, id);
        }

        [HttpGet]
        public async Task<BidDetailVM> GetCustomerDetailsByIdWeb(long id)
        {
            var tradesmanId = await GetEntityIdByUserId();
            return await customerService.GetCustomerDetailsByIdWeb(tradesmanId, id);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Organization })]
        public async Task<BidDetailVM> GetJobDetailsById(long jobDetailId)
        {
            return await customerService.GetJobDetailsById(jobDetailId, await GetEntityIdByUserId());
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<BidDetailVM> GetJobDetailsByIdWeb(long jobDetailId)
        {
            return await customerService.GetJobDetailsByIdWeb(jobDetailId, await GetEntityIdByUserId());
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<FavoritesVM>> GetCustomerFavoriteByCustomerId()
        {
            return await customerService.GetCustomerFavoriteByCustomerId(await GetEntityIdByUserId());
        }

        [HttpGet]
        public async Task<List<IdValueVM>> GetCityList()
        {
            return await customerService.GetCityList();
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> SetTradesmanToCustomerFavorite(long tradesmanId, bool isFavorite)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.SetTradesmanToCustomerFavorite(tradesmanId, customerId, userId, isFavorite);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<CallHistoryVM>> GetJobQuotationCallLogs(long jobQuotationId)
        {
            return await customerService.GetJobQuotationCallLogs(jobQuotationId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<JobQuotationCallInfoVM> GetJobQuotationCallInfo(long tradesmanId, long bidId)
        {
            return await customerService.GetJobQuotationCallInfo(tradesmanId, bidId, await GetEntityIdByUserId());
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.AddDeleteCustomerSavedAd(isSaved, supplierAdId, customerId, userId);
        }
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.AddDeleteCustomerLikedAd(isLiked, supplierAdId, customerId, userId);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> AddCustomerProductRating(int rating, long supplierAdId)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.AddCustomerProductRating(rating, supplierAdId, customerId, userId);
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.AddDeleteCustomerSavedTradesman(isLiked, tradesmanId, customerId, userId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<GetPaymentRecordVM>> getPaymentRecords()
        {

            return await customerService.getPaymentRecords(await GetEntityIdByUserId());
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<Response> DeleteJobQuotation(long jobQuotationId)
        {
            return await customerService.DeleteJobQuotation(jobQuotationId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> UpdateJobQuotationStatus(long jobQuotationId, int statusId)
        {
            return await customerService.UpdateJobQuotationStatus(jobQuotationId, statusId);

        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> AddAdViews(long supplierAdId)
        {
            long customerId = await GetEntityIdByUserId();
            string userId = DecodeTokenForUser().Id;
            return await customerService.AddAdViews(supplierAdId, customerId, userId);
        }


        //[HttpPost]
        ////[Permission(new string[] { UserRoles.Customer })]
        //public async Task<Response> JobQuotation([FromBody] TESTVM quotationVM)
        //{
        //    Response response = new Response();
        //    return response;
        //}
        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> JobQuotation([FromBody] QuotationVM quotationVM)
        {
            quotationVM.UserId = await GetEntityIdByUserId();
            quotationVM.CreatedBy = DecodeTokenForUser().Id;
            return await customerService.JobQuotations(quotationVM);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> UpdateJobQuotation([FromBody] JobQuotationDetailVM quotationDetailVM)
        {
            string createdBy = DecodeTokenForUser().Id;
            return await customerService.UpdateJobQuotation(quotationDetailVM, createdBy);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<UserProfileVM> GetCustomerByUserId(string userId = "")
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                UserRegisterVM userVM = DecodeTokenForUser();
                userId = userVM?.Id;
            }
            return await customerService.GetCustomerByUserId(userId);
        }


        [HttpGet]
        [Produces("application/json")]
        public async Task<SupplierViewModels.CallInfoVM> GetCustomerById(long customerId, bool todaysRecordOnly)
        {
            return await customerService.GetCustomerById(await GetEntityIdByUserId(), customerId, todaysRecordOnly);
        }

        [HttpGet]
        public async Task<PersonalDetailsVM> GetPersonalDetails()
        {
            var customerId = await GetEntityIdByUserId();
            return await customerService.GetPersonalDetails(customerId);
        }
        [HttpGet]
        public async Task<PersonalDetailsVM> GetPersonalDetailsByCustomerId(long customerId)
        {
            return await customerService.GetPersonalDetails(customerId);
        }

        [HttpPost]
        public async Task<bool> UpdatePersonalDetails([FromBody] PersonalDetailsVM personalDetailVM)
        {
            return await customerService.UpdatePersonalDetails(personalDetailVM);
        }

        [HttpPost]
        public async Task AddVideo([FromBody] UserViewModels.VideoVM videoVM)
        {
            await customerService.AddVideo(videoVM, DecodeTokenForUser().Id);
        }


        [HttpPost]
        public async Task<Response> PostJobQuotationWeb([FromBody] QuotationVM jobQuotationVM)
        {
            jobQuotationVM.UserId = await GetEntityIdByUserId();
            jobQuotationVM.CreatedBy = DecodeTokenForUser().Id;
            return await customerService.PostJobQuotationWeb(jobQuotationVM);
        }

        [HttpPost]
        public async Task<Response> Login([FromBody] LoginVM model)
        {
            return await customerService.Login(model);
        }


        [HttpPost]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<Response> JobQuotationsWeb([FromBody] QuotationVM quotationVM)
        {
            //quotationVM.UserId = 341188;
            //quotationVM.CreatedBy = "191cd94c-4293-4397-a5aa-2aed1c9e547e";
            quotationVM.UserId = await GetEntityIdByUserId();
            quotationVM.CreatedBy = DecodeTokenForUser().Id;
            return await customerService.JobQuotationsWeb(quotationVM);
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer, UserRoles.Supplier })]
        public async Task<List<AdViewerVM>> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return await customerService.GetAdViewerList(supplierAdId, pageSize, pageNumber);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer, UserRoles.Supplier })]
        public async Task<List<AdViewerVM>> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return await customerService.GetAdLikerList(supplierAdId, pageSize, pageNumber);
        }
        [HttpGet]
        public async Task<List<AdRatingsVm>> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber)
        {
            return await customerService.GetAdRatedList(supplierAdId, pageSize, pageNumber);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<UserFavoriteTradesmenVM>> GetCustomerFavoriteTradesman(int pageSize, int pageNumber)
        {
            long customerId = await GetEntityIdByUserId();

            return await customerService.GetCustomerFavoriteTradesman(customerId, pageSize, pageNumber);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<List<TopTradesmanVM>> GetTopTradesman(int pageSize, int pageNumber, long CategoryId)
        {
            long customerId = await GetEntityIdByUserId();
            return await customerService.GetTopTradesman(pageSize, pageNumber, CategoryId, customerId);
        }
        [HttpGet]
        public async Task<bool> UpdateCustomerPublicId(long customerId, string publicId)
        {
            bool result;
            result = await customerService.UpdateCustomerPublicId(customerId, publicId);
            return result;
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Customer })]
        public async Task<CustomerDashBoardCountVM> GetCustomerDashBoardCount(long customerId, string userId)
        {
            return await customerService.GetCustomerDashBoardCount(customerId, userId);
        }

        [HttpPost]
        public async Task<string> SaveAndRemoveProductsInWishlist([FromBody] string data)
        {
            string result = await customerService.SaveAndRemoveProductsInWishlist(data);
            return result;
        }

        [HttpGet]
        public async Task<Response> CheckProductStatusInWishList(long customerId,long productId)
        {
            return await customerService.CheckProductStatusInWishList(customerId, productId);
        }

        [HttpGet]
        public async Task<string> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize)
        {
                return await customerService.GetCustomerWishListProducts(customerId,  pageNumber, pageSize);
        }
        [HttpGet]
        public async Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize)
        {
            return await customerService.GetCustomerWishListProductsMobile(customerId,  pageNumber, pageSize);
        }

        [HttpGet]
        public async Task<string> GetUserDetailsByUserRole(string userId, string userRole)
        {
            return await customerService.GetUserDetailsByUserRole( userId, userRole);
        }
        
        [HttpGet]
        public async Task<AwsImage> GetProductImageByFileName(string Key)
        {
            return await aWSS3FileService.GetFile(Key);
        }

    }
}
