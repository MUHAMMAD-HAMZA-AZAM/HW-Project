using Google.Cloud.Vision.V1;
using HW.ImageModels;
using HW.ReportsViewModels;
using HW.SupplierModels.DTOs;
using HW.SupplierViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HW.ImageApi.Services
{
    public interface IImagesService
    {
        IQueryable<JobImages> GetImages();
        Task<List<JobImages>> GetJobImagesByIds(List<long> ids);
        Task<List<JobImages>> GetJobBidImages(long jobQuotaionId);
        Task<List<SupplierAdImage>> GetAdsImages(long supplierAdsId);
        IQueryable<JobImages> GetJobQuotationImages();
        CustomerProfileImage GetImageByCustomerId(long customerId);
        SupplierProfileImage GetProfileImageBySupplierId(long supplierId);
        IQueryable<JobImages> GetByJobQuotationId(long id);
        List<JobImages> GetImageByJobDetailId(long id);
        IQueryable<JobImages> GetAllImage();
        TradesmanProfileImage GetTradesmanImageById(long tradesmanId);
        List<CustomerProfileImage> GetCustomerProfileImageList(List<long> customersIds);
        List<TradesmanProfileImage> GetTradesmanProfileImages(List<long> tradesmanIds);
        List<JobImages> GetJobImagesListByJobQuotationId(long jobQuotationId);
        List<JobImages> GetJobImagesListByJobQuotationIds(List<long> jobQutationIds);
        TradesmanProfileImage GetTradesmanProfileImageByTradesmanId(long tradesmanId);
        List<TradesmanSkillImage> GetTradesmanProfileImageBySkillids(List<long> skillIds);
        List<SupplierPcImage> GetSupplierProductImageByProductCategoryId(List<long> productCategoryId);
        //IQueryable<JobQuotationBidImage> GetByJobQuotationId(long id);
        void SaveJobImages(List<JobImages> jobImages);
        bool SaveDisputeImages(List<DisputeImages> disputeImages);
        IQueryable<SupplierPcImage> GetProductCategoryImages();
        List<SupplierAdImage> GetSupplierAdImagesByAdId(long supplierAdId);
        IQueryable<SupplierAdImage> GetSupplierAdImagesBySupplierAdIds(List<long> supplierAdIds);
        Task DeleteJobImages(long jobQuotationId);
        Task DeleteSupplierAdImages(long supplierAdId);
        Task SubmitAndUpdateSupplierAdImages(List<SupplierAdImage> supplierAdImages);
        Response AddUpdateTradesmanProfileImage(TradesmanProfileImage tradesmanProfileImage);
        Response AddUpdateUserProfileImage(CustomerProfileImage customerProfileImage);
        Response AddUpdateSupplierProfileImage(SupplierProfileImage supplierProfileImage);
        JobImages GetJobImages(string imageUrl);
        SupplierAdImage GetSupplierAdImageById(long supplierAdImageId);
        SupplierAdImage GetSupplierAdImageByUrl(string imageUrl);
        JobImages GetJobImageById(long jobImageId);
        Response FeaturedSupplierImages(FeaturedSupplier featuredSupplier);
        Response UpdateFeaturedSupplierImages(FeaturedSupplier featuredSupplier);
        List<SupplierDTO> GetFeaturedSupplierImages();
        JobImages GetJobMainImages(long quotationId);
        List<JobImages> GetJobImage(long quotationId);
        ImageVM GetMarkeetPlaceProductsImages(long adImageId);
        Task<Response> AddSupplierProductImages(AddProductVM addProductVM);
        Task<Response> MarkProductImageAsMain(SupplierViewModels.ProductImageDTO productImageDTO);
        Task<Response> AddAndUpdateLogo(string data);
        Task<Response> GetLogoData(long supplierId);

    }
    public class ImagesService : IImagesService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        private readonly IWebHostEnvironment hostingEnvironment;
        private bool isImageReplaced = false;
        private ImageAnnotatorClient client;

        public ImagesService(IUnitOfWork uow, IExceptionService Exc, IWebHostEnvironment hostingEnvironment)
        {
            this.uow = uow;
            this.Exc = Exc;
            this.hostingEnvironment = hostingEnvironment;

            try
            {
                if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
                {
                    hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
                {
                    var path = Path.Combine(hostingEnvironment.WebRootPath, "HoomWork-4dd6a1575253.json");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                }

                client = ImageAnnotatorClient.Create();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public IQueryable<JobImages> GetImages()
        {
            try
            {
                return uow.Repository<JobImages>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>().AsQueryable();
            }
        }

        public async Task<List<JobImages>> GetJobImagesByIds(List<long> ids)
        {
            try
            {
                List<JobImages> bidImages = new List<JobImages>();
                IQueryable<JobImages> query = uow.Repository<JobImages>().GetAll();
                bidImages = query.Where(c => ids.Any(id => c.JobQuotationId == id)).ToList();
                return bidImages;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobImages>();
            }
        }

        public async Task<List<JobImages>> GetJobBidImages(long jobQuotaionId)
        {
            try
            {
                List<JobImages> bidImages = new List<JobImages>();
                bidImages = uow.Repository<JobImages>().Get(ji => ji.JobQuotationId == jobQuotaionId).ToList();
                return bidImages;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobImages>();
            }
        }

        public async Task<List<SupplierAdImage>> GetAdsImages(long supplierAdsId)
        {
            try
            {
                return uow.Repository<SupplierAdImage>().Get(ji => ji.SupplierAdsId == supplierAdsId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SupplierAdImage>();
            }
        }

        public IQueryable<JobImages> GetJobQuotationImages()
        {
            try
            {
                return uow.Repository<JobImages>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobImages>().AsQueryable();
            }
        }

        public List<CustomerProfileImage> GetCustomerProfileImageList(List<long> customersIds)
        {
            try
            {
                return uow.Repository<CustomerProfileImage>().GetAll().Where(c => customersIds.Contains(c.CustomerId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<CustomerProfileImage>();
            }
        }

        public CustomerProfileImage GetImageByCustomerId(long customerId)
        {
            try
            {
                return uow.Repository<CustomerProfileImage>().Get(c => c.CustomerId == customerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new CustomerProfileImage();
            }
        }

        public SupplierProfileImage GetProfileImageBySupplierId(long supplierId)
        {
            try
            {
                return uow.Repository<SupplierProfileImage>().Get(c => c.SupplierId == supplierId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierProfileImage();
            }
        }

        public TradesmanProfileImage GetTradesmanImageById(long tradesmanId)
        {
            try
            {
                return uow.Repository<TradesmanProfileImage>().Get(x => x.TradesmanId == tradesmanId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new TradesmanProfileImage();
            }
        }

        public IQueryable<JobImages> GetAllImage()
        {
            try
            {
                return uow.Repository<JobImages>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>().AsQueryable();
            }
        }

        public IQueryable<JobImages> GetByJobQuotationId(long id)
        {
            try
            {
                return uow.Repository<JobImages>().Get(a => a.JobQuotationId == id);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>().AsQueryable();
            }
        }

        public List<JobImages> GetImageByJobDetailId(long id)
        {
            try
            {
                return uow.Repository<JobImages>().Get(a => a.JobQuotationId == id).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>();
            }
        }

        public List<TradesmanProfileImage> GetTradesmanProfileImages(List<long> tradesmanIds)
        {
            try
            {
                return uow.Repository<TradesmanProfileImage>().GetAll().Where(images => tradesmanIds.Contains(images.TradesmanId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<TradesmanProfileImage>();
            }
        }

        public List<JobImages> GetJobImagesListByJobQuotationId(long jobQuotationId)
        {
            try
            {
                List<JobImages> images = uow.Repository<JobImages>().GetAll().Where(m => m.JobQuotationId == jobQuotationId).ToList();
                return images;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>();
            }
        }

        public JobImages GetJobImageById(long jobImageId)
        {
            try
            {
                JobImages image = uow.Repository<JobImages>().GetById(jobImageId);
                return image;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new JobImages();
            }
        }

        public List<JobImages> GetJobImagesListByJobQuotationIds(List<long> jobQutationIds)
        {
            try
            {
                return uow.Repository<JobImages>().GetAll().Where(x => jobQutationIds.Contains(x.JobQuotationId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<JobImages>();
            }
        }

        public TradesmanProfileImage GetTradesmanProfileImageByTradesmanId(long tradesmanId)
        {
            try
            {
                return uow.Repository<TradesmanProfileImage>().Get().FirstOrDefault(i => i.TradesmanId == tradesmanId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new TradesmanProfileImage();
            }
        }

        public List<TradesmanSkillImage> GetTradesmanProfileImageBySkillids(List<long> skillIds)
        {
            try
            {
                return uow.Repository<TradesmanSkillImage>().GetAll().Where(x => skillIds.Contains(x.SkillId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<TradesmanSkillImage>();
            }
        }

        public List<SupplierPcImage> GetSupplierProductImageByProductCategoryId(List<long> productCategoryId)
        {
            try
            {
                return uow.Repository<SupplierPcImage>().GetAll().Where(x => productCategoryId.Contains(x.ProductCategoryId)).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierPcImage>();
            }
        }

        public IQueryable<SupplierPcImage> GetProductCategoryImages()
        {
            try
            {
                return uow.Repository<SupplierPcImage>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierPcImage>().AsQueryable();
            }
        }

        public List<SupplierAdImage> GetSupplierAdImagesByAdId(long supplierAdId)
        {
            try
            {
                return uow.Repository<SupplierAdImage>().GetAll().Where(x => x.SupplierAdsId == supplierAdId).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierAdImage>();
            }
        }

        public IQueryable<SupplierAdImage> GetSupplierAdImagesBySupplierAdIds(List<long> supplierAdIds)
        {
            try
            {
                return uow.Repository<SupplierAdImage>().Get(x => supplierAdIds.Contains(x.SupplierAdsId));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierAdImage>().AsQueryable();
            }
        }

        public SupplierAdImage GetSupplierAdImageById(long supplierAdImageId)
        {
            try
            {
                return uow.Repository<SupplierAdImage>().GetById(supplierAdImageId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new SupplierAdImage();
            }
        }

        public void SaveJobImages(List<JobImages> jobImages)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
                {
                    hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
                {
                    var path = Path.Combine(hostingEnvironment.WebRootPath, "HoomWork-4dd6a1575253");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                }

                var client = ImageAnnotatorClient.Create();

                var jobQuotationId = jobImages.Count > 0 ? jobImages[0].JobQuotationId : 0;
                var existingImages = uow.Repository<JobImages>().Get(x => x.JobQuotationId == jobQuotationId);
                var existingImagesList = existingImages.ToList();



                if (existingImagesList.Count == 0)
                {
                    foreach (var item in jobImages)
                    {
                        uow.Repository<JobImages>().Add(item);
                    }
                    uow.Save();
                }
                else
                {
                    if (jobImages.Count > 0)
                    {
                        foreach (var item in existingImagesList)
                        {
                            uow.Repository<JobImages>().Delete(item);
                        }
                        uow.Save();


                        foreach (JobImages image in jobImages)
                        {
                            image.BidImage = FilterImages(image.BidImage);
                            uow.Repository<JobImages>().Add(image);
                            if (image.BidImage != null)
                            {
                                uow.Save();
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task DeleteJobImages(long jobQuotationId)
        {
            try
            {
                if (jobQuotationId > 0)
                {
                    IQueryable<JobImages> deleteQuery = uow.Repository<JobImages>().GetAll().Where(s => s.JobQuotationId == jobQuotationId);

                    await uow.Repository<JobImages>().DeleteAllAsync(deleteQuery);

                    await uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
        }

        public bool SaveDisputeImages(List<DisputeImages> disputeImages)
        {
            try
            {
                foreach (DisputeImages item in disputeImages)
                {
                    uow.Repository<DisputeImages>().Add(item);
                    uow.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return false;
            }
        }

        public Response AddUpdateTradesmanProfileImage(TradesmanProfileImage tradesmanProfileImage)
        {
            try
            {
                TradesmanProfileImage existingTradesman = uow.Repository<TradesmanProfileImage>().GetAll().Where(x => x.TradesmanId == tradesmanProfileImage.TradesmanId).FirstOrDefault();

                //tradesmanProfileImage.ProfileImage = FilterImages(tradesmanProfileImage.ProfileImage);

                if (existingTradesman == null)
                {
                    uow.Repository<TradesmanProfileImage>().Add(tradesmanProfileImage);
                }
                else
                {
                    existingTradesman.ProfileImage = tradesmanProfileImage.ProfileImage;
                    existingTradesman.ModifiedBy = tradesmanProfileImage.CreatedBy;
                    existingTradesman.ModifiedOn = tradesmanProfileImage.CreatedOn;

                    uow.Repository<TradesmanProfileImage>().Update(existingTradesman);
                }

                uow.Save();

                return new Response() { Status = ResponseStatus.OK };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response() { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }

        public Response AddUpdateUserProfileImage(CustomerProfileImage customerProfileImage)
        {
            try
            {
                CustomerProfileImage existingCustomer = uow.Repository<CustomerProfileImage>().GetAll().Where(x => x.CustomerId == customerProfileImage.CustomerId).FirstOrDefault();

                //customerProfileImage.ProfileImage = FilterImages(customerProfileImage.ProfileImage);
                customerProfileImage.ProfileImage = customerProfileImage.ProfileImage;

                if (existingCustomer == null)
                {
                    uow.Repository<CustomerProfileImage>().Add(customerProfileImage);
                }
                else
                {
                    existingCustomer.ProfileImage = customerProfileImage.ProfileImage;
                    existingCustomer.ModifiedBy = customerProfileImage.CreatedBy;
                    existingCustomer.ModifiedOn = customerProfileImage.CreatedOn;

                    uow.Repository<CustomerProfileImage>().Update(existingCustomer);
                }

                uow.Save();

                return new Response() { Status = ResponseStatus.OK };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response() { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }

        public async Task DeleteSupplierAdImages(long supplierAdId)
        {
            try
            {
                IRepository<SupplierAdImage> repository = uow.Repository<SupplierAdImage>();

                IQueryable<SupplierAdImage> supplierAdImageList = repository.GetAll().Where(x => x.SupplierAdsId == supplierAdId);
                await repository.DeleteAllAsync(supplierAdImageList);
                await uow.SaveAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task SubmitAndUpdateSupplierAdImages(List<SupplierAdImage> supplierAdImages)
        {
            try
            {
                IRepository<SupplierAdImage> repository = uow.Repository<SupplierAdImage>();
                IQueryable<SupplierAdImage> supplierAdImageList = repository.GetAll().Where(x => x.SupplierAdsId == supplierAdImages.FirstOrDefault().SupplierAdsId);
                await repository.DeleteAllAsync(supplierAdImageList);

                foreach (SupplierAdImage item in supplierAdImages)
                {
                    //item.AdImage = FilterImages(item.AdImage);
                    item.AdImage = item.AdImage;
                    item.FileName = "img-" + DateTime.Now.Ticks + ".jpg";
                    item.ModifiedOn = DateTime.Now;
                    repository.Add(item);
                }
                await uow.SaveAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                Console.WriteLine(ex.Message);
            }
        }

        public Response AddUpdateSupplierProfileImage(SupplierProfileImage supplierProfileImage)
        {
            try
            {
                SupplierProfileImage existingCustomer = uow.Repository<SupplierProfileImage>().GetAll().Where(x => x.SupplierId == supplierProfileImage.SupplierId).FirstOrDefault();

                //supplierProfileImage.ProfileImage = FilterImages(supplierProfileImage.ProfileImage);

                if (existingCustomer == null)
                {
                    uow.Repository<SupplierProfileImage>().Add(supplierProfileImage);
                }
                else
                {
                    existingCustomer.ProfileImage = supplierProfileImage.ProfileImage;
                    existingCustomer.ModifiedBy = supplierProfileImage.CreatedBy;
                    existingCustomer.ModifiedOn = supplierProfileImage.CreatedOn;

                    uow.Repository<SupplierProfileImage>().Update(existingCustomer);
                }

                uow.Save();

                return new Response() { Status = ResponseStatus.OK };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response() { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }

        public JobImages GetJobImages(string imageUrl)
        {
            try
            {
                return uow.Repository<JobImages>().GetAll().Where(x => x.FileName == imageUrl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobImages();
            }
        }

        public SupplierAdImage GetSupplierAdImageByUrl(string imageUrl)
        {
            try
            {
                return uow.Repository<SupplierAdImage>().GetAll().Where(x => x.FileName == imageUrl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierAdImage();
            }
        }

        public Response FeaturedSupplierImages(FeaturedSupplier featuredSupplier)
        {
            Response response = new Response();
            //var splitName = skill.Base64Image.Split(',');
            //string convert = skill.Base64Image.Replace(splitName[0] + ",", String.Empty);
            //skill1.SkillImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
            var i = 0;
            foreach (var item in featuredSupplier.base64ImageArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    FeaturedSupplierImages featuredSupplierImages = new FeaturedSupplierImages();
                    if (i == 0)
                        featuredSupplierImages.IsActive = featuredSupplier.imageStatus1;
                    else if (i == 1)
                        featuredSupplierImages.IsActive = featuredSupplier.imageStatus2;
                    else
                        featuredSupplierImages.IsActive = featuredSupplier.imageStatus3;


                    var splitName = item.Split(',');
                    string convert = item.Replace(splitName[0] + ",", String.Empty);
                    featuredSupplierImages.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    featuredSupplierImages.SupplierId = featuredSupplier.supplierId;
                    uow.Repository<FeaturedSupplierImages>().Add(featuredSupplierImages);
                    uow.Save();
                }
                i++;
            }
            response.Status = ResponseStatus.OK;
            response.Message = "Images Added Succesfully!";
            return response;
        }

        public Response UpdateFeaturedSupplierImages(FeaturedSupplier featuredSupplier)
        {
            Response response = new Response();
            var i = 0;
            foreach (var item in featuredSupplier.base64ImageArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (i == 0)
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId1).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus1;
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            firstImage.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }
                        else
                        {
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            FeaturedSupplierImages fsi = new FeaturedSupplierImages();
                            fsi.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            fsi.IsActive = featuredSupplier.imageStatus1;
                            fsi.SupplierId = featuredSupplier.supplierId;
                            uow.Repository<FeaturedSupplierImages>().Add(fsi);
                            uow.Save();
                        }
                    }
                    else if (i == 1)
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId2).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus2;
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            firstImage.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }
                        else
                        {
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            FeaturedSupplierImages fsi = new FeaturedSupplierImages();
                            fsi.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            fsi.IsActive = featuredSupplier.imageStatus2;
                            fsi.SupplierId = featuredSupplier.supplierId;
                            uow.Repository<FeaturedSupplierImages>().Add(fsi);
                            uow.Save();
                        }

                    }
                    else
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId3).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus3;
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            firstImage.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }
                        else
                        {
                            var splitName = item.Split(',');
                            string convert = item.Replace(splitName[0] + ",", String.Empty);
                            FeaturedSupplierImages fsi = new FeaturedSupplierImages();
                            fsi.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            fsi.IsActive = featuredSupplier.imageStatus3;
                            fsi.SupplierId = featuredSupplier.supplierId;
                            uow.Repository<FeaturedSupplierImages>().Add(fsi);
                            uow.Save();
                        }
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId1).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus1;
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }
                    }
                    else if (i == 1)
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId2).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus2;
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }

                    }
                    else
                    {
                        var firstImage = uow.Repository<FeaturedSupplierImages>().Get(x => x.ImageId == featuredSupplier.imageId3).FirstOrDefault();
                        if (firstImage != null)
                        {
                            firstImage.IsActive = featuredSupplier.imageStatus3;
                            uow.Repository<FeaturedSupplierImages>().Update(firstImage);
                            uow.Save();
                        }
                    }

                }
                i++;
            }
            response.Status = ResponseStatus.OK;
            response.Message = "Images upated Succesfully!";
            return response;
        }

        public List<SupplierDTO> GetFeaturedSupplierImages()
        {
            List<SupplierDTO> listDto = new List<SupplierDTO>();
            try
            {
                SqlParameter[] sqlParameters = { };
                var ImagesList = uow.ExecuteReaderSingleDS<SupplierDTO>("GetFeaturedSupplierImages", sqlParameters);
                return ImagesList;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<SupplierDTO>();
            }
        }

        private byte[] FilterImages(byte[] _imageBytes)
        {
            try
            {
                byte[] filteredImage = _imageBytes;

                isImageReplaced = false;

                var googleImage = Image.FromBytes(_imageBytes);
                SafeSearchAnnotation responce = client.DetectSafeSearch(googleImage);

                switch (responce.Adult)
                {
                    case Likelihood.Possible:
                    case Likelihood.Likely:
                    case Likelihood.VeryLikely:
                        if (!isImageReplaced)
                        {
                            filteredImage = ReplaceUnsafeImage(_imageBytes);
                            isImageReplaced = true;
                        }
                        break;
                }

                switch (responce.Racy)
                {
                    case Likelihood.Possible:
                    case Likelihood.Likely:
                    case Likelihood.VeryLikely:
                        if (!isImageReplaced)
                        {
                            filteredImage = ReplaceUnsafeImage(_imageBytes);
                            isImageReplaced = true;
                        }
                        break;
                }

                switch (responce.Violence)
                {
                    case Likelihood.Possible:
                    case Likelihood.Likely:
                    case Likelihood.VeryLikely:
                        if (!isImageReplaced)
                        {
                            filteredImage = ReplaceUnsafeImage(_imageBytes);
                            isImageReplaced = true;
                        }
                        break;
                }

                return filteredImage;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return _imageBytes;
            }
        }

        public byte[] ReplaceUnsafeImage(byte[] _imageBytes)
        {
            string path = Path.Combine(hostingEnvironment.WebRootPath, "inapplicable.jpg");
            byte[] imageBytes = File.ReadAllBytes(path);
            return imageBytes;
        }

        public JobImages GetJobMainImages(long quotationId)
        {
            JobImages bidImage = new JobImages();
            bidImage = uow.Repository<JobImages>().Get(ji => ji.JobQuotationId == quotationId && ji.IsMain).FirstOrDefault();
            return bidImage;
        }

        public List<JobImages> GetJobImage(long quotationId)
        {
            List<JobImages> bidImage = new List<JobImages>();
            bidImage = uow.Repository<JobImages>().Get(ji => ji.JobQuotationId == quotationId).ToList();
            return bidImage;
        }

        public ImageVM GetMarkeetPlaceProductsImages(long adImageId)
        {
            SupplierAdImage supplierAd = new SupplierAdImage();
            ImageVM imageVM = new ImageVM();
            supplierAd = uow.Repository<SupplierAdImage>().Get(x => x.AdImageId == adImageId && x.IsMain == true).FirstOrDefault();
            if (supplierAd?.ThumbnailImage != null)
            {
                imageVM.ThumbImageContent = supplierAd?.ThumbnailImage;
            }
            else
            {
                imageVM.ThumbImageContent = supplierAd?.AdImage;
            }
            return imageVM;
        }
        public async Task<Response> AddSupplierProductImages(AddProductVM addProductVM)
        {
            try
            {
                Response response = new Response();
                if (addProductVM.Action == "edit")
                {
                    SqlParameter[] sqlParameters = {
                        new SqlParameter("productId",addProductVM.Id)
                    };
                    uow.ExecuteReaderSingleDS<Response>("SP_DeleteProductsImagesByProductId", sqlParameters).FirstOrDefault();
                }
                foreach (var file in addProductVM.Files)
                {
                    ProductImages productImages = new ProductImages();
                    productImages.ProductId = addProductVM.Id;
                    productImages.FileName = file.FileName;
                    productImages.FilePath = file.FilePath;
                    productImages.IsMain = file.IsMain;
                    productImages.CreatedOn = DateTime.Now;
                    await uow.Repository<ProductImages>().AddAsync(productImages);
                    await uow.SaveAsync();
                }
                response.Status = ResponseStatus.OK;
                response.Message = "Data saved successfully!";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Response();
            }

        }
        public async Task<Response> MarkProductImageAsMain(SupplierViewModels.ProductImageDTO productImageDTO)
        {
            try
            {
                Response response = new Response();
                var imageList = uow.Repository<ProductImages>().Get(x => x.ProductId == productImageDTO.productId).ToList();
                if (imageList.Count > 0)
                {
                    foreach (var file in imageList)
                    {
                        if (file.FileName == productImageDTO.fileName)
                        {
                            file.IsMain = true;
                        }
                        else
                        {
                            file.IsMain = false;
                        }
                        uow.Repository<ProductImages>().Update(file);
                        await uow.SaveAsync();
                    }

                    response.Status = ResponseStatus.OK;
                    response.Message = "Main image marked!";
                }
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new Response();
            }

        }
        public async Task<Response> AddAndUpdateLogo(string data)
        {
            var response = new Response();
            try
            {
                var entity = JsonConvert.DeserializeObject<ProfileImageDTO>(data);
                var splitName = entity.ProfileImage?.Split(',');

                string convert = entity.ProfileImage.Replace(splitName[0] + ",", String.Empty);
                var convertedImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@ProfileImageId",entity.ProfileImageId ),
                    new SqlParameter("@SupplierId",entity.SupplierId ),
                    new SqlParameter("@ProfileImage",convertedImage),
                    new SqlParameter("@CreatedBy", entity.CreatedBy)

                };
                var result = uow.ExecuteReaderSingleDS<Response>("Sp_AddorUpdateSupplierLogo", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Logo Successfully !!!";



            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }
        public async Task<Response> GetLogoData(long supplierId)
        {
            var response = new Response();
            try
            {
                var ProfileData = uow.Repository<SupplierProfileImage>().Get(x => x.SupplierId == supplierId).FirstOrDefault();
                response.ResultData = ProfileData;
                response.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
            }
            return response;
        }


    }
}