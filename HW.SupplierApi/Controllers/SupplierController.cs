using HW.SupplierApi.Services;
using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalDetailsVM = HW.SupplierViewModels.PersonalDetailsVM;
using HW.ReportsViewModels;
using HW.PackagesAndPaymentsModels;
using HW.SupplierModels.DTOs;
using HW.UserWebViewModels;
using FeaturedSupplier = HW.SupplierViewModels.FeaturedSupplier;

namespace HW.SupplierApi.Controllers
{
    [Produces("application/json")]

    public class SupplierController : BaseController
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        public string Start()
        {
            return "Suppliers api has been started";
        }

        [HttpGet]
        public List<ProductCategory> GetAllProductCatagories()
        {
            return supplierService.GetAllProductCatagories();
        }

        [HttpGet]
        public Supplier GetSupplierBySupplierId(long supplierId)
        {
            return supplierService.GetSupplierBySupplierId(supplierId);
        }

        [HttpGet]
        public SupplierAds GetSupplierAdByAdId(long supplierAdId)
        {
            return supplierService.GetSupplierAdByAdId(supplierAdId);
        }

        [HttpGet]
        public IQueryable<ProductSubCategory> GetSubCategoriesByProductCategoryId(long productCategoryId)
        {
            return supplierService.GetSubCategoriesByProductCategoryId(productCategoryId);
        }

        [HttpPost]
        public IQueryable<SupplierAds> GetSupplierAdsByProductSubCategoryIds([FromBody] List<long> productSubCategoryId)
        {
            return supplierService.GetSupplierAdsByProductSubCategoryIds(productSubCategoryId);
        }

        [HttpPost]
        public async Task<Response> GetSuppliersOrderlist([FromBody] OrderslistDTO orderslistDTO)
        {
            return await supplierService.GetSuppliersOrderlist(orderslistDTO);
        }
        [HttpPost]
        public async Task<Response> GetSuppliersLeadgerlist([FromBody] OrderslistDTO orderslistDTO)
        {
            return await supplierService.GetSuppliersLeadgerlist(orderslistDTO);
        }

        [HttpGet]
        public IQueryable<SupplierAds> GetSupplierAdsByProductCategoryId(long productCategoryId)
        {

            return supplierService.GetSupplierAdsByProductCategoryId(productCategoryId);

        }


        [HttpPost]
        public List<Supplier> GetSuppliersByIds([FromBody] List<long> supplierIds)
        {
            return supplierService.GetSuppliersByIds(supplierIds);
        }


        [HttpGet]
        public async Task<Supplier> GetSupplierById(long supplierId)
        {
            return await supplierService.GetSupplierById(supplierId);
        }
        [HttpGet]
        public async Task<Response> GetSuppliersOrderlistById(long id)
        {
            return await supplierService.GetSuppliersOrderlistById(id);
        }
    
        [HttpGet]
        public async Task<Response> GetOrderDetailById(long orderId)
        {
          return await supplierService.GetOrderDetailById(orderId);
        }

        [HttpPost]
        public async Task<Response> UpdateStockLevel([FromBody] string data)
        {
          return await supplierService.UpdateStockLevel(data);
        }

        [HttpGet]
        public IQueryable<SupplierAds> GetSupplierAdsBySupplierId(long supplierId)
        {
            return supplierService.GetSupplierAdsBySupplierId(supplierId);
        }

        [HttpGet]
        public IQueryable<SupplierAds> GetSupplierAdsByStatusId(int statusId)
        {
            return supplierService.GetSupplierAdsByStatusId(statusId);
        }

        [HttpPost]
        public async Task<Response> AddEditSupplier([FromBody] Supplier data)
        {
            Response response = new Response();
            if (ModelState.IsValid)
                response = await supplierService.AddEditSupplier(data);
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Model is not valid.";
            }

            return response;
        }

        [HttpGet]
        public long GetEntityIdByUserId(string userId)
        {
            return supplierService.GetEntityIdByUserId(userId);
        }

        [HttpGet]
        public List<ProductCategory> GetAllProductCategory()
        {
            return supplierService.GetAllProductCategory();
        }
        [HttpGet]
        public Response GetProfileVerification(long? supplierId)
        {
            return supplierService.GetProfileVerification(supplierId);
        }

        [HttpGet]
        public List<ProductSubCategory> GetAllProductSubCatagories(long productId)
        {
            return supplierService.GetAllProductSubCatagories(productId);
        }

        [HttpPost]
        public bool AddRegistrationDetail([FromBody] Supplier supplier)
        {
            return supplierService.AddRegistrationDetail(supplier);
        }

        [HttpGet]
        public Supplier GetAllSupplier(long supplierId)
        {
            return supplierService.GetAllSupplier(supplierId);
        }

        [HttpGet]
        public List<Supplier> GetAllSuppliers()
        {
            return supplierService.GetAllSuppliers();
        }

        [HttpGet]
        public List<SupplierAds> GetAllSupplierAds(long supplierId)
        {
            return supplierService.GetAllSupplierAds(supplierId);
        }
        
        [HttpGet]
        public List<ProductCategoryGroupVM> GetAllProduCatcategoryGroup()
        {
          return supplierService.GetAllProduCatcategoryGroup();
        }
        [HttpGet]
        public ProductCategory productCategory(long productId)
        {
            return supplierService.productCategory(productId);
        }
        [HttpGet]
        public string GetCategoryName(long productId)
        {
            return supplierService.GetCategoryName(productId);
        }

        [HttpPost]
        public long SaveAndUpdateAd([FromBody] SupplierAds model)
        {
            return supplierService.SaveAndUpdateAd(model);
        }

        [HttpGet]
        public SupplierAds GetEditAdDetail(long supplierAdsId)
        {
            return supplierService.GetEditAdDetail(supplierAdsId);
        }

        [HttpGet]
        public async Task<List<ProductSubCategory>> AllSubCategory()
        {
            return supplierService.AllSubCategory();
        }

        [HttpGet]
        public long GetProductCategoryIdBySupplierID(long supplierId)
        {
            return supplierService.GetProductCategoryIdBySupplierId(supplierId);
        }

        [HttpGet]
        public Task<bool> UpdateAdcStatus(long supplierAdsId, long supplieradsStatusId, int days)
        {
            return supplierService.UpdateAdcStatus(supplierAdsId, supplieradsStatusId, days);
        }

        [HttpPost]
        public Response UpdateAd([FromBody] SupplierAds supplierAds)
        {
            return supplierService.UpdateAd(supplierAds);
        }

        [HttpGet]
        public Supplier GetSupplierByUserId(string userId)
        {
            return supplierService.GetSupplierByUserId(userId);
        }

        public async Task<Response> SetSupplierSubCategories([FromBody] List<SupplierSubCategory> subCategories)
        {
            return await supplierService.SetSupplierSubCategories(subCategories);
        }

      
     
        [HttpGet]
        public async Task<Response> GetOrderStatusList()
        {
            return await supplierService.GetOrderStatusList();
        }
        public Supplier GetSupplierTradeNmae(long supplierId)
        {
            return supplierService.GetSupplierTradeName(supplierId);
        }

        [HttpGet]
        public async Task<List<ProductSubCategoryDTO>> GetProductSubCategoryById(long productCatgoryId)
        {
            return await supplierService.GetProductSubCategoryById(productCatgoryId);

        }

        [HttpPost]
        public bool UpdatePersonalDetail([FromBody] PersonalDetailsVM model)
        {
            return supplierService.UpdatePersonalDetail(model);
        }

        [HttpGet]
        public void DeleteAd(long supplierAdId)
        {
            supplierService.DeleteAd(supplierAdId);
        }

        public void UpdateSupplierAdViewCount(long supplierAdId)
        {
            supplierService.UpdateSupplierAdViewCount(supplierAdId);
        }


        [HttpGet]
        public List<SupplierSubCategory> GetSelectedSupplierSubCategory(long supplierId)
        {
            return supplierService.GetSelectedSupplierSubCategory(supplierId);
        }

        [HttpPost]
        public List<ProductSubCategory> getProductSubCategoriesById([FromBody] List<long> supplierSubCategoryId)
        {
            return supplierService.getProductSubCategoriesById(supplierSubCategoryId);
        }

        [HttpGet]
        public List<ManageAdsVM> SpGetActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            return supplierService.SpGetActiveAds(supplierId, pageNumber, pageSize);
        }

        [HttpGet]
        public List<InactiveManageAdsVM> SpGetInActiveAds(long supplierId, int pageNumber, int pageSize)
        {
            return supplierService.SpGetInActiveAds(supplierId, pageNumber, pageSize);
        }

        [HttpGet]
        public List<ManageAdsVMWithImage> SpGetActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            return supplierService.SpGetActiveAdsWithImages(supplierId, pageNumber, pageSize);
        }

        [HttpGet]
        public List<InactiveManageAdsVMWithImages> SpGetInActiveAdsWithImages(long supplierId, int pageNumber, int pageSize)
        {
            return supplierService.SpGetInActiveAdsWithImages(supplierId, pageNumber, pageSize);
        }
        [HttpGet]
        public List<MarketSimilarProductsVM> MarketSimilarProductsVMs(long categoryId)
        {
            return supplierService.MarketSimilarProductsVMs(categoryId);
        }
        [HttpGet]
        public SupplierAds GetSupplierAdsById(long supplierId)
        {
            return supplierService.GetSupplierAdsById(supplierId);
        }
        [HttpGet]
        public List<Supplier> AllSupplier()
        {
            return supplierService.AllSupplier();
        }
        [HttpGet]
        public List<SupplierAds> GetAllAds()
        {
            return supplierService.GetAllAds();
        }
        [HttpGet]
        public List<ProductSubCategory> GetAllSubcategory(string search)
        {
            return supplierService.GetAllSubcategory(search);
        }

        [HttpPost]
        public List<SpSupplierListVM> SpGetSupplierList([FromBody] GenericUserVM genericUserVMn)
        {
            return supplierService.SpGetSupplierList(genericUserVMn);
        }
        [HttpPost]
        public List<SpSupplierListVM> SpGetHoomWorkSupplierList([FromBody] GenericUserVM genericUserVMn)
        {
            return supplierService.SpGetHoomWorkSupplierList(genericUserVMn);
        }
        [HttpPost]
        public List<SpSupplierListVM> SpGetLocalSupplierList([FromBody] GenericUserVM genericUserVMn)
        {
            return supplierService.SpGetLocalSupplierList(genericUserVMn);
        }

        [HttpGet]
        public SpSupplierStatsVM SpGetSupplierStats()
        {
            return supplierService.SpGetSupplierStats();
        }
        [HttpGet]
        public List<Supplier> GetAllSuppliersFromToReport(DateTime StartDate, DateTime EndDate)
        {
            return supplierService.GetAllSuppliersFromToReport(StartDate, EndDate);
        }
        [HttpGet]
        public List<Supplier> GetAllSuppliersYearlyReport()
        {
            return supplierService.GetAllSuppliersYearlyReport();
        }
        [HttpGet]
        public List<IdValueVM> GetCategoriesForDropDown()
        {
            return supplierService.GetCategoriesForDropDown();
        }
        [HttpGet]
        public List<IdValueCategoryVM> GetSubCategoriesForDropDown(long categoryId)
        {

          return supplierService.GetSubCategoriesForDropDown(categoryId);
        }
        [HttpGet] 
        public List<ProductCategoryVM> GetCategoriesForListing(int productCategoryId)
        {
            return supplierService.GetCategoriesForListing(productCategoryId);
        }

        [HttpGet]
        public List<ProductCategoryVM> GetCategoriesForAdminListing()
        {
            return supplierService.GetCategoriesForAdminListing();
        }
        [HttpGet]
        public List<IdValueDTO> GetCategoriesNameWithId()
        {
            return supplierService.GetCategoriesNameWithId();
        }
        // Api Level Micro S
        [HttpGet]
        public ImageVM GetCategoryImageById(long categoryImageId)
        {
            return supplierService.GetCategoryImageById(categoryImageId);
        }
        [HttpGet]
        public Response CheckProductAvailability(string productName)
        {
            return supplierService.CheckProductAvailability(productName);
        }

        public List<SupplierDTO> GetSupplierForReport(DateTime? StartDate, DateTime? EndDate, string categories, string supplier, string city, bool lastActive, string location, string mobile, string cnic, string userType, string emailtype, string mobileType)
        //public List<SupplierDTO> GetSupplierForReport(ReportsAndUsersVM reportsAndUsersVM)
        {
            //return supplierService.GetSupplierForReport(reportsAndUsersVM);
            return supplierService.GetSupplierForReport(StartDate, EndDate, categories, supplier, city, lastActive, location, mobile, cnic, userType, emailtype, mobileType);
        }

        public List<SupplierAdsDTO> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, DateTime? StartDate, DateTime? EndDate, string supplier, string city, bool lastActive, string location, string adId)
        {
            return supplierService.GetPostedAdsForDynamicReport(pageSize, pageNumber, dataOrderBy, StartDate, EndDate, supplier, city, lastActive, location, adId);
        }

        [HttpPost]
        public List<Supplier> GetSupplierReport([FromBody] List<string> userId)
        {
            return supplierService.GetSupplierReport(userId);
        }

        [HttpGet]
        public bool UpdateSupplierPublicId(long supplierId, string publicId)
        {
            return supplierService.UpdateSupplierPublicId(supplierId, publicId);
        }

        [HttpGet]
        public List<SupplierDTO> GetSupplierLast24HourRegistred(System.DateTime StartDate, System.DateTime EndDate)
        {
            return supplierService.GetSupplierLast24HourRegistred(StartDate, EndDate);
        }

        [HttpGet]
        public List<SupplierAdsDTO> GetPostedadsLastDay(System.DateTime StartDate, System.DateTime EndDate)
        {
            return supplierService.GetPostedadsLastDay(StartDate, EndDate);
        }

        [HttpGet]
        public List<Supplier> GetSupplierAddressList()
        {
            return supplierService.GetSupplierAddressList();
        }

        [HttpGet]
        public List<SupplierAds> GetSupplierAdsAddressList()
        {
            return supplierService.GetSupplierAdsAddressList();
        }

        [HttpGet]
        public Response BlockSupplier(string supplierId, bool status)
        {
            return supplierService.BlockSupplier(supplierId, status);
        }

        [HttpPost]
        public Response AddUpdateNewVariant([FromBody]ProductVariant productVariant)
        {
          return supplierService.AddUpdateNewVariant(productVariant);
        }
        [HttpPost]
        public List<SupplierDTO> SupplierByCategory([FromBody] SupplierByCatVM supplierByCatVM)
        {
            return supplierService.SupplierByCategory(supplierByCatVM);
        }

        [HttpPost]
        public Response AddNewProducts([FromBody] ProductCategoryVM productCategory)
        {
            return supplierService.AddNewProducts(productCategory);
        }
        [HttpPost]
        public Response AddNewSubProducts([FromBody] SubProducts subProducts)
        {
             return supplierService.AddNewSubProducts(subProducts);
        }
        [HttpPost]
        public Response AddUpdateProductsCategoryGroup([FromBody] ProductCategoryGroup productCategoryGroup)
        {
          return supplierService.AddUpdateProductsCategoryGroup(productCategoryGroup);
        }
        [HttpGet]
        public List<SubProducts> GetAllSubProducts()
        {
            return supplierService.GetAllSubProducts();
        }
        [HttpGet]
        public List<ProductVariant> GetAllProductVariant()
        {
          return supplierService.GetAllProductVariant();
        }
        [HttpGet]
        public List<SupplierListVM> GetSupplierList()
        {
            return supplierService.GetSupplierList();
        }
        [HttpGet]
        public List<SupplierListVM> GetSupplierImageList(long imageId)
        {
            return supplierService.GetSupplierImageList(imageId);
        }
        [HttpGet]
        public Response GetBusinessDetailsStatus(string id)
        {
            return supplierService.GetBusinessDetailsStatus(id);
        }
        [HttpPost]
        public Response FeaturedSupplier([FromBody] HW.SupplierViewModels.FeaturedSupplier featuredSupplier)
        {
            return supplierService.FeaturedSupplier(featuredSupplier);
        }

        [HttpGet]
        public async Task<Response> RelistAd(long SupplierAdsId)
        {
            return await supplierService.RelistAd(SupplierAdsId);
        }

        [HttpPost]
        public  List<CustomerFeedBack> GetCustomerFeedBackList([FromBody]string data)
        {
          return supplierService.GetCustomerFeedBackList(data);
        }

        [HttpPost]
        public async Task<Response> GetPaymentHistory([FromBody]OrderItemVM orderItemVM)
        {
          return await supplierService.GetPaymentHistory(orderItemVM);
        }
        [HttpGet]
        public async Task<Response> GetPaymentDetail(long supplierId,long orderId)
        {
          return await supplierService.GetPaymentDetail(supplierId,orderId);
        }
        [HttpGet]
        public SupplierProductAdDetails GetSupplierAdDetails(long adId)
        {
            return supplierService.GetSupplierAdDetails(adId);
        }

        [HttpPost]
        public Response AddCustomerFeedBack([FromBody]string data)
        {
          return supplierService.AddCustomerFeedBack(data);
        }

        [HttpPost]
        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceProducts([FromBody] AdsParameterVM adsParameterVM)
        {
            return supplierService.GetMarkeetPlaceProducts(adsParameterVM);
        }
        [HttpGet]
        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProducts(int pageSize, int pageNumber, long customerId)
        {
            return supplierService.GetMarkeetPlaceTopRatedProducts(pageSize, pageNumber, customerId);
        }
        [HttpGet]
        public List<GetMarkeetPlaceProducts> GetMarkeetPlaceTopRatedProductsforWeb(int pageSize, long customerId)
        {
            return supplierService.GetMarkeetPlaceTopRatedProductsforWeb(pageSize, customerId);
        }
        [HttpGet]
        public void DeleteSelectedSupplierAdId(string selectedAdId, string deletedByUserId)
        {
            supplierService.DeleteSelectedSupplierAdId(selectedAdId, deletedByUserId);
        }
        [HttpGet]
        public Response AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            return supplierService.AddLinkedSalesman(SalesmanId, CustomerId);
        }        

        [HttpGet]
        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            return await supplierService.UpdatePhoneNumberByUserId(userId, phoneNumber);
        }
        [HttpPost]
        public List<GetMarkeetPlaceProducts> GetMarketPlaceAds([FromBody] AdsParameterVM adsParameterVM)
        {
            return supplierService.GetMarketPlaceAds(adsParameterVM);
        }        
        [HttpPost]
        public Response AddUpdateProductAttribute([FromBody] ProductVM productVM)
        {
            return supplierService.AddUpdateProductAttribute(productVM);
        }   
        [HttpPost]
        public async Task<Response> AddUpdateSupplierSlider([FromBody] string data)
        {
            return await supplierService.AddUpdateSupplierSlider(data);
        }        
        [HttpPost]
        public Response AddUpdateProductCategoryAttribute([FromBody] ProductVM productVM)
        {
            return supplierService.AddUpdateProductCategoryAttribute(productVM);
        }
        [HttpGet]
        public Response GetProductAttributeList()
        {
            return supplierService.GetProductAttributeList();
        }
        [HttpGet]
        public Response GetSupplierSliderList()
        {
            return supplierService.GetSupplierSliderList();
        }          
        [HttpPost]
        public Response GetSupplierProductList([FromBody]ProductVM productVM)
        {
            return supplierService.GetSupplierProductList(productVM);
        }            
        [HttpGet]
        public Response GetSupplierProductDetail(long productId)
        {
            return supplierService.GetSupplierProductDetail(productId);
        }        
        [HttpGet]
        public async Task<Response> GetProductDetailWeb(long productId)
        {
            return await supplierService.GetProductDetailWeb(productId);
        }
        [HttpGet]
        public async Task<List<ProductVM>> GetProductDetailMob(long productId)
        {
          return await supplierService.GetProductDetailMob(productId);
        }
        [HttpGet]
        public Response GetProductCategoryAttributeList()
        {
            return supplierService.GetProductCategoryAttributeList();
        }        
        [HttpGet]
        public Response GetProductAttributeListByCategoryId(long categoryId ,string categoryLevel)
        {
            return supplierService.GetProductAttributeListByCategoryId(categoryId, categoryLevel);
        }        
        [HttpGet]
        public Response GetProductCategoryGroupListById(long subCategoryId)
        {
            return supplierService.GetProductCategoryGroupListById(subCategoryId);
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

        [HttpGet]
        public async Task<Response> GetSupplierWithDetails(long supplierId)
        {
            return await supplierService.GetSupplierWithDetails(supplierId);
        }
    
        [HttpGet]
        public async Task<Response> GetTopFiveProductCategory()
        {
          return await supplierService.GetTopFiveProductCategory();
        }
    public async Task<Response> AddAndUpdateSellerAccount([FromBody] string data)
        {
            return await supplierService.AddAndUpdateSellerAccount(data);
        }
        

        public async Task<Response> AddAndUpdateBusinessAccount([FromBody] string data)
        {
            return await supplierService.AddAndUpdateBusinessAccount(data);
        }

        [HttpPost]
        public async Task<Response> GetHomeProductList([FromBody] AddProductVM addProductVM)
        {
            return await supplierService.GetHomeProductList(addProductVM);
        }
        [HttpPost]
        public async Task<Response> GetSupplierProductListWeb([FromBody] ProductVM productVM)
        {
          return await supplierService.GetSupplierProductListWeb(productVM);
        }
        
        [HttpGet]
        public async Task<Response> GetCountryList()
        {
            return await supplierService.GetCountryList();
        }        
        [HttpGet]
        public async Task<Response> GetSupplierShopUrl(string shopUrl)
        {
            return await supplierService.GetSupplierShopUrl(shopUrl);
        }        
        [HttpGet]
        public async Task<Response> GetSupplierShopDetails(string shopUrl)
        {
            return await supplierService.GetSupplierShopDetails(shopUrl);
        }
        [HttpGet]
        public async Task<Response> GetSateList(int countryId)
        {
            return await supplierService.GetSateList(countryId);
        }        
        [HttpGet]
        public async Task<Response> GetProductSearchTagsList(string inputText)
        {
            return await supplierService.GetProductSearchTagsList(inputText);
        }
        [HttpPost]
        public async Task<Response> GetProductsByCategory([FromBody] ProductVM productVM)
        {
            return await supplierService.GetProductsByCategory(productVM);
        }
        [HttpPost]
        public async Task<Response> GetProductsByName([FromBody] ProductVM productVM)
        {
          return await supplierService.GetProductsByName(productVM);
        }
        [HttpGet]
        public async Task<Response> GetAreaList(int? cityId)
        {
            return await supplierService.GetAreaList(cityId);
        }

        [HttpGet]
        public async Task<Response> GetLocationList(int areaId)
        {
            return await supplierService.GetLocationList(areaId);
        }

        [HttpGet]
        public async Task<Response> GetBanksList()
        {
            return await supplierService.GetBanksList();
        }

        [HttpPost]
        public async Task<Response> SaveAndUpdateBankAccountData([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateBankAccountData(data);
        }

        [HttpGet]
        public async Task<Response> GetBankAccountData(long supplierId)
        {
            return await supplierService.GetBankAccountData(supplierId);
        }

        [HttpPost]
        public async Task<Response> SaveAndUpdateWhareHouseAddress([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateWhareHouseAddress(data);
        }

        [HttpGet]
        public async Task<Response> GetWareHouseAddress(long supplierId)
        {
            return await supplierService.GetWareHouseAddress(supplierId);
        }

        [HttpPost]
        public async Task<Response> SaveAndUpdateReturnAddress([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateReturnAddress(data);
        }
        [HttpPost]
        public async Task<Response> SaveAndUpdateSocialLinks([FromBody] string data)
        {
            return await supplierService.SaveAndUpdateSocialLinks(data);
        }

        [HttpGet]
        public async Task<Response> GetReturnAddress(long supplierId)
        {
            return await supplierService.GetReturnAddress(supplierId);
        }
        [HttpGet]
        public async Task<Response> GetSocialLinks(long supplierId,string isSupplierWeb)
        {
            return await supplierService.GetSocialLinks(supplierId, isSupplierWeb);
        }
        [HttpGet]
        public async Task<Response> GetProductCategories()
        {
            return await supplierService.GetProductCategories();
        }
        [HttpGet]
        public async Task<Response> GetCategoryGroupsById(long subCategoryId)
        {
            return await supplierService.GetCategoryGroupsById(subCategoryId);
        }

        [HttpPost]
        public async Task<Response> GetOrdersList([FromBody] string data)
        {
            return await supplierService.GetOrdersList(data);
        }
        [HttpPost]
        public async Task<Response> GetSalesSummary([FromBody] string data)
        {
          return await supplierService.GetSalesSummary(data);
        }
        [HttpPost]
        public async Task<Response> PlaceOrder([FromBody] PlaceOrderVM orderItemVM)
        {
            return await supplierService.PlaceOrder(orderItemVM);
        }
        [HttpGet]
        public async Task<Response> GetOrderDetailsById(long orderId,long supplierId)
        {
            return await supplierService.GetOrderDetailsById(orderId,supplierId);
        }       
        [HttpGet]
        public async Task<Response> GetSupplierProductById(long productId)
        {
            return await supplierService.GetSupplierProductById(productId);
        }        
        [HttpGet]
        public async Task<Response> GetVariantsByProductId(long productId)
        {
            return await supplierService.GetVariantsByProductId(productId);
        }

        [HttpPost]
        public async Task<Response> UpdateOrderStatus([FromBody] string data)
        {
            return await supplierService.UpdateOrderStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetProfile(long supplierId)
        {
            return await supplierService.GetProfile(supplierId);
        }

        [HttpPost]
        public async Task<Response> GetCustomerOrdersList([FromBody] string data)
        {
            return await supplierService.GetCustomerOrdersList(data);
        }

        [HttpPost]
        public async Task<Response> GetCustomerOrderedProductsList([FromBody] string data)
        {
            return await supplierService.GetCustomerOrderedProductsList(data);
        }

        [HttpPost]
        public async Task<Response> GetSuppliersProductsListForApproval([FromBody] string data)
        {
            return await supplierService.GetSuppliersProductsListForApproval(data);
        }

        [HttpPost]
        public async Task<Response> ApproveSupplierProduct([FromBody] string data)
        {
            return await supplierService.ApproveSupplierProduct(data);
        }
        [HttpPost]
        public async Task<Response> UpdateSupplierAllGoodStatus([FromBody] string data)
        {
            return await supplierService.UpdateSupplierAllGoodStatus(data);
        }

        [HttpGet]
        public async Task<Response> GetSupplierProductImagesbyProductId(long productId)
        {
            return await supplierService.GetSupplierProductImagesbyProductId(productId);
        }
    

        [HttpGet]
        public async Task<Response> GetShippingCost(int id)
        {
            return await supplierService.GetShippingCost(id);
        }

        [HttpGet]
        public async Task<Response> GetShippingChargesList()
        {
            return await supplierService.GetShippingChargesList();
        }

        [HttpPost]
        public async Task<Response> AddUpdateShippingCost([FromBody]string data)
        {
            return await supplierService.AddUpdateShippingCost(data);
        }

        [HttpPost]
        public async Task<Response> CancelCustomerOrder([FromBody] string data)
        {
            return await supplierService.CancelCustomerOrder(data);
        }

        [HttpPost]
        public async Task<Response> GetCustomerCanclledOrdersList([FromBody] string data)
        {
            return await supplierService.GetCustomerCanclledOrdersList(data);
        }

        [HttpGet]
        public async Task<Response> GetOrderCancellationReasonsList(int userRole)
        {
            return await supplierService.GetOrderCancellationReasonsList(userRole);
        }

        [HttpPost]
        public async Task<Response> OrderCancelByAdmin([FromBody] string data)
        {
            return await supplierService.OrderCancelByAdmin(data);
        }

        [HttpPost]
        public async Task<Response> InsertAndUpdateCancellationReason([FromBody] string data)
        {
            return await supplierService.InsertAndUpdateCancellationReason(data);
        }

        [HttpGet]
        public async Task<Response> GetCanellationReasonsListForAdmin()
        {
            return await supplierService.GetCanellationReasonsListForAdmin();
        }
        [HttpGet]
        public async Task<Response> GetOrderByOrderId(long orderId)
        {
            return await supplierService.GetOrderByOrderId(orderId);
        }
        [HttpPost]
        public async Task<Response> AddSupplierLeadgerEntry([FromBody] string data)
        {
            return await supplierService.AddSupplierLeadgerEntry(data);
        }

        

        [HttpPost]
        public async Task<Response> GetSupplierProfileDetails([FromBody] string data)
        {
            return await supplierService.GetSupplierProfileDetails(data);
        }
        [HttpGet]
        public async Task<Response> GetCountryListForAdmin()
        {
            return await supplierService.GetCountryListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateCountry([FromBody] string data)
        {
            return await supplierService.AddUpdateCountry(data);
        }
        [HttpPost]
        public async Task<Response> deleteCountryStatus([FromBody] string data)
        {
            return await supplierService.deleteCountryStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetStateListForAdmin()
        {
            return await supplierService.GetStateListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateState([FromBody] string data)
        {
            return await supplierService.AddUpdateState(data);
        }
        [HttpPost]
        public async Task<Response> deletestateStatus([FromBody] string data)
        {
            return await supplierService.deletestateStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetBanksListForAdmin()
        {
            return await supplierService.GetBanksListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdatebank([FromBody] string data)
        {
            return await supplierService.AddUpdatebank(data);
        }
        [HttpPost]
        public async Task<Response> deletebankStatus([FromBody] string data)
        {
            return await supplierService.deletebankStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetAreaListForAdmin()
        {
            return await supplierService.GetAreaListForAdmin();
        }
        [HttpPost]
        public async Task<Response> deleteareaStatus([FromBody] string data)
        {
            return await supplierService.deleteareaStatus(data);
        }
        [HttpPost]
        public async Task<Response> saveAndUpdateArea([FromBody] string data)
        {
            return await supplierService.saveAndUpdateArea(data);
        }

        [HttpGet]
        public async Task<Response> GetLocationListForAdmin()
        {
            return await supplierService.GetLocationListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateLocation([FromBody] string data)
        {
            return await supplierService.AddUpdateLocation(data);
        }
        [HttpPost]
        public async Task<Response> deletelocationStatus([FromBody] string data)
        {
            return await supplierService.deletelocationStatus(data);
        }

        
        [HttpPost]
        public async Task<Response> GetTransactionHistory([FromBody] TransactionsVM transactionsVM)
        {
            return await supplierService.GetTransactionHistory(transactionsVM);
        }
        [HttpPost]
        public async Task<Response> GetLoggedSupplierCanelledOrdersList([FromBody] string data)
        {
            return await supplierService.GetLoggedSupplierCanelledOrdersList(data);
        }
      

        [HttpPost]
        public async Task<Response> GetOrderTracking([FromBody] string data)
        {
            return await supplierService.GetOrderTracking(data);
        }

        [HttpPost]
        public async Task<Response> GetOrderedItemTracking([FromBody] string data)
        {
            return await supplierService.GetOrderedItemTracking(data);
        }

        [HttpGet]
        public Task<Response> GetProductsList(string productName)
        {
            return supplierService.GetProductsList(productName);
        }
        [HttpGet]
        public async Task<Response> GetSupplierOrderBySupplierId(long supplierId)
        {
            return await supplierService.GetSupplierOrderBySupplierId(supplierId);

        }

        [HttpPost]
        public async Task<Response> deleteLinkStatus([FromBody] string data)
        {
            return await supplierService.deleteLinkStatus(data);
        }
        [HttpPost]
        public async Task<Response> GetProductsByTag([FromBody] ProductVM productVM)
        {
            return await supplierService.GetProductsByTag(productVM);
        }
        [HttpGet]
        public Task<Response> GetAllCatSubCatGroupCategories()
        {
            return supplierService.GetAllCatSubCatGroupCategories();
        }
        [HttpGet]
        public async Task<Response> BlockProduct (long ProductId, bool status)
        {
            return await supplierService.BlockProduct(ProductId, status);
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
        [HttpGet]
        public async Task<Response> SupplierList()
        {
            return await supplierService.SupplierList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateSupplierCommission([FromBody] string data)
        {
            return await supplierService.AddUpdateSupplierCommission(data);
        }
        [HttpPost]
        public async Task<Response> GetSupplierCommissionList([FromBody] string data)
        {
            return await supplierService.GetSupplierCommissionList(data);
        }
    
    }
} 
