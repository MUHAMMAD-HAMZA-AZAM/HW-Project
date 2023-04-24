using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW.CMSModels;
using HW.CMSViewModel;
//using HW.CMSViewModel;
using HW.GatewayApi.Admin;
using HW.GatewayApi.AdminServices;
using HW.GatewayApi.AuthO;
using HW.GatewayApi.Services;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.GatewayApi.AdminControllers
{
    [Produces("application/json")]
    public class AdminCMSController : AdminBaseController
    {
        private readonly IAdminCMSService adminCMSService;

        public AdminCMSController(IAdminCMSService adnCMSService_, IUserManagementService userManagementService) : base(userManagementService)
        {
           this.adminCMSService = adnCMSService_;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]

        public async Task<Response> InsertAndUpDateCategory([FromBody] Category category)
        {
            return await adminCMSService.InsertAndUpDateCategory(category);

        }

        [HttpPost]

        public async Task<Response> InsertAndUpDateSubCategory([FromBody] SubCategory category)
        {
            return await adminCMSService.InsertAndUpDateSubCategory(category);

        }

        [HttpPost]

        public async Task<Response> DeleteCategory([FromBody]  int categoryId)
        {
            return await adminCMSService.DeleteCategory(categoryId);

        }

        [HttpGet]

        public async Task<Response> GetUser()
        {
            return await adminCMSService.GetUser();
        }
        [HttpGet]
        public async Task<List<CategoryVM>> GetCategoryList()
        {
            return await adminCMSService.GetCategoryList();
        }
        [HttpPost]
        public async Task<List<PostVM>> GetPostsList([FromBody] PostVM postVM)
        {
            return await adminCMSService.GetPostsList(postVM);
        }
        [HttpPost]
        public async Task<Response> CreateUpdatePost([FromBody] PostVM postVM)
        {
            return await adminCMSService.CreateUpdatePost(postVM);
        }
        [HttpGet]
        public async Task<PostVM> GetPostDetails(int postId)
        {
            return await adminCMSService.GetPostDetails(postId);
        }
        [HttpPost]
        public async Task<Response> CreateUpdatePageSeo([FromBody] PagesSeo pagesSeo)
        {
            return await adminCMSService.CreateUpdatePageSeo(pagesSeo);
        }
        [HttpGet]
        public async Task<List<PageSeoVM>> GetPagesList()
        {
            return await adminCMSService.GetPagesList();
        }
        [HttpGet]
        public async Task<string> GetSeoPageById(int pageId)
        {
            return await adminCMSService.GetSeoPageById(pageId);
        }
        [HttpGet]
        public async Task<Response> GetSitePagesList()
        {
            return await adminCMSService.GetSitePagesList();
        }
        [HttpPost]
        public async Task<Response> AddUpdateSitePage([FromBody] SitePagesVM sitePagesVM)
        {
            return await adminCMSService.AddUpdateSitePage(sitePagesVM);
        }
        [HttpGet]
        public async Task<Response> GetSitePagesListByPageId(int projectId)
        {
            return await adminCMSService.GetSitePagesListByPageId(projectId);
        }

    }
}
