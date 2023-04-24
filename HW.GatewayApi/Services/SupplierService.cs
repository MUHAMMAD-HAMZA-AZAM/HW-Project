using Hw.EmailViewModel;
using HW.CallModels;
using HW.CommunicationModels;
using HW.CustomerModels;
using HW.Http;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.JobModels;
using HW.NotificationViewModels;
using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.UserWebViewModels;
using HW.Utility;
using HW.VideoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using static HW.Utility.ClientsConstants;
using ImageVM = HW.SupplierViewModels.ImageVM;
using PersonalDetailsVM = HW.IdentityViewModels.PersonalDetailsVM;
using ProductCategory = HW.SupplierModels.ProductCategory;
using Supplier = HW.SupplierModels.Supplier;
using SupplierAds = HW.SupplierModels.SupplierAds;
using VideoVM = HW.SupplierViewModels.VideoVM;
using SupplierAdImage = HW.ImageModels.SupplierAdImage;
using SupplierProfileImage = HW.ImageModels.SupplierProfileImage;
using Microsoft.AspNetCore.Internal;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using HW.PackagesAndPaymentsModels;
using CustomerProfileImage = HW.ImageModels.CustomerProfileImage;
using HW.SupplierModels.DTOs;
using ProductCategoryVM = HW.ReportsViewModels.ProductCategoryVM;
using HW.EmailViewModel;
using HW.PackagesAndPaymentsViewModels;

namespace HW.GatewayApi.Services
{
    public interface ISupplierService
    {
        Task<List<SuppliersCatagoryVM>> GetAllProductCatagories();
        Task<List<UserViewModels.ProductCategoryVM>> GetProductCategory();
        Task<SupplierDetailsVM> GetSupplierBySupplierId(long supplierId);
        Task<List<SuppliersFeedbackVM>> GetSuppliersFeedbackBySupplierId(long supplierId);
        Task<SupplierProductDetailVM> GetProductDetailsByAdId(long supplierAdId);
        Task<ProductCategory_HomeVM> GetProductCategoryDetails(long productCategoryId, long customerId);
        Task<SupplierShopVM> GetSupplierShop(long supplierAdId);
        Task<Response> UpdateStockLevel(string data);
        Task<Response> AddCustomerFeedBack(string data);
        Task<List<CustomerFeedBack>> GetCustomerFeedBackList(string data);
        Task<SupplierShopWebVM> GetSupplierShopWeb(long supplierId);
        Task<List<SpotLightAdsVM>> GetSupplierAdsByStatusId(int statusId);
        Task<List<IdValueVM>> GetAllProductSubCatagories(long productId);
        Task<bool> AddAllProductSubCatgory(SupplierBusinessDetailVM businessDetailVM);
        Task<List<ManageAdsVM>> GetManageAds(long supplierId);
        Task<List<InactiveManageAdsVM>> GetInactiveManageAds(long supplierId);
        Task<List<HomeProductVM>> GetProductImages();
        Task<List<IdValueVM>> GetProductCatogory();
        Task<PostAdVM> GetEditAdDetail(long supplierAdsId);
        Task<IdValueVM> GetProductCatogory(long SupplierId);
        Task<long> GetProductCategoryIdBySupplierId(long id);
        Task<List<IdValueVM>> GetAllProductCategory();
        Task<string> GetProfileVerification(long? supplierId);
        Task<string> GetPaymentHistory(OrderItemVM orderItemVM);
        Task<string> GetPaymentDetail(long supplierId, long orderId);
        Task<Response> DeleteAdVideo(long supplierAdsId);
        Task<Response> UpdateAd(PostAdVM postAdVM, long id);
        Task<List<CallHistoryLogVM>> GetSuppliersCallLog(long supplierId);
        Task<bool> DeleteSuppliersCallLogs(List<long> selectedCallLogIds);
        Task<UserProfileVM> GetSupplierByUserId(string userId);
        Task<long> SaveAndUpdateAd(long id, PostAdVM postadd);
        Task<Response> DeleteAd(long supplierAdId, long supplierId);
        Task<BusinessProfileVM> GetBusinessProfile(long supplierId);
        Task<SupplierProfileDetailVM> GetBusinessAndPersnalProfileWeb(long supplierId);
        Task<PersonalDetailsVM> GetPersonalInformation(long supplierId);
        Task<bool> PostPersonalInformation(long id, PersonalDetailsVM model);
        Task<bool> UpdateSupplierAdsstatus(long supplierAdsId, long supplieradsStatusId, int days);
        Task<SupplierProductDetailVM> GetProductDataByAdId(long supplierAdId, long customerId, string userId);
        Task<MediaVM> GetProductMediaByAdId(long supplierAdId);
        Task<List<MarketSimilarProductsVM>> MarketSimilarProductsVMs(long categoryId, long supplierAdId);
        Task<UserViewModels.ImageVM> GetSupplierAdImageById(long supplierAdImageId);
        Task<UserViewModels.ImageVM> GetCategoryImageById(long supplierAdImageId);
        Task<UserViewModels.VideoVM> GetProductVideoByAdId(long supplierAdId);
        Task<List<ManageAdsVM>> SpGetActiveAds(long supplierId, int pageNumber, int pageSize);
        Task<List<InactiveManageAdsVM>> SpGetInActiveAds(long supplierId, int pageNumber, int pageSize);
        Task<List<ExpiryNotificationVM>> ExpiryNotification(List<ExpiryNotificationVM> postAdVM); 
        Task<List<SendFeedBackEmail>> SendFeedBackEmail(List<SendFeedBackEmail> sendFeedBackEmails);
        Task<List<ManageAdsVMWithImage>> SpGetActiveAdsWithImages(long supplierId, int pageNumber, int pageSize);
        Task<List<InactiveManageAdsVMWithImages>> SpGetInActiveAdsWithImages(long supplierId, int pageNumber, int pageSize);
        Task<List<SupplierViewModels.ImageVM>> GetPostAdImagesList(long supplierAdsId);
        Task<VideoVM> GetSupplierAdVideoVM(long supplierAdsId);
        Task<ProductCategory_HomeVM> GetProductCategoryDetailWeb(long productCategoryId, long customerId, long sortId);
        Task<List<SupplierAdVM>> GetAdBySubCategoryIdsWeb(List<long> subCategoryIds, long customerId);
        Task<List<SupplierAdVM>> GetAdBySearchWeb(long productCategoryId, long customerId, string search);
        Task<List<WebAdsSearch>> WebAdsSearch(string search);
        Task<List<LandingSearch>> GetAllSubcategory(string search);
        Task<Response> GetBusinessDetailsStatus(string id);
        Task<List<ReportsViewModels.ProductCategoryVM>> GetCategoriesForListing(int productCategoryId);
        Task<List<SupplierListVM>> GetSupplierList();
        Task<BusinessProfileVM> GetSupplierBusinessProfile(long supplierId);
        Task<List<SupplierListVM>> GetSupplierImageList(long imageId);
        Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceProducts(AdsParameterVM adsParameterVM);
        Task<Response> GetTransactionHistory(TransactionsVM transactionsVM);
        Task<List<GetMarkeetPlaceProducts>> GetMarketPlaceAds(AdsParameterVM adsParameterVM);
        Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProducts(int pageSize, int pageNumber, long customerId);
        Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize, long customerId);
        //Task<List<SupplierReportVM>> GetSupplierForReport(System.DateTime StartDate, System.DateTime EndDate, string categories);
        Task<Response> RelistAd(long SupplierAdsId);
        Task<List<IdValueVM>> AllSubCategory();
        Task<bool> UpdateSupplierPublicId(long supplierId, string publicId);
        Task<string> GetSupplierWithDetails(long supplierId);
        Task<string> AddAndUpdateSellerAccount(string data);
        Task<Response> GetWithdrawalListById(long id);
        Task<string> AddAndUpdateBusinessAccount(string data);
        Task<string> GetCountryList();
        Task<string> GetSupplierShopUrl(string shopUrl);
        Task<string> GetSupplierShopDetails(string shopUrl);
        Task<List<ProductCategoryVM>> GetCategoriesForAdminListing();
        Task<List<IdValueDTO>> GetCategoriesNameWithId();
        Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long productCatgoryId);
        Task<Response> AddUpdateProductAttribute(ProductVM productVM);
        Task<Response> AddUpdateSupplierSlider(string data);
        Task<Response> AddUpdateProductCategoryAttribute(ProductVM productVM);
        Task<string> GetProductAttributeList();
        Task<string> GetSupplierSliderList();
        Task<string> GetSupplierProductList(ProductVM productVM);
        Task<string> GetSupplierProductDetail(long productId);
        Task<Response> AddUpdateNewVariant(ProductVariant productVariant);
        Task<List<ProductVariant>> GetAllProductVariant();
        Task<string> GetProductCategoryAttributeList();
        Task<Response> GetProductCategoryGroupListById(long subCategoryId);
        Task<string> GetTopFiveProductCategory();
        Task<Response> GetProductAttributeListByCategoryId(long categoryId, string categoryLevel);
        Task<Response> AddNewSupplierProduct(AddProductVM addProductVM);
        Task<Response> UpdateSupplierProduct(AddProductVM addProductVM);
        Task<List<ProductVM>> GetProductDetailMob(long productId);

        Task<string> GetHomeProductList(AddProductVM addProductVM);
        Task<string> GetSupplierProductListWeb(ProductVM productVM);
        Task<string> GetProductsByCategory(ProductVM productVM);
        Task<string> GetProductsByName(ProductVM productVM);
        Task<string> GetProductDetailWeb(long productId);
        Task<string> GetProductCategories();
        Task<string> GetCategoryGroupsById(long subCategoryId);
        Task<string> GetProductSearchTagsList(string inputText);

        Task<string> GetSateList(int? countryId);
        Task<string> GetAreaList(int? cityId);
        Task<string> GetLocationList(int areaId);
        Task<string> GetBanksList();
        Task<string> SaveAndUpdateBankAccountData(string data);
        Task<string> AddAndUpdateLogo(string data);
        Task<string> GetBankAccountData(long supplierId);
        Task<string> GetLogoData(long supplierId);
        Task<string> GetProfile(long supplierId);
        Task<string> SaveAndUpdateWhareHouseAddress(string data);
        Task<string> GetWareHouseAddress(long supplierId);
        Task<string> SaveAndUpdateReturnAddress(string data);
        Task<string> SaveAndUpdateSocialLinks(string data);
        Task<string> GetReturnAddress(long supplierId);
        Task<string> GetSocialLinks(long supplierId, string isSupplierWeb);
        Task<string> GetOrdersList(string data);
        Task<string> GetSalesSummary(string data);
        Task<Response> PlaceOrder(PlaceOrderVM orderItemVM);
        Task<string> GetOrderDetailById(long orderId);
        Task<string> GetOrderDetailsById(long orderId, long supplierId);
        Task<string> GetSupplierProductById(long productId);
        Task<string> GetVariantsByProductId(long productId);
        Task<string> UpdateOrderStatus(string data);
        Task<string> GetCustomerOrdersList(string data);
        Task<string> GetCustomerOrderedProductsList(string data);
        Task<string> GetShippingCost(int id);
        Task<Response> CancelCustomerOrder(string data);
        Task<string> GetCustomerCanclledOrdersList(string data);

        Task<string> GetOrderCancellationReasonsList(int userRole);
        Task<Response> AddSupplierLeadgerEntry(string data);
        Task<Response> GetLoggedSupplierCanelledOrdersList(string data);
        
        Task<string> GetOrderTracking(string data);
        Task<string> GetOrderedItemTracking(string data);
        Task<Response> GetProductsList(string productName);
        Task<string> GetSupplierOrderBySupplierId(long supplierId);
        Task<Response> GetProductsByTag(ProductVM productVM);
        Task<string> GetAllCatSubCatGroupCategories();
        Task<string> AddUpdateFreeShipping(string data);
        Task<Response> GetFreeShippingList(string data);
        Task<Response> DeleteFreeShipping(long freeShippingId);
    }

    public class SupplierService : ISupplierService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public SupplierService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.Exc = Exc;
            _apiConfig = apiConfig;
        }

        public async Task<List<SuppliersCatagoryVM>> GetAllProductCatagories()
        {
            List<SuppliersCatagoryVM> suppliersCatagoryVMs = new List<SuppliersCatagoryVM>();

            try
            {
                List<ProductCategory> productCategory = JsonConvert.DeserializeObject<List<ProductCategory>>
                        (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCatagories}", ""));


                //List<SupplierPcImage> supplierProductImages = JsonConvert.DeserializeObject<List<SupplierPcImage>>
                //    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProductCategoryImages}", ""));


                suppliersCatagoryVMs = productCategory.Select(x => new SuppliersCatagoryVM()
                {

                    CatagoryName = x?.Name,
                    ProductCatagoryId = x?.ProductCategoryId ?? 0

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return suppliersCatagoryVMs;
        }

        public async Task<List<UserViewModels.ProductCategoryVM>> GetProductCategory()
        {
            List<UserViewModels.ProductCategoryVM> productCategoryList = new List<UserViewModels.ProductCategoryVM>();
            try
            {
                List<ImageModels.SupplierPcImage> supplierPcImages = JsonConvert.DeserializeObject<List<ImageModels.SupplierPcImage>>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProductCategoryImages}", ""));
                List<ProductCategory> productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCatagories}", ""));

                productCategoryList = productCategories.Select(p => new UserViewModels.ProductCategoryVM
                {
                    ProductCategoryId = p?.ProductCategoryId ?? 0,
                    ProductCategoryImage = supplierPcImages.Where(s => s.ProductCategoryId == p.ProductCategoryId).FirstOrDefault()?.Pcimage,
                    ProductCategoryName = p?.Name

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return productCategoryList;
        }

        public async Task<SupplierDetailsVM> GetSupplierBySupplierId(long supplierId)
        {
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                        (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierBySupplierId}?supplierId={supplierId}", ""));

                SupplierAds supplierAds = JsonConvert.DeserializeObject<SupplierAds>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsById}?supplierId={supplierId}"));


                City city = JsonConvert.DeserializeObject<City>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={supplier.CityId}"));

                SupplierDetailsVM supplierDetailsVM = new SupplierDetailsVM()
                {
                    SupplierId = supplier.SupplierId,
                    CompanyName = supplier.CompanyName,
                    MobileNumber = supplier.MobileNumber,
                    Address = $"{supplierAds.Address} {supplier.BusinessAddress} {supplier?.State} ,{city.Name}",
                    Email = supplier?.EmailAddress,
                    LatLong = supplier.GpsCoordinates,
                    SupplierAddress = $"{supplier.BusinessAddress.Trim()}, {city.Name}"
                };

                return supplierDetailsVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierDetailsVM();
            }
        }

        public async Task<List<SuppliersFeedbackVM>> GetSuppliersFeedbackBySupplierId(long supplierId)
        {
            List<SuppliersFeedbackVM> suppliersFeedbackVMs = new List<SuppliersFeedbackVM>();

            try
            {
                List<SupplierFeedback> supplierFeedbacks = JsonConvert.DeserializeObject<List<SupplierFeedback>>
                        (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetSupplierFeedbackBySupplierId}?supplierId={supplierId}", ""));

                List<long> customerIds = supplierFeedbacks.Select(x => x.CustomerId).ToList();

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customerIds));

                suppliersFeedbackVMs = supplierFeedbacks.Select(p => new SuppliersFeedbackVM
                {
                    SupplierFeedbackId = p?.SupplierFeedbackId ?? 0,
                    Comments = p?.Comments,
                    CreatedOn = p.ModifiedOn.HasValue ? p.ModifiedOn.Value : p.CreatedOn,
                    CustomerName = $"{customers.FirstOrDefault(x => x.CustomerId == p.CustomerId)?.FirstName} {customers.FirstOrDefault(x => x.CustomerId == p.CustomerId)?.LastName}",
                    Rating = p.OverallRating.HasValue ? p.OverallRating.Value : 0,
                    CustomerId = p.CustomerId

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return suppliersFeedbackVMs;
        }

        public async Task<SupplierProductDetailVM> GetProductDetailsByAdId(long supplierAdId)
        {
            try
            {
                SupplierAds supplierAds = JsonConvert.DeserializeObject<SupplierAds>
                        (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdByAdId}?supplierAdId={supplierAdId}", ""));

                await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierAdViewCount}?supplierAdId={supplierAdId}", "");

                Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                    (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierBySupplierId}?SupplierId={supplierAds.SupplierId}", ""));

                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesByAdId}?supplierAdId={supplierAdId}", ""));

                // SupplierAdVideos supplierAdVideos = JsonConvert.DeserializeObject<SupplierAdVideos>
                //   (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoByAdId}?supplierAdId={supplierAdId}"));

                SupplierProductDetailVM supplierProductDetailVMs = new SupplierProductDetailVM()
                {
                    SupplierAdId = supplierAds?.SupplierAdsId ?? 0,
                    ProductName = supplierAds?.AdTitle,
                    ProductPrice = supplierAds?.Price ?? 0,
                    ProductBy = supplier?.CompanyName,
                    ImageIds = supplierAdImages?.Select(x => x?.AdImageId ?? 0).ToList(),
                    // VideoId = supplierAdVideos?.AdVideoId ?? 0,
                    //VideoName = supplierAdVideos?.VideoName,
                    CollectionAvailable = supplierAds?.IsAvailable ?? false,
                    DeleiveryAvailable = supplierAds?.IsDeliverable ?? false,
                    ProductDescription = supplierAds?.AdDescription,
                    SupplierEmail = supplier?.EmailAddress,
                    SupplierAdReference = supplierAds?.AdReference,
                    MobileNumber = supplier?.MobileNumber
                };

                return supplierProductDetailVMs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierProductDetailVM();
            }
        }

        public async Task<SupplierProductDetailVM> GetProductDataByAdId(long supplierAdId, long customerId, string userId)
        {
            SupplierProductDetailVM supplierProductDetailVM = new SupplierProductDetailVM();
            try
            {
                await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierAdViewCount}?supplierAdId={supplierAdId}", "");

                Response response = JsonConvert.DeserializeObject<Response>
                       (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddAdViews}?supplierAdId={supplierAdId}", ""));

                SupplierAds supplierAds = JsonConvert.DeserializeObject<SupplierAds>
                       (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdByAdId}?supplierAdId={supplierAdId}", ""));
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                    (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierBySupplierId}?SupplierId={supplierAds.SupplierId}", ""));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesByAdId}?supplierAdId={supplierAdId}", ""));

                //SupplierAdVideos supplierAdVideos = JsonConvert.DeserializeObject<SupplierAdVideos>
                //  (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoByAdId}?supplierAdId={supplierAdId}"));
                var supplierSavedAdJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.CheckSavedAdsByadId}?supplierAdId={supplierAdId}&customerId={customerId}", "");
                bool savedAds = JsonConvert.DeserializeObject<bool>(supplierSavedAdJson);

                var supplierLikedAdJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.CheckLikedAdsByadId}?supplierAdId={supplierAdId}&customerId={customerId}", "");
                bool likedAds = JsonConvert.DeserializeObject<bool>(supplierLikedAdJson);
                var supplierRatedAdJson = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.CheckRatedAdsByadId}?supplierAdId={supplierAdId}&customerId={customerId}", "");
                int ratedAds = JsonConvert.DeserializeObject<int>(supplierRatedAdJson);

                JsonConvert.DeserializeObject<Response>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddAdViews}?supplierAdId={supplierAdId}&customerId={customerId}&userId={userId}"));

                int adViewCout = JsonConvert.DeserializeObject<int>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AdViewsCount}?supplierAdId={supplierAdId}"));


                Decimal? DiscountPrice = 0;
                if (!string.IsNullOrEmpty(supplierAds?.Discount))
                {
                    Decimal? total_discount = Convert.ToDecimal(supplierAds.Discount.Replace("%", "")) / 100;
                    DiscountPrice = total_discount * supplierAds.Price;
                }


                supplierProductDetailVM.SupplierAdId = supplierAds?.SupplierAdsId ?? 0;
                supplierProductDetailVM.ProductName = supplierAds?.AdTitle;
                supplierProductDetailVM.ProductPrice = supplierAds?.Price ?? 0;
                supplierProductDetailVM.Discount = supplierAds.Discount;
                supplierProductDetailVM.FinalPrice = supplierAds.Price - DiscountPrice;
                supplierProductDetailVM.ProductBy = supplier?.CompanyName;
                supplierProductDetailVM.ImageIds = supplierAdImages?.Select(x => x?.AdImageId ?? 0).ToList();
                supplierProductDetailVM.ImageNames = supplierAdImages?.Select(x => x?.FileName).ToList();
                //ProductVideo = supplierAdVideos?.AdVideo,
                //supplierProductDetailVM.VideoId = supplierAdVideos?.AdVideoId ?? 0;
                //supplierProductDetailVM.VideoName = supplierAdVideos?.VideoName;
                supplierProductDetailVM.CollectionAvailable = supplierAds?.IsAvailable ?? false;
                supplierProductDetailVM.DeleiveryAvailable = supplierAds?.IsDeliverable ?? false;
                supplierProductDetailVM.ProductDescription = supplierAds?.AdDescription;
                supplierProductDetailVM.SupplierEmail = supplier?.EmailAddress;
                supplierProductDetailVM.SupplierAdReference = supplierAds?.AdReference;
                supplierProductDetailVM.MobileNumber = supplier?.MobileNumber;
                supplierProductDetailVM.AdViews = adViewCout;
                supplierProductDetailVM.ProductId = supplierAds?.ProductCategoryId.Value ?? 0;
                supplierProductDetailVM.SupplierId = supplierAds?.SupplierId ?? 0;
                supplierProductDetailVM.IsSaved = savedAds;
                supplierProductDetailVM.IsLiked = likedAds;
                supplierProductDetailVM.Rating = ratedAds;
                supplierProductDetailVM.CreatedOn = supplierAds.CreatedOn;





                return supplierProductDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierProductDetailVM();
            }
        }

        public async Task<MediaVM> GetProductMediaByAdId(long supplierAdId)
        {
            try
            {
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>
                        (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesByAdId}?supplierAdId={supplierAdId}", ""));

                SupplierAdVideos supplierAdVideos = JsonConvert.DeserializeObject<SupplierAdVideos>
                    (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoByAdId}?supplierAdId={supplierAdId}"));

                MediaVM mediaVM = new MediaVM()
                {
                    Images = supplierAdImages?.Select(i => new UserViewModels.ImageVM { Id = i?.AdImageId ?? 0, FilePath = i?.FileName }).ToList(),
                    Video = new UserViewModels.VideoVM() { FilePath = supplierAdVideos?.VideoName, VideoId = supplierAdVideos?.AdVideoId ?? 0 },

                };

                return mediaVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new MediaVM();
            }
        }

        public async Task<UserViewModels.VideoVM> GetProductVideoByAdId(long supplierAdId)
        {
            try
            {
                SupplierAdVideos supplierAdVideos = JsonConvert.DeserializeObject<SupplierAdVideos>
                        (await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoByAdId}?supplierAdId={supplierAdId}"));

                UserViewModels.VideoVM videoVM = new UserViewModels.VideoVM()
                {
                    FilePath = supplierAdVideos?.VideoName,
                    VideoContent = supplierAdVideos?.AdVideo

                };

                return videoVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.VideoVM();
            }
        }

        public async Task<ProductCategory_HomeVM> GetProductCategoryDetails(long productCategoryId, long customerId)
        {
            try
            {
                List<CustomerSavedAds> customerSavedAds = new List<CustomerSavedAds>();

                List<ProductSubCategory> productSubCategories = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSubCategoriesByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersByIds}", supplierAds.Select(x => x.SupplierId).Distinct().ToList()));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                if (customerId > 0)
                {
                    SavedAdsVM savedAdsVMIds = new SavedAdsVM() { CustomerId = customerId, SupplierAdIds = supplierAds.Select(x => x.SupplierAdsId).ToList() };
                    customerSavedAds = JsonConvert.DeserializeObject<List<CustomerSavedAds>>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerSavedAds}", savedAdsVMIds));
                }
                ProductCategory_HomeVM productCategoryHomeVM = new ProductCategory_HomeVM()
                {
                    SupplierAd = supplierAds?.Select(p => new SupplierAdVM
                    {
                        SupplierAdId = p?.SupplierAdsId ?? 0,
                        SupplierAdTitle = p?.AdTitle,
                        Price = p?.Price ?? 0,
                        AdStatus = p?.AdsStatusId ?? 0,
                        //SupplierAdImage = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImage,
                        AdImageId = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId && x.IsMain.Value)?.AdImageId ?? 0,
                        ImageName = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId && x.IsMain.Value)?.FileName,
                        SupplierCompanyName = suppliers?.FirstOrDefault(x => x?.SupplierId == p?.SupplierId)?.CompanyName,
                        IsSaved = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? true : false,
                        CustomerSavedAdsId = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? customerSavedAds.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId).CustomerSavedAdsId : 0,
                        SubCategoryId = p?.ProductSubCategoryId ?? 0

                    }).ToList(),

                    CategoryList = productSubCategories.Select(x => new CategoryListVM
                    {
                        CategoryId = x.ProductSubCategoryId,
                        CategoryName = x.SubCategoryName
                    }).ToList()
                };

                return productCategoryHomeVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ProductCategory_HomeVM();
            }
        }

        public async Task<ProductCategory_HomeVM> GetProductCategoryDetailWeb(long productCategoryId, long customerId, long sortId)
        {
            try
            {
                List<CustomerSavedAds> customerSavedAds = new List<CustomerSavedAds>();

                List<ProductSubCategory> productSubCategories = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSubCategoriesByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersByIds}", supplierAds.Select(x => x.SupplierId).Distinct().ToList()));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                if (customerId > 0)
                {
                    SavedAdsVM savedAdsVMIds = new SavedAdsVM() { CustomerId = customerId, SupplierAdIds = supplierAds.Select(x => x.SupplierAdsId).ToList() };
                    customerSavedAds = JsonConvert.DeserializeObject<List<CustomerSavedAds>>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerSavedAds}", savedAdsVMIds));
                }
                ProductCategory_HomeVM productCategoryHomeVM = new ProductCategory_HomeVM()
                {
                    SupplierAd = supplierAds?.Select(p => new SupplierAdVM
                    {
                        SupplierAdId = p?.SupplierAdsId ?? 0,
                        SupplierAdTitle = p?.AdTitle,
                        Price = p?.Price ?? 0,
                        AdStatus = p?.AdsStatusId ?? 0,
                        SupplierAdImage = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImage,
                        AdImageId = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImageId ?? 0,
                        ImageName = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.FileName,
                        SupplierCompanyName = suppliers?.FirstOrDefault(x => x?.SupplierId == p?.SupplierId)?.CompanyName,
                        IsSaved = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? true : false,
                        CustomerSavedAdsId = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? customerSavedAds.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId).CustomerSavedAdsId : 0,
                        SubCategoryId = p?.ProductSubCategoryId ?? 0

                    }).ToList(),

                    CategoryList = productSubCategories.Select(x => new CategoryListVM
                    {
                        CategoryId = x.ProductSubCategoryId,
                        CategoryName = x.SubCategoryName
                    }).ToList()
                };


                if (sortId > 0)
                {
                    switch (sortId)
                    {
                        case 1:
                            productCategoryHomeVM.SupplierAd = productCategoryHomeVM.SupplierAd.OrderBy(o => o.Price).ToList();
                            break;

                        case 2:
                            productCategoryHomeVM.SupplierAd = productCategoryHomeVM.SupplierAd.OrderByDescending(o => o.Price).ToList();
                            break;

                        case 3:
                            productCategoryHomeVM.SupplierAd = productCategoryHomeVM.SupplierAd.OrderByDescending(o => o.IsSaved).ToList();
                            break;

                        default:
                            break;
                    }

                }

                return productCategoryHomeVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ProductCategory_HomeVM();
            }
        }

        public async Task<List<SupplierAdVM>> GetAdBySearchWeb(long productCategoryId, long customerId, string search)
        {
            List<SupplierAdVM> supplierAdVM = new List<SupplierAdVM>();
            try
            {

                List<CustomerSavedAds> customerSavedAds = new List<CustomerSavedAds>();

                List<ProductSubCategory> productSubCategories = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSubCategoriesByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsByProductCategoryId}?productCategoryId={productCategoryId}", ""));
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersByIds}", supplierAds.Select(x => x.SupplierId).Distinct().ToList()));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                if (customerId > 0)
                {
                    SavedAdsVM savedAdsVMIds = new SavedAdsVM() { CustomerId = customerId, SupplierAdIds = supplierAds.Select(x => x.SupplierAdsId).ToList() };
                    customerSavedAds = JsonConvert.DeserializeObject<List<CustomerSavedAds>>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerSavedAds}", savedAdsVMIds));
                }

                supplierAdVM = supplierAds?.Select(p => new SupplierAdVM
                {
                    SupplierAdId = p?.SupplierAdsId ?? 0,
                    SupplierAdTitle = p?.AdTitle,
                    Price = p?.Price ?? 0,
                    AdStatus = p?.AdsStatusId ?? 0,
                    SupplierAdImage = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImage,
                    AdImageId = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImageId ?? 0,
                    ImageName = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.FileName,
                    SupplierCompanyName = suppliers?.FirstOrDefault(x => x?.SupplierId == p?.SupplierId)?.CompanyName,
                    IsSaved = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? true : false,
                    CustomerSavedAdsId = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? customerSavedAds.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId).CustomerSavedAdsId : 0,
                    SubCategoryId = p?.ProductSubCategoryId ?? 0

                }).ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    supplierAdVM = supplierAdVM.Where(x => x.SupplierAdTitle.ToLower().Contains(search.ToLower())).Distinct().ToList();

                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return supplierAdVM;
        }

        public async Task<List<SupplierAdVM>> GetAdBySubCategoryIdsWeb(List<long> subCategoryIds, long customerId)
        {
            List<SupplierAdVM> supplierAdVM = new List<SupplierAdVM>();
            try
            {
                List<CustomerSavedAds> customerSavedAds = new List<CustomerSavedAds>();

                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsByProductSubCategoryIds}", subCategoryIds));
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersByIds}", supplierAds.Select(x => x.SupplierId).Distinct().ToList()));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                if (customerId > 0)
                {
                    SavedAdsVM savedAdsVMIds = new SavedAdsVM() { CustomerId = customerId, SupplierAdIds = supplierAds.Select(x => x.SupplierAdsId).ToList() };
                    customerSavedAds = JsonConvert.DeserializeObject<List<CustomerSavedAds>>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerSavedAds}", savedAdsVMIds));
                }

                supplierAdVM = supplierAds?.Select(p => new SupplierAdVM
                {
                    SupplierAdId = p?.SupplierAdsId ?? 0,
                    SupplierAdTitle = p?.AdTitle,
                    Price = p?.Price ?? 0,
                    AdStatus = p?.AdsStatusId ?? 0,
                    SupplierAdImage = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImage,
                    AdImageId = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.AdImageId ?? 0,
                    ImageName = supplierAdImages?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId)?.FileName,
                    SupplierCompanyName = suppliers?.FirstOrDefault(x => x?.SupplierId == p?.SupplierId)?.CompanyName,
                    IsSaved = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? true : false,
                    CustomerSavedAdsId = customerSavedAds?.FirstOrDefault(x => x?.SupplierAdsId == p?.SupplierAdsId) != null ? customerSavedAds.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId).CustomerSavedAdsId : 0,
                    SubCategoryId = p?.ProductSubCategoryId ?? 0

                }).ToList();


                return supplierAdVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return supplierAdVM;
            }
        }

        public async Task<SupplierShopVM> GetSupplierShop(long supplierAdId)
        {
            try
            {
                SupplierAds supplierAd = JsonConvert.DeserializeObject<SupplierAds>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdByAdId}?supplierAdId={supplierAdId}", ""));
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierAd.SupplierId}", ""));
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsBySupplierId}?supplierId={supplier.SupplierId}", ""));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>
                    (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                SupplierShopVM supplierShopVM = new SupplierShopVM()
                {
                    SupplierId = supplier?.SupplierId ?? 0,
                    SupplierCompanyName = supplier?.CompanyName,
                    SupplierAddress = supplier?.BusinessAddress + " " + supplier.State + ", " + cities.FirstOrDefault(x => x.Id == supplier.CityId).Value,
                    SupplierCity = cities.FirstOrDefault(x => x.Id == supplier.CityId).Value,

                    SupplierAds = supplierAds?.Select(p => new ShopSupplierAdVM
                    {
                        AdId = p.SupplierAdsId,
                        AdTitle = p.AdTitle,
                        Price = p.Price,
                        CategoryId = p.ProductCategoryId.HasValue ? p.ProductCategoryId.Value : 0,
                        //AdImage = supplierAdImages?.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId)?.AdImage,
                        AdImageId = supplierAdImages?.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId)?.AdImageId ?? 0,
                        ImageName = supplierAdImages?.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId)?.FileName,

                    }).ToList()
                };


                return supplierShopVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierShopVM();
            }
        }

    public async Task<List<CustomerFeedBack>> GetCustomerFeedBackList(string data)
    {
      try
      {
        return JsonConvert.DeserializeObject<List<CustomerFeedBack>>
                   (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCustomerFeedBackList}", data, ""));
      }
      catch (Exception ex)
      {
        Exc.AddErrorLog(ex);
        return new List<CustomerFeedBack>();
      }

    }

    public async Task<Response> UpdateStockLevel(string data)
        {
            Response response = new Response();
            try
            {
              response = JsonConvert.DeserializeObject<Response>
                   (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateStockLevel}", data, ""));
            }
            catch (Exception ex)
            {
              Exc.AddErrorLog(ex);
              response.Status = ResponseStatus.Error;
            }
            return response;

        }
        public async Task<Response> AddCustomerFeedBack(string data)
        {
            Response response = new Response();
            try
            {
              response = JsonConvert.DeserializeObject<Response>
                   (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddCustomerFeedBack}", data, ""));
            }
            catch (Exception ex)
            {
              Exc.AddErrorLog(ex);
              response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<SupplierShopWebVM> GetSupplierShopWeb(long supplierId)
        {
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierId}", ""));
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsBySupplierId}?supplierId={supplier.SupplierId}", ""));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));

                List<SupplierFeedback> supplierFeedbacks = JsonConvert.DeserializeObject<List<SupplierFeedback>>
                       (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetSupplierFeedbackBySupplierId}?supplierId={supplier.SupplierId}", ""));

                List<long> customerIds = supplierFeedbacks.Select(x => x.CustomerId).ToList();

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customerIds));

                ImageModels.SupplierProfileImage profileImage = JsonConvert.DeserializeObject<ImageModels.SupplierProfileImage>(
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProfileImageBySupplierId}?supplierId={supplier.SupplierId}")
                    );

                List<CustomerProfileImage> customerProfileImages = JsonConvert.DeserializeObject<List<CustomerProfileImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetCustomerProfileImageList}", customers.Select(x => x.CustomerId).ToList()));

                List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>
                    (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));


                SupplierShopWebVM supplierShopWebVM = new SupplierShopWebVM()
                {
                    SupplierId = supplier?.SupplierId ?? 0,
                    SupplierCompanyName = supplier?.CompanyName,
                    SupplierAddress = supplier?.BusinessAddress + " " + supplier.State + ", " + cities.FirstOrDefault(x => x.Id == supplier.CityId).Value,
                    ContactNo = supplier?.MobileNumber,
                    Email = supplier?.EmailAddress,
                    LatLong = supplier?.GpsCoordinates,
                    SupplierImage = profileImage?.ProfileImage,

                    SupplierAds = supplierAds?.Select(p => new ShopSupplierAdVM
                    {
                        AdId = p.SupplierAdsId,
                        AdTitle = p.AdTitle,
                        Price = p.Price,
                        AdImage = supplierAdImages?.FirstOrDefault(x => x.SupplierAdsId == p.SupplierAdsId)?.AdImage,
                        SupplierCompanyName = supplier?.CompanyName,

                    }).ToList(),

                    SupplierFeedbacks = supplierFeedbacks.Select(p => new SuppliersFeedbackVM
                    {
                        SupplierFeedbackId = p?.SupplierFeedbackId ?? 0,
                        Comments = p?.Comments,
                        CreatedOn = p.CreatedOn,
                        CustomerProfileImage = customerProfileImages?.FirstOrDefault(x => x.CustomerId == p.CustomerId)?.ProfileImage,
                        CustomerName = $"{customers.FirstOrDefault(x => x.CustomerId == p.CustomerId)?.FirstName} {customers.FirstOrDefault(x => x.CustomerId == p.CustomerId)?.LastName}",
                        Rating = p.OverallRating.HasValue ? p.OverallRating.Value : 0
                    }).ToList()
                };


                return supplierShopWebVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierShopWebVM();
            }
        }

        public async Task<UserViewModels.ImageVM> GetSupplierAdImageById(long supplierAdImageId)
        {
            try
            {
                SupplierAdImage supplierAdImage = JsonConvert.DeserializeObject<SupplierAdImage>
                       (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImageById}?supplierAdImageId={supplierAdImageId}", ""));

                UserViewModels.ImageVM imageVM = new UserViewModels.ImageVM()
                {
                    FilePath = supplierAdImage?.FileName,
                    ImageContent = supplierAdImage?.AdImage
                };


                return imageVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.ImageVM();
            }
        }
        public async Task<UserViewModels.ImageVM> GetCategoryImageById(long categoryImageId)
        {
            try
            {
                UserViewModels.ImageVM categoryImage = JsonConvert.DeserializeObject<UserViewModels.ImageVM>
                       (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoryImageById}?categoryImageId={categoryImageId}", ""));

                return categoryImage;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.ImageVM();
            }
        }

        public async Task<List<SpotLightAdsVM>> GetSupplierAdsByStatusId(int statusId)
        {
            List<SpotLightAdsVM> supplierShopVM = new List<SpotLightAdsVM>();
            try
            {
                List<SupplierAds> supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsByStatusId}?statusId={statusId}", ""));
                List<SupplierAdImage> supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierAds.Select(x => x.SupplierAdsId).ToList()));


                supplierShopVM = supplierAds.Select(a => new SpotLightAdsVM
                {

                    SupplierAdId = a.SupplierAdsId,
                    SupplierAdTitle = a?.AdTitle,
                    SupplierAdImage = supplierAdImages.FirstOrDefault(x => x.SupplierAdsId == a.SupplierAdsId)?.AdImage

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return supplierShopVM;
        }

        public async Task<List<IdValueVM>> GetAllProductSubCatagories(long productId)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ProductSubCategory>>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductSubCatagories}?productId={productId}")
                   ).Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<bool> AddAllProductSubCatgory(SupplierBusinessDetailVM businessDetailVM)
        {
            try
            {
                bool jsonResult = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddRegistrationDetail}", businessDetailVM));

                return jsonResult;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<List<IdValueVM>> AllSubCategory()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ProductSubCategory>>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AllSubCategory}")
                   ).Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<List<ManageAdsVM>> GetManageAds(long supplierId)
        {
            List<ManageAdsVM> manageAdsVMs = new List<ManageAdsVM>();
            try
            {
                Supplier supplier = new Supplier();
                List<SupplierAds> supplierAds = new List<SupplierAds>();
                List<SupplierAdImage> supplierAdImages = new List<SupplierAdImage>();

                supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSupplier}?supplierId={supplierId}", ""));
                supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSupplierAds}?supplierId={supplierId}", ""));


                List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                List<long> supplierIds = supplierAds.Select(x => x.SupplierAdsId).ToList();
                List<IdValueVM> productSubCategories = await AllSubCategory();
                supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierIds));

                List<SupplierAds> filteredAds = supplierAds.Where(x => x.ActiveTo > DateTime.Now).ToList();

                manageAdsVMs = filteredAds?.Select(x => new ManageAdsVM
                {
                    AdTitle = x?.AdTitle,
                    Price = x?.Price ?? 0,
                    ActiveFrom = x.ActiveFrom,
                    ActiveTo = x.ActiveTo,
                    SubCategoryValue = productSubCategories.Where(s => s.Id == x.ProductSubCategoryId).Select(s => s.Value).FirstOrDefault(),
                    AdsStatusId = x?.AdsStatusId ?? 0,
                    City = string.IsNullOrWhiteSpace(cities.Where(c => c.Id == x.CityId).Select(c => c?.Value).FirstOrDefault()) ? "" : cities.Where(c => c.Id == x.CityId).Select(c => c?.Value).FirstOrDefault(),
                    SupplierAdsId = x?.SupplierAdsId ?? 0,
                    //BusinessAddress = $"{x?.Town}  {x?.Address}",
                    AdViewCount = x.AdViewCount.HasValue ? x.AdViewCount.Value : 0,
                    AdImageName = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain == true)?.FileName,
                    //AdImage = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain==true)?.AdImage,
                    //TotalDay =  (x.ActiveTo.Date.Day- x.ActiveFrom.Day)
                    //TotalDay = (x.ActiveTo - x.ActiveFrom).Days

                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return manageAdsVMs;
        }

        public async Task<List<InactiveManageAdsVM>> GetInactiveManageAds(long supplierId)
        {
            List<InactiveManageAdsVM> manageAdsVMs = new List<InactiveManageAdsVM>();
            try
            {
                InactiveManageAdsVM manageAds = new InactiveManageAdsVM();
                Supplier supplier = new Supplier();
                List<SupplierAds> supplierAds = new List<SupplierAds>();
                List<SupplierAdImage> supplierAdImages = new List<SupplierAdImage>();
                supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSupplierAds}?supplierId={supplierId}", ""));
                supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSupplier}?supplierId={supplierId}", ""));
                List<IdValueVM> productSubCategories = await AllSubCategory();

                List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                List<long> supplierIds = supplierAds.Where(x => x.SupplierId == supplierId).Select(x => x.SupplierAdsId).ToList();

                supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierIds));

                List<SupplierAds> filteredAds = supplierAds.Where(x => x.ActiveTo < DateTime.Now).ToList();

                manageAdsVMs = filteredAds?.Select(x => new InactiveManageAdsVM
                {
                    AdTitle = x?.AdTitle,
                    Price = x?.Price ?? 0,
                    ActiveFrom = x.ActiveFrom,
                    ActiveTo = x.ActiveTo,
                    SubCategoryValue = productSubCategories.Where(s => s.Id == x.ProductSubCategoryId).Select(s => s.Value).FirstOrDefault(),
                    City = string.IsNullOrWhiteSpace(cities.Where(c => c.Id == x.CityId).Select(c => c.Value).FirstOrDefault()) ? "" : cities.Where(c => c.Id == x.CityId).Select(c => c.Value).FirstOrDefault(),
                    //BusinessAddress = $"{x?.Town} {x?.Address}",
                    SupplierAdsId = x?.SupplierAdsId ?? 0,
                    AdImageName = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain == true)?.FileName,
                    //AdImage = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain==true)?.AdImage,
                    //TotalDay = (DateTime.Now - x.ActiveTo).Days
                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return manageAdsVMs;
        }

        public async Task<List<HomeProductVM>> GetProductImages()
        {
            List<HomeProductVM> AllhomeProduct = new List<HomeProductVM>();

            try
            {
                List<ProductCategory> productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>
                        (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCatagories}", ""));

                //List<long> productCategoryId = productCategories.Select(s => s.ProductCategoryId).Distinct().ToList();

                //List<SupplierPcImage> supplierProductImages = JsonConvert.DeserializeObject<List<SupplierPcImage>>
                //    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierProductImageByProductCategoryId}", productCategoryId));

                AllhomeProduct = productCategories.Select(x => new HomeProductVM() { ProductId = x.ProductCategoryId, ProductName = x.Name }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return AllhomeProduct;
        }

        public async Task<List<IdValueVM>> GetProductCatogory()
        {
            List<IdValueVM> idValue = new List<IdValueVM>();

            try
            {
                List<ProductCategory> productCategory = JsonConvert.DeserializeObject<List<ProductCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCatagories}")).ToList();

                idValue = productCategory.Select(x => new IdValueVM
                {
                    Id = x.ProductCategoryId,
                    Value = x.Name


                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            return idValue;
        }

        public async Task<long> SaveAndUpdateAd(long id, PostAdVM postadd)
        {
            try
            {
                long supplierAdId = 0;
                SupplierAds supplierAds = new SupplierAds
                {
                    SupplierId = id,
                    ProductCategoryId = postadd.ProductCategoryId,
                    ProductSubCategoryId = postadd.ProductSubcategoryId,
                    AdTitle = postadd.PostTitle,
                    AdDescription = postadd.PostDiscription,
                    Price = postadd.Price,
                    //Discount = postadd?.Discount?.Replace("%", ""),
                    Discount = postadd.Discount,
                    Town = postadd.Town,
                    AdReference = RandomAdsRefrence(),
                    Address = postadd.Address,
                    CityId = postadd.CityId,
                    SupplierAdsId = postadd.SupplierAdId,
                    IsDeliverable = postadd.DeliveryAvailable,
                    IsAvailable = postadd.CollectionAvailable,
                    CreatedBy = postadd.CreatedBy,
                    ActiveTo = postadd.ActiveTo,
                    ActiveFrom = postadd.ActiveFrom,
                    AdsStatusId = (int)postadd.StatusId,
                    Active = true
                };

                if (id > 0)
                {
                    supplierAdId = JsonConvert.DeserializeObject<long>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl }{ApiRoutes.Supplier.SaveAndUpdateAd}", supplierAds));

                }
                if (supplierAdId > 0)
                {
                    try
                    {
                        if (postadd.ImageVMs?.Count > 0)
                        {
                            List<SupplierAdImage> supplierAdImagesList = new List<SupplierAdImage>();
                            foreach (ImageVM item in postadd?.ImageVMs ?? new List<ImageVM>())
                            {
                                if (!string.IsNullOrWhiteSpace(item.ImageBase64))
                                {
                                    var checkFormatList = item.ImageBase64.Split(',');
                                    //For Angular Web UserProfile Image
                                    if (checkFormatList[0] == "data:image/png;base64")
                                    {
                                        string convert = item.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                                        item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                    }
                                    else if (checkFormatList[0] == "data:image/jpg;base64")
                                    {
                                        string convert = item.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                                        item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                    }
                                    else if (checkFormatList[0] == "data:image/jpeg;base64")
                                    {
                                        string convert = item.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                                        item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                    }

                                }
                                var thumbImage = (dynamic)null;
                                if (item.ThumbImage != null)
                                {
                                    var splitName = item.ThumbImage.Split(',');
                                    string convert = item.ThumbImage.Replace(splitName[0] + ",", String.Empty);
                                    thumbImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];

                                }
                                else
                                {
                                    thumbImage = null;
                                }
                                SupplierAdImage supplierAdImage = new SupplierAdImage
                                {
                                    AdImage = item.ImageContent,
                                    FileName = $"img-{DateTime.Now}",
                                    SupplierAdsId = postadd.SupplierAdId != 0 ? postadd.SupplierAdId : supplierAdId,
                                    IsMain = item.IsMain,
                                    CreatedBy = postadd.CreatedBy,
                                    CreatedOn = DateTime.Now,
                                    ThumbnailImage = thumbImage,
                                };
                                supplierAdImagesList.Add(supplierAdImage);
                            }
                            string supplierAdsImagesJson = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.SubmitAndUpdateSupplierAdImages}", supplierAdImagesList);

                        }
                    }
                    catch (Exception ex)
                    {

                        Exc.AddErrorLog(ex);
                    }
                    try
                    {
                        if (postadd.VideoVM != null && postadd.VideoVM.VideoContent?.Length > 0)
                        {
                            SupplierAdVideos supplierAdVideo = new SupplierAdVideos
                            {
                                CreatedBy = postadd.CreatedBy,
                                CreatedOn = DateTime.Now,
                                VideoName = postadd.VideoVM.FilePath,
                                AdVideo = postadd.VideoVM.VideoContent,
                                SupplierAdsId = postadd.SupplierAdId != 0 ? postadd.SupplierAdId : supplierAdId
                            };
                            string submitAndUpdateSupplierVideo = await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.SubmitAndUpdateAdVideo }", supplierAdVideo);
                        }

                    }
                    catch (Exception ex)
                    {
                        Exc.AddErrorLog(ex);
                    }
                }
                #region Sending Post Ad Email 

                var productCategoryName = JsonConvert.DeserializeObject<string>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoryName}?productId={postadd.ProductCategoryId}", ""));
                var supplierDetail = await GetPersonalInformation(id);

                PostSupplierAdsEmail postSupplierAdsEmail = new PostSupplierAdsEmail
                {
                    address = postadd.Address,
                    adRefrenceNumber = supplierAds.AdReference,
                    adTitle = postadd.PostTitle,
                    category = productCategoryName,
                    supplierName = supplierDetail.FirstName + " " + supplierDetail.LastName,
                    email_ = supplierDetail.Email,
                    Email = new Email
                    {
                        CreatedBy = supplierDetail.UserId,
                        IsSend = false,
                        Retries = 0,
                    }
                };
                var result = JsonConvert.DeserializeObject<bool>(await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.PostAdEmail}", postSupplierAdsEmail));


                #endregion
                return supplierAdId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public async Task<PostAdVM> GetEditAdDetail(long supplierAdsId)
        {
            PostAdVM manageAdsVM = new PostAdVM();
            try
            {
                string supplierAdsJson = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetEditAdDetail}?supplierAdsId={supplierAdsId}", "");
                SupplierAds supplierAds = JsonConvert.DeserializeObject<SupplierAds>(supplierAdsJson);
                //string supplierAdVideoJson = await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoNameByAdId}?supplierAdId={supplierAdsId}", "");
                //var path = JsonConvert.DeserializeObject<string>(supplierAdVideoJson);


                List<City> cities = JsonConvert.DeserializeObject<List<City>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                if (supplierAds != null)
                {
                    manageAdsVM.SupplierAdId = supplierAds.SupplierAdsId;
                    manageAdsVM.StatusId = supplierAds.AdsStatusId != 0 ? supplierAds.AdsStatusId : 0;
                    manageAdsVM.PostTitle = string.IsNullOrWhiteSpace(supplierAds.AdTitle) ? "" : supplierAds.AdTitle;
                    manageAdsVM.Price = supplierAds.Price != 0 ? Math.Round(supplierAds.Price, 2) : 0;
                    manageAdsVM.PostDiscription = string.IsNullOrWhiteSpace(supplierAds.AdDescription) ? "" : supplierAds.AdDescription;
                    manageAdsVM.CityId = supplierAds?.CityId ?? 0;
                    manageAdsVM.Town = string.IsNullOrWhiteSpace(supplierAds.Town) ? "" : supplierAds.Town;
                    manageAdsVM.Address = string.IsNullOrWhiteSpace(supplierAds.Address) ? "" : supplierAds.Address;
                    manageAdsVM.Discount = supplierAds.Discount;
                    manageAdsVM.DeliveryAvailable = supplierAds.IsDeliverable;
                    manageAdsVM.CollectionAvailable = supplierAds.IsAvailable;
                    manageAdsVM.ActiveFrom = supplierAds.ActiveFrom;
                    manageAdsVM.ActiveTo = supplierAds.ActiveTo;
                    manageAdsVM.ProductCategoryId = supplierAds.ProductCategoryId.Value;
                    manageAdsVM.ProductSubcategoryId = supplierAds.ProductSubCategoryId != 0 ? supplierAds.ProductSubCategoryId : 0;
                    //manageAdsVM.VideoPath = path;

                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return manageAdsVM;
        }

        public async Task<long> GetProductCategoryIdBySupplierId(long id)
        {
            try
            {
                return JsonConvert.DeserializeObject<long>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductCategoryIdBySupplierID}?supplierId={id}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }
        public async Task<string> GetOrderDetailById(long orderId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderDetailById}?orderId={orderId}", "");
        }
        public async Task<List<IdValueVM>> GetAllProductCategory()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ProductCategory>>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCategory}", "")
                    ).Select(p => new IdValueVM { Id = p.ProductCategoryId, Value = p.Name }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }
        public async Task<string> GetProfileVerification(long? supplierId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProfileVerification}?supplierId={supplierId}", "");

        }
        public async Task<string> GetPaymentHistory(OrderItemVM orderItemVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetPaymentHistory}", orderItemVM, "");
        }
        public async Task<string> GetPaymentDetail(long supplierId, long orderId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetPaymentDetail}?supplierId={supplierId}&orderId={orderId}", "");
        }

        public async Task<List<CallHistoryLogVM>> GetSuppliersCallLog(long supplierId)
        {
            List<CallHistoryLogVM> callHistoryLogVMs = new List<CallHistoryLogVM>();

            try
            {
                List<SupplierCallLog> supplierCallLog = JsonConvert.DeserializeObject<List<SupplierCallLog>>
                       (await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetSuppliersCallLog}?supplierId={supplierId}", ""));

                List<long> customerIds = supplierCallLog.Select(x => x.CustomerId).ToList();

                List<CallType> callTypes = JsonConvert.DeserializeObject<List<CallType>>
                    (await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}", ""));

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByIdList}", customerIds, ""));

                List<ImageModels.CustomerProfileImage> customerProfileImages = JsonConvert.DeserializeObject<List<ImageModels.CustomerProfileImage>>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetCustomerProfileImageList}", customerIds, ""));

                foreach (SupplierCallLog callLog in supplierCallLog)
                {
                    Customer customerName = customers.FirstOrDefault(a => a.CustomerId == callLog?.CustomerId);

                    callHistoryLogVMs.Add(new CallHistoryLogVM
                    {
                        CustomerId = callLog.CustomerId,
                        CallerName = $"{customerName.FirstName} {customerName.LastName}",
                        CallDuration = callLog?.Duration,
                        CallTime = callLog.CreatedOn,
                        CallType = callTypes.FirstOrDefault(a => a?.CallTypeId == callLog.CallType)?.Name,
                        CallerImage = customerProfileImages.FirstOrDefault(a => a.CustomerId == callLog.CustomerId)?.ProfileImage,
                        SupplierId = callLog.SupplierId,
                        SupplierCallLogId = callLog.SupplierCallLogId
                    });
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            return callHistoryLogVMs;
        }

        public async Task<bool> DeleteSuppliersCallLogs(List<long> selectedCallLogIds)
        {
            try
            {
                return JsonConvert.DeserializeObject<bool>
                       (await httpClient.PostAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.DeleteSuppliersCallLogs}", selectedCallLogIds));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new bool();
            }
        }

        public async Task<Response> DeleteAdVideo(long supplierAdsId)
        {
            Response response = new Response();
            try
            {
                string deleteAdVideoJson = await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.DeleteAdVideo}?supplierAdsId={supplierAdsId}", "");
                response = JsonConvert.DeserializeObject<Response>(deleteAdVideoJson);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> UpdateAd(PostAdVM postAdVM, long id)
        {
            Response response = new Response();
            try
            {
                SupplierAds supplierAds = new SupplierAds()
                {
                    ProductSubCategoryId = postAdVM.ProductSubCategory,
                    SupplierId = id,
                    SupplierAdsId = postAdVM.SupplierAdId,
                    AdsStatusId = (int)postAdVM.StatusId,
                    AdTitle = postAdVM.PostTitle,
                    AdDescription = postAdVM.PostDiscription,
                    Price = Math.Round(Convert.ToDecimal(postAdVM.Price), 2),
                    ActiveFrom = postAdVM.ActiveFrom,
                    ActiveTo = postAdVM.ActiveTo,
                    Address = postAdVM.Address,
                    Town = postAdVM.Town,
                    CityId = postAdVM.CityId,
                    IsAvailable = postAdVM.CollectionAvailable,
                    IsDeliverable = postAdVM.DeliveryAvailable,
                    ModifiedOn = DateTime.Now
                };

                response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateAd}", supplierAds));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<UserProfileVM> GetSupplierByUserId(string userId)
        {
            UserProfileVM userVM = new UserProfileVM();

            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierByUserId}?userId={userId}")
                    );


                if (supplier != null)
                {
                    List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>(
                        await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", "")
                    );

                    SupplierProfileImage profileImage = JsonConvert.DeserializeObject<SupplierProfileImage>(
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProfileImageBySupplierId}?supplierId={supplier.SupplierId}")
                    );

                    userVM = new UserProfileVM()
                    {
                        EntityId = supplier.SupplierId,
                        UserName = $"{supplier.FirstName} {supplier.LastName}",
                        City = cities?.FirstOrDefault(c => c.Id == supplier.CityId)?.Value,
                        Skills = new List<string> { supplier.PrimaryTrade },
                        ProfileImage = profileImage?.ProfileImage,
                        PublicId = supplier.PublicId
                    };
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return userVM;
        }

        public async Task<Response> DeleteAd(long supplierAdId, long supplierId)
        {
            Response response = new Response();
            try
            {
                string supplierAdsJson = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetEditAdDetail}?supplierAdsId={supplierAdId}", "");
                SupplierAds supplierAds = JsonConvert.DeserializeObject<SupplierAds>(supplierAdsJson);
                supplierAds.Active = false;

                response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateAd}", supplierAds));
                if (response.Status == ResponseStatus.OK)
                {
                    response.Message = "Suuplier Ad Remove Successfully!";
                    response.Status = ResponseStatus.OK;

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<BusinessProfileVM> GetBusinessProfile(long supplierId)
        {

            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierTradeNmae}?supplierId={supplierId}"));

                List<ProductSubCategoryDTO> productSubCategory = JsonConvert.DeserializeObject<List<ProductSubCategoryDTO>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductSubCategoryById}?productCatgoryId={supplier.ProductCategoryId}"));
                List<SupplierSubCategory> supplierSubCategories = JsonConvert.DeserializeObject<List<SupplierSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSelectedSupplierSubCategory}?supplierId={supplierId}"));
                List<long> supplierSubCategoryId = supplierSubCategories.Select(x => x.ProductSubCategoryId).ToList();
                List<ProductSubCategory> productSubCategoriesById = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.getProductSubCategoriesById}", supplierSubCategoryId));
                BusinessProfileVM businessProfileVM = new BusinessProfileVM()
                {
                    SupplierId = supplierId,
                    CategoryId = supplier.ProductCategoryId,
                    CompanyName = string.IsNullOrWhiteSpace(supplier.CompanyName) ? "" : supplier.CompanyName,
                    RegistrationNumber = supplier.RegistrationNumber,
                    PrimaryTrade = supplier.PrimaryTrade,
                    PrimaryTradeId = supplier.ProductCategoryId,
                    DeliveryRadius = supplier.DeliveryRadius,
                    City = supplier.CityId.Value,
                    Town = supplier.State,
                    BusinessAddress = supplier.BusinessAddress,
                    LocationCoordinates = supplier.GpsCoordinates,
                    selectedSubCategory = productSubCategoriesById.Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList(),
                    ProductsubCategory = productSubCategory.Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList()
                };

                return businessProfileVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new BusinessProfileVM();
            }
        }

        public async Task<PersonalDetailsVM> GetPersonalInformation(long supplierId)
        {
            PersonalDetailsVM detailsVM = new PersonalDetailsVM();
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                       (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierId}"));

                Response identityUser = JsonConvert.DeserializeObject<Response>
                       (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={supplier.UserId}", ""));

                UserRegisterVM userRegister = JsonConvert.DeserializeObject<UserRegisterVM>(identityUser?.ResultData?.ToString());

                SupplierProfileImage supplierProfileImage = JsonConvert.DeserializeObject<SupplierProfileImage>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProfileImageBySupplierId}?supplierId={supplierId}"));

                detailsVM.FirstName = supplier?.FirstName;
                detailsVM.LastName = supplier?.LastName;
                detailsVM.Email = supplier?.EmailAddress;
                detailsVM.Cnic = supplier?.Cnic;
                detailsVM.Gender = (int)supplier.Gender;
                detailsVM.MobileNumber = supplier?.MobileNumber;
                detailsVM.IsEmailConfirmed = userRegister.IsEmailConfirmed;
                detailsVM.IsNumberConfirmed = userRegister.IsNumberConfirmed;
                detailsVM.UserId = userRegister.Id;

                if (supplier.Dob != null)
                {
                    detailsVM.DateOfBirth = (DateTime)supplier.Dob;
                }
                if (supplierProfileImage != null)
                {
                    detailsVM.ProfileImage = supplierProfileImage.ProfileImage;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return detailsVM;
        }

        public async Task<SupplierProfileDetailVM> GetBusinessAndPersnalProfileWeb(long supplierId)
        {
            SupplierProfileDetailVM SupplierProfileDetailVM = new SupplierProfileDetailVM();
            try
            {
                SupplierProfileDetailVM.PersnalDetails = await GetPersonalInformation(supplierId);
                SupplierProfileDetailVM.BusinessDetails = await GetBusinessProfile(supplierId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            return SupplierProfileDetailVM;
        }

        public async Task<bool> PostPersonalInformation(long id, PersonalDetailsVM model)
        {
            try
            {
                PersonalDetailsVM detailsVM = new PersonalDetailsVM()
                {
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    EntityId = id

                };
                bool resultJson = JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdatePersonalDetail}", detailsVM));

                return resultJson;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public Task<IdValueVM> GetProductCatogory(long SupplierId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateSupplierAdsstatus(long supplierAdsId, long supplieradsStatusId, int days)
        {
            try
            {
                bool result = JsonConvert.DeserializeObject<bool>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateAdcStatus}?supplierAdsId={supplierAdsId}&supplieradsStatusId={supplieradsStatusId}&days={days}", ""));
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public string RandomAdsRefrence()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] stringChars = new char[5];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            string number = random.Next(99999).ToString("D5");
            string finalString = new string(stringChars);
            return finalString + number;
        }

        public async Task<List<ManageAdsVM>> SpGetActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<List<ManageAdsVM>>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetActiveAds}?supplierId={supplierId}&pageNumber={pageNumber}&pageSize={pageSize}", ""));
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ManageAdsVM>();
            }

        }

        public async Task<List<InactiveManageAdsVM>> SpGetInActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                var res = JsonConvert.DeserializeObject<List<InactiveManageAdsVM>>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetInActiveAds}?supplierId={supplierId}&pageNumber={pageNumber}&pageSize={pageSize}")
                   );
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InactiveManageAdsVM>();
            }
        }

        public async Task<List<ManageAdsVMWithImage>> SpGetActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<List<ManageAdsVMWithImage>>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetActiveAdsWithImages}?supplierId={supplierId}&pageNumber={pageNumber}&pageSize={pageSize}", ""));
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ManageAdsVMWithImage>();
            }

        }

        public async Task<List<InactiveManageAdsVMWithImages>> SpGetInActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                var res = JsonConvert.DeserializeObject<List<InactiveManageAdsVMWithImages>>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetInActiveAdsWithImages}?supplierId={supplierId}&pageNumber={pageNumber}&pageSize={pageSize}")
                   );
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InactiveManageAdsVMWithImages>();
            }
        }

        public async Task<List<ImageVM>> GetPostAdImagesList(long supplierAdsId)
        {
            List<ImageVM> ImageVMs = new List<ImageVM>();
            try
            {
                string supplierAdsImageJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetAdsImages}?supplierAdsId={supplierAdsId}", "");
                List<SupplierAdImage> suppliersAdsImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(supplierAdsImageJson);
                if (suppliersAdsImages != null)
                {

                    foreach (SupplierAdImage item in suppliersAdsImages)
                    {
                        ImageVM imageVM = new ImageVM
                        {
                            Id = item.AdImageId,
                            ImageContent = item.AdImage,
                            FilePath = item.FileName,
                            IsMain = item.IsMain.Value
                        };
                        ImageVMs.Add(imageVM);
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return ImageVMs;
        }

        public async Task<VideoVM> GetSupplierAdVideoVM(long supplierAdsId)
        {
            VideoVM VideoVM = new VideoVM();

            try
            {
                string supplierAdVideoJson = await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetSupplierAdVideoByAdId}?supplierAdId={supplierAdsId}", "");
                SupplierAdVideos supplierAdVideos = JsonConvert.DeserializeObject<SupplierAdVideos>(supplierAdVideoJson);
                if (supplierAdVideos != null)
                {
                    {
                        VideoVM.VideoId = supplierAdVideos.AdVideoId;
                        VideoVM.VideoContent = supplierAdVideos.AdVideo;
                        VideoVM.FilePath = supplierAdVideos.VideoName;
                    };
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return VideoVM;
        }

        public async Task<List<MarketSimilarProductsVM>> MarketSimilarProductsVMs(long categoryId, long supplierAdId)
        {
            List<MarketSimilarProductsVM> marketSimilarProducts = new List<MarketSimilarProductsVM>();
            try
            {
                string supplierAdsListbyCategoryId = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.MarketSimilarProductsVMs}?categoryId={categoryId}", "");
                marketSimilarProducts = JsonConvert.DeserializeObject<List<MarketSimilarProductsVM>>(supplierAdsListbyCategoryId);
                List<long> suppliedAdsIdsList = marketSimilarProducts.Select(id => id.SupplierAdId).ToList();
                string adImageJson = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", suppliedAdsIdsList);
                var adImageList = JsonConvert.DeserializeObject<List<SupplierAdImage>>(adImageJson);
                foreach (var item in marketSimilarProducts)
                {
                    item.FileName = adImageList.Where(e => e.SupplierAdsId == item.SupplierAdId && e.IsMain == true).Select(s => s.AdImage).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return marketSimilarProducts;
        }

        public async Task<List<WebAdsSearch>> WebAdsSearch(string search)
        {
            List<WebAdsSearch> manageAdsVMs = new List<WebAdsSearch>();
            try
            {
                List<Supplier> supplier = new List<Supplier>();
                List<SupplierAds> supplierAds = new List<SupplierAds>();
                List<SupplierAdImage> supplierAdImages = new List<SupplierAdImage>();

                supplier = JsonConvert.DeserializeObject<List<Supplier>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AllSupplier}", ""));
                supplierAds = JsonConvert.DeserializeObject<List<SupplierAds>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllAds}", ""));


                List<IdValueVM> cities = JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));

                List<long> supplierIds = supplierAds.Select(x => x.SupplierAdsId).ToList();
                List<IdValueVM> productSubCategories = await AllSubCategory();
                supplierAdImages = JsonConvert.DeserializeObject<List<SupplierAdImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImagesBySupplierAdIds}", supplierIds));

                List<SupplierAds> filteredAds = supplierAds.Where(x => x.ActiveTo > DateTime.Now).ToList();

                manageAdsVMs = filteredAds?.Select(x => new WebAdsSearch
                {
                    SupplierAdTitle = x?.AdTitle,
                    Price = x?.Price ?? 0,
                    ActiveFrom = x.ActiveFrom,
                    ActiveTo = x.ActiveTo,
                    SupplierCompanyName = supplier.Where(xi => xi.SupplierId == x.SupplierId).Select(s => s.CompanyName).FirstOrDefault(),
                    SubCategoryValue = productSubCategories.Where(s => s.Id == x.ProductSubCategoryId).Select(s => s.Value).FirstOrDefault(),
                    AdsStatusId = x?.AdsStatusId ?? 0,
                    City = string.IsNullOrWhiteSpace(cities.Where(c => c.Id == x.CityId).Select(c => c?.Value).FirstOrDefault()) ? "" : cities.Where(c => c.Id == x.CityId).Select(c => c?.Value).FirstOrDefault(),
                    SupplierAdId = x?.SupplierAdsId ?? 0,
                    AdViewCount = x.AdViewCount.HasValue ? x.AdViewCount.Value : 0,
                    AdImageName = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain == true)?.FileName,
                    SupplierAdImage = supplierAdImages.FirstOrDefault(a => a.SupplierAdsId == x.SupplierAdsId && a.IsMain == true)?.AdImage,
                    //TotalDay =  (x.ActiveTo.Date.Day- x.ActiveFrom.Day)
                    //TotalDay = (x.ActiveTo - x.ActiveFrom).Days

                }).ToList();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    manageAdsVMs = manageAdsVMs.Where(x => x.SupplierAdTitle.ToLower().Contains(search.ToLower())).Distinct().ToList();

                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return manageAdsVMs;
        }

        public async Task<List<LandingSearch>> GetAllSubcategory(string search)
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSubcategory}?search={search}", "");
            var subcategoryList = JsonConvert.DeserializeObject<List<ProductSubCategory>>(subcategoryJsonString);
            List<LandingSearch> subCategories = new List<LandingSearch>();
            foreach (var item in subcategoryList)
            {
                LandingSearch landingSearch = new LandingSearch
                {
                    ProducCategoryId = item.ProductCategoryId,
                    ProductSubCategoryId = item.ProductSubCategoryId,
                    SubCategoryName = item.SubCategoryName
                };
                subCategories.Add(landingSearch);

            }

            return subCategories;
        }

        public async Task<Response> GetBusinessDetailsStatus(string id)
        {
            var aa = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetBusinessDetailsStatus}?id={id}");
            Response response = JsonConvert.DeserializeObject<Response>(aa);
            return response;
        }
        public async Task<List<ReportsViewModels.ProductCategoryVM>> GetCategoriesForListing(int productCategoryId)
        {
            return JsonConvert.DeserializeObject<List<ReportsViewModels.ProductCategoryVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesForListing}?productCategoryId={productCategoryId}", ""));

        }
        public async Task<List<SupplierListVM>> GetSupplierList()
        {
            return JsonConvert.DeserializeObject<List<SupplierListVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierList}", ""));

        }

        public async Task<BusinessProfileVM> GetSupplierBusinessProfile(long supplierId)
        {
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierTradeNmae}?supplierId={supplierId}"));
                List<ProductSubCategoryDTO> productSubCategory = JsonConvert.DeserializeObject<List<ProductSubCategoryDTO>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductSubCategoryById}?productCatgoryId={supplier.ProductCategoryId}"));
                List<SupplierSubCategory> supplierSubCategories = JsonConvert.DeserializeObject<List<SupplierSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSelectedSupplierSubCategory}?supplierId={supplierId}"));
                List<long> supplierSubCategoryId = supplierSubCategories.Select(x => x.ProductSubCategoryId).ToList();
                List<ProductSubCategory> productSubCategoriesById = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.getProductSubCategoriesById}", supplierSubCategoryId));
                List<City> cities = JsonConvert.DeserializeObject<List<City>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityList}"));
                BusinessProfileVM businessProfileVM = new BusinessProfileVM()
                {
                    SupplierId = supplierId,
                    CompanyName = string.IsNullOrWhiteSpace(supplier.CompanyName) ? "" : supplier.CompanyName,
                    RegistrationNumber = supplier.RegistrationNumber,
                    PrimaryTrade = supplier.PrimaryTrade,
                    PrimaryTradeId = supplier.ProductCategoryId,
                    DeliveryRadius = supplier.DeliveryRadius,
                    City = supplier.CityId.Value,
                    Town = supplier.State,
                    BusinessAddress = supplier.BusinessAddress,
                    LocationCoordinates = supplier.GpsCoordinates,
                    CityName = cities.FirstOrDefault(x => x.CityId == supplier.CityId.Value).Name,
                    selectedSubCategory = productSubCategoriesById.Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList(),
                    ProductsubCategory = productSubCategory.Select(x => new IdValueVM { Id = x.ProductSubCategoryId, Value = x.SubCategoryName }).ToList()
                };

                return businessProfileVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new BusinessProfileVM();
            }

        }

        public async Task<Response> RelistAd(long SupplierAdsId)
        {
            return JsonConvert.DeserializeObject<Response>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.RelistAd}?SupplierAdsId={SupplierAdsId}", ""));

        }

        public async Task<List<SupplierListVM>> GetSupplierImageList(long imageId)
        {
            return JsonConvert.DeserializeObject<List<SupplierListVM>>
               (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierImageList}?imageId={imageId}", ""));
        }

        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceProducts(AdsParameterVM adsParameterVM)
        {
            return JsonConvert.DeserializeObject<List<GetMarkeetPlaceProducts>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetMarkeetPlaceProducts}", adsParameterVM));
        }

        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProducts(int pageSize, int pageNumber, long customerId)
        {
            return JsonConvert.DeserializeObject<List<GetMarkeetPlaceProducts>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetMarkeetPlaceTopRatedProducts}?pageSize={pageSize}&pageNumber={pageNumber}&customerId={customerId}"));
        }

        public async Task<List<GetMarkeetPlaceProducts>> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize, long customerId)
        {
            return JsonConvert.DeserializeObject<List<GetMarkeetPlaceProducts>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetMarkeetPlaceTopRatedProductsforWeb}?pageSize={pageSize}&customerId={customerId}"));
        }
    
        public async Task<List<SendFeedBackEmail>> SendFeedBackEmail(List<SendFeedBackEmail> sendFeedBackEmails)
        {
            List<FeedBackEmailVM> feedBackEmailVMs = new List<FeedBackEmailVM>();
            try
            {
                var FeedBackList = sendFeedBackEmails.Where(s => s.NoOfEmails < 4).ToList();
                if (FeedBackList.Count > 0)
                {
                  foreach (var item in FeedBackList)
                  {
                    FeedBackEmailVM feedBackEmailVM = new FeedBackEmailVM()
                    {
                      Subject = "Dear " + item.FullName + ", are you satisfied with HoomWork",
                      FullName = item.FullName,
                      EmailAddress = item.EmailAddress,
                      Message = "We Need Your FeedBack! Kindly open given link and rate us http://localhost:4200/product/product-feedback?productId=" + item.ProductId,
                      Email = new Email
                      {
                        CreatedBy = "Send To Help Desk.",
                        Retries = 0,
                        IsSend = false
                      }
                    };
                    feedBackEmailVMs.Add(feedBackEmailVM);
                  }

                  bool response = JsonConvert.DeserializeObject<bool>(
                                  await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendFeedBackEmail}", feedBackEmailVMs)
                          );
                  return null;
                }
                else
                {
                  return null;
                }
            }
            catch (System.Exception ex)
            {
              Exc.AddErrorLog(ex);

              return null;
            }
        }
        public async Task<List<ExpiryNotificationVM>> ExpiryNotification(List<ExpiryNotificationVM> postAdVM)
        {

            foreach (var item in postAdVM)
            {
                PostNotificationVM postNotificationVM = new PostNotificationVM()
                {
                    SenderUserId = item.CreatedBy,
                    Body = $"Your Ad of {item.AdTitle} will be expire soon .Please relist it after expiry.",
                    Title = NotificationTitles.ExpiredAd,
                    TargetActivity = "Supply",
                    To = item.FirebaseClientId,
                    TragetUserId = item.CreatedBy,
                    IsRead = false,
                    TargetDatabase = TargetDatabase.Supplier
                };

                bool result = JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                );

            }
            return postAdVM;
        }
        public async Task<bool> UpdateSupplierPublicId(long supplierId, string publicId)
        {

            var result = JsonConvert.DeserializeObject<bool>(
                                   await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierPublicId}?supplierId={supplierId}&publicId={publicId}")
                               );
            return result;
        }

        public async Task<List<GetMarkeetPlaceProducts>> GetMarketPlaceAds(AdsParameterVM adsParameterVM)
        {
            return JsonConvert.DeserializeObject<List<GetMarkeetPlaceProducts>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetMarketPlaceAds}", adsParameterVM));
        }

        public async Task<string> GetSupplierWithDetails(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierWithDetails}?supplierId={supplierId}", "");
            return result;
        }
        public async Task<string> SaveAndUpdateBankAccountData(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SaveAndUpdateBankAccountData}", obj);
        }
        public async Task<string> AddAndUpdateLogo(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.AddAndUpdateLogo}", obj);
        }
        public async Task<List<ProductCategoryVM>> GetCategoriesForAdminListing()
        {
            return JsonConvert.DeserializeObject<List<ProductCategoryVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesForAdminListing}", ""));

        }
        public async Task<List<IdValueDTO>> GetCategoriesNameWithId()
        {
            return JsonConvert.DeserializeObject<List<IdValueDTO>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesNameWithId}", ""));

        }
        public async Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long supplierSubCategoryId)
        {
            try
            {
                List<ProductSubCategoryDTO> productSubCategory = JsonConvert.DeserializeObject<List<ProductSubCategoryDTO>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductSubCategoryById}?productCatgoryId={supplierSubCategoryId}"));
                return productSubCategory;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategoryDTO>();
            }
        }
        public async Task<Response> AddUpdateProductAttribute(ProductVM productVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateProductAttribute}", productVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateSupplierSlider(string data)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateSupplierSlider}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateProductCategoryAttribute(ProductVM productVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateProductCategoryAttribute}", productVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<string> GetProductAttributeList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductAttributeList}", "");

        }
        public async Task<string> GetSupplierSliderList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierSliderList}", "");
        }
        public async Task<string> GetProductCategoryAttributeList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductCategoryAttributeList}", "");
        }
        public async Task<Response> GetProductAttributeListByCategoryId(long categoryId, string categoryLevel)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductAttributeListByCategoryId}?categoryId={categoryId}&categoryLevel={categoryLevel}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetProductCategoryGroupListById(long subCategoryId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductCategoryGroupListById}?subCategoryId={subCategoryId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetTopFiveProductCategory()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetTopFiveProductCategory}", "");
        }
        public async Task<string> GetSupplierProductList(ProductVM productVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProductList}", productVM, "");
        }
        public async Task<string> GetSupplierProductDetail(long productId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProductDetail}?productId={productId}", "");
        }
        public async Task<Response> AddUpdateNewVariant(ProductVariant productVariant)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateNewVariant}", productVariant);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<ProductVariant>> GetAllProductVariant()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductVariant}", "");
            return JsonConvert.DeserializeObject<List<ProductVariant>>(response);
        }
        public async Task<Response> AddNewSupplierProduct(AddProductVM addProductVM)
        {
            var response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddNewSupplierProduct}", addProductVM));
            return response;
        }
        public async Task<Response> UpdateSupplierProduct(AddProductVM addProductVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierProduct}", addProductVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<ProductVM>> GetProductDetailMob(long productId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductDetailMob}?productId={productId}", "");
            return JsonConvert.DeserializeObject<List<ProductVM>>(response);
        }
        public async Task<string> GetProductDetailWeb(long productId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductDetailWeb}?productId={productId}", "");
        }

        public async Task<string> GetHomeProductList(AddProductVM addProductVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetHomeProductList}", addProductVM, "");
        }
        public async Task<string> GetCountryList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCountryList}");
        }
        public async Task<string> GetSateList(int? countryId)
        {
            return await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSateList}?countryId={countryId}", "");
        }
        public async Task<string> GetProductSearchTagsList(string inputText)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductSearchTagsList}?inputText={inputText}", "");
        }
        public async Task<string> GetAreaList(int? cityId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAreaList}?cityId={cityId}", "");
        }
        public async Task<string> GetLocationList(int areaId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetLocationList}?areaId={areaId}", "");
        }

        public async Task<string> GetSupplierProductListWeb(ProductVM productVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProductListWeb}", productVM, "");
        }
        public async Task<string> GetProductsByCategory(ProductVM productVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductsByCategory}", productVM);
        }
        public async Task<string> GetProductsByName(ProductVM productVM)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductsByName}", productVM);
        }
        public async Task<string> GetBanksList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetBanksList}");
        }

        public async Task<string> AddAndUpdateSellerAccount(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddAndUpdateSellerAccount}", obj);
        }

        public async Task<string> AddAndUpdateBusinessAccount(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddAndUpdateBusinessAccount}", obj);
        }

        public async Task<string> GetBankAccountData(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetBankAccountData}?supplierId={supplierId}", "");
            return result;
        }

        public async Task<string> GetLogoData(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetLogoData}?supplierId={supplierId}", "");
            return result;
        }
        public async Task<string> GetProfile(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProfile}?supplierId={supplierId}", "");
            return result;
        }


        public async Task<string> SaveAndUpdateWhareHouseAddress(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SaveAndUpdateWhareHouseAddress}", obj);
        }

        public async Task<string> GetWareHouseAddress(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetWareHouseAddress}?supplierId={supplierId}", "");
            return result;
        }

        public async Task<string> SaveAndUpdateReturnAddress(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SaveAndUpdateReturnAddress}", obj);
        }
         public async Task<string> SaveAndUpdateSocialLinks(string obj)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SaveAndUpdateSocialLinks}", obj);
        }

        public async Task<string> GetReturnAddress(long supplierId)
        {
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetReturnAddress}?supplierId={supplierId}", "");
            return result;
        }
        public async Task<string> GetSocialLinks(long supplierId, string isSupplierWeb)
        {                                                                                         
            var result = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSocialLinks}?supplierId={supplierId}&isSupplierWeb={isSupplierWeb}");
            return result;
        }

        public async Task<string> GetProductCategories()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductCategories}");
        }
        public async Task<string> GetCategoryGroupsById(long subCategoryId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoryGroupsById}{subCategoryId}");
        }

        public async Task<string> GetOrdersList(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrdersList}", data);
        }
    
        public async Task<string> GetSalesSummary(string data)
        {
          return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSalesSummary}", data);
        }
        public async Task<Response> PlaceOrder(PlaceOrderVM orderItemVM)
        {
            Response response = new Response();
            response = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.PlaceOrder}", orderItemVM));
            if (response.Status == ResponseStatus.OK)
            {
                await PlaceOrderNotification(orderItemVM, response);
            }
            return response;
        }

        private async Task<bool> PlaceOrderNotification(PlaceOrderVM orderItemVM, Response response)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={orderItemVM.CustomerId}"));
                foreach (var item in orderItemVM.supplierIdArr)
                {
                    Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={item.SupplierId}"));
                    Response identityResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={supplier.UserId}"));
                    UserRegisterVM userVM = JsonConvert.DeserializeObject<UserRegisterVM>(identityResponse?.ResultData?.ToString());

                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        Title = "New Order Placed",
                        Body = $"{customer.FirstName} {customer.LastName} placed an order.",
                        To = $"{userVM.FirebaseClientId}",
                        TargetActivity = "NewOrderPlaced",
                        SenderUserId = customer?.UserId,
                        SenderEntityId = Convert.ToString(customer.CustomerId),
                        TargetDatabase = TargetDatabase.Supplier,
                        TragetUserId = supplier.UserId,
                        IsRead = false,
                        isFromWeb = orderItemVM.isFromWeb
                    };

                    bool notificationResult = JsonConvert.DeserializeObject<bool>(
                        await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
                    );
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> GetOrderDetailsById(long orderId, long supplierId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderDetailsById}?orderId={orderId}&supplierId={supplierId}");
        }
        public async Task<string> GetSupplierProductById(long productId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProductById}?productId={productId}", "");
        }
        public async Task<string> GetVariantsByProductId(long productId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetVariantsByProductId}?productId={productId}", "");
        }
        public async Task<string> UpdateOrderStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateOrderStatus}", data);
            var result = JsonConvert.DeserializeObject<Response>(response);
            var entity = JsonConvert.DeserializeObject<OrderUpdateDTO>(data);
            if (result.Status == ResponseStatus.OK)
            {
                await OrderStatusNotification(entity);
            }
            return response;
        }
        private async Task<bool> OrderStatusNotification(OrderUpdateDTO orderItemVM)
        {
            Customer customer = JsonConvert.DeserializeObject<Customer>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={orderItemVM.CustomerId}"));
            Supplier supplier = JsonConvert.DeserializeObject<Supplier>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={orderItemVM.SupplierId}"));
            string orderStatusBody = "";
            if (orderItemVM.OrderStatus == (int)EOrderStatus.Received)
                orderStatusBody = "has been Received";
            else if (orderItemVM.OrderStatus == (int)EOrderStatus.PackedAndShipped)
                orderStatusBody = "has been Packed & Shipped";
            else if (orderItemVM.OrderStatus == (int)EOrderStatus.Delievred)
                orderStatusBody = "has been Delivered";
            else if (orderItemVM.OrderStatus == (int)EOrderStatus.Completed)
                orderStatusBody = "has been Completed";
            else if (orderItemVM.OrderStatus == (int)EOrderStatus.Cancelled)
                orderStatusBody = "has been Canceled";
            else
            {
                orderStatusBody = "has been Declined";
            }
           
            PostNotificationVM postNotificationVM = new PostNotificationVM()
            {
                Title = "Order Status",
                Body = $"Your order from {supplier.FirstName} {supplier.LastName} with OrderID {orderItemVM.OrderId} {orderStatusBody}",
                To = $"{orderItemVM.FirebaseClientId}",
                TargetActivity = "OrderStatus",
                SenderUserId = supplier?.UserId,
                SenderEntityId = Convert.ToString(orderItemVM.OrderId),
                TargetDatabase = TargetDatabase.Customer,
                TragetUserId = customer.UserId,
                IsRead = false,
                isFromWeb = orderItemVM.isFromWeb
            };
            var data = postNotificationVM;
            bool notificationResult = JsonConvert.DeserializeObject<bool>(
                await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostDataNotification}", postNotificationVM)
            );
            return notificationResult;
        }
        public async Task<string> GetCustomerOrdersList(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCustomerOrdersList}", data);
        }

        public async Task<string> GetCustomerOrderedProductsList(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCustomerOrderedProductsList}", data);
        }

        public async Task<string> GetShippingCost(int id)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetShippingCost}?id={id}", "");

        }

        public async Task<Response> CancelCustomerOrder(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.CancelCustomerOrder}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<string> GetCustomerCanclledOrdersList(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCustomerCanclledOrdersList}", data);
        }

        public async Task<string> GetOrderCancellationReasonsList(int userRole)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderCancellationReasonsList}?userRole={userRole}", "");
        }

        public async Task<Response> AddSupplierLeadgerEntry(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddSupplierLeadgerEntry}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetTransactionHistory(TransactionsVM transactionsVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetTransactionHistory}", transactionsVM, ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<string> GetOrderTracking(string data)
        {
            var result = string.Empty;
            try
            {
                result = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderTracking}", data);

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
            return result;
        }

        public async Task<string> GetOrderedItemTracking(string data)
        {
            var result = string.Empty;
            try
            {
                result = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderedItemTracking}", data);
                
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
            return result;
        }
        public async Task<Response> GetLoggedSupplierCanelledOrdersList(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetLoggedSupplierCanelledOrdersList}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<string> GetSupplierShopUrl(string shopUrl)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierShopUrl}?shopUrl={shopUrl}");
        } 
        public async Task<string> GetSupplierShopDetails(string shopUrl)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierShopDetails}?shopUrl={shopUrl}");
        }

        public async Task<Response> GetProductsList(string productName)
        {
            Response productsList = new Response();
            try
            {
                productsList = JsonConvert.DeserializeObject<Response>
               (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductsList}?productName={productName}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return productsList;
        }
        public async Task<string> GetSupplierOrderBySupplierId(long supplierId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierOrderBySupplierId}?supplierId={supplierId}");
        }

        public async Task<Response> GetProductsByTag(ProductVM productVM)
        {
            var response= await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductsByTag}", productVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetAllCatSubCatGroupCategories()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllCatSubCatGroupCategories}", "");
        }

        public async Task<string> AddUpdateFreeShipping(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateFreeShipping}",data);
        }

        public async Task<Response> GetFreeShippingList(string data)
        {
            var res= await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetFreeShippingList}", data);
            return JsonConvert.DeserializeObject<Response>(res);
        }

        public async Task<Response> DeleteFreeShipping(long freeShippingId)
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.DeleteFreeShipping}?freeShippingId={freeShippingId}"));
        }

        public async Task<Response> GetWithdrawalListById(long id)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetWithdrawalListById}?Id={id}"));
            }
            catch (Exception ex)
            {
                return new Response() ;
            }
        }
    }
}
