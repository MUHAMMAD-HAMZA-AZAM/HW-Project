using HW.CustomerModels;
using HW.Events;
using HW.UserViewModels;
using HW.Utility;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.Job_ViewModels;
using HW.SupplierViewModels;
using HW.CustomerModels.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Response = HW.Utility.Response;

namespace HW.CustomerApi.Services
{
    public interface ICustomerService
    {
        Task<IQueryable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(long id);
        Task<List<Customer>> GetCustomersByIds(List<long> ids);
        Customer GetCustomerByName(string name);
        Task<Response> AddEditCustomer(Customer customer);
        Task<Response> DeleteCustomer(long id);
        Task<List<Customer>> GetCustomerByIdList(List<long> customersIds);
        List<CustomerFavorites> GetCustomerFavoriteByCustomerId(long customersId);
        Response SetTradesmanToCustomerFavorite(CustomerFavorites customerFavorites);
        Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId, long customerId, string userId);
        Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId, long customerId, string userId);
        Task<Response> AddCustomerProductRating(int rating, long supplierAdId, long customerId, string userId);
        Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId, long customerId, string userId);
        Task<Response> AddAdViews(long supplierAdId, long customerId, string userId);
        IQueryable<CustomerSavedAds> GetCustomerSavedAds(SavedAdsVM savedAdsVM);
        long GetEntityIdByUserId(string userId);
        Customer GetCustomerByUserId(string userId);
        Customer GetPersonalDetails(long customerId);
        bool UpdatePersonalDetails(Customer customer);
        bool GetTradesmanIsFavorite(long customerId, long tradesmanId);
        bool CheckSavedAdsByadId(long supplierAdId, long customerId);
        bool CheckLikedAdsByadId(long supplierAdId, long customerId);
        int CheckRatedAdsByadId(long supplierAdId, long customerId);
        bool CheckSavedTradesmanById(long tradesmanId, long customerId);

        List<AdminDashboardVM> SpGetAdminDashBoard();
        List<SpPrimaryUserVM> SpGetPrimaryUsersList(GenericUserVM genericUserVM);
        SpUserProfileVM SpGetUserProfile(string role, long userId, string aspUserId);
        List<CustomersDTO> Get_All_Customers(DateTime? startDate, DateTime? endDate);
        List<IdValueVM> Get_All_CustomersDropdown();
        List<Customer> Get_All_Customers_Yearly_Report();
        List<Customer> Get_All_Customers_From_To_Report(DateTime StartDate, DateTime EndDate);
        List<Customer> GetCustomerReport(List<string> userId);
        bool UpdateCustomerPublicId(long customerId, string publicId);
        List<CustomersDTO> GetCustomersFordaynamicReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location, string mobile, string cnic, string emailtype, string mobileType, string userType, string jobsType);
        List<Customer> GetCustomerAddressList();
        Response BlockCustomer(string customerId, bool status);
        List<Customer> GetAllActiveCustomer();
        int AdViewsCount(long supplierAdId);
        List<AdViewerVM> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber);

        List<UserFavoriteTradesmenVM> GetCustomerFavoriteTradesman(long customerId, int pageSize, int pageNumber);
        List<AdViewerVM> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber);
        List<AdRatingsVm> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber);
        List<TopTradesmanVM> GetTopTradesman(int pageSize, int pageNumber, long CategoryId, long customerId);
        Response AddLinkedSalesman(SalesmanVM salesmanVM);
        Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber);
        string GetCustomerByPublicId(string publicID);
        CustomerDashBoardCountVM GetCustomerDashBoardCount(long customerId, string userId);
        Response SaveAndRemoveProductsInWishlist(string data);
        Task<Response> CheckProductStatusInWishList(long customerId, long productId);
        Task<Response> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize);
        Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize);

        Task<Response> GetUserDetailsByUserRole(string userId, string userRole);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork uow;
        private readonly IBus bus;
        private readonly IExceptionService Exc;
        private readonly IMapper _mapper;
        public CustomerService(IUnitOfWork uow, /*IBus bus,*/  IExceptionService Exc, IMapper mapper)
        {
            this.uow = uow;
            //this.bus = bus;
            this.Exc = Exc;
            _mapper = mapper;
        }

        public async Task<IQueryable<Customer>> GetAllCustomers()
        {
            await bus.Publish(new LoggingEvent { LogLevel = (int)LogLevel.Information, Message = "Get All customers" });
            return uow.Repository<Customer>().GetAll();
        }

        public async Task<Customer> GetCustomerById(long customerId)
        {
            try
            {

                var getCustomer = await uow.Repository<Customer>().GetByIdAsync(customerId);
                return getCustomer;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Customer();
            }
        }

        public Customer GetCustomerByName(string name)
        {
            try
            {
                return uow.Repository<Customer>().Get(c => c.FirstName == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Customer();
            }
        }

        public async Task<Response> AddEditCustomer(Customer customer)
        {
            Response response = new Response();
            try
            {
                var customerId = uow.Repository<Customer>().GetAll().Where(x => x.UserId == customer.UserId).Select(s => s.CustomerId).FirstOrDefault();
                customer.CustomerId = customer.CustomerId != 0 ? customer.CustomerId : customerId;

                if (customer.CustomerId > 0)
                {
                    var existingData = await GetCustomerById(customer.CustomerId);

                    if (existingData != null)
                    {
                        var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        var jsonValues = JsonConvert.SerializeObject(customer, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<Customer>().Update(existingData);
                    }
                }
                else
                {
                    await uow.Repository<Customer>().AddAsync(customer);
                }
                await uow.SaveAsync();

                response.ResultData = customer;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
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

        public async Task<Response> DeleteCustomer(long id)
        {
            Response response = new Response();
            try
            {
                await uow.Repository<Customer>().DeleteAsync(id);
                await uow.SaveAsync();
                response.Message = "Successfully deleted.";
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

        public async Task<List<Customer>> GetCustomersByIds(List<long> ids)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                var query = uow.Repository<Customer>().GetAll();
                customers = query.Where(c => ids.Any(id => c.CustomerId == id)).ToList();
                return customers;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }
        }

        public async Task<List<Customer>> GetCustomerByIdList(List<long> customersIds)
        {
            try
            {
                return uow.Repository<Customer>().GetAll().Where(c => customersIds.Contains(c.CustomerId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<Customer>();
            }
        }

        public List<CustomerFavorites> GetCustomerFavoriteByCustomerId(long customersId)
        {
            try
            {
                return uow.Repository<CustomerFavorites>().GetAll().Where(c => c.CustomerId == customersId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<CustomerFavorites>();
            }
        }

        public Response SetTradesmanToCustomerFavorite(CustomerFavorites customerFavorites)
        {
            Response response = new Response();

            try
            {
                var query = uow.Repository<CustomerFavorites>().GetAll().FirstOrDefault(c => c.CustomerId == customerFavorites.CustomerId && c.TradesmanId == customerFavorites.TradesmanId);
                if (query == null)
                {
                    uow.Repository<CustomerFavorites>().Add(customerFavorites);
                    uow.SaveAsync();
                    response.Message = "Successfully added to favorites";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Already in your favorites";
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

        public bool GetTradesmanIsFavorite(long customerId, long tradesmanId)
        {
            bool response = false;
            try
            {

                CustomerFavorites query = uow.Repository<CustomerFavorites>().GetAll().FirstOrDefault(c => c.CustomerId == customerId && c.TradesmanId == tradesmanId);
                if (query != null)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return response;
        }


        public IQueryable<CustomerSavedAds> GetCustomerSavedAds(SavedAdsVM savedAdsVM)
        {
            try
            {
                return uow.Repository<CustomerSavedAds>().Get(x => x.CustomerId == savedAdsVM.CustomerId && savedAdsVM.SupplierAdIds.Contains(x.SupplierAdsId));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<CustomerSavedAds>().AsQueryable();
            }
        }

        public async Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                if (isSaved && supplierAdId > 0 && customerId > 0)
                {
                    CustomerSavedAds existingData = uow.Repository<CustomerSavedAds>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();

                    if (existingData == null)
                    {
                        CustomerSavedAds customerSavedAd = new CustomerSavedAds()
                        {
                            CustomerId = customerId,
                            SupplierAdsId = supplierAdId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userId
                        };

                        await uow.Repository<CustomerSavedAds>().AddAsync(customerSavedAd);
                        await uow.SaveAsync();
                        response.Message = "Successfull Created.";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Ad is already saved.";
                        response.Status = ResponseStatus.Error;
                    }

                }

                else if (!isSaved && supplierAdId > 0 && customerId > 0)
                {
                    CustomerSavedAds existingData = uow.Repository<CustomerSavedAds>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();
                    if (existingData != null)
                    {
                        await uow.Repository<CustomerSavedAds>().DeleteAsync(existingData.CustomerSavedAdsId);
                        await uow.SaveAsync();
                        response.Message = "Successfull Deleted.";
                        response.Status = ResponseStatus.OK;
                    }
                }

                else
                {
                    response.Message = "Login to save this ad";
                    response.Status = ResponseStatus.Error;
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
        public async Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                if (isLiked && supplierAdId > 0 && customerId > 0)
                {
                    CustomerLikedAds existingData = uow.Repository<CustomerLikedAds>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();

                    if (existingData == null)
                    {
                        CustomerLikedAds customerLikedAd = new CustomerLikedAds()
                        {
                            CustomerId = customerId,
                            SupplierAdsId = supplierAdId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userId
                        };

                        await uow.Repository<CustomerLikedAds>().AddAsync(customerLikedAd);
                        await uow.SaveAsync();
                        response.Message = "Successfull Created.";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Ad is already saved.";
                        response.Status = ResponseStatus.Error;
                    }

                }

                else if (!isLiked && supplierAdId > 0 && customerId > 0)
                {
                    CustomerLikedAds existingData = uow.Repository<CustomerLikedAds>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();
                    if (existingData != null)
                    {
                        await uow.Repository<CustomerLikedAds>().DeleteAsync(existingData.CustomerLikedAdsId);
                        await uow.SaveAsync();
                        response.Message = "Successfull Deleted.";
                        response.Status = ResponseStatus.OK;
                    }
                }

                else
                {
                    response.Message = "Login to save this ad";
                    response.Status = ResponseStatus.Error;
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
        public async Task<Response> AddCustomerProductRating(int rating, long supplierAdId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                if (rating > 0 && supplierAdId > 0 && customerId > 0)
                {
                    CustomerProductRating existingData = uow.Repository<CustomerProductRating>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();

                    if (existingData == null)
                    {
                        CustomerProductRating customerRatedProduct = new CustomerProductRating()
                        {
                            CustomerId = customerId,
                            SupplierAdsId = supplierAdId,
                            Rating = rating,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userId
                        };

                        await uow.Repository<CustomerProductRating>().AddAsync(customerRatedProduct);
                        await uow.SaveAsync();
                        response.Message = "Product Rated Successfully.";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Product Already Rated.";
                        response.Status = ResponseStatus.Error;
                    }

                }

                else if (rating == 0 && supplierAdId > 0 && customerId > 0)
                {

                    response.Message = "Please Rate Product";
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
        public async Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                if (isLiked && tradesmanId > 0 && customerId > 0)
                {
                    CustomerFavoritesTradesman existingData = uow.Repository<CustomerFavoritesTradesman>().Get(x => x.TradesmanId == tradesmanId && x.CustomerId == customerId).FirstOrDefault();

                    if (existingData == null)
                    {
                        CustomerFavoritesTradesman customerLikedTradesman = new CustomerFavoritesTradesman()
                        {
                            CustomerId = customerId,
                            TradesmanId = tradesmanId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userId
                        };

                        await uow.Repository<CustomerFavoritesTradesman>().AddAsync(customerLikedTradesman);
                        await uow.SaveAsync();
                        response.Message = "Successfull Created.";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Ad is already saved.";
                        response.Status = ResponseStatus.Error;
                    }

                }

                else if (!isLiked && tradesmanId > 0 && customerId > 0)
                {
                    CustomerFavoritesTradesman existingData = uow.Repository<CustomerFavoritesTradesman>().Get(x => x.TradesmanId == tradesmanId && x.CustomerId == customerId).FirstOrDefault();
                    if (existingData != null)
                    {
                        await uow.Repository<CustomerLikedAds>().DeleteAsync(existingData.CustomerFavoritesTradesmanId);
                        await uow.SaveAsync();
                        response.Message = "Successfull Deleted.";
                        response.Status = ResponseStatus.OK;
                    }
                }

                else
                {
                    response.Message = "Login to save this ad";
                    response.Status = ResponseStatus.Error;
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


        public async Task<Response> AddAdViews(long supplierAdId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                if (supplierAdId > 0 && customerId > 0)
                {
                    AdViews existingData = uow.Repository<AdViews>().Get(x => x.SupplierAdsId == supplierAdId && x.CustomerId == customerId).FirstOrDefault();

                    if (existingData == null)
                    {
                        AdViews adview = new AdViews()
                        {
                            CustomerId = customerId,
                            SupplierAdsId = supplierAdId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = userId
                        };

                        await uow.Repository<AdViews>().AddAsync(adview);
                        await uow.SaveAsync();
                        response.Message = "Successfully Created.";
                        response.Status = ResponseStatus.OK;
                    }
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

        public int AdViewsCount(long supplierAdId)
        {
            int count = 0;
            try
            {
                if (supplierAdId > 0)
                {
                    count = uow.Repository<AdViews>().Get(x => x.SupplierAdsId == supplierAdId).ToList().Count();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return count;
        }

        public long GetEntityIdByUserId(string userId)
        {
            try
            {
                return uow.Repository<Customer>().GetAll().FirstOrDefault(t => t.UserId == userId)?.CustomerId ?? 0;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return 0;
            }
        }

        public Customer GetCustomerByUserId(string userId)
        {
            Customer customer = new Customer();

            try
            {
                customer = uow.Repository<Customer>().GetAll().FirstOrDefault(x => x.UserId == userId);
                return customer;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Customer();
            }


        }

        public Customer GetPersonalDetails(long customerId)
        {
            try
            {
                return uow.Repository<Customer>().GetAll().Where(x => x.CustomerId == customerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Customer();
            }
        }

        public bool UpdatePersonalDetails(Customer customer)
        {
            try
            {
                Customer isCustomerPresent = uow.Repository<Customer>().GetAll().Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();

                if (isCustomerPresent != null)
                {
                    isCustomerPresent.CustomerId = customer.CustomerId;
                    isCustomerPresent.FirstName = customer.FirstName;
                    isCustomerPresent.LastName = customer.LastName;
                    isCustomerPresent.ModifiedOn = customer.ModifiedOn;
                    isCustomerPresent.Dob = customer.Dob;
                    uow.Repository<Customer>().Update(isCustomerPresent);
                    uow.Save();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public bool CheckSavedAdsByadId(long supplierAdId, long customerId)
        {
            try
            {
                var adsCheck = uow.Repository<CustomerSavedAds>().GetAll().Where(s => s.CustomerId == customerId && s.SupplierAdsId == supplierAdId).FirstOrDefault();
                if (adsCheck != null)
                {
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
        public bool CheckLikedAdsByadId(long supplierAdId, long customerId)
        {
            try
            {
                var adsCheck = uow.Repository<CustomerLikedAds>().GetAll().Where(s => s.CustomerId == customerId && s.SupplierAdsId == supplierAdId).FirstOrDefault();
                if (adsCheck != null)
                {
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
        public int CheckRatedAdsByadId(long supplierAdId, long customerId)
        {
            try
            {
                var adsCheck = uow.Repository<CustomerProductRating>().GetAll().Where(s => s.CustomerId == customerId && s.SupplierAdsId == supplierAdId).FirstOrDefault();
                if (adsCheck != null)
                {
                    return adsCheck.Rating;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }
        public bool CheckSavedTradesmanById(long tradesmanId, long customerId)
        {
            try
            {
                var adsCheck = uow.Repository<CustomerFavoritesTradesman>().GetAll().Where(s => s.CustomerId == customerId && s.TradesmanId == tradesmanId).FirstOrDefault();
                if (adsCheck != null)
                {
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

        public List<AdminDashboardVM> SpGetAdminDashBoard()
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                };
                var supplier = uow.ExecuteReaderSingleDS<AdminDashboardVM>("SP_GetSupplierCatDashboard", sqlParameters).ToList();
                var customer = uow.ExecuteReaderSingleDS<AdminDashboardVM>("SP_GetCustomerJobsDashboard", sqlParameters).ToList();
                var orgnization = uow.ExecuteReaderSingleDS<AdminDashboardVM>("SP_GetOrgnizationBySkillDashBorad", sqlParameters).ToList();
                List<AdminDashboardVM> adminDashboardVM = uow.ExecuteReaderSingleDS<AdminDashboardVM>("Sp_AdminDashboard", sqlParameters).ToList();
                foreach (var item in customer)
                {
                    AdminDashboardVM customerDashboardVM = new AdminDashboardVM()
                    {
                        CustomerCity = item.CustomerCity,
                        CustomerCount = item.CustomerCount,
                        CustomerSkillName = item.CustomerSkillName
                    };
                    adminDashboardVM.Add(customerDashboardVM);
                }
                foreach (var item in supplier)
                {
                    AdminDashboardVM supplierDashboardVM = new AdminDashboardVM()
                    {
                        SupplierCity = item.SupplierCity,
                        SupplieCount = item.SupplieCount,
                        SupplierCategory = item.SupplierCategory
                    };
                    adminDashboardVM.Add(supplierDashboardVM);
                }
                foreach (var item in orgnization)
                {
                    AdminDashboardVM orgDashboardVM = new AdminDashboardVM()
                    {
                        OrgCity = item.OrgCity,
                        OrgSkillName = item.OrgSkillName,
                        OrgCount = item.OrgCount
                    };
                    adminDashboardVM.Add(orgDashboardVM);
                }
                return adminDashboardVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AdminDashboardVM>();
            }
        }


        public List<SpPrimaryUserVM> SpGetPrimaryUsersList(GenericUserVM genericUserVM)
        {
            List<SpPrimaryUserVM> PrimaryUsersList = new List<SpPrimaryUserVM>();
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@pageSize",genericUserVM.pageSize),
                    new SqlParameter("@pageNumber",genericUserVM.pageNumber),
                    new SqlParameter("@dataOrderBy",genericUserVM.dataOrderBy),
                    new SqlParameter("@customerName",genericUserVM.userName),
                    new SqlParameter("@startDate",genericUserVM.startDate),
                    new SqlParameter("@endDate",genericUserVM.endDate),
                    new SqlParameter("@city",genericUserVM.city),
                    new SqlParameter("@skill",genericUserVM.skills),
                    new SqlParameter("@location",genericUserVM.location),
                    new SqlParameter("@mobile",genericUserVM.mobile),
                    new SqlParameter("@usertype",genericUserVM.usertype),
                    new SqlParameter("@emailtype",genericUserVM.emailtype),
                    new SqlParameter("@mobileType",genericUserVM.mobileType),
                    new SqlParameter("@jobsType",genericUserVM.jobsType),
                    new SqlParameter("@cnic",genericUserVM.cnic),
                    new SqlParameter("@sourceOfReg",genericUserVM.sourceOfReg),
                    new SqlParameter("@email",genericUserVM.email),
                    new SqlParameter("@SalesmanId",genericUserVM.SalesmanId),
                    new SqlParameter("@customerId",genericUserVM.customerId),
                };
                PrimaryUsersList = uow.ExecuteReaderSingleDS<SpPrimaryUserVM>("Sp_PrimaryUsers", sqlParameters);
                return PrimaryUsersList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpPrimaryUserVM>();
            }

        }

        public SpUserProfileVM SpGetUserProfile(string role, long userId, string aspUserId)
        {
            try
            {
                SqlParameter[] sqlParameters = {

                   new SqlParameter("@userRole",role ),
                   new SqlParameter("@userId",userId),
                   new SqlParameter("@aspUserId",aspUserId)

                };
                return uow.ExecuteReaderSingleDS<SpUserProfileVM>("Sp_UserProfile", sqlParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpUserProfileVM();
            }

        }
        public List<CustomersDTO> Get_All_Customers(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("@FromDate",startDate),
                    new SqlParameter("@ToDate",endDate)
                };
                var res = uow.ExecuteReaderSingleDS<CustomersDTO>("Sp_PrimaryUsers_Report", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CustomersDTO>();
            }

        }
        public List<IdValueVM> Get_All_CustomersDropdown()
        {
            try
            {
                var result = uow.Repository<Customer>().GetAll().Select(s => new IdValueVM { Id = s.CustomerId, Value = s.FirstName + " " + s.LastName }).ToList(); ;
                return result.ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }

        }
        public List<Customer> Get_All_Customers_Yearly_Report()
        {
            try
            {
                var res = uow.Repository<Customer>().GetAll().Where(x => x.CreatedOn.Year == DateTime.Now.Year);
                return res.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }
        }

        public List<Customer> Get_All_Customers_From_To_Report(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var res = uow.Repository<Customer>().GetAll().Where(x => x.CreatedOn.Date >= StartDate.Date && x.CreatedOn.Date <= EndDate.Date);
                return res.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }
        }

        public List<Customer> GetCustomerReport(List<string> userId)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                customers = uow.Repository<Customer>().GetAll().Where(c => userId.Contains(c.UserId)).ToList();
                return customers;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }



        }

        public bool UpdateCustomerPublicId(long customerId, string publicId)
        {
            try
            {
                Customer customer = uow.Repository<Customer>().GetById(customerId);
                if (customer != null)
                {
                    customer.PublicId = publicId;

                    uow.Repository<Customer>().Update(customer);
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
        public List<CustomersDTO> GetCustomersFordaynamicReport(DateTime? startDate, DateTime? endDate, string customer, string status, string city, bool lastactive, string location, string mobile, string cnic, string emailtype, string mobileType, string userType, string jobsType)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate",endDate),
                new SqlParameter("@Customer",customer),
                new SqlParameter("@City",city),
                new SqlParameter("@LastActive",lastactive),
                new SqlParameter("@Location",location),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@emailtype",emailtype),
                new SqlParameter("@mobileType",mobileType),
                new SqlParameter("@cnic",cnic),
                new SqlParameter("@userType",userType),
                new SqlParameter("@jobsType",jobsType),

            };
                var result = uow.ExecuteReaderSingleDS<CustomersDTO>("Sp_CustomerRegistretionDynamic_Report", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }
        public List<Customer> GetCustomerAddressList()
        {
            try
            {
                List<Customer> customersAddressList = new List<Customer>();
                //customersAddressList = uow.Repository<Customer>().GetAll().Select(x => new Customer { LastName = x.LastName }).Where(x => x.LastName != null && x.LastName != "").ToList();
                customersAddressList = uow.Repository<Customer>().GetAll().Select(x => new Customer { StreetAddress = x.StreetAddress }).Where(x => x.StreetAddress != null && x.StreetAddress != "").ToList();
                return customersAddressList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }
        }

        public Response BlockCustomer(string customerId, bool status)
        {
            Response response = new Response();
            try
            {
                int id = Convert.ToInt32(customerId);
                var isCutomer = uow.Repository<Customer>().GetById(id);
                if (isCutomer != null)
                {
                    if (status)
                    {
                        isCutomer.IsActive = true;
                    }
                    else
                    {
                        isCutomer.IsActive = false;
                    }
                    uow.Repository<Customer>().Update(isCutomer);
                    uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Customer status has been changed!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error in changing custome status!";
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
        public List<Customer> GetAllActiveCustomer()
        {
            try
            {
                var result = uow.Repository<Customer>().GetAll().Select(s => new Customer { UserId = s.UserId, FirstName = s.FirstName + " " + s.LastName }).Where(x => x.FirstName != "").OrderBy(x => x.FirstName).ToList(); ;
                return result.ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Customer>();
            }
        }


        public List<AdViewerVM> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@supplierAdId", supplierAdId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<AdViewerVM> result = uow.ExecuteReaderSingleDS<AdViewerVM>("SP_AdViewers", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public List<UserFavoriteTradesmenVM> GetCustomerFavoriteTradesman(long customerId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@customerId", customerId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<UserFavoriteTradesmenVM> result = uow.ExecuteReaderSingleDS<UserFavoriteTradesmenVM>("SP_GetCustomerFavorites", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }
        public List<AdViewerVM> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@supplierAdId", supplierAdId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<AdViewerVM> result = uow.ExecuteReaderSingleDS<AdViewerVM>("SP_AdLikers", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }
        public List<AdRatingsVm> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                new SqlParameter("@supplierAdId", supplierAdId),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageNumber",pageNumber)

            };
                List<AdRatingsVm> result = uow.ExecuteReaderSingleDS<AdRatingsVm>("SP_AdRated", sqlParameters);

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public List<TopTradesmanVM> GetTopTradesman(int pageSize, int pageNumber, long CategoryId, long customerId)
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
                    new SqlParameter("@pageSize",pageSize),
                    new SqlParameter("@pageNumber",pageNumber),
                    new SqlParameter("@CategoryId",CategoryId),
                    new SqlParameter("@isTestUser",isTestUser),
                    new SqlParameter("@customerId",customerId)
                };
                List<TopTradesmanVM> result = uow.ExecuteReaderSingleDS<TopTradesmanVM>("SP_TopTradesman", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Response AddLinkedSalesman(SalesmanVM salesmanVM)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SalesmanId",salesmanVM.SalesmanId),
                    new SqlParameter("@CustomerId",salesmanVM.CustomerId),
                    new SqlParameter("@CreatedBy",salesmanVM.CreatedBy),
                    new SqlParameter("@CampaignId",salesmanVM.CampaignId),
                };
                var data = uow.ExecuteReaderSingleDS<Response>("Sp_AddCustomerRegisterBy", sqlParameters);
                res.Message = "Data added successfully!";
                res.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                res.Message = ex.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            Response response = new Response();

            try
            {
                Customer customer = uow.Repository<Customer>().GetAll().FirstOrDefault(x => x.UserId == userId);

                if (customer != null)
                {
                    customer.MobileNumber = phoneNumber;

                    uow.Repository<Customer>().Update(customer);
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

        public string GetCustomerByPublicId(string publicID)
        {
            string userID = uow.Repository<Customer>().GetAll().Where(x => x.PublicId == publicID).Select(s => s.UserId).FirstOrDefault();
            return userID;
        }

        public CustomerDashBoardCountVM GetCustomerDashBoardCount(long customerId, string userId)
        {
            Guid customerNotificationId = Guid.Parse(userId);
            CustomerDashBoardCountVM customerDashBoardCountVM = new CustomerDashBoardCountVM();
            try
            {
                SqlParameter[] sqlParameters = {

                    new SqlParameter("customerId",customerId),
                    new SqlParameter("customerNotificationId",customerNotificationId)
                };
                customerDashBoardCountVM = uow.ExecuteReaderSingleDS<CustomerDashBoardCountVM>("Sp_GetCustomerDashBoardCounts", sqlParameters).FirstOrDefault();

                return customerDashBoardCountVM;

            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return customerDashBoardCountVM;
        }

        public Response SaveAndRemoveProductsInWishlist(string data)
        {
            var aa = JsonConvert.SerializeObject(data);
            Exc.AddErrorLog($"Line no 1281 data {aa}");
            var response = new Response();
            try
            {
                Exc.AddErrorLog($"Line no 1285{aa}");
                var entity = JsonConvert.DeserializeObject<ProductsWishListDTO>(data);
                var bb = JsonConvert.SerializeObject(entity);
                Exc.AddErrorLog($"Line no 1288 entity {bb}");
                var findProduct = uow.Repository<ProductsWishList>().Get(x => x.Id == entity.Id || (x.CustomerId == entity.CustomerId && x.ProductId == entity.ProductId)).FirstOrDefault();
                var cc = JsonConvert.SerializeObject(entity);
                Exc.AddErrorLog($"Line no 1291 findProduct {cc}");

                if (findProduct != null)
                {
                    uow.Repository<ProductsWishList>().Delete(findProduct);
                    uow.Save();
                    Exc.AddErrorLog("Line no 1298 deleted successfully");

                    response.Status = ResponseStatus.OK;
                    response.Message = "Successfully Deleted";
                }
                else
                {
                    entity.CreatedOn = DateTime.Now;
                    SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@SupplierId",entity.SupplierId ),
                        new SqlParameter("@CustomerId",entity.CustomerId),
                        new SqlParameter("@ProductId",entity.ProductId ),
                        new SqlParameter("@isFavorite ",entity.IsFavorite),
                        new SqlParameter("@Active",entity.Active),
                        new SqlParameter("@ModifiedBy",entity.ModifiedBy),
                        new SqlParameter("@CreatedBy",entity.CreatedBy),
                        };
                    Exc.AddErrorLog(JsonConvert.SerializeObject(sqlParameters));
                    var result = uow.ExecuteReaderSingleDS<ProductsWishListDTO>("SP_SaveAndRemoveProductsInWishlist", sqlParameters);
                    Exc.AddErrorLog(JsonConvert.SerializeObject(result));
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


        public async Task<Response> CheckProductStatusInWishList(long customerId, long productId)
        {
            var response = new Response();
            try
            {
                var FavouriteProductDetails = await uow.Repository<ProductsWishList>().Get(x => x.CustomerId == customerId && x.ProductId == productId).FirstOrDefaultAsync();
                response.ResultData = FavouriteProductDetails;
                response.Status = ResponseStatus.OK;
                response.Message = "Product Details";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@customerId",customerId),
                    new SqlParameter("@pageNumber",pageNumber),
                    new SqlParameter("@pageSize",pageSize)
                };
                var wishListProducts = uow.ExecuteReaderSingleDS<ProductsWishListDTO>("Sp_GetCustomerWishListProductsList", sqlParameter);
                response.ResultData = wishListProducts;
                response.Status = ResponseStatus.OK;
                response.Message = "WishList Products List";
            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                    new SqlParameter("@customerId",customerId),
                    new SqlParameter("@pageNumber",pageNumber),
                    new SqlParameter("@pageSize",pageSize)
                };
                var wishListProducts = uow.ExecuteReaderSingleDS<ProductsWishListDTO>("Sp_GetCustomerWishListProductsListMobile", sqlParameter);
                response.ResultData = wishListProducts;
                response.Status = ResponseStatus.OK;
                response.Message = "WishList Products List";
            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }


        public async Task<Response> GetUserDetailsByUserRole(string userId, string userRole)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@userRole", userRole),
                };
                response.ResultData = uow.ExecuteReaderSingleDS<PersonalDetailsDTO>("SP_GetUserDashBoardDetailsByUserRole", sqlParameters).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                response.Message = "User Details";
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
    }
}
