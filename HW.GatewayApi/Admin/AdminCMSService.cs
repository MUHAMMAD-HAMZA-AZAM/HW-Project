using HW.CustomerModels;
using HW.GatewayApi.Code;
using HW.GatewayApi.Services;
using HW.Http;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.ReportsViewModels;
using HW.UserManagmentModels;
using Microsoft.AspNetCore.Mvc;
using HW.PackagesAndPaymentsViewModels;
using HW.IdentityViewModels;
using HW.CMSModels;
using HW.CMSViewModel;
//using HW.CMSViewModel;

namespace HW.GatewayApi.AdminServices
{
    public interface IAdminCMSService
    {
        Task<Response> GetUser();
        Task<List<CategoryVM>>GetCategoryList();
        Task<List<PostVM>> GetPostsList(PostVM postVM);
        Task<Response> InsertAndUpDateCategory(Category category);
        Task<Response> InsertAndUpDateSubCategory(SubCategory category);
        Task<Response> DeleteCategory(int categoryId);
        Task<Response> CreateUpdatePost(PostVM postVM);
        Task<PostVM> GetPostDetails(int postId);
        Task<Response> CreateUpdatePageSeo(PagesSeo pagesSeo);
        Task<List<PageSeoVM>> GetPagesList();
        Task<Response> GetSitePagesList();
        Task<Response> AddUpdateSitePage(SitePagesVM sitePagesVM);
        Task<string> GetSeoPageById(int pageId);
        Task<Response> GetSitePagesListByPageId(int projectId);

    }
    public class AdminCMSService : IAdminCMSService
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly ICommunicationService communicationService; // sending job post confirmation email
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public AdminCMSService(IHttpClientService httpClient, ClientCredentials clientCred, ICommunicationService communicationService, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
            this.communicationService = communicationService;
        }

        public async Task<Response> GetUser()
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>
                    (await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.getuser}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
        public async Task<Response> InsertAndUpDateCategory(Category category)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.InsertAndUpDateCategory}", category);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> InsertAndUpDateSubCategory(SubCategory category)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.InsertAndUpDateSubCategory}", category);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> DeleteCategory(int categoryId)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.DeleteCategory}", categoryId);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<CategoryVM>> GetCategoryList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetCategoryList}");
            return JsonConvert.DeserializeObject<List<CategoryVM>>(response);
        }
        public async Task<List<PostVM>> GetPostsList(PostVM postVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetPostsList}" , postVM);
            return JsonConvert.DeserializeObject<List<PostVM>>(response);
        }
        public async Task<Response> CreateUpdatePost(PostVM postVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.CreateUpdatePost}", postVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<PostVM> GetPostDetails(int postId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetPostDetails}?postId={postId}", "");
            return JsonConvert.DeserializeObject<PostVM>(response);
        }
        public async Task<Response> CreateUpdatePageSeo(PagesSeo pagesSeo)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.CreateUpdatePageSeo}", pagesSeo);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<List<PageSeoVM>> GetPagesList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetPagesList}");
            return JsonConvert.DeserializeObject<List<PageSeoVM>>(response);
        }
        public async Task<Response> GetSitePagesList()
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetSitePagesList}");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<Response> AddUpdateSitePage(SitePagesVM sitePagesVM)
        {
            string response = await httpClient.PostAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.AddUpdateSitePage}", sitePagesVM);
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<string> GetSeoPageById(int pageId)
        {
            return await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetSeoPageById}?pageId={pageId}", "");
        }
        public async Task<Response> GetSitePagesListByPageId(int projectId)
        {
            string response = await httpClient.GetAsync($"{_apiConfig.CMSApiUrl}{ApiRoutes.CMS.GetSitePagesListByPageId}?ProjectId={projectId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
    }
}
