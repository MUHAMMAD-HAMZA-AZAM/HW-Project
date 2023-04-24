using HW.Http;
using HW.ImageModels;
using HW.SupplierViewModels;
using HW.TradesmanViewModels;
using HW.UserViewModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageVM = HW.TradesmanViewModels.ImageVM;

namespace HW.Gateway.Services
{
    public interface IImagesService
    {
        Task<bool> AddUpdateTradesmanProfileImage(UpdateTradesmanProfileImageVM updateTradesmanProfileImageVM);
        Task<bool> AddUpdateUserProfileImage(UpdateCustomerProfileImageVM updateCustomerProfileImageVM);
        Task<bool> AddUpdateSupplierProfileImage(UpdateSupplierProfileImageVM updateSupplierProfileImageVM);
        Task<ImageVM> GetJobImageByUrl(string imageUrl);
        Task<ImageVM> GetSupplierAdImageByUrl(string imageUrl);
        Task<JobImages> GetJobMainImages(long quotationId);
        Task<List<JobImages>> GetJobImages(long quotationId);
        Task<ImageVM> GetTradesmanProfileImageByTradesmanId(long profileImageId);
        Task<ImageVM> GetImageByCustomerId(long customerId);
        Task<ImageVM> GetMarkeetPlaceProductsImages(long adImageId);
        Task<Response> AddSupplierProductImages(AddProductVM addProductVM);
        Task<Response> MarkProductImageAsMain(ProductImageDTO productImageDTO);
    }

    public class ImagesService : IImagesService
    {
        private readonly IHttpClientService httpClient;
        private readonly ApiConfig _apiConfig;
        private readonly IExceptionService Exc;

        public ImagesService(IHttpClientService httpClient, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<JobImages> GetBidImage()
        {
            try
            {
                var data = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetBidImage}", "");
                return JsonConvert.DeserializeObject<JobImages>(data);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        public async Task<bool> AddUpdateTradesmanProfileImage(UpdateTradesmanProfileImageVM updateTradesmanProfileImageVM)
        {
            try
            {
                //For Angular Web UserProfile Image
                if (!string.IsNullOrWhiteSpace(updateTradesmanProfileImageVM.ImageBase64))
                {
                    var checkFormatList = updateTradesmanProfileImageVM.ImageBase64.Split(',');

                    if (checkFormatList[0] == "data:image/png;base64")
                    {
                        string convert = updateTradesmanProfileImageVM.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                        updateTradesmanProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpg;base64")
                    {
                        string convert = updateTradesmanProfileImageVM.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                        updateTradesmanProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpeg;base64")
                    {
                        string convert = updateTradesmanProfileImageVM.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                        updateTradesmanProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                }


                TradesmanProfileImage tradesmanProfileImage = new TradesmanProfileImage()
                {
                    TradesmanId = updateTradesmanProfileImageVM.TradesmanId,
                    ProfileImage = updateTradesmanProfileImageVM.ProfileImage,
                    CreatedBy = updateTradesmanProfileImageVM.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                Response response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.AddUpdateTradesmanProfileImage}", tradesmanProfileImage)
                );

                return response.Status == ResponseStatus.OK ? true : false;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public async Task<bool> AddUpdateUserProfileImage(UpdateCustomerProfileImageVM updateCustomerProfileImageVM)
        {
            try
            {   //For Angular Web UserProfile Image
                if (!string.IsNullOrWhiteSpace(updateCustomerProfileImageVM.ImageBase64))
                {
                    var checkFormatList = updateCustomerProfileImageVM.ImageBase64.Split(',');
                    if (checkFormatList[0] == "data:image/png;base64")
                    {
                        string convert = updateCustomerProfileImageVM.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                        updateCustomerProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpg;base64")
                    {
                        string convert = updateCustomerProfileImageVM.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                        updateCustomerProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpeg;base64")
                    {
                        string convert = updateCustomerProfileImageVM.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                        updateCustomerProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                }
                CustomerProfileImage customerProfileImage = new CustomerProfileImage()
                {
                    CustomerId = updateCustomerProfileImageVM.UserId,
                    ProfileImage = updateCustomerProfileImageVM.ProfileImage,
                    CreatedBy = updateCustomerProfileImageVM.CreatedBy,
                    CreatedOn = DateTime.Now
                };
                if (updateCustomerProfileImageVM != null && updateCustomerProfileImageVM.UserId > 0)
                {
                    Response response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.AddUpdateUserProfileImage}", customerProfileImage));
                    return response?.Status == ResponseStatus.OK ? true : false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        //public byte[] ConvertToByte(string imageFile)
        //{
        //    return Convert.FromBase64String(imageFile);
        //}
        public async Task<bool> AddUpdateSupplierProfileImage(UpdateSupplierProfileImageVM updateSupplierProfileImageVM)
        {
            try
            {


                //For Angular Web UserProfile Image
                if (!string.IsNullOrWhiteSpace(updateSupplierProfileImageVM.ImageBase64))
                {
                    var checkFormatList = updateSupplierProfileImageVM.ImageBase64.Split(',');

                    if (checkFormatList[0] == "data:image/png;base64")
                    {
                        string convert = updateSupplierProfileImageVM.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                        updateSupplierProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpg;base64")
                    {
                        string convert = updateSupplierProfileImageVM.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                        updateSupplierProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                    else if (checkFormatList[0] == "data:image/jpeg;base64")
                    {
                        string convert = updateSupplierProfileImageVM.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                        updateSupplierProfileImageVM.ProfileImage = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                    }
                }




                SupplierProfileImage supplierProfileImage = new SupplierProfileImage()
                {
                    SupplierId = updateSupplierProfileImageVM.SupplierId,
                    ProfileImage = updateSupplierProfileImageVM.ProfileImage,
                    CreatedBy = updateSupplierProfileImageVM.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                Response response = JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.AddUpdateSupplierProfileImage}", supplierProfileImage)
                );

                return response.Status == ResponseStatus.OK ? true : false;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }
        public async Task<ImageVM> GetJobImageByUrl(string imageUrl)
        {
            try
            {
                string jobImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImages}?imageUrl={imageUrl}");
                JobImages jobImages = JsonConvert.DeserializeObject<JobImages>(jobImagesJson);

                if (jobImages?.BidImage?.Length > 0)
                {
                    ImageVM imageVM = new ImageVM()
                    {
                        ImageContent = jobImages.BidImage,
                        IsMain = jobImages.IsMain,
                        Id = jobImages.BidImageId,
                        FilePath = jobImages.FileName
                    };

                    return imageVM;
                }

                return new ImageVM();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ImageVM();
            }
        }

        public async Task<ImageVM> GetTradesmanProfileImageByTradesmanId(long profileImageId)
        {
            try
            {
                TradesmanProfileImage tradesmanProfileImage = JsonConvert.DeserializeObject<TradesmanProfileImage>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImageByTradesmanId}?tradesmanId={profileImageId}", ""));


                ImageVM imageVM = new ImageVM()
                {
                    ImageContent = tradesmanProfileImage?.ProfileImage,
                    Id = tradesmanProfileImage?.ProfileImageId ?? 0
                };

                return imageVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ImageVM();
            }
        }

        public async Task<ImageVM> GetSupplierAdImageByUrl(string imageUrl)
        {
            try
            {
                var jobImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetSupplierAdImageByUrl}?imageUrl={imageUrl}");
                SupplierAdImage supplierAdImage = JsonConvert.DeserializeObject<SupplierAdImage>(jobImagesJson);
                ImageVM imageVM = new ImageVM()
                {
                    ImageContent = supplierAdImage.AdImage,
                    IsMain = supplierAdImage.IsMain.Value,
                    Id = supplierAdImage.AdImageId,
                    FilePath = supplierAdImage.FileName
                };

                return imageVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ImageVM();
            }
        }

        public async Task<JobImages> GetJobMainImages(long quotationId)
        {
            string jobImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobMainImage}?quotationid={quotationId}");
            JobImages jobImages = JsonConvert.DeserializeObject<JobImages>(jobImagesJson);
            return jobImages;
        } 
        public async Task<List<JobImages>> GetJobImages(long quotationId)
        {
            var jobImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetJobImage}?quotationid={quotationId}");
            List<JobImages> jobImages = JsonConvert.DeserializeObject<List<JobImages>>(jobImagesJson);
            return jobImages;
        }

        public async Task<ImageVM> GetImageByCustomerId(long customerId)
        {
            try
            {
                ImageVM imageVM = new ImageVM();
                string userImagesJson = await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customerId}");
                CustomerProfileImage customerProfileImg = JsonConvert.DeserializeObject<CustomerProfileImage>(userImagesJson);
                if (customerProfileImg != null)
                {
                    imageVM.Id = customerProfileImg.CustomerId;
                    imageVM.ImageContent = customerProfileImg.ProfileImage;
                }
                return imageVM;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new ImageVM();
            }
           
            

          
        }

        public async Task<ImageVM> GetMarkeetPlaceProductsImages(long adImageId)
        {
            return JsonConvert.DeserializeObject<ImageVM>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetMarkeetPlaceProductsImages}?adImageId={adImageId}"));
        }
        public async Task<Response> AddSupplierProductImages(AddProductVM addProductVM)
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.AddSupplierProductImages}",addProductVM));
        }        
        public async Task<Response> MarkProductImageAsMain(ProductImageDTO productImageDTO)
        {
            return JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.MarkProductImageAsMain}", productImageDTO));
        }
    }
}
