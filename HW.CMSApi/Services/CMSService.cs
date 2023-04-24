using HW.Utility;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using HW.CMSModels;
using HW.CMSViewModel;
using System.Threading.Tasks;

namespace HW.CMSApi.Services
{
    public interface ICMSService
    {
        Response InsertAndUpDateCategory(Category category);
        Response CreateUpdatePost(PostVM postVM);
        Response DeleteCategory(int categoryId);
        List<CategoryVM> GetCategoryList();
        List<PostVM> GetPostsList(PostVM postVM);
        PostVM GetPostDetails(int postId);
        Response CreateUpdatePageSeo(PagesSeo pagesSeo);
        List<PageSeoVM> GetPagesList();
        Task<Response> GetSitePagesList();
        Task<Response> AddUpdateSitePage(SitePagesVM sitePagesVM);
        Task<Response> GetSeoPageById(int pageId);
        Task<Response> GetSitePagesListByPageId(int projectId);

    }
    public class CMSService : ICMSService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public CMSService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }
        public Response InsertAndUpDateCategory(Category category)
        {
            Response response = new Response();
            try
            {
                if (category.CategoryId <= 0)
                {
                    var findByCatName = uow.Repository<Category>().Get(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()).FirstOrDefault();
                    if (findByCatName == null)
                    {
                        category.CreatedOn = DateTime.Now;
                        category.Slug = category.CategoryName;
                        category.IsActive = true;
                        uow.Repository<Category>().Add(category);
                        uow.Save();
                        response.Message = "Data saved successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "alreadyexist";
                    }
                }
                else
                {
                    var findByCatName = uow.Repository<Category>().Get(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()).FirstOrDefault();
                    var findByCatId = uow.Repository<Category>().GetById(category.CategoryId);
                    if (findByCatName == null)
                    {
                        findByCatId.CategoryName = category.CategoryName;
                        findByCatId.ModifiedBy = category.ModifiedBy;
                        findByCatId.ModifiedOn = DateTime.Now;
                        uow.Repository<Category>().Update(findByCatId);
                        uow.Save();
                        response.Message = "Data saved successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "alreadyexist";
                    }

                }
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
        public Response CreateUpdatePost(PostVM postVM)
        {
            Response response = new Response();
            try
            {
                Post post = new Post();
                if (postVM.ImageBase64 != null)
                {
                    var splitName = postVM.ImageBase64.Split(',');
                    string convert = postVM.ImageBase64.Replace(splitName[0] + ",", String.Empty);
                    post.HeaderImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                }
                if (postVM.PostId <= 0 && postVM.PostAction == "add")
                {
                    post.CategoryId = postVM.CategoryId;
                    post.PostTitle = postVM.PostTitle;
                    post.PostStatus = postVM.PostStatus;
                    post.CommentStatus = postVM.CommentStatus;
                    post.CreatedBy = postVM.CreatedBy;
                    post.CreatedOn = DateTime.Now;
                    post.PostContent = postVM.PostContent;
                    post.Summary = postVM.Summary;
                    post.Slug = postVM.Slug;
                    post.MetaTags = postVM.MetaTags;
                    uow.Repository<Post>().Add(post);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Post added successfully";
                    return response;
                }
                else if (postVM.CategoryId >= 0 && postVM.PostAction == "edit")
                {
                    var findByPostId = uow.Repository<Post>().Get(x => x.PostId == postVM.PostId).FirstOrDefault();
                    if (findByPostId != null)
                    {
                        //findByPostId.PostId = postVM.PostId;
                        findByPostId.CategoryId = postVM.CategoryId;
                        findByPostId.PostTitle = postVM.PostTitle;
                        findByPostId.PostStatus = postVM.PostStatus;
                        findByPostId.CommentStatus = postVM.CommentStatus;
                        findByPostId.ModifiedBy = postVM.ModifiedBy;
                        findByPostId.ModifiedOn = DateTime.Now;
                        findByPostId.PostContent = postVM.PostContent;
                        findByPostId.Summary = postVM.Summary;
                        findByPostId.HeaderImage = post.HeaderImage;
                        findByPostId.Slug = postVM.Slug;
                        findByPostId.MetaTags = postVM.MetaTags;
                        uow.Repository<Post>().Update(findByPostId);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Post updated successfully";
                        return response;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Something Went Wrong";
                        return response;

                    }
                }
                else if (postVM.CategoryId >= 0 && postVM.PostAction == "delete")
                {
                    var findByPostId = uow.Repository<Post>().Get(x => x.PostId == postVM.PostId).FirstOrDefault();
                    if (findByPostId != null)
                    {
                        //Post status
                        // 1 = Published
                        // 2 = Pending
                        // 3 = Deleted
                        // 4 = Rejected
                        findByPostId.PostStatus = 3;
                        uow.Repository<Post>().Update(findByPostId);
                        uow.Save();
                        response.Status = ResponseStatus.OK;
                        response.Message = "Post deleted successfully";
                        return response;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Something Went Wrong";
                        return response;

                    }

                }
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
        public Response DeleteCategory(int categoryId)
        {
            Response response = new Response();
            try
            {
                if (categoryId > 0)
                {
                    var findByCatName = uow.Repository<Category>().GetById(categoryId);
                    if (findByCatName != null)
                    {
                        findByCatName.IsActive = findByCatName.IsActive == true ? false : true;
                        uow.Repository<Category>().Update(findByCatName);
                        uow.Save();
                        response.Message = "Stutus changed successfully!";
                        response.Status = ResponseStatus.OK;
                    }
                    else
                    {
                        response.Message = "Something went wrong";
                    }
                }
                else
                {
                    response.Message = "Something went wrong";
                }
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
        public List<CategoryVM> GetCategoryList()
        {
            try
            {
                List<CategoryVM> categories = new List<CategoryVM>();
                SqlParameter[] sqlParameters = {
                };
                categories = uow.ExecuteReaderSingleDS<CategoryVM>("SP_GetCatList", sqlParameters).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CategoryVM>();
            }

        }
        public List<PostVM> GetPostsList(PostVM postVM)
        {
            try
            {
                List<PostVM> posts = new List<PostVM>();
                SqlParameter[] sqlParameters = {
                     new SqlParameter("@pageNumber" , postVM.pageNumber),
                    new SqlParameter("@pageSize" , postVM.pageSize),

                };
                posts = uow.ExecuteReaderSingleDS<PostVM>("SP_GetPostList", sqlParameters).ToList();
                return posts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PostVM>();
            }

        }
        public PostVM GetPostDetails(int postId)
        {
            try
            {
                PostVM posts = new PostVM();
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@postId" , postId),
                };
                posts = uow.ExecuteReaderSingleDS<PostVM>("SP_GetPostDetails", sqlParameters).FirstOrDefault();
                return posts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new PostVM();
            }

        }
        public Response CreateUpdatePageSeo(PagesSeo pagesSeo)
        {
            Response response = new Response();
            try
            {
                if (pagesSeo.PageId <= 0)
                {
              var data = uow.Repository<PagesSeo>().Get(x => x.SitePageId == pagesSeo.SitePageId).FirstOrDefault();
                    if (data != null)
                    {
                        response.Message = "Page Name Already Exist!!!";
                        response.Status = ResponseStatus.OK;
                        response.ResultData = data;
                        return response;
                    }
                    pagesSeo.CreatedOn = DateTime.Now;
                    uow.Repository<PagesSeo>().Add(pagesSeo);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data saved successfully!";
                }
                else if (pagesSeo.PageId > 0)
                {
                    var data = uow.Repository<PagesSeo>().Get(x => x.SitePageId == pagesSeo.SitePageId && x.PageId != pagesSeo.PageId).FirstOrDefault();
                    if (data != null)
                    {
                        response.Message = "Page Name Already Exist!!!";
                        response.Status = ResponseStatus.OK;
                        response.ResultData = data;
                        return response;
                    }
                    var getPageById = uow.Repository<PagesSeo>().Get(x => x.PageId == pagesSeo.PageId).FirstOrDefault();
                    getPageById.SitePageId = pagesSeo.SitePageId;
                    getPageById.ProjectId = pagesSeo.ProjectId;
                    getPageById.PageTitle = pagesSeo.PageTitle;
                    getPageById.Keywords = pagesSeo.Keywords;
                    getPageById.OgTitle = pagesSeo.OgTitle;
                    getPageById.OgDescription = pagesSeo.OgDescription;
                    getPageById.Canonical = pagesSeo.Canonical;
                    getPageById.Description = pagesSeo.Description;
                    getPageById.ModifiedBy = pagesSeo.ModifiedBy;
                    getPageById.ModifiedOn = DateTime.Now;
                    uow.Repository<PagesSeo>().Update(getPageById);
                    uow.Save();
                    response.Status = ResponseStatus.OK;
                    response.Message = "Data updated successfully!";
                }
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
        public List<PageSeoVM> GetPagesList()
        {
            try
            {
                List<PageSeoVM> posts = new List<PageSeoVM>();
                SqlParameter[] sqlParameters = {
                };
                posts = uow.ExecuteReaderSingleDS<PageSeoVM>("SP_GetPagesList", sqlParameters).ToList();
                return posts;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<PageSeoVM>();
            }

        }
        public async Task<Response> GetSeoPageById(int pageId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@PageId" , pageId)
                };
                response.ResultData = await uow.ExecuteReaderSingleDSNew<PageSeoVM>("Sp_GetSeoPageById", sqlParameters);
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task<Response> GetSitePagesList()
        {
            Response response = new Response();
            try
            {
              

                    response.ResultData =  uow.ExecuteCommand<SitePagesVM>("Sp_GetSitePagesList");
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public async Task<Response> AddUpdateSitePage(SitePagesVM sitePagesVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters = {
                    new SqlParameter("@PageId" , sitePagesVM.PageId),
                         new SqlParameter("@ProjectId" , sitePagesVM.ProjectId),
                       new SqlParameter("@PageName" , sitePagesVM.PageName),
                          new SqlParameter("@CreatedBy" , sitePagesVM.UserId),
                             new SqlParameter("@ModifiedBy " , sitePagesVM.UserId),
                };

                response.ResultData =await uow.ExecuteReaderSingleDSNew<SitePagesVM>("SP_AddUpdateSitePages", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved successfully!";
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }


        public async Task<Response> GetSitePagesListByPageId(int projectId)
        {
            Response response = new Response();
            try { 
           
                response.ResultData = uow.Repository<SitePage>().Get(x => x.ProjectId == projectId ).ToList();
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Exc.AddErrorLog(ex);
            }
            return response;
        }
    }
}
