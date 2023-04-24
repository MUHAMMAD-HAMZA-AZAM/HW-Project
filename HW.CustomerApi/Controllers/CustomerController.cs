using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.CustomerApi.Services;
using HW.CustomerModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using HW.UserViewModels;
using HW.ReportsViewModels;
using HW.SupplierViewModels;
using Response = HW.Utility.Response;

namespace HW.CustomerApi.Controllers
{
    [Produces("application/json")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IBus bus;
        public CustomerController(ICustomerService customerService /*IBus bus*/)
        {
            this.customerService = customerService;
            //this.bus = bus;
        }
        [HttpGet]
        public async Task<string> Start()
        {
            //await bus.Publish(new LoggingEvent { LogLevel = (int)LogLevel.Information, Message = "Customer service is started." });
            return "Customer service is started.";
        }
        [HttpGet]
        //  [Authorize(Roles = "Trademan")]
        public async Task<List<Customer>> GetAll()
        {
            var result = await customerService.GetAllCustomers();
            return result.ToList();
        }
        [HttpGet]
        public async Task<Customer> GetCustomerById(long customerId)
        {
            return await customerService.GetCustomerById(customerId);
        }

        [HttpPost]
        public async Task<Response> Add([FromBody] Customer data)
        {
            Response response = new Response();
            if (ModelState.IsValid)
                response = await customerService.AddEditCustomer(data);
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Model is not valid.";
            }

            return response;
        }
        [HttpDelete]
        public async Task<Response> Delete(long id)
        {
            return await customerService.DeleteCustomer(id);
        }

        [HttpPost]
        public async Task<List<Customer>> GetCustomerByIdList([FromBody] List<long> customersIds)
        {
            return await customerService.GetCustomerByIdList(customersIds);
        }

        [HttpGet]
        public List<CustomerFavorites> GetCustomerFavoriteByCustomerId(long customerId)
        {
            return customerService.GetCustomerFavoriteByCustomerId(customerId);
        }

        [HttpPost]
        public Response SetTradesmanToCustomerFavorite([FromBody] CustomerFavorites customerFavorites)
        {
            return customerService.SetTradesmanToCustomerFavorite(customerFavorites);
        }

        [HttpGet]
        public Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId, long customerId, string userId)
        {
            return customerService.AddDeleteCustomerSavedAd(isSaved, supplierAdId, customerId, userId);
        }
        [HttpGet]
        public Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId, long customerId, string userId)
        {
            return customerService.AddDeleteCustomerLikedAd(isLiked, supplierAdId, customerId, userId);
        }
        [HttpGet]
        public Task<Response> AddCustomerProductRating(int rating, long supplierAdId, long customerId, string userId)
        {
            return customerService.AddCustomerProductRating(rating, supplierAdId, customerId, userId);
        }
        [HttpGet]
        public Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId, long customerId, string userId)
        {
            return customerService.AddDeleteCustomerSavedTradesman(isLiked, tradesmanId, customerId, userId);
        }

        [HttpGet]
        public Task<Response> AddAdViews(long supplierAdId, long customerId, string userId)
        {
            return customerService.AddAdViews(supplierAdId, customerId, userId);
        }
        [HttpPost]
        public IQueryable<CustomerSavedAds> GetCustomerSavedAds([FromBody] SavedAdsVM savedAdsVM)
        {
            return customerService.GetCustomerSavedAds(savedAdsVM);
        }

        [HttpPost]
        public async Task<Response> AddEditCustomer([FromBody] Customer model)
        {
            return await customerService.AddEditCustomer(model);
        }

        [HttpGet]
        public long GetEntityIdByUserId(string userId)
        {
            return customerService.GetEntityIdByUserId(userId);
        }

        [HttpGet]
        public Customer GetCustomerByUserId(string userId)
        {
            return customerService.GetCustomerByUserId(userId);
        }

        [HttpGet]
        public bool GetTradesmanIsFavorite(long customerId, long tradesmanId)
        {
            return customerService.GetTradesmanIsFavorite(customerId, tradesmanId);
        }

        [HttpGet]
        public Customer GetPersonalDetails(long customerId)
        {
            return customerService.GetPersonalDetails(customerId);
        }

        [HttpPost]
        public bool UpdatePersonalDetails([FromBody] Customer customer)
        {
            return customerService.UpdatePersonalDetails(customer);
        }

        [HttpGet]
        public bool CheckSavedAdsByadId(long supplierAdId, long customerId)
        {
            return customerService.CheckSavedAdsByadId(supplierAdId, customerId);
        }
        [HttpGet]
        public bool CheckLikedAdsByadId(long supplierAdId, long customerId)
        {
            return customerService.CheckLikedAdsByadId(supplierAdId, customerId);
        }
        [HttpGet]
        public int CheckRatedAdsByadId(long supplierAdId, long customerId)
        {
            return customerService.CheckRatedAdsByadId(supplierAdId, customerId);
        }
        [HttpGet]
        public bool CheckSavedTradesmanById(long tradesmanId, long customerId)
        {
            return customerService.CheckSavedTradesmanById(tradesmanId, customerId);
        }
        [HttpGet]
        public List<AdminDashboardVM> SpGetAdminDashBoard()
        {
            return customerService.SpGetAdminDashBoard();
        }


        [HttpPost]
        public List<SpPrimaryUserVM> SpGetPrimaryUsersList([FromBody] GenericUserVM genericUserVM)
        {
            return customerService.SpGetPrimaryUsersList(genericUserVM);
        }

        [HttpGet]
        public SpUserProfileVM SpGetUserProfile(string role, long userId, string aspUserId)
        {
            return customerService.SpGetUserProfile(role, userId, aspUserId);
        }

        [HttpGet]
        public List<CustomersDTO> GetAllCustomers(System.DateTime? startDate, System.DateTime? endDate)
        {
            return customerService.Get_All_Customers(startDate, endDate);
        }
        [HttpGet]
        public List<Customer> Get_All_Customers_Yearly_Report()
        {
            return customerService.Get_All_Customers_Yearly_Report();
        }
        [HttpGet]
        public List<Customer> Get_All_Customers_From_To_Report([FromQuery] DateTime StartDate, DateTime EndDate)
        {
            return customerService.Get_All_Customers_From_To_Report(StartDate, EndDate);
        }
        [HttpPost]
        public List<Customer> GetCustomerReport([FromBody] List<string> userId)
        {
            return customerService.GetCustomerReport(userId);
        }
        [HttpGet]
        public List<IdValueVM> GetAllCustomersDropdown()
        {
            return customerService.Get_All_CustomersDropdown();
        }

        [HttpGet]
        public bool UpdateCustomerPublicId(long customerId, string publicId)
        {
            return customerService.UpdateCustomerPublicId(customerId, publicId);
        }
        [HttpGet]
        public List<CustomersDTO> GetRegistredCustomerForDaynamicReport(System.DateTime? startDate, System.DateTime? endDate, string customers, string status, string city, bool lastactive, string location, string mobile, string cnic, string emailtype, string mobileType, string userType, string jobsType)
        {
            return customerService.GetCustomersFordaynamicReport(startDate, endDate, customers, status, city, lastactive, location, mobile, cnic, emailtype, mobileType, userType, jobsType);
        }
        //[HttpGet]
        //public List<CustomersDTO> GetRegistredCustomerForDaynamicReport(System.DateTime? startDate, System.DateTime? endDate, string customers, string status, string city, bool lastactive, string location)
        //{
        //    return customerService.GetRegistredCustomerForDaynamicReport(startDate, endDate, customers, status, city, lastactive, location);
        //}
        [HttpGet]
        public List<Customer> GetCustomerAddressList()
        {
            return customerService.GetCustomerAddressList();
        }
        [HttpGet]
        public Response BlockCustomer(string customerId, bool status)
        {
            return customerService.BlockCustomer(customerId, status);
        }
        [HttpGet]
        //  [Authorize(Roles = "Trademan")]
        public List<Customer> GetAllActiveCustomer()
        {
            var result = customerService.GetAllActiveCustomer();
            return result.ToList();
        }

        [HttpGet]
        public int AdViewsCount(long supplierAdId)
        {
            return customerService.AdViewsCount(supplierAdId);
        }

        [HttpGet]
        public List<AdViewerVM> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return customerService.GetAdViewerList(supplierAdId, pageSize, pageNumber);
        }
        [HttpGet]
        public List<AdViewerVM> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return customerService.GetAdLikerList(supplierAdId, pageSize, pageNumber);
        }
        [HttpGet]
        public List<AdRatingsVm> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber)
        {
            return customerService.GetAdRatedList(supplierAdId, pageSize, pageNumber);
        }

        [HttpGet]
        public List<UserFavoriteTradesmenVM> GetCustomerFavoriteTradesman(long customerId, int pageSize, int pageNumber)
        {
            return customerService.GetCustomerFavoriteTradesman(customerId, pageSize, pageNumber);
        }
        [HttpGet]
        public List<TopTradesmanVM> GetTopTradesman(int pageSize, int pageNumber, long CategoryId, long customerId)
        {
            return customerService.GetTopTradesman(pageSize, pageNumber, CategoryId, customerId);
        }
        [HttpPost]
        public Response AddLinkedSalesman([FromBody] SalesmanVM salesmanVM)
        {
            return customerService.AddLinkedSalesman(salesmanVM);
        }


        [HttpGet]
        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            return await customerService.UpdatePhoneNumberByUserId(userId, phoneNumber);
        }
        [HttpGet]
        public string GetCustomerByPublicId(string publicID)
        {
            return customerService.GetCustomerByPublicId(publicID);
        }

        [HttpGet]
        public CustomerDashBoardCountVM GetCustomerDashBoardCount(long customerId, string userId)
        {
            return customerService.GetCustomerDashBoardCount(customerId, userId);
        }

        [HttpPost]
        public Response SaveAndRemoveProductsInWishlist([FromBody] string data)
        {
            return  customerService.SaveAndRemoveProductsInWishlist(data);
        }

        [HttpGet]
        public async Task<Response> CheckProductStatusInWishList(long customerId, long productId)
        {
            return await customerService.CheckProductStatusInWishList(customerId,  productId);
        }

        [HttpGet]
        public async Task<Response> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize)
        {
            return await customerService.GetCustomerWishListProducts(customerId, pageNumber, pageSize);
        }
        [HttpGet]
        public async Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize)
        {
            return await customerService.GetCustomerWishListProductsMobile(customerId, pageNumber, pageSize);
        }

        [HttpGet]
        public async Task<Response> GetUserDetailsByUserRole(string userId, string userRole)
        {
            return await customerService.GetUserDetailsByUserRole(userId, userRole);
        }
    }
}
