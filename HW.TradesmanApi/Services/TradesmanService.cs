using HW.EmailViewModel;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using System.Security.Cryptography.X509Certificates;
using Autofac.Features.Metadata;
using HW.IdentityServerModels;

namespace HW.TradesmanApi.Services
{
    public interface ITradesmanService
    {
        IQueryable<Skill> GetAllSkills(long skillId);
        IQueryable<SkillAndSubSkillVM> GetSkillListAdmin();
        Tradesman GetPersonalDetails(long tradesmanId);
        List<SubSkill> GetTradesmanSubSkills(long skillId);
        Task<bool> UpdatePersonalDetails(Tradesman tradesman);
        List<Tradesman> GetTradesmanDetailsByTradesmanIds(List<long> tradesmanIds);
        Skill GetSkillBySkillId(long skillId);
        List<SubSkill> GetSubSkillBySkillId(long skillId);
        SubSkill GetSubSkillbySubSkillId(long subSkillId);
        List<SubSkill> GetSubSkillsBySkillId(long skillId);
        Tradesman GetTradesmanByTradesmanId(long tradesmanId);
        List<Skill> GetLocalProfessionalImages();
        Task<Tradesman> GetTradesmanById(long tradesmanId);
        List<SkillSet> GetSkillSetByTradesmanId(long tradesmanId, bool isActive);
        List<Tradesman> GetTradmanDetails(List<long> tradmanId);
        SubSkill GetSubSkillById(long subSkillId);
        //Task<bool> UpdatePersonalDetails(Tradesman tradesman);
        List<IdValueVM> GetTradesmanSkills();
        Task<Response> AddEditTradesman(Tradesman tradesman);
        Task<Response> SetTradesmanSkills(List<SkillSet> skillSets);
        Task<bool> DeleteTradesman(long id);
        long GetEntityIdByUserId(string userId);
        SkillSet GetSkillSetByTradesmanIds(long tradesmanId);
        List<Skill> GetSkillsByIds(List<long> skillIds);
        Tradesman GetTradesmanByUserId(string userId);
        List<SubSkill> GetSubSkill();
        List<SkillAndSubSkillVM> GetSubSkillList();
        List<long> GetTradesmanSkillIds(long tradesmanId);
        string GetTradesmanSkillName(long skillId);
        List<TradesmanEmailVM> SP_GetTradesmanWithSkillIdAndCityId(long skillId, string cityName);
        List<AdminTradesmanListVM> SpGetTradesmanList(GenericUserVM genericUserVM);
        SpTradesmanStats SpGetTradesmanStats();
        SpBusinessProfileVM SpGetBusinessDetails(long userId, string role);
        List<Tradesman> GetLAllTradesman();
        List<Tradesman> GetLAllTradesmanYearlyReport();
        List<TradesmanDTO> GetLAllTradesmanFromToReport(DateTime? StartDate, DateTime? EndDate, string skills, string tradesman, string city, bool lastActive, string location, string mobile, string cnic, string emailtype, string mobileType, string activityType, string userType);
        List<string> GetTradesmenListBySkillIdAndCityId(long skillId, string cityName);
        List<IdValueVM> GetTradesmanSkillsForDropdown();
        List<TradesmanReportbySkillVM> GetLAllTradesmanbyCategoryReport(DateTime StartDate, DateTime EndDate, string skills);
        List<Tradesman> GetTradesmanReport(List<string> userId);
        List<TradesmanReportbySkillVM> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId);
        bool UpdateTradesmanPublicId(long tradesmanId, string publicId);
        List<TradesmanDTO> GetLAllTradesmanLast24Hours(DateTime StartDate, DateTime EndDate);
        List<Tradesman> GetTradesmanAddressList();
        Response CheckSkillAvailability(string skillName, string subSkillName, int skillId, int orderBy);
        Response CheckOrderAvailability(TradesmanCommonVM tradesmanCommonVM);
        Response AddNewSkill(SkillAndSubSkillVM skill);
        Response AddOrUpdateSubSkill(SkillAndSubSkillVM subSkill);
        Response UpdateSkill(UpdateSkillVM city);
        Response DeleteSkill(int skillId, int subSkillId);
        List<GetInActiveUserVM> GetInactiveUserReport(int pageNumber, int pageSize, string dataOrderBy, DateTime? fromDate, DateTime? toDate, string city, string selectedUser);
        List<TradesmanDTO> TradesmanByCategory(TradesmanByCatVMcs tradesmanByCatVMcs);
        Response GetBusinessDetailsStatus(string id);
        List<Tradesman> GetAllActiveTradesman(bool isOrganisation);
        Response AddLinkedSalesman(string SalesmanId, string CustomerId);
        Skill GetSkillTagsBySkillId(long skillId);
        List<MetaTags> GetCommonMetaTags();
        SubSkill GetSubSkillTagsById(long subSkillId);
        Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber);
        List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo);
        Response BlockTradesman(string tradesmanId, bool status);
        Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city);

         List<SubSkillWithSkillVM> GetSubSkillsWithSkill();
    }

    public class TradesmanService : ITradesmanService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        public TradesmanService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public IQueryable<Skill> GetAllSkills(long skillId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@skillId",skillId)
                };
                var data= uow.ExecuteReaderSingleDS<Skill>("SP_GetSkillList", sqlParameters).AsQueryable();
                return data;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Skill>().AsQueryable();
            }
        }
        public IQueryable<SkillAndSubSkillVM> GetSkillListAdmin()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                return uow.ExecuteReaderSingleDS<SkillAndSubSkillVM>("SP_GetSkillListAdmin", sqlParameters).AsQueryable();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SkillAndSubSkillVM>().AsQueryable();
            }
        }

        public async Task<Tradesman> GetTradesmanById(long tradesmanId)
        {
            try
            {
                Exc.AddErrorLog(new Exception($"Tradesman Api line 127 =  {tradesmanId}"));

                return await uow.Repository<Tradesman>().GetByIdAsync(tradesmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Tradesman();
            }
        }

        public List<SkillSet> GetSkillSetByTradesmanId(long tradesmanId, bool isActive)
        {
            try
            {
                if (isActive)
                {
                    return uow.Repository<SkillSet>().Get(x => x.TradesmanId == tradesmanId && x.IsActive == isActive).ToList();
                }
                else
                {
                    return uow.Repository<SkillSet>().Get(x => x.TradesmanId == tradesmanId).ToList();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SkillSet>();
            }

        }

        public Tradesman GetPersonalDetails(long tradesmanId)
        {
            try
            {
                return uow.Repository<Tradesman>().GetById(tradesmanId);

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Tradesman();
            }
        }

        public async Task<bool> UpdatePersonalDetails(Tradesman tradesman)
        {
            try
            {
                Tradesman tradesmanOld = GetPersonalDetails(tradesman.TradesmanId);
                tradesmanOld.TradesmanId = tradesman.TradesmanId;
                tradesmanOld.LastName = tradesman.LastName;
                tradesmanOld.Dob = tradesman.Dob;
                tradesmanOld.FirstName = tradesman.FirstName;
                tradesmanOld.ModifiedOn = tradesman.ModifiedOn;
                tradesmanOld.ModifiedBy = tradesman.ModifiedBy;
                tradesmanOld.Gender = tradesman.Gender;
                tradesmanOld.EmailAddress = tradesman.EmailAddress;
                tradesmanOld.Cnic = tradesman.Cnic;
                tradesmanOld.MobileNumber = tradesman.MobileNumber;


                uow.Repository<Tradesman>().Update(tradesmanOld);
                await uow.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public List<IdValueVM> GetTradesmanSkills()
        {
            try
            {
                return uow.Repository<Skill>().GetAll().Where(x => x.IsActive == true).Select(x => new IdValueVM { Id = x.SkillId, Value = x.Name }).OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<Response> AddEditTradesman(Tradesman tradesman)
        {
            Response response = new Response();
            try
            {
                var tradesmanId = uow.Repository<Tradesman>().GetAll().Where(x => x.UserId == tradesman.UserId).Select(s => s.TradesmanId).FirstOrDefault();
                tradesman.TradesmanId = tradesman.TradesmanId != 0 ? tradesman.TradesmanId : tradesmanId;
                if (tradesman.TradesmanId > 0)
                {
                    Tradesman existingData = GetPersonalDetails(tradesman.TradesmanId);
                    if (existingData != null)
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        string jsonValues = JsonConvert.SerializeObject(tradesman, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<Tradesman>().Update(existingData);

                    }
                }
                else
                {
                    await uow.Repository<Tradesman>().AddAsync(tradesman);
                }
                await uow.SaveAsync();

                response.ResultData = tradesman;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public Skill GetSkillBySkillId(long skillId)
        {
            try
            {
                return uow.Repository<Skill>().GetById(skillId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Skill();
            }
        }

        public List<SubSkill> GetSubSkillBySkillId(long skillId)
        {
            try
            {
                return uow.Repository<SubSkill>().GetAll().Where(s => s.SkillId == skillId && s.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SubSkill>();
            }
        }
        public SubSkill GetSubSkillbySubSkillId(long subSkillId)
        {
            SubSkill subskillObj = new SubSkill();
            try
            {
                subskillObj = uow.Repository<SubSkill>().GetAll().Where(s => s.SubSkillId == subSkillId && s.IsActive == true).FirstOrDefault();
                return subskillObj;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubSkill();
            }
        }
        public async Task<Response> SetTradesmanSkills(List<SkillSet> skillSets)
        {
            Response response = new Response();
            IRepository<SkillSet> repository = uow.Repository<SkillSet>();

            try
            {
                IQueryable<SkillSet> deleteQuery = repository.GetAll().Where(s => s.TradesmanId == skillSets.FirstOrDefault().TradesmanId && s.IsActive == true);
                await repository.DeleteAllAsync(deleteQuery);

                foreach (var skillSet in skillSets ?? new List<SkillSet>())
                {
                    if (skillSet.SkillId != 0)
                    {
                        await repository.AddAsync(skillSet);
                    }
                }


                await uow.SaveAsync();

                response.Message = "Successfully updated.";
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



        public async Task<bool> DeleteTradesman(long id)
        {
            try
            {
                await uow.Repository<Tradesman>().DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }

        }

        public Tradesman GetTradesmanByTradesmanId(long tradesmanId)
        {
            try
            {
                return uow.Repository<Tradesman>().GetById(tradesmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Tradesman();
            }
        }

        public List<Skill> GetLocalProfessionalImages()
        {
            try
            {
                return uow.Repository<Skill>().GetAll().ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Skill>();
            }
        }

        public List<SubSkill> GetTradesmanSubSkills(long skillId)
        {
            try
            {
                return uow.Repository<SubSkill>().GetAll().Where(s => s.SkillId == skillId && s.IsActive == true).OrderBy(s => s.Name).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SubSkill>();
            }
        }

        public List<Tradesman> GetTradesmanDetailsByTradesmanIds(List<long> tradesmanIds)
        {
            try
            {
                return uow.Repository<Tradesman>().GetAll().Where(x => tradesmanIds.Contains(x.TradesmanId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }
        }

        public List<Tradesman> GetTradmanDetails(List<long> tradmanId)
        {
            try
            {
                List<Tradesman> tradesmen = new List<Tradesman>();
                tradesmen = uow.Repository<Tradesman>().GetAll().Where(x => tradmanId.Contains(x.TradesmanId)).ToList();

                return tradesmen;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }

        }

        public SubSkill GetSubSkillById(long SubSkillId)
        {
            try
            {
                return uow.Repository<SubSkill>().GetById(SubSkillId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubSkill();
            }
        }

        public long GetEntityIdByUserId(string userId)
        {
            try
            {
                return uow.Repository<Tradesman>().GetAll().FirstOrDefault(t => t.UserId == userId)?.TradesmanId ?? 0;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public SkillSet GetSkillSetByTradesmanIds(long tradesmanId)
        {
            try
            {
                SkillSet skillSet = uow.Repository<SkillSet>().GetAll().Where(x => x.TradesmanId == tradesmanId && x.IsActive == true).FirstOrDefault();
                return skillSet;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SkillSet();
            }

        }

        public List<Skill> GetSkillsByIds(List<long> skillIds)
        {
            try
            {
                return uow.Repository<Skill>().GetAll().Where(x => skillIds.Contains(x.SkillId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Skill>();
            }
        }

        public Tradesman GetTradesmanByUserId(string userId)
        {
            try
            {
                return uow.Repository<Tradesman>().GetAll().FirstOrDefault(x => x.UserId == userId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Tradesman();
            }
        }

        public List<SubSkill> GetSubSkill()
        {
            try
            {
                return uow.Repository<SubSkill>().GetAll().Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SubSkill>();
            }
        }
        public List<SkillAndSubSkillVM> GetSubSkillList()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                return uow.ExecuteReaderSingleDS<SkillAndSubSkillVM>("SP_GetSubSkills", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SkillAndSubSkillVM>();
            }
        }

        public List<long> GetTradesmanSkillIds(long tradesmanId)
        {
            try
            {
                return uow.Repository<SkillSet>().Get(x => x.TradesmanId == tradesmanId).Select(s => s.SkillId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<long>();
            }
        }

        public string GetTradesmanSkillName(long skillId)
        {
            try
            {
                return uow.Repository<Skill>().Get().Where(x => x.SkillId == skillId).Select(s => s.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
        }

        public List<TradesmanEmailVM> SP_GetTradesmanWithSkillIdAndCityId(long skillId, string cityName)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SkillId",skillId),
                    new SqlParameter("@CityName",cityName)
                };

                return uow.ExecuteReaderSingleDS<TradesmanEmailVM>("SP_GetTradesmanWithSkillIdAndCityId", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanEmailVM>();
            }

        }

        public List<AdminTradesmanListVM> SpGetTradesmanList(GenericUserVM genericUserVM)

        {
            List<AdminTradesmanListVM> TradeManList = new List<AdminTradesmanListVM>();
            try
            {

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@IsOrganisation",genericUserVM.isOrganisation),
                    new SqlParameter("@pageSize",genericUserVM.pageSize),
                    new SqlParameter("@pageNumber",genericUserVM.pageNumber),
                    new SqlParameter("@dataOrderBy",genericUserVM.dataOrderBy),
                    new SqlParameter("@tradesmanName",genericUserVM.userName),
                    new SqlParameter("@startDate",genericUserVM.startDate),
                    new SqlParameter("@endDate",genericUserVM.endDate),
                    new SqlParameter("@city",genericUserVM.city),
                    new SqlParameter("@skills",genericUserVM.skills),
                    new SqlParameter("@location",genericUserVM.location),
                    new SqlParameter("@cnic",genericUserVM.cnic),
                    new SqlParameter("@usertype",genericUserVM.usertype),
                    new SqlParameter("@emailtype",genericUserVM.emailtype),
                    new SqlParameter("@mobileType",genericUserVM.mobileType),
                    new SqlParameter("@activityType",genericUserVM.activityType),
                    new SqlParameter("@mobile",genericUserVM.mobile),
                    new SqlParameter("@sourceOfReg",genericUserVM.sourceOfReg),
                    new SqlParameter("@email",genericUserVM.email),
                    new SqlParameter("@SalesmanId",genericUserVM.SalesmanId),
                    new SqlParameter("@tradesmanId",genericUserVM.tradesmanId),
                };
                TradeManList = uow.ExecuteReaderSingleDS<AdminTradesmanListVM>("Sp_GetTradesmanList", sqlParameters);
                return TradeManList;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AdminTradesmanListVM>();
            }

        }

        public SpTradesmanStats SpGetTradesmanStats()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                return uow.ExecuteReaderSingleDS<SpTradesmanStats>("Sp_TradesmanStats", sqlParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpTradesmanStats();
            }

        }

        public SpBusinessProfileVM SpGetBusinessDetails(long userId, string role)
        {
            SpBusinessProfileVM spBusinessProfileVM;

            //List<TrradesmanJobsFeedbackVM> trradesmanJobsFeedbackVM;
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@userId",userId),
                    new SqlParameter("@role",role)
                };
                spBusinessProfileVM = uow.ExecuteReaderSingleDS<SpBusinessProfileVM>("Sp_GetBusinessProfile", sqlParameters).FirstOrDefault();
                if (role == "Tradesman")
                {
                    SqlParameter[] sqlParameters1 =
                   {
                    new SqlParameter("@tradesmanID",userId),
                };
                    spBusinessProfileVM.trradesmanJobsFeedbackVMs = uow.ExecuteReaderSingleDS<TrradesmanJobsFeedbackVM>("TradesmanJobsRating", sqlParameters1);
                }
                return spBusinessProfileVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SpBusinessProfileVM();
            }
        }

        public List<Tradesman> GetLAllTradesman()
        {
            try
            {
                return uow.Repository<Tradesman>().GetAll().ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }
        }
        public List<Tradesman> GetAllActiveTradesman(bool isOrganisation)
        {
            try
            {
                return uow.Repository<Tradesman>().GetAll().Where(x => x.FirstName != "").Select(x => new Tradesman { FirstName = x.FirstName + ' ' + x.LastName, UserId = x.UserId, IsOrganization = x.IsOrganization }).ToList();

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }
        }

        public List<Tradesman> GetLAllTradesmanYearlyReport()
        {
            try
            {
                var res = uow.Repository<Tradesman>().GetAll().Where(x => x.CreatedOn.Year == DateTime.Now.Year);
                return res.ToList();
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }
        }

        public List<TradesmanDTO> GetLAllTradesmanFromToReport(DateTime? StartDate, DateTime? EndDate, string skills, string tradesman, string city, bool lastActive, string location, string mobile, string cnic, string emailtype, string mobileType, string activityType, string userType)
        {
            try
            {
                SqlParameter[] sqlParameters =
            {
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@endDate",EndDate),
                    new SqlParameter("@skill", skills),
                    new SqlParameter("@tradesman",tradesman),
                    new SqlParameter("@city",city),
                    new SqlParameter("@lastActive",lastActive),
                    new SqlParameter("@location",location),
                    new SqlParameter("@mobile",mobile),
                    new SqlParameter("@emailtype",emailtype),
                    new SqlParameter("@mobileType",mobileType),
                    new SqlParameter("@cnic",cnic),
                    new SqlParameter("@activityType",activityType),
                    new SqlParameter("@userType",userType),
                };
                var rstd = uow.ExecuteReaderSingleDS<TradesmanDTO>("Sp_TradesmanRegistretionDynamic_Report", sqlParameters);
                return rstd;
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<TradesmanDTO>();
            }
        }
        public List<TradesmanReportbySkillVM> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                                {
                    //new SqlParameter("@stDte",StartDate),
                    //new SqlParameter("@endDte",EndDate),
                    new SqlParameter("@skill", skills),
                    new SqlParameter("@state",town),
                    new SqlParameter("@tradesmanId",tradesmanId),

                };

                var rstd = uow.ExecuteReaderSingleDS<TradesmanReportbySkillVM>("SP_GetTradesmanListBySkillAndTown", sqlParameters);
                return rstd;
            }

            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<TradesmanReportbySkillVM>();
            }
        }
        public List<TradesmanDTO> GetLAllTradesmanLast24Hours(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@FromDate",StartDate),
                    new SqlParameter("@ToDate",EndDate)

                };
                var rstd = uow.ExecuteReaderSingleDS<TradesmanDTO>("Sp_PrimaryTradesman_Report", sqlParameters);

                return rstd;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanDTO>();
            }
        }
        public List<string> GetTradesmenListBySkillIdAndCityId(long skillId, string _cityName)
        {

            var tradesmanIds = uow.Repository<SkillSet>().GetAll().Where(s => s.SkillId == skillId).Select(x => x.TradesmanId).ToList();
            return uow.Repository<Tradesman>().GetAll().Where(t => tradesmanIds.Contains(t.TradesmanId) && t.City == _cityName).Select(x => x.UserId).ToList();
        }
        public List<IdValueVM> GetTradesmanSkillsForDropdown()
        {
            try
            {
                return uow.Repository<Skill>().GetAll().Where(x => x.IsActive == true).Select(s => new IdValueVM { Id = s.SkillId, Value = s.Name }).ToList();
            }
            catch (Exception exp)
            {
                Exc.AddErrorLog(exp);
                return null;
            }


        }

        public List<Tradesman> GetTradesmanReport(List<string> userId)
        {
            try
            {
                List<Tradesman> tradesmen = new List<Tradesman>();

                tradesmen = uow.Repository<Tradesman>().GetAll().Where(t => userId.Contains(t.UserId)).ToList();
                return tradesmen;
            }
            catch (Exception exp)
            {

                Exc.AddErrorLog(exp);
                return null;
            }

        }
        public List<TradesmanReportbySkillVM> GetLAllTradesmanbyCategoryReport(DateTime StartDate, DateTime EndDate, string skills)
        {
            try
            {
                //string rawCSV = "170801/1,170801/2,170801/3";
                string[] ids = skills.Split(',');
                string rst = "";
                foreach (var id in ids)
                {
                    rst += id + ",";

                }
                rst = rst.TrimEnd(',');
                string command = " SELECT DISTINCT s.SkillId,sk.Name, s.TradesmanId, t.* from SkillSet s " +
                                     "inner join Tradesman t on t.TradesmanId = s.TradesmanId " +
                                      "inner join Skill sk on sk.SkillId = s.SkillId " +
                                      "where s.SkillId  in (" + rst + ") " +
                                      "and s.CreatedOn >=" + "'" + StartDate + "'" + " and s.CreatedOn <=" + "'" + EndDate + "'" +
                                      " order by sk.name";

                SqlParameter[] sqlParameters =
                        {
                    new SqlParameter("@startdte",StartDate),
                    new SqlParameter("@enddte",EndDate),
                    new SqlParameter("@skills",rst),

                };



                var rstd = uow.ExecuteCommand<TradesmanReportbySkillVM>(command).ToList();
                return rstd;
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return new List<TradesmanReportbySkillVM>();
            }
        }

        public bool UpdateTradesmanPublicId(long tradesmanId, string publicId)
        {
            try
            {
                Tradesman tradesman = uow.Repository<Tradesman>().GetById(tradesmanId);
                if (tradesman != null)
                {
                    tradesman.PublicId = publicId;

                    uow.Repository<Tradesman>().Update(tradesman);
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
        public List<Tradesman> GetTradesmanAddressList()
        {
            try
            {
                List<Tradesman> customersAddressList = new List<Tradesman>();
                customersAddressList = uow.Repository<Tradesman>().GetAll().Select(x => new Tradesman { ShopAddress = x.ShopAddress }).Where(x => x.ShopAddress != null && x.ShopAddress != "").ToList();
                return customersAddressList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<Tradesman>();
            }
        }
        public Response BlockTradesMan(string tradesmanId, bool status)
        {
            Response response = new Response();
            try
            {
                int id = Convert.ToInt32(tradesmanId);
                var istradesman = uow.Repository<Tradesman>().GetById(id);
                if (istradesman != null)
                {
                    if (status)
                    {
                        istradesman.IsActive = true;
                    }
                    else
                    {
                        istradesman.IsActive = false;

                    }
                    uow.Repository<Tradesman>().Update(istradesman);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Tradesman status has been changed!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error in changing Tradesman status!";
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

        public async Task<Response> UpdatePhoneNumberByUserId(string userId, string phoneNumber)
        {
            Response response = new Response();

            try
            {
                Tradesman tradesman = uow.Repository<Tradesman>().GetAll()?.FirstOrDefault(x => x.UserId == userId);

                if (tradesman != null)
                {
                    tradesman.MobileNumber = phoneNumber;

                    uow.Repository<Tradesman>().Update(tradesman);
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

        public Response CheckSkillAvailability(string skillName, string subSkillName, int skillId, int orderBy)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(skillName) && orderBy > 0)
                {
                    var isSkill = uow.Repository<Skill>().Get(x => x.Name == skillName && x.OrderByColumn == orderBy).FirstOrDefault();
                    if (isSkill != null)
                    {
                        response.Message = "skillAndOrderSame";
                    }
                    else
                    {
                        response.Message = "skillAndOrderNoSame";
                    }
                    return response;
                }
                if (!string.IsNullOrEmpty(subSkillName) && skillId > 0)
                {
                    var isSkill = uow.Repository<SubSkill>().Get(x => x.Name == subSkillName).FirstOrDefault();
                    if (isSkill != null)
                    {
                        if (isSkill.SkillId == skillId)
                            response.Message = "subSkillWithSkillId";
                        else
                            response.Message = "false";
                        return response;
                    }
                }
                if (!string.IsNullOrEmpty(skillName))
                {
                    var isSkill = uow.Repository<Skill>().Get(x => x.Name == skillName).FirstOrDefault();
                    if (isSkill != null)
                        response.Message = "true";
                    else
                        response.Message = "false";
                }
                if (!string.IsNullOrEmpty(subSkillName))
                {
                    var isSubSkill = uow.Repository<SubSkill>().Get(x => x.Name == subSkillName).FirstOrDefault();
                    if (isSubSkill != null)
                        response.Message = "true";
                    else
                        response.Message = "false";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;

        }
        public Response CheckOrderAvailability(TradesmanCommonVM tradesmanCommonVM)
        {
            Response response = new Response();
            try
            {
                var isOrder = uow.Repository<Skill>().Get(x => x.OrderByColumn == tradesmanCommonVM.OrderBy).FirstOrDefault();
                if (isOrder != null)
                {
                    response.Message = "orderByTrue";
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;

        }
        public Response AddOrUpdateSubSkill(SkillAndSubSkillVM subSkill)
        {
            SubSkill modelSubSkill = new SubSkill();
            Response response = new Response();
            try
            {
                if (subSkill.SubSkillId > 0)
                {
                    var findBySubSkillName = uow.Repository<SubSkill>().Get(x => x.Name.ToLower() == subSkill.SubSkillName.ToLower() && x.SubSkillId == subSkill.SubSkillId).FirstOrDefault();
                    if (findBySubSkillName?.SubSkillId == subSkill?.SubSkillId | findBySubSkillName == null)
                    {
                        var isSubSkill = uow.Repository<SubSkill>().Get(x => x.SubSkillId == subSkill.SubSkillId).FirstOrDefault();
                        if (isSubSkill != null)
                        {
                            if (subSkill.Base64Image != null)
                            {
                                var splitName = subSkill?.Base64Image.Split(',');
                                if (splitName[0] == "")
                                    isSubSkill.SubSkillImage = !string.IsNullOrEmpty(subSkill.Base64Image) ? Convert.FromBase64String(subSkill.Base64Image) : new byte[0];
                                else
                                {
                                    string convert = subSkill.Base64Image.Replace(splitName[0] + ",", String.Empty);
                                    var convertedImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                    isSubSkill.SubSkillImage = convertedImage;
                                }
                            }
                            else
                            {
                                isSubSkill.SubSkillImage = null;
                            }
                            if (string.IsNullOrEmpty(subSkill.ImagePath))
                            {
                                subSkill.ImagePath = null;
                            }
                            isSubSkill.ModifiedOn = DateTime.Now;
                            isSubSkill.ModifiedBy = subSkill.ModifiedBy;
                            isSubSkill.Name = subSkill.SubSkillName;
                            isSubSkill.SkillId = subSkill.SkillId;
                            isSubSkill.Description = subSkill.Description;
                            isSubSkill.MetaTags = subSkill.MetaTags;
                            isSubSkill.SubSkillTitle = subSkill.SubSkillTitle;
                            isSubSkill.Slug = subSkill.Slug;
                            isSubSkill.ImagePath = subSkill.ImagePath;
                            isSubSkill.SubSkillPrice = subSkill.SubSkillPrice;
                            isSubSkill.VisitCharges = subSkill.VisitCharges;
                            isSubSkill.PriceReview = subSkill.PriceReview;
                            uow.Repository<SubSkill>().Update(isSubSkill);
                            uow.Save();
                            response.Status = ResponseStatus.OK;
                            response.Message = "SubSkill Updated successfully";
                            return response;
                        }
                        else
                        {
                            response.Message = "SubSkill is not found";
                            return response;
                        }
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "SubSkill name alrady exist";
                        return response;
                    }

                }
                else
                {
                    var findBySubSkillName = uow.Repository<SubSkill>().Get(x => x.Name.ToLower() == subSkill.SubSkillName.ToLower()).FirstOrDefault();
                    if (findBySubSkillName == null)
                    {
                        if (subSkill.Base64Image != null)
                        {
                            var splitName = subSkill.Base64Image.Split(',');
                            string convert = subSkill.Base64Image.Replace(splitName[0] + ",", String.Empty);
                            modelSubSkill.SubSkillImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                        }
                        modelSubSkill.SkillId = subSkill.SkillId;
                        modelSubSkill.CreatedBy = subSkill.CreatedBy;
                        modelSubSkill.Name = subSkill.SubSkillName;
                        modelSubSkill.Description = subSkill.Description;
                        modelSubSkill.MetaTags = subSkill.MetaTags;
                        modelSubSkill.SubSkillTitle = subSkill.SubSkillTitle;
                        modelSubSkill.IsActive = subSkill.IsActive;
                        modelSubSkill.CreatedOn = DateTime.Now;
                        modelSubSkill.Slug = subSkill.Slug;
                        modelSubSkill.ImagePath = subSkill.ImagePath;
                        modelSubSkill.SubSkillPrice = subSkill.SubSkillPrice;
                        modelSubSkill.VisitCharges = subSkill.VisitCharges;
                        modelSubSkill.PriceReview = subSkill.PriceReview;
                        uow.Repository<SubSkill>().Add(modelSubSkill);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "SubSkill added successfully";
                        return response;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "SubSkill name alrady exist";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
        public Response AddNewSkill(SkillAndSubSkillVM skill)
        {
            Skill skill1 = new Skill();
            Response response = new Response();
            try
            {
                var getSkillName = uow.Repository<Skill>().Get(x => x.Name.ToLower() == skill.SkillName.ToLower()).FirstOrDefault();
                if (getSkillName == null)
                {
                    if (skill.Base64Image != null)
                    {
                        var splitName = skill.Base64Image.Split(',');
                        string convert = skill.Base64Image.Replace(splitName[0] + ",", String.Empty);
                        skill1.SkillImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    skill1.CreatedBy = skill.CreatedBy;
                    skill1.Name = skill.SkillName;
                    skill1.SkillTitle = skill.SkillTitle;
                    skill1.MetaTags = skill.MetaTags;
                    skill1.Description = skill.Description;
                    skill1.OrderByColumn = skill.OrderByColumn;
                    skill1.CreatedOn = DateTime.Now;
                    skill1.IsActive = skill.IsActive;
                    skill1.Slug = skill.Slug;
                    skill1.ImagePath = skill.ImagePath;
                    skill1.SkillIconPath = skill.SkillIconPath;
                    skill1.OgDescription = skill.OgDescription;
                    skill1.SeoPageTitle = skill.SeoPageTitle;
                    skill1.OgTitle = skill.OgTitle;
                    var getMax = uow.Repository<Skill>().GetAll().Max(x => x.OrderByColumn);
                    if (skill1.OrderByColumn <= 0)
                        skill1.OrderByColumn = getMax + 1;
                    if (skill1.OrderByColumn > getMax)
                        skill1.OrderByColumn = getMax + 1;
                    uow.Repository<Skill>().Add(skill1);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Skill added successfully";
                    return response;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Skill name alreay exists!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
        public Response UpdateSkill(UpdateSkillVM skill)
        {
            Response response = new Response();
            try
            {
                var findBySubSkillName = uow.Repository<Skill>().Get(x => x.Name.ToLower() == skill.Name.ToLower()).FirstOrDefault();
                if (findBySubSkillName?.SkillId == skill?.SkillId | findBySubSkillName == null)
                {
                    var findByOrder = uow.Repository<Skill>().Get(x => x.SkillId == skill.SkillId).FirstOrDefault(); //10029
                    var findBySkillName = uow.Repository<Skill>().Get(x => x.Name == skill.Name).FirstOrDefault(); //24
                    //if (findBySkillName == null)
                    //{
                    //    findByOrder.Name = skill.Name;
                    //    //can add new skill
                    //}
                    //else if (findByOrder.Name == findBySkillName.Name)
                    //{
                    //    findByOrder.Name = findBySkillName.Name;
                    //}
                    //else
                    //{
                    //    response.Message = "SkillName Already Exist";
                    //    response.Status = ResponseStatus.Error;
                    //    return response;
                    //}
                    if (findByOrder != null)
                    {
                        if (skill.Base64Image != null)
                        {
                            var splitName = skill.Base64Image.Split(',');
                            string convert = skill.Base64Image.Replace(splitName[0] + ",", String.Empty);
                            findByOrder.SkillImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                        }
                     
                        if(string.IsNullOrEmpty(skill.ImagePath))
                        {
                            skill.ImagePath = null;
                        }
                        var shiftTo = uow.Repository<Skill>().Get(x => x.OrderByColumn == skill.OrderByColumn).FirstOrDefault();
                        findByOrder.Name = skill.Name;
                        findByOrder.OrderByColumn = skill.OrderByColumn;
                        findByOrder.ModifiedBy = skill.ModifiedBy;
                        findByOrder.ModifiedOn = DateTime.Now;
                        findByOrder.Description = skill.Description;
                        findByOrder.MetaTags = skill.MetaTags;
                        findByOrder.SkillTitle = skill.SkillTitle;
                        findByOrder.Slug = skill.Slug;
                        findByOrder.ImagePath = skill.ImagePath;
                        findByOrder.SkillIconPath = skill.SkillIconPath;
                        findByOrder.OgDescription = skill.OgDescription;
                        findByOrder.SeoPageTitle = skill.SeoPageTitle;
                        findByOrder.OgTitle = skill.OgTitle;
                        int getMax = (int)uow.Repository<Skill>().GetAll().Max(x => x.OrderByColumn);
                        if (skill.OrderByColumn > getMax)
                        {
                            findByOrder.OrderByColumn = getMax + 1;
                        }
                        uow.Repository<Skill>().Update(findByOrder);
                        uow.Save();
                        var skillsList = uow.Repository<Skill>().GetAll().Where(x => x.OrderByColumn >= skill.OrderByColumn).ToList();
                        foreach (var item in skillsList)
                        {
                            if (item != null && item.OrderByColumn != skill.OrderByColumn)
                            {
                                item.OrderByColumn++;
                                uow.Repository<Skill>().Update(item);
                                uow.Save();
                            }
                            else
                            {
                                if (shiftTo != null && shiftTo.SkillId == item.SkillId && skillsList.Count > 1)
                                {
                                    item.OrderByColumn++;
                                    uow.Repository<Skill>().Update(item);
                                    uow.Save();
                                }
                            }

                        }
                        response.Status = ResponseStatus.OK;
                        response.Message = "Skill updated successfully";
                        return response;
                    }
                    else
                    {
                        //Skill isSkill = uow.Repository<Skill>().Get(x => x.SkillId == skill.SkillId).FirstOrDefault();
                        //isSkill.ModifiedOn = DateTime.Now;
                        //isSkill.ModifiedBy = skill.ModifiedBy;
                        //isSkill.Name = skill.Name;
                        //isSkill.OrderByColumn = skill.OrderByColumn;
                        //uow.Repository<Skill>().Update(isSkill);
                        //uow.Save();
                        response.Status = ResponseStatus.Error;
                        response.Message = "Something went wrong!";
                        return response;
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Skill already exists!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
        public Response DeleteSkill(int skillId, int subSkillId)
        {
            Response response = new Response();
            try
            {
                if (skillId > 0)
                {
                    Skill isSkill = uow.Repository<Skill>().Get(x => x.SkillId == skillId).FirstOrDefault();
                    if (isSkill.IsActive == true)
                        isSkill.IsActive = false;
                    else
                        isSkill.IsActive = true;
                    uow.Repository<Skill>().Update(isSkill);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "skflase";
                }
                if (subSkillId > 0)
                {
                    SubSkill isSkill = uow.Repository<SubSkill>().Get(x => x.SubSkillId == subSkillId).FirstOrDefault();
                    if (isSkill.IsActive == true)
                        isSkill.IsActive = false;
                    else
                        isSkill.IsActive = true;
                    uow.Repository<SubSkill>().Update(isSkill);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "sstrue";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
        public List<GetInActiveUserVM> GetInactiveUserReport(int pageNumber, int pageSize, string dataOrderBy, DateTime? fromDate, DateTime? toDate, string city, string selectedUser)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@pageNumber",pageNumber),
                    new SqlParameter("@pageSize",pageSize),
                    new SqlParameter("@dataOrderBy",dataOrderBy),
                    new SqlParameter("@fromDate",fromDate),
                    new SqlParameter("@toDate",toDate),
                    new SqlParameter("@city",city),
                    new SqlParameter("@selectedUser",selectedUser),

                };
                List<GetInActiveUserVM> inactiveUsers = uow.ExecuteReaderSingleDS<GetInActiveUserVM>("Sp_InactiveUserRepor", sqlParameters);

                return inactiveUsers;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<GetInActiveUserVM>();
            }
        }

        public List<TradesmanDTO> TradesmanByCategory(TradesmanByCatVMcs tradesmanByCatVMcs)
        {
            try
            {
                var let = uow.Repository<Tradesman>().GetAll();
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@cities",tradesmanByCatVMcs.cityIds),
                    new SqlParameter("@skills",tradesmanByCatVMcs.skillIds),
                    new SqlParameter("@isOrgnization",tradesmanByCatVMcs.isOrgnization),
                    new SqlParameter("@location",tradesmanByCatVMcs.location),
                    new SqlParameter("@usertype",tradesmanByCatVMcs.usertype),
                    new SqlParameter("@mobileType",tradesmanByCatVMcs.mobileType),
                    new SqlParameter("@emailtype",tradesmanByCatVMcs.emailtype),
                    new SqlParameter("@activityType",tradesmanByCatVMcs.activityType),
                    new SqlParameter("@startDate",tradesmanByCatVMcs.startDate),
                    new SqlParameter("@endDate",tradesmanByCatVMcs.endDate),
                };
                List<TradesmanDTO> tradesmanDTOs = uow.ExecuteReaderSingleDS<TradesmanDTO>("TradesmanByCategory", sqlParameters);

                return tradesmanDTOs;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<TradesmanDTO>();
            }
        }

        public Response GetBusinessDetailsStatus(string id)
        {
            Response response = new Response();
            Tradesman tradesman = uow.Repository<Tradesman>().Get(x => x.UserId == id).FirstOrDefault();

            if (tradesman.City != null && tradesman.ShopAddress != null)
            {
                response.Status = ResponseStatus.OK;
                response.ResultData = "Successfully";
            }
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Tradesman are notv exist";
            }


            return response;
        }
        public Response AddLinkedSalesman(string SalesmanId, string CustomerId)
        {
            Response res = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@SalesmanId",SalesmanId),
                    new SqlParameter("@CustomerId",CustomerId)

                };
                var data = uow.ExecuteReaderSingleDS<Response>("Sp_AddTradesmanRegisterBy", sqlParameters);
                return res;
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
                return res;
            }

        }
        public Skill GetSkillTagsBySkillId(long skillId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@skillId",skillId)
                };
                return uow.ExecuteReaderSingleDS<Skill>("SP_GetSkillListBySkillId", sqlParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Skill();
            }
        }
        public List<MetaTags> GetCommonMetaTags()
        {
            try
            {
                SqlParameter[] sqlParameters = { };
                return uow.ExecuteReaderSingleDS<MetaTags>("SP_GetCommonmetaTags", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<MetaTags>();
            }
        }
        public SubSkill GetSubSkillTagsById(long subSkillId)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@subSkillId",subSkillId)
                };
                return uow.ExecuteReaderSingleDS<SubSkill>("SP_GetSubSkillBySubSkillId", sqlParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SubSkill();
            }
        }
        public List<PersonalDetailVM> GetTradesmanByName(string tradesmanName, long tradesmanId, string tradesmanPhoneNo)
        {
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@tradesmanName",tradesmanName),
                new SqlParameter("@tradesmanId",tradesmanId),
                new SqlParameter("@tradesmanPhoneNo",tradesmanPhoneNo),
                };
                var respone = uow.ExecuteReaderSingleDS<PersonalDetailVM>("SP_GetTradesmanByName", sqlParameters).ToList();
                return respone;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PersonalDetailVM>();
            }
        }

        public List<SubSkill> GetSubSkillsBySkillId(long subSkillId)
        {
            List<SubSkill> subSkills = new List<SubSkill>();
            try
            {
                SqlParameter[] sqlParameters = {
                new SqlParameter("@subSkillId",subSkillId)
                };
                subSkills = uow.ExecuteReaderSingleDS<SubSkill>("SP_GetSubSkillBySkillId", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return subSkills;
        }

        public Response BlockTradesman(string tradesmanId, bool status)
        {
            Response response = new Response();
            try
            {
                int id = Convert.ToInt32(tradesmanId);
                var isTradesman = uow.Repository<Tradesman>().GetById(id);
                if (isTradesman != null)
                {
                    if (status)
                    {
                        isTradesman.IsActive = true;
                    }
                    else
                    {
                        isTradesman.IsActive = false;
                    }
                    uow.Repository<Tradesman>().Update(isTradesman);
                    uow.SaveAsync();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Tradesman status has been changed!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Error in changing Tradesman status!";
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
        public async Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city)
        {
            Response response = new Response();
            try
            {
                if (categoryId > 0 && !String.IsNullOrWhiteSpace(city))
                {
                    SqlParameter[] sqlParameters = {
                    new SqlParameter("@categoryId",categoryId),
                    new SqlParameter("@cityName",city),
                    };
                    response.ResultData = await uow.ExecuteReaderSingleDSNew<PersonalDetailVM>("Sp_GetTradesmanFirebaseIdListBySkillAndCity", sqlParameters);
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "category or city name is nulll";
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

        public List<SubSkillWithSkillVM> GetSubSkillsWithSkill()
        {
            List<SubSkillWithSkillVM> subSkills = new List<SubSkillWithSkillVM>();
            try
            {
                SqlParameter[] sqlParameters = {
                };
                subSkills = uow.ExecuteReaderSingleDS<SubSkillWithSkillVM>("SP_GetSubSkillWithSkill", sqlParameters);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
            return subSkills;
        }
    }
}
