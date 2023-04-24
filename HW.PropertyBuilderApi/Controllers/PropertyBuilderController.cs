using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.OrganizationApi.Services;
using HW.OrganizationModels;
using HW.Utility;
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
            return "Property Builder service is started.";
        }

        [HttpPost]
        public async Task<Response> AddEditOrganization([FromBody]Organization model)
        {
            return await organizationService.AddEditOrganization(model);
        }

        [HttpGet]
        public List<IdValueVM> GetAllServices()
        {
            return organizationService.GetAllServices().ToList();
        }

        [HttpPost]
        public async Task<Response> RegisterOrganization([FromBody]Organization data)
        {
            Response response = new Response();
            if (ModelState.IsValid)
                response = await organizationService.AddEditOrganization(data);
            else
            {
                response.Status = ResponseStatus.Error;
                response.Message = "Model is not valid.";
            }
            return response;
        }

        //[HttpPost]
        //public async Task<Response> SetPBSkills([FromBody] List<PropertyBuilderSkillSetList> data)
        //{
        //    Response response = new Response();
        //    if (ModelState.IsValid)
        //        response = await organizationService.SetPBSkills(data);
        //    else
        //    {
        //        response.Status = ResponseStatus.Error;
        //        response.Message = "Model is not valid.";
        //    }
        //    return response;
        //}

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
        public Organization GetOrganizationByUserId(string userId)
        {
            return organizationService.GetOrganizationByUserId(userId);
        }

        [HttpGet]
        public long GetOrganizationSkillId(long organizationId)
        {
            return organizationService.GetOrganizationSkillId(organizationId);
        }
    }
}