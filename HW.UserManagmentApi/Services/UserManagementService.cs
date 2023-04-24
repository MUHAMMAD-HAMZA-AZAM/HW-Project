using HW.ReportsViewModels;
using HW.SupplierViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.IdentityViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HW.SupplierModels.DTOs;

namespace HW.UserManagementApi.Services
{
    public interface IUserManagementService
    {
        IdentityViewModels.PersonalDetailsVM GetUserDetailsByUserRole(string userRole, string userId);
        List<City> GetCityList();
        Task<Response> GetCityListWithTraxCityId();
        City GetCityById(long cityId);
        List<IdValueVM> GetAllCities();
        List<CityAndStateVM> GetCitiesList();
        List<State> GetStatesList();
        List<IdValueVM> GetDistances();
        Task<Response> GetOtp(string mobileNo, bool fromPersonalDetails);
        Task<Response> VerifyOtp(string userId, string code);
        long GetCityIdByName(string cityName);
        long GetDistanceBuName(string travellingDistance);
        List<City> GetCityNameByCityId();
        City GetCityByName(string cityName);
        City CheckCityAvailability(string cityName);
        Response AddNewCity(City city);
        Response AddUpdateTown(TownVM town);
        Response UpdateCity(City city);
        Response DeleteCity(string cityId);
        Response InsertAndUpDateFAQs(Faqs fAQs);
        List<Faqs> GetFAQsList();
        Response InsertAndUpDateActivePromotion(ActivePromotionVM activePromotionVM);
        Response AddUpdateToolTipForm(TooltipVM tooltipVM);
        Response AddUpdateToolTipFormDetails(TooltipVM tooltipVM);
        Response AddAndUpdateApplicationSetting(ApplicationSettingVM applicationSettingVM);
        List<ApplicationSettingVM> GetSettingList();        
        Response AddAndUpdateApplicationSettingDetails(ApplicationSettingVM applicationSettingVM);
        Response UnlinkSalesman(SalesmanVM salesmanVM);
        List<ApplicationSettingVM> GetSettingDetailsList();
        List<ActivePromotionVM> GetPromotionList();
        List<ActivePromotionVM> GetPromotionListForWeb();
        List<ActivePromotionVM> GetActivePromotionList();
        Response InsertAndUpDateAgreement(Agreements agreements);
        Response AddAndUpdateCampaigns(CampaignVM campaignVM);
        List<Agreements> GetAgreementsList();
        List<CampaignVM> GetCampaignsList();
        List<Town> GetTownsByCityId(long cityId);
        List<TownVM> GetAllTown();
        Response AddUpdateSalesman(SalesmanVM salesmanVM);
        List<SalesmanVM> GetAllSalesman();
        List<SalesmanVM> LinkedSalesManList(SalesmanVM salesmanVM);
        List<TooltipVM> GetToolTipFormsList();
        List<TooltipVM> GetToolTipFormsDetailsList();
        List<TooltipVM> GetSingleFormDetails(long id);
        List<ActivePromotionMobileVM> GetPromotionListIdValue();
        ActivePromotionMobileVM GetPromotionImageById(int promotionId);
        Response CheckUserExist(string data);
        Task<Response> AddUpdateState(string data);
        Task<Response> GetStateListForAdmin();
        Task<Response> deletestateStatus(string data);
        Task<Response> GetSateList(int? countryId);
        Task<Response> GetCityListByReleventState(int? stateId);

        Task<Response> GetCompaignsTypeList(int CampaignTypeId);
        Task<Response> GetCompaignsTypesListForAdmin();
        Task<Response> GetTimonialsTypesListForAdmin();
       
 Task<Response> AddUpdateTestinomials(string data);
        Task<Response> GetTestimonialsListForAdmin();
        Task<Response> DeleteTesimoaialsStatus(string data);



    }

    public class UserManagementService : IUserManagementService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public UserManagementService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public City GetCityById(long cityId)
        {
            try
            {
                var city = uow.Repository<City>().Get().Where(x => x.CityId == cityId).FirstOrDefault();
                return city;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new City();
            }

        }

        public List<IdValueVM> GetAllCities()
        {
            try
            {
                //List<IdValueVM> cc = uow.Repository<City>().GetAll().Where(c => c.IsActive == true).Select(x => new IdValueVM { Id = x.CityId, Value = x.Name }).ToList();
                return uow.Repository<City>().GetAll().Where(c => c.IsActive == true).Select(x => new IdValueVM { Id = x.CityId, Value = x.Name }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public List<IdValueVM> GetDistances()
        {
            try
            {
                return uow.Repository<Distance>().GetAll().Select(x => new IdValueVM { Id = x.Value, Value = x.Unit.ToString() }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<Response> GetOtp(string mobileNo, bool fromPersonalDetails)
        {
            Response response = new Response();
            try
            {
                IRepository<OneTimePassword> otpRepo = uow.Repository<OneTimePassword>();
                OneTimePassword userOtp = new OneTimePassword();


                //Check if user has already requested a code within last 5 mins and this code is not verified yet
                userOtp = otpRepo.GetAll()
                        .OrderByDescending(otp => otp.OtpId)
                        .FirstOrDefault(otp => otp.UserId == mobileNo && otp.CreatedOn >= DateTime.UtcNow.AddMinutes(-1) && otp.IsVerified == false);


                if (userOtp == null || fromPersonalDetails == true)
                {
                    byte[] key = KeyGeneration.GenerateRandomKey(20);
                    string base32String = Base32Encoding.ToString(key);
                    byte[] secretKey = Base32Encoding.ToBytes(base32String);

                    userOtp = new OneTimePassword()
                    {
                        UserId = mobileNo,
                        SecretKey = secretKey,
                        RequestCount = 0,
                        AttemptCount = 0,
                        IsVerified = false,
                        CreatedOn = DateTime.UtcNow
                    };

                    if (userOtp?.TimeWindowUsed <= 0)
                    {
                        userOtp.TimeWindowUsed = DateTime.Now.Ticks / 2;
                    }

                    await otpRepo.AddAsync(userOtp);
                }
                else
                {
                    //if the code has been sent to user less than 3 times within last hour, increment the RequestCount
                    if (userOtp.RequestCount < 3)
                    {
                        userOtp.RequestCount = userOtp.RequestCount + 1;
                        otpRepo.Update(userOtp);
                    }
                }
                await uow.SaveAsync();


                //if the code has been sent to user less than 3 times within last hour, send code again
                if (userOtp.RequestCount < 3)
                {
                    Totp totp = new Totp(
                        secretKey: userOtp.SecretKey,
                        step: 600,                      //verification window in seconds
                        mode: OtpHashMode.Sha512,
                        totpSize: 6
                    );

                    string totpCode = totp.ComputeTotp();

                    response.Status = ResponseStatus.OK;
                    response.ResultData = totpCode;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Your account has been temporarily blocked due to suspicious activity. Please try again later after ten minutes.";
                }


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> VerifyOtp(string userId, string code)
        {
            Response response = new Response();

            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Both UserId and Code are required parameters.";
                }
                else
                {
                    bool verificationResult = false;

                    

                    IRepository <OneTimePassword> otpRepo = uow.Repository<OneTimePassword>();

                    //Check if user has already requested or tried a code within last 60 mins and is not verified yet
                    OneTimePassword userOtp = otpRepo.GetAll()
                            .OrderByDescending(otp => otp.OtpId)
                            //.FirstOrDefault(otp => otp.UserId == userId  && otp.IsVerified == false);
                            .FirstOrDefault(otp => otp.UserId == userId && otp.CreatedOn >= DateTime.UtcNow.AddMinutes(-1) && otp.IsVerified == false);

                    if (userOtp != null)
                    {
                        //if the user has made less than 3 retries, verify the code
                        if (userOtp.AttemptCount < 3)
                        {
                            Totp totp = new Totp(
                                secretKey: userOtp.SecretKey,
                                step: 600,                      //verification window in seconds
                                mode: OtpHashMode.Sha512,
                                totpSize: 6
                            );

                            verificationResult = totp.VerifyTotp(code, out long timeWindowUsed, VerificationWindow.RfcSpecifiedNetworkDelay);

                            userOtp.TimeWindowUsed = timeWindowUsed;
                            userOtp.AttemptCount = userOtp.AttemptCount + 1;
                            userOtp.IsVerified = verificationResult;

                            otpRepo.Update(userOtp);
                            await uow.SaveAsync();

                            response.Status = verificationResult ? ResponseStatus.OK : ResponseStatus.Error;
                            response.Message = verificationResult ? "Verified." : "Invalid code!";
                            response.ResultData = verificationResult;
                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "Your account has been temporarily blocked due to suspicious activity. Please try again later after few minutes.";
                        }
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "The provided code is either invalid or is already used.";
                    }
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

        public IdentityViewModels.PersonalDetailsVM GetUserDetailsByUserRole(string userRole, string userId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@userRole" , userRole),
                    new SqlParameter("@userId" , userId),
                };
                 
                var userData = uow.ExecuteReaderSingleDS<IdentityViewModels.PersonalDetailsVM>("SP_GetUserDetailsByUserRole" , sqlParameters).FirstOrDefault();
                return userData;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new IdentityViewModels.PersonalDetailsVM();
            }
        }
        public List<City> GetCityList()
        {
            try
            {
                return uow.Repository<City>().Get(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<City>();
            }
        }        
        public async Task<Response> GetCityListWithTraxCityId()
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = { };

                response.ResultData = await uow.ExecuteReaderSingleDSNew<CityListVM>("SP_GetCityListWithTraxCityId", sqlParameters);
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        public long GetCityIdByName(string cityName)
        {
            try
            {
                return uow.Repository<City>().GetAll().FirstOrDefault(x => x.Name == cityName && x.IsActive == true)?.CityId ?? 0;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public long GetDistanceBuName(string travellingDistance)
        {
            try
            {
                Distance query = uow.Repository<Distance>().GetAll().Where(x => x.Value.ToString() == travellingDistance).FirstOrDefault();
                return query.DistanceId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }

        }

        public List<City> GetCityNameByCityId()
        {
            List<City> cityName = new List<City>();
            try
            {
                cityName = uow.Repository<City>().GetAll().Where(x => x.IsActive == true).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return cityName;
        }

        public City GetCityByName(string cityName)
        {
            try
            {
                return uow.Repository<City>().GetAll().FirstOrDefault(x => x.Name == cityName && x.IsActive == true);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new City();
            }
        }

        public City CheckCityAvailability(string cityName)
        {
            try
            {
                //var issCity = uow.Repository<City>().GetAll();
                City isCity = uow.Repository<City>().Get().Where(x => x.Name.ToLower() == cityName.ToLower()).FirstOrDefault();
                return isCity;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new City();
            }

        }

        public List<CityAndStateVM> GetCitiesList()
        {
            List<CityAndStateVM> cityList;
            try
            {

                //cityList =  uow.Repository<City>().GetAll().OrderByDescending(x => x.CityId).ToList();
                SqlParameter[] sqlParameters = { };
                cityList = uow.ExecuteReaderSingleDS<CityAndStateVM>("SP_GetCityList", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CityAndStateVM>();
            }
            return cityList;
        }

        public List<State> GetStatesList()
        {
            try
            {
                return uow.Repository<State>().GetAll().OrderByDescending(x => x.StateId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<State>();
            }
        }

        public Response AddNewCity(City city)
        {
            Response response = new Response();
            try
            {
                city.Code = city.Name + city.StateId;
                city.CreatedOn = DateTime.Now;
                uow.Repository<City>().Add(city);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "City added successfully";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public Response UpdateCity(City city)
        {
            Response response = new Response();
            try
            {
                City isCity = uow.Repository<City>().Get(x => x.CityId == city.CityId).FirstOrDefault();
                isCity.Name = city.Name;
                isCity.StateId = city.StateId;
                isCity.ModifiedOn = DateTime.Now;
                isCity.Code = city.Name + city.StateId;
                isCity.ModifiedBy = city.ModifiedBy;
                uow.Repository<City>().Update(isCity);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "City updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public Response DeleteCity(string cityId)
        {
            Response response = new Response();
            try
            {
                long Id = Convert.ToInt64(cityId);
                City isCity = uow.Repository<City>().Get(x => x.CityId == Id).FirstOrDefault();
                if (isCity.IsActive == true)
                    isCity.IsActive = false;
                else
                    isCity.IsActive = true;
                uow.Repository<City>().Update(isCity);
                uow.Save();
                response.Status = ResponseStatus.OK;
                response.Message = "City deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }

        public Response InsertAndUpDateFAQs(Faqs fAQs)
        {
            Response response = new Response();
            try
            {
                if (fAQs.FaqId <= 0)
                {
                    fAQs.CreatedOn = DateTime.Now;
                }
                if (fAQs.FaqId > 0)
                {
                    fAQs.ModifiedOn = DateTime.Now;
                }
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@FaqId",fAQs.FaqId),
                    new SqlParameter("@Header",fAQs.Header),
                    new SqlParameter("@FAQsText",fAQs.FaqsText),
                    new SqlParameter("@IsActive",fAQs.IsActive),
                    new SqlParameter("@CreatedBy",fAQs.CreatedBy),
                    new SqlParameter("@CreatedOn",fAQs.CreatedOn),
                    new SqlParameter("@ModifiedBy",fAQs.ModifiedBy),
                    new SqlParameter("@ModifiedOn",fAQs.ModifiedOn),
                    new SqlParameter("@OrderByColumn",fAQs.OrderByColumn),
                    new SqlParameter("@UserType",fAQs.UserType),
                };
                var res = uow.ExecuteReaderSingleDS<Response>("Sp_InsetUpdateFAQs", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = res.Select(x => x.Message).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response InsertAndUpDateActivePromotion(ActivePromotionVM activePromotionVM)
        {
            Response response = new Response();
            try
            {
                ActivePromotion activePromotion = new ActivePromotion();
                if (activePromotionVM.PromotionId <= 0 && activePromotionVM.Action == "add")
                {
                    if (activePromotionVM.Base64Image != null)
                    {
                        var splitName = activePromotionVM.Base64Image.Split(',');
                        string convert = activePromotionVM.Base64Image.Replace(splitName[0] + ",", String.Empty);
                        activePromotion.Image = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    if (activePromotionVM.Base64Mobile != null)
                    {
                        var split = activePromotionVM.Base64Mobile.Split(',');
                        string convert = activePromotionVM.Base64Mobile.Replace(split[0] + ",", String.Empty);
                        activePromotion.ImageMobile = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    activePromotion.Name = activePromotionVM.Name;
                    activePromotion.CreatedOn = DateTime.Now;
                    activePromotion.CreatedBy = activePromotionVM.CreatedBy;
                    activePromotion.Description = activePromotionVM.Description;
                    activePromotion.SkillId = activePromotionVM.SkillId;
                    activePromotion.IsAcitve = activePromotionVM.IsAcitve;
                    activePromotion.IsMain = activePromotionVM.IsMain;
                    activePromotion.UserRoleId = activePromotionVM.UserRoleId;
                    activePromotion.PromotionStartDate = activePromotionVM.PromotionStartDate;
                    activePromotion.PromotionEndDate = activePromotionVM.PromotionEndDate;
                    activePromotion.SubSkillIds = activePromotionVM.SubSkillIds;
                    activePromotion.CampaignTypeId = activePromotionVM.CampaignTypeId;
                    
                    uow.Repository<ActivePromotion>().Add(activePromotion);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (activePromotionVM.PromotionId > 0 && activePromotionVM.Action =="update")
                {
                    activePromotion = uow.Repository<ActivePromotion>().Get(x=> x.PromotionId == activePromotionVM.PromotionId).FirstOrDefault();
                    if (activePromotionVM.Base64Image != null)
                    {
                        var splitName = activePromotionVM.Base64Image.Split(',');
                        string convert = activePromotionVM.Base64Image.Replace(splitName[0] + ",", String.Empty);
                        activePromotion.Image = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    if (activePromotionVM.Base64Mobile != null)
                    {
                        var splitName = activePromotionVM.Base64Mobile.Split(',');
                        string convert = activePromotionVM.Base64Mobile.Replace(splitName[0] + ",", String.Empty);
                        activePromotion.ImageMobile = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    activePromotion.ModifiedOn = DateTime.Now;
                    activePromotion.Name = activePromotionVM.Name;
                    activePromotion.IsAcitve = activePromotionVM.IsAcitve;
                    activePromotion.IsMain = activePromotionVM.IsMain;
                    activePromotion.ModifiedBy = activePromotionVM.ModifiedBy;
                    activePromotion.SkillId = activePromotionVM.SkillId;
                    activePromotion.Description = activePromotionVM.Description;
                    activePromotion.UserRoleId = activePromotionVM.UserRoleId;
                    activePromotion.PromotionStartDate = activePromotionVM.PromotionStartDate;
                    activePromotion.PromotionEndDate = activePromotionVM.PromotionEndDate;
                    activePromotion.SubSkillIds = activePromotionVM.SubSkillIds;
                    activePromotion.CampaignTypeId = activePromotionVM.CampaignTypeId;
                    uow.Repository<ActivePromotion>().Update(activePromotion);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (activePromotionVM.PromotionId > 0 && activePromotionVM.Action =="delete")
                {
                    activePromotion = uow.Repository<ActivePromotion>().Get(x=> x.PromotionId == activePromotionVM.PromotionId).FirstOrDefault();
                    activePromotion.ModifiedOn = DateTime.Now;
                    activePromotion.IsAcitve = activePromotion.IsAcitve == true ?  !activePromotion.IsAcitve : true;
                    activePromotion.ModifiedBy = activePromotionVM.ModifiedBy;
                    uow.Repository<ActivePromotion>().Update(activePromotion);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Status changed successfully!";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AddUpdateToolTipForm(TooltipVM tooltipVM)
        {
            Response response = new Response();
            try
            {
                TooltipForm tooltipForm = new TooltipForm();
                if(tooltipVM.FormId <= 0 && tooltipVM.Action == "add")
                {
                    tooltipForm.FormName = tooltipVM.Name;
                    tooltipForm.CreatedOn = DateTime.Now;
                    tooltipForm.CreatedBy = tooltipVM.CreatedBy;
                    tooltipForm.IsActive = true;
                    uow.Repository<TooltipForm>().Add(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added successfully!";
                }
                else if(tooltipVM.FormId > 0 && tooltipVM.Action == "update")
                {
                    tooltipForm = uow.Repository<TooltipForm>().GetById(tooltipVM.FormId);
                    tooltipForm.FormName = tooltipVM.Name;
                    tooltipForm.ModifiedOn = DateTime.Now;
                    tooltipForm.ModifiedBy = tooltipVM.ModifiedBy;
                    uow.Repository<TooltipForm>().Update(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data updated successfully!";

                }
                else if(tooltipVM.FormId > 0 && tooltipVM.Action == "delete")
                {
                    tooltipForm = uow.Repository<TooltipForm>().GetById(tooltipVM.FormId);
                    tooltipForm.IsActive = tooltipForm.IsActive == true ? false : true; 
                    tooltipForm.ModifiedOn = DateTime.Now;
                    tooltipForm.ModifiedBy = tooltipVM.ModifiedBy;
                    uow.Repository<TooltipForm>().Update(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Status changed successfully!";

                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AddUpdateToolTipFormDetails(TooltipVM tooltipVM)
        {
            Response response = new Response();
            try
            {
                TooltipFormDetail tooltipForm = new TooltipFormDetail();
                if(tooltipVM.FormDetailId <= 0 && tooltipVM.Action == "add")
                {
                    tooltipForm.Name = tooltipVM.Name;
                    tooltipForm.FormId = tooltipVM.FormId;
                    tooltipForm.Description = tooltipVM.Description;
                    tooltipForm.IsActive = true;
                    tooltipForm.CreatedOn = DateTime.Now;
                    tooltipForm.CreatedBy = tooltipVM.CreatedBy;
                    uow.Repository<TooltipFormDetail>().Add(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added successfully!";
                }
                else if(tooltipVM.FormDetailId > 0 && tooltipVM.Action == "update")
                {
                    tooltipForm = uow.Repository<TooltipFormDetail>().GetById(tooltipVM.FormDetailId);
                    tooltipForm.Name = tooltipVM.Name;
                    tooltipForm.FormId = tooltipVM.FormId;
                    tooltipForm.Description = tooltipVM.Description;
                    tooltipForm.ModifiedOn = DateTime.Now;
                    tooltipForm.ModifiedBy = tooltipVM.ModifiedBy;
                    uow.Repository<TooltipFormDetail>().Update(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data updated successfully!";

                }
                else if(tooltipVM.FormDetailId > 0 && tooltipVM.Action == "delete")
                {
                    tooltipForm = uow.Repository<TooltipFormDetail>().GetById(tooltipVM.FormDetailId);
                    tooltipForm.ModifiedOn = DateTime.Now;
                    tooltipForm.ModifiedBy = tooltipVM.ModifiedBy;
                    tooltipForm.IsActive = tooltipForm.IsActive == true ? false : true;
                    uow.Repository<TooltipFormDetail>().Update(tooltipForm);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data deleted successfully!";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AddAndUpdateApplicationSetting(ApplicationSettingVM applicationSettingVM)
        {
            Response response = new Response();
            try
            {
                ApplicationSetting applicationSetting = new ApplicationSetting();
                if(applicationSettingVM.ApplicationSettingId <= 0 && applicationSettingVM.Action == "add")
                {
                    applicationSetting.SettingName = applicationSettingVM.SettingName;
                    applicationSetting.IsActive = applicationSettingVM.IsActive;
                    applicationSetting.CreatedOn = DateTime.Now;
                    applicationSetting.CreatedBy = applicationSettingVM.UserId;
                    uow.Repository<ApplicationSetting>().Add(applicationSetting);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added successfully!";
                }
                else if(applicationSettingVM.ApplicationSettingId > 0 && applicationSettingVM.Action == "update")
                {
                    applicationSetting = uow.Repository<ApplicationSetting>().Get(x => x.ApplicationSettingId == applicationSettingVM.ApplicationSettingId).FirstOrDefault();
                    applicationSetting.SettingName = applicationSettingVM.SettingName;
                    applicationSetting.IsActive = applicationSettingVM.IsActive;
                    applicationSetting.ModifiedOn = DateTime.Now;
                    applicationSetting.ModifiedBy = applicationSettingVM.UserId;
                    uow.Repository<ApplicationSetting>().Update(applicationSetting);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data updated successfully!";

                }
                else if(applicationSettingVM.ApplicationSettingId > 0 && applicationSettingVM.Action == "delete")
                {
                    applicationSetting = uow.Repository<ApplicationSetting>().Get(x=> x.ApplicationSettingId == applicationSettingVM.ApplicationSettingId).FirstOrDefault();
                    applicationSetting.ModifiedOn = DateTime.Now;
                    applicationSetting.ModifiedBy = applicationSettingVM.UserId;
                    applicationSetting.IsActive = applicationSetting.IsActive == true ? false : true;
                    uow.Repository<ApplicationSetting>().Update(applicationSetting);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data deleted successfully!";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public List<ApplicationSettingVM> GetSettingList()
        {
            List<ApplicationSettingVM> applicationSetting = new List<ApplicationSettingVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };
                applicationSetting = uow.ExecuteReaderSingleDS<ApplicationSettingVM>("SP_GetSettingList", sqlParameters).ToList();
                return applicationSetting;
            }
            catch (Exception ex)
            {   
                Exc.AddErrorLog(ex);
                return new List<ApplicationSettingVM>();
            }
        }

        public Response AddAndUpdateApplicationSettingDetails(ApplicationSettingVM applicationSettingVM)
        {
            Response response = new Response();
            try
            {
                ApplicationSettingDetail applicationSettingDetail = new ApplicationSettingDetail();
                if (applicationSettingVM.ApplictaionSettingDetailId <= 0 && applicationSettingVM.Action == "add")
                {
                    applicationSettingDetail.ApplicationSettingId = applicationSettingVM.ApplicationSettingId;
                    applicationSettingDetail.SettingKeyName = applicationSettingVM.SettingKeyName;
                    applicationSettingDetail.SettingKeyValue = applicationSettingVM.SettingKeyValue;
                    applicationSettingDetail.CreatedOn = DateTime.Now;
                    applicationSettingDetail.CreatedBy = applicationSettingVM.UserId;
                    uow.Repository<ApplicationSettingDetail>().Add(applicationSettingDetail);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data added successfully!";
                }
                else if (applicationSettingVM.ApplictaionSettingDetailId > 0 && applicationSettingVM.Action == "update")
                {
                    applicationSettingDetail = uow.Repository<ApplicationSettingDetail>().Get(x => x.ApplictaionSettingDetailId == applicationSettingVM.ApplictaionSettingDetailId).FirstOrDefault();
                    applicationSettingDetail.ApplicationSettingId = applicationSettingVM.ApplicationSettingId;
                    applicationSettingDetail.SettingKeyName = applicationSettingVM.SettingKeyName;
                    applicationSettingDetail.SettingKeyValue = applicationSettingVM.SettingKeyValue;
                    applicationSettingDetail.ModifiedOn = DateTime.Now;
                    applicationSettingDetail.ModifiedBy = applicationSettingVM.UserId;
                    uow.Repository<ApplicationSettingDetail>().Update(applicationSettingDetail);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data updated successfully!";
                }
                else if (applicationSettingVM.ApplictaionSettingDetailId > 0 && applicationSettingVM.Action == "delete")
                {
                    applicationSettingDetail = uow.Repository<ApplicationSettingDetail>().Get(x => x.ApplictaionSettingDetailId == applicationSettingVM.ApplictaionSettingDetailId).FirstOrDefault();
                    applicationSettingDetail.ModifiedOn = DateTime.Now;
                    applicationSettingDetail.ModifiedBy = applicationSettingVM.UserId;
                    applicationSettingDetail.IsActive = applicationSettingDetail.IsActive == true ? false :true;
                    uow.Repository<ApplicationSettingDetail>().Update(applicationSettingDetail);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data deleted successfully!";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response UnlinkSalesman(SalesmanVM salesmanVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters ={
                    new SqlParameter("@SalesmanId" , salesmanVM.SalesmanId),
                    new SqlParameter("@CustomerId" , salesmanVM.CustomerId),
                };
                uow.ExecuteReaderSingleDS<Response>("SP_UnlinkSalesman", sqlParameters);
                response.Message = "Salesman unlink successfully!";
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

        public List<ApplicationSettingVM> GetSettingDetailsList()
        {
            List<ApplicationSettingVM> applicationSetting = new List<ApplicationSettingVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };
                applicationSetting = uow.ExecuteReaderSingleDS<ApplicationSettingVM>("SP_GetSettingDetailsList", sqlParameters).ToList();
                return applicationSetting;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ApplicationSettingVM>();
            }
        }
        public List<ActivePromotionVM> GetPromotionList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<ActivePromotionVM>("Sp_GetPromotionList", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ActivePromotionVM>();
            }
        }
        public List<ActivePromotionVM> GetPromotionListForWeb()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<ActivePromotionVM>("Sp_GetPromotionListForWeb", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ActivePromotionVM>();
            }
        }
        public List<ActivePromotionVM> GetActivePromotionList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<ActivePromotionVM>("Sp_GetActivePromotionList", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ActivePromotionVM>();
            }
        }

        public Response InsertAndUpDateAgreement(Agreements agreements)
        {
            Response response = new Response();
            try
            {
                if (agreements.AgreementId <= 0)
                {
                    agreements.CreatedOn = DateTime.Now;
                }
                if (agreements.AgreementId > 0)
                {
                    agreements.ModifiedOn = DateTime.Now;
                }

                SqlParameter[] sqlParameters = {
                    new SqlParameter("@AgreementId",agreements.AgreementId),
                    new SqlParameter("@Header",agreements.Header),
                    new SqlParameter("@AgreementsText",agreements.AgreementsText),
                    new SqlParameter("@CreatedBy",agreements.CreatedBy),
                    new SqlParameter("@CreatedOn",agreements.CreatedOn),
                    new SqlParameter("@ModifiedBy",agreements.ModifiedBy),
                    new SqlParameter("@ModifiedOn",agreements.ModifiedOn),
                    new SqlParameter("@IsActive",agreements.IsActive),
                    new SqlParameter("@UserType",agreements.UserType),
                    new SqlParameter("@OrderByColumn",agreements.OrderByColumn),
                };
                var res = uow.ExecuteReaderSingleDS<Response>("Sp_InsetUpdateAgreements", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = response.Message = res.Select(x => x.Message).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public Response AddAndUpdateCampaigns(CampaignVM campaignVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@CampaignId",campaignVM.CampaignId),
                    new SqlParameter("@FullName",campaignVM.FullName),
                    new SqlParameter("@ShortName",campaignVM.ShortName),
                    new SqlParameter("@Description",campaignVM.Description),
                    new SqlParameter("@StartDate",campaignVM.StartDate),
                    new SqlParameter("@EndDate",campaignVM.EndDate),
                    new SqlParameter("@CreatedBy",campaignVM.CreatedBy),
                    new SqlParameter("@ModifiedBy",campaignVM.ModifiedBy),
                    new SqlParameter("@IsActive",campaignVM.IsActive),
                    new SqlParameter("@Action",campaignVM.Action),
                };
                var res = uow.ExecuteReaderSingleDS<Response>("Sp_AddAndUpdateCampaigns", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = response.Message = res.Select(x => x.Message).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public List<Faqs> GetFAQsList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<Faqs>("Sp_GetFaqs", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Faqs>();
            }
        }

        public List<Agreements> GetAgreementsList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<Agreements>("Sp_GetAgreements", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Agreements>();
            }
        }        
        public List<CampaignVM> GetCampaignsList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                var res = uow.ExecuteReaderSingleDS<CampaignVM>("SP_GetCampaignsList", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CampaignVM>();
            }
        }

        public List<Town> GetTownsByCityId(long cityId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@cityId", cityId)
                };

                List<Town> result = uow.ExecuteReaderSingleDS<Town>("GetGetTownsByCityId", sqlParameters);

                return result.OrderBy(o => o.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Town>();
            }
        }
        public Response AddUpdateTown(TownVM town)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@TownId", town.TownId),
                    new SqlParameter("@Name", town.Name),
                    new SqlParameter("@IsActive", town.IsActive),
                    new SqlParameter("@CreatedBy", town.CreatedBy),
                    new SqlParameter("@CreatedOn", town.CreatedOn),
                    new SqlParameter("@CityId", town.CityId),
                };

                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddUpdateTown", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
                //return uow.Repository<Town>().Get(x => x.CityId == cityId).OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return res;
            }
        }

        public List<TownVM> GetAllTown()
        {
            List<TownVM> town = new List<TownVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };

                town = uow.ExecuteReaderSingleDS<TownVM>("GetTown", sqlParameters);

                return town;
                //return uow.Repository<Town>().Get(x => x.CityId == cityId).OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TownVM>();
            }
        }

        public Response AddUpdateSalesman(SalesmanVM salesmanVM)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@salesmanId", salesmanVM.SalesmanId),
                    new SqlParameter("@name", salesmanVM.Name),
                    new SqlParameter("@address", salesmanVM.Address),
                    new SqlParameter("@createdBy", salesmanVM.CreatedBy),
                    new SqlParameter("@createdOn", salesmanVM.CreatedOn),
                    new SqlParameter("@mobileNumber", salesmanVM.MobileNumber),
                    new SqlParameter("@isActive", salesmanVM.IsActive),
                };

                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddupdateSalesman", sqlParameters);
                res.Message = result.Select(x => x.Message).FirstOrDefault();
                return res;
                //return uow.Repository<Town>().Get(x => x.CityId == cityId).OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return res;
            }
        }

        public List<SalesmanVM> GetAllSalesman()
        {
            List<SalesmanVM> salesmanVM = new List<SalesmanVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };

                salesmanVM = uow.ExecuteReaderSingleDS<SalesmanVM>("Sp_GetSalesManAll", sqlParameters);

                return salesmanVM;
                //return uow.Repository<Town>().Get(x => x.CityId == cityId).OrderBy(a => a.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SalesmanVM>();
            }
        }
        public List<SalesmanVM> LinkedSalesManList(SalesmanVM salesmanVM)
        {
            List<SalesmanVM> salesmanList = new List<SalesmanVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@Name",salesmanVM.Name),
                    new SqlParameter("@SalesmanId",salesmanVM.SalesmanId),
                    new SqlParameter("@MobileNumber",salesmanVM.MobileNumber),
                    new SqlParameter("@CustomerId",salesmanVM.CustomerId),
                    new SqlParameter("@CustomerPhoneNumber",salesmanVM.CustomerPhoneNumber),
                    new SqlParameter("@CampaignName",salesmanVM.CampaignName),
                    new SqlParameter("@pageNumber",salesmanVM.pageNumber),
                    new SqlParameter("@pageSize",salesmanVM.pageSize),
                };
                salesmanList = uow.ExecuteReaderSingleDS<SalesmanVM>("SP_LinkedSalesmanList",sqlParameters);
                return salesmanList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SalesmanVM>();
            }
        }
        public List<TooltipVM> GetToolTipFormsList()
        {
            List<TooltipVM> tooltipVM = new List<TooltipVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };
                tooltipVM = uow.ExecuteReaderSingleDS<TooltipVM>("SP_GetToolTipFormsList", sqlParameters).ToList();
                return tooltipVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TooltipVM>();
            }
        }
        public List<TooltipVM> GetToolTipFormsDetailsList()
        {
            List<TooltipVM> tooltipVM = new List<TooltipVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {

                };
                tooltipVM = uow.ExecuteReaderSingleDS<TooltipVM>("SP_GetToolTipFormsDetailList", sqlParameters).ToList();
                return tooltipVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TooltipVM>();
            }
        }
        public List<TooltipVM> GetSingleFormDetails(long id)
        {
            List<TooltipVM> tooltipVM = new List<TooltipVM>();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@id",id)
                };
                tooltipVM = uow.ExecuteReaderSingleDS<TooltipVM>("SP_GetSingleFormDetails", sqlParameters).ToList();
                return tooltipVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TooltipVM>();
            }
        }

        public List<ActivePromotionMobileVM> GetPromotionListIdValue()
        {
            try
            {
                int id = 0;
                SqlParameter[] sqlParameters = {
                    
                new SqlParameter("@promotionId", id)
                };
                var res = uow.ExecuteReaderSingleDS<ActivePromotionMobileVM>("Sp_GetPromotionListIdNameImage", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<ActivePromotionMobileVM>();
            }
        }

        public ActivePromotionMobileVM GetPromotionImageById(int promotionId)
        {
            try
            {
                SqlParameter[] sqlParameters = 
                {
                    new SqlParameter("@promotionId",promotionId)
                };
                var res = uow.ExecuteReaderSingleDS<ActivePromotionMobileVM>("Sp_GetPromotionListIdNameImage", sqlParameters).FirstOrDefault();
                return res;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new ActivePromotionMobileVM();
            }
        }

        public Response CheckUserExist(string data)
        {
            Response response = new Response();
            var entity = JsonConvert.DeserializeObject<CheckUserExistenceVM>(data);
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@Id",entity.Id)
                };
                var result = uow.ExecuteReaderSingleDS<CheckUserExistenceVM>("Sp_CheckUserExist", sqlParameters).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                response.ResultData = result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

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
                        new SqlParameter("@code",entity.Code ),

                       new SqlParameter("@active",entity.Active ),
                       new SqlParameter("@CreatedBy",entity.UserId),
                       new SqlParameter("@ModifiedBy",entity.UserId),


                };

                var res = await uow.ExecuteReaderSingleDSNew<StateListVM>("SP_AddUpdateState", sqlParameters);
                response.ResultData = res;
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved Successfully!!";


            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCompaignsTypeList(int CampaignTypeId)
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                  new SqlParameter("@CampaignTypeId",CampaignTypeId),
                };
                var CampaignTypeList = uow.ExecuteReaderSingleDS<ActivePromotionVM>("Sp_GetCampaignTypeList", sqlParameter);
                response.ResultData = CampaignTypeList;
                response.Status = ResponseStatus.OK;
                response.Message = "Campaign Type List";
            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetCompaignsTypesListForAdmin()
        {
            var response = new Response();
            try
            {
                SqlParameter[] sqlParameter = {
                 
                };
                var CampaignTypeList = uow.ExecuteReaderSingleDS<CampaignTypeVM>("Sp_GetCampaignTypesListForAdmin", sqlParameter).ToList();
                response.ResultData = CampaignTypeList;
                response.Status = ResponseStatus.OK;
                response.Message = "Campaign Types List";
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
            Exc.AddErrorLog("1313");
            var response = new Response();
            try
            {
                Exc.AddErrorLog("1316");
                SqlParameter[] sqlParameters = { };
                var statelist =await uow.ExecuteReaderSingleDSNew<StateListVM>("Sp_GetStateList", sqlParameters);
                Exc.AddErrorLog($"1319 {statelist} ");
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
        public async Task<Response> GetSateList(int? countryId)
        {
            Response response = new Response();
            List<State> state = new List<State>();
            try
            {
                if (countryId == 0)
                {
                  state = uow.Repository<State>().GetAll().Where(x => x.Active == true).ToList();
                   
                }
                else
                {
                    state =  uow.Repository<State>().GetAll().Where(x => x.CountryId == countryId && x.Active == true).ToList();
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
        public async Task<Response> GetCityListByReleventState(int? stateId)
        {
            Response response = new Response();
            List<City> city = new List<City>();
            try
            {
                if (stateId == 0)
                {
                    city = uow.Repository<City>().GetAll().Where(x => x.IsActive == true).ToList();
                }
                else
                {
                    city = uow.Repository<City>().GetAll().Where(x => x.StateId == stateId && x.IsActive == true).ToList();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Get Relevant States City !!";
                response.ResultData = city;
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

        public async Task<Response> GetTimonialsTypesListForAdmin()
        {
            var response = new Response();
            List<TestimonialsType> testimonialsType = new List<TestimonialsType>();
            try
            {

                testimonialsType = uow.Repository<TestimonialsType>().GetAll().ToList();
                response.ResultData = testimonialsType;
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
        public async Task<Response> AddUpdateTestinomials(string data)
        {
            var response = new Response();
            var testimonial = new Testimonial();
            var entity = JsonConvert.DeserializeObject<TestinomialVM>(data);
            try
            {
                if (entity.TestimonialsId == 0)
                {
                    testimonial.Title = entity.Title;
                    testimonial.Description = entity.Description;
                    testimonial.Active = entity.Active;
                    testimonial.UserType = entity.UserType;
                    testimonial.Url = entity.Url;
                    testimonial.Testimonialtype = entity.Testimonialtype;
                    testimonial.CreatedBy = entity.UserId;
                    testimonial.CreatedOn = DateTime.Now;
                uow.Repository<Testimonial>().Add(testimonial);
                    response.Message = "Data Save Successfully!!";
                    uow.Save();
                }
                else
                {
                    testimonial= uow.Repository<Testimonial>().Get(x => x.TestimonialsId == entity.TestimonialsId).FirstOrDefault();
                    testimonial.Title = entity.Title;
                    testimonial.Description = entity.Description;
                    testimonial.Active = entity.Active;
                    testimonial.UserType = entity.UserType;
                    testimonial.Url = entity.Url;
                    testimonial.Testimonialtype = entity.Testimonialtype;
                    testimonial.ModifiedBy = entity.UserId;
                    testimonial.ModifiedOn = DateTime.Now;
                    uow.Repository<Testimonial>().Update(testimonial);
                    response.Message = "Data updated Successfully!!";
                    uow.Save();
                }

                response.Status = ResponseStatus.OK;
   
        


            }
            catch (Exception ex)
            {
                // Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async Task<Response> GetTestimonialsListForAdmin()
        {
            var response = new Response();
            try
            {

                SqlParameter[] sqlParameters = { };
                var testimonials = await uow.ExecuteReaderSingleDSNew<TestinomialVM>("Sp_TestimonialsList", sqlParameters);
                response.ResultData = testimonials;
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
        public async Task<Response> DeleteTesimoaialsStatus(string data)
        {
            var response = new Response();
            var obj = new Testimonial();
            var entity = JsonConvert.DeserializeObject<TestinomialVM>(data);
            try
            {

                obj = uow.Repository<Testimonial>().Get(o => o.TestimonialsId == entity.TestimonialsId).FirstOrDefault();
                obj.Active = obj.Active == true ? false : true;
                obj.ModifiedOn = DateTime.Now;
                obj.ModifiedBy = entity.UserId;
                uow.Repository<Testimonial>().Update(obj);
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
     
    }
}
