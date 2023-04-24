using HW.ReportsViewModels;
using HW.SupplierViewModels;
using HW.UserManagementApi.Services;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.IdentityViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HW.UserManagementApi.Controllers
{
    [Produces("application/json")]
    public class UserManagementController : BaseController
    {
        private readonly IUserManagementService userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            this.userManagementService = userManagementService;
        }

        [HttpGet]
        public string Start()
        {
            return "User Management service is started.";
        }

        [HttpGet]
        public IdentityViewModels.PersonalDetailsVM GetUserDetailsByUserRole(string userRole, string userId)
        {
            return userManagementService.GetUserDetailsByUserRole(userRole , userId);
        }
        [HttpGet]
        public List<City> GetCityList()
        {
            return userManagementService.GetCityList();
        }        
        [HttpGet]
        public Task<Response> GetCityListWithTraxCityId()
        {
            return userManagementService.GetCityListWithTraxCityId();
        }

        [HttpGet]
        public City GetCityById(long cityId)
        {
            return userManagementService.GetCityById(cityId);
        }

        [HttpGet]
        public List<IdValueVM> GetAllCities()
        {
            return userManagementService.GetAllCities();
        }

        [HttpGet]
        public List<IdValueVM> GetDistances()
        {
            return userManagementService.GetDistances();
        }

        [HttpGet]
        public async Task<Response> GetOtp(string userId,bool fromPersonalDetails)
        {
            return await userManagementService.GetOtp(userId, fromPersonalDetails);
        }

        [HttpGet]
        public async Task<Response> VerifyOtp(string userId, string code)
        {
            return await userManagementService.VerifyOtp(userId, code);
        }

        [HttpGet]
        public long GetCityIdByName(string cityName)
        {
            return userManagementService.GetCityIdByName(cityName);
        }

        [HttpGet]
        public City GetCityByName([FromQuery]string cityName)
        {
            return userManagementService.GetCityByName(cityName);
        }

        [HttpGet]
        public long GetDistanceBuName([FromQuery]string TravellingDistance)
        {

            return userManagementService.GetDistanceBuName(TravellingDistance);
        }

        [HttpPost]
        public List<City> GetCityNameByCityId()
        {
            return userManagementService.GetCityNameByCityId();
        }

        [HttpGet]
        public City CheckCityAvailability(string cityName)
        {
            return userManagementService.CheckCityAvailability(cityName);
        }

        [HttpGet]
        public List<State> GetStatesList()
        {
            return userManagementService.GetStatesList();
        }

        [HttpGet]
        public List<CityAndStateVM> GetCitiesList()
        {
            return userManagementService.GetCitiesList();
        }

        [HttpPost]
        public Response AddNewCity([FromBody] City city)
        {
            return userManagementService.AddNewCity(city);
        }

       [HttpPost]
        public Response AddUpdateTown([FromBody] TownVM town)
        {
            return userManagementService.AddUpdateTown(town);
        }

        [HttpPost]
        public Response UpdateCity([FromBody]City city)
        {
            return userManagementService.UpdateCity(city);
        }

        [HttpGet]
        public Response DeleteCity(string cityId)
        {
            return userManagementService.DeleteCity(cityId);
        }

        [HttpPost]
        public Response InsertAndUpDateFAQs([FromBody] Faqs fAQs)
        {
            return userManagementService.InsertAndUpDateFAQs(fAQs);
        }

        [HttpGet]
        public List<Faqs> GetFAQsList()
        {
            return userManagementService.GetFAQsList();
        }
        [HttpGet]
        public List<ActivePromotionVM> GetPromotionList()
        {
            return userManagementService.GetPromotionList();
        }
        [HttpGet]
        public List<ActivePromotionVM> GetPromotionListForWeb()
        {
            return userManagementService.GetPromotionListForWeb();
        }
        [HttpGet]
        public List<ActivePromotionMobileVM> GetPromotionListIdValue()
        {
            return userManagementService.GetPromotionListIdValue();
        }
        [HttpGet]
        public List<ActivePromotionVM> GetActivePromotionList()
        {
            return userManagementService.GetActivePromotionList();
        }

        [HttpPost]
        public Response InsertAndUpDateAgreement([FromBody] Agreements agreements)
        {
            return userManagementService.InsertAndUpDateAgreement(agreements);
        }        
        [HttpPost]
        public Response AddAndUpdateCampaigns([FromBody] CampaignVM campaignVM)
        {
            return userManagementService.AddAndUpdateCampaigns(campaignVM);
        }

        [HttpGet]
        public List<Agreements> GetAgreementsList()
        {
            return userManagementService.GetAgreementsList();
        }        
        [HttpGet]
        public List<CampaignVM> GetCampaignsList()
        {
            return userManagementService.GetCampaignsList();
        }

        [HttpGet]
        public List<Town> GetTownsByCityId(long cityId)
        {
            return userManagementService.GetTownsByCityId(cityId);
        }
        [HttpGet]
        public List<TownVM> GetAllTown()
        {
            return userManagementService.GetAllTown();
        }
        [HttpPost]
        public Response AddUpdateSalesman([FromBody] SalesmanVM salesmanVM)
        {
            return userManagementService.AddUpdateSalesman(salesmanVM);
        }

        [HttpGet]
        public List<SalesmanVM> GetAllSalesman()
        {
            return userManagementService.GetAllSalesman();
        }        
        [HttpPost]
        public List<SalesmanVM> LinkedSalesManList([FromBody] SalesmanVM salesmanVM)
        {
            return userManagementService.LinkedSalesManList(salesmanVM);
        }
        [HttpPost]
        public Response InsertAndUpDateActivePromotion([FromBody] ActivePromotionVM activePromotionVM)
        {
            return userManagementService.InsertAndUpDateActivePromotion(activePromotionVM);

        }        
        [HttpPost]
        public Response AddUpdateToolTipForm([FromBody] TooltipVM tooltipVM)
        {
            return userManagementService.AddUpdateToolTipForm(tooltipVM);
        } 
        [HttpPost]
        public Response AddUpdateToolTipFormDetails([FromBody] TooltipVM tooltipVM)
        {
            return userManagementService.AddUpdateToolTipFormDetails(tooltipVM);
        }        
        [HttpPost]
        public Response AddAndUpdateApplicationSetting([FromBody]ApplicationSettingVM applicationSettingVM)
        {
            return userManagementService.AddAndUpdateApplicationSetting(applicationSettingVM);
        }
        [HttpGet]
        public List<ApplicationSettingVM> GetSettingList()
        {
            return userManagementService.GetSettingList();
        }         
        [HttpPost]
        public Response AddAndUpdateApplicationSettingDetails([FromBody]ApplicationSettingVM applicationSettingVM)
        {
            return userManagementService.AddAndUpdateApplicationSettingDetails(applicationSettingVM);
        }       
        [HttpPost]
        public Response UnlinkSalesman([FromBody]SalesmanVM salesmanVM)
        {
            return userManagementService.UnlinkSalesman(salesmanVM);
        }
        [HttpGet]
        public List<ApplicationSettingVM> GetSettingDetailsList()
        {
            return userManagementService.GetSettingDetailsList();
        }        
        [HttpGet]
        public List<TooltipVM> GetToolTipFormsList()
        {
            return userManagementService.GetToolTipFormsList();
        }
        [HttpGet]
        public List<TooltipVM> GetToolTipFormsDetailsList()
        {
            return userManagementService.GetToolTipFormsDetailsList();
        }        
        [HttpGet]
        public List<TooltipVM> GetSingleFormDetails(long id)
        {
            return userManagementService.GetSingleFormDetails(id);
        }
        [HttpGet]
        public ActivePromotionMobileVM GetPromotionImageById(int promotionId)
        {
            return userManagementService.GetPromotionImageById(promotionId);
        }

        [HttpPost]
        public Response CheckUserExist([FromBody] string data)
        {
            return userManagementService.CheckUserExist(data);
        }
        [HttpPost]
        public async Task<Response> AddUpdateState([FromBody] string data)
        {
            return await userManagementService.AddUpdateState(data);
        }
        [HttpGet]
        public async Task<Response> GetStateListForAdmin()
        {
            return await userManagementService.GetStateListForAdmin();
        }
        [HttpPost]
        public async Task<Response> deletestateStatus([FromBody] string data)
        {
            return await userManagementService.deletestateStatus(data);
        }
        [HttpGet]
        public async Task<Response> GetSateList(int? countryId)
        {
            return await userManagementService.GetSateList(countryId);
        }

        [HttpGet]
        public async Task<Response> GetCityListByReleventState(int? stateId)
        {
            return await userManagementService.GetCityListByReleventState(stateId);
        }
    

        [HttpGet]
        public async Task<Response> GetCompaignsTypeList(int CampaignTypeId)
        {
            return await userManagementService.GetCompaignsTypeList(CampaignTypeId);
        }

        [HttpGet]
        public async Task<Response> GetCompaignsTypesListForAdmin()
        {
            return await userManagementService.GetCompaignsTypesListForAdmin();
        }
        [HttpGet]
        public async Task<Response> GetTimonialsTypesListForAdmin()
        {
            return await userManagementService.GetTimonialsTypesListForAdmin();
        }

        [HttpPost]
        public async Task<Response> AddUpdateTestinomials([FromBody] string data)
        {
            return await userManagementService.AddUpdateTestinomials(data);
        }
        [HttpGet]
        public async Task<Response> GetTestimonialsListForAdmin()
        {
            return await userManagementService.GetTestimonialsListForAdmin();
        }
        [HttpPost]
        public async Task<Response> DeleteTesimoaialsStatus([FromBody] string data)
        {
            return await userManagementService.DeleteTesimoaialsStatus(data);
        }
  
    }
}