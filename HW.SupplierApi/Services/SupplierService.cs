using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.UserWebViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using System.Data;
using HW.PackagesAndPaymentsModels;
using HW.Job_ViewModels;
using AutoMapper;
using HW.SupplierModels.DTOs;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using FeaturedSupplier = HW.SupplierViewModels.FeaturedSupplier;
using HW.SupplierModels.DTOs.ShippingApiDTO;

namespace HW.SupplierApi.Services
{
    public interface ISupplierService
    {
        List<ProductCategory> GetAllProductCatagories();
        IQueryable<ProductSubCategory> GetSubCategoriesByProductCategoryId(long productCategoryId);
        IQueryable<SupplierAds> GetSupplierAdsByProductSubCategoryIds(List<long> productSubCategoryId);

        List<Supplier> GetSuppliersByIds(List<long> supplierIds);
        List<CustomerFeedBack> GetCustomerFeedBackList(string data);
        Task<Supplier> GetSupplierById(long supplierId);

        IQueryable<SupplierAds> GetSupplierAdsBySupplierId(long supplierId);
        Supplier GetSupplierBySupplierId(long supplierId);
        SupplierAds GetSupplierAdByAdId(long supplierAdId);
        IQueryable<SupplierAds> GetSupplierAdsByStatusId(int statusId);
        Task<Response> AddEditSupplier(Supplier supplier);
        Task<Response> GetSuppliersOrderlist(OrderslistDTO orderslistDTO);
        Task<Response> GetOrderStatusList();
        Task<Response> GetSuppliersOrderlistById(long id);

        Task<Response> GetOrderDetailById(long orderId);
        long GetEntityIdByUserId(string userId);
        List<ProductSubCategory> GetAllProductSubCatagories(long productId);
        bool AddRegistrationDetail(Supplier supplier);
        Supplier GetAllSupplier(long supplierId);
        List<Supplier> GetAllSuppliers();
        List<Supplier> GetAllSuppliersYearlyReport();
        List<Supplier> GetAllSuppliersFromToReport(DateTime StartDate, DateTime EndDate);
        List<SupplierAds> GetAllSupplierAds(long supplierId);
        List<ProductCategoryGroupVM> GetAllProduCatcategoryGroup();
        List<SupplierAds> GetAllAds();
        ProductCategory productCategory(long productId);
        Task<Response> GetSuppliersLeadgerlist(OrderslistDTO orderslistDTO);
        long SaveAndUpdateAd(SupplierAds model);
        SupplierAds GetEditAdDetail(long supplierAdsId);
        long GetProductCategoryIdBySupplierId(long supplierId);
        Response UpdateAd(SupplierAds supplierAds);
        Task<bool> UpdateAdcStatus(long supplierAdsId, long supplieradsStatusId, int days);
        List<ProductCategory> GetAllProductCategory();
        Response GetProfileVerification(long? supplierId);
        Response AddCustomerFeedBack(string data);
        List<ProductSubCategory> AllSubCategory();
        Supplier GetSupplierByUserId(string userId);
        Task<Response> SetSupplierSubCategories(List<SupplierSubCategory> subCategories);
        Supplier GetSupplierTradeName(long supplierId);
        Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long productCatgoryId);
        void DeleteAd(long suuplierAdId);
        void UpdateSupplierAdViewCount(long supplierAdId);
        bool UpdatePersonalDetail(PersonalDetailsVM model);
        List<SupplierSubCategory> GetSelectedSupplierSubCategory(long supplierId);
        List<ProductSubCategory> getProductSubCategoriesById(List<long> supplierSubCategoryId);

        List<ManageAdsVM> SpGetActiveAds(long supplierId, int pageNumber, int pageSize);
        List<InactiveManageAdsVM> SpGetInActiveAds(long supplierId, int pageNumber, int pageSize);
        List<ManageAdsVMWithImage> SpGetActiveAdsWithImages(long supplierId, int pageNumber, int pageSize);
        List<InactiveManageAdsVMWithImages> SpGetInActiveAdsWithImages(long supplierId, int pageNumber, int pageSize);
        string GetCategoryName(long productId);
        List<MarketSimilarProductsVM> MarketSimilarProductsVMs(long categoryId);
        IQueryable<SupplierAds> GetSupplierAdsByProductCategoryId(long productCategoryId);
        SupplierAds GetSupplierAdsById(long supplierId);
        List<Supplier> AllSupplier();
        List<ProductSubCategory> GetAllSubcategory(string search);

        List<SpSupplierListVM> SpGetSupplierList(GenericUserVM genericUserVM);
        List<SpSupplierListVM> SpGetHoomWorkSupplierList(GenericUserVM genericUserVM);
        List<SpSupplierListVM> SpGetLocalSupplierList(GenericUserVM genericUserVM);
        SpSupplierStatsVM SpGetSupplierStats();
        List<IdValueVM> GetCategoriesForDropDown();
        List<IdValueCategoryVM> GetSubCategoriesForDropDown(long categoryId);
        List<SupplierDTO> GetSupplierForReport(DateTime? StartDate, DateTime? EndDate, string categories, string supplier, string city, bool lastActive, string location, string mobile, string cnic, string userType, string emailtype, string mobileType);
        //List<SupplierDTO> GetSupplierForReport(ReportsAndUsersVM reportsAndUsersVM);
        List<Supplier> GetSupplierReport(List<string> userId);
        bool UpdateSupplierPublicId(long supplierId, string publicId);
        List<SupplierDTO> GetSupplierLast24HourRegistred(System.DateTime StartDate, System.DateTime EndDate);
        List<SupplierAdsDTO> GetPostedadsLastDay(System.DateTime StartDate, System.DateTime EndDate);
        List<SupplierAdsDTO> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, DateTime? StartDate, DateTime? EndDate, string supplier, string city, bool lastActive, string location, string adId);
        List<Supplier> GetSupplierAddressList();
        List<SupplierAds> GetSupplierAdsAddressList();
        Response BlockSupplier(string supplierId, bool status);
        List<SupplierDTO> SupplierByCategory(SupplierByCatVM supplierByCatVM);
        List<ProductCategoryVM> GetCategoriesForListing(int productCategoryId);
        List<ProductCategoryVM> GetCategoriesForAdminListing();
        List<IdValueDTO> GetCategoriesNameWithId();
        ImageVM GetCategoryImageById(long categoryImageId);
        Response CheckProductAvailability(string productName);
        Response AddNewProducts(ProductCategoryVM productCategory);
        Response AddNewSubProducts(SubProducts subProducts);
        Response AddUpdateProductsCategoryGroup(ProductCategoryGroup productCategoryGroup);
        Response FeaturedSupplier(HW.SupplierViewModels.FeaturedSupplier featuredSupplier);
        List<SubProducts> GetAllSubProducts();
        List<SupplierListVM> GetSupplierList();
        Response GetBusinessDetailsStatus(string id);
        List<SupplierListVM> GetSupplierImageList(long imageId);
        List<GetMarkeetPlaceProducts> GetMarkeetPlaceProducts(AdsParameterVM adsParameterVM);
        List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProducts(int pageSize, int pageNumber, long customerId);
        List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize, long customerId);
        Task<Response> RelistAd(long SupplierAdsId);
        Task<Response> GetPaymentHistory(OrderItemVM orderItemVM);
        Task<Response> GetPaymentDetail(long supplierId, long orderId);
        SupplierProductAdDetails GetSupplierAdDetails(long adId);
        void DeleteSelectedSupplierAdId(string selectedAdId, string userIdForAdDeleter);
        Response AddLinkedSalesman(string SalesmanId, string CustomerId);
        Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber);
        List<GetMarkeetPlaceProducts> GetMarketPlaceAds(AdsParameterVM adsParameterVM);
        Response AddUpdateProductAttribute(ProductVM productVM);
        Task<Response> AddUpdateSupplierSlider(string data);
        Response AddUpdateNewVariant(ProductVariant productVariant);
        Response AddUpdateProductCategoryAttribute(ProductVM productVM);
        Response GetProductAttributeList();
        Response GetSupplierSliderList();
        Response GetSupplierProductList(ProductVM productVM);
        Response GetSupplierProductDetail(long productId);
        List<ProductVariant> GetAllProductVariant();
        Response GetProductCategoryAttributeList();
        Response GetProductAttributeListByCategoryId(long categoryId, string categoryLevel);
        Response GetProductCategoryGroupListById(long subCategoryId);
        Task<Response> AddNewSupplierProduct(AddProductVM addProductVM);
        Task<Response> UpdateSupplierProduct(AddProductVM addProductVM);
        Task<Response> GetSupplierWithDetails(long supplierId);
        Task<Response> GetTopFiveProductCategory();
        Task<Response> AddAndUpdateSellerAccount(string data);

        Task<Response> AddAndUpdateBusinessAccount(string data);
        Task<Response> GetProductDetailWeb(long productId);
        Task<List<ProductVM>> GetProductDetailMob(long productId);
        Task<Response> GetHomeProductList(AddProductVM addProductVM);
        Task<Response> GetSupplierProductListWeb(ProductVM productVM);
        Task<Response> GetProductsByCategory(ProductVM productVM);
        Task<Response> GetProductsByName(ProductVM productVM);
        Task<Response> GetCountryList();
        Task<Response> GetSupplierShopUrl(string shopUrl);
        Task<Response> GetSupplierShopDetails(string shopUrl);
        Task<Response> GetSateList(int countryId);
        Task<Response> GetProductSearchTagsList(string inputText);
        Task<Response> GetAreaList(int? cityId);
        Task<Response> GetLocationList(int areaId);
        Task<Response> GetBanksList();
        Task<Response> SaveAndUpdateBankAccountData(string data);
        Task<Response> GetBankAccountData(long supplierId);
        Task<Response> SaveAndUpdateWhareHouseAddress(string data);
        Task<Response> GetWareHouseAddress(long supplierId);
        Task<Response> SaveAndUpdateReturnAddress(string data);
        Task<Response> SaveAndUpdateSocialLinks(string data);
        Task<Response> GetReturnAddress(long supplierId);
        Task<Response> GetSocialLinks(long supplierId, String isSupplierWeb);
        Task<Response> GetProductCategories();
        Task<Response> GetCategoryGroupsById(long subCategoryId);

        Task<Response> GetSalesSummary(string data);
        Task<Response> GetOrdersList(string data);
        Task<Response> PlaceOrder(PlaceOrderVM orderItemVM);
        Task<Response> GetOrderDetailsById(long orderId, long supplierId);
        Task<Response> GetSupplierProductById(long productId);
        Task<Response> GetVariantsByProductId(long productId);
        Task<Response> UpdateOrderStatus(string data);
        Task<Response> GetProfile(long supplierId);
        Task<Response> GetCustomerOrdersList(string data);
        Task<Response> GetCustomerOrderedProductsList(string data);
        Task<Response> GetSuppliersProductsListForApproval(string data);
        Task<Response> ApproveSupplierProduct(string data);
        Task<Response> UpdateSupplierAllGoodStatus(string data);
        Task<Response> GetSupplierProductImagesbyProductId(long productId);
        Task<Response> GetShippingCost(int id);
        Task<Response> GetShippingChargesList();
        Task<Response> AddUpdateShippingCost(string data);
        Task<Response> UpdateStockLevel(string data);
        Task<Response> CancelCustomerOrder(string data);
        Task<Response> GetCustomerCanclledOrdersList(string data);
        Task<Response> GetOrderCancellationReasonsList(int userRole);
        Task<Response> OrderCancelByAdmin(string data);
        Task<Response> GetCanellationReasonsListForAdmin();
        Task<Response> InsertAndUpdateCancellationReason(string data);
        Task<Response> GetOrderByOrderId(long orderId);
        Task<Response> AddSupplierLeadgerEntry(string data);

        Task<Response> GetSupplierProfileDetails(string data);
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
        Task<Response> GetTransactionHistory(TransactionsVM transactionsVM);
        Task<Response> GetLoggedSupplierCanelledOrdersList(string data);
        Task<Response> GetOrderTracking(string data);
        Task<Response> GetOrderedItemTracking(string data);
        Task<Response> GetProductsList(string productName);
        Task<Response> GetSupplierOrderBySupplierId(long supplierId);
        Task<Response> deleteLinkStatus(string data);


        Task<Response> GetProductsByTag(ProductVM productVM);
        Task<Response> GetAllCatSubCatGroupCategories();
        Task<Response> BlockProduct(long productId, bool status);
        Task<string> AddUpdateFreeShipping(string data);
        Task<Response> GetFreeShippingList(string data);
        Task<Response> DeleteFreeShipping(long freeShippingId);
        Task<Response> SupplierList();
        Task<Response> AddUpdateSupplierCommission(string data);
        Task<Response> GetSupplierCommissionList(string data);
      

    }

    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        private readonly IMapper _mapper;
        private readonly IShippingApiService _shippingApiService;
        public SupplierService(IUnitOfWork uow, IExceptionService Exc, IMapper mapper, IShippingApiService shippingApiService)
        {
            this.uow = uow;
            this.Exc = Exc;
            _mapper = mapper;
            this._shippingApiService = shippingApiService;
        }

        public List<ProductCategory> GetAllProductCatagories()
        {
            List<ProductCategory> test = new List<ProductCategory>();
            try
            {
                test = uow.Repository<ProductCategory>().GetAll().Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return test;
        }

        public IQueryable<ProductSubCategory> GetSubCategoriesByProductCategoryId(long productCategoryId)
        {
            try
            {
                return uow.Repository<ProductSubCategory>().Get(x => x.ProductCategoryId == productCategoryId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<ProductSubCategory>().AsQueryable();
            }
        }

        public IQueryable<SupplierAds> GetSupplierAdsByProductSubCategoryIds(List<long> productSubCategoryId)
        {
            try
            {
                return uow.Repository<SupplierAds>().Get(x => productSubCategoryId.Contains(x.ProductSubCategoryId)).Where(d => d.IsAvailable == true && d.ActiveTo >= DateTime.Now && d.ActiveFrom <= DateTime.Now).OrderByDescending(o => o.CreatedOn);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>().AsQueryable();
            }
        }

        public IQueryable<SupplierAds> GetSupplierAdsByProductCategoryId(long productCategoryId)
        {
            try
            {
                var supplierAds = uow.Repository<SupplierAds>().Get(d => d.IsAvailable == true && d.ActiveTo >= DateTime.Now && d.ProductCategoryId == productCategoryId).OrderByDescending(o => o.CreatedOn);
                return supplierAds;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>().AsQueryable();
            }
        }

        public IQueryable<SupplierAds> GetSupplierAdsBySupplierId(long supplierId)
        {
            try
            {
                return uow.Repository<SupplierAds>().Get(x => x.SupplierId == supplierId).OrderByDescending(o => o.CreatedOn);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>().AsQueryable();
            }
        }

        public async Task<Supplier> GetSupplierById(long supplierId)
        {
            try
            {
                return await uow.Repository<Supplier>().GetByIdAsync(supplierId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Supplier();
            }

        }

        public List<Supplier> GetSuppliersByIds(List<long> supplierIds)
        {
            try
            {
                List<Supplier> query = new List<Supplier>();
                query = uow.Repository<Supplier>().GetAll().Where(x => supplierIds.Contains(x.SupplierId)).ToList();
                return query;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Supplier>();
            }

        }

        public Supplier GetSupplierBySupplierId(long supplierId)
        {
            try
            {
                return uow.Repository<Supplier>().GetById(supplierId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Supplier();
            }
        }

        public SupplierAds GetSupplierAdByAdId(long supplierAdId)
        {
            var supplierAd = uow.Repository<SupplierAds>().GetById(supplierAdId);
            return supplierAd;
        }

        public async Task<Response> AddEditSupplier(Supplier supplier)
        {
            Response response = new Response();
            try
            {
                var supplierId = uow.Repository<Supplier>().GetAll().Where(x => x.UserId == supplier.UserId).Select(s => s.SupplierId).FirstOrDefault();
                supplier.SupplierId = supplier.SupplierId != 0 ? supplier.SupplierId : supplierId;
                if (supplier.SupplierId > 0)
                {
                    Supplier existingData = await GetSupplierById(supplier.SupplierId);
                    if (existingData != null)
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        string jsonValues = JsonConvert.SerializeObject(supplier, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<Supplier>().Update(existingData);
                    }
                }
                else
                {
                    await uow.Repository<Supplier>().AddAsync(supplier);
                }
                await uow.SaveAsync();

                response.ResultData = supplier;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public IQueryable<SupplierAds> GetSupplierAdsByStatusId(int statusId)
        {
            try
            {
                return uow.Repository<SupplierAds>().Get(x => x.AdsStatusId == statusId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>().AsQueryable();
            }
        }
        public async Task<Response> GetSuppliersOrderlistById(long id)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                    new SqlParameter("@orderId",id),
                };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<SupplierOrerListByIdVM>("Sp_GetSuppliersOrderlistById", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetOrderDetailById(long orderId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                        new SqlParameter("@orderId",orderId)
                    };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<OrderDetailDTO>("Sp_GetOrdersById", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetSuppliersOrderlist(OrderslistDTO orderslistDTO)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                        new SqlParameter("@CustomerId",orderslistDTO.CustomerId),
                        new SqlParameter("@CustomerName",orderslistDTO.CustomerName),
                        new SqlParameter("@StartDate",orderslistDTO.StartDate),
                        new SqlParameter("@EndDate",orderslistDTO.EndDate),
                        new SqlParameter("@city",orderslistDTO.City),
                        new SqlParameter("@OrderId",orderslistDTO.OrderId),
                      };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<SupplierOderListVM>("Sp_GetSuppliersOrderlist", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> UpdateStockLevel(string data)
        {
            Response response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<List<ProductStockDTO>>(data);
                foreach (var item in entity)
                {
                    SqlParameter[] sqlParameters =
                        {
                     new SqlParameter("@ProductId",item.ProductId ),
                     new SqlParameter("@VarientId",item.VarientId),
                     new SqlParameter("@Quantity",item.Quantity)
                    };
                    var result = uow.ExecuteReaderSingleDS<Response>("SP_Update_Stock", sqlParameters);
                    response.Message = result.Select(x => x.Message).FirstOrDefault();
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }
        public async Task<Response> GetOrderStatusList()
        {
            Response response = new Response();
            try
            {
                response.ResultData = uow.Repository<OrderStatus>().GetAll();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public long GetEntityIdByUserId(string userId)
        {
            long id = 0;
            try
            {
                id = uow.Repository<Supplier>().GetAll().FirstOrDefault(t => t.UserId == userId)?.SupplierId ?? 0;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return id;
        }

        public List<ProductSubCategory> GetAllProductSubCatagories(long productId)
        {
            try
            {
                return uow.Repository<ProductSubCategory>().GetAll().Where(x => x.ProductCategoryId == productId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategory>();
            }
        }

        public bool AddRegistrationDetail(Supplier supplier)
        {
            try
            {
                uow.Repository<Supplier>().Add(supplier);
                uow.Save();
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }
        public Response AddNewProducts(ProductCategoryVM productCategory)
        {
            Response response = new Response();
            if (!string.IsNullOrWhiteSpace(productCategory.imagebase64))
            {
                var checkFormatList = productCategory.imagebase64.Split(',');

                if (checkFormatList[0] == "data:image/png;base64")
                {
                    string convert = productCategory.imagebase64.Replace("data:image/png;base64,", String.Empty);
                    productCategory.ProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                else if (checkFormatList[0] == "data:image/jpg;base64")
                {
                    string convert = productCategory.imagebase64.Replace("data:image/jpg;base64,", String.Empty);
                    productCategory.ProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                else if (checkFormatList[0] == "data:image/jpeg;base64")
                {
                    string convert = productCategory.imagebase64.Replace("data:image/jpeg;base64,", String.Empty);
                    productCategory.ProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                else if (checkFormatList[0] != "" && checkFormatList[0].Length > 200)
                {
                    productCategory.ProductImage = !string.IsNullOrEmpty(checkFormatList[0]) ? Convert.FromBase64String(checkFormatList[0]) : new byte[0];
                }
            }
            try
            {
                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@ProductCategoryId",productCategory.ProductCategoryId ),
                new SqlParameter("@Code",productCategory.Code),
                new SqlParameter("@Name",productCategory.Name),
                new SqlParameter("@IsActive",productCategory.IsActive),
                new SqlParameter("@orderByColumn",productCategory.OrderByColumn),
                new SqlParameter("@ProductImage",productCategory.ProductImage),
                new SqlParameter("@CreatedOn",DateTime.Now),
                new SqlParameter("@CreatedBy",productCategory.CreatedBy),
                new SqlParameter("@ModifyOn" ,DateTime.Now),
                new SqlParameter("@ModifyBy",productCategory.ModifiedBy),
                  new SqlParameter("@SeoTitle",productCategory.SeoTitle),
                    new SqlParameter("@SeoDescription",productCategory.SeoDescription),
                      new SqlParameter("@OgTitle",productCategory.OgTitle),
                        new SqlParameter("@OgDescription",productCategory.OgDescription),
                          new SqlParameter("@Canonical",productCategory.Canonical),
                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_InsertUpdateProducts", sqlParameters);
                response.Message = result.Select(x => x.Message).FirstOrDefault();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public Response AddUpdateNewVariant(ProductVariant productVariant)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@Id",productVariant.Id ),
                    new SqlParameter("@ColorName",productVariant.ColorName),
                    new SqlParameter("@HexCode",productVariant.HexCode),
                    new SqlParameter("@IsActive",productVariant.IsActive),
                    new SqlParameter("@CreatedOn",DateTime.Now),
                    new SqlParameter("@CreatedBy",productVariant.CreatedBy),
                    new SqlParameter("@ModifyOn" ,DateTime.Now),
                    new SqlParameter("@ModifyBy",productVariant.ModifiedBy),
                    };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_InsertUpdateProductVariant", sqlParameters);
                response.Message = result.Select(x => x.Message).FirstOrDefault();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }



        public Response AddNewSubProducts(SubProducts subProducts)
        {
            Response response = new Response();
            if (!string.IsNullOrWhiteSpace(subProducts.imagebase64))
            {
                var checkFormatList = subProducts.imagebase64.Split(',');

                if (checkFormatList[0] == "data:image/png;base64")
                {
                    string convert = subProducts.imagebase64.Replace("data:image/png;base64,", String.Empty);
                    subProducts.SubProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                else if (checkFormatList[0] == "data:image/jpg;base64")
                {
                    string convert = subProducts.imagebase64.Replace("data:image/jpg;base64,", String.Empty);
                    subProducts.SubProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                else if (checkFormatList[0] == "data:image/jpeg;base64")
                {
                    string convert = subProducts.imagebase64.Replace("data:image/jpeg;base64,", String.Empty);
                    subProducts.SubProductImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
            }
            try
            {
                var obj = uow.Repository<ProductCategory>().Get(o => o.ProductCategoryId == subProducts.ProductCategoryId && o.HasSubItems == true).FirstOrDefault();
                if (obj == null)
                {
                    var updateCatagory = uow.Repository<ProductCategory>().Get(o => o.ProductCategoryId == subProducts.ProductCategoryId).FirstOrDefault();
                    updateCatagory.HasSubItems = true;
                    uow.Repository<ProductCategory>().Update(updateCatagory);
                    uow.SaveAsync();
                }
                //obj.HasSubItems = obj.Active == true ? false : true;
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@ProductSubCategoryId",subProducts.ProductSubCategoryId ),
                new SqlParameter("@ProductCategoryId",subProducts.ProductCategoryId ),
                new SqlParameter("@Code",subProducts.SubCategoryCode),
                new SqlParameter("@Name",subProducts.SubCategoryName),
                new SqlParameter("@IsActive",subProducts.IsActive),
                new SqlParameter("@orderbycolumn",subProducts.OrderByColumn),
                new SqlParameter("@SubProductImage",subProducts.SubProductImage),
                  new SqlParameter("@SeoTitle",subProducts.SeoTitle),
                    new SqlParameter("@SeoDescription",subProducts.SeoDescription),
                      new SqlParameter("@OgTitle",subProducts.OgTitle),
                        new SqlParameter("@OgDescription",subProducts.OgDescription),
                          new SqlParameter("@Canonical",subProducts.Canonical),
                new SqlParameter("@CreatedOn",DateTime.Now),
                new SqlParameter("@CreatedBy",subProducts.CreatedBy),
                new SqlParameter("@ModifyOn" ,DateTime.Now),
                new SqlParameter("@ModifyBy",subProducts.ModifiedBy),
                new SqlParameter("@Commission",subProducts.Commission),
            };
                var res = uow.ExecuteReaderSingleDS<Response>("Sp_InsertUpdateSubProducts", sqlParameters);
                response.Message = res.Select(x => x.Message).FirstOrDefault();
                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public Response AddUpdateProductsCategoryGroup(ProductCategoryGroup productCategoryGroup)
        {
            Response response = new Response();
            try
            {
                if (productCategoryGroup != null)
                {
                    var obj = uow.Repository<ProductSubCategory>().Get(o => o.ProductSubCategoryId == productCategoryGroup.SubCategoryId && o.HasSubItems == true).FirstOrDefault();
                    if (obj == null)
                    {
                        var updateCatagory = uow.Repository<ProductSubCategory>().Get(o => o.ProductSubCategoryId == productCategoryGroup.SubCategoryId).FirstOrDefault();
                        updateCatagory.HasSubItems = true;
                        uow.Repository<ProductSubCategory>().Update(updateCatagory);
                        uow.SaveAsync();
                    }
                    SqlParameter[] sqlParameters =
                     {
                        new SqlParameter("@id",productCategoryGroup?.Id ),
                        new SqlParameter("@SubCategoryId",productCategoryGroup.SubCategoryId ),
                        new SqlParameter("@Name",productCategoryGroup.Name ),
                        new SqlParameter("@Active",productCategoryGroup.Active),
                        new SqlParameter("@CreatedOn",DateTime.Now),
                        new SqlParameter("@CreatedBy",productCategoryGroup?.CreatedBy),
                        new SqlParameter("@ModifyOn" ,DateTime.Now),
                        new SqlParameter("@ModifyBy",productCategoryGroup?.ModifiedBy),
                         new SqlParameter("@SeoTitle",productCategoryGroup?.SeoTitle),
                    new SqlParameter("@SeoDescription",productCategoryGroup?.SeoDescription),
                      new SqlParameter("@OgTitle",productCategoryGroup?.OgTitle),
                        new SqlParameter("@OgDescription",productCategoryGroup?.OgDescription),
                          new SqlParameter("@Canonical",productCategoryGroup?.Canonical),
                    };
                    var res = uow.ExecuteReaderSingleDS<Response>("Sp_InsertUpdateProductGroupCategory", sqlParameters);
                    response.Message = res.Select(x => x.Message).FirstOrDefault();
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error";
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public Supplier GetAllSupplier(long supplierId)
        {
            try
            {
                return uow.Repository<Supplier>().GetAll().Where(x => x.SupplierId == supplierId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Supplier();
            }
        }

        public List<ProductCategoryGroupVM> GetAllProduCatcategoryGroup()
        {
            try
            {
                List<ProductCategoryGroupVM> categoryGroups = new List<ProductCategoryGroupVM>();
                SqlParameter[] sqlParameters = {
                    };
                categoryGroups = uow.ExecuteReaderSingleDS<ProductCategoryGroupVM>("Sp_GetProductcategoryGroup", sqlParameters).ToList();
                return categoryGroups;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductCategoryGroupVM>();
            }
        }
        public List<ProductVariant> GetAllProductVariant()
        {
            try
            {
                List<ProductVariant> categoryGroups = new List<ProductVariant>();
                SqlParameter[] sqlParameters = {
                        };
                categoryGroups = uow.ExecuteReaderSingleDS<ProductVariant>("Sp_GetProductVariant", sqlParameters).ToList();
                return categoryGroups;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductVariant>();
            }
        }

        public List<SupplierAds> GetAllSupplierAds(long supplierId)
        {
            try
            {
                return uow.Repository<SupplierAds>().GetAll().Where(x => x.SupplierId == supplierId).OrderByDescending(s => s.ActiveFrom).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>();
            }
        }

        public ProductCategory productCategory(long productId)
        {
            try
            {
                return uow.Repository<ProductCategory>().GetAll().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ProductCategory();
            }
        }

        public SupplierAds GetEditAdDetail(long supplierAdsId)
        {
            try
            {
                return uow.Repository<SupplierAds>().GetById(supplierAdsId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierAds();
            }
        }

        public long GetProductCategoryIdBySupplierId(long supplierId)
        {
            try
            {
                return uow.Repository<Supplier>().GetAll().Where(s => s.SupplierId == supplierId).Select(pid => pid.ProductCategoryId.HasValue ? pid.ProductCategoryId.Value : 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }
        public Response CheckProductAvailability(string productName)
        {
            Response response = new Response();
            try
            {
                ProductCategory productCategory = uow.Repository<ProductCategory>().Get().Where(x => x.Name == productName).FirstOrDefault();
                if (productCategory != null)
                {
                    response.Message = "Exist";
                    response.Status = ResponseStatus.Restrected;
                }
                else
                {
                    response.Message = "True";
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public List<ProductCategory> GetAllProductCategory()
        {
            try
            {
                //return uow.Repository<ProductCategory>().GetAll().Where(x => x.IsActive == true).ToList();
                return uow.Repository<ProductCategory>().GetAll().ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductCategory>();
            }
        }
        public Response GetProfileVerification(long? supplierId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@supplierId",supplierId),
                };

                response.ResultData = uow.ExecuteReaderSingleDS<ProfileVerificationVM>("SP_GetProfileVerification", sqlParameters);
                if (response.ResultData != null)
                    response.Status = ResponseStatus.OK;
                else
                    response.Status = ResponseStatus.Restrected;
              
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
            return response;
        }

        public long SaveAndUpdateAd(SupplierAds model)
        {
            try
            {
                if (model.SupplierAdsId > 0)
                {
                    SupplierAds supplierAds = uow.Repository<SupplierAds>().GetById(model.SupplierAdsId);
                    model.ActiveFrom = supplierAds.ActiveFrom >= model.ActiveFrom ? supplierAds.ActiveFrom : model.ActiveFrom;
                    model.ActiveTo = supplierAds.ActiveTo >= model.ActiveTo ? supplierAds.ActiveTo : model.ActiveTo;
                    model.CreatedOn = supplierAds.CreatedOn;
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    string jsonValues = JsonConvert.SerializeObject(model, jsonSerializerSettings);
                    JsonConvert.PopulateObject(jsonValues, supplierAds);
                    uow.Repository<SupplierAds>().Update(supplierAds);
                }
                else
                {
                    model.CreatedOn = DateTime.Now;
                    model.ActiveFrom = DateTime.Now.Date;
                    model.ActiveTo = model.ActiveTo < DateTime.Now ? model.ActiveFrom.AddDays(21) : model.ActiveTo;
                    model.AdsStatusId = model.AdsStatusId == 0 ? 1 : model.AdsStatusId;
                    uow.Repository<SupplierAds>().Add(model);
                }
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                string exceptionMessage = ex.Message;
            }
            return model.SupplierAdsId;
        }

        public async Task<Response> RelistAd(long SupplierAdsId)
        {
            Response response = new Response();

            try
            {
                if (SupplierAdsId > 0)
                {
                    SupplierAds supplierAds = uow.Repository<SupplierAds>().GetById(SupplierAdsId);
                    supplierAds.ActiveFrom = DateTime.Now.Date;
                    supplierAds.ActiveTo = DateTime.Now.Date.AddDays(21);
                    supplierAds.CreatedOn = DateTime.Now;
                    supplierAds.ModifiedOn = DateTime.Now;
                    supplierAds.ModifiedBy = supplierAds.CreatedBy;
                    //JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    //string jsonValues = JsonConvert.SerializeObject(model, jsonSerializerSettings);
                    //JsonConvert.PopulateObject(jsonValues, supplierAds);
                    uow.Repository<SupplierAds>().Update(supplierAds);
                    await uow.SaveAsync();

                    response.Message = "Ad relisted successfully";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Ad reference does not exist";
                    response.Status = ResponseStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                string exceptionMessage = ex.Message;

                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }

            return response;
        }
        public SupplierProductAdDetails GetSupplierAdDetails(long adId)
        {
            SupplierProductAdDetails supplierProductAdDetails = new SupplierProductAdDetails();

            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@adId",adId)
                };

                if (adId > 0)
                {
                    supplierProductAdDetails = uow.ExecuteReaderSingleDS<SupplierProductAdDetails>("SP_GetSupplierAdDetails", sqlParameters).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                string exceptionMessage = ex.Message;
            }

            return supplierProductAdDetails;
        }


        public async Task<bool> UpdateAdcStatus(long supplierAdsId, long supplieradsStatusId, int days)
        {
            SupplierAds supplierAds = new SupplierAds();

            SupplierAds existingdata = uow.Repository<SupplierAds>().GetById(supplierAdsId);
            try
            {
                if (existingdata != null)
                {

                    existingdata.AdsStatusId = Convert.ToInt32(supplieradsStatusId);
                    existingdata.ActiveFrom = DateTime.Now;
                    existingdata.ActiveTo = existingdata.ActiveFrom.AddDays(days);
                    uow.Repository<SupplierAds>().Update(existingdata);
                    await uow.SaveAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public void UpdateSupplierAdViewCount(long supplierAdId)
        {
            try
            {
                SupplierAds existingdata = uow.Repository<SupplierAds>().GetAll().Where(x => x.SupplierAdsId == supplierAdId).FirstOrDefault();
                if (existingdata.AdViewCount != null)
                {
                    existingdata.AdViewCount++;
                }
                else
                {
                    existingdata.AdViewCount = 1;
                }
                uow.Repository<SupplierAds>().Update(existingdata);
                uow.Save();


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

        }

        public Response UpdateAd(SupplierAds supplierAds)
        {
            SupplierAds ads = uow.Repository<SupplierAds>().GetAll().Where(x => x.SupplierAdsId == supplierAds.SupplierAdsId).FirstOrDefault();
            try
            {
                if (ads != null)
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                    string jsonValues = JsonConvert.SerializeObject(supplierAds, jsonSerializerSettings);
                    JsonConvert.PopulateObject(jsonValues, ads);
                    uow.Repository<SupplierAds>().Update(ads);
                    uow.Save();
                }

                return new Response()
                {
                    Message = "Information saved successfully.",
                    Status = ResponseStatus.OK,
                };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public Supplier GetSupplierByUserId(string userId)
        {
            try
            {
                return uow.Repository<Supplier>().GetAll().FirstOrDefault(x => x.UserId == userId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Supplier();
            }
        }
        public async Task<Response> SetSupplierSubCategories(List<SupplierSubCategory> subCategories)
        {
            Response response = new Response();
            IRepository<SupplierSubCategory> repository = uow.Repository<SupplierSubCategory>();

            try
            {
                IQueryable<SupplierSubCategory> deleteQuery = repository.GetAll().Where(s => s.SupplierId == subCategories.FirstOrDefault().SupplierId);
                await repository.DeleteAllAsync(deleteQuery);

                foreach (SupplierSubCategory subCategory in subCategories)
                {
                    await repository.AddAsync(subCategory);
                }

                await uow.SaveAsync();

                response.Message = "Successfully updated.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public Supplier GetSupplierTradeName(long supplierId)
        {
            try
            {
                return uow.Repository<Supplier>().GetById(supplierId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Supplier();
            }
        }

        public async Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long productCatgoryId)
        {
            try
            {
                return await uow.Repository<ProductSubCategory>().GetAll()
                        .Where(x => x.ProductCategoryId == productCatgoryId).OrderBy(y => y.SubCategoryName)
                        .ProjectTo<ProductSubCategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategoryDTO>();
            }
        }

        public bool UpdatePersonalDetail(PersonalDetailsVM model)
        {
            try
            {
                Supplier supplier = uow.Repository<Supplier>().Get().Where(x => x.SupplierId == model.SupplierId).FirstOrDefault();

                if (supplier != null)
                {
                    supplier.EmailAddress = model.EmailAddress;
                    supplier.MobileNumber = model.MobileNumber;
                    uow.Repository<Supplier>().Update(supplier);
                    uow.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public void DeleteAd(long suuplierAdId)
        {
            try
            {
                uow.Repository<SupplierAds>().Delete((int)suuplierAdId);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

        }

        public List<ProductSubCategory> AllSubCategory()
        {
            try
            {
                return uow.Repository<ProductSubCategory>().GetAll().Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategory>();
            }

        }

        public List<SupplierSubCategory> GetSelectedSupplierSubCategory(long supplierId)
        {
            try
            {
                return uow.Repository<SupplierSubCategory>().GetAll().Where(x => x.SupplierId == supplierId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierSubCategory>();
            }
        }

        public List<ProductSubCategory> getProductSubCategoriesById(List<long> supplierSubCategoryId)
        {
            try
            {
                return uow.Repository<ProductSubCategory>().GetAll().Where(x => supplierSubCategoryId.Contains(x.ProductSubCategoryId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductSubCategory>();
            }
        }

        public List<ManageAdsVM> SpGetActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@supplierId",supplierId),
            };
                var result = uow.ExecuteReaderSingleDS<ManageAdsVM>("SP_ActiveAds", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ManageAdsVM>();
            }


        }

        public List<InactiveManageAdsVM> SpGetInActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@supplierId",supplierId),
            };
                var result = uow.ExecuteReaderSingleDS<InactiveManageAdsVM>("SP_InActiveAds", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InactiveManageAdsVM>();
            }

        }

        public List<ManageAdsVMWithImage> SpGetActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@supplierId",supplierId),
            };
                var result = uow.ExecuteReaderSingleDS<ManageAdsVMWithImage>("SP_ActiveAds_Web", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ManageAdsVMWithImage>();
            }


        }

        public List<InactiveManageAdsVMWithImages> SpGetInActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@pageNumber",pageNumber),
                new SqlParameter("@supplierId",supplierId),
            };
                var result = uow.ExecuteReaderSingleDS<InactiveManageAdsVMWithImages>("SP_InActiveAds_Web", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InactiveManageAdsVMWithImages>();
            }

        }

        public string GetCategoryName(long productId)
        {
            try
            {
                return uow.Repository<ProductCategory>().Get().Where(p => p.ProductCategoryId == productId).Select(c => c.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
        }

        public List<MarketSimilarProductsVM> MarketSimilarProductsVMs(long categoryId)
        {
            List<MarketSimilarProductsVM> marketproductsVM = new List<MarketSimilarProductsVM>();
            try
            {
                var supplierAds = uow.Repository<SupplierAds>().GetAll().Where(e => e.ProductCategoryId == categoryId && e.ActiveTo >= DateTime.Now.Date).ToList();
                var catageoryName = uow.Repository<ProductCategory>().GetById(categoryId);
                foreach (var item in supplierAds)
                {
                    MarketSimilarProductsVM marketSimilarProductsVM = new MarketSimilarProductsVM
                    {
                        AdBudget = item.Price.ToString("N0"),
                        CategoryName = catageoryName.Name,
                        AdTitle = item.AdTitle,
                        SupplierAdId = item.SupplierAdsId
                    };
                    marketproductsVM.Add(marketSimilarProductsVM);
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return marketproductsVM;
        }

        public SupplierAds GetSupplierAdsById(long supplierId)
        {
            SupplierAds supplierAds = uow.Repository<SupplierAds>().Get().Where(x => x.SupplierId == supplierId).FirstOrDefault();
            return supplierAds;
        }

        public List<Supplier> AllSupplier()
        {
            List<Supplier> suppliers = new List<Supplier>();
            suppliers = uow.Repository<Supplier>().GetAll().ToList();
            return suppliers;
        }

        public List<SupplierAds> GetAllAds()
        {
            return uow.Repository<SupplierAds>().GetAll().ToList();
        }

        public List<ProductSubCategory> GetAllSubcategory(string search)
        {
            return uow.Repository<ProductSubCategory>().GetAll().Where(x => x.SubCategoryName.Contains(search)).ToList();
        }


        public List<SpSupplierListVM> SpGetSupplierList(GenericUserVM genericUserVM)
        {
            try
            {



                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageSize",genericUserVM.pageSize),
                    new SqlParameter("@pageNumber",genericUserVM.pageNumber),
                    new SqlParameter("@dataOrderBy",genericUserVM.dataOrderBy),
                    new SqlParameter("@supplierName",genericUserVM.userName),
                    new SqlParameter("@startDate",genericUserVM.startDate),
                    new SqlParameter("@endDate",genericUserVM.endDate),
                    new SqlParameter("@city",genericUserVM.city),
                    new SqlParameter("@categories",genericUserVM.categories),
                    new SqlParameter("@location",genericUserVM.location),
                    new SqlParameter("@cnic",genericUserVM.cnic),
                    new SqlParameter("@usertype",genericUserVM.usertypeid),
                    new SqlParameter("@emailtype",genericUserVM.emailtype),
                    new SqlParameter("@mobileType",genericUserVM.mobileType),
                    new SqlParameter("@mobile",genericUserVM.mobile),
                    new SqlParameter("@sourceOfReg",genericUserVM.sourceOfReg),
                    new SqlParameter("@email",genericUserVM.email),
                    new SqlParameter("@SalesmanId",genericUserVM.SalesmanId),

                };

                List<SpSupplierListVM> result = uow.ExecuteReaderSingleDS<SpSupplierListVM>("Sp_SupplierList", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }

        }
        public List<SpSupplierListVM> SpGetHoomWorkSupplierList(GenericUserVM genericUserVM)
        {
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageSize",genericUserVM.pageSize),
                    new SqlParameter("@pageNumber",genericUserVM.pageNumber),
                    new SqlParameter("@dataOrderBy",genericUserVM.dataOrderBy),
                    new SqlParameter("@supplierName",genericUserVM.userName),
                    new SqlParameter("@startDate",genericUserVM.startDate),
                    new SqlParameter("@endDate",genericUserVM.endDate),
                    new SqlParameter("@city",genericUserVM.city),
                    new SqlParameter("@categories",genericUserVM.categories),
                    new SqlParameter("@location",genericUserVM.location),
                    new SqlParameter("@cnic",genericUserVM.cnic),
                    new SqlParameter("@usertype",genericUserVM.usertypeid),
                    new SqlParameter("@emailtype",genericUserVM.emailtype),
                    new SqlParameter("@mobileType",genericUserVM.mobileType),
                    new SqlParameter("@mobile",genericUserVM.mobile),
                    new SqlParameter("@sourceOfReg",genericUserVM.sourceOfReg),
                    new SqlParameter("@email",genericUserVM.email),
                    new SqlParameter("@SalesmanId",genericUserVM.SalesmanId),

                };

                List<SpSupplierListVM> result = uow.ExecuteReaderSingleDS<SpSupplierListVM>("SpGetHoomWorkSupplierList", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }

        }
        public List<SpSupplierListVM> SpGetLocalSupplierList(GenericUserVM genericUserVM)
        {
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageSize",genericUserVM.pageSize),
                    new SqlParameter("@pageNumber",genericUserVM.pageNumber),
                    new SqlParameter("@dataOrderBy",genericUserVM.dataOrderBy),
                    new SqlParameter("@supplierName",genericUserVM.userName),
                    new SqlParameter("@startDate",genericUserVM.startDate),
                    new SqlParameter("@endDate",genericUserVM.endDate),
                    new SqlParameter("@city",genericUserVM.city),
                    new SqlParameter("@categories",genericUserVM.categories),
                    new SqlParameter("@location",genericUserVM.location),
                    new SqlParameter("@cnic",genericUserVM.cnic),
                    new SqlParameter("@usertype",genericUserVM.usertypeid),
                    new SqlParameter("@emailtype",genericUserVM.emailtype),
                    new SqlParameter("@mobileType",genericUserVM.mobileType),
                    new SqlParameter("@mobile",genericUserVM.mobile),
                    new SqlParameter("@sourceOfReg",genericUserVM.sourceOfReg),
                    new SqlParameter("@email",genericUserVM.email),
                    new SqlParameter("@SalesmanId",genericUserVM.SalesmanId),

                };

                List<SpSupplierListVM> result = uow.ExecuteReaderSingleDS<SpSupplierListVM>("SpGetLocalSupplierList", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpSupplierListVM>();
            }

        }
        public List<SupplierListVM> GetSupplierList()
        {
            try
            {
                SqlParameter[] sqlParameters = {
                };

                List<SupplierListVM> result = uow.ExecuteReaderSingleDS<SupplierListVM>("SP_SupplierProfileList", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierListVM>();
            }

        }

        public List<CustomerFeedBack> GetCustomerFeedBackList(string data)
        {
            try
            {
                var entity = JsonConvert.DeserializeObject<CustomerFeedBackParams>(data);
                SqlParameter[] sqlParameters = {
                        new SqlParameter("@pageSize",entity.PageSize),
                        new SqlParameter("@pageNumber",entity.PagesNumber),
                        new SqlParameter("@customerId",entity.CustomerId),
                        new SqlParameter("@productId",entity.ProductId),
                      };

                List<CustomerFeedBack> result = uow.ExecuteReaderSingleDS<CustomerFeedBack>("GetCustomerFeedBack", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CustomerFeedBack>();
            }

        }


        public SpSupplierStatsVM SpGetSupplierStats()
        {
            try
            {
                SqlParameter[] sqlParameters = { };

                SpSupplierStatsVM result = uow.ExecuteReaderSingleDS<SpSupplierStatsVM>("Sp_SupplierStats", sqlParameters).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpSupplierStatsVM();
            }

        }

        public List<Supplier> GetAllSuppliers()
        {
            return uow.Repository<Supplier>().GetAll().ToList();
        }

        public List<Supplier> GetAllSuppliersYearlyReport()
        {
            try
            {
                var res = uow.Repository<Supplier>().GetAll().Where(x => x.CreatedOn.Value.Year == DateTime.Now.Year);
                return res.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<Supplier>();
            }
        }

        public List<Supplier> GetAllSuppliersFromToReport(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var res = uow.Repository<Supplier>().GetAll().Where(x => x.CreatedOn.Value.Date >= StartDate.Date && x.CreatedOn.Value.Date <= EndDate.Date);
                return res.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<Supplier>();
            }
        }

        public List<IdValueVM> GetCategoriesForDropDown()
        {
            try
            {
                return uow.Repository<ProductCategory>().GetAll().Where(s => s.IsActive == true).Select(s => new IdValueVM { Id = s.ProductCategoryId, Value = s.Name }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }
        public List<IdValueCategoryVM> GetSubCategoriesForDropDown(long categoryId)
        {
            try
            {
                return uow.Repository<ProductSubCategory>().GetAll().Where(s => s.ProductCategoryId == categoryId && s.IsActive == true).Select(s => new IdValueCategoryVM { SubCategoryId = s.ProductSubCategoryId, SubCategoryName = s.SubCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueCategoryVM>();
            }
        }
        public List<ProductCategoryVM> GetCategoriesForListing(int productCategoryId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@Status", true),
                    new SqlParameter("@productCategoryId",productCategoryId),
                    };
                var result = uow.ExecuteReaderSingleDS<ProductCategoryVM>("SP_GetAllProducts", sqlParameters);
                return result;
                // uow.Repository<ProductCategory>().GetAll().Select(x => x).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductCategoryVM>();
            }
        }

        public List<ProductCategoryVM> GetCategoriesForAdminListing()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var result = uow.ExecuteReaderSingleDS<ProductCategoryVM>("SP_GetAllProductsAdmin", sqlParameters);
                return result;
                // uow.Repository<ProductCategory>().GetAll().Select(x => x).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ProductCategoryVM>();
            }
        }
        public List<IdValueDTO> GetCategoriesNameWithId()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var result = uow.ExecuteReaderSingleDS<IdValueDTO>("SP_GetCategoriesNameWithId", sqlParameters);
                return result;
                // uow.Repository<ProductCategory>().GetAll().Select(x => x).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueDTO>();
            }
        }
        public ImageVM GetCategoryImageById(long categoryImageId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@categoryImageId", categoryImageId),
                };
                ImageVM result = uow.ExecuteReaderSingleDS<ImageVM>("SP_GetCategoryImageById", sqlParameters).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ImageVM();
            }
        }
        public List<SupplierDTO> GetSupplierForReport(DateTime? StartDate, DateTime? EndDate, string categories, string supplier, string city, bool lastActive, string location, string mobile, string cnic, string userType, string emailtype, string mobileType)
        // public List<SupplierDTO> GetSupplierForReport(ReportsAndUsersVM reportsAndUsersVM)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@categories", categories),
                new SqlParameter("@Supplier",supplier),
                new SqlParameter("@City", city),
                new SqlParameter("@LastActive",lastActive),
                new SqlParameter("@Location",location),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@userType",userType),
                new SqlParameter("@emailtype",emailtype),
                new SqlParameter("@mobileType",mobileType),
                new SqlParameter("@cnic",cnic),

            };
                var result = uow.ExecuteReaderSingleDS<SupplierDTO>("Sp_SupplierRegistretionDynamic_Report", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierDTO>();
            }


        }

        public List<Supplier> GetSupplierReport(List<string> userId)
        {
            try
            {
                List<Supplier> suppliers = new List<Supplier>();
                suppliers = uow.Repository<Supplier>().GetAll().Where(s => userId.Contains(s.UserId)).ToList();
                return suppliers;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Supplier>();
            }
        }

        public bool UpdateSupplierPublicId(long supplierId, string publicId)
        {
            try
            {
                Supplier supplier = uow.Repository<Supplier>().GetById(supplierId);
                if (supplier != null)
                {
                    supplier.PublicId = publicId;

                    uow.Repository<Supplier>().Update(supplier);
                    uow.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public List<SupplierDTO> GetSupplierLast24HourRegistred(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@FromDate", StartDate),
                new SqlParameter("@ToDate",EndDate)
            };
                var result = uow.ExecuteReaderSingleDS<SupplierDTO>("Sp_PrimarySupplier_Report", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierDTO>();
            }
        }
        public List<SupplierAdsDTO> GetPostedadsLastDay(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@FromDate", StartDate),
                new SqlParameter("@ToDate",EndDate)
            };
                var result = uow.ExecuteReaderSingleDS<SupplierAdsDTO>("Sp_PrimaryAds_Report", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAdsDTO>();
            }
        }
        public List<SupplierAdsDTO> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, DateTime? StartDate, DateTime? EndDate, string supplier, string city, bool lastActive, string location, string adId)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@pageSize", pageSize),
                new SqlParameter("@pageNumber", pageNumber),
                new SqlParameter("@dataOrderBy", dataOrderBy),
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate",EndDate),
                new SqlParameter("@supplier", supplier),
                new SqlParameter("@city",city),
                new SqlParameter("@lastActive", lastActive),
                new SqlParameter("@location",location),
                new SqlParameter("@adId",adId)
            };
                var result = uow.ExecuteReaderSingleDS<SupplierAdsDTO>("Sp_PostedAdsDynamic_Report", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAdsDTO>();
            }
        }
        public List<Supplier> GetSupplierAddressList()
        {
            try
            {
                List<Supplier> customersAddressList = new List<Supplier>();
                customersAddressList = uow.Repository<Supplier>().GetAll().Select(x => new Supplier { BusinessAddress = x.BusinessAddress }).Where(x => x.BusinessAddress != null && x.BusinessAddress != "").ToList();
                return customersAddressList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Supplier>();
            }
        }
        public List<SupplierAds> GetSupplierAdsAddressList()
        {
            try
            {
                List<SupplierAds> customersAddressList = new List<SupplierAds>();
                customersAddressList = uow.Repository<SupplierAds>().GetAll().Select(x => new SupplierAds { Address = x.Address }).Where(x => x.Address != null && x.Address != "").ToList();
                return customersAddressList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAds>();
            }
        }
        public Response BlockSupplier(string supplierId, bool status)
        {
            Response response = new Response();
            try
            {
                int id = Convert.ToInt32(supplierId);
                var isSupplier = uow.Repository<Supplier>().GetById(id);
                if (isSupplier != null)
                {
                    if (status)
                    {
                        isSupplier.IsActive = true;
                    }
                    else
                    {
                        isSupplier.IsActive = false;
                    }
                    uow.Repository<Supplier>().Update(isSupplier);
                    uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Supplier status has been changed!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error in changing Supplier status!";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;

        }
        public List<SupplierDTO> SupplierByCategory(SupplierByCatVM supplierByCatVM)
        {
            try
            {
                var let = uow.Repository<Supplier>().GetAll();
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@categories",supplierByCatVM.catIds),
                    new SqlParameter("@cities",supplierByCatVM.productId),
                    new SqlParameter("@location",supplierByCatVM.location),
                    new SqlParameter("@usertype",supplierByCatVM.usertype),
                    new SqlParameter("@mobileType",supplierByCatVM.mobileType),
                    new SqlParameter("@emailtype",supplierByCatVM.emailtype),
                    new SqlParameter("@startDate",supplierByCatVM.startDate),
                    new SqlParameter("@endDate",supplierByCatVM.endDate),

                };
                SqlParameter[] sqlParameters1 = { };
                List<SupplierDTO> supplierDTO = uow.ExecuteReaderSingleDS<SupplierDTO>("SP_SupplierByCategory", sqlParameters);
                return supplierDTO;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierDTO>();
            }
        }


        public List<SubProducts> GetAllSubProducts()
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@status",true),
                };
                SqlParameter[] sqlParameters1 = { };
                List<SubProducts> supplierDTO = uow.ExecuteReaderSingleDS<SubProducts>("Sp_GetAllSubProducts", sqlParameters);
                return supplierDTO;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SubProducts>();
            }
        }

        public Response GetBusinessDetailsStatus(string id)
        {
            Response response = new Response();
            Supplier supplier = uow.Repository<Supplier>().Get(x => x.UserId == id).FirstOrDefault();

            if (supplier.CityId != null && supplier.BusinessAddress != null)
            {
                response.Status = ResponseStatus.OK;
                response.ResultData = "Successfully";
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "supplier are not exist";
            }


            return response;
        }

        public Response FeaturedSupplier(HW.SupplierViewModels.FeaturedSupplier featuredSupplier)
        {
            Response response = new Response();
            //var splitName = skill.Base64Image.Split(',');
            //string convert = skill.Base64Image.Replace(splitName[0] + ",", String.Empty);
            //skill1.SkillImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
            Supplier supplier = uow.Repository<Supplier>().Get(x => x.SupplierId == featuredSupplier.supplierId).FirstOrDefault();
            if (supplier != null)
            {
                supplier.FeaturedSupplier = featuredSupplier.featuredSupplier;
                uow.Repository<Supplier>().Update(supplier);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Data Added Succesfully!";
                return response;
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Something went wrong!";
                return response;
            }
            //var i = 0;
            //foreach (var item in featuredSupplier.base64ImageArray)
            //{
            //    if(!string.IsNullOrEmpty(item))
            //    {
            //        FeaturedSupplierImages featuredSupplierImages = new FeaturedSupplierImages();
            //        if (i == 0)
            //            featuredSupplierImages.IsActive = featuredSupplier.img1;
            //        else if (i == 1)
            //            featuredSupplierImages.IsActive = featuredSupplier.img2;
            //        else
            //            featuredSupplierImages.IsActive = featuredSupplier.img3;


            //        var splitName = item.Split(',');
            //        string convert = item.Replace(splitName[0] + ",", String.Empty);
            //        featuredSupplierImages.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
            //        featuredSupplierImages.SupplierId = featuredSupplier.supplierId;
            //        uow.Repository<FeaturedSupplierImages>().Add(featuredSupplierImages);
            //        uow.Save();
            //    }
            //    i++;
            //}
        }

        public List<SupplierListVM> GetSupplierImageList(long imageId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter ("imageId",imageId)
                };

                List<SupplierListVM> result = uow.ExecuteReaderSingleDS<SupplierListVM>("SupplierImagesList", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierListVM>();
            }

        }

        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceProducts(AdsParameterVM adsParameterVM)
        {
            List<GetMarkeetPlaceProducts> result = new List<GetMarkeetPlaceProducts>();

            try
            {
                SqlParameter[] isTestUserParameters =
                {
                  new SqlParameter("@customerId", adsParameterVM.CustomerId)
                };

                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByCustomerId", isTestUserParameters);

                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0].IsTestUser;
                else isTestUser = false;

                if (adsParameterVM.ProductCategoryIds?.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id", typeof(int));

                    adsParameterVM.ProductCategoryIds.ForEach(x => dt.Rows.Add(x));
                    SqlParameter[] sqlParameters = {
                    new SqlParameter ("customerId",adsParameterVM.CustomerId),
                    new SqlParameter("categoryIds", SqlDbType.Structured) { TypeName = "dbo.id_list", Value = dt },
                    new SqlParameter ("sortBy",adsParameterVM.SortBy+""),
                    new SqlParameter ("pageNumber",adsParameterVM.PageNumber),
                    new SqlParameter ("pageSize",adsParameterVM.PageSize),
                    new SqlParameter("@isTestUser",isTestUser)
                };

                    result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetAdsByCategoryIds", sqlParameters);
                }
                else if (adsParameterVM.SubCategoryId > 0)
                {
                    SqlParameter[] sqlParameters = {
                    new SqlParameter ("customerId",adsParameterVM.CustomerId),
                    new SqlParameter ("subCategoryId",adsParameterVM.SubCategoryId),
                    new SqlParameter ("sortBy",adsParameterVM.SortBy+""),
                    new SqlParameter ("pageNumber",adsParameterVM.PageNumber),
                    new SqlParameter ("pageSize",adsParameterVM.PageSize),
                    new SqlParameter("@isTestUser",isTestUser)
                };

                    result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetAdsBySubCategoryId", sqlParameters);
                }
                else if (adsParameterVM.SortBy == "LikedOnly")
                {
                    SqlParameter[] sqlParameters = {
                    new SqlParameter ("customerId",adsParameterVM.CustomerId),
                    new SqlParameter ("pageNumber",adsParameterVM.PageNumber),
                    new SqlParameter ("pageSize",adsParameterVM.PageSize),
                    new SqlParameter("@isTestUser",isTestUser)
                };

                    result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetCustomerLikedAds", sqlParameters);
                }
                else
                {
                    SqlParameter[] sqlParameters = {
                    new SqlParameter ("customerId",adsParameterVM.CustomerId),
                    new SqlParameter ("sortBy",adsParameterVM.SortBy+""),
                    new SqlParameter ("pageNumber",adsParameterVM.PageNumber),
                    new SqlParameter ("pageSize",adsParameterVM.PageSize),
                    new SqlParameter("@isTestUser",isTestUser)
                };

                    result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetMarkeetPlaceProducts", sqlParameters);

                    if (adsParameterVM.SortBy == "Saved")
                    {
                        result = result.Where(s => s.IsSaved == 1).ToList();
                    }
                    else if (adsParameterVM.SortBy == "Liked")
                    {
                        result = result.Where(s => s.IsLiked == 1).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return result;
        }

        // My Work !!!
        public void DeleteSelectedSupplierAdId(string selectedAdId, string deletedByUserId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@selectedAdId",selectedAdId),
                    new SqlParameter("@deletedByUserId",deletedByUserId)
                };
                uow.ExecuteNonQuery<Response>("Sp_DeleteSupplierAdId", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }
        public Response AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SalesmanId",SalesmanId),
                    new SqlParameter("@CustomerId",CustomerId)

                };
                var data = uow.ExecuteReaderSingleDS<Response>("Sp_AddSupplierRegisterBy", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Response AddCustomerFeedBack(string data)
        {
            Response res = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<CustomerFeedBack>(data);
                SqlParameter[] sqlParameters =
                {
                          new SqlParameter("@ProductId",entity.ProductId),
                          new SqlParameter("@CustomerId",entity.CustomerId),
                          new SqlParameter("@Description",entity.Description),
                          new SqlParameter("@Rating",entity.Rating)

              };
                var responses = uow.ExecuteReaderSingleDS<Response>("SP_AddUpdateCustomerFeedBack", sqlParameters);
                res.Message = "Information saved successfully.";
                res.Status = ResponseStatus.OK;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = ResponseStatus.Error;
                return res;
            }
        }
        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProducts(int pageSize, int pageNumber, long customerId)
        {
            List<GetMarkeetPlaceProducts> result = new List<GetMarkeetPlaceProducts>();
            try
            {
                SqlParameter[] isTestUserParameters =
               {
                  new SqlParameter("@customerId", customerId)
                };

                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByCustomerId", isTestUserParameters);

                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0].IsTestUser;
                else isTestUser = false;

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@pageSize",pageSize),
                    new SqlParameter("@pageNumber",pageNumber),
                    new SqlParameter("@isTestUser",isTestUser),
                    new SqlParameter("@customerId",customerId)
                };
                result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetMarkeetPlaceTopRatedProducts", sqlParameters).ToList();
                return result;


            }
            catch (Exception ex)
            {
                return result;
                //throw ex;
            }
        }

        public async Task<Response> GetPaymentDetail(long supplierId, long orderId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] Parameters =
                  {
                              new SqlParameter("@supplierId", supplierId),
                              new SqlParameter("@orderId", orderId)
                            };
                response.ResultData = await uow.ExecuteReaderSingleDSNew<PayementHistoryDetailDTO>("Sp_GetPaymentHistoryList", Parameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> GetPaymentHistory(OrderItemVM orderItemVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] Parameters =
                  {
                        new SqlParameter("@customerId", orderItemVM.CustomerId),
                      };
                response.ResultData = await uow.ExecuteReaderSingleDSNew<PayementHistoryDTO>("Sp_GetOrderPaymentHistory", Parameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize, long customerId)
        {
            try
            {
                SqlParameter[] isTestUserParameters =
               {
                  new SqlParameter("@customerId", customerId)
                };

                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByCustomerId", isTestUserParameters);

                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0].IsTestUser;
                else isTestUser = false;

                SqlParameter[] sqlParameters =
                {
                   // new SqlParameter("@pageSize",pageSize),
                    new SqlParameter("@isTestUser",isTestUser),
                };
                List<GetMarkeetPlaceProducts> result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("GetMarkeetPlaceTopRatedProductsforWeb", sqlParameters).ToList();
                return result;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            Response response = new Response();

            try
            {
                Supplier supplier = uow.Repository<Supplier>().GetAll().FirstOrDefault(x => x.UserId == userId);

                if (supplier != null)
                {
                    supplier.MobileNumber = phoneNumber;

                    uow.Repository<Supplier>().Update(supplier);
                    await uow.SaveAsync();

                    response.Message = $"Mobile number updated successfully";
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }

            return response;
        }
        public List<GetMarkeetPlaceProducts> GetMarketPlaceAds(AdsParameterVM adsParameterVM)
        {
            List<GetMarkeetPlaceProducts> result = new List<GetMarkeetPlaceProducts>();

            try
            {
                SqlParameter[] isTestUserParameters =
                {
                  new SqlParameter("@customerId", adsParameterVM.UserId)
                };
                var retuenVal = uow.ExecuteReaderSingleDS<IsTestUserVM>("GetUsertypeByCustomerId", isTestUserParameters);
                bool isTestUser;

                if (retuenVal != null)
                    isTestUser = retuenVal[0].IsTestUser;
                else isTestUser = false;

                SqlParameter[] filterBy =
                {
                  new SqlParameter("@catIds", adsParameterVM.CatIds),
                  new SqlParameter("@sortBy", adsParameterVM.SortBy),
                    new SqlParameter("@pageNumber", adsParameterVM.PageNumber),
                  new SqlParameter("@pageSize", adsParameterVM.PageSize),
                  new SqlParameter("@isTestUser", isTestUser),
                };

                result = uow.ExecuteReaderSingleDS<GetMarkeetPlaceProducts>("SP_GetFilteredMarketPlaceAds", filterBy).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return result;
        }
        public Response AddUpdateProductAttribute(ProductVM productVM)
        {
            Response response = new Response();
            ProductAttribute productAttribute = new ProductAttribute();
            try
            {
                var isExist = uow.Repository<ProductAttribute>().Get(x => x.Name.ToLower() == productVM.Name.ToLower()).FirstOrDefault();
                if (isExist == null && productVM.Action == "add")
                {
                    productAttribute.Name = productVM.Name;
                    productAttribute.CreatedBy = productVM.CreatedBy;
                    productAttribute.CreatedOn = DateTime.Now;
                    productAttribute.IsActive = productVM.Active;
                    uow.Repository<ProductAttribute>().Add(productAttribute);
                    uow.Save();
                }
                else if ((isExist != null && isExist.Id == productVM.Id && productVM.Action == "update") | (isExist == null && productVM.Action == "update"))
                {
                    productAttribute = uow.Repository<ProductAttribute>().Get(x => x.Id == Convert.ToInt64(productVM.Id)).FirstOrDefault();
                    productAttribute.Name = productVM.Name;
                    productAttribute.ModifiedBy = productVM.ModifiedBy;
                    productAttribute.ModifiedOn = DateTime.Now;
                    productAttribute.IsActive = productVM.Active;
                    uow.Repository<ProductAttribute>().Update(productAttribute);
                    uow.Save();
                }

                else if (isExist != null && productVM.Action == "add")
                {
                    response.Message = "Attribute already exist!";
                    response.Status = ResponseStatus.Error;
                    return response;
                }
                response.Message = "Data saved successfully!";
                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<Response> AddUpdateSupplierSlider(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<SupplierSliderDTO>(data);

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@id" , entity.Id),
                    new SqlParameter("@imageName" , entity.ImageName),
                    new SqlParameter("@imagePath" , entity.ImagePath),
                    new SqlParameter("@status" , entity.Status),
                    new SqlParameter("@startDate" , entity.StartDate),
                    new SqlParameter("@endDate" , entity.EndDate),
                    new SqlParameter("@createdBy" , entity.CreatedBy),
                };
                var result = await uow.ExecuteReaderSingleDSNew<SupplierSliderDTO>("Sp_AddUpdateSupplierSlider", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Updated Sucessfully";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Failed ..";
            }

            return response;
        }
        public Response AddUpdateProductCategoryAttribute(ProductVM productVM)
        {
            Response response = new Response();
            ProductCategoryAttribute productAttributes = new ProductCategoryAttribute();
            try
            {
                var isExist = uow.Repository<ProductCategoryAttribute>()
                     .Get(x => x.AttributeId == productVM.AttributeId && x.CategoryId == productVM.CategoryId
                     && x.SubCategoryId == productVM.SubCategoryId && x.CategoryGroupId == productVM.CategoryGroupId).FirstOrDefault();
                if (isExist == null && productVM.Action == "add")
                {
                    productAttributes.AttributeId = productVM.AttributeId;
                    productAttributes.CategoryId = productVM.CategoryId;
                    productAttributes.SubCategoryId = productVM.SubCategoryId;
                    productAttributes.CategoryGroupId = productVM.CategoryGroupId;
                    productAttributes.Active = productVM.Active;
                    productAttributes.CreatedBy = productVM.CreatedBy;
                    productAttributes.CreatedOn = DateTime.Now;
                    uow.Repository<ProductCategoryAttribute>().Add(productAttributes);
                    uow.Save();
                    response.Message = "Data saved successfully!";
                    response.Status = ResponseStatus.OK;
                }
                else if ((isExist != null && isExist.Id == productVM.Id && productVM.Action == "update") | (isExist == null && productVM.Action == "update"))
                {
                    productAttributes = uow.Repository<ProductCategoryAttribute>().GetById(productVM.Id);
                    productAttributes.AttributeId = productVM.AttributeId;
                    productAttributes.CategoryId = productVM.CategoryId;
                    productAttributes.SubCategoryId = productVM.SubCategoryId;
                    productAttributes.CategoryGroupId = productVM.CategoryGroupId;
                    productAttributes.Active = productVM.Active;
                    productAttributes.ModifiedBy = productVM.ModifiedBy;
                    productAttributes.ModifiedOn = DateTime.Now;
                    uow.Repository<ProductCategoryAttribute>().Update(productAttributes);
                    uow.Save();
                    response.Message = "Data saved successfully!";
                    response.Status = ResponseStatus.OK;
                }
                else if (isExist != null && productVM.Action == "add")
                {
                    response.Message = "Attribute already exist!";
                    response.Status = ResponseStatus.Error;
                    return response;
                }
                response.Message = "Data saved successfully!";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public Response GetProductAttributeList()
        {
            Response res = new Response();
            try
            {
                res.ResultData = uow.ExecuteCommand<ProductVM>("SP_GetProductAttributeList");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response GetSupplierSliderList()
        {
            Response response = new Response();
            try
            {
                response.ResultData = uow.ExecuteCommand<SupplierSliderDTO>("SP_GetSupplierSliderList");
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                throw ex;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public Response GetSupplierProductList(ProductVM productVM)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                        {
                            new SqlParameter("@categoryId" , productVM.CategoryId),
                            new SqlParameter("@subCategoryId" , productVM.SubCategoryId),
                            new SqlParameter("@categoryGroupId" , productVM.CategoryGroupId),
                            new SqlParameter("@supplierId",productVM.SupplierId),
                            new SqlParameter("@pageSize",productVM.PageSize),
                            new SqlParameter("@pageNumber",productVM.PageNumber),
                            new SqlParameter("@id",productVM?.Id ?? 0),
                            new SqlParameter("@name",productVM.Name),
                            new SqlParameter("@fromprice",productVM?.Price ?? 0),
                            new SqlParameter("@toprice",productVM?.ToPrice ?? 0)
                        };
                res.ResultData = uow.ExecuteReaderSingleDS<ProductVM>("SP_GetSupplierProductListAdmin", sqlParameters);
                if (res.ResultData != null)
                    res.Status = ResponseStatus.OK;
                else
                    res.Status = ResponseStatus.Restrected;
            }
            catch (Exception ex)
            {
                res.Status = ResponseStatus.Restrected;
            }
            return res;
        }
        public Response GetSupplierProductDetail(long productId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@productId" , productId)
                };
                res.ResultData = uow.ExecuteReaderSingleDS<ProductVM>("SP_GetSupplierProductDetailAdmin", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response GetProductCategoryAttributeList()
        {
            Response res = new Response();
            try
            {
                res.ResultData = uow.ExecuteCommand<ProductVM>("SP_GetProductCategoryAttributeList");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response GetProductAttributeListByCategoryId(long categoryId, string categoryLevel)
        {
            Response res = new Response();
            try
            {
                List<ProductCategoryAttribute> productAttributes = new List<ProductCategoryAttribute>();
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@categoryId" , categoryId),
                    new SqlParameter("@categoryLevel" , categoryLevel)
                };
                res.ResultData = uow.ExecuteReaderSingleDS<ProductVM>("SP_GetProductAttributeListByCategoryId", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response GetProductCategoryGroupListById(long subCategoryId)
        {
            Response res = new Response();
            try
            {
                res.ResultData = uow.Repository<ProductCategoryGroup>().Get(x => x.SubCategoryId == subCategoryId && x.Active == true).ToList();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> AddNewSupplierProduct(AddProductVM addProductVM)
        {
            try
            {
                Response res = new Response();
                ProductCatalog productCatalog = new ProductCatalog();
                productCatalog.Title = addProductVM.Title;
                productCatalog.Slug = addProductVM.Slug;
                productCatalog.IsActive = addProductVM.IsActive;
                productCatalog.SupplierId = addProductVM.SupplierId;
                productCatalog.Description = addProductVM.Description;
                productCatalog.YoutubeURL = addProductVM.YoutubeURL;
                productCatalog.CategoryId = addProductVM.CategoryId;
                productCatalog.Weight = addProductVM.Weight;
                productCatalog.SubCategoryId = addProductVM.SubCategoryId;
                productCatalog.CategoryGroupId = addProductVM.SubCategoryGroupId;
                productCatalog.CreatedOn = DateTime.Now;
                uow.Repository<ProductCatalog>().Add(productCatalog);
                uow.Save();
                long productId = productCatalog.ProductId;
                foreach (var sku in addProductVM.ProductSku)
                {
                    if (sku != null)
                    {
                        ProductInventory productInventory = new ProductInventory();
                        productInventory.ProductId = productId;
                        productInventory.Quantity = sku.Quantity;
                        productInventory.Price = sku.Price;
                        productInventory.DiscountInPercentage = sku.DiscountInPercentage > 0 ? sku.DiscountInPercentage : 0;
                        productInventory.DiscountedPrice = sku.DiscountInPercentage > 0 ? (sku.Price) - ((sku.Price / 100) * productInventory.DiscountInPercentage) : 0;
                        productInventory.Discount = sku.DiscountInPercentage > 0 ? sku.Price - ((sku.Price) - ((sku.Price / 100) * productInventory.DiscountInPercentage)) : 0;
                        productInventory.ProductVariantId = sku.ProductVariantId;
                        productInventory.Availability = sku.Availability;
                        await uow.Repository<ProductInventory>().AddAsync(productInventory);
                    }
                }
                if (addProductVM.BulkSku != null)
                {
                    foreach (var sku in addProductVM.BulkSku)
                    {
                        if (sku != null)
                        {
                            BulkOrdering bulkOrder = new BulkOrdering();
                            bulkOrder.SupplierId = addProductVM.SupplierId;
                            bulkOrder.ProductId = productId;
                            bulkOrder.MinQuantity = sku.minQuantity;
                            bulkOrder.MaxQuantity = sku.maxQuantity;
                            bulkOrder.BulkPrice = sku.bulkPrice;
                            bulkOrder.Discount = sku.bulkDiscount > 0 ? sku.bulkDiscount : 0;
                            if (sku.varientId != null)
                            {
                                bulkOrder.BulkVarientId = sku.varientId;
                            }
                            else
                            {
                                bulkOrder.BulkVarientId = sku.bulkVarientId;
                            }
                            await uow.Repository<BulkOrdering>().AddAsync(bulkOrder);
                        }
                    }
                }
                if (addProductVM.ProductAttributes != null)
                {
                    foreach (var attribute in addProductVM.ProductAttributes)
                    {
                        if (attribute != null)
                        {
                            ProductCatalogAttribute productCatalogAttribute = new ProductCatalogAttribute();
                            productCatalogAttribute.ProductId = productId;
                            productCatalogAttribute.AttributeValue = attribute.AttributeValue;
                            productCatalogAttribute.AttributeId = attribute.AttributeId;
                            await uow.Repository<ProductCatalogAttribute>().AddAsync(productCatalogAttribute);
                        }
                    }
                }
                if (addProductVM.searchTag != null)
                {
                    foreach (var tag in addProductVM.searchTag)
                    {
                        if (tag != null)
                        {
                            SupplierProductTags supplierProductTags = new SupplierProductTags();
                            supplierProductTags.TagName = tag.Display;
                            supplierProductTags.ProductId = productId;
                            uow.Repository<SupplierProductTags>().Add(supplierProductTags);
                        }
                    }
                }

                await uow.SaveAsync();
                res.Status = ResponseStatus.OK;
                res.ResultData = productId;
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> GetTopFiveProductCategory()
        {
            var response = new Response();
            try
            {
                var result = uow.ExecuteCommand<ProductCategoryDTO>("Sp_GetTopFiveProductCategory");
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetSupplierWithDetails(long supplierId)
        {
            var response = new Response();
            try
            {
                var supplier = await uow.Repository<Supplier>().Get(x => x.SupplierId == supplierId).FirstOrDefaultAsync();
                response.ResultData = supplier;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> AddAndUpdateSellerAccount(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<ProfileDTO>(data);
                entity.ModifiedOn = DateTime.Now;

                SqlParameter[] sqlParameters =
           {
                new SqlParameter("@SupplierId",entity.SupplierId ),
                new SqlParameter("@FirstName",entity.FirstName),
                new SqlParameter("@LastName",entity.LastName),
                new SqlParameter("@Email",entity.EmailAddress),
                new SqlParameter("@Mobile",entity.MobileNumber),
                new SqlParameter("@ShopName",entity.ShopName),
                new SqlParameter("@ShopUrl",entity.ShopUrl),
                new SqlParameter("@HolidayMode",entity.HolidayMode),
                new SqlParameter("@HolidayStart",entity.HolidayStart),
                new SqlParameter("@HoilidayEnd",entity.HoilidayEnd),
                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_UpdateSellerAccountProfileInformation", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Profile Updated Successfully !!!";



            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddAndUpdateBusinessAccount(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<ProfileDTO>(data);


                var SplitShopCoverImage = entity.ShopCoverImage?.Split(',');
                string ConvertShopCoverImage = entity.ShopCoverImage.Replace(SplitShopCoverImage[0] + ",", String.Empty);
                var ShopCoverImageBinaryformat = !string.IsNullOrEmpty(ConvertShopCoverImage) ? Convert.FromBase64String(ConvertShopCoverImage) : new byte[0];

                var splitName = entity.idFrontImage?.Split(',');
                string convert = entity.idFrontImage.Replace(splitName[0] + ",", String.Empty);
                var convertedImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];

                var splitName1 = entity.idBackSideImage?.Split(',');
                string convert1 = entity.idBackSideImage.Replace(splitName1[0] + ",", String.Empty);
                var convertedImage1 = !string.IsNullOrEmpty(convert1) ? Convert.FromBase64String(convert1) : new byte[0];

                entity.ModifiedOn = DateTime.Now;

                SqlParameter[] sqlParameters =
                     {
                        new SqlParameter("@SupplierId",entity.SupplierId ),
                        new SqlParameter("@CompanyName",entity.CompanyName),
                        new SqlParameter("@address",entity.BusinessAddress),
                        new SqlParameter("@ntnNumber",entity.Ntnnumber),
                        new SqlParameter("@personInCharge",entity.InChargePerson),
                        new SqlParameter("@registrationNo",entity.RegistrationNumber),
                        new SqlParameter("@businessDescription",entity.BusinessDescription),
                        new SqlParameter("@shopCoverImage",ShopCoverImageBinaryformat),
                        new SqlParameter("@IDFrontImage",convertedImage),
                        new SqlParameter("@CNIC",entity.Cnic),
                        new SqlParameter("@countryId",entity.CountryId),
                        new SqlParameter("@State",entity.StateId),
                         new SqlParameter("@City",entity.CityId),
                        new SqlParameter("@Area",entity.AreaId),
                        new SqlParameter("@Location",entity.Location),
                        new SqlParameter("@IDBackImage",convertedImage1),
                        new SqlParameter("@PersonInChargeMobileNo",entity.InchargePersonMobileNo),
                        new SqlParameter("@PersonInChargeEmail",entity.InchargePersonEmail),
                    };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_UpdateBusinessInformationProfile", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Profile Updated Successfully !!!";


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> UpdateSupplierProduct(AddProductVM addProductVM)
        {
            try
            {
                Response res = new Response();
                var findProductInventory = uow.Repository<ProductInventory>().Get(x => x.ProductId == addProductVM.Id);
                await uow.Repository<ProductInventory>().DeleteAllAsync(findProductInventory);

                var findBulkInventory = uow.Repository<BulkOrdering>().Get(x => x.ProductId == addProductVM.Id);
                await uow.Repository<BulkOrdering>().DeleteAllAsync(findBulkInventory);

                var findProductAttribute = uow.Repository<ProductCatalogAttribute>().Get(x => x.ProductId == addProductVM.Id);
                await uow.Repository<ProductCatalogAttribute>().DeleteAllAsync(findProductAttribute);

                var findProductTags = uow.Repository<SupplierProductTags>().Get(x => x.ProductId == addProductVM.Id);
                await uow.Repository<SupplierProductTags>().DeleteAllAsync(findProductTags);

                ProductCatalog productCatalog = new ProductCatalog();
                productCatalog = uow.Repository<ProductCatalog>().GetById(addProductVM.Id);
                productCatalog.Title = addProductVM.Title;
                productCatalog.Slug = addProductVM.Slug;
                productCatalog.IsActive = addProductVM.IsActive;
                productCatalog.Description = addProductVM.Description;
                productCatalog.YoutubeURL = addProductVM.YoutubeURL;
                productCatalog.CategoryId = addProductVM.CategoryId;
                productCatalog.SubCategoryId = addProductVM.SubCategoryId;
                productCatalog.CategoryGroupId = addProductVM.SubCategoryGroupId;
                productCatalog.Weight = addProductVM.Weight;
                productCatalog.ModifiedOn = DateTime.Now;
                uow.Repository<ProductCatalog>().Update(productCatalog);
                uow.Save();
                //long productId = productCatalog.ProductId;
                foreach (var sku in addProductVM.ProductSku)
                {
                    if (sku != null)
                    {
                        ProductInventory productInventory = new ProductInventory();
                        productInventory.ProductId = productCatalog.ProductId;
                        productInventory.Quantity = sku.Quantity;
                        productInventory.Price = sku.Price;
                        productInventory.DiscountInPercentage = sku.DiscountInPercentage > 0 ? sku.DiscountInPercentage : 0;
                        productInventory.DiscountedPrice = sku.DiscountInPercentage > 0 ? (sku.Price) - ((sku.Price / 100) * productInventory.DiscountInPercentage) : 0;
                        productInventory.Discount = sku.DiscountInPercentage > 0 ? sku.Price - ((sku.Price) - ((sku.Price / 100) * productInventory.DiscountInPercentage)) : 0;
                        productInventory.ProductVariantId = sku.ProductVariantId;
                        productInventory.Availability = sku.Availability;
                        await uow.Repository<ProductInventory>().AddAsync(productInventory);
                    }
                }
                foreach (var sku in addProductVM.BulkSku)
                {
                    if (sku != null)
                    {
                        BulkOrdering bulkOrder = new BulkOrdering();
                        bulkOrder.SupplierId = productCatalog.SupplierId;
                        bulkOrder.ProductId = productCatalog.ProductId;
                        bulkOrder.MinQuantity = sku.minQuantity;
                        bulkOrder.MaxQuantity = sku.maxQuantity;
                        bulkOrder.BulkPrice = sku.bulkPrice;
                        bulkOrder.BulkVarientId = sku.bulkVarientId;
                        bulkOrder.Discount = sku.bulkDiscount > 0 ? sku.bulkDiscount : 0;
                        await uow.Repository<BulkOrdering>().AddAsync(bulkOrder);
                    }
                }
                foreach (var attribute in addProductVM.ProductAttributes)
                {
                    if (attribute != null)
                    {
                        ProductCatalogAttribute productCatalogAttribute = new ProductCatalogAttribute();
                        productCatalogAttribute.ProductId = productCatalog.ProductId;
                        productCatalogAttribute.AttributeValue = attribute.AttributeValue;
                        productCatalogAttribute.AttributeId = attribute.AttributeId;
                        await uow.Repository<ProductCatalogAttribute>().AddAsync(productCatalogAttribute);
                    }
                }
                if (addProductVM.searchTag != null)
                {
                    foreach (var tag in addProductVM.searchTag)
                    {
                        if (tag != null)
                        {
                            SupplierProductTags supplierProductTags = new SupplierProductTags();
                            supplierProductTags.TagName = tag.Display;
                            supplierProductTags.ProductId = addProductVM.Id;
                            uow.Repository<SupplierProductTags>().Add(supplierProductTags);
                        }
                    }
                }
                await uow.SaveAsync();
                res.Status = ResponseStatus.OK;
                res.Message = "Data saved successfully!";
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> GetProductDetailWeb(long productId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@productId" , productId)
                };
                res.ResultData = uow.ExecuteReaderSingleDS<ProductVM>("SP_GetSupplierProductDetailWeb", sqlParameters);
            }
            catch (Exception ex)
            {
                res.Status = ResponseStatus.Error;
                throw ex;
            }
            return res;
        }

        public async Task<List<ProductVM>> GetProductDetailMob(long productId)
        {
            List<ProductVM> productVMs = new List<ProductVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                        new SqlParameter("@productId" , productId)
                    };
                productVMs = uow.ExecuteReaderSingleDS<ProductVM>("SP_GetSupplierProductDetailWeb", sqlParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productVMs;
        }
        public async Task<Response> GetHomeProductList(AddProductVM addProductVM)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                        {
                            new SqlParameter("@noOfTopCategory" , addProductVM.noOfTopCategory)
                        };
                var productList = uow.ExecuteReaderSingleDS<SupplierProductDTO>("SP_GetHomeProductList", sqlParameters);
                response.ResultData = productList;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> GetSupplierProductListWeb(ProductVM productVM)
        {
            var response = new Response();
            //if (!string.IsNullOrEmpty(productVM.ShopUrl))
            //{
            //    Supplier getSupplierByShopUrl = uow.Repository<Supplier>().Get(x => x.ShopUrl == productVM.ShopUrl).FirstOrDefault();
            //    productVM.SupplierId = getSupplierByShopUrl.SupplierId;
            //}
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@supplierId" , productVM.SupplierId),
                    new SqlParameter("@pageNumber",productVM.PageNumber),
                    new SqlParameter("@pageSize",productVM.PageSize),
                    new SqlParameter("@categoryId",productVM.CategoryId),
                };
                var productList = await uow.ExecuteReaderSingleDSNew<VendorProfileDTO>("SP_GetSupllierProductList", sqlParameters);
                List<IdValueDTO> productCategoriesBySupplierId = new List<IdValueDTO>();
                if (productVM.count == 1)
                {
                    SqlParameter[] sqlParameters1 = { new SqlParameter("@supplierId", productVM.SupplierId), };
                    productCategoriesBySupplierId = await uow.ExecuteReaderSingleDSNew<IdValueDTO>("SP_ProductCategoriesBySupplierId", sqlParameters1);
                }
                response.ResultData = new { ProductList = productList, CategoryList = productCategoriesBySupplierId };
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> GetCountryList()
        {
            Response response = new Response();
            List<CountryDTO> country = new List<CountryDTO>();
            try
            {
                country = await uow.Repository<Country>().GetAll().Where(x => x.Active == true).ProjectTo<CountryDTO>(_mapper.ConfigurationProvider).ToListAsync();
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Country";
                response.ResultData = country;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }
        public async Task<Response> GetSupplierShopUrl(string shopUrl)
        {
            Response response = new Response();
            try
            {
                SupplierShopDTO shop = uow.Repository<Supplier>().Get(x => x.ShopUrl == shopUrl).ProjectTo<SupplierShopDTO>(_mapper.ConfigurationProvider).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                response.ResultData = shop;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }
        public async Task<Response> GetSupplierShopDetails(string shopUrl)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
{
                    new SqlParameter("@shopUrl" , shopUrl),
                };
                var shopDetails = await uow.ExecuteReaderSingleDSNew<VendorProfileDTO>("SP_GetSupplierShopDetails", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = shopDetails != null ? shopDetails[0] : shopDetails;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }

        public async Task<Response> GetSateList(int countryId)
        {
            Response response = new Response();
            List<StateDTO> state = new List<StateDTO>();
            try
            {
                if (countryId == 0)
                {
                    state = await uow.Repository<State>().GetAll().ProjectTo<StateDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                else
                {
                    state = await uow.Repository<State>().GetAll().Where(x => x.CountryId == countryId).ProjectTo<StateDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Relevant Country States !!";
                response.ResultData = state;
                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }
        public async Task<Response> GetProductSearchTagsList(string inputText)
        {
            Response response = new Response();
            try
            {
                if (!String.IsNullOrWhiteSpace(inputText))
                {
                    SqlParameter[] sqlParameters =
                    {
                          new SqlParameter("@searchInput",inputText),
                    };
                    var result = await uow.ExecuteReaderSingleDSNew<ProductSearchTagVM>("SP_GetProductSearchTagsList", sqlParameters);
                    response.Status = ResponseStatus.OK;
                    response.Message = "Success";
                    response.ResultData = result;
                }
                else
                {
                    response.Message = "Input required";

                }
                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }
        public async Task<Response> GetAreaList(int? cityId)
        {
            Response response = new Response();
            List<AreaDTO> area = new List<AreaDTO>();
            try
            {
                if (cityId == 0)
                {
                    area = await uow.Repository<Area>().GetAll().Where(x => x.Active == true).ProjectTo<AreaDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                else
                {
                    area = await uow.Repository<Area>().GetAll().Where(x => x.CityId == cityId && x.Active == true).ProjectTo<AreaDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Relevant Areas of State !!";
                response.ResultData = area;
                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }

        public async Task<Response> GetLocationList(int areaId)
        {
            Response response = new Response();
            List<LocationDTO> location = new List<LocationDTO>();
            try
            {
                if (areaId == 0)
                {
                    location = await uow.Repository<Location>().GetAll().Where(x => x.Active == true).ProjectTo<LocationDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                else
                {
                    location = await uow.Repository<Location>().GetAll().Where(x => x.AreaId == areaId && x.Active == true).ProjectTo<LocationDTO>(_mapper.ConfigurationProvider).ToListAsync();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Relevant Locations of  Area !!";
                response.ResultData = location;

                return response;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }

        public async Task<Response> GetBanksList()
        {
            Response response = new Response();
            try
            {
                List<BankDTO> bank = new List<BankDTO>();
                bank = await uow.Repository<Banks>().GetAll().Where(x => x.Active == true).ProjectTo<BankDTO>(_mapper.ConfigurationProvider).ToListAsync();

                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Banks";
                response.ResultData = bank;

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = "Error";
                return response;
            }
        }

        public async Task<Response> SaveAndUpdateBankAccountData(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<BankDetailsDTO>(data);
                var splitName = entity.ChequeImageName?.Split(',');

                string convert = entity.ChequeImageName.Replace(splitName[0] + ",", String.Empty);
                var convertedImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                entity.ModifiedOn = DateTime.Now;
                entity.CreatedOn = DateTime.Now;
                SqlParameter[] sqlParameters =
                    {
                          new SqlParameter("@SupplierId",entity.SupplierId),
                           new SqlParameter("@Id", entity.Id),
                           new SqlParameter("@AccountTitle", entity.AccountTitle),
                           new SqlParameter("@AccountNumber", entity.AccountNumber),
                           new SqlParameter("@BankId", entity.BankId),
                           new SqlParameter("@BranchCode",entity.BranchCode),
                           new SqlParameter("@IBAN", entity.Iban),
                           new SqlParameter("@ChequeImage",convertedImage),
                           new SqlParameter("@ChequeImageName",entity.ChequeImageName),
                           new SqlParameter("@ModifiedBy", entity.ModifiedBy),
                           new SqlParameter("@CreatedBy", entity.CreatedBy)

                    };
                var result = uow.ExecuteReaderSingleDS<BankDetailsDTO>("Sp_AddUpdateBankDetails", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
                response.Message = "Profile Updated Successfully !!!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetBankAccountData(long supplierId)
        {
            var response = new Response();
            try
            {
                var BankAccountData = await uow.Repository<BankDetails>().Get(x => x.SupplierId == supplierId).FirstOrDefaultAsync();
                response.ResultData = BankAccountData;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> SaveAndUpdateWhareHouseAddress(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<WhareHouseAddressDTO>(data);

                SqlParameter[] sqlParameters =
           {
                new SqlParameter("@Id",entity.Id ),
                new SqlParameter("@SupplierId",entity.SupplierId ),
                new SqlParameter("@FirstAndLastName",entity.FirstAndLastName),
                new SqlParameter("@Address",entity.Address),
                new SqlParameter("@MobileNumber",entity.MobileNumber),
                new SqlParameter("@Email",entity.Email),
                new SqlParameter("@Country",entity.CountryId),
                 new SqlParameter("@State",entity.StateId),
                 new SqlParameter("@Area",entity.AreaId),
                new SqlParameter("@City",entity.CityId),
                 new SqlParameter("@Location",entity.Location),
                 new SqlParameter("@ModifiedBy",entity.ModifiedBy),
                 new SqlParameter("@CreatedBy",entity.CreatedBy),
                };
                var result = uow.ExecuteReaderSingleDS<WhareHouseAddressDTO>("Sp_AddUpdateWhereHouseAddress", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
                response.Message = "Profile Updated Successfully !!!";

            }
            catch (Exception ex)
            {
                //  Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetWareHouseAddress(long supplierId)
        {
            var response = new Response();
            try
            {
                var WareHouseData = await uow.Repository<WherehouseAddress>().Get(x => x.SupplierId == supplierId).FirstOrDefaultAsync();
                response.ResultData = WareHouseData;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> SaveAndUpdateReturnAddress(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<ReturnAddressDTO>(data);

                SqlParameter[] sqlParameters =
           {
                new SqlParameter("@Id",entity.Id ),
                new SqlParameter("@SupplierId",entity.SupplierId ),
                new SqlParameter("@IsWarehouseAddress",entity.IsWarehouseAddress),
                new SqlParameter("@FirstAndLastName",entity.Name),
                new SqlParameter("@Address",entity.Address),
                new SqlParameter("@MobileNumber",entity.MobileNumber),
                new SqlParameter("@Email",entity.Email),
                new SqlParameter("@Country",entity.CountryId),
                 new SqlParameter("@State",entity.StateId),
                   new SqlParameter("@City",entity.CityId),
                 new SqlParameter("@Area",entity.AreaId),
                 new SqlParameter("@Location",entity.Location),
                 new SqlParameter("@ModifiedBy",entity.ModifiedBy),
                 new SqlParameter("@CreatedBy",entity.CreatedBy),

                };
                var result = uow.ExecuteReaderSingleDS<ReturnAddressDTO>("Sp_AddUpdateReturnAddress", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
                response.Message = "Profile Updated Successfully !!!";
            }
            catch (Exception ex)
            {
                //  Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }
        public async Task<Response> SaveAndUpdateSocialLinks(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<SocialMediaLinksDTO>(data);

                SqlParameter[] sqlParameters =
                {
                new SqlParameter("@Id",entity.Id ),
                new SqlParameter("@SupplierId",entity.SupplierId ),
                new SqlParameter("@FacebookUrl",entity.FacebookUrl),
                new SqlParameter("@YoutubeUrl",entity.YoutubeUrl),
                new SqlParameter("@InstagramUrl",entity.InstagramUrl),
                new SqlParameter("@TwitterUrl",entity.TwitterUrl),
                new SqlParameter("@LinkedInUrl",entity.LinkedInUrl),
                 new SqlParameter("@ModifiedBy",entity.ModifiedBy),
                 new SqlParameter("@CreatedBy",entity.CreatedBy),
                };
                var result = uow.ExecuteReaderSingleDS<SocialMediaLinksDTO>("Sp_AddUpdateSocialMediaLinks", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
                response.Message = "Profile Updated Successfully !!!";
            }
            catch (Exception ex)
            {
                //  Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;

        }

        public async Task<Response> GetReturnAddress(long supplierId)
        {
            var response = new Response();
            try
            {
                var ReturnAddressData = await uow.Repository<ReturnAddress>().Get(x => x.SupplierId == supplierId).FirstOrDefaultAsync();
                response.ResultData = ReturnAddressData;
                if (response.ResultData != null)
                    response.Status = ResponseStatus.OK;
                else
                    response.Status = ResponseStatus.Restrected;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetSocialLinks(long supplierId, string isSupplierWeb)
        {
            var response = new Response();

            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SupplierId",supplierId )
                };
                if (isSupplierWeb == "SupplierWebApp")
                {
                    var result = await uow.Repository<SocialMediaLinks>().Get(x => x.SupplierId == supplierId).FirstOrDefaultAsync();
                    response.ResultData = result;
                }
                else
                {
                    var result = uow.ExecuteReaderSingleDS<SocialMediaLinks>("Sp_GetSocialLinksBySupplierId", sqlParameters).FirstOrDefault();
                    response.ResultData = result;
                }
                response.Status = ResponseStatus.OK;


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetProductCategories()
        {
            Response response = new Response();
            try
            {
                var result = await uow.Repository<ProductCategory>().Get(x => x.IsActive == true).OrderBy(y => y.Name)
                                .Select(p => new ProductCategoryIdValueDTO { Id = p.ProductCategoryId, Text = p.Name, HasSubItem = p.HasSubItems }).ToListAsync();
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetCategoryGroupsById(long subCategoryId)
        {
            Response response = new Response();
            try
            {
                var result = await uow.Repository<ProductCategoryGroup>().Get(x => x.SubCategoryId == subCategoryId && x.Active == true).OrderBy(y => y.Name)
                                .Select(p => new ProductCategoryIdValueDTO { Id = p.Id, Text = p.Name }).ToListAsync();
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetProductsByCategory(ProductVM productVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@pageSize" ,productVM.PageSize ),
                    new SqlParameter("@pageNumber" ,productVM.PageNumber ),
                    new SqlParameter("@categoryLevel" ,productVM.CategoryLevel ),
                    new SqlParameter("@categoryId" ,productVM.CategoryId)
                };
                response.ResultData = uow.ExecuteReaderSingleDS<SupplierProductDTO>("SP_GetProductsByCategory", sqlParameters);
                if (response.ResultData != null)
                    response.Status = ResponseStatus.OK;
                else
                    response.Status = ResponseStatus.Restrected;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.FirstOrDefault().ToString();
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetProductsByName(ProductVM productVM)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                        new SqlParameter("@pageSize" ,productVM.PageSize ),
                        new SqlParameter("@pageNumber" ,productVM.PageNumber ),
                        new SqlParameter("@productName" ,productVM.Name )
                    };
                response.ResultData = uow.ExecuteReaderSingleDS<SupplierProductDTO>("SP_GetProductsByName", sqlParameters);
                if (response.ResultData != null)
                    response.Status = ResponseStatus.OK;
                else
                    response.Status = ResponseStatus.Restrected;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.FirstOrDefault().ToString();
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetOrdersList(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<InventoryDTO>(data);
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SupplierId",entity.SupplierId )
                };
                var result = await uow.ExecuteReaderSingleDSNew<OrdersDTO>("Sp_GetOrderListBySuppliers", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
                response.Message = "Supplier OrdersList !";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetSalesSummary(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<InventoryDTO>(data);
                SqlParameter[] sqlParameters =
                {
                        new SqlParameter("@SupplierId",entity.SupplierId ),
                         new SqlParameter("@FromDate",entity.FromDate ),
                    new SqlParameter("@ToDate",entity.ToDate )
                    };
                var result = await uow.ExecuteReaderSingleDSNew<OrdersDTO>("Sp_GetSalesSummaryBySuppliers", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
                response.Message = "Profile Updated Successfully !!!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> PlaceOrder(PlaceOrderVM placeOrderVM)
        {
            var response = new Response();
            try
            {
                SupplierModels.Orders orders = new SupplierModels.Orders();
                orders.CustomerId = placeOrderVM.CustomerId;
                orders.CreatedOn = DateTime.Now;
                orders.TotalAmount = placeOrderVM.OrderTotal;
                orders.TotalShippingCost = placeOrderVM.TotalShippingCost;
                orders.OrderMessage = placeOrderVM.OrderMessage;
                uow.Repository<SupplierModels.Orders>().Add(orders);
                uow.Save();
                long orderId = orders.Id;
                foreach (var item in placeOrderVM.items)
                {
                    OrderItems orderItem = new OrderItems();
                    orderItem.OrderId = orderId;
                    orderItem.PromotionId = item.PromotionId;
                    orderItem.PromotionAmount = item.PromotionAmount;
                    orderItem.ProductId = item.ProductId;
                    orderItem.SupplierId = item.SupplierId;
                    orderItem.Quantity = item.Quantity;
                    orderItem.VariantId = item.VariantId;
                    orderItem.Price = item.Price;
                    orderItem.Discount = item.Discount;
                    orderItem.DiscountedAmount = item.DiscountedPrice;
                    orderItem.ShippingAmount = item.ShippingAmount;
                    orderItem.IsFreeShipping = item.IsFreeShipping;
                    await uow.Repository<OrderItems>().AddAsync(orderItem);
                    var findProductInventory = uow.Repository<ProductInventory>().Get(x => x.ProductId == item.ProductId && x.ProductVariantId == item.VariantId).FirstOrDefault();
                    if (findProductInventory != null)
                    {
                        findProductInventory.Quantity = findProductInventory.Quantity - item.Quantity;
                        uow.Repository<ProductInventory>().Update(findProductInventory);
                    }
                    await uow.SaveAsync();
                }
                var data = placeOrderVM.items.GroupBy(x => x.SupplierId).Select(y => y.First());

                foreach (var item in data)
                {
                    SupplierOrders supplierOrders = new SupplierOrders();
                    supplierOrders.OrderId = orderId;
                    supplierOrders.SupplierId = item.SupplierId.Value;
                    supplierOrders.OrderStatus = placeOrderVM.OrderStatus;
                    supplierOrders.TrackingId = "T" + Guid.NewGuid().ToString("N").Substring(0, 9);
                    await uow.Repository<SupplierOrders>().AddAsync(supplierOrders);
                    await uow.SaveAsync();
                }
                ShippingAddress shippingAddress = new ShippingAddress();
                shippingAddress.OrderId = orderId;
                shippingAddress.City = placeOrderVM.shippingDetails.City;
                shippingAddress.CustomerId = placeOrderVM.CustomerId.Value;
                shippingAddress.Email = placeOrderVM.shippingDetails.Email;
                shippingAddress.LastName = placeOrderVM.shippingDetails.LastName;
                shippingAddress.FirstName = placeOrderVM.shippingDetails.FirstName;
                shippingAddress.Address = placeOrderVM.shippingDetails.Address;
                shippingAddress.PhoneNumber = placeOrderVM.shippingDetails.PhoneNumber;
                await uow.Repository<ShippingAddress>().AddAsync(shippingAddress);
                await uow.SaveAsync();
                response.Status = ResponseStatus.OK;
                response.ResultData = orderId;
                response.Message = "Your Order has been placed successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetOrderDetailsById(long orderId, long supplierId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@OrderId",orderId),
                    new SqlParameter("@supplierId",supplierId),
                };
                var result = await uow.ExecuteReaderSingleDSNew<OrderItemDTO>("Sp_GetOrderDetailsById", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
                response.Message = "Order Details  !!!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetSupplierProductById(long productId)
        {
            var response = new Response();
            try
            {

                var result = await uow.Repository<ProductCatalog>().GetByIdAsync(productId);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetVariantsByProductId(long productId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@productId",productId)
                };
                var result = await uow.ExecuteReaderSingleDSNew<ProductVM>("SP_GetVariantsByProductId", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> UpdateOrderStatus(string data)
        {
            var response = new Response();
            try
            {
                long pickupId = 0;
                string res;
                var entity = JsonConvert.DeserializeObject<OrderUpdateDTO>(data);
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@OrderId",entity.OrderId ),
                    new SqlParameter("@OrderStatus",entity.OrderStatus ),
                    new SqlParameter("@SupplierId",entity.SupplierId )
                };

                await uow.ExecuteReaderSingleDSNew<OrdersDTO>("Sp_UpdateOrderStatus", sqlParameters);

                if (entity.OrderStatus == (int)EOrderStatus.PackedAndShipped)
                {
                    SqlParameter[] sqlParameters1 =
               {
                    new SqlParameter("@SupplierId",entity.SupplierId )
                };
                    pickupId = await uow.ExecuteScalar<long>("Sp_PickupAddressId", sqlParameters1);

                    if (pickupId == 0)
                    {
                        await _shippingApiService.AddPickupAddres(entity.SupplierId);
                    }

                    SqlParameter[] sqlParameters2 =
                    {
                    new SqlParameter("@OrderId",entity.OrderId),
                    new SqlParameter("@SupplierId",entity.SupplierId)
                };
                    var bookShipment = await uow.ExecuteReaderSingleDSNew<BookShipmentDTO>("Sp_GetBookShipment", sqlParameters2);
                    foreach (var item in bookShipment)
                    {
                        res = await _shippingApiService.BookShipment(item);
                        var tackingInfo = JsonConvert.DeserializeObject<BookShipmentResponseDTO>(res);
                        SqlParameter[] sqlParameters3 =
                    {
                    new SqlParameter("@OrderItemId",item.open_box),
                    new SqlParameter("@TrackingNumber",tackingInfo.tracking_number)
                };
                        uow.ExecuteNonQuery<BookShipmentDTO>("Sp_UpdateTrackingNumber", sqlParameters3);
                    }
                }

                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetOrderTracking(string data)
        {
            string responseData = string.Empty;
            Response response = new Response();
            var entity = JsonConvert.DeserializeObject<OrderTrackingDTO>(data);
            try
            {
                if (entity.UserRole == Utility.UserRoles.Supplier || entity.UserRole == Utility.UserRoles.Admin)
                {
                    responseData = await _shippingApiService.TrackOrderShipment(entity);
                    var trackingData = JsonConvert.DeserializeObject<SupplierModels.DTOs.OrderTracking.OrderShipmentTrackingDTO>(responseData);

                    if (trackingData.status == 0)
                    {
                        response.ResultData = trackingData.details;
                        response.Status = ResponseStatus.OK;
                        response.Message = trackingData.message;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }
                }
                else
                {
                    responseData = await _shippingApiService.TrackOrderShipment(entity);
                    var trackingData = JsonConvert.DeserializeObject<SupplierModels.DTOs.OrderTracking.OrderShipmentTrackingDTO>(responseData);
                        
                    if (trackingData.status == 0)
                    {
                        response.ResultData = trackingData.details;
                        response.Status = ResponseStatus.OK;
                        response.Message = trackingData.message;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }

                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                //response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetOrderedItemTracking(string data)
        {
            string responseData = string.Empty;
            Response response = new Response();
            var entity = JsonConvert.DeserializeObject<OrderItemTrackingDTO>(data);
            try
            {
                if (entity.UserRole == Utility.UserRoles.Supplier || entity.UserRole == Utility.UserRoles.Admin)
                {
                    responseData = await _shippingApiService.TrackOrderItemShipment(entity);
                    var trackingData = JsonConvert.DeserializeObject<OrderItemShipmentTrackingDTO>(responseData);

                    if (trackingData.status == 0)
                    {
                        response.ResultData = trackingData.details;
                        response.Status = ResponseStatus.OK;
                        response.Message = trackingData.message;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }
                }
                else
                {
                    responseData = await _shippingApiService.TrackOrderItemShipment(entity);
                    var trackingData = JsonConvert.DeserializeObject<OrderItemShipmentTrackingDTO>(responseData);

                    if (trackingData.status == 0)
                    {
                        response.ResultData = trackingData.details;
                        response.Status = ResponseStatus.OK;
                        response.Message = trackingData.message;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                    }

                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                //response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCustomerOrdersList(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<CustomerOrderItemsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@customerId",entity.CustomerId),
                    new SqlParameter("@pageNumber",entity.PageNumber),
                    new SqlParameter("@pageSize",entity.PageSize)
                };
                var customerOrderItems = uow.ExecuteReaderSingleDS<CustomerOrderItemsDTO>("Sp_GetCustomerOrderItemsList", sqlParameter);
                response.ResultData = customerOrderItems;
                response.Status = ResponseStatus.OK;
                response.Message = "Customer OrderItems List";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCustomerOrderedProductsList(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<CustomerOrderItemsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@orderId",entity.OrderId),
                    new SqlParameter("@pageNumber",entity.PageNumber),
                    new SqlParameter("@pageSize",entity.PageSize)
                };
                var customerOrderedProductsList = uow.ExecuteReaderSingleDS<CustomerOrderItemsDTO>("Sp_GetCustomerOrderedProductsList", sqlParameter);
                response.ResultData = customerOrderedProductsList;
                response.Status = ResponseStatus.OK;
                response.Message = "Customer OrderedProducts List";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetProfile(long supplierId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@supplierId",supplierId)
                };

                var result = await uow.ExecuteReaderSingleDSNew<SupplierProfileVM>("Sp_GetSupplierProfile", sqlParameters);

                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetSuppliersProductsListForApproval(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<ProductDetailsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                       new SqlParameter("@supplierId",entity.SupplierId),
                       new SqlParameter("@productId",entity.ProductId),
                       new SqlParameter("@mobileNumber",entity.MobileNumber),
                       new SqlParameter("@startDate",entity.StartDate),
                       new SqlParameter("@endDate",entity.EndDate),
                       new SqlParameter("@pageNumber",entity.PageNumber),
                       new SqlParameter("@pageSize",entity.PageSize),
                       new SqlParameter("@IsBlock",entity.IsBlock)
                };
                var supplierProductsList = uow.ExecuteReaderSingleDS<ProductDetailsDTO>("Sp_GetSuppliersProductsListForApproval", sqlParameter);
                response.ResultData = supplierProductsList;
                response.Status = ResponseStatus.OK;
                response.Message = "Supplier Products List Details ";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> ApproveSupplierProduct(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<ProductDetailsDTO>(data);
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@supplierId",entity.SupplierId),
                    new SqlParameter("@productId",entity.ProductId),
                    new SqlParameter("@modifiedBy",entity.ModifiedBy)
                };
                uow.ExecuteNonQuery<Response>("Sp_ApproveSupplierProduct", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Product Approved successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> UpdateSupplierAllGoodStatus(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<SupplierAllGoodStatus>(data);
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SupplierId",entity.SupplierId),
                    new SqlParameter("@Status",entity.status)
                };
                uow.ExecuteNonQuery<Response>("Sp_UpdateSupplierAllGoodStatus", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed successfully!";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetSupplierProductImagesbyProductId(long productId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@productId",productId )
                };
                var supplierProductsImages = uow.ExecuteReaderSingleDS<SupplierModels.DTOs.ProductImageDTO>("Sp_GetSupplierProductImagesbyProductId", sqlParameters);
                response.ResultData = supplierProductsImages;
                response.Status = ResponseStatus.OK;
                response.Message = "Supplier Products Images ";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }


        public async Task<Response> GetShippingCost(int id)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@cityId",id )
                };

                var ShippingCost = await uow.ExecuteReaderSingleDSNew<ShippingCost>("Sp_GetShippingCost", sqlParameters);
                response.ResultData = ShippingCost;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                //response.Message = _loggingService.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> GetShippingChargesList()
        {
            Response response = new Response();
            try
            {
                var ShippingChargeList = uow.ExecuteCommand<GetShippingCostVM>("Sp_GetShippingCostList");
                response.ResultData = ShippingChargeList;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddUpdateShippingCost(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<GetShippingCostVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.Id ),
                    new SqlParameter("@Cost",entity.Cost ),
                    new SqlParameter("@CityId",entity.CityId ),
                    new SqlParameter("@CreatedBy",entity.UserId ),
                    new SqlParameter("@ModifiedBy",entity.UserId )
                };

                var res = await uow.ExecuteReaderSingleDSNew<GetShippingCostVM>("SP_AddUpdateShippingCost", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> CancelCustomerOrder(string data)
        {
            var response = new Response();
            try
            {

                List<OrderItems> CustomerOderedItemsList = new List<OrderItems>();
                var entity = JsonConvert.DeserializeObject<OrdersDTO>(data);
                CustomerOderedItemsList = uow.Repository<OrderItems>().Get(x => x.OrderId == entity.OrderId).ToList();
                foreach (var item in CustomerOderedItemsList)
                {
                    var findProductInventory = uow.Repository<ProductInventory>().Get(x => x.ProductId == item.ProductId && x.ProductVariantId == item.VariantId).FirstOrDefault();
                    if (findProductInventory != null)
                    {
                        findProductInventory.Quantity = findProductInventory.Quantity + item.Quantity;
                        uow.Repository<ProductInventory>().Update(findProductInventory);
                    }
                    await uow.SaveAsync();
                }

                SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@orderId",entity.OrderId),
                        new SqlParameter("@orderStatus",entity.OrderStatus),
                        new SqlParameter("@reasonId",entity.ReasonId),
                        new SqlParameter("@cancellationPolicy",entity.CancellationPolicy),
                        new SqlParameter("@comments",entity.Comments),
                        new SqlParameter("@createdBy",entity.CreatedBy),
                        new SqlParameter("@userRole",entity.UserRole),

                };
                uow.ExecuteReaderSingleDS<Response>("Sp_CancelCustomerOrder", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Your order has been cancelled !";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCustomerCanclledOrdersList(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<CustomerOrderItemsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@customerId",entity.CustomerId),
                };
                var customerOrderItems = uow.ExecuteReaderSingleDS<CustomerOrderItemsDTO>("Sp_GetCustomerCancelledOrdersList", sqlParameter);
                response.ResultData = customerOrderItems;
                response.Status = ResponseStatus.OK;
                response.Message = "Customer Cancelled Orders List";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetOrderCancellationReasonsList(int userRole)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                  new SqlParameter("@userRole",userRole),
                };
                var CancellationReasonsList = uow.ExecuteReaderSingleDS<IdValueVM>("Sp_GetOrderCancellationReasonsList", sqlParameter).ToList();
                response.ResultData = CancellationReasonsList;
                response.Status = ResponseStatus.OK;
                response.Message = "Cancellation Reasone List";
            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }


        public async Task<Response> OrderCancelByAdmin(string data)
        {
            var response = new Response();
            List<OrderItems> CustomerOderedItemsList = new List<OrderItems>();
            var entity = JsonConvert.DeserializeObject<OrdersDTO>(data);
            CustomerOderedItemsList = uow.Repository<OrderItems>().Get(x => x.OrderId == entity.OrderId).ToList();
            foreach (var item in CustomerOderedItemsList)
            {
                var findProductInventory = uow.Repository<ProductInventory>().Get(x => x.ProductId == item.ProductId && x.ProductVariantId == item.VariantId).FirstOrDefault();
                if (findProductInventory != null)
                {
                    findProductInventory.Quantity = findProductInventory.Quantity + item.Quantity;
                    uow.Repository<ProductInventory>().Update(findProductInventory);
                }
                await uow.SaveAsync();
            }
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@orderId",entity.OrderId),
                    new SqlParameter("@orderStatus",entity.OrderStatus),
                    new SqlParameter("@reasonId",entity.ReasonId),
                    new SqlParameter("@cancellationPolicy",entity.CancellationPolicy),
                    new SqlParameter("@comments",entity.Comments),
                    new SqlParameter("@createdBy",entity.CreatedBy),
                    new SqlParameter("@userRole",entity.UserRole),

                };
                uow.ExecuteNonQuery<Response>("Sp_CancelCustomerOrder", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Order has been successfully Cancelled !";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCanellationReasonsListForAdmin()
        {
            Response response = new Response();
            try
            {
                var cancellationReasonsByUserRole = uow.ExecuteCommand<CancellationReasonDTO>("Sp_GetCanellationReasonsListForAdmin");
                response.ResultData = cancellationReasonsByUserRole;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }


        public async Task<Response> InsertAndUpdateCancellationReason(string data)
        {
            Response response = new Response();
            OrderCancellationReason orderCancellationReason = new OrderCancellationReason();
            var entity = JsonConvert.DeserializeObject<CancellationReasonDTO>(data);
            try
            {
                if (entity.Id <= 0 && entity.Action == "add")
                {
                    orderCancellationReason.ReasonName = entity.ReasonName;
                    orderCancellationReason.IsActive = entity.IsActive;
                    orderCancellationReason.CreatedOn = DateTime.Now;
                    orderCancellationReason.CreatedBy = entity.UserId;
                    orderCancellationReason.UserRole = entity.UserRole;
                    uow.Repository<OrderCancellationReason>().Add(orderCancellationReason);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Cancellation Reason Has Been Added!";
                }
                else if (entity.Id > 0 && entity.Action == "update")
                {
                    orderCancellationReason = uow.Repository<OrderCancellationReason>().Get(o => o.Id == entity.Id).FirstOrDefault();
                    orderCancellationReason.ReasonName = entity.ReasonName;
                    orderCancellationReason.IsActive = entity.IsActive;
                    orderCancellationReason.ModifiedOn = DateTime.Now;
                    orderCancellationReason.ModifiedBy = entity.UserId;
                    orderCancellationReason.UserRole = entity.UserRole;
                    uow.Repository<OrderCancellationReason>().Update(orderCancellationReason);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Cancellation Reason Has Been Updated!";
                }
                else if (entity.Id > 0 && entity.Action == "delete")
                {
                    orderCancellationReason = uow.Repository<OrderCancellationReason>().Get(o => o.Id == entity.Id).FirstOrDefault();
                    orderCancellationReason.IsActive = orderCancellationReason.IsActive == true ? false : true;
                    orderCancellationReason.ModifiedOn = DateTime.Now;
                    orderCancellationReason.ModifiedBy = entity.UserId;
                    uow.Repository<OrderCancellationReason>().Update(orderCancellationReason);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Cancellation Reason Has Been Deleted Successfully";
                }
                else
                {

                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        public async Task<Response> GetOrderByOrderId(long orderId)
        {
            Response response = new Response();
            try
            {
                var order = uow.Repository<HW.SupplierModels.Orders>().Get(x => x.Id == orderId);
                response.ResultData = order;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> AddSupplierLeadgerEntry(string data)
        {
            Response response = new Response();
            try
            {
                var orderId = JsonConvert.DeserializeObject<OrderIdDTO>(data).OrderId;
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@orderId",orderId)
                };
                uow.ExecuteNonQuery<Response>("Sp_SupplierLeadgerEntry", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Order has been successfully Cancelled !";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                return new Response();
            }
        }



        public async Task<Response> GetSupplierProfileDetails(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<Supplier>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@SupplierId",entity.SupplierId ),
                    new SqlParameter("@SupplierRole",entity.SupplierRole ),

                };

                var res = await uow.ExecuteReaderSingleDSNew<SupplierProfileDetailHistroryVM>("Sp_GetSuppliersProfileDetail", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> GetSupplierOrderBySupplierId(long supplierId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@SupplierId",supplierId ),
                };

                var res = await uow.ExecuteReaderSingleDSNew<SupplierOrderDetailsDTO>("Sp_GetSupplierOrderBySupplierId", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetCountryListForAdmin()
        {
            var response = new Response();


            try
            {

                var countrylist = uow.ExecuteCommand<CountryListVM>("Sp_GetCoutnryList");

                response.ResultData = countrylist;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> AddUpdateCountry(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<CountryListVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.CountryId ),
                    new SqlParameter("@CountryName",entity.CountryName ),
                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<CountryListVM>("SP_AddUpdateCountry", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> deleteCountryStatus(string data)
        {
            var response = new Response();
            var obj = new Country();
            var entity = JsonConvert.DeserializeObject<CountryListVM>(data);
            try
            {

                obj = uow.Repository<Country>().Get(o => o.CountryId == entity.CountryId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<Country>().Update(obj);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed Has Been  Successfully";



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<Response> GetStateListForAdmin()
        {
            var response = new Response();


            try
            {


                var statelist = uow.ExecuteCommand<StateListVM>("Sp_GetStateList");
                response.ResultData = statelist;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> AddUpdateState(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<StateListVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.StateId ),
                    new SqlParameter("@countryId",entity.CountryId ),
                      new SqlParameter("@stateName",entity.Name ),

                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<CountryListVM>("SP_AddUpdateState", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> deletestateStatus(string data)
        {
            var response = new Response();
            var obj = new State();
            var entity = JsonConvert.DeserializeObject<StateListVM>(data);
            try
            {

                obj = uow.Repository<State>().Get(o => o.StateId == entity.StateId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<State>().Update(obj);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed Has Been  Successfully";



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> GetBanksListForAdmin()
        {
            var response = new Response();


            try
            {


                var banklist = uow.ExecuteCommand<BanksListVM>("Sp_GetBankList");
                response.ResultData = banklist;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }


            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetTransactionHistory(TransactionsVM transactionsVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] Parameters =
                  {
                        new SqlParameter("@customerId", transactionsVM.CustomerId),
                        new SqlParameter("@pageNumber", transactionsVM.PageNumber),
                        new SqlParameter("@pageSize", transactionsVM.PageSize),
                        new SqlParameter("@sortBy", transactionsVM.SortBy),
                  };
                response.ResultData = await uow.ExecuteReaderSingleDSNew<PayementHistoryDTO>("Sp_GetCustomerOrderTransactions", Parameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> AddUpdatebank(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<BanksListVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.BankId ),

                      new SqlParameter("@bankName",entity.BankName ),

                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<BanksListVM>("SP_AddUpdateBank", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> deletebankStatus(string data)
        {
            var response = new Response();
            var obj = new Banks();
            var entity = JsonConvert.DeserializeObject<BanksListVM>(data);
            try
            {

                obj = uow.Repository<Banks>().Get(o => o.BankId == entity.BankId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<Banks>().Update(obj);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed Has Been  Successfully";



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> GetAreaListForAdmin()
        {
            var response = new Response();


            try
            {


                var arealist = uow.ExecuteCommand<AreaListVM>("Sp_GetAreaList");
                response.ResultData = arealist;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> deleteareaStatus(string data)
        {
            var response = new Response();
            var obj = new Area();
            var entity = JsonConvert.DeserializeObject<AreaListVM>(data);
            try
            {

                obj = uow.Repository<Area>().Get(o => o.AreaId == entity.AreaId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<Area>().Update(obj);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed Has Been  Successfully";



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> saveAndUpdateArea(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<AreaListVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.AreaId ),

                      new SqlParameter("@areaName",entity.AreaName ),
                             new SqlParameter("@stateId",entity.StateId ),
                           new SqlParameter("@cityId",entity.CityId ),
                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<AreaListVM>("SP_AddUpdateArea", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> GetLocationListForAdmin()
        {
            var response = new Response();


            try
            {


                var locationlist = uow.ExecuteCommand<LocationListVM>("Sp_GetLocationList");
                response.ResultData = locationlist;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> AddUpdateLocation(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<LocationListVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.LocationId ),

                      new SqlParameter("@locationName",entity.LocationName ),
                             new SqlParameter("@areaId",entity.AreaId ),
                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<LocationListVM>("SP_AddUpdateLocation", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> deletelocationStatus(string data)
        {
            var response = new Response();
            var obj = new Location();
            var entity = JsonConvert.DeserializeObject<LocationListVM>(data);
            try
            {

                obj = uow.Repository<Location>().Get(o => o.LocationId == entity.LocationId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<Location>().Update(obj);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "Status changed Has Been  Successfully";



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetLoggedSupplierCanelledOrdersList(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<CustomerOrderItemsDTO>(data);
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@supplierId",entity.SupplierId),
                };
                var customerOrderItems = await uow.ExecuteReaderSingleDSNew<CustomerOrderItemsDTO>("SupplierCancelledOrdersList", sqlParameter);
                response.ResultData = customerOrderItems;
                response.Status = ResponseStatus.OK;
                response.Message = "Supplier Cancelled Orders List";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<string> AddUpdateFreeShipping(string data)
        {
            try
            {
                var entity = JsonConvert.DeserializeObject<FreeShippingDTO>(data);
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@FreeShippingId",entity.Id),
                    new SqlParameter("@SupplierId",entity.SupplierId),
                    new SqlParameter("@CategoryId",entity.CategoryId),
                    new SqlParameter("@SubCategoryId",entity.SubCategoryId),
                    new SqlParameter("@CategoryGroupId",entity.CategoryGroupId),
                    new SqlParameter("@StarDate",entity.StarDate),
                    new SqlParameter("@EndDate",entity.EndDate),
                    new SqlParameter("@Active",entity.Active),
                };
                var customerOrderItems = await uow.ExecuteReaderSingleDSNew<FreeShippingDTO>("Sp_AddUpdateFreeShipping", sqlParameter);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return "Data Saved";
        }
        public async Task<Response> GetFreeShippingList(string data)
        {   
            Response response = new Response();
            try
            {
                var freeShipping = JsonConvert.DeserializeObject<FreeShippingDTO>(data);
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@categoryId", freeShipping.CategoryId),
                    new SqlParameter("@subCategoryId", freeShipping.SubCategoryId),
                    new SqlParameter("@categoryGroupId",freeShipping.CategoryGroupId),
                    new SqlParameter("@startDate",freeShipping.StarDate),
                    new SqlParameter("@endDate",freeShipping.EndDate),
                    new SqlParameter("@status", freeShipping.Active),
                    new SqlParameter("@supplierId", freeShipping.SupplierId)
                 };
                var result = await uow.ExecuteReaderSingleDSNew<GetFreeShippingListDTO>("Sp_GetFreeShippingList", sqlParameters);
                response.ResultData = result;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<Response> DeleteFreeShipping(long freeShippingId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                  {
                    new SqlParameter("@freeShippingId", freeShippingId)
                 };
                var result = await uow.ExecuteReaderSingleDSNew<Response>("sp_DelateFreeShipping", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }





        public async Task<Response> GetProductsList(string productName)
        {
            Response productsList = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@productName",productName),
                };
                var list = await uow.ExecuteReaderSingleDSNew<ProductNames>("SP_GetProductsName", sqlParameter);
                productsList.ResultData = list;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return productsList;
        }
        public async Task<Response> deleteLinkStatus(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<SupplierProfileDetailHistroryVM>(data);
            var obj = new SocialMediaLinks();
            try
            {

                obj = uow.Repository<SocialMediaLinks>().Get(o => o.SupplierId == entity.SupplierId).FirstOrDefault();
                if (obj != null)
                {
                    obj.IsActive = obj.IsActive == true ? false : true;
                    obj.ModifiedOn = DateTime.Now;
                    obj.ModifiedBy = entity.UserId;
                    uow.Repository<SocialMediaLinks>().Update(obj);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Status changed Has Been  Successfully";
                }
                else
                {
                    response.Message = "Data not available for update";
                    response.Status = ResponseStatus.OK;
                }



            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetProductsByTag(ProductVM productVM)
        {
            var response = new Response();

            try
            {
                SqlParameter[] sqlParameters =
   {
                        new SqlParameter("@pageSize" ,productVM.PageSize ),
                        new SqlParameter("@pageNumber" ,productVM.PageNumber ),
                        new SqlParameter("@tagname" ,productVM.Name )
                    };

                var res = await uow.ExecuteReaderSingleDSNew<SupplierProductDTO>("SP_GetProductsByTags", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetSuppliersLeadgerlist(OrderslistDTO orderslistDTO)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                        new SqlParameter("@CustomerId",orderslistDTO.CustomerId),
                        new SqlParameter("@OrderId",orderslistDTO.CustomerName),
                        new SqlParameter("@StartDate",orderslistDTO.StartDate),
                        new SqlParameter("@EndDate",orderslistDTO.EndDate),
                      };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<SupplierOderListVM>("Sp_SupplierLeadgerHistory", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetAllCatSubCatGroupCategories()
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                        
                      };

                response.ResultData = await uow.ExecuteReaderMultipleDS<CategorySubAndGroupCategoryDTO>("GetAllCatSubCatGroupCategories", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> BlockProduct(long productId, bool status)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                      {
                        new SqlParameter("@ProductId",productId),
                        new SqlParameter("@Status",status),
                      };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<Response>("Sp_BlockProduct",sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> SupplierList()
        {
            var response = new Response();


            try
            {


                var supplierList = uow.ExecuteCommand<SupplierListDTO>("Sp_GetSupplierList");
                response.ResultData = supplierList;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> AddUpdateSupplierCommission(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<SupplirCommissionVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@Id",entity.Id ),
                     new SqlParameter("@SupplierId",entity.SupplierId ),
                    new SqlParameter("@Commission",entity.SupplierCommision ),
                       new SqlParameter("@Isactive",entity.IsActive ),        
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<SuppliersCommision>("SP_SupplierCommission", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response> GetSupplierCommissionList(string data)
        {
            var response = new Response();
            var entity = JsonConvert.DeserializeObject<SupplirCommissionVM>(data);


            try
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@pageSize",entity.PageSize ),
                     new SqlParameter("@pageNumber",entity.PageNumber ),
                 


                };


                var supplierList = await uow.ExecuteReaderSingleDSNew<SupplirCommissionVM>("Sp_SupplierCommissionList", sqlParameters);
                response.ResultData = supplierList;
                response.Status = ResponseStatus.OK;
                response.Message = "Get Data Successfully!!";

            }

            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }
       
    }

}


