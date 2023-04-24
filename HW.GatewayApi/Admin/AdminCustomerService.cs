using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.GatewayApi.Services;
using HW.Http;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.UserManagmentModels;
using Microsoft.AspNetCore.Mvc;
using HW.PackagesAndPaymentsViewModels;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.SupplierViewModels;
using HW.PackagesAndPaymentsModels;
using HW.JobModels;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminCustomerServices
    {
        string GetUser();
        Task<List<AdminDashboardVM>> SpGetAdminDashBoard();
        Task<Customer> GetCustomerById(long customerId);
        Task<List<SpPrimaryUserVM>> SpGetPrimaryUsersList(GenericUserVM genericUserVM);
        Task<AdminWebUserProfileVM> SpGetUserProfile(string role, long userId, string aspUserId);
        Task<List<CustomersDTO>> GetAllCustomers(string startDate, string endDate);
        Task<List<IdValueVM>> GetAllCustomersDropdown();
        Task<List<Customer>> GetAllCustomersYearlyReport();
        Task<List<Customer>> GetAllCustomersFromToReport(DateTime StartDate, DateTime EndDate);
        Task<List<IdValueVM>> GetCityList();
        Task<string> GetCityListWithTraxCityId();
        Task<List<CityAndStateVM>> GetCitiesList();
        Task<List<State>> GetStatesList();
        Task<List<CustomersDTO>> GetRegitredCustomerDaynamicReport(string startDate, string endDate, string customer, string status, string city, bool lastactive, string location, string mobile, string cnic, string emailtype, string mobileType, string userType, string jobsType);
        Task<List<Customer>> GetCustomerAddressList();
        Task<Response> BlockCustomer(string customerId,string userId, bool status);
        Task<City> CheckCityAvailability(string cityName);
        Task<Response> AddNewCity(City city);
        Task<Response> AddUpdateTown(TownVM town);
        Task<Response> UpdateCity(City city);
        Task<Response> DeleteCity(string cityId);
        Task<List<AllUsersVM>> GetAll();
        Task<IdentityViewModels.PersonalDetailsVM> GetPersonalDetails(long customerId);
        Task<List<TownVM>> GetAllTown();
        Task<Response> AddLinkedSalesman(SalesmanVM salesmanVM);
        Task<List<JobImages>> GetJobImages(long quotationId);
        Task<Response> AddUpdateSecurityRoleItem(SecurityRoleItemVM securityRoleItem);
        Task<Response> DeleteSecurityRoleItem(int securityRoleItemId);
        Task<List<GetPaymentRecordVM>> GetUserPaymentReceipts(long customerId);
    }

    public class AdminCustomerService : IAdminCustomerServices
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly ICommunicationService communicationService; // sending job post confirmation email
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public AdminCustomerService(IHttpClientService httpClient, ClientCredentials clientCred, ICommunicationService communicationService, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
            this.communicationService = communicationService;
        }

        public string GetUser()
        {
            return "Ho gea";
        }

    public async Task<Customer> GetCustomerById(long customerId)
    {
      try
      {
        Customer customer = JsonConvert.DeserializeObject<Customer>
               (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", ""));

        
        return customer;
      }
      catch (Exception ex)
      {
        Exc.AddErrorLog(ex);
        return new Customer();
      }
    }

    public async Task<List<AdminDashboardVM>> SpGetAdminDashBoard()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<AdminDashboardVM>>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.SpGetAdminDashBoard}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AdminDashboardVM>();
            }
        }


        public async Task<List<SpPrimaryUserVM>> SpGetPrimaryUsersList(GenericUserVM genericUserVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SpPrimaryUserVM>>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.SpGetPrimaryUsersList}", genericUserVM));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SpPrimaryUserVM>();
            }
        }

        public async Task<AdminWebUserProfileVM> SpGetUserProfile(string role, long userId, string aspUserId)
        {
            try
            {
                SpUserProfileVM spUserProfileVM = JsonConvert.DeserializeObject<SpUserProfileVM>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.SpGetUserProfile}?role={role}&userId={userId}&aspUserId={aspUserId}"));


                AdminWebUserProfileVM adminWebUserProfileVM = new AdminWebUserProfileVM
                {
                    UserId = spUserProfileVM.UserId,
                    FirstName = spUserProfileVM.FirstName,
                    LastName = spUserProfileVM.LastName,
                    CNIC = spUserProfileVM.CNIC,
                    DateofBirth = spUserProfileVM.DateofBirth,
                    Email = spUserProfileVM.Email,
                    Gender = spUserProfileVM.Gender,
                    MobileNo = spUserProfileVM.MobileNo,
                    CreatedOn = spUserProfileVM.CreatedOn,
                    ProfileImage = spUserProfileVM.ProfileImage != null ?
                        "data:image/jpg;base64," + Convert.ToBase64String(spUserProfileVM?.ProfileImage) : null,
                };


                return adminWebUserProfileVM;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new AdminWebUserProfileVM();
            }
        }
        public async Task<List<CustomersDTO>> GetAllCustomers(string startDate, string endDate)
        {
            string str = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAllCustomers}?startDate={startDate}&endDate={endDate}", "");
            return JsonConvert.DeserializeObject<List<CustomersDTO>>(str);
        }
        public async Task<List<IdValueVM>> GetAllCustomersDropdown()
        {
            string str = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAllCustomersDropdown}", "");
            return JsonConvert.DeserializeObject<List<IdValueVM>>(str);
        }
        public async Task<List<Customer>> GetAllCustomersYearlyReport()
        {
            string str = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.Get_All_Customers_Yearly_Report}", "");
            return JsonConvert.DeserializeObject<List<Customer>>(str);
        }

        public async Task<List<Customer>> GetAllCustomersFromToReport(DateTime StartDate, DateTime EndDate)
        {
            string str = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.Get_All_Customers_From_To_Report}?StartDate={StartDate}&EndDate={EndDate}", "");
            return JsonConvert.DeserializeObject<List<Customer>>(str);
        }
        public async Task<List<IdValueVM>> GetCityList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<IdValueVM>>(
                       await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", "")
                   );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }
        public async Task<string> GetCityListWithTraxCityId()
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityListWithTraxCityId}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return string.Empty;
            }
        }
        public async Task<List<CityAndStateVM>> GetCitiesList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<CityAndStateVM>>(
                       await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCitiesList}", "")
                   );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CityAndStateVM>();
            }
        }
        public async Task<List<State>> GetStatesList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<State>>(
                       await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetStatesList}", "")
                   );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<State>();
            }
        }
        public async Task<List<CustomersDTO>> GetRegitredCustomerDaynamicReport(string startDate, string endDate, string customer, string status, string city, bool lastactive, string location, string mobile, string cnic, string emailtype, string mobileType, string userType, string jobsType)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomersFordaynamicReport}?startDate={startDate}&endDate={endDate}&customers={customer}&status={status}&city={city}&lastactive={lastactive}&location={location}&mobile={mobile}&cnic={cnic}&emailtype={emailtype}&mobileType={mobileType}&userType={userType}&jobsType={jobsType}");

            return JsonConvert.DeserializeObject<List<CustomersDTO>>(response);
        }
        public async Task<List<Customer>> GetCustomerAddressList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerAddressList}", "");
            return JsonConvert.DeserializeObject<List<Customer>>(response);
        }
        public async Task<Response> BlockCustomer(string customerId, string userId, bool status)
        {
            Response response = new Response();
            try
            {

               Response customerResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.BlockCustomer}?customerId={customerId}&status={status}", ""));

                if(customerResponse.Status== ResponseStatus.OK)
                {
                    Response identityResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.BlockUser}?userId={userId}&status={status}", ""));

                    if(identityResponse.Status == ResponseStatus.OK){
                        response.Message = "Customer status has been changed!";
                        response.Status = ResponseStatus.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<City> CheckCityAvailability(string cityName)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.CheckCityAvailability}?cityName={cityName}", "");
            return JsonConvert.DeserializeObject<City>(response);
        }
        public async Task<Response> UpdateCity(City city)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.UpdateCity}", city);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> DeleteCity(string cityId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.DeleteCity}?cityId={cityId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddNewCity(City city)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddNewCity}", city);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> AddUpdateTown(TownVM town)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddUpdateTown}", town);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<AllUsersVM>> GetAll()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetAllActiveUsers}");
            return JsonConvert.DeserializeObject<List<AllUsersVM>>(response);
        }
        public async Task<IdentityViewModels.PersonalDetailsVM> GetPersonalDetails(long customerId)
        {
            try
            {
                if (customerId > 0)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>
                       (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetPersonalDetails}?customerId={customerId}", ""));


                    return new IdentityViewModels.PersonalDetailsVM()
                    {
                        EntityId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Cnic = customer.Cnic,
                        Email = customer.EmailAddress,
                        Gender = customer.Gender.Value,
                        DateOfBirth = customer.Dob.Value,
                        MobileNumber = customer.MobileNumber,
                        UserId = customer.UserId,
                        ProfileImage = null,
                        CityId = customer.CityId.HasValue ? customer.CityId.Value : 0,
                        Town = customer.State

                    };
                }
                else
                {
                    return new IdentityViewModels.PersonalDetailsVM();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new IdentityViewModels.PersonalDetailsVM();
            }
        }
        public async Task<List<TownVM>> GetAllTown()
        {
            var response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllTown}");
            return JsonConvert.DeserializeObject<List<TownVM>>(response);
        }
        public async Task<Response> AddLinkedSalesman(SalesmanVM salesmanVM)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddLinkedSalesman}", salesmanVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<JobImages>> GetJobImages(long quotationId)
        {
            var jobImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImage}?quotationid={quotationId}");
            return JsonConvert.DeserializeObject<List<JobImages>>(jobImagesJson);

        }

        public async Task<Response> AddUpdateSecurityRoleItem(SecurityRoleItemVM securityRoleItem)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AddUpdateSecurityRoleItem}", securityRoleItem);
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<Response> DeleteSecurityRoleItem(int securityRoleItemId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.DeleteSecurityRoleItem}?securityRoleItemId={securityRoleItemId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<List<GetPaymentRecordVM>> GetUserPaymentReceipts(long customerId)
        {
            List<GetPaymentRecordVM> getPaymentRecordVMs = new List<GetPaymentRecordVM>();
            try
            {
                List<TradesmanJobReceipts> tradesmanJobReceipts = new List<TradesmanJobReceipts>();

                List<JobDetail> jobDetail = JsonConvert.DeserializeObject<List<JobDetail>>
                   (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobStatusByCustomerId}?customerId={customerId}&statusId={(int)Utility.BidStatus.Completed}", ""));

                tradesmanJobReceipts = JsonConvert.DeserializeObject<List<TradesmanJobReceipts>>
                      (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getPaymentRecords}", jobDetail.Select(j => j.JobDetailId).ToList()));

                List<PaymentMethod> paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPaymentMethods}", ""));

                getPaymentRecordVMs = tradesmanJobReceipts.Select(x => new GetPaymentRecordVM
                {
                    Amount = x?.Amount ?? 0,
                    CreatedOn = x.PaymentDate,
                    DirectPayment = x.DirectPayment,
                    JobDetailId = jobDetail.Where(a => a.JobDetailId == x.JobDetailId).Select(a => a.JobQuotationId).FirstOrDefault(),
                    Title = jobDetail.Where(s => s.JobDetailId == x.JobDetailId).Select(d => d.Title).FirstOrDefault(),
                    TransactionType = paymentMethods.Where(s => s.PaymentMethodId == x.PaymentMethodId).Select(s => s.Name).FirstOrDefault(),
                    DiscountedAmount = x?.DiscountedAmount ?? 0,
                    PaymentStatus = x?.PaymentStatus ?? 0,
                    PaidViaWallet = x?.PaidViaWallet ?? 0,
                    PayableAmount = x?.PayableAmount ?? x.Amount,


                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return getPaymentRecordVMs;
        }

    }

}
