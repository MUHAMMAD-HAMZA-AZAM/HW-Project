using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.Services;
using HW.SupplierModels;
using HW.SupplierViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW.ReportsViewModels;
using HW.Utility;
using HW.GatewayApi.AuthO;
using HW.SupplierModels.DTOs;
using FeaturedSupplier = HW.SupplierViewModels.FeaturedSupplier;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminSupplierController : AdminBaseController
    {
        private readonly IAdminSupplierService adminSupplierService;
        public AdminSupplierController(IAdminSupplierService adnSupplierService_, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.adminSupplierService = adnSupplierService_;
        }

        [HttpPost]

        public async Task<List<SpSupplierListVM>> SpGetSupplierList([FromBody] GenericUserVM genericUserVM)
        {
            return await adminSupplierService.SpGetSupplierList(genericUserVM);
        }

        [HttpPost]

        public async Task<List<SpSupplierListVM>> SpGetHoomWorkSupplierList([FromBody] GenericUserVM genericUserVM)
        {
            return await adminSupplierService.SpGetHoomWorkSupplierList(genericUserVM);
        }
        [HttpPost]

        public async Task<List<SpSupplierListVM>> SpGetLocalSupplierList([FromBody] GenericUserVM genericUserVM)
        {
            return await adminSupplierService.SpGetLocalSupplierList(genericUserVM);
        }

        [HttpGet]
        public async Task<SpSupplierStatsVM> SpGetSupplierStats()
        {
            return await adminSupplierService.SpGetSupplierStats();
        }


        [HttpGet]
        public async Task<string> GetSuppliersOrderlistById(long id)
        {
            return await adminSupplierService.GetSuppliersOrderlistById(id);
        }
      
        [HttpPost]


        public async Task<string> GetSuppliersOrderlist([FromBody] OrderslistDTO orderslistDTO)
        {
            return await adminSupplierService.GetSuppliersOrderlist(orderslistDTO);
        }
        [HttpPost]
        public async Task<string> GetSuppliersLeadgerlist([FromBody] OrderslistDTO orderslistDTO)
        {
            return await adminSupplierService.GetSuppliersLeadgerlist(orderslistDTO);
        }

        [HttpGet]

        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await adminSupplierService.GetAllSuppliers();
        }
        [HttpGet]
        public async Task<Response> GetOrderStatusList()
        {
            return await adminSupplierService.GetOrderStatusList();
        }

        [HttpGet]

        public async Task<List<ProductCategoryGroupVM>> GetAllProduCatcategoryGroup()
        {
          return await adminSupplierService.GetAllProduCatcategoryGroup();
        }
        [HttpGet]

        public async Task<List<Supplier>> GetAllSuppliersYearlyReport()
        {
            return await adminSupplierService.GetAllSuppliersYearlyReport();
        }

        [HttpGet]

        public async Task<List<Supplier>> GetLAllTradesmanFromToReport([FromQuery] DateTime StartDate, DateTime EndDate)
        {
            return await adminSupplierService.GetAllSuppliersFromToReport(StartDate, EndDate);
        }

        [HttpGet]

        public async Task<List<HW.Utility.IdValueVM>> GetCategoriesForDropDown()
        {

            return await adminSupplierService.GetCategoriesForDropDown();

        }
        [HttpGet]

        public async Task<List<HW.Utility.IdValueCategoryVM>> GetSubCategoriesForDropDown(long categoryId)
        {

          return await adminSupplierService.GetSubCategoriesForDropDown(categoryId);

        }

        [HttpGet]
        public async Task<List<ProductCategoryVM>> GetCategoriesForListing(int productCategoryId)
        {
            return await adminSupplierService.GetCategoriesForListing(productCategoryId);
        }

        [HttpGet]
        public async Task<List<ProductCategoryVM>> GetCategoriesForAdminListing()
        {
            return await adminSupplierService.GetCategoriesForAdminListing();
        }

     

        [HttpGet]

        public async Task<Response> CheckProductAvailability(string productName)
        {
            return await adminSupplierService.CheckProductAvailability(productName);
        }
        [HttpGet]
        public async Task<List<SupplierDTO>> GetSupplierForReport(string StartDate, string EndDate, string skills , string supplier , string city , bool lastActive , string location, string mobile, string cnic , string  userType,string emailtype , string mobileType)
        //public async Task<List<SupplierDTO>> GetSupplierForReport([FromBody] ReportsAndUsersVM reportsAndUsersVM)
        {
            //return await adminSupplierService.GetSupplierForReport(reportsAndUsersVM);
            return await adminSupplierService.GetSupplierForReport(StartDate, EndDate, skills , supplier ,city ,lastActive , location, mobile, cnic , userType, emailtype, mobileType);

        }
        [HttpGet]

        public async Task<List<SupplierDTO>> GetSupplierWithDate24Hour(string startDate , string endDate)
        {
            return await adminSupplierService.GetSupplierWithDate24Hour(startDate , endDate);
        }
        [HttpGet]

        public async Task<List<SupplierAdsDTO>> GetPostedadsLastDay(string startDate, string endDate)
        {
            return await adminSupplierService.GetPostedadsLastDay(startDate, endDate);
        }

        [HttpGet]

        public async Task<List<SupplierAdsDTO>> GetPostedAdsForDynamicReport(int pageSize, int pageNumber, string dataOrderBy, string StartDate, string EndDate, string supplier, string city, bool lastActive, string location , string adId)
        {
            return await adminSupplierService.GetPostedAdsForDynamicReport(pageSize , pageNumber , dataOrderBy ,StartDate, EndDate, supplier, city, lastActive, location , adId);

        }

        [HttpGet]

        public async Task<List<Supplier>> GetSupplierAddressList()
        {
            return await adminSupplierService.GetSupplierAddressList();
        }

        [HttpGet]

        public async Task<List<SupplierAds>> GetSupplierAdsAddressList()
        {
            return await adminSupplierService.GetSupplierAdsAddressList();
        }

        [HttpGet]

        public async Task<Response> BlockSupplier(string supplierId , bool status)
        {
            return await adminSupplierService.BlockSupplier(supplierId, status);
        }

        [HttpPost]

        public async Task<List<SupplierDTO>> SupplierByCategory([FromBody] SupplierByCatVM supplierByCatVM)
        {
            return await adminSupplierService.SupplierByCategory(supplierByCatVM);
        }

        [HttpPost]
        public async Task<Response> AddNewProduct([FromBody] ProductCategoryVM productCategory)
        {
            return await adminSupplierService.AddNewProduct(productCategory);
        }

        [HttpPost]
        public async Task<Response> AddUpdateProductsCategoryGroup([FromBody] ProductCategoryGroup productCategoryGroup)
        {
          return await adminSupplierService.AddUpdateProductsCategoryGroup(productCategoryGroup);
        }

        [HttpGet]

        public async Task<List<SubProducts>> GetSubCategoriesForListing()
        {
            return await adminSupplierService.GetSubCategoriesForListing();
        }

        [HttpPost]

        public async Task<Response> AddNewSubProduct([FromBody] SubProducts subProducts)
        {
            return await adminSupplierService.AddNewSubProduct(subProducts);
        }

        [HttpPost]

        public async Task<Response> FeaturedSupplier([FromBody] HW.SupplierViewModels.FeaturedSupplier featuredSupplier)
        {
            return await adminSupplierService.FeaturedSupplier(featuredSupplier);
        }

        [HttpGet]

        public async Task<List<SupplierDTO>> GetFeaturedSupplierImages()
        {
            return await adminSupplierService.GetFeaturedSupplierImages();
        }

        [HttpGet]

        public async Task<SupplierProductAdDetails> GetSupplierAdDetails(long adId)
        {
            return await adminSupplierService.GetSupplierAdDetails(adId);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<SupplierProfileDetailVM> GetBusinessAndPersnalProfileAdmin(long userid)
        {
            return await adminSupplierService.GetBusinessAndPersnalProfileWeb(userid);
        }

        [HttpPost]
        [Produces("application/json")]

        public async Task<long> SaveAndUpdateAd([FromBody]PostAdVM postadd)
        {
            postadd.CreatedBy = "Admin";
            //var temp = GetEntityIdByUserId();
            return await adminSupplierService.SaveAndUpdateAd(postadd.SupplierId, postadd);
        }


        public void DeleteAdWithAdId(string AdIdforDelete, string deletedByUserId)
        {
            adminSupplierService.DeleteAdWithAdId(AdIdforDelete, deletedByUserId);
        }

        [HttpGet]
        public async Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            return await adminSupplierService.AddLinkedSalesman(SalesmanId, CustomerId);
        }

        [HttpPost]
        public async Task<string> GetSuppliersProductsListForApproval([FromBody] string data)
        {
            var a= await adminSupplierService.GetSuppliersProductsListForApproval(data);
            return a;
        }

        [HttpPost]
        public async Task<Response> ApproveSupplierProduct([FromBody] string data)
        {
            return await adminSupplierService.ApproveSupplierProduct(data);
        }

        [HttpPost]
        public async Task<Response> UpdateSupplierAllGoodStatus([FromBody] string data)
        {
            return await adminSupplierService.UpdateSupplierAllGoodStatus(data);
        }

        [HttpGet]
        public async Task<string> GetSupplierProductImagesbyProductId( long productId)
        {
            return await adminSupplierService.GetSupplierProductImagesbyProductId( productId);
        }

        [HttpGet]
        public async Task<string> GetShippingChargesList()
        {
            return await adminSupplierService.GetShippingChargesList();
        }

        [HttpPost]
        public async Task<Response> AddUpdateShippingCost([FromBody] string data)
        {
            return await adminSupplierService.AddUpdateShippingCost(data);
        }


        [HttpPost]
        public async Task<Response> OrderCancelByAdmin([FromBody] string data)
        {
            return await adminSupplierService.OrderCancelByAdmin(data);
        }

        [HttpGet]
        public async Task<Response> GetCanellationReasonsListForAdmin()
        {
            return await adminSupplierService.GetCanellationReasonsListForAdmin();
        }

        [HttpPost]
        public async Task<Response> InsertAndUpdateCancellationReason([FromBody] string data)
        {
            return await adminSupplierService.InsertAndUpdateCancellationReason(data);
        }

        [HttpPost]
        public async Task<string> GetSupplierProfileDetails([FromBody] string data)
        {
            return await adminSupplierService.GetSupplierProfileDetails(data);
        }
            [HttpGet]
            public async Task<Response> GetCountryListForAdmin()
            {
                return await adminSupplierService.GetCountryListForAdmin();
            }
        [HttpPost]
        public async Task<Response> AddUpdateCountry([FromBody] string data)
        {
            return await adminSupplierService.AddUpdateCountry(data);
        }
        [HttpPost]
        public async Task<Response> deleteCountryStatus([FromBody] string data)
        {
            return await adminSupplierService.deleteCountryStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetStateListForAdmin()
        {
            return await adminSupplierService.GetStateListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateState([FromBody] string data)
        {
            return await adminSupplierService.AddUpdateState(data);
        }
        [HttpPost]
        public async Task<Response> deletestateStatus([FromBody] string data)
        {
            return await adminSupplierService.deletestateStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetBanksListForAdmin()
        {
            return await adminSupplierService.GetBanksListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdatebank([FromBody] string data)
        {
            return await adminSupplierService.AddUpdatebank(data);
        }
        [HttpPost]
        public async Task<Response> deletebankStatus([FromBody] string data)
        {
            return await adminSupplierService.deletebankStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetAreaListForAdmin()
        {
            return await adminSupplierService.GetAreaListForAdmin();
        }
        [HttpPost]
        public async Task<Response> deleteareaStatus([FromBody] string data)
        {
            return await adminSupplierService.deleteareaStatus(data);
        }
        [HttpPost]
        public async Task<Response> saveAndUpdateArea([FromBody] string data)
        {
            return await adminSupplierService.saveAndUpdateArea(data);
        }
        [HttpGet]
        public async Task<Response> GetLocationListForAdmin()
        {
            return await adminSupplierService.GetLocationListForAdmin();
        }
        [HttpPost]
        public async Task<Response> AddUpdateLocation([FromBody] string data)
        {
            return await adminSupplierService.AddUpdateLocation(data);
        }
        [HttpPost]
        public async Task<Response> deletelocationStatus([FromBody] string data)
        {
            return await adminSupplierService.deletelocationStatus(data);
        }
        [HttpPost]
        public async Task<Response> deleteLinkStatus([FromBody] string data)
        {
            return await adminSupplierService.deleteLinkStatus(data);
        }
        [HttpGet]
        public async Task<Response> BlockProduct(long productId, bool status)
        {
            return await adminSupplierService.BlockProduct(productId, status);

        }
        [HttpGet]
        public async Task<Response> SupplierList()
        {
            return await adminSupplierService.SupplierList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateSupplierCommission([FromBody] string data)
        {
            return await adminSupplierService.AddUpdateSupplierCommission(data);
        }
        [HttpPost]
        public async Task<Response> GetSupplierCommissionList([FromBody] string data)
        {
            return await adminSupplierService.GetSupplierCommissionList(data);
        }
     
    }
}
