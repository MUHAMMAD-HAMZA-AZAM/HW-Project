using HW.ImageApi.Services;
using HW.ImageModels;
using HW.ReportsViewModels;
using HW.SupplierViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ImageApi.Controllers
{
    [Produces("application/json")]
    public class ImagesController : Controller
    {
        private readonly IImagesService imageService;

        public ImagesController(IImagesService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        public string Start()
        {
            return "Images API started.";
        }

        [HttpPost]
        public List<JobImages> GetJobQuotationImages([FromBody] List<long> jobQuotationIds)
        {
            return imageService.GetJobQuotationImages().Where(x => jobQuotationIds.Contains(x.JobQuotationId)).ToList();
        }

        [HttpGet]
        public CustomerProfileImage GetImageByCustomerId(long customerId)
        {
            CustomerProfileImage customerProfileImage = imageService.GetImageByCustomerId(customerId);
            return customerProfileImage;
        }

        [HttpGet]
        //[Authorize(Roles = "Trademan")]
        public List<JobImages> GetAllImage()
        {
            return imageService.GetAllImage().ToList();
        }
        [HttpPost]
        public List<CustomerProfileImage> GetCustomerProfileImageList([FromBody] List<long> customersIds)
        {
            return imageService.GetCustomerProfileImageList(customersIds);
        }
        [HttpPost]
        public bool SaveDisputeImages([FromBody] List<DisputeImages> disputeImages)
        {
            return imageService.SaveDisputeImages(disputeImages);
        }

        [HttpGet]
        public IQueryable<JobImages> GetByJobQuotationId(long jobQuotationId)
        {
            return imageService.GetByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        //[Authorize(Roles = "Trademan")]
        public List<JobImages> GetImages()
        {
            return imageService.GetImages().ToList();
        }

        public async Task<List<JobImages>> GetJobImagesByIds([FromBody] List<long> ids)
        {
            return await imageService.GetJobImagesByIds(ids);
        }

        [HttpGet]
        public async Task<List<JobImages>> GetJobBidImages([FromQuery] long jobQuotaionId)
        {
            return await imageService.GetJobBidImages(jobQuotaionId);
        }

        [HttpGet]
        public async Task<List<SupplierAdImage>> GetAdsImages(long supplierAdsId)
        {
            return await imageService.GetAdsImages(supplierAdsId);
        }

        [HttpGet]
        public List<JobImages> GetImageByJobDetailId(long jobQuotationId)
        {
            return imageService.GetImageByJobDetailId(jobQuotationId);
        }

        [HttpPost]
        public List<TradesmanProfileImage> GetTradesmanProfileImages([FromBody] List<long> tradesmanIds)
        {
            return imageService.GetTradesmanProfileImages(tradesmanIds);
        }

        [HttpGet]
        public List<JobImages> GetJobImagesListByJobQuotationId(long jobQuotationId)
        {
            return imageService.GetJobImagesListByJobQuotationId(jobQuotationId);
        }

        [HttpPost]
        public List<JobImages> GetJobImagesListByJobQuotationIds([FromBody] List<long> jobQutationIds)
        {
            return imageService.GetJobImagesListByJobQuotationIds(jobQutationIds);
        }

        [HttpGet]
        public TradesmanProfileImage GetTradesmanProfileImageByTradesmanId(long tradesmanId)
        {
            return imageService.GetTradesmanProfileImageByTradesmanId(tradesmanId);
        }

        [HttpPost]
        public List<TradesmanSkillImage> GetTradesmanProfileImageBySkillIds([FromBody] List<long> skillIds)
        {
            return imageService.GetTradesmanProfileImageBySkillids(skillIds);
        }

        [HttpPost]
        public List<SupplierPcImage> GetSupplierProductImageByProductCategoryId([FromBody] List<long> productCategoryId)
        {
            return imageService.GetSupplierProductImageByProductCategoryId(productCategoryId);
        }

        [HttpGet]
        public TradesmanProfileImage GetTradesmanImageById(long tradesmanId)
        {
            return imageService.GetTradesmanImageById(tradesmanId);
        }

        [HttpGet]
        public IQueryable<SupplierPcImage> GetProductCategoryImages()
        {
            return imageService.GetProductCategoryImages();
        }

        [HttpGet]
        public List<SupplierAdImage> GetSupplierAdImagesByAdId(long supplierAdId)
        {
            return imageService.GetSupplierAdImagesByAdId(supplierAdId);
        }

        [HttpGet]
        public async Task DeleteJobImages(long jobQuotationId)
        {
            await imageService.DeleteJobImages(jobQuotationId);
        }


        [HttpPost]
        public IQueryable<SupplierAdImage> GetSupplierAdImagesBySupplierAdIds([FromBody] List<long> supplierAdIds)
        {
            return imageService.GetSupplierAdImagesBySupplierAdIds(supplierAdIds);
        }

        [HttpPost]
        public void SaveJobImages([FromBody] List<JobImages> jobquoteImages)
        {
            imageService.SaveJobImages(jobquoteImages);
        }

        public SupplierProfileImage GetProfileImageBySupplierId(long supplierId)
        {
            SupplierProfileImage supplierProfileImage = imageService.GetProfileImageBySupplierId(supplierId);
            return supplierProfileImage;
        }

        public async Task DeleteSupplierAdImages(long supplierAdId)
        {
            await imageService.DeleteSupplierAdImages(supplierAdId);
        }

        public Response AddUpdateTradesmanProfileImage([FromBody] TradesmanProfileImage tradesmanProfileImage)
        {
            return imageService.AddUpdateTradesmanProfileImage(tradesmanProfileImage);
        }

        [HttpPost]
        public Response AddUpdateUserProfileImage([FromBody] CustomerProfileImage customerProfileImage)
        {
            return imageService.AddUpdateUserProfileImage(customerProfileImage);
        }

        [HttpPost]
        public Response AddUpdateSupplierProfileImage([FromBody] SupplierProfileImage supplierProfileImage)
        {
            return imageService.AddUpdateSupplierProfileImage(supplierProfileImage);
        }

        [HttpPost]
        public async Task SubmitAndUpdateSupplierAdImages([FromBody] List<SupplierAdImage> supplierAdImages)
        {
            await imageService.SubmitAndUpdateSupplierAdImages(supplierAdImages);
        }
        [HttpGet]
        public JobImages GetJobImages(string imageUrl)
        {
            return imageService.GetJobImages(imageUrl);
        }

        [HttpGet]
        public SupplierAdImage GetSupplierAdImageById(long supplierAdImageId)
        {
            return imageService.GetSupplierAdImageById(supplierAdImageId);
        }
        [HttpGet]
        public SupplierAdImage GetSupplierAdImageByUrl(string imageUrl)
        {
            return imageService.GetSupplierAdImageByUrl(imageUrl);
        }


        [HttpGet]
        public JobImages GetJobImageById(long jobImageId)
        {
            return imageService.GetJobImageById(jobImageId);
        }
        [HttpGet]
        public JobImages GetJobMainImages(long quotationId)
        {
            return imageService.GetJobMainImages(quotationId);
        }
        [HttpGet]
        public List<JobImages> GetJobImage(long quotationId)
        {
            return imageService.GetJobImage(quotationId);
        }
        [HttpPost]
        public Response FeaturedSupplierImages([FromBody] FeaturedSupplier featuredSupplier)
        {
            return imageService.FeaturedSupplierImages(featuredSupplier);
        }        
        [HttpPost]
        public Response UpdateFeaturedSupplierImages([FromBody] FeaturedSupplier featuredSupplier)
        {
            return imageService.UpdateFeaturedSupplierImages(featuredSupplier);
        }
        [HttpGet]
        public List<SupplierDTO> GetFeaturedSupplierImages()
        {
            return imageService.GetFeaturedSupplierImages();
        }
        [HttpGet]
        public ImageVM GetMarkeetPlaceProductsImages(long adImageId)
        {
            return imageService.GetMarkeetPlaceProductsImages(adImageId);
        }
        [HttpPost]
        public async Task<Response> AddSupplierProductImages([FromBody] AddProductVM addProductVM)
        {
            return await imageService.AddSupplierProductImages(addProductVM);
        }        
        [HttpPost]
        public async Task<Response> MarkProductImageAsMain([FromBody] ProductImageDTO productImageDTO)
        {
            return await imageService.MarkProductImageAsMain(productImageDTO);
        }
        public async Task<Response> AddAndUpdateLogo([FromBody] string data)
        {
            return await imageService.AddAndUpdateLogo(data);
        }
        [HttpGet]
        public async Task<Response> GetLogoData(long supplierId)
        {
            return await imageService.GetLogoData(supplierId);
        }
        
    }
}
