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
        Task<Response> AddEdit(Organization data);
        void Delete(int id);
        Task<Response> SetSkills(List<SkillSet> skillSets);
        Organization GetPersonalDetails(long organizationId);
        long GetEntityIdByUserId(string userId);
        Organization GetByUserId(string userId);
        List<long> GetSkillIds(long organizationId);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork uow;

        public OrganizationService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Organization GetPersonalDetails(long organizationId)
        {
            return uow.Repository<Organization>().GetById(organizationId);
        }

        public async Task<Response> AddEdit(Organization organization)
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

        public async void Delete(int id)
        {
            await uow.Repository<Organization>().DeleteAsync(id);
        }

        public long GetEntityIdByUserId(string userId)
        {
            return uow.Repository<Organization>().GetAll().FirstOrDefault(t => t.UserId == userId)?.OrganizationId ?? 0;
        }

        public Organization GetByUserId(string userId)
        {
            return uow.Repository<Organization>().GetAll().FirstOrDefault(x => x.UserId == userId);
        }

        public List<long> GetSkillIds(long organizationId)
        {
            return uow.Repository<SkillSet>().Get(x => x.OrganizationId == organizationId).Select(s => s.SkillId).ToList();
        }

        public async Task<Response> SetSkills(List<SkillSet> skillSets)
        {
            Response response = new Response();
            IRepository<SkillSet> repository = uow.Repository<SkillSet>();

            try
            {
                IQueryable<SkillSet> deleteQuery = repository.GetAll().Where(s => s.OrganizationId == skillSets.FirstOrDefault().OrganizationId);
                await repository.DeleteAllAsync(deleteQuery);

                foreach (var skillSet in skillSets ?? new List<SkillSet>())
                {
                    await repository.AddAsync(skillSet);
                }

                await uow.SaveAsync();

                response.Message = "Successfully updated.";
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
    }
}
