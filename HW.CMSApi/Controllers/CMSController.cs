using System.Collections.Generic;
using System.Threading.Tasks;
using HW.CMSApi.Services;
using HW.CMSModels;
using HW.CMSViewModel;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;

namespace HW.CMSApi.Controllers
{
    [Produces("application/json")]
    public class CMSController : ControllerBase
    {
        private readonly ICMSService cMSService;

        public CMSController(ICMSService cMSService)
        {
            this.cMSService = cMSService;
        }
        [HttpGet]
        public Response getuser()
        {
            return new Response();
        }
        [HttpPost]
        public Response InsertAndUpDateCategory([FromBody] Category category)
        {
            return cMSService.InsertAndUpDateCategory(category);

        }
        [HttpPost]
        public Response CreateUpdatePost([FromBody] PostVM postVM)
        {
            return cMSService.CreateUpdatePost(postVM);

        }
        [HttpPost]
        public Response DeleteCategory([FromBody] int categoryId)
        {
            return cMSService.DeleteCategory(categoryId);

        }
        [HttpGet]
        public List<CategoryVM> GetCategoryList()
        {   
            return cMSService.GetCategoryList();
        }
        [HttpPost]
        public List<PostVM> GetPostsList([FromBody] PostVM postVM)
        {
            return cMSService.GetPostsList(postVM);
        }
        [HttpGet]
        public PostVM GetPostDetails(int postId)
        {
            return cMSService.GetPostDetails(postId);
        }
        [HttpPost]
        public Response CreateUpdatePageSeo([FromBody] PagesSeo pagesSeo)
        {
            return cMSService.CreateUpdatePageSeo(pagesSeo);

        }
        [HttpGet]
        public List<PageSeoVM> GetPagesList()
        {
            return cMSService.GetPagesList();
        }
        [HttpGet]
        public async Task<Response> GetSeoPageById(int pageId)
        {
            return await cMSService.GetSeoPageById(pageId);
        }
        [HttpGet]
        public async Task<Response> GetSitePagesList()
        {
            return await cMSService.GetSitePagesList();
        }

        [HttpPost]
        public async Task<Response> AddUpdateSitePage([FromBody] SitePagesVM sitePagesVM)
        {
            return await cMSService.AddUpdateSitePage(sitePagesVM);
        }
        [HttpGet]
        public async Task<Response> GetSitePagesListByPageId(int ProjectId)
        {
            return await cMSService.GetSitePagesListByPageId(ProjectId);
        }
  
    }
}
