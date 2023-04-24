using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.NotificationViewModels;
using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.UserViewModels;
using HW.UserWebViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using PersonalDetailsVM = HW.IdentityViewModels.PersonalDetailsVM;
using HW.TradesmanViewModels;
using HW.GatewayApi.AuthO;
using ProductCategoryVM = HW.ReportsViewModels.ProductCategoryVM;
using HW.SupplierModels.DTOs;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]

    public class SupplierController : BaseController
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.supplierService = supplierService;
        }

        [HttpGet]
        public async Task<List<SuppliersCatagoryVM>> GetAllProductCatagories()
        {
            return await supplierService.GetAllProductCatagories();
        }

        [HttpGet]
        public async Task<List<UserViewModels.ProductCategoryVM>> GetProductCategory()
        {
            return await supplierService.GetProductCategory();
        }


        [HttpPost]
        public async Task<List<CustomerFeedBack>> GetCustomerFeedBackList([FromBody]string data)
        {
          return await supplierService.GetCustomerFeedBackList(data);
        }

        [HttpPost]
        public async Task<Response> UpdateStockLevel([FromBody] string data)
        {
          return await supplierService.UpdateStockLevel(data);
        }

        [HttpPost]
        public async Task<Response> AddCustomerFeedBack([FromBody]string data)
        {
          return await supplierService.AddCustomerFeedBack(data);
        }

        [HttpGet]

        public async Task<ProductCategory_HomeVM> GetProductCategoryDetails(long productCategoryId)
        {
            long customerId = await GetEntityIdByUserId();
            return await supplierService.GetProductCategoryDetails(productCategoryId, customerId);
        }


        [HttpGet]
        public async Task<SupplierShopVM> GetSupplierShop(long supplierAdId)
        {
            return await supplierService.GetSupplierShop(supplierAdId);

        }


        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<SupplierShopWebVM> GetSupplierShopWeb(long supplierId)
        {
            return await supplierService.GetSupplierShopWeb(supplierId);

        }

        [HttpGet]
        public async Task<SupplierDetailsVM> GetSupplierBySupplierId(long supplierId)
        {
            return await supplierService.GetSupplierBySupplierId(supplierId);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<List<SuppliersFeedbackVM>> GetSuppliersFeedbackBySupplierId(long supplierId)
        {
            if (supplierId == 0)
            {
                supplierId = await GetEntityIdByUserId();
            }
            return await supplierService.GetSuppliersFeedbackBySupplierId(supplierId);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Customer, UserRoles.Supplier })]
        public async Task<SupplierProductDetailVM> GetProductDetailsByAdId(long supplierAdId)
        {
            long id = await GetEntityIdByUserId();
            return await supplierService.GetProductDetailsByAdId(supplierAdId);
        }

        [HttpGet]
        public async Task<List<SpotLightAdsVM>> GetSupplierAdsByStatusId(int statusId)
        {
            return await supplierService.GetSupplierAdsByStatusId(statusId);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<SupplierProductDetailVM> GetProductDataByAdId(long supplierAdId)
        {
            long customerId = 0;
            string userId = string.Empty;

            try
            {
                customerId = await GetEntityIdByUserId();
                userId = DecodeTokenForUser()?.Id;
            }
            catch (System.Exception ex)
            {

            }
            return await supplierService.GetProductDataByAdId(supplierAdId, customerId, userId);
        }

        [HttpGet]
        public async Task<MediaVM> GetProductMediaByAdId(long supplierAdId)
        {
            return await supplierService.GetProductMediaByAdId(supplierAdId);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Customer, })]
        public async Task<UserViewModels.ImageVM> GetSupplierAdImageById(long supplierAdImageId)
        {
            return await supplierService.GetSupplierAdImageById(supplierAdImageId);
        }
        
        [HttpGet]
        public async Task<UserViewModels.ImageVM> GetCategoryImageById(long categoryImageId)
        {
            return await supplierService.GetCategoryImageById(categoryImageId);
        }



        [HttpGet]
        //[Permission(new string[] { UserRoles.Supplier })]
        public Task<List<IdValueVM>> GetAllProductSubCatagories(long productId)
        {
            return supplierService.GetAllProductSubCatagories(productId);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public Task<List<IdValueVM>> GetAllProductCategory()
        {
            return supplierService.GetAllProductCategory();
        }
        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetProfileVerification(long? supplierId)
        {
            return await supplierService.GetProfileVerification(supplierId);
        }
    
        [HttpPost]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetPaymentHistory([FromBody] OrderItemVM orderItemVM)
        {
          return await supplierService.GetPaymentHistory(orderItemVM);
        }
        [HttpPost]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetTransactionHistory([FromBody] TransactionsVM transactionsVM)
        {
          return await supplierService.GetTransactionHistory(transactionsVM);
        }
        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<string> GetPaymentDetail(long supplierId,long orderId)
        {
          return await supplierService.GetPaymentDetail(supplierId,orderId);
        }
        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<Response> GetWithdrawalListById(long Id)
        {
          return await supplierService.GetWithdrawalListById(Id);
        }
    
        [HttpPost]
        [Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public Task<bool> AddAllProductSubCatgory([FromBody] SupplierBusinessDetailVM businessDetailVM)
        {
            return supplierService.AddAllProductSubCatgory(businessDetailVM);
        }

        [HttpGet]
        public async Task<List<ManageAdsVM>> GetManageAds()
        {
            // var id = GetEntityIdByUserId();
            return await supplierService.GetManageAds(await GetEntityIdByUserId());

        }

        [HttpGet]
        public async Task<List<InactiveManageAdsVM>> GetInactiveManageAds()
        {
            return await supplierService.GetInactiveManageAds(await GetEntityIdByUserId());
        }

        [HttpGet]
        public async Task<List<HomeProductVM>> GetProductImages()
        {
            return await supplierService.GetProductImages();
        }

        [HttpGet]
        public Task<List<IdValueVM>> GetProductCatogory()
        {
            //var id = await GetEntityIdByUserId();
            return supplierService.GetProductCatogory();
        }

        [HttpPost]
        [Produces("application/json")]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<long> SaveAndUpdateAd([FromBody] PostAdVM postadd)
        {
            postadd.CreatedBy = DecodeTokenForUser().Id;
            //var temp = GetEntityIdByUserId();
            return await supplierService.SaveAndUpdateAd(await GetEntityIdByUserId(), postadd);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public Task<PostAdVM> GetEditAdDetail(long supplierAdsId)
        {
            return supplierService.GetEditAdDetail(supplierAdsId);
        }

        [HttpGet]
        public Task<List<SupplierViewModels.ImageVM>> GetPostAdImagesList(long supplierAdsId)
        {
            return supplierService.GetPostAdImagesList(supplierAdsId);
        }
        [HttpGet]
        public async Task<SupplierViewModels.VideoVM> GetSupplierAdVideoVM(long supplierAdsId)
        {
            return await supplierService.GetSupplierAdVideoVM(supplierAdsId);
        }

        [HttpGet]
        public async Task<ProductCategory_HomeVM> GetProductCategoryDetailWeb(long productCategoryId, long sortId = 0)
        {
            long customerId = 0;
            try
            {
                customerId = await GetEntityIdByUserId();
            }
            catch (System.Exception)
            {

            }
            return await supplierService.GetProductCategoryDetailWeb(productCategoryId, customerId, sortId);
        }

        [HttpGet]
        public async Task<List<SupplierAdVM>> GetAdBySearchWeb(long productCategoryId, string search)
        {
            long customerId = 0;
            try
            {
                customerId = await GetEntityIdByUserId();
            }
            catch (System.Exception)
            {

            }
            return await supplierService.GetAdBySearchWeb(productCategoryId, customerId, search);
        }



        [HttpPost]
        public async Task<List<SupplierAdVM>> GetAdBySubCategoryIdsWeb([FromBody] List<long> subCategoryIds)
        {
            long customerId = 0;
            try
            {
                customerId = await GetEntityIdByUserId();
            }
            catch (System.Exception)
            {

            }
            return await supplierService.GetAdBySubCategoryIdsWeb(subCategoryIds, customerId);
        }


        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<long> GetProductCategoryIdBySupplierID()
        {
            return await supplierService.GetProductCategoryIdBySupplierId(await GetEntityIdByUserId());
        }
        [HttpPost]
        public async Task<Response> DeleteAdVideo(long supplierAdsId)
        {
            return await supplierService.DeleteAdVideo(supplierAdsId);
        }

        [HttpPost]
        public async Task<Response> UpdateAd(PostAdVM postAdVM)
        {
            return await supplierService.UpdateAd(postAdVM, await GetEntityIdByUserId());
        }

        [HttpGet]
        public async Task<List<CallHistoryLogVM>> GetSuppliersCallLog()
        {
            return await supplierService.GetSuppliersCallLog(await GetEntityIdByUserId());
        }

        [HttpPost]
        public async Task<bool> DeleteSuppliersCallLogs([FromBody] List<long> selectedCallLogIds)
        {
            return await supplierService.DeleteSuppliersCallLogs(selectedCallLogIds);
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<UserProfileVM> GetSupplierByUserId()
        {
            UserRegisterVM userVM = DecodeTokenForUser();
            return await supplierService.GetSupplierByUserId(userVM.Id);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<Response> DeleteAd(long supplierAdId)
        {
            return await supplierService.DeleteAd(supplierAdId, await GetEntityIdByUserId());
        }
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] {  UserRoles.Supplier, UserRoles.Admin })]
        public async Task<BusinessProfileVM> GetBusinessProfile()
        {
            return await supplierService.GetBusinessProfile(await GetEntityIdByUserId());
        }

        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<PersonalDetailsVM> GetPersonalInformation()
        {
            return await supplierService.GetPersonalInformation(await GetEntityIdByUserId());
        }
        [HttpGet]
        [Produces("application/json")]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<PersonalDetailsVM> GetPersonalInformationBySupplierId(long supplierId)
        {

            return await supplierService.GetPersonalInformation(supplierId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<SupplierProfileDetailVM> GetBusinessAndPersnalProfileWeb()
        {
            return await supplierService.GetBusinessAndPersnalProfileWeb(await GetEntityIdByUserId());
        }

        [HttpPost]
        public async Task<bool> PostPersonalInformation([FromBody] PersonalDetailsVM model)
        {
            return await supplierService.PostPersonalInformation(await GetEntityIdByUserId(), model);
        }

        [HttpGet]
        [Permission(new string[] {  UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier })]

        public async Task<bool> UpdateSupplierAdsstatus(long supplierAdsId, long supplieradsStatusId, int days)
        {
            return await supplierService.UpdateSupplierAdsstatus(supplierAdsId, supplieradsStatusId, days);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<List<ManageAdsVM>> SpGetActiveAds(int pageNumber, int pageSize)
        {
            return await supplierService.SpGetActiveAds(await GetEntityIdByUserId(), pageNumber, pageSize);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<List<InactiveManageAdsVM>> SpGetInActiveAds(int pageNumber, int pageSize)
        {
            return await supplierService.SpGetInActiveAds(await GetEntityIdByUserId(), pageNumber, pageSize);
        }
        //Permission cannot applied due to request is from sql server
        [HttpPost]
        public async Task ExpiryNotification([FromBody]List<ExpiryNotificationVM> postAdVM)
        {
            await supplierService.ExpiryNotification(postAdVM);
        }

        [HttpPost]
        public async Task SendFeedBackEmail([FromBody] List<SendFeedBackEmail> sendFeedBackEmails)
        {
          await supplierService.SendFeedBackEmail(sendFeedBackEmails);
        }

        [HttpGet]
        [Permission(new string[] {  UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier })]

        public async Task<List<ManageAdsVMWithImage>> SpGetActiveAdsWithImages(int pageNumber, int pageSize)
        {
            return await supplierService.SpGetActiveAdsWithImages(await GetEntityIdByUserId(), pageNumber, pageSize);
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier })]
        public async Task<List<InactiveManageAdsVMWithImages>> SpGetInActiveAdsWithImages(int pageNumber, int pageSize)
        {
            return await supplierService.SpGetInActiveAdsWithImages(await GetEntityIdByUserId(), pageNumber, pageSize);
        }

        [HttpGet]
        public async Task<UserViewModels.VideoVM> GetProductVideoByAdId(long supplierAdId)
        {
            return await supplierService.GetProductVideoByAdId(supplierAdId);
        }

        [HttpGet]
        public async Task<List<MarketSimilarProductsVM>> MarketSimilarProductsVMs(long categoryId, long supplierAdId)
        {
            return await supplierService.MarketSimilarProductsVMs(categoryId, supplierAdId);
        }
        [HttpGet]
        public async Task<List<WebAdsSearch>> WebAdsSearch(string search)
        {
            return await supplierService.WebAdsSearch(search);
        }
        [HttpGet]
        [Permission(new string[] {  UserRoles.Customer })]
        public async Task<List<LandingSearch>> GetAllSubcategory(string search)
        {
            return await supplierService.GetAllSubcategory(search);
        }
        //[HttpGet]
        //public async Task<List<SupplierReportVM>> GetSupplierForReport(System.DateTime StartDate, System.DateTime EndDate, string categories)
        //{
        //    return await supplierService.GetSupplierForReport(StartDate,EndDate,categories);
        //}

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public Task<Response> GetBusinessDetailsStatus()
        {
            return supplierService.GetBusinessDetailsStatus(DecodeTokenForUser()?.Id);
        }
        [HttpGet]
        public async Task<List<ReportsViewModels.ProductCategoryVM>> GetCategoriesForListing(int productCategoryId)
        {
            return await supplierService.GetCategoriesForListing(productCategoryId);
        }
        [HttpGet]
        //[Permission(new string[] {  UserRoles.Customer, UserRoles.Supplier })]
        public async Task<List<SupplierListVM>> GetSupplierList()
        {
            return await supplierService.GetSupplierList();
        }
        [HttpGet]
        public async Task<List<SupplierListVM>> GetSupplierImageList(long imageId)
        {
            return await supplierService.GetSupplierImageList(imageId);
        }
        [HttpGet]
        public async Task<BusinessProfileVM> GetSupplierBusinessProfile(long supplierId)
        {
            return await supplierService.GetSupplierBusinessProfile(supplierId);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Anonymous })]
        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceProducts([FromBody] AdsParameterVM adsParameterVM)
        {
            long customerId = 0;
            try
            {
                customerId = await GetEntityIdByUserId();
            }
            catch (System.Exception)
            {

            }
            adsParameterVM.CustomerId = customerId;

            return await supplierService.GetMarkeetPlaceProducts(adsParameterVM);
        }
        [HttpGet]
        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProducts(int pageSize , int pageNumber)
        {
            try
            {
                long customerId = 0;
                    customerId = await GetEntityIdByUserId();
                return await supplierService.GetMarkeetPlaceTopRatedProducts(pageSize, pageNumber, customerId);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize)
        {
            try
            {
                long customerId = 0;
                customerId = await GetEntityIdByUserId();
                return await supplierService.GetMarkeetPlaceTopRatedProductsforWeb(pageSize, customerId);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Permission(new string[] { UserRoles.Supplier, UserRoles.Admin })]
        public async Task<Response> RelistAd(long SupplierAdsId)
        {
            return await supplierService.RelistAd(SupplierAdsId);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Supplier, UserRoles.Admin })]
        public async Task<List<IdValueVM>> AllSubCategory()
        {
            return await supplierService.AllSubCategory();
        }
        [HttpGet]
        public async Task<bool> UpdateSupplierPublicId(long supplierId, string publicId)
        {
            bool result;
            result = await supplierService.UpdateSupplierPublicId(supplierId, publicId);
            return result;
        }
        [HttpPost]
        //[Permission(new string[] { UserRoles.Anonymous })]
        public async Task<List<GetMarkeetPlaceProducts>> GetMarketPlaceAds([FromBody] AdsParameterVM adsParameterVM)
        {
            return await supplierService.GetMarketPlaceAds(adsParameterVM);
        }

        [HttpGet]
        public async Task<string> GetSupplierWithDetails(long supplierId)
        {
            //long supplierId = 0;
            //supplierId = await GetEntityIdByUserId();
            return await supplierService.GetSupplierWithDetails(supplierId);
        }

        [HttpPost]
        public async Task<string> AddAndUpdateSellerAccount([FromBody] string data)
        {
            return await supplierService.AddAndUpdateSellerAccount(data);
        }

        [HttpPost]
        public async Task<string> AddAndUpdateBusinessAccount([FromBody] string data)
        {
            return await supplierService.AddAndUpdateBusinessAccount(data);
        }

        [HttpGet]
        public async Task<string> GetCountryList()
        {
            return await supplierService.GetCountryList();
        }
        [HttpGet]
        public async Task<List<ProductCategoryVM>> GetCategoriesForAdminListing()
        {
            return await supplierService.GetCategoriesForAdminListing();
        }
        [HttpGet]
        public async Task<List<IdValueDTO>> GetCategoriesNameWithId()
        {
            return await supplierService.GetCategoriesNameWithId();
        }
        [HttpGet]
        public async Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long productCatgoryId)
        {
            return await supplierService.GetProductSubCategoryById(productCatgoryId);

        }
        [HttpPost]
        public async Task<Response> AddUpdateProductAttribute([FromBody] ProductVM productVM)
        {
            return await supplierService.AddUpdateProductAttribute(productVM);
        }
        [HttpPost]
        public async Task<Response> AddUpdateSupplierSlider([FromBody] string data)
        {
            return await supplierService.AddUpdateSupplierSlider(data);
        }
        [HttpPost]
        public async Task<Response> AddUpdateProductCategoryAttribute([FromBody] ProductVM productVM)
        {
            return await supplierService.AddUpdateProductCategoryAttribute(productVM);
        }
        [HttpGet]
        public async Task<string> GetProductAttributeList()
        {
            return await supplierService.GetProductAttributeList();
        }
        [HttpGet]
        public async Task<string> GetSupplierSliderList()
        {
            return await supplierService.GetSupplierSliderList();
        }
        [HttpPost]
        public async Task<string> GetSupplierProductList([FromBody] ProductVM productVM)
        {
            return await supplierService.GetSupplierProductList(productVM);
        }
        [HttpGet]
        public async Task<string> GetSupplierProductDetail(long productId)
        {
            return await supplierService.GetSupplierProductDetail(productId);
        }        
        [HttpGet]
        public async Task<string> GetProductDetailWeb(long productId)
        {
            return await supplierService.GetProductDetailWeb(productId);
        }

        public async Task<List<ProductVM>> GetProductDetailMob(long productId)
        {
          return await supplierService.GetProductDetailMob(productId);
        }

        [HttpPost]
        public async Task<Response> AddUpdateNewVariant([FromBody] ProductVariant productVariant)
        {
            return await supplierService.AddUpdateNewVariant(productVariant);
        }
        [HttpGet]
        public async Task<List<ProductVariant>> GetAllProductVariant()
        {
            return await supplierService.GetAllProductVariant();
        }
        [HttpGet]
        public async Task<string> GetProductCategoryAttributeList()
        {
            return await supplierService.GetProductCategoryAttributeList();
        }
        [HttpGet]
        public async Task<Response> GetProductAttributeListByCategoryId(long categoryId, string categoryLevel)
        {
            return await supplierService.GetProductAttributeListByCategoryId(categoryId, categoryLevel);
        }
        [HttpGet]
        public async Task<Response> GetProductCategoryGroupListById(long subCategoryId)
        {
            return await supplierService.GetProductCategoryGroupListById(subCategoryId);
        }
        [HttpGet]
        public async Task<string> GetTopFiveProductCategory()
        {
          return await supplierService.GetTopFiveProductCategory();
        }
        [HttpPost]
        public async Task<Response> AddNewSupplierProduct([FromBody] AddProductVM addProductVM)
        {
            return await supplierService.AddNewSupplierProduct(addProductVM);
        }
        [HttpPost]
        public async Task<Response> UpdateSupplierProduct([FromBody] AddProductVM addProductVM)
        {
            return await supplierService.UpdateSupplierProduct(addProductVM);
        }        
        [HttpPost]
        public async Task<string> GetHomeProductList([FromBody] AddProductVM addProductVM)
        {
            return await supplierService.GetHomeProductList(addProductVM);
        }
        [HttpPost]
        public async Task<string> GetProductsByCategory([FromBody] ProductVM productVM)
        {
            var result = await supplierService.GetProductsByCategory(productVM);
            return result;
        }
    
        [HttpPost]
        public async Task<string> GetProductsByName([FromBody] ProductVM productVM)
        {
          return await supplierService.GetProductsByName(productVM);
        }
        [HttpPost]
        public async Task<string> GetSupplierProductListWeb([FromBody] ProductVM productVM)
        {
          return await supplierService.GetSupplierProductListWeb(productVM);
        }
        [HttpGet]
        public async Task<string> GetSateList(int? countryId)
        {
            return await supplierService.GetSateList(countryId);
        }
        [HttpGet]
        public async Task<string> GetProductSearchTagsList([FromQuery] string inputText)
        {
            return await supplierService.GetProductSearchTagsList(inputText);
        }
        [HttpGet]
        public async Task<string> GetProductCategories()
        {
            return await supplierService.GetProductCategories();
        }
        [HttpGet]
        public async Task<string> GetCategoryGroupsById(long subCategoryId)
        {
            return await supplierService.GetCategoryGroupsById(subCategoryId);
        }

        [HttpGet]
        public async Task<string> GetAreaList(int? cityId)
        {
            return await supplierService.GetAreaList(cityId);
        }

        [HttpGet]
        public async Task<string> GetLocationList(int areaId)
        {
            return await supplierService.GetLocationList(areaId);
        }
        [HttpGet]
        public async Task<string> GetBanksList()
        {
            return await supplierService.GetBanksList();
        }

        [HttpPost]
        public async Task<string> SaveAndUpdateBankAccountData([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateBankAccountData(data);
        }
        [HttpPost]
        public async Task<string> AddAndUpdateLogo([FromBody] string data)
        {
            return await supplierService.AddAndUpdateLogo(data);
        }


        [HttpGet]
        public async Task<string> GetBankAccountData(long supplierId)
        {
            return await supplierService.GetBankAccountData(supplierId);
        }
        [HttpGet]
        public async Task<string> GetLogoData(long supplierId)
        {
            return await supplierService.GetLogoData(supplierId);
        }
        [HttpGet]
        public async Task<string> GetProfile(long supplierId)
        {
            return await supplierService.GetProfile(supplierId);
        }

        [HttpPost]
        public async Task<string> SaveAndUpdateWhareHouseAddress([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateWhareHouseAddress(data);
        }

        [HttpGet]
        public async Task<string> GetWareHouseAddress(long supplierId)
        {
            return await supplierService.GetWareHouseAddress(supplierId);
        }

        [HttpPost]
        public async Task<string> SaveAndUpdateReturnAddress([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateReturnAddress(data);
        }
        [HttpPost]
        public async Task<string> SaveAndUpdateSocialLinks([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateSocialLinks(data);
        }

        [HttpGet]
        public async Task<string> GetReturnAddress(long supplierId)
        {
            return await supplierService.GetReturnAddress(supplierId);
        }
        [HttpGet]
        public async Task<string> GetSocialLinks(long supplierId , string isSupplierWeb)
        {
            return await supplierService.GetSocialLinks(supplierId, isSupplierWeb);
        }

        [HttpPost]
        public async Task<string> GetOrdersList([FromBody] string data)
        {
            return await supplierService.GetOrdersList(data);
        }
        [HttpPost]
        public async Task<string> GetSalesSummary([FromBody] string data)
        {
          return await supplierService.GetSalesSummary(data);
        }
        [HttpPost]
        public async Task<Response> PlaceOrder([FromBody] PlaceOrderVM orderItemVM)
        {
            return await supplierService.PlaceOrder(orderItemVM);
        }
        [HttpGet]

        public async Task<string> GetOrderDetailById(long orderId)
        {
          return await supplierService.GetOrderDetailById(orderId);
        }

        [HttpGet]
        public async Task<string> GetOrderDetailsById(long orderId,long supplierId)
        {
            return await supplierService.GetOrderDetailsById(orderId,supplierId);
        }        
        [HttpGet]
        public async Task<string> GetSupplierProductById(long productId)
        {
            return await supplierService.GetSupplierProductById(productId);
        }        
        [HttpGet]
        public async Task<string> GetVariantsByProductId(long productId)
        {
            return await supplierService.GetVariantsByProductId(productId);
        }

        [HttpPost]
        public async Task<string> UpdateOrderStatus([FromBody] string data)
        {
            return await supplierService.UpdateOrderStatus(data);
        }


        [HttpPost]
        public async Task<string> GetCustomerOrdersList([FromBody] string data)
        {
            return await supplierService.GetCustomerOrdersList(data);
        }

        [HttpPost]
        public async Task<string> GetCustomerOrderedProductsList([FromBody] string data)
        {
            return await supplierService.GetCustomerOrderedProductsList(data);
        }

        [HttpGet]
        public async Task<string> GetShippingCost(int id)
        {
            var resultData = await supplierService.GetShippingCost(id);
            return resultData;
        }

        [HttpPost]
        public async Task<Response> CancelCustomerOrder([FromBody] string data)
        {
            return await supplierService.CancelCustomerOrder(data);
        }

        [HttpPost]
        public async Task<string> GetCustomerCanclledOrdersList([FromBody] string data)
        {
            return await supplierService.GetCustomerCanclledOrdersList(data);
        }

        [HttpGet]
        public async Task<string> GetOrderCancellationReasonsList(int userRole)
        {
            return await supplierService.GetOrderCancellationReasonsList(userRole);
        }
        public async Task<Response> AddSupplierLeadgerEntry([FromBody] string data)
        {
            return await supplierService.AddSupplierLeadgerEntry(data);
        }
        public async Task<Response> GetLoggedSupplierCanelledOrdersList([FromBody] string data)
        {
            return await supplierService.GetLoggedSupplierCanelledOrdersList(data);
        }
        [HttpGet]
        public async Task<string> GetSupplierShopUrl(string shopUrl)
        {
            return await supplierService.GetSupplierShopUrl(shopUrl);
        }
        [HttpGet]
        public async Task<string> GetSupplierShopDetails(string shopUrl)
        {
            return await supplierService.GetSupplierShopDetails(shopUrl);
        }
        [HttpGet]
        public async Task<string> GetSupplierOrderBySupplierId(long supplierId)
        {
            return await supplierService.GetSupplierOrderBySupplierId(supplierId);
        }
        [HttpPost]
        public async Task<string> AddUpdateFreeShipping([FromBody] string data)
        {
            return await supplierService.AddUpdateFreeShipping(data);
        }
        [HttpPost]
        public async Task<Response> GetFreeShippingList([FromBody] string data)
        {
            return await supplierService.GetFreeShippingList(data);

        }
        [HttpGet]
        public async Task<Response> DeleteFreeShipping(long freeShippingId)
        {
            return await supplierService.DeleteFreeShipping(freeShippingId);
        }


        [HttpPost]
        public async Task<string> GetOrderTracking([FromBody] string data)
        {
            return await supplierService.GetOrderTracking(data);
        }


        [HttpPost]
        public async Task<string> GetOrderedItemTracking([FromBody] string data)
        {
            return await supplierService.GetOrderedItemTracking(data);
        }
        [HttpGet]
        public async Task<Response> GetProductsList(string productName)
        {
            return await supplierService.GetProductsList(productName);
        }
        [HttpPost]
        public async Task<Response> GetProductsByTag([FromBody] ProductVM productVM)
        {
            return await supplierService.GetProductsByTag(productVM);
        }
        [HttpGet]
        public async Task<string> GetAllCatSubCatGroupCategories()
        {
            return await supplierService.GetAllCatSubCatGroupCategories();
        }
    }
}
