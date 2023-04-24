using HW.OrganizationModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.OrganizationApi.Services
{
    public interface IOrganizationService
    {
        List<IdValueVM> GetAllServices();
        Task<Response> AddEditOrganization(Organization data);
        void DeleteOrganization(int id);
        //Task<Response> SetPBSkills(List<PropertyBuilderSkillSetList> data);
        Organization GetPersonalDetails(long organizationId);

        long GetEntityIdByUserId(string userId);
        Organization GetOrganizationByUserId(string userId);
        long GetOrganizationSkillId(long organizationId);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork uow;

        public OrganizationService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public List<IdValueVM> GetAllServices()
        {
            //var PBServices = uow.Repository<PropertyBuilderSkillCategory>().GetAll();
            List<IdValueVM> IdValueVMList = new List<IdValueVM>();
            //foreach (var item in PBServices)
            //{
            //    IdValueVM IdValueVM = new IdValueVM();
            //    IdValueVM.Id = item.PropertyBuilderSkillCategoryId;
            //    IdValueVM.Value = item.SkillCategoryName;

            //    IdValueVMList.Add(IdValueVM);
            //}
            return IdValueVMList;
        }

        public Organization GetPersonalDetails(long organizationId)
        {
            return uow.Repository<Organization>().GetById(organizationId);
        }

        public async Task<Response> AddEditOrganization(Organization organization)
        {
            Response response = new Response();
            try
            {
                if (organization.OrganizationId > 0)
                {
                    var existingData = GetPersonalDetails(organization.OrganizationId);
                    if (existingData != null)
                    {
                        var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
                        var jsonValues = JsonConvert.SerializeObject(organization, settings);
                        JsonConvert.PopulateObject(jsonValues, existingData);
                        uow.Repository<Organization>().Update(existingData);
                    }
                }
                else
                {
                    await uow.Repository<Organization>().AddAsync(organization);
                }
                await uow.SaveAsync();

                response.ResultData = organization;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }

        public async void DeleteOrganization(int id)
        {
            await uow.Repository<Organization>().DeleteAsync(id);
        }

        //public async Task<Response> SetPBSkills(List<PropertyBuilderSkillSetList> dataList)
        //{
        //    Response response = new Response();
        //    foreach (var data in dataList)
        //    {
        //        try
        //        {
        //            var existingData = await uow.Repository<PropertyBuilderSkillSetList>().GetByIdAsync(data.PropertyBuilderSkillSetListId);
        //            if (existingData != null)
        //            {
        //                var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        //                var jsonValues = JsonConvert.SerializeObject(data, settings);
        //                JsonConvert.PopulateObject(jsonValues, existingData);
        //                uow.Repository<PropertyBuilderSkillSetList>().Update(existingData);
        //            }
        //            else
        //            {
        //                await uow.Repository<PropertyBuilderSkillSetList>().AddAsync(data);
        //            }
        //            await uow.SaveAsync();
        //            response.Message = "Successfull Created.";
        //            response.Status = ResponseStatus.OK;
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Message = ex.Message;
        //            response.Status = ResponseStatus.Error;
        //        }
        //    }
        //    return response;
        //}

        public long GetEntityIdByUserId(string userId)
        {
            return uow.Repository<Organization>().GetAll().FirstOrDefault(t => t.UserId == userId)?.OrganizationId ?? 0;
        }

        public Organization GetOrganizationByUserId(string userId)
        {            
           return uow.Repository<Organization>().GetAll().Where(x => x.UserId == userId).FirstOrDefault();
        }

        public long GetOrganizationSkillId(long organizationId)
        {
            //uow.Repository<>
            return 1;
        }
    }
}
