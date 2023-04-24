using HW.EmailViewModel;
using HW.TradesmanApi.Services;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;

namespace HW.TradesmanApi.Controllers
{
    [Produces("application/json")]
    public class TradesmanController : BaseController
    {
        private readonly ITradesmanService tradesmanService;

        public TradesmanController(ITradesmanService tradesmanService)
        {
            this.tradesmanService = tradesmanService;
        }

        [HttpGet]
        public string Start()
        {
            return "Tradesman service is started.";
        }

        [HttpGet]
        public List<Skill> GetAllSkills(long skillId)
        {
            return tradesmanService.GetAllSkills(skillId).ToList();
        }
        [HttpGet]
        public List<SkillAndSubSkillVM> GetSkillListAdmin()
        {
            return tradesmanService.GetSkillListAdmin().ToList();
        }

        [HttpGet]
        public List<SkillSet> GetSkillSetByTradesmanId(long tradesmanId, bool isActive)
        {
            return tradesmanService.GetSkillSetByTradesmanId(tradesmanId, isActive);
        }

        [HttpGet]
        public Tradesman GetPersonalDetails(long tradesmanId)
        {
            return tradesmanService.GetPersonalDetails(tradesmanId);
        }

        [HttpPost]
        public async Task<bool> UpdatePersonalDetails([FromBody] Tradesman tradesman)
        {
            return await tradesmanService.UpdatePersonalDetails(tradesman);
        }

        [HttpPost]
        public List<Tradesman> GetTradesmanDetailsByTradesmanIds([FromBody] List<long> tradesmanIds)
        {
            return tradesmanService.GetTradesmanDetailsByTradesmanIds(tradesmanIds);
        }

        [HttpGet]
        public List<SubSkill> GetTradesmanSubSkills(long parentId)
        {
            return tradesmanService.GetTradesmanSubSkills(parentId);
        }

        [HttpGet]
        public Skill GetSkillBySkillId(long skillId)
        {
            return tradesmanService.GetSkillBySkillId(skillId);
        }

        [HttpGet]
        public List<SubSkill> GetSubSkillBySkillId(long skillId)
        {
            return tradesmanService.GetSubSkillBySkillId(skillId);
        }
        [HttpGet]
        public SubSkill GetSubSkillbySubSkillId(long subSkillId)
        {
            return tradesmanService.GetSubSkillbySubSkillId(subSkillId);
        }
        [HttpGet]
        public List<SubSkill> GetSubSkillsBySkillId(long skillId)
        {
            return tradesmanService.GetSubSkillsBySkillId(skillId);
        }

        [HttpGet]
        public Tradesman GetTradesmanByTradesmanId(long tradesmanId)
        {
            return tradesmanService.GetTradesmanByTradesmanId(tradesmanId);
        }

        [HttpGet]
        public List<Skill> GetLocalProfessionalImages()
        {
            return tradesmanService.GetLocalProfessionalImages();
        }

        [HttpGet]
        public async Task<Tradesman> GetTradesmanById(long tradesmanId)
        {
            return await tradesmanService.GetTradesmanById(tradesmanId);
        }

        [HttpPost]
        public List<Tradesman> GetTradmanDetails([FromBody] List<long> TradmanId)
        {
            return tradesmanService.GetTradmanDetails(TradmanId);
        }

        [HttpGet]
        public SubSkill GetSubSkillById(long SubSkillId)
        {
            return tradesmanService.GetSubSkillById(SubSkillId);
        }

        [HttpGet]
        public List<IdValueVM> GetTradesmanSkills()
        {
            return tradesmanService.GetTradesmanSkills().ToList();
        }

        [HttpPost]
        public async Task<Response> AddEditTradesman([FromBody] Tradesman model)
        {
            return await tradesmanService.AddEditTradesman(model);
        }

        [HttpPost]
        public async Task<Response> SetTradesmanSkills([FromBody] List<SkillSet> skillSets)
        {
            return await tradesmanService.SetTradesmanSkills(skillSets);
        }

        [HttpPost]
        public async Task<bool> DeleteTradesman(long id)
        {
            return await tradesmanService.DeleteTradesman(id);
        }

        [HttpGet]
        public long GetEntityIdByUserId(string userId)
        {
            return tradesmanService.GetEntityIdByUserId(userId);
        }

        [HttpGet]
        public SkillSet GetSkillSetByTradesmanIds(long tradesmanId)
        {
            return tradesmanService.GetSkillSetByTradesmanIds(tradesmanId);

        }

        [HttpPost]
        public List<Skill> GetSkillsByIds([FromBody] List<long> skillIds)
        {
            return tradesmanService.GetSkillsByIds(skillIds);
        }

        [HttpGet]
        public Tradesman GetTradesmanByUserId(string userId)
        {
            return tradesmanService.GetTradesmanByUserId(userId);
        }

        [HttpGet]
        public List<SubSkill> GetSubSkill()
        {
            return tradesmanService.GetSubSkill();
        }
        [HttpGet]
        public List<SkillAndSubSkillVM> GetSubSkillList()
        {
            return tradesmanService.GetSubSkillList();
        }

        [HttpGet]
        public List<long> GetTradesmanSkillIds(long tradesmanId)
        {
            return tradesmanService.GetTradesmanSkillIds(tradesmanId);
        }

        [HttpGet]
        public string GetTradesmanSkillName([FromQuery] long skillId)
        {
            return tradesmanService.GetTradesmanSkillName(skillId);
        }

        [HttpGet]
        public List<TradesmanEmailVM> SP_GetTradesmanWithSkillIdAndCityId(long skillId, string cityName)
        {
            return tradesmanService.SP_GetTradesmanWithSkillIdAndCityId(skillId, cityName);
        }

        [HttpPost]
        public List<AdminTradesmanListVM> SpGetTradesmanList([FromBody] GenericUserVM genericUserVM)
        {
            return tradesmanService.SpGetTradesmanList(genericUserVM);
        }

        [HttpGet]
        public SpTradesmanStats SpGetTradesmanStats()
        {
            return tradesmanService.SpGetTradesmanStats();
        }

        [HttpGet]
        public SpBusinessProfileVM SpGetBusinessDetails(long userId, string role)
        {
            return tradesmanService.SpGetBusinessDetails(userId, role);
        }


        [HttpGet]
        public List<Tradesman> GetLAllTradesman()
        {
            return tradesmanService.GetLAllTradesman();
        }
        [HttpGet]
        public List<Tradesman> GetLAllActiveTradesman(bool isOrganisation)
        {
            return tradesmanService.GetAllActiveTradesman(isOrganisation);
        }
        [HttpGet]
        public List<TradesmanDTO> GetLAllTradesmanFromToReport(DateTime? StartDate, DateTime? EndDate, string skills, string tradesman, string city, bool lastActive, string location, string mobile, string cnic, string emailtype, string mobileType, string activityType, string userType)
        {
            return tradesmanService.GetLAllTradesmanFromToReport(StartDate, EndDate, skills, tradesman, city, lastActive, location, mobile, cnic, emailtype, mobileType, activityType, userType);
        }
        [HttpGet]
        public List<TradesmanDTO> GetLAllTradesmanLast24Hours(System.DateTime StartDate, System.DateTime EndDate)
        {
            return tradesmanService.GetLAllTradesmanLast24Hours(StartDate, EndDate);
        }
        [HttpGet]
        public List<Tradesman> GetLAllTradesmanYearlyReport()
        {
            return tradesmanService.GetLAllTradesmanYearlyReport();
        }

        [HttpGet]
        public List<string> GetTradesmenListBySkillIdAndCityId(long _skillId, string _cityName)
        {
            return tradesmanService.GetTradesmenListBySkillIdAndCityId(_skillId, _cityName);
        }
        [HttpGet]
        public List<IdValueVM> GetTradesmanSkillsForDropdown()
        {
            return tradesmanService.GetTradesmanSkillsForDropdown();
        }

        [HttpGet]
        public List<TradesmanReportbySkillVM> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId)
        {
            return tradesmanService.GetLAllTradesmanbySkillTown(skills, town, tradesmanId);
        }

        [HttpGet]
        public List<TradesmanReportbySkillVM> GetLAllTradesmanbyCategoryReport(DateTime StartDate, DateTime EndDate, string skills)
        {
            return tradesmanService.GetLAllTradesmanbyCategoryReport(StartDate, EndDate, skills);
        }

        [HttpPost]
        public List<Tradesman> GetTradesmanReport([FromBody] List<string> userId)
        {
            return tradesmanService.GetTradesmanReport(userId);
        }

        [HttpGet]
        public bool UpdateTradesmanPublicId(long tradesmanId, string publicId)
        {
            return tradesmanService.UpdateTradesmanPublicId(tradesmanId, publicId);
        }
        [HttpGet]
        public List<Tradesman> GetTradesmanAddressList()
        {
            return tradesmanService.GetTradesmanAddressList();
        }

        [HttpGet]
        public Response CheckSkillAvailability(string skillName, string subSkillName, int skillId, int orderBy)
        {
            return tradesmanService.CheckSkillAvailability(skillName, subSkillName, skillId, orderBy);
        }
        [HttpPost]
        public Response CheckOrderAvailability([FromBody] TradesmanCommonVM tradesmanCommonVM)
        {
            return tradesmanService.CheckOrderAvailability(tradesmanCommonVM);
        }
        [HttpPost]
        public Response AddNewSkill([FromBody] SkillAndSubSkillVM skill)
        {
            return tradesmanService.AddNewSkill(skill);
        }
        [HttpPost]
        public Response AddOrUpdateSubSkill([FromBody] SkillAndSubSkillVM subSkill)
        {
            return tradesmanService.AddOrUpdateSubSkill(subSkill);
        }
        [HttpPost]
        public Response UpdateSkill([FromBody] UpdateSkillVM skill)
        {
            return tradesmanService.UpdateSkill(skill);
        }
        [HttpGet]
        public Response DeleteSkill(int skillId, int subSkillId)
        {
            return tradesmanService.DeleteSkill(skillId, subSkillId);
        }

        [HttpGet]
        public List<GetInActiveUserVM> GetInactiveUserReport(int pageNumber, int pageSize, string dataOrderBy, DateTime? fromDate, DateTime? toDate, string city, string selectedUser)
        {
            return tradesmanService.GetInactiveUserReport(pageNumber, pageSize, dataOrderBy, fromDate, toDate, city, selectedUser);
        }
        [HttpPost]
        public List<TradesmanDTO> TradesmanByCategory([FromBody] TradesmanByCatVMcs tradesmanByCatVMcs)
        {
            return tradesmanService.TradesmanByCategory(tradesmanByCatVMcs);
        }
        [HttpGet]
        public Response GetBusinessDetailsStatus(string id)
        {
            return tradesmanService.GetBusinessDetailsStatus(id);
        }
        [HttpGet]
        public Response AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            return tradesmanService.AddLinkedSalesman(SalesmanId, CustomerId);
        }
        [HttpGet]
        public Skill GetSkillTagsBySkillId(long skillId)
        {
            return tradesmanService.GetSkillTagsBySkillId(skillId);
        }

        [HttpGet]
        public List<MetaTags> GetCommonMetaTags()
        {
            return tradesmanService.GetCommonMetaTags();
        }

        [HttpGet]
        public SubSkill GetSubSkillTagsById(long subSkillId)
        {
            return tradesmanService.GetSubSkillTagsById(subSkillId);
        }

        [HttpGet]
        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            return await tradesmanService.UpdatePhoneNumberByUserId(userId, phoneNumber);
        }
        [HttpGet]
        public List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo)
        {
            return tradesmanService.GetTradesmanByName(tradesmanName, tradesmanId, tradesmanPhoneNo);
        }

        [HttpGet]
        public Response BlockTradesman(string tradesmanId, bool status)
        {
            return tradesmanService.BlockTradesman(tradesmanId, status);
        }        
        [HttpGet]
        public async Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city)

        {
            return await tradesmanService.GetTradesmanFirebaseIdListBySkillAndCity(categoryId, city);
        }
        [HttpGet]
        public List<SubSkillWithSkillVM> GetSubSkillsWithSkill()
        {
            return tradesmanService.GetSubSkillsWithSkill();
        }
    }
}

