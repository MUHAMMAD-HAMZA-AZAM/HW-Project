using FluentValidation.Results;
using Hw.EmailViewModel;
using HW.CommunicationModels;
using HW.CustomerModels;
using HW.EmailViewModel;
using HW.Http;
using HW.IdentityViewModels;
using HW.SupplierModels;
using HW.SupplierViewModels;
using HW.TradesmanModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminUserManagmentService
    {
        Task<string> AdminForgotPasswordAuthentication(string email, string role = "Admin");
        Task<Response> InsertAndUpDateFAQs(Faqs fAQs);
        Task<List<Faqs>> GetFAQsList();
        Task<Response> InsertAndUpDateActivePromotion(ActivePromotionVM activePromotionVM);
        Task<Response> AddUpdateToolTipForm(TooltipVM tooltipVM);
        Task<Response> AddUpdateToolTipFormDetails(TooltipVM tooltipVM);
        Task<Response> AddAndUpdateApplicationSetting(ApplicationSettingVM applicationSettingVM);
        Task<List<ApplicationSettingVM>> GetSettingList();
        Task<Response> AddAndUpdateApplicationSettingDetails(ApplicationSettingVM applicationSettingVM);
        Task<List<ApplicationSettingVM>> GetSettingDetailsList();
        Task<List<ActivePromotionVM>> GetPromotionList();
        Task<List<TooltipVM>> GetToolTipFormsList();
        Task<List<TooltipVM>>GetSingleFormDetails(long id);
        Task<List<TooltipVM>> GetToolTipFormsDetailsList();
        Task<List<CampaignVM>> GetCampaignsList();
        Task<List<Agreements>> GetAgreementsList();
        Task<Response> InsertAndUpDateAgreement(Agreements agreements);
        Task<Response> AddAndUpdateCampaigns(CampaignVM campaignVM);
        Task<Response> UpdatePersonalDetails(IdentityViewModels.PersonalDetailsVM model, string userRole);
        Task<Response> AddSupplierBusinessDetails(SupplierBusinessDetailVM model, long supplierId = 0);
        Task<Response> AddEditTradesmanWithSkills(TmBusinessDetailVM model);
        Task<Response> AddSalesman(SalesmanVM salesmanVM);
        Task<Response> UnlinkSalesman(SalesmanVM salesmanVM);
        Task<List<SalesmanVM>> GetAllSalesman();
        Task<List<SalesmanVM>> LinkedSalesManList(SalesmanVM salesmanVM);
        Task<List<TownListVM>> GetTownsByCityId(long cityId);
        Task<Response> CheckUserExist(string data);
        Task<Response> GetCompaignsTypesListForAdmin();
        Task<Response> GetTimonialsTypesListForAdmin();
 
        Task<Response> AddUpdateTestinomials(string data);
        Task<Response> GetTestimonialsListForAdmin();
        Task<Response> DeleteTesimoaialsStatus(string data);
 

    }
    public class AdminUserManagmentService : IAdminUserManagmentService
    {
        private readonly IHttpClientService httpClient;
        //private readonly ClientCredentials clientCred;
        //private readonly ICommunicationService communicationService; // sending job post confirmation email
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;
        public AdminUserManagmentService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.Exc = Exc;
            this._apiConfig = apiConfig;
        }

        public async Task<string> AdminForgotPasswordAuthentication(string email, string role = "Admin")
        {
            string userId = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.AdminForgotPasswordAuthentication}?email={email}&role={role}", "");
            if (!string.IsNullOrWhiteSpace(userId))
            {
                AdminForgotEmail adminForgotEmail = new AdminForgotEmail
                {
                    RecivereEmail = email,
                    UserId= userId,
                    Email = new Email
                    {
                        CreatedBy = "Admin Forgot password.",
                        Retries = 0,
                        IsSend = false
                    }
                };
                var result = httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.AdminForgotPasswordAuthenticationEmail}", adminForgotEmail);
                return "Email is Sent";
            }
            else
            {
                return "Can't Send Email.";
            }
        }

        public async Task<Response> InsertAndUpDateFAQs(Faqs fAQs)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.InsertAndUpDateFAQs}", fAQs);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<Faqs>> GetFAQsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetFAQsList}");
            return JsonConvert.DeserializeObject<List<Faqs>>(response);
        }
        public async Task<Response> InsertAndUpDateActivePromotion(ActivePromotionVM activePromotionVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.InsertAndUpDateActivePromotion}", activePromotionVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }        
        public async Task<Response> AddUpdateToolTipForm(TooltipVM tooltipVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddUpdateToolTipForm}", tooltipVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateToolTipFormDetails(TooltipVM tooltipVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddUpdateToolTipFormDetails}", tooltipVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }        
        public async Task<Response> AddAndUpdateApplicationSetting(ApplicationSettingVM applicationSettingVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddAndUpdateApplicationSetting}", applicationSettingVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<ApplicationSettingVM>> GetSettingList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSettingList}");
            return JsonConvert.DeserializeObject<List<ApplicationSettingVM>>(response);
        }           
        public async Task<Response> AddAndUpdateApplicationSettingDetails(ApplicationSettingVM applicationSettingVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddAndUpdateApplicationSettingDetails}", applicationSettingVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<ApplicationSettingVM>> GetSettingDetailsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSettingDetailsList}");
            return JsonConvert.DeserializeObject<List<ApplicationSettingVM>>(response);
        }        
        public async Task<List<ActivePromotionVM>> GetPromotionList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetPromotionList}");
            return JsonConvert.DeserializeObject<List<ActivePromotionVM>>(response);
        }
        public async Task<List<TooltipVM>> GetToolTipFormsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetToolTipFormsList}");
            return JsonConvert.DeserializeObject<List<TooltipVM>>(response);
        }        
        public async Task<List<TooltipVM>> GetSingleFormDetails(long id)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSingleFormDetails}?id={id}","");
            return JsonConvert.DeserializeObject<List<TooltipVM>>(response);
        }
        public async Task<List<TooltipVM>> GetToolTipFormsDetailsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetToolTipFormsDetailsList}");
            return JsonConvert.DeserializeObject<List<TooltipVM>>(response);
        }
        public async Task<List<Agreements>> GetAgreementsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAgreementsList}");
            return JsonConvert.DeserializeObject<List<Agreements>>(response);
        }        
        public async Task<List<CampaignVM>> GetCampaignsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCampaignsList}");
            return JsonConvert.DeserializeObject<List<CampaignVM>>(response);
        }
        public async Task<Response> InsertAndUpDateAgreement(Agreements agreements)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.InsertAndUpDateAgreement}", agreements);
            return JsonConvert.DeserializeObject<Response>(response);
        }        
        public async Task<Response> AddAndUpdateCampaigns(CampaignVM campaignVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddAndUpdateCampaigns}", campaignVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        
        public async Task<Response> AddSalesman(SalesmanVM salesmanVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddSalesman}", salesmanVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }        
        public async Task<Response> UnlinkSalesman(SalesmanVM salesmanVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.UnlinkSalesman}", salesmanVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<SalesmanVM>> GetAllSalesman()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllSalesman}");
            return JsonConvert.DeserializeObject<List<SalesmanVM>>(response);
        }        
        public async Task<List<SalesmanVM>> LinkedSalesManList(SalesmanVM salesmanVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.LinkedSalesManList}", salesmanVM);
            return JsonConvert.DeserializeObject<List<SalesmanVM>>(response);
        }
        public async Task<Response> UpdatePersonalDetails(IdentityViewModels.PersonalDetailsVM model, string userRole)
        {
            var response = new Response();
            try
            {
                List<ErrorModel> errors = new List<ErrorModel>();
                ValidationResult validationResult = new PdUpdateVmValidator().Validate(model);

                if (validationResult.IsValid)
                {
                    UserRegisterVM userVM = new UserRegisterVM()
                    {
                        Id = model.UserId,
                        PhoneNumber = model.MobileNumber,
                        Role = userRole,
                        Email = model.Email
                    };

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateUser}", userVM)
                    );

                    if (response.Status == ResponseStatus.OK)
                    {
                        if (userVM.Role == "Tradesman" || userVM.Role == "Organization")
                        {

                            Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>
                                (await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={model.EntityId}", ""));

                            if (tradesman != null)
                            {
                                byte gender = 0;
                                tradesman.MobileNumber = model.MobileNumber;
                                tradesman.EmailAddress = model.Email;
                                tradesman.ModifiedBy = model.UserId;
                                tradesman.ModifiedOn = DateTime.Now;
                                tradesman.Cnic = model.Cnic;
                                tradesman.EmailAddress = model.Email;
                                tradesman.Dob = model.DateOfBirth;
                                if (model.Gender == 0) gender = 0;
                                if (model.Gender == 1) gender = 1;
                                if (model.Gender == 3) gender = 3;
                                tradesman.Gender = gender;
                                response = JsonConvert.DeserializeObject<Response>(
                                    await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddEditTradesman}", tradesman)
                                );
                            }

                        }
                        else if (userVM.Role == "Customer")
                        {

                            Customer customer = JsonConvert.DeserializeObject<Customer>
                                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={model.EntityId}", ""));

                            if (customer != null)
                            {
                                byte gender = 0;

                                customer.FirstName = model.FirstName;
                                customer.LastName = model.LastName;
                                customer.EmailAddress = model.Email;
                                customer.ModifiedBy = model.UserId;
                                customer.ModifiedOn = DateTime.Now;
                                customer.Cnic = model.Cnic;
                                customer.EmailAddress = model.Email;
                                customer.State = model.Town;
                                customer.CityId = model.CityId;
                                customer.MobileNumber = model.MobileNumber;
                                customer.Dob = model.DateOfBirth;
                                if (model.Gender == 0) gender = 0;
                                if (model.Gender == 1) gender = 1;
                                if (model.Gender == 3) gender = 3;
                                customer.Gender = gender;
                                response = JsonConvert.DeserializeObject<Response>(
                                 await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddEditCustomer}", customer));
                            }
                        }

                        else if (userVM.Role == "Supplier")
                        {
                            Supplier supplier = JsonConvert.DeserializeObject<Supplier>
                                (await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierById}?supplierId={model.EntityId}", ""));

                            if (supplier != null)
                            {
                                byte gender = 0;
                                supplier.MobileNumber = model.MobileNumber;
                                supplier.FirstName = model.FirstName;
                                supplier.LastName = model.LastName;
                                supplier.EmailAddress = model.Email;
                                supplier.ModifiedBy = model.UserId;
                                supplier.ModifiedOn = DateTime.Now;
                                supplier.EmailAddress = model.Email;
                                supplier.Cnic = model.Cnic;
                                supplier.Dob = model.DateOfBirth;
                                if (model.Gender == 0) gender = 0;
                                if (model.Gender == 1) gender = 1;
                                if (model.Gender == 3) gender = 3;
                                supplier.Gender = gender;
                                response = JsonConvert.DeserializeObject<Response>(
                                    await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddEditSupplier}", supplier));
                            }
                        }

                        response.Status = ResponseStatus.OK;
                        response.Message = "Personal details have been successfully saved.";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Personal details could not be saved. Please try later.";
                    response.ResultData = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response> AddSupplierBusinessDetails(SupplierBusinessDetailVM model, long supplierId = 0)
        {

            Response response = new Response();
            try
            {
                model.SupplierId = model.SupplierId > 0 ? model.SupplierId : supplierId;

                Supplier existingData = JsonConvert.DeserializeObject<Supplier>(
                    await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetSupplierBySupplierId}?supplierId={model.SupplierId}", "")
                );

                existingData.CompanyName = model.CompanyName;
                existingData.RegistrationNumber = model.CompanyRegistrationNo;
                existingData.ProductCategoryId = model.PrimaryTradeId;
                existingData.PrimaryTrade = model.PrimaryTrade;
                existingData.DeliveryRadius = model.DeliveryRadius;
                existingData.CityId = model.CityId;
                existingData.BusinessAddress = model.BusinessAddress;
                existingData.GpsCoordinates = model.LocationCoordinates;
                existingData.State = model.Town;
                existingData.ModifiedBy = existingData.UserId;
                existingData.ModifiedOn = DateTime.Now;
                existingData.FeaturedSupplier = model.FeaturedSupplier;

                response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddEditSupplier}", existingData)
                );

                if (model.CityId > 0)
                {
                    City city = JsonConvert.DeserializeObject<City>
                        (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={model.CityId}"));

                    var supplierData = JsonConvert.DeserializeObject<Supplier>(response.ResultData.ToString());

                    var publicId = $"S{city.Code}{supplierData.SupplierId}";

                    var result = JsonConvert.DeserializeObject<bool>(
                        await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdateSupplierPublicId}?supplierId={supplierData.SupplierId}&publicId={publicId}")
                    );
                }

                if (response.Status == ResponseStatus.OK)
                {
                    List<SupplierSubCategory> supplierSubCategories = model.ProductIds.Select(p => new SupplierSubCategory
                    {
                        SupplierId = model.SupplierId,
                        ProductSubCategoryId = p,
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = existingData.UserId
                    }).ToList();

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.SetSupplierSubCategories}", supplierSubCategories)
                    );

                    #region Welcome Email
                    if (!string.IsNullOrWhiteSpace(model?.EmailAddress))
                    {
                        PostJobEmailVM postJobEmailVM = new PostJobEmailVM
                        {
                            name = model.CompanyName,
                            email_ = model?.EmailAddress,
                            Email = new Email
                            {
                                CreatedBy = existingData.UserId,
                                Retries = 0,
                                IsSend = false
                            }
                        };
                        var result = httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SupplierWelcomeEmail}", postJobEmailVM);
                    }

                    #endregion
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;


        }

        public async Task<Response> AddEditTradesmanWithSkills(TmBusinessDetailVM model)
        {
            var response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();
               // ValidationResult validationResult = model.IsOrganization ? new OrganizationVmValidator().Validate(model) : new TradesmanVmValidator().Validate(model);

                //if (validationResult.IsValid)
                //{
                    Tradesman existingData = JsonConvert.DeserializeObject<Tradesman>(
                        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetPersonalDetails}?tradesmanId={model.TradesmanId}", "")
                    );

                    existingData.Area = model.Town;
                    existingData.City = model.City;
                    existingData.ShopAddress = model.BusinessAddress;
                    existingData.IsOrganization = model.IsOrganization;
                    existingData.GpsCoordinates = model.LocationCoordinates;
                    existingData.AddressLine = model.AddressLine;
                    existingData.CompanyName = model?.CompanyName;
                    existingData.CompanyRegNo = model?.CompanyRegNo;
                    existingData.TravellingDistance = model?.TravelingDistance;

                    existingData.ModifiedBy = existingData.UserId;
                    existingData.ModifiedOn = DateTime.Now;

                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddEditTradesman}", existingData)
                    );


                    if (!string.IsNullOrWhiteSpace(model.City))
                    {
                        City city = JsonConvert.DeserializeObject<City>
                            (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityByName}?cityName={model.City}"));

                        var tradesmanData = JsonConvert.DeserializeObject<Tradesman>(response.ResultData.ToString());

                        var publicId = $"T{city.Code}{tradesmanData.TradesmanId}";

                        var result = JsonConvert.DeserializeObject<bool>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdateTradesmanPublicId}?tradesmanId={tradesmanData.TradesmanId}&publicId={publicId}")
                        );
                    }

                    if (response.Status == ResponseStatus.OK)
                    {
                        List<SkillSet> skillSetList = model.SkillIds.Select(skillId => new SkillSet
                        {
                            SkillId = skillId,
                            TradesmanId = model.TradesmanId,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = existingData.UserId
                        }).ToList();

                        response = JsonConvert.DeserializeObject<Response>(
                            await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SetTradesmanSkills}", skillSetList)
                        );

                        success = response.Status == ResponseStatus.OK ? true : false;
                    }
                //}
                //else
                //{
                //    errors = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                //}

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Business details have been successfully saved.";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Business details could not be saved. Please try later.";
                    response.ResultData = errors;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<List<TownListVM>> GetTownsByCityId(long cityId)
        {
            List<TownListVM> townLists = new List<TownListVM>();

            try
            {
                townLists = JsonConvert.DeserializeObject<List<TownListVM>>(
                    await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetTownsByCityId}?cityId={cityId}", "")
                );


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return townLists;
        }

        public async Task<Response> CheckUserExist(string data)
        {
            Response response = new Response();
            try
            {
                response = JsonConvert.DeserializeObject<Response>(
                         await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.CheckUserExist}", data)
                     );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> GetCompaignsTypesListForAdmin()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCompaignsTypesListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetTimonialsTypesListForAdmin()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetTimonialsTypesListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateTestinomials(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.AddUpdateTestinomials}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> GetTestimonialsListForAdmin()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetTestimonialsListForAdmin}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> DeleteTesimoaialsStatus(string data)
        {
            var response = await httpClient.PostAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.DeleteTesimoaialsStatus}", data);
            return JsonConvert.DeserializeObject<Response>(response);
        }
 
    }
}
