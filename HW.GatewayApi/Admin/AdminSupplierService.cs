using HW.Http;
using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using PersonalDetailsVM = HW.IdentityViewModels.PersonalDetailsVM;
using HW.VideoModels;
using Hw.EmailViewModel;
using HW.CommunicationModels;
using HW.SupplierModels.DTOs;
using FeaturedSupplier = HW.SupplierViewModels.FeaturedSupplier;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminSupplierService
    {
        Task<List<SpSupplierListVM>> SpGetSupplierList(GenericUserVM genericUserVM);
        Task<List<SpSupplierListVM>> SpGetHoomWorkSupplierList(GenericUserVM genericUserVM);
        Task<List<SpSupplierListVM>> SpGetLocalSupplierList(GenericUserVM genericUserVM);
        Task<SpSupplierStatsVM> SpGetSupplierStats();
        Task<List<Supplier>> GetAllSuppliers();
        Task<string> GetSuppliersOrderlist(OrderslistDTO orderslistDTO);
        Task<string> GetSuppliersOrderlistById(long id);
        Task<Response> GetOrderStatusList();

        Task<List<ProductCategoryGroupVM>> GetAllProduCatcategoryGroup();
        Task<List<Supplier>> GetAllSuppliersYearlyReport();
        Task<List<Supplier>> GetAllSuppliersFromToReport(DateTime StartDate, DateTime EndDate);
        Task<List<IdValueVM>> GetCategoriesForDropDown();
        Task<List<IdValueCategoryVM>> GetSubCategoriesForDropDown(long categoryId);
        Task<List<ProductCategoryVM>> GetCategoriesForListing(int productCategoryId);
        Task<List<ProductCategoryVM>> GetCategoriesForAdminListing();
        Task<Response> CheckProductAvailability(string productName);
        Task<List<SupplierDTO>> GetSupplierForReport(string StartDate, string EndDate, string skills, string supplier, string city, bool lastActive, string location, string mobile, string cnic, string userType, string emailtype, string mobileType);
        //Task<List<SupplierDTO>> GetSupplierForReport(ReportsAndUsersVM reportsAndUsersVM); 
        Task<List<SupplierDTO>> GetSupplierWithDate24Hour(string startDate, string endDate);
        Task<List<SupplierAdsDTO>> GetPostedadsLastDay(string startDate, string endDate);
        Task<List<SupplierAdsDTO>> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, string StartDate, string EndDate, string supplier, string city, bool lastActive, string location, string adId);
        Task<string> GetSuppliersLeadgerlist(OrderslistDTO orderslistDTO);
        Task<List<Supplier>> GetSupplierAddressList();
        Task<List<SupplierAds>> GetSupplierAdsAddressList();
        Task<Response> BlockSupplier(string customerId, bool status);
        Task<List<SupplierDTO>> SupplierByCategory(SupplierByCatVM supplierByCatVM);
        Task<Response> AddNewProduct(ProductCategoryVM supplierByCatVM);
        Task<Response> AddUpdateProductsCategoryGroup(ProductCategoryGroup productCategoryGroup);
        Task<Response> AddNewSubProduct(SubProducts supplierByCatVM);
        Task<Response> FeaturedSupplier(HW.SupplierViewModels.FeaturedSupplier featuredSupplier);
        Task<List<SupplierDTO>> GetFeaturedSupplierImages();
        Task<SupplierProductAdDetails> GetSupplierAdDetails(long adId);
        Task<List<SubProducts>> GetSubCategoriesForListing();
        Task<SupplierProfileDetailVM> GetBusinessAndPersnalProfileWeb(long supplierId);
        Task<long> SaveAndUpdateAd(long id, PostAdVM postadd);
        void DeleteAdWithAdId(string selectedAdId, string userIdForAdDeleter);
        Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId);

        Task<string> GetSuppliersProductsListForApproval(string data);
        Task<Response> ApproveSupplierProduct(string data);
        Task<Response> UpdateSupplierAllGoodStatus(string data);
        Task<string> GetSupplierProductImagesbyProductId(long productId);
        Task<string> GetShippingChargesList();
        Task<Response> AddUpdateShippingCost(string data);
        Task<Response> OrderCancelByAdmin(string data);
        Task<Response> GetCanellationReasonsListForAdmin();
        Task<Response> InsertAndUpdateCancellationReason(string data);
        Task<string> GetSupplierProfileDetails(string data);
        Task<Response> GetCountryListForAdmin();
        Task<Response> AddUpdateCountry(string data);
        Task<Response> deleteCountryStatus(string data);
        Task<Response> GetStateListForAdmin();
        Task<Response> AddUpdateState(string data);
        Task<Response> deletestateStatus(string data);
        Task<Response> GetBanksListForAdmin();
        Task<Response> AddUpdatebank(string data);
        Task<Response> deletebankStatus(string data);
        Task<Response> GetAreaListForAdmin();
        Task<Response> deleteareaStatus(string data);
        Task<Response> saveAndUpdateArea(string data);
        Task<Response> GetLocationListForAdmin();
        Task<Response> AddUpdateLocation(string data);
        Task<Response> deletelocationStatus(string data);
        Task<Response> deleteLinkStatus(string data);
        Task<Response> BlockProduct(long productId, bool status);
        Task<Response> SupplierList();
        Task<Response> AddUpdateSupplierCommission(string data);
        Task<Response> GetSupplierCommissionList(string data);
    

    }

    public class AdminSupplierService : IAdminSupplierService
    {
        private readonly IHttpClientService httpClient;
        //private readonly ClientCredentials clientCred;
        //private readonly ICommunicationService communicationService; // sending job post confirmation email
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;
        public AdminSupplierService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.Exc = Exc;
            this._apiConfig = apiConfig;
        }

        public async Task<List<SpSupplierListVM>> SpGetSupplierList(GenericUserVM genericUserVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SpSupplierListVM>>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetSupplierList}", genericUserVM));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }
        }
        public async Task<List<SpSupplierListVM>> SpGetHoomWorkSupplierList(GenericUserVM genericUserVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SpSupplierListVM>>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetHoomWorkSupplierList}", genericUserVM));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }
        }
        public async Task<List<SpSupplierListVM>> SpGetLocalSupplierList(GenericUserVM genericUserVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SpSupplierListVM>>
                    (await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetLocalSupplierList}", genericUserVM));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }
        }

        public async Task<SpSupplierStatsVM> SpGetSupplierStats()
        {
            try
            {
                return JsonConvert.DeserializeObject<SpSupplierStatsVM>
                    (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SpGetSupplierStats}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpSupplierStatsVM();
            }
        }
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSuppliers}", "");
            return JsonConvert.DeserializeObject<List<Supplier>>(subcategoryJsonString);
        }

        public async Task<string> GetSuppliersOrderlist(OrderslistDTO orderslistDTO)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersOrderlist}", orderslistDTO, "");
        }
        public async Task<string> GetSuppliersOrderlistById(long id)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersOrderlistById}?id={id}", "");
        }

        public async Task<Response> GetOrderStatusList()
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetOrderStatusList}", "");
            return JsonConvert.DeserializeObject<Response>(subcategoryJsonString);
        }

        public async Task<List<ProductCategoryGroupVM>> GetAllProduCatcategoryGroup()
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProduCatcategoryGroup}", "");
            return JsonConvert.DeserializeObject<List<ProductCategoryGroupVM>>(subcategoryJsonString);
        }
        public async Task<List<Supplier>> GetAllSuppliersYearlyReport()
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSuppliersYearlyReport}", "");
            return JsonConvert.DeserializeObject<List<Supplier>>(subcategoryJsonString);
        }

        public async Task<List<Supplier>> GetAllSuppliersFromToReport(DateTime StartDate, DateTime EndDate)
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSuppliersFromToReport}?StartDate={StartDate}&EndDate={EndDate}", "");
            return JsonConvert.DeserializeObject<List<Supplier>>(subcategoryJsonString);
        }
        public async Task<List<IdValueVM>> GetCategoriesForDropDown()
        {
            return JsonConvert.DeserializeObject<List<IdValueVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesForDropDown}", ""));

        }

        public async Task<List<IdValueCategoryVM>> GetSubCategoriesForDropDown(long categoryId)
        {
            return JsonConvert.DeserializeObject<List<IdValueCategoryVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSubCategoriesForDropDown + "?categoryId=" + categoryId}", ""));

        }
        public async Task<List<ProductCategoryVM>> GetCategoriesForListing(int productCategoryId)
        {
            return JsonConvert.DeserializeObject<List<ProductCategoryVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesForListing}?productCategoryId={productCategoryId}", ""));

        }

        public async Task<List<ProductCategoryVM>> GetCategoriesForAdminListing()
        {
            return JsonConvert.DeserializeObject<List<ProductCategoryVM>>
                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCategoriesForAdminListing}", ""));

        }

        public async Task<List<SupplierDTO>> GetSupplierForReport(string StartDate, string EndDate, string skills, string supplier, string city, bool lastActive, string location, string mobile, string cnic, string userType, string emailtype, string mobileType)
        //public async Task<List<SupplierDTO>> GetSupplierForReport(ReportsAndUsersVM reportsAndUsersVM)
        {
            //return JsonConvert.DeserializeObject<List<SupplierDTO>>(await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierForReport}" , reportsAndUsersVM, ""));
            return JsonConvert.DeserializeObject<List<SupplierDTO>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierForReport}?StartDate={StartDate}&EndDate={EndDate}&categories={skills}&supplier={supplier}&city={city}&lastActive={lastActive}&location={location}&mobile={mobile}&cnic={cnic}&userType={userType}&emailtype={emailtype}&mobileType={mobileType}", ""));

        }
        public async Task<List<SupplierDTO>> GetSupplierWithDate24Hour(string startDate, string endDate)
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierLast24HourRegistred}?StartDate={startDate}&EndDate={endDate}", "");
            return JsonConvert.DeserializeObject<List<SupplierDTO>>(subcategoryJsonString);
        }
        public async Task<List<SupplierAdsDTO>> GetPostedadsLastDay(string startDate, string endDate)
        {
            var subcategoryJsonString = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetPostedadsLastDay}?StartDate={startDate}&EndDate={endDate}", "");
            return JsonConvert.DeserializeObject<List<SupplierAdsDTO>>(subcategoryJsonString);
        }
        public async Task<List<SupplierAdsDTO>> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, string StartDate, string EndDate, string supplier, string city, bool lastActive, string location, string adId)
        {
            return JsonConvert.DeserializeObject<List<SupplierAdsDTO>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetPostedAdsForDynamicReport}?pageSize={pageSize}&pageNumber={pageNumber}&dataOrderBy={dataOrderBy}&StartDate={StartDate}&EndDate={EndDate}&supplier={supplier}&city={city}&lastActive={lastActive}&location={location}&adId={adId}", ""));

        }
        public async Task<List<Supplier>> GetSupplierAddressList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAddressList}", "");
            return JsonConvert.DeserializeObject<List<Supplier>>(response);
        }
        public async Task<List<SupplierAds>> GetSupplierAdsAddressList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdsAddressList}", "");
            return JsonConvert.DeserializeObject<List<SupplierAds>>(response);
        }
        public async Task<Response> BlockSupplier(string supplierId, bool status)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.BlockSupplier}?supplierId={supplierId}&status={status}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<SupplierDTO>> SupplierByCategory(SupplierByCatVM supplierByCatVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SupplierByCategory}", supplierByCatVM);
            return JsonConvert.DeserializeObject<List<SupplierDTO>>(response);
        }
        public async Task<Response> CheckProductAvailability(string productName)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.CheckProductAvailability}?productName={productName}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }


        public async Task<Response> AddNewProduct(ProductCategoryVM productCategory)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddNewProduct}", productCategory);
            return JsonConvert.DeserializeObject<Response>(response);

        }
        public async Task<Response> AddUpdateProductsCategoryGroup(ProductCategoryGroup productCategoryGroup)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateProductsCategoryGroup}", productCategoryGroup);
            return JsonConvert.DeserializeObject<Response>(response);

        }
        public async Task<Response> AddNewSubProduct(SubProducts subProducts)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddNewSubProduct}", subProducts);
            return JsonConvert.DeserializeObject<Response>(response);

        }
        public async Task<Response> FeaturedSupplier(HW.SupplierViewModels.FeaturedSupplier featuredSupplier)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.FeaturedSupplier}", featuredSupplier);
            if (featuredSupplier.action == "saving")
            {
                string response1 = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.FeaturedSupplierImages}", featuredSupplier);
                return JsonConvert.DeserializeObject<Response>(response1);
            }
            else
            {
                string response2 = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.UpdateFeaturedSupplierImages}", featuredSupplier);
                return JsonConvert.DeserializeObject<Response>(response2);
            }

        }
        public async Task<List<SubProducts>> GetSubCategoriesForListing()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllSubProducts}", "");
            return JsonConvert.DeserializeObject<List<SubProducts>>(response);

        }
        public async Task<List<SupplierDTO>> GetFeaturedSupplierImages()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetFeaturedSupplierImages}", "");
            return JsonConvert.DeserializeObject<List<SupplierDTO>>(response);

        }
        public async Task<SupplierProductAdDetails> GetSupplierAdDetails(long adId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierAdDetails}?adId={adId}", "");
            return JsonConvert.DeserializeObject<SupplierProductAdDetails>(response);

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
        public async Task<PersonalDetailsVM> GetPersonalInformation(long supplierId)
        {
            PersonalDetailsVM detailsVM = new PersonalDetailsVM();
            try
            {
                Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                       (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={supplierId}"));

                //SupplierProfileImage supplierProfileImage = JsonConvert.DeserializeObject<SupplierProfileImage>
                //    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetProfileImageBySupplierId}?supplierId={supplierId}"));
                detailsVM.FirstName = supplier?.FirstName;
                detailsVM.LastName = supplier?.LastName;
                detailsVM.Email = supplier?.EmailAddress;
                detailsVM.Cnic = supplier?.Cnic;
                detailsVM.Gender = (int)supplier.Gender;
                detailsVM.MobileNumber = supplier?.MobileNumber;
                detailsVM.DateOfBirth = (DateTime)supplier.Dob;
                detailsVM.FeaturedSupplier = supplier.FeaturedSupplier;
                //if (supplierProfileImage != null)
                //{
                //    detailsVM.ProfileImage = supplierProfileImage.ProfileImage;
                //}
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return detailsVM;
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

        public async Task<List<ProductSubCategory>> getProductSubCategoryById(long supplierSubCategoryId)
        {
            try
            {
                List<ProductSubCategory> productSubCategory = JsonConvert.DeserializeObject<List<ProductSubCategory>>(await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetProductSubCategoryById}?productCatgoryId={supplierSubCategoryId}"));
                return productSubCategory;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategory>();
            }
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
                    AdsStatusId = (int)postadd.StatusId
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

                                SupplierAdImage supplierAdImage = new SupplierAdImage
                                {
                                    AdImage = item.ImageContent,
                                    FileName = $"img-{DateTime.Now}",
                                    SupplierAdsId = postadd.SupplierAdId != 0 ? postadd.SupplierAdId : supplierAdId,
                                    IsMain = item.IsMain,
                                    CreatedBy = postadd.CreatedBy,
                                    CreatedOn = DateTime.Now
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
                        //if (postadd.VideoVM != null && postadd.VideoVM.VideoContent?.Length > 0)
                        //{
                        //    SupplierAdVideos supplierAdVideo = new SupplierAdVideos
                        //    {
                        //        CreatedBy = postadd.CreatedBy,
                        //        CreatedOn = DateTime.Now,
                        //        VideoName = postadd.VideoVM.FilePath,
                        //        AdVideo = postadd.VideoVM.VideoContent,
                        //        SupplierAdsId = postadd.SupplierAdId != 0 ? postadd.SupplierAdId : supplierAdId
                        //    };
                        //    string submitAndUpdateSupplierVideo = await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.SubmitAndUpdateAdVideo }", supplierAdVideo);
                        //}

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

        public async void DeleteAdWithAdId(string selectedAdId, string deletedByUserId)
        {
            var DeletedAdJson = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.DeleteSelectedSupplierAdId}?selectedAdId={selectedAdId}&deletedByUserId={deletedByUserId}");
        }
        public async Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddLinkedSalesman}?SalesmanId={SalesmanId}&CustomerId={CustomerId} ", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetSuppliersProductsListForApproval(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersProductsListForApproval}", data);
        }

        public async Task<Response> ApproveSupplierProduct(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.ApproveSupplierProduct}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> UpdateSupplierAllGoodStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierAllGoodStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetSupplierProductImagesbyProductId(long productId)
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProductImagesbyProductId}?productId={productId} ", "");
        }

        public async Task<string> GetShippingChargesList()
        {
            return await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetShippingChargesList}", "");
        }
        public async Task<Response> AddUpdateShippingCost(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateShippingCost}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> OrderCancelByAdmin(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.OrderCancelByAdmin}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetCanellationReasonsListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCanellationReasonsListForAdmin}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> InsertAndUpdateCancellationReason(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.InsertAndUpdateCancellationReason}", data);
            return JsonConvert.DeserializeObject<Response>(response);

        }

        public async Task<string> GetSupplierProfileDetails(string data)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierProfileDetails}", data);
        }
        public async Task<Response> GetCountryListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetCountryListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateCountry(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateCountry}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> deleteCountryStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.deleteCountryStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetStateListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetStateListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateState(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddUpdateState}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> deletestateStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.deletestateStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetBanksListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetBanksListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdatebank(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdatebank}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> deletebankStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.deletebankStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetAreaListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAreaListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> deleteareaStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.deleteareaStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> saveAndUpdateArea(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.saveAndUpdateArea}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetLocationListForAdmin()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetLocationListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateLocation(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateLocation}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> deletelocationStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.deletelocationStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetSuppliersLeadgerlist(OrderslistDTO orderslistDTO)
        {
            return await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSuppliersLeadgerlist}", orderslistDTO, "");
        }
        public async Task<Response> deleteLinkStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.deleteLinkStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> BlockProduct(long productId, bool status)
        {
            var response= await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.BlockProduct}?productId={productId}&status={status}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> SupplierList()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SupplierList}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateSupplierCommission(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddUpdateSupplierCommission}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetSupplierCommissionList(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierCommissionList}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }


    }
}
