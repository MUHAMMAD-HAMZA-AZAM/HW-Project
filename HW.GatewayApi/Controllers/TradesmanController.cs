using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.IdentityViewModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageVM = HW.TradesmanViewModels.ImageVM;

namespace HW.GatewayApi.Controllers
{
    [Produces("application/json")]
    public class TradesmanController : BaseController
    {
        private readonly ITradesmanService tradesmanService;

        public TradesmanController(ITradesmanService tradesmanService, IUserManagementService userManagementService) : base(userManagementService)
        {
            this.tradesmanService = tradesmanService;
        }

        [HttpGet]
        public async Task<string> GetAllSkills()
        {
            return await tradesmanService.GetAllSkills();
        }

        [HttpGet]
        public async Task<List<BidVM>> GetActiveBids([FromQuery]int bidsStatusId)
        {
            List<BidVM> activeJobs = await tradesmanService.GetActiveBids(await GetEntityIdByUserId(), bidsStatusId);
            return activeJobs;
        }

        [HttpGet]
        public async Task<List<BidVM>> GetDeclinedBids([FromQuery]int bidsStatusId)
        {
            List<BidVM> activeJobs = await tradesmanService.GetDeclinedBids(await GetEntityIdByUserId(), bidsStatusId);
            return activeJobs;
        }

        [HttpGet]
        public async Task<List<JobLeadsVM>> GetJobLeadsByTradesmanId(int pageNumber,long tradesmanid)
        {
            List<JobLeadsVM> result = new List<JobLeadsVM>();

           // long entityId = await GetEntityIdByUserId();

            //if (entityId > 0)
            //{
                result = await tradesmanService.GetJobLeadsByTradesmanId(tradesmanid,pageNumber);
            //}
            return result;
        }

        [HttpGet]
        public async Task<Response> GetJobsDetails([FromQuery]int jobStatusId)
        {
            Response response = await tradesmanService.GetJobsDetail(await GetEntityIdByUserId(), jobStatusId);
            return response;
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer,  UserRoles.Organization })]
        public async Task<CompletedJobDetailVM> GetCompletedJob([FromQuery] long jobDetailId)
        {
            return await tradesmanService.GetCompletedJob(jobDetailId, await GetEntityIdByUserId());
        }

        [HttpGet]
        public async Task<JobQuotationEditBidVM> jobQuotationEditBid(long jobQuotationId)
        {
            JobQuotationEditBidVM jobs = await tradesmanService.GetJobQuotationsEditBid(jobQuotationId);
            return jobs;
        }
        
        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman , UserRoles.Organization })]
        public async Task<Response> SubmitBid([FromBody]EditBidVM editBidVM)
        {
            editBidVM.CreatedBy = DecodeTokenForUser().Id;
            editBidVM.TradesmanId = await GetEntityIdByUserId();
            return await tradesmanService.SubmitBid(editBidVM);
        }
        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman , UserRoles.Organization })]
        public async Task<Response> SubmitBidsVoice([FromBody]EditBidVM editBidVM, string resultData = "")
        {
            editBidVM.CreatedBy = DecodeTokenForUser().Id;
            editBidVM.TradesmanId = await GetEntityIdByUserId();
            return await tradesmanService.SubmitBidsVoice(editBidVM, resultData);
        }
        
        [HttpGet]
        public async Task<CallInfoVM> GetCallInfo(long customerId, bool todaysRecordOnly = false)
        {
            return await tradesmanService.GetCallInfo(await GetEntityIdByUserId(), customerId, todaysRecordOnly);
        }

        [HttpGet]
        public async Task<List<UserViewModels.CallHistoryVM>> GetTradesmanCallLogs()
        {
            return await tradesmanService.GetTradesmanCallLogs(await GetEntityIdByUserId());
        }

        [HttpPost]
        public async Task<bool> DeleteCallLogs([FromBody]List<long> selectedCallLogIds)
        {
            return await tradesmanService.DeleteCallLogs(selectedCallLogIds);
        }

        [HttpGet]
        //[Permission(new string[] {  UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Organization })]
        public async Task<PersonalDetailsVM> GetPersonalDetails()
        {
            return await tradesmanService.GetPersonalDetails(await GetEntityIdByUserId());
        }

        [HttpGet]
        public async Task<List<InvoiceVM>> GetInvoiceMembership()
        {
            return await tradesmanService.GetInvoiceMembership(await GetEntityIdByUserId());
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<InvoiceVM>> GetInvoiceJobReceipts()
        {
            return await tradesmanService.GetInvoiceJobReceipts(await GetEntityIdByUserId());
        }
        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<List<InvoiceVM>> GetInvoiceJobReceiptsById(long tradesmanId)
        {
            return await tradesmanService.GetInvoiceJobReceipts(tradesmanId);
        }
        [HttpGet]
        public async Task<List<IdValueVM>> GetTradesmanSkillsByParentId(long parentId)
        {
            return await tradesmanService.GetTradesmanSkillsByParentId(parentId);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Organization })]
        public async Task<List<MediaImagesVM>> GetJobBidImages([FromQuery] long jobQuotationId)
        {
            return await tradesmanService.GetJobBidImages(jobQuotationId);
        }

        [HttpPost]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<bool> UpdatePersonalDetails([FromBody]PersonalDetailVM personalDetailVM)
        {

            return await tradesmanService.UpdatePersonalDetails(await GetEntityIdByUserId(), personalDetailVM);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<TradesmanProfileVM> GetTradesmanProfile(long tradesmanId, bool isActive)
        {
            return await tradesmanService.GetTradesmanProfile(tradesmanId, isActive);
        }

        [HttpGet]
        public async Task<List<LocalProfessionVM>> GetLocalProfessionalImages()
        {
            return await tradesmanService.GetLocalProfessionalImages();
        }

        [HttpGet]
        public async Task<RateTradesmanVM> RateTradesmanByJobQuotationId(long jobDetailId)
        {
            long id = await GetEntityIdByUserId();
            return await tradesmanService.RateTradesmanByJobQuotationId(jobDetailId);
        }


        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Organization })]
        public async Task<List<IdValueVM>> GetTradesmanSkills()
        {
            return await tradesmanService.GetTradesmanSkills();
        }


        [HttpGet]
        public async Task<TmBusinessDetailVM> GetBusinessDetail()
        {
            long tradsmanId = await GetEntityIdByUserId();
            return await tradesmanService.GetBusinessDetail(tradsmanId);
        }

        [HttpGet]
        public async Task<UserProfileVM> GetTradesmanByUserId()
        {
            UserProfileVM result = null;
            UserRegisterVM userVM = DecodeTokenForUser();

            if (userVM != null)
            {
                result = await tradesmanService.GetTradesmanByUserId(userVM.Id);
            }

            return result;
        }

        [HttpGet]
        public async Task<List<IdValueVM>> GetSubSkillBySkillId(long tradesmanSkillId)
        {
            return await tradesmanService.GetSubSkillBySkillId(tradesmanSkillId);
        }
        
        
        [HttpGet]
        public async Task<List<IdValuePriceVM>> GetSubSkillsBySkillId(long skillId=0)
        {
            return await tradesmanService.GetSubSkillsBySkillId(skillId);
        }

        [HttpGet]
        public async Task<IdValuePriceVM> GetSubSkillbySubSkillId(long subSkillId)
        {
            IdValuePriceVM Record = new IdValuePriceVM();
            Record = await tradesmanService.GetSubSkillbySubSkillId(subSkillId);
            return Record;
        }


        [HttpGet]
        public async Task<List<IdValueVM>> GetSubSkill()
        {
            return await tradesmanService.GetSubSkill();
        }
        [HttpGet]
        public async Task<bool> CheckFeedBackStatus(long jobDetailId)
        {
            return await tradesmanService.CheckFeedBackStatus(jobDetailId);

        }
        [HttpPost]
        public async Task<Response> Login([FromBody]LoginVM model)
        {
            return await tradesmanService.Login(model);
        }

        [HttpGet]
        public async Task<List<TradesmanReportbySkillVM>> GetLAllTradesmanbySkillTown(string skills, string town, long tradesmanId)
        {
            return await tradesmanService.GetLAllTradesmanbySkillTown(skills, town,tradesmanId);
        }
      

        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public async Task<TradesManProfileDetailsVM> GetBusinessAndPersnalProfileWeb()
        {
            return await tradesmanService.GetBusinessAndPersnalProfileWeb(await GetEntityIdByUserId());
        }
        public Task<List<Tradesman>> GetTradesmanCompletedJobsFeeback()
        {
            return tradesmanService.GetTradesmanCompletedJobsFeeback();
        }

        [HttpPost]
        public async Task JobEndReminderNotification([FromBody] List<ExpiryJobNotificationVM> postAdVM)
        {
            await tradesmanService.JobEndReminderNotification(postAdVM);
        }

        [HttpGet]
        //[Permission(new string[] { UserRoles.Tradesman, UserRoles.Admin, UserRoles.Customer, UserRoles.Supplier, UserRoles.Organization })]
        public Task<Response> GetBusinessDetailsStatus ()
        {
            return tradesmanService.GetBusinessDetailsStatus(DecodeTokenForUser()?.Id);
        }
        [HttpGet]
        public async Task<List<Skill>> GetSkillList(long skillId)
        {
            return await tradesmanService.GetSkillList(skillId);
        }
        [HttpGet]
        public async Task<Skill> GetSkillTagsBySkillId(long skillId)
        {
            return await tradesmanService.GetSkillTagsBySkillId(skillId);
        }
        [HttpGet]
        public async Task<List<TradesmanViewModels.MetaTagsVM>> GetCommonMetaTags()
        {
            return await tradesmanService.GetCommonMetaTags();
        }
        [HttpGet]
        public async Task<SubSkill> GetSubSkillTagsById(long subSkillId)
        {
            return await tradesmanService.GetSubSkillTagsById(subSkillId);
        }        
        [HttpGet]
        public async Task<SubSkill> GetSubSkillById(long subSkillId)
        {
            return await tradesmanService.GetSubSkillById(subSkillId);
        }
        [HttpGet]
        public async Task<List<SubSkill>> GetSubSkillTagsBySkillId(long SkillId)
        {
            return await tradesmanService.GetSubSkillTagsBySkillId(SkillId);
        }
        [HttpGet]
        public async Task<bool> UpdateTradesmanPublicId(long tradesmanId, string publicId)
        {
            bool result;
            result = await tradesmanService.UpdateTradesmanPublicId(tradesmanId, publicId);
            return result;
        }
        [HttpGet]
        public async Task<Response> GetTradesmanFirebaseIdListBySkillAndCity(int categoryId, string city)
        {
            return await tradesmanService.GetTradesmanFirebaseIdListBySkillAndCity(categoryId, city);
        }
        [HttpGet]
        public async Task<List<SubSkillWithSkillVM>> GetSubSkillsWithSkill()
        {
            return await tradesmanService.GetSubSkillsWithSkill();
        }
    }
}
