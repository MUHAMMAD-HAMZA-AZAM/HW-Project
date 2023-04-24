using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.OrganizationApi.Services;
using HW.OrganizationModels;
using HW.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW.OrganizationApi.Controllers
{
    [Produces("application/json")]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet]
        public string Start()
        {
            return "Organization service is started.";
        }

        [HttpPost]
        public async Task<Response> AddEdit([FromBody]Organization model)
        {
            return await organizationService.AddEdit(model);
        }

        //[HttpPost]
        public async Task<Response> SetSkills([FromBody]List<SkillSet> skillSets)
        {
            return await organizationService.SetSkills(skillSets);
        }

        [HttpGet]
        public Organization GetPersonalDetails(long organizationId)
        {
            return organizationService.GetPersonalDetails(organizationId);
        }

        [HttpGet]
        public long GetEntityIdByUserId(string userId)
        {
            return organizationService.GetEntityIdByUserId(userId);
        }

        [HttpGet]
        public Organization GetByUserId(string userId)
        {
            return organizationService.GetByUserId(userId);
        }

        [HttpGet]
        public List<long> GetSkillIds(long organizationId)
        {
            return organizationService.GetSkillIds(organizationId);
        }        
    }
}