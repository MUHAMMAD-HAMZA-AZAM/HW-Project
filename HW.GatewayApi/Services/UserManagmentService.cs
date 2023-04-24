using FluentValidation.Results;
using Hw.EmailViewModel;
using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.CustomerModels;
using HW.EmailViewModel;
using HW.Http;
using HW.IdentityViewModels;
using HW.SupplierModels;
using HW.TradesmanModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.UserWebViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Response = HW.Utility.Response;

namespace HW.GatewayApi.Services
{
    public interface IUserManagementService
    {
        Task<PersonalDetailsVM> GetUserDetailsByUserRole(string userRole, string userId);
        Task<List<IdValueVM>> GetAllCities();
        Task<List<IdValueVM>> GetDistances();
        Task<SupplierViewModels.GetCitiesAndDistanceVM> GetDropDownOptionWeb();
        Task<Response> GetOtp(UserRegisterVM userVM, string userId);
        Task<Response> VerifyOtpAndRegisterUser(UserRegisterVM userVM);
        Task<Response> VerifyOtpAndGetPasswordResetToken(UserRegisterVM userVM);
        Task<Response> CreateRoleBasedEntity(PersonalDetailsVM model);
        Task<Response> UpdatePersonalDetails(PersonalDetailsVM model, string userRole);
        Task<Response> AddEditTradesmanWithSkills(TmBusinessDetailVM model);
        //Task<long> GetEntityIdByUserId(UserRegisterVM userVM);
        UserRegisterVM DecodeToken(string token);
        Task<Response> AddSupplierBusinessDetails(SupplierBusinessDetailVM model, long supplierId = 0);
        Task<Response> GetwebOtp(UserRegisterVM userVM, string userId);
        Task<Response> WebUserRegistration(WebRegistrationBasicVM webRegistrationBasicVM);
        Task<Response> VerifyOtp(string userId, string code, string phoneNumber, Role role, string email);
        Task<List<TownListVM>> GetTownsByCityId(long cityId);
        Task<List<ActivePromotionVM>> GetPromotionList();
        Task<List<ActivePromotionVM>> GetPromotionListForWeb();
        Task<List<ApplicationSettingVM>> GetSettingDetailsList();
        Task<List<ApplicationSettingVM>> GetSettingList();
        Task<List<ActivePromotionMobileVM>> GetPromotionListIdValue();
        Task<ImageVM> GetPromotionImageById(int promotionId);
         Task<bool> SendMessage(SmsVM smsVm);
        Task<Response> GetCityListByReleventState(int? stateId);
        Task<string> GetCampaignTypeList( int CampaignTypeId);
    }

    public class UserManagementService : IUserManagementService
    {
        private readonly IHttpClientService httpClient;
        private readonly ICommunicationService communicationService;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;
        private long ClientId;
        public UserManagementService(IHttpClientService httpClient, ICommunicationService communicationService, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.communicationService = communicationService;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<PersonalDetailsVM> GetUserDetailsByUserRole(string userRole, string userId)
        {
            try
            {
                var userDetails =
                JsonConvert.DeserializeObject<PersonalDetailsVM>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetUserDetailsByUserRole}?userRole={userRole}&userId={userId}", ""));
                return userDetails;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new PersonalDetailsVM();
            }

        }
        public async Task<List<IdValueVM>> GetAllCities()
        {
            try
            {
                List<IdValueVM> cityList =
                JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", ""));
                return cityList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }

        }

        public async Task<List<IdValueVM>> GetDistances()
        {
            try
            {
                List<IdValueVM> distanceList = JsonConvert.DeserializeObject<List<IdValueVM>>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetDistances}", ""));
                return distanceList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<SupplierViewModels.GetCitiesAndDistanceVM> GetDropDownOptionWeb()
        {
            SupplierViewModels.GetCitiesAndDistanceVM data = new SupplierViewModels.GetCitiesAndDistanceVM();
            try
            {
                data.ProductCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.GetAllProductCategory}", "")
                   ).Select(p => new IdValueVM { Id = p.ProductCategoryId, Value = p.Name }).ToList();

                data.Cities = await GetAllCities();
                data.Distances = await GetDistances();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierViewModels.GetCitiesAndDistanceVM();
            }
            return data;
        }

        public async Task<Response> GetOtp(UserRegisterVM userVM, string userId = "")
        {
            var response = new Response();
            try
            {
                bool emailProvided = !string.IsNullOrWhiteSpace(userVM.Email) ? true : false;
                bool mobileNumberProvided = !string.IsNullOrWhiteSpace(userVM.PhoneNumber) ? true : false;
                string responseMsg = string.Empty;


                if (!emailProvided && !mobileNumberProvided)
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Email or Phone number is required.";
                }
                else
                {
                    string emailOrPhone = emailProvided ? userVM.Email : userVM.PhoneNumber;
                    string phoneNumber;

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        var userNumber = await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetPhoneNumberByUserId}?userId={userId}");
                        //var aphoneNumber = JsonConvert.DeserializeObject<string>(aa);

                        if (string.IsNullOrEmpty(userNumber) || userVM.PhoneNumber == userNumber)
                        {
                            response = JsonConvert.DeserializeObject<Response>(
                                await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetOtp}?userId={emailOrPhone}&fromPersonalDetails={userVM.FromPersonalDetails}", "")
                            );

                            if (response.Status == ResponseStatus.OK)
                            {
                                if (mobileNumberProvided)
                                {
                                    var messageMobile = $"<#> Use {response.ResultData.ToString()} - as your Hoomwork verification code. {userVM.HashKey}";
                                    await communicationService.SendSmsAsync(new SmsVM()
                                    {
                                        MobileNumber = userVM.PhoneNumber,
                                        Message = messageMobile
                                    });
                                }

                                if (emailProvided)
                                {
                                    await communicationService.SendOtpEmail(new EmailOTPVM()
                                    {
                                        OtpCode = response.ResultData.ToString(),
                                        UserEmail = userVM.Email,
                                        Role = userVM.Role,
                                        Email = new Email
                                        {
                                            CreatedBy = "One Time Password Email.",
                                            Retries = 0,
                                            IsSend = false
                                        }
                                    });
                                }
                                if (mobileNumberProvided && emailProvided) //both Mobile number and Email are provided
                                {
                                    responseMsg = "Otp is sent to User via SMS and Email.";
                                }
                                else if (mobileNumberProvided && !emailProvided) //only mobile number provided
                                {
                                    responseMsg = "Otp is sent to User via SMS.";
                                }
                                else if (!mobileNumberProvided && emailProvided) //only email provided
                                {
                                    responseMsg = "Otp is sent to User via Email.";
                                }

                                response.Message = responseMsg;
                                response.ResultData = null;     //because we don't want to return OTP to the Client
                            }
                        }
                        else if (!string.IsNullOrEmpty(userVM.Email) && string.IsNullOrEmpty(userVM.PhoneNumber))
                        {
                            response = JsonConvert.DeserializeObject<Response>(
                               await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetOtp}?userId={emailOrPhone}", "")
                           );

                            if (response.Status == ResponseStatus.OK)
                            {
                                if (emailProvided)
                                {
                                    await communicationService.SendOtpEmail(new EmailOTPVM()
                                    {
                                        OtpCode = response.ResultData.ToString(),
                                        UserEmail = userVM.Email,
                                        Role = userVM.Role,
                                        Email = new Email
                                        {
                                            CreatedBy = "One Time Password Email.",
                                            Retries = 0,
                                            IsSend = false
                                        }
                                    });
                                }
                                if (emailProvided) //only email provided
                                {
                                    responseMsg = "Otp is sent to User via Email.";
                                }

                                response.Message = responseMsg;
                                response.ResultData = null;
                            }
                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "Invalid phone number. Phone number does not exit";
                            response.ResultData = null;     //because we don't want to return OTP to the Client
                        }
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<Response>(
                                await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetOtp}?userId={emailOrPhone}", "")
                            );

                        if (response.Status == ResponseStatus.OK)
                        {
                            if (mobileNumberProvided)
                            {
                                var messageMobile = $"<#> Use {response.ResultData.ToString()} - as your Hoomwork verification code. {userVM.HashKey}";
                                await communicationService.SendSmsAsync(new SmsVM()
                                {
                                    MobileNumber = userVM.PhoneNumber,
                                    Message = messageMobile
                                });
                            }

                            if (emailProvided)
                            {
                                await communicationService.SendOtpEmail(new EmailOTPVM()
                                {
                                    OtpCode = response.ResultData.ToString(),
                                    UserEmail = userVM.Email,
                                    Role = userVM.Role,
                                    Email = new Email
                                    {
                                        CreatedBy = "One Time Password Email.",
                                        Retries = 0,
                                        IsSend = false
                                    }
                                });
                            }

                            if (mobileNumberProvided && emailProvided) //both Mobile number and Email are provided
                            {
                                responseMsg = "Otp is sent to User via SMS and Email.";
                            }
                            else if (mobileNumberProvided && !emailProvided) //only mobile number provided
                            {
                                responseMsg = "Otp is sent to User via SMS.";
                            }
                            else if (!mobileNumberProvided && emailProvided) //only email provided
                            {
                                responseMsg = "Otp is sent to User via Email.";
                            }

                            response.Message = responseMsg;
                            response.ResultData = null;     //because we don't want to return OTP to the Client
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }


        public async Task<Response> VerifyOtpAndRegisterUser(UserRegisterVM userVM)
        {
            try
            {
                //var response = JsonConvert.DeserializeObject<Response>(
                //    await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.VerifyOtp}?userId={GetUserIdForOtp(userVM)}&code={userVM.VerificationCode}", ""));

                string result = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.RegisterUser}", userVM);
                Response response = JsonConvert.DeserializeObject<Response>(result);

                if (response?.Status == ResponseStatus.OK)
                {
                    var userIdJson = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData + "");
                    PersonalDetailsVM personalDetailsVM = new PersonalDetailsVM
                    {
                        MobileNumber = userVM.PhoneNumber,
                        Email = userVM.Email,
                        UserId = userIdJson.Id,
                        AccountType=userVM.SellerType,
                        ShopName=userVM.ShopName,
                         IsAllGoodStatus= userVM.IsAllGoodStatus
                    };
                    await CreateRoleBasedEntity(personalDetailsVM);
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public async Task<Response> VerifyOtp(string userId, string code, string phoneNumber, Role role, string email)
        {

            try
            {
                Response response = new Response();
                var userIds = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : email;

                response = JsonConvert.DeserializeObject<Response>(
                       await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.VerifyOtp}?userId={userIds}&code={code}", ""));

                if (response.Status == ResponseStatus.OK)
                {
                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.UpdateUsersPhoneNumberByUserId}?userId={userId}&phoneNumber={phoneNumber}&email={email}")
                    );

                    switch (role)
                    {
                        //     case Role.Tradesman:
                        //         JsonConvert.DeserializeObject<Response>(
                        //     await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdatePhoneNumberByUserId}?userId={userId}&phoneNumber={phoneNumber}")
                        // );
                        //         break;
                        //     case Role.Organization:
                        //         JsonConvert.DeserializeObject<Response>(
                        //    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdatePhoneNumberByUserId}?userId={userId}&phoneNumber={phoneNumber}")
                        //);
                        // break;
                        case Role.Customer:
                            JsonConvert.DeserializeObject<Response>(
                       await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.UpdatePhoneNumberByUserId}?userId={userId}&phoneNumber={phoneNumber}")
                   );
                            break;
                        case Role.Supplier:
                            JsonConvert.DeserializeObject<Response>(
                       await httpClient.GetAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.UpdatePhoneNumberByUserId}?userId={userId}&phoneNumber={phoneNumber}")
                   );
                            break;
                        case Role.Admin:
                            break;
                        default:
                            break;
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> VerifyOtpAndGetPasswordResetToken(UserRegisterVM userVM)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<Response>(
                await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.VerifyOtp}?userId={GetUserIdForOtp(userVM)}&code={userVM.VerificationCode}", "")
            );

                if (response.Status == ResponseStatus.OK)
                {
                    response = JsonConvert.DeserializeObject<Response>(
                        await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetPasswordResetToken}?userId={userVM.Id}", "")
                    );
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public async Task<Response> AddEditTradesmanWithSkills(TmBusinessDetailVM model)
        {
            var response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();
                ValidationResult validationResult = model.IsOrganization ? new OrganizationVmValidator().Validate(model) : new TradesmanVmValidator().Validate(model);
                if (validationResult.IsValid)
                {
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

                    //if (model.IsOrganization)
                    //{
                    //    existingData.CompanyName = model.CompanyName;
                    //    existingData.CompanyRegNo = model.CompanyRegNo;
                    //}
                    //else
                    //{
                    //    existingData.TravellingDistance = model.TravelingDistance;
                    //}

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
                        var publicId = $"ref{Guid.NewGuid().ToString("N").Substring(0, 9)}";
                       // var publicId = $"T{city.Code}{tradesmanData.TradesmanId}";

                        var result = JsonConvert.DeserializeObject<bool>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.UpdateTradesmanPublicId}?tradesmanId={tradesmanData.TradesmanId}&publicId={publicId}")
                        );
                    }

                    if (response.Status == ResponseStatus.OK)
                    {
                       
                        if (model.SkillIds.Count > 0)
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
                        }
                        else
                        {
                            List<SkillSet> skillSetList = new List<SkillSet>();
                            skillSetList.Add(new SkillSet() {
                                TradesmanId = model.TradesmanId,
                                IsActive = true,
                                CreatedOn = DateTime.Now,
                                CreatedBy = existingData.UserId
                            }); 

                            response = JsonConvert.DeserializeObject<Response>(
                            await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SetTradesmanSkills}", skillSetList)
                            );
                        }
                        

                        success = response.Status == ResponseStatus.OK ? true : false;
                    }
                }
                else
                {
                    errors = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                }

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

                response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddEditSupplier}", existingData)
                );

                if (model.CityId > 0)
                {
                    City city = JsonConvert.DeserializeObject<City>
                        (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={model.CityId}"));

                    var supplierData = JsonConvert.DeserializeObject<Supplier>(response.ResultData.ToString());
                    var publicId = $"ref{Guid.NewGuid().ToString("N").Substring(0, 9)}";
                    //var publicId = $"S{city.Code}{supplierData.SupplierId}";

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

        public async Task<Response> CreateRoleBasedEntity(PersonalDetailsVM model)
        {
            var response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();
                ValidationResult validationResult = new PersonalDetailsVmValidator().Validate(model);

                //if (validationResult.IsValid)
                //{
                UserRegisterVM userVM = new UserRegisterVM();
                if (model != null)
                {
                    response = JsonConvert.DeserializeObject<Response>(
                    await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={model.UserId}", "")
                );

                    if (response.Status == ResponseStatus.OK)
                    {
                        userVM = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());

                        if (userVM.Role == "Tradesman" || userVM.Role == "Organization")
                        {
                            bool isOrgnization = userVM.Role == "Tradesman" ? false : true;
                            Tradesman tradesman = new Tradesman()
                            {
                                UserId = model.UserId,
                                FirstName = model?.FirstName,
                                LastName = model?.LastName,
                                Cnic = model?.Cnic,
                                City=model.City,
                                Gender = Convert.ToByte(model?.Gender),
                                Dob = model.DateOfBirth == DateTime.MinValue ? DateTime.Now : model?.DateOfBirth,
                                MobileNumber = userVM?.PhoneNumber + "",
                                EmailAddress = userVM.Email,
                                CreatedOn = DateTime.Now,
                                CreatedBy = model.UserId,
                                IsOrganization = isOrgnization,
                            };

                            response = JsonConvert.DeserializeObject<Response>(
                                await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.AddEditTradesman}", tradesman)
                            );

                            if (response.Status == ResponseStatus.OK && !string.IsNullOrEmpty(tradesman.FirstName))
                            {
                                await communicationService.SendWellcomeEmailTradesman(new WellcomeEmailVM()
                                {
                                    UserEmail = tradesman.EmailAddress,
                                    UserName = $"{tradesman.FirstName} {tradesman.LastName}",
                                    Email = new Email
                                    {
                                        CreatedBy = tradesman.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                });
                            }
                        }
                        else if (userVM.Role == "Customer")
                        {
                             
                            if(model.DateOfBirth == null)
                            {
                                model.DateOfBirth = DateTime.MinValue;
                            }
                             Customer customer = new Customer()
                            {
                                UserId = model.UserId,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Cnic = model?.Cnic,
                                Gender = Convert.ToByte(model?.Gender ?? 0),
                                Dob = model.DateOfBirth == DateTime.MinValue ? Convert.ToDateTime("1/1/1900 12:00:00 AM") : model.DateOfBirth,
                                MobileNumber = userVM?.PhoneNumber,
                                EmailAddress = userVM.Email,
                                State = model?.Town,
                                CityId = model?.CityId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = model.UserId,
                                LatLong = model.LatLong
                            };

                            response = JsonConvert.DeserializeObject<Response>(
                                await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddEditCustomer}", customer)
                            );

                            //if (model.CityId > 0)
                            //{
                            //    City city = JsonConvert.DeserializeObject<City>
                            //        (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={model.CityId}"));

                            //    var customerData = JsonConvert.DeserializeObject<Customer>(response.ResultData.ToString());

                            //    var publicId = $"C{city.Code}{customerData.CustomerId}";

                            //    var result = JsonConvert.DeserializeObject<bool>(
                            //        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.UpdateCustomerPublicId}?customerId={customerData.CustomerId}&publicId={publicId}")
                            //    );
                            //}
                            //else
                            //{
                            if (!string.IsNullOrEmpty(response?.ResultData?.ToString()))
                            {
                                var customerData = JsonConvert.DeserializeObject<Customer>(response?.ResultData?.ToString());

                                var publicId = $"ref{Guid.NewGuid().ToString("N").Substring(0, 9)}";

                                var result = JsonConvert.DeserializeObject<bool>(
                                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.UpdateCustomerPublicId}?customerId={customerData.CustomerId}&publicId={publicId}")
                                );
                            }

                            //}
                                //await communicationService.SendWellcomeEmail(new WellcomeEmailVM()
                                //{
                                //    UserEmail = customer.EmailAddress,
                                //    UserName = $"{customer.FirstName} {customer.LastName}",
                                //    Email = new Email
                                //    {
                                //        CreatedBy = customer.UserId,
                                //        Retries = 0,
                                //        IsSend = false
                                //    }
                                //});
                            if (response.Status == ResponseStatus.OK && !string.IsNullOrEmpty(customer.FirstName))
                            {
                                await communicationService.SendWellcomeEmail(new WellcomeEmailVM()
                                {
                                    UserEmail = customer.EmailAddress,
                                    UserName = $"{customer.FirstName} {customer.LastName}",
                                    Email = new Email
                                    {
                                        CreatedBy = customer.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                });
                            }
                        }
                        else if (userVM.Role == "Supplier")
                        {
                            Supplier supplier = new Supplier()
                            {
                                UserId = model.UserId,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Cnic = model.Cnic,
                                Gender = Convert.ToByte(model.Gender),
                                Dob = model.DateOfBirth == DateTime.MinValue ? DateTime.Now : model.DateOfBirth,
                                MobileNumber = userVM.PhoneNumber,
                                EmailAddress = userVM.Email,
                                CityId= model.CityId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = model.UserId,
                                SupplierRole=model.AccountType,
                                ShopName=model.ShopName,
                                IsAllGoodStatus = model.IsAllGoodStatus,
                                PublicId = $"SI{Guid.NewGuid().ToString("N").Substring(0, 9)}"
                        };

                            response = JsonConvert.DeserializeObject<Response>(
                                await httpClient.PostAsync($"{_apiConfig.SupplierApiUrl}{ApiRoutes.Supplier.AddEditSupplier}", supplier)
                            );
                            if (response.Status == ResponseStatus.OK && !string.IsNullOrEmpty(model.ShopName))
                            {
                                PostJobEmailVM postJobEmailVM = new PostJobEmailVM
                                {
                                    name = model.ShopName,
                                    email_ = userVM.Email,
                                    Email = new Email
                                    {
                                        CreatedBy = model.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                };
                                var result = httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SupplierWelcomeEmail}", postJobEmailVM);
                            }

                        }

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
                        response.Message = "Personal details have been successfully saved.";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Personal details could not be saved. Please try later.";
                        response.ResultData = errors;
                    }

                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public UserRegisterVM DecodeToken(string token)
        {
            UserRegisterVM userVM = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(token) && token != "null")
                {
                    var handler = new JwtSecurityTokenHandler();
                    var tokenObj = handler.ReadToken(token) as JwtSecurityToken;
                    var payloadData = tokenObj.Payload;
                    userVM = new UserRegisterVM { Id = payloadData["UserId"] + "", FirebaseClientId = payloadData["FirebaseClientId"] + "" , Role = payloadData["Role"] + "", ClientId = Convert.ToInt64(payloadData["Id"] + "") };
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return userVM;
        }
        public async Task<List<ActivePromotionVM>> GetPromotionList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetActivePromotionList}");
            return JsonConvert.DeserializeObject<List<ActivePromotionVM>>(response);
        }
        public async Task<List<ActivePromotionVM>> GetPromotionListForWeb()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetPromotionListForWeb}");
            return JsonConvert.DeserializeObject<List<ActivePromotionVM>>(response);
        }
        public async Task<List<ApplicationSettingVM>> GetSettingList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSettingList}");
            return JsonConvert.DeserializeObject<List<ApplicationSettingVM>>(response);
        }
        public async Task<List<ApplicationSettingVM>> GetSettingDetailsList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetSettingDetailsList}");
            return JsonConvert.DeserializeObject<List<ApplicationSettingVM>>(response);
        }
        #region Helpers
        private string GetUserIdForOtp(UserRegisterVM userVM)
        {
            string userId = string.Empty;
            try
            {
                bool emailProvided = !string.IsNullOrWhiteSpace(userVM.Email) ? true : false;
                bool mobileNumberProvided = !string.IsNullOrWhiteSpace(userVM.PhoneNumber) ? true : false;

                if (!emailProvided && !mobileNumberProvided)
                {
                    userId = null;
                }
                else
                {
                    userId = emailProvided ? userVM.Email : userVM.PhoneNumber;
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return userId;
        }

        public async Task<Response> UpdatePersonalDetails(PersonalDetailsVM model, string userRole)
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
                        Email = model.Email,

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
                                tradesman.MobileNumber = model.MobileNumber;
                                tradesman.EmailAddress = model.Email;
                                tradesman.ModifiedBy = model.UserId;
                                tradesman.ModifiedOn = DateTime.Now;
                                tradesman.Cnic = model.Cnic;
                                tradesman.EmailAddress = model.Email;
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
                                customer.FirstName = model.FirstName;
                                customer.LastName = model.LastName;
                                customer.ModifiedBy = model.UserId;
                                customer.ModifiedOn = DateTime.Now;
                                customer.Cnic = model.Cnic;
                                customer.Gender = Convert.ToByte(model.Gender);
                                customer.MobileNumber = model.MobileNumber;
                                customer.Dob = model.DateOfBirth;
                                customer.EmailAddress = model.Email;
                                customer.State = model.Town;
                                customer.CityId = model.CityId;
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
                                //supplier.MobileNumber = model.MobileNumber;
                                supplier.FirstName = model.FirstName;
                                supplier.LastName = model.LastName;
                                supplier.EmailAddress = model.Email;
                                supplier.ModifiedBy = model.UserId;
                                supplier.ModifiedOn = DateTime.Now;
                                supplier.EmailAddress = model.Email;
                                supplier.Cnic = model.Cnic;
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

        public async Task<Response> WebUserRegistration(WebRegistrationBasicVM webRegistrationBasicVM)
        {
            Response response = new Response();
            try
            {
                UserRegisterVM userVM = new UserRegisterVM
                {
                    Email =string.IsNullOrEmpty(webRegistrationBasicVM.EmailAddress)?webRegistrationBasicVM.Email:webRegistrationBasicVM.EmailAddress,
                    Password = webRegistrationBasicVM.Password,
                    ConfirmPassword = webRegistrationBasicVM.Password,
                    PhoneNumber = webRegistrationBasicVM.PhoneNumber,
                    Role = webRegistrationBasicVM.Role,
                    SellerType=webRegistrationBasicVM.SellerType,
                    AccountType=webRegistrationBasicVM.AccountType,
                    VerificationCode = webRegistrationBasicVM.VerificationCode,
                    FacebookUserId = webRegistrationBasicVM.FacebookUserId,
                    GoogleUserId = webRegistrationBasicVM.GoogleUserId,
                    TermsAndConditions = webRegistrationBasicVM.TermsAndConditions,
                    UserName = webRegistrationBasicVM.UserName,
                    ShopName=webRegistrationBasicVM.ShopName,
                    IsAllGoodStatus = webRegistrationBasicVM.IsAllGoodStatus
                };
                response = await VerifyOtpAndRegisterUser(userVM);

                //Note => this was an extra request , if throw any exception while saving supplier details uncomment this parts

                if (response.Status == ResponseStatus.OK)
                {
                    var obj = JsonConvert.DeserializeObject<UserRegisterVM>(response.ResultData.ToString());
                    PersonalDetailsVM personalDetailsVM = new PersonalDetailsVM();
                    {
                        personalDetailsVM.UserId = obj.Id;
                        personalDetailsVM.Cnic = webRegistrationBasicVM.CNIC;
                        personalDetailsVM.DateOfBirth = webRegistrationBasicVM?.DateOfBirth;
                        personalDetailsVM.Email = webRegistrationBasicVM.EmailAddress;
                        personalDetailsVM.FirstName = webRegistrationBasicVM.FirstName;
                        personalDetailsVM.LastName = webRegistrationBasicVM.LastName;
                        if (webRegistrationBasicVM.Role == "Tradesman" || webRegistrationBasicVM.Role == "Organization")
                        {
                            personalDetailsVM.City = webRegistrationBasicVM.City;
                        }
                        else
                        {
                            personalDetailsVM.CityId = webRegistrationBasicVM.City != "" ? Convert.ToInt64(webRegistrationBasicVM.City) : 0;
                            //personalDetailsVM.City = webRegistrationBasicVM.City;
                        }

                        if (webRegistrationBasicVM.Gender == "Male")
                        {
                            personalDetailsVM.Gender = 1;
                        }
                        else if (webRegistrationBasicVM.Gender == "Female")
                        {
                            personalDetailsVM.Gender = 2;
                        }
                        else if (webRegistrationBasicVM.Gender == "Other")
                        {
                            personalDetailsVM.Gender = 3;
                        }

                        personalDetailsVM.MobileNumber = webRegistrationBasicVM.PhoneNumber;
                    };
                    var testDetails = personalDetailsVM;

                    response = await CreateRoleBasedEntity(personalDetailsVM);

                }
                if (response.Status == ResponseStatus.OK)
                {
                    response.Message = "Data is successfully Inserted.";
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> GetwebOtp(UserRegisterVM userVM, string userId)
        {
            var response = new Response();
            try
            {
                bool emailProvided = !string.IsNullOrWhiteSpace(userVM.Email) ? true : false;
                bool mobileNumberProvided = !string.IsNullOrWhiteSpace(userVM.PhoneNumber) ? true : false;
                string responseMsg = string.Empty;


                if (!emailProvided && !mobileNumberProvided)
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Email or Phone number is required.";
                }
                else
                {
                    string emailOrPhone = emailProvided ? userVM.Email : userVM.PhoneNumber;


                    response = JsonConvert.DeserializeObject<Response>(
                            await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetOtp}?userId={emailOrPhone}", "")
                        );

                    if (response.Status == ResponseStatus.OK)
                    {
                        if (mobileNumberProvided)
                        {
                            var messageMobile = $"Use {response.ResultData.ToString()} as your Hoomwork verification code.";
                            await communicationService.SendSmsAsync(new SmsVM()
                            {
                                MobileNumber = userVM.PhoneNumber,
                                Message = messageMobile
                            });
                        }

                        if (emailProvided)
                        {
                            await communicationService.SendOtpEmail(new EmailOTPVM()
                            {
                                OtpCode = response.ResultData.ToString(),
                                UserEmail = userVM.Email,
                                Role = userVM.Role,
                                Email = new Email
                                {
                                    CreatedBy = "One Time Password Email.",
                                    Retries = 0,
                                    IsSend = false
                                }
                            });
                        }

                        if (mobileNumberProvided && emailProvided) //both Mobile number and Email are provided
                        {
                            responseMsg = "Otp is sent to User via SMS and Email.";
                        }
                        else if (mobileNumberProvided && !emailProvided) //only mobile number provided
                        {
                            responseMsg = "Otp is sent to User via SMS.";
                        }
                        else if (!mobileNumberProvided && emailProvided) //only email provided
                        {
                            responseMsg = "Otp is sent to User via Email.";
                        }

                        response.Message = responseMsg;
                        response.ResultData = null;     //because we don't want to return OTP to the Client
                    }
                }
            }

            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }


        #endregion
        public async Task<List<ActivePromotionMobileVM>> GetPromotionListIdValue()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetPromotionListIdValue}");
            return JsonConvert.DeserializeObject<List<ActivePromotionMobileVM>>(response);
        }

        public async Task<ImageVM> GetPromotionImageById(int promotionId)
        {
            try
            {
                ActivePromotionMobileVM promotionImage = JsonConvert.DeserializeObject<ActivePromotionMobileVM>
                       (await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetPromotionImageById}?promotionId={promotionId}", ""));

                UserViewModels.ImageVM imageVM = new UserViewModels.ImageVM()
                {
                    Id = promotionImage.PromotionId,
                    ImageContent = promotionImage?.Image
                };


                return imageVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new UserViewModels.ImageVM();
            }
        }

        public async Task<bool> SendMessage(SmsVM smsVm)
        {
          return  await communicationService.SendSmsAsync(new SmsVM(){
                            MobileNumber = smsVm.MobileNumber,
                            Message = smsVm.Message
                        });
        }



        public async Task<Response> GetCityListByReleventState(int? stateId)
        {
                var response = await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityListByReleventState}?stateId={stateId}");
              return JsonConvert.DeserializeObject<Response>(response);
            
        }

        public async Task<string> GetCampaignTypeList(int CampaignTypeId)
        {
            return await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCompaignsTypeList}?CampaignTypeId={CampaignTypeId}", "");
        }
    }
}
