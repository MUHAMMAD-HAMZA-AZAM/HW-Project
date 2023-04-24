using HW.CustomerModels;
using HW.Http;
using HW.IdentityViewModels;
using HW.SupplierModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.UserManagmentModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.PackagesAndPaymentsViewModels;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminTradesmanService
    {
        Task<List<AdminTradesmanListVM>> SpGetTradesmanList(GenericUserVM genericUserVM);
        Task<SpTradesmanStats> SpGetTradesmanStats();
        Task<SpBusinessProfileVM> SpGetBusinessDetails(long userId, string role);
        Task<List<Tradesman>> GetLAllTradesman();
        Task<List<Tradesman>> GetAllActiveTradesman(bool isOrganisation);
        Task<List<Tradesman>> GetLAllTradesmanYearlyReport();
        Task<List<Tradesman>> GetLAllTradesmanFromToReport(DateTime StartDate, DateTime EndDate);
        Task<List<IdValueVM>> GetSkillsForDropDown();
        Task<List<Skill>> GetSkillList(long skillId);
        Task<List<SkillAndSubSkillVM>> GetSkillListAdmin();
        Task<List<SkillAndSubSkillVM>> GetSubSkillList();
        Task<List<TradesmanDTO>> GetLAllTradesmanbyCategoryReport(string StartDate, string EndDate, string skills, string tradesman, string city, bool lastActive, string location, string mobile, string cnic , string emailtype , string mobileType, string activityType, string userType);
        Task<List<GetInActiveUserVM>> getAllInActiveFromToReport(int pageNumber, int pageSize, string dataOrderBy,string fromDate, string toDate, string city, string selectedUser);
        Task<List<SecurityRoleItemVM>> GetSecurityRoleItem();
        Task<List<SecurityRoleVM>> GetSecurityRoles();
        Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId);
        Task<bool> AddSecurityRoleDetails(List<GetSecurityRoleDetailsVM> detailsVMs);
        Task<List<GetAdminUserDetails>> GetAdminUserDetails(int roleId);
        Task<bool> UpdateAdminUserdetails(GetAdminUserDetails detailsVMs);
        Task<bool> DeleteAdminUser(string userId);
        Task<List<TradesmanDTO>> GetTradesmanLast24HourReport(string startDate , string endDate );
        Task<List<Tradesman>> GetTradesmanAddressList();
        Task<Response> CheckSkillAvailability(string skillName , string subSkillName , int skillId, int orderBy);
        Task<Response> CheckOrderAvailability(TradesmanCommonVM tradesmanCommonVM);
        Task<Response> AddNewSkill(SkillAndSubSkillVM skill);
        Task<Response> AddOrUpdateSubSkill(SkillAndSubSkillVM subSkill);
        Task<Response> UpdateSkill(UpdateSkillVM skill);
        Task<Response> DeleteSkill(int skillId , int subSkillId);
        Task<List<TradesmanDTO>> TradesmanByCategory(TradesmanByCatVMcs tradesmanByCatVMcs);
        Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb(long tradsmanId);
        Task<bool> UpdatePersonalDetails(long id, PersonalDetailVM personalDetailVM);
        Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId);
        Task<List<PersonalDetailVM>> GetTradesmanByName(string tradesmanName,long tradesmanId,string tradesmanPhoneNo,long jobQuotationId);
        Task<Response> BlockTradesman(string tradesmanId, string userId, bool status);
        Task<List<InvoiceVM>> GetTradesmanPaymentReceipts(long tradesmanId);

    }

    public class AdminTradesmanService : IAdminTradesmanService
    {
        private readonly IHttpClientService httpClient;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public AdminTradesmanService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.Exc = Exc;
            this._apiConfig = apiConfig;
        }

        public async Task<List<AdminTradesmanListVM>> SpGetTradesmanList(GenericUserVM genericUserVM)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<AdminTradesmanListVM>>
                (await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SpGetTradesmanList}", genericUserVM)); 

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AdminTradesmanListVM>();
            }
        }

        public async Task<SpTradesmanStats> SpGetTradesmanStats()
        {
            try
            {
                return JsonConvert.DeserializeObject<SpTradesmanStats>
                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SpGetTradesmanStats}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpTradesmanStats();
            }
        }

        public async Task<SpBusinessProfileVM> SpGetBusinessDetails(long userId, string role)
        {
            try
            {
                return JsonConvert.DeserializeObject<SpBusinessProfileVM>
                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SpGetBusinessDetails}?userId={userId}&role={role}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpBusinessProfileVM();
            }
        }

        public async Task<List<Tradesman>> GetLAllTradesman()
        {
            return JsonConvert.DeserializeObject<List<Tradesman>>(
            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllTradesman}", ""));
        } 
        public async Task<List<Tradesman>> GetAllActiveTradesman(bool isOrganisation)
        {
            return JsonConvert.DeserializeObject<List<Tradesman>>(
            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllActiveTradesman}?isOrganisation={isOrganisation}", ""));
        }
        public async Task<List<TradesmanDTO>> GetTradesmanLast24HourReport(string startDate , string endDate)
        {
            return JsonConvert.DeserializeObject<List<TradesmanDTO>>(
            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllTradesmanLast24Hours}?StartDate={startDate}&EndDate={endDate}", ""));
        }

        public async Task<List<Tradesman>> GetLAllTradesmanYearlyReport()
        {
            return JsonConvert.DeserializeObject<List<Tradesman>>(
            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllTradesmanYearlyReport}", ""));
        }

        public async Task<List<Tradesman>> GetLAllTradesmanFromToReport(DateTime StartDate, DateTime EndDate)
        {
            return JsonConvert.DeserializeObject<List<Tradesman>>(
            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetInactiveUserReport}?StartDate={StartDate}&EndDate={EndDate}", ""));
        }

        public async Task<List<IdValueVM>> GetTradesmanSkills()
        {
            try
            {

                return JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkills}", ""));



            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }

        }

        public async Task<List<IdValueVM>> GetSkillsForDropDown()
        {
            try
            {

                return JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillsForDropdown}", ""));

            }


            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }

        }
        public async Task<List<Skill>> GetSkillList(long skillId)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Skill>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetAllSkills}?skillId={skillId}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Skill>();
            }

        }
        public async Task<List<SkillAndSubSkillVM>> GetSkillListAdmin()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SkillAndSubSkillVM>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillListAdmin}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SkillAndSubSkillVM>();
            }

        }
        public async Task<List<SkillAndSubSkillVM>> GetSubSkillList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<SkillAndSubSkillVM>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSubSkillList}", ""));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SkillAndSubSkillVM>();
            }

        }

        public async Task<List<TradesmanDTO>> GetLAllTradesmanbyCategoryReport(string StartDate, string EndDate, string skills, string tradesman, string city, bool lastActive, string location, string mobile, string cnic, string emailtype, string mobileType, string activityType, string userType)
        {
            return JsonConvert.DeserializeObject<List<TradesmanDTO>>
            (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetLAllTradesmanFromToReport}?StartDate={StartDate}&EndDate={EndDate}&skills={skills}&tradesman={tradesman}&city={city}&lastActive={lastActive}&location={location}&mobile={mobile}&cnic={cnic}&emailtype={emailtype}&mobileType={mobileType}&activityType={activityType}&userType={userType}", ""));
        }

        public async Task<List<GetInActiveUserVM>> getAllInActiveFromToReport(int pageNumber, int pageSize, string dataOrderBy,  string fromDate, string toDate, string city, string selectedUser)
        {


            return JsonConvert.DeserializeObject<List<GetInActiveUserVM>>
            (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetInactiveUserReport}?pageNumber={pageNumber}&pageSize={pageSize}&dataOrderBy={dataOrderBy}&fromDate={fromDate}&toDate={toDate}&city={city}&selectedUser={selectedUser}", ""));

            //switch (selectedDuration)
            //{
            //    case "1w":
            //        timeDuration = (int)Duration.week;
            //        break;
            //    case "2w":
            //        timeDuration = (int)Duration.twoWeek;
            //        break;
            //    case "3w":
            //        timeDuration = (int)Duration.threeWeek;
            //        break;
            //    case "4w":
            //        timeDuration = (int)Duration.fourWeek;
            //        break;
            //    case "2m":
            //        timeDuration = (int)Duration.twoMonth;
            //        break;
            //    case "4m":
            //        timeDuration = (int)Duration.fourMonth;
            //        break;
            //    case "8m":
            //        timeDuration = (int)Duration.eightMonth;
            //        break;
            //    case "1y":
            //        timeDuration = (int)Duration.Year;
            //        break;
            //    default:
            //        break;
            //}
            //List<GetInActiveUserVM> getInActiveUserVMs = new List<GetInActiveUserVM>();
            //List<InactiveUserVM> inactiveUserVMs = new List<InactiveUserVM>();
            //List<Customer> customerList = new List<Customer>();
            //List<Tradesman> tradesmenList = new List<Tradesman>();
            //List<Supplier> supplierList = new List<Supplier>();
            //var uu = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.getAllInActiveUser}?timeDuration={timeDuration}");
            //inactiveUserVMs = JsonConvert.DeserializeObject<List<InactiveUserVM>>(uu);
            //List<string> userId = inactiveUserVMs.Select(x => x.UserId).ToList();
            //switch (selectedUser)
            //{
            //    case "Customer":
            //        var aa = await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerReport}", userId);
            //        customerList = JsonConvert.DeserializeObject<List<Customer>>(aa);
            //        foreach (var item in customerList)
            //        {
            //            GetInActiveUserVM getInActive = new GetInActiveUserVM()
            //            {
            //                UserIds = item.CustomerId,
            //                FirstName = item.FirstName,
            //                LastName = item.LastName,
            //                //City = item.City,
            //                Area = string.Empty,
            //                BusinessAddress = string.Empty,
            //                MobileNumber = item.MobileNumber,
            //                LastActive = inactiveUserVMs.FirstOrDefault(c => c.UserId == item.UserId).LastActive
            //            };
            //            getInActiveUserVMs.Add(getInActive);
            //        }

            //        break;
            //    case "Tradesman":
            //        var bb = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanReport}", userId);
            //        tradesmenList = JsonConvert.DeserializeObject<List<Tradesman>>(bb);
            //        foreach (var item in tradesmenList)
            //        {
            //            GetInActiveUserVM getInActive = new GetInActiveUserVM()
            //            {
            //                UserIds = item.TradesmanId,
            //                FirstName = item.FirstName,
            //                LastName = item.LastName,
            //                City = item.City,
            //                Area = item.Area,
            //                BusinessAddress = item.ShopAddress,
            //                MobileNumber = item.MobileNumber,
            //                LastActive = inactiveUserVMs.FirstOrDefault(c => c.UserId == item.UserId).LastActive
            //            };
            //            getInActiveUserVMs.Add(getInActive);
            //        }
            //        break;
            //    case "Supplier":
            //        var cc = await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierReport}", userId);
            //        supplierList = JsonConvert.DeserializeObject<List<Supplier>>(cc);
            //        Exc.AddErrorLog(new Exception($"MyError{supplierList}"));
            //        List<string> city = new List<string>();
            //        foreach (var item in supplierList)
            //        {
            //            if(item.CityId != null )
            //            {
            //                //city = supplierList.Select(x => x.CityId.Value).ToList();
            //                city.Add(item.CityId.Value.ToString());
            //            }
            //            else
            //            {
            //                city.Add("");
            //            }
            //        }

            //        var zz = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityNameByCityId}","");
            //        List<City> cityList = JsonConvert.DeserializeObject<List<City>>(zz);
            //        GetInActiveUserVM getInActive2 = new GetInActiveUserVM();
            //        foreach (var item in supplierList)
            //        {
            //                GetInActiveUserVM getInActive = new GetInActiveUserVM()
            //                {
            //                    UserIds = item.SupplierId,
            //                    FirstName = item.FirstName,
            //                    LastName = item.LastName,
            //                    //City = cityList.FirstOrDefault(x => x.CityId == item.CityId).Name,
            //                    Area = string.Empty,
            //                    BusinessAddress = item.BusinessAddress,
            //                    MobileNumber = item.MobileNumber,
            //                    LastActive = inactiveUserVMs.FirstOrDefault(c => c.UserId == item.UserId).LastActive
            //                };
            //                if(item.CityId != null)
            //                {
            //                    getInActive2.City = item.CityId.Value.ToString();
            //                }
            //                else
            //                {
            //                    getInActive2.City = null;
            //                }
            //                getInActive.City = getInActive2.City;
            //                getInActiveUserVMs.Add(getInActive);
            //        }
            //        break;
            //    default:
            //        break;

            //}
            //return getInActiveUserVMs;
        }

        public async Task<List<SecurityRoleItemVM>> GetSecurityRoleItem()
        {
            var aa = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSecurityRoleItem}", "");
            List<SecurityRoleItemVM> securityRoleItemVMs = JsonConvert.DeserializeObject<List<SecurityRoleItemVM>>(aa);
            return securityRoleItemVMs;
        }

        public async Task<List<SecurityRoleVM>> GetSecurityRoles()
        {
            var a = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSecurityRoles}", "");
            List<SecurityRoleVM> securityRoleItemVMs = JsonConvert.DeserializeObject<List<SecurityRoleVM>>(a);
            return securityRoleItemVMs;
        }

        public async Task<List<GetSecurityRoleDetailsVM>> GetSecurityRoleDetails(int roleId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetSecurityRoleDetails}?roleId={roleId}", "");
            List<GetSecurityRoleDetailsVM> securityRoleItemVMs = JsonConvert.DeserializeObject<List<GetSecurityRoleDetailsVM>>(response);
            return securityRoleItemVMs;
        }

        public async Task<bool> AddSecurityRoleDetails(List<GetSecurityRoleDetailsVM> detailsVMs)
        {
            var c = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AddSecurityRoleDetails}", detailsVMs);
            bool res = JsonConvert.DeserializeObject<bool>(c);
            return res;
        }

        public async Task<List<GetAdminUserDetails>> GetAdminUserDetails(int roleId)
        {
            List<GetAdminUserDetails> getAdminUserDetails = new List<GetAdminUserDetails>();
            var c = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetAdminUserDetails}?roleId={roleId}");
             getAdminUserDetails = JsonConvert.DeserializeObject<List<GetAdminUserDetails>>(c);
            return getAdminUserDetails;
        }

        public async Task<bool> UpdateAdminUserdetails(GetAdminUserDetails detailsVMs)
        {
            var aa =await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateAdminUserdetails}",detailsVMs);
            bool result= JsonConvert.DeserializeObject<bool>(aa);
            return result; 
        }

        public async Task<bool> DeleteAdminUser(string userId)
        {
            var bb = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.DeleteAdminUser}?userId={userId}");
            bool res = JsonConvert.DeserializeObject<bool>(bb);
            return res;
        }
        public async Task<List<Tradesman>> GetTradesmanAddressList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanAddressList}", "");
            return JsonConvert.DeserializeObject<List<Tradesman>>(response);
        }

        public async Task<Response> CheckSkillAvailability(string skillName ,string subSkillName ,  int skillId , int orderBy)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.CheckSkillAvailability}?skillName={skillName}&subSkillName={subSkillName}&skillId={skillId}&orderBy={orderBy}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> CheckOrderAvailability(TradesmanCommonVM tradesmanCommonVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.CheckOrderAvailability}", tradesmanCommonVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddNewSkill(SkillAndSubSkillVM skill)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddNewSkill}", skill);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddOrUpdateSubSkill(SkillAndSubSkillVM subSkill)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddOrUpdateSubSkill}", subSkill);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> UpdateSkill(UpdateSkillVM skill)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdateSkill}", skill);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> DeleteSkill(int skillId , int subSkillId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.DeleteSkill}?skillId={skillId}&subSkillId={subSkillId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<List<TradesmanDTO>> TradesmanByCategory(TradesmanByCatVMcs tradesmanByCatVMcs)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.TradesmanByCategory}" , tradesmanByCatVMcs);
            return JsonConvert.DeserializeObject<List<TradesmanDTO>>(response);
        }

        public async Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb(long tradsmanId)
        {
            TradesManProfileDetailsVM tradesmanProfileDetailVM = new TradesManProfileDetailsVM();
            try
            {
                tradesmanProfileDetailVM.PersnalDetails = await GetPersonalDetails(tradsmanId);
                tradesmanProfileDetailVM.BusinessDetails = await GetBusinessDetail(tradsmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }


            return tradesmanProfileDetailVM;
        }
        public async Task<TmBusinessDetailVM> GetBusinessDetail(long tradsmanId)
        {
            try
            {
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradsmanId}", ""));
                List<long> skillIds = JsonConvert.DeserializeObject<List<long>>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillIds}?tradesmanId={tradesman.TradesmanId}"));
                long cityId = JsonConvert.DeserializeObject<long>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityIdByName}?cityName={tradesman.City}", ""));
                List<Skill> tradesmanSkills = JsonConvert.DeserializeObject<List<Skill>>(await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetSkillsByIds}", skillIds));
                List<IdValueVM> idValueVM = new List<IdValueVM>();
                idValueVM = tradesmanSkills.Select(x => new IdValueVM { Id = x.SkillId, Value = x.Name }).ToList();
                TmBusinessDetailVM businessDetailVM = new TmBusinessDetailVM()
                {
                    Town = tradesman.Area,
                    BusinessAddress = tradesman.ShopAddress,
                    SkillIds = skillIds,
                    CityId = cityId,
                    CompanyName = tradesman.CompanyName,
                    CompanyRegNo = tradesman.CompanyRegNo,
                    TravelingDistance = tradesman.TravellingDistance ?? 0,
                    TradesmanId = tradesman.TradesmanId,
                    tradesmanSkills = idValueVM,
                    LatLng = tradesman.GpsCoordinates,
                    AddressLine = tradesman.AddressLine,
                    IsOrganization = (bool)tradesman.IsOrganization

                };
                return businessDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TmBusinessDetailVM();
            }

        }
        public async Task<PersonalDetailsVM> GetPersonalDetails(long tradesmanId)
        {
            try
            {
                string tradesmanjson = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetPersonalDetails}?tradesmanId={tradesmanId}", "");
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(tradesmanjson);

                //TradesmanProfileImage tradesmanProfileImage = JsonConvert.DeserializeObject<TradesmanProfileImage>
                //    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageByTradesmanId}?tradesmanId={tradesmanId}", ""));

                PersonalDetailsVM personalDetailVM = new PersonalDetailsVM()
                {
                    EntityId = tradesman.TradesmanId,
                    FirstName = tradesman.FirstName,
                    LastName = tradesman.LastName,
                    Cnic = tradesman.Cnic,
                    Email = tradesman.EmailAddress,
                    Gender = (int)tradesman.Gender,
                    DateOfBirth = (DateTime)tradesman.Dob,
                    MobileNumber = tradesman.MobileNumber,
                    UserId = tradesman.UserId,
                    ProfileImage = null,
                };

                return personalDetailVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new PersonalDetailsVM();
            }

        }
        public async Task<bool> UpdatePersonalDetails(long id, PersonalDetailVM personalDetailVM)
        {
            try
            {
                    var response = new Response();
                    UserRegisterVM userVM = new UserRegisterVM()
                    {
                      Id = personalDetailVM.UserId,
                      PhoneNumber = personalDetailVM.MobileNumber,
                      Role = UserRoles.Tradesman,
                      Email = personalDetailVM.Email
                    };

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateUser}", userVM));

                  if(response.Status==ResponseStatus.OK)
                  {
                    Tradesman tradesmanEntity = new Tradesman()
                    {
                      TradesmanId = id,
                      LastName = personalDetailVM.LastName,
                      Dob = personalDetailVM.DateOfBirth,
                      FirstName = personalDetailVM.FirstName,
                      Cnic = personalDetailVM.Cnic,
                      EmailAddress = personalDetailVM.Email,
                      Gender = personalDetailVM.Gender,
                      MobileNumber = personalDetailVM.MobileNumber,
                      ModifiedOn = DateTime.Now
                    };
                    string resultJson = await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdatePersonalDetails}", tradesmanEntity);
                    return true;
                  }
               return false;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }
        public async Task<Response> AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddLinkedSalesman}?SalesmanId={SalesmanId}&CustomerId={CustomerId} ", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<PersonalDetailVM>> GetTradesmanByName(string tradesmanName,long tradesmanId,string tradesmanPhoneNo,long jobQuotationId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetTradesmanByName}?tradesmanName={tradesmanName}&tradesmanId={tradesmanId}&tradesmanPhoneNo={tradesmanPhoneNo}&jobQuotationId={jobQuotationId}", "");
            return JsonConvert.DeserializeObject<List<PersonalDetailVM>>(response);
        }

        public async Task<Response> BlockTradesman(string tradesmanId, string userId, bool status)
        {
            Response response = new Response();
            try
            {

                Response tradesmanResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.BlockTradesman}?tradesmanId={tradesmanId}&status={status}", ""));

                if (tradesmanResponse.Status == ResponseStatus.OK)
                {
                    Response identityResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.BlockUser}?userId={userId}&status={status}", ""));

                    if (identityResponse.Status == ResponseStatus.OK)
                    {
                        response.Message = "Tradesman status has been changed!";
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

        public async Task<List<InvoiceVM>> GetTradesmanPaymentReceipts(long tradesmanId)
        {
            try
            {
                List<TradesmanJobReceiptsVM> tradesmanJobReceipts = JsonConvert.DeserializeObject<List<TradesmanJobReceiptsVM>>
                (await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetJobReceiptsByTradesmanId}?tradesmanId={tradesmanId}", ""));

                List<InvoiceVM> Invoice_JobReceipts = new List<InvoiceVM>();
                Invoice_JobReceipts = tradesmanJobReceipts?.Select(m => new InvoiceVM
                {
                    JobQuotationId = m.JobQuotationId,
                    PaymentMonth = m.PaymentDate.ToString("dd MMM yyyy"),
                    Amount = m.Amount,
                    PaymentStatus = m.PaymentStatus ?? 0,
                    ServiceCharges = m.ServiceCharges ?? 0,
                    OtherCharges = m.OtherCharges ?? 0,
                    AdditionalCharges = m.AdditionalCharges ?? 0,
                    DiscountedAmount = m.DiscountedAmount ?? 0,
                    Commission = m.Commission ?? 0,
                    NetPayableToTradesman = m.NetPayableToTradesman ?? 0,
                    PayableAmount = m.PayableAmount ?? 0,
                    PaidViaWallet = m.PaidViaWallet ?? 0,
                    TradesmanInitialBudget = m.TradesmanInitialBudget

                }).ToList();

                return Invoice_JobReceipts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<InvoiceVM>();
            }

        }

    }
}
