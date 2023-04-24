
using Hw.EmailViewModel;
using HW.AudioModels;
using HW.CallModels;
using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.CustomerModels;
using HW.EmailViewModel;
using HW.GatewayApi.Code;
using HW.Http;
using HW.IdentityViewModels;
using HW.ImageModels;
using HW.Job_ViewModels;
using HW.JobModels;
using HW.NotificationViewModels;
using HW.PackagesAndPaymentsModels;
using HW.TradesmanModels;
using HW.TradesmanViewModels;
using HW.UserManagmentModels;
using HW.UserViewModels;
using HW.Utility;
using HW.VideoModels;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;
using BidStatus = HW.Utility.BidStatus;
using CallHistoryVM = HW.TradesmanViewModels.CallHistoryVM;
using ImageVM = HW.UserViewModels.ImageVM;
using Response = HW.Utility.Response;

namespace HW.GatewayApi.Services
{
    public interface ICustomerService
    {
        //Task<string> GetAll();
        Task<string> GetById(long id);
        Task<string> GetByName(string name);
        Task<string> Add(Customer data);
        Task<string> Delete(long id);
        Task<BidDetailVM> GetCustomerDetailsById(long tradesmanid, long id);
        Task<BidDetailVM> GetCustomerDetailsByIdWeb(long tradesmanid, long id);
        Task<BidDetailVM> GetJobDetailsById(long jobDetailId, long tradesmanId);
        Task<BidDetailVM> GetJobDetailsByIdWeb(long jobDetailId, long tradesmanId);
        Task<List<FavoritesVM>> GetCustomerFavoriteByCustomerId(long customerId);
        Task<List<GetPaymentRecordVM>> getPaymentRecords(long customerId);
        Task<List<CallHistoryVM>> GetJobQuotationCallLogs(long jobQuotationId);
        Task<JobQuotationCallInfoVM> GetJobQuotationCallInfo(long tradesmanId, long bidId, long customerId);
        Task<Response> JobQuotations(QuotationVM quotationVM);
        Task<List<IdValueVM>> GetCityList();
        Task<Response> SetTradesmanToCustomerFavorite(long tradesmanId, long customerId, string createdBy, bool isFavorite);
        Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId, long customerId, string userId);
        Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId, long customerId, string userId);
        Task<Response> AddCustomerProductRating(int rating, long supplierAdId, long customerId, string userId);
        Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId, long customerId, string userId);
        Task<Response> AddAdViews(long supplierAdId, long customerId, string userId);
        Task<Response> UpdateJobQuotation(JobQuotationDetailVM quotationDetailVM, string createdBy);
        Task<UserProfileVM> GetCustomerByUserId(string userId);
        Task<Response> DeleteJobQuotation(long jobQuotationId);
        Task<SupplierViewModels.CallInfoVM> GetCustomerById(long tradesmanId, long customerId, bool todaysRecordOnly);
        Task<PersonalDetailsVM> GetPersonalDetails(long customerId);
        Task<bool> UpdatePersonalDetails(PersonalDetailsVM personalDetailVM);
        Task AddVideo(UserViewModels.VideoVM videoVM, string userId);
        Task<Response> PostJobQuotationWeb(QuotationVM quotationVM);
        Task<Response> Login(LoginVM model);
        Task<Response> JobQuotationsWeb(QuotationVM quotationVM);
        Task<List<Customer>> GetAllCustomers();
        Task<List<AdViewerVM>> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber);
        Task<List<UserFavoriteTradesmenVM>> GetCustomerFavoriteTradesman(long customerId, int pageSize, int pageNumber);
        Task<List<AdViewerVM>> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber);
        Task<List<AdRatingsVm>> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber);
        Task<List<TopTradesmanVM>> GetTopTradesman(int pageSize, int pageNumber, long CategoryId, long customerId);
        Task<bool> UpdateCustomerPublicId(long customerId, string publicId);
        Task<CustomerDashBoardCountVM> GetCustomerDashBoardCount(long customerId, string userId);
        Task<Response> UpdateJobQuotationStatus(long jobQuotationId, int statusId);
        Task<string> SaveAndRemoveProductsInWishlist(string data);
        Task<Response> CheckProductStatusInWishList(long customerId, long productId);
        Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize);

        Task<string> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize);
        Task<string> GetUserDetailsByUserRole(string userId, string userRole);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientService httpClient;
        private readonly IUnitOfWork uow;
        private readonly ClientCredentials clientCred;
        private readonly ICommunicationService communicationService; // sending job post confirmation email
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public CustomerService(IHttpClientService httpClient, ClientCredentials clientCred, ICommunicationService communicationService, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
            this.communicationService = communicationService;
        }

        //public async Task<string> GetAll()
        //{
        //    try
        //    {
        //        DiscoveryResponse disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl);
        //        return await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAll}", "eyJhbGciOiJSUzI1NiIsImtpZCI6ImE1YWNiMTk2MmFlODVhZDZmZDIwNmM3ZDkxNWYxNTdkIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1MzAxODAzMzgsImV4cCI6MTUzMDE4MzkzOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTI0MyIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjUxMjQzL3Jlc291cmNlcyIsIkN1c3RvbWVyQXBpIl0sImNsaWVudF9pZCI6IkdhdGV3YXkiLCJzdWIiOiI5MWM0OTNhMi04MDhiLTQ2MTEtYjA0NS1hZWU5MDAzYmMzZWUiLCJhdXRoX3RpbWUiOjE1MzAxODAzMzgsImlkcCI6ImxvY2FsIiwicm9sZSI6IkN1c3RvbWVyIiwic2NvcGUiOlsiQ3VzdG9tZXJBcGkiXSwiYW1yIjpbInB3ZCJdfQ.i7x8thqFtpCnv0-7FwSYalh3ZYLLG87bIiG40VnHjbrMrDUqgpT3b9uDrY4QZZv_dP8mCQ6QqqNNd9kaVVIfR6F-ZMHiCk-nG9_NFeaAu9YpOOaMGyIk4mFZ1etCmecRT6s8b59cfoDdDjXyRacXi0dan2zDnzBVJy68GC0GLNU3EvT6uR0wa2DeWNr-JRTrdudV8ZY3B2tXYosuCJImAPtJlMdQsg-ZNtEJsxvhN55MOtW0iyQVcBSYKcVgDJ4YZph0rt4hbHvrB8mXuOCtaIQSyR_NCjmt8XUt1LMle2dP7JP_bbWgSbByCi95QKDt6SkZQV19u5Z8NkVT9VtYtw");

        //    }
        //    catch (Exception ex)
        //    {
        //        Exc.AddErrorLog(ex);

        //        return ex.Message;
        //    }
        //}

        public async Task<string> GetById(long id)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}/{id}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<string> GetByName(string name)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetByName}?name={name}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<string> Add(Customer data)
        {
            try
            {
                return await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.Add}", data);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<string> Delete(long id)
        {
            try
            {
                return await httpClient.DeleteAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.Delete}", id);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public async Task<BidDetailVM> GetCustomerDetailsById(long tradesmanId, long id)
        {
            BidDetailVM custDetailVM = new BidDetailVM();

            try
            {
                BidAudio audio = new BidAudio();

                JobQuotation job = JsonConvert.DeserializeObject<JobQuotation>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={id}", ""));

                if (job == null) { job = new JobQuotation(); }

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={job.CustomerId}", "")
                );

                if (customer == null) { customer = new Customer(); }

                Bids bids = JsonConvert.DeserializeObject<Bids>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidJobQuotaionId}?jobQuotationId={job.JobQuotationId}&tradesmanId={tradesmanId}", "")
                );

                if (bids != null)
                {
                    audio = JsonConvert.DeserializeObject<BidAudio>(
                        await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetByJobQuotationId}?jobQuotationId={bids.BidsId}", "")
                    );
                }

                JobQuotationVideo video = JsonConvert.DeserializeObject<JobQuotationVideo>(
                    await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={id}", "")
                );

                List<JobImages> images = JsonConvert.DeserializeObject<List<JobImages>>(
                    await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByJobDetailId}?jobQuotationId={id}", "")
                );

                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={job.JobQuotationId}", "")
                );

                City city = JsonConvert.DeserializeObject<City>(
                    await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}")
                );

                if (images != null)
                {
                    custDetailVM.JobImages = new List<TradesmanViewModels.ImageVM>();

                    custDetailVM.JobImages = images.Select(img => new TradesmanViewModels.ImageVM
                    {
                        ImageContent = img.BidImage,
                        FilePath = img.FileName,
                        Id = img.BidImageId,
                        IsMain = img.IsMain

                    }).ToList();
                }

                if (video != null)
                {
                    TradesmanViewModels.VideoVM bidvideo = new TradesmanViewModels.VideoVM()
                    {
                        FilePath = video.FileName != null ? video.FileName : "",
                        //VideoContent = video.Video
                    };
                    custDetailVM.Video = bidvideo;
                }
                if (audio != null)
                {
                    TradesmanViewModels.AudioVM bidAudio = new TradesmanViewModels.AudioVM()
                    {
                        FileName = audio.FileName != null ? audio.FileName : "",
                        //AudioContent = audio.Audio
                    };
                    custDetailVM.AudioMessage = bidAudio;
                }
                if (bids != null)
                {
                    custDetailVM.TradesmanBid = bids?.Amount != 0 ? Math.Round(bids.Amount, 2) : 0;
                    custDetailVM.BidId = bids.BidsId;
                }
                custDetailVM.JobQuotationId = job.JobQuotationId != 0 ? job.JobQuotationId : 0;
                custDetailVM.CustomerName = customer?.FirstName + " " + customer?.LastName;
                custDetailVM.CustomerId = customer?.CustomerId ?? 0;
                // custDetailVM.CustomerProfileImage=customer.
                custDetailVM.JobTitle = job?.WorkTitle;
                custDetailVM.JobDescription = job?.WorkDescription;
                custDetailVM.Budget = job.WorkBudget.HasValue ? Math.Round(job.WorkBudget.Value, 2) : 0;
                custDetailVM.ExpectedJobStartDate = job.WorkStartDate?.ToString("dd MMM yyyy");
                custDetailVM.ExpectedJobStartTime = job.WorkStartDate?.ToString("hh:mm tt");
                custDetailVM.TradesmanMessage = bids?.Comments;
                custDetailVM.JobLocation = jobAddress?.GpsCoordinates;
                custDetailVM.JobAddressLine = jobAddress?.AddressLine;
                custDetailVM.JobAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerPhoneNumber = customer?.MobileNumber;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return custDetailVM;
        }

        public async Task<BidDetailVM> GetCustomerDetailsByIdWeb(long tradesmanId, long id)
        {
            BidDetailVM custDetailVM = new BidDetailVM();

            try
            {
                BidAudio audio = new BidAudio();
                JobQuotation job = JsonConvert.DeserializeObject<JobQuotation>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobQuotationById}?id={id}", ""));
                if (job == null) { job = new JobQuotation(); }
                Customer customer = JsonConvert.DeserializeObject<Customer>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={job.CustomerId}", ""));
                Response userDetails =JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}", ""));
                UserRegisterVM userRegister = JsonConvert.DeserializeObject<UserRegisterVM>(userDetails?.ResultData?.ToString());
                if (customer == null) { customer = new Customer(); }
                Bids bids = JsonConvert.DeserializeObject<Bids>
                    (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidJobQuotaionId}?jobQuotationId={job.JobQuotationId}&tradesmanId={tradesmanId}", ""));

                if (bids != null)
                {
                    audio = JsonConvert.DeserializeObject<BidAudio>(await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetByJobQuotationId}?jobQuotationId={bids.BidsId}", ""));
                }

                //JobQuotationVideo video = JsonConvert.DeserializeObject<JobQuotationVideo>(await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={id}", ""));
                List<JobImages> images = JsonConvert.DeserializeObject<List<JobImages>>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByJobDetailId}?jobQuotationId={id}", ""));
                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={job.JobQuotationId}", ""));
                City city = JsonConvert.DeserializeObject<City>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}"));

                if (images != null)
                {
                    custDetailVM.JobImages = new List<TradesmanViewModels.ImageVM>();

                    custDetailVM.JobImages = images.Select(img => new TradesmanViewModels.ImageVM
                    {
                        ImageContent = img.BidImage,
                        FilePath = img.FileName,
                        Id = img.BidImageId,
                        IsMain = img.IsMain

                    }).ToList();
                }

                //if (video != null)
                //{
                //    TradesmanViewModels.VideoVM bidvideo = new TradesmanViewModels.VideoVM()
                //    {
                //        FilePath = video.FileName != null ? video.FileName : "",
                //        //VideoContent = video.Video
                //    };
                //    custDetailVM.Video = bidvideo;
                //}
                if (audio != null)
                {
                    TradesmanViewModels.AudioVM bidAudio = new TradesmanViewModels.AudioVM()
                    {
                        FileName = audio.FileName != null ? audio.FileName : "",
                        AudioContent = audio.Audio
                    };
                    custDetailVM.AudioMessage = bidAudio;
                }
                if (bids != null)
                {
                    custDetailVM.TradesmanBid = bids?.Amount != 0 ? Math.Round(bids.Amount, 2) : 0;
                    custDetailVM.BidId = bids.BidsId;
                }
                custDetailVM.JobQuotationId = job.JobQuotationId != 0 ? job.JobQuotationId : 0;
                custDetailVM.CustomerName = customer?.FirstName + " " + customer?.LastName;
                custDetailVM.CustomerId = customer?.CustomerId ?? 0;
                custDetailVM.JobTitle = job?.WorkTitle;
                custDetailVM.JobDescription = job?.WorkDescription;
                custDetailVM.Budget = job.WorkBudget.HasValue ? Math.Round(job.WorkBudget.Value, 2) : 0;
                custDetailVM.ExpectedJobStartDate = job.WorkStartDate?.ToString("dd MMM yyyy");
                custDetailVM.ExpectedJobStartTime = job.WorkStartDate?.ToString("hh:mm tt");
                custDetailVM.TradesmanMessage = bids?.Comments;
                custDetailVM.JobLocation = jobAddress?.GpsCoordinates;
                custDetailVM.JobAddressLine = jobAddress?.AddressLine;
                custDetailVM.JobAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.FirebaseClientId = userRegister.FirebaseClientId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return custDetailVM;
        }

        public async Task<BidDetailVM> GetJobDetailsById(long jobDetailId, long tradesmanId)
        {
            BidDetailVM custDetailVM = new BidDetailVM();
            JobQuotationVideo video = new JobQuotationVideo();
            List<JobImages> images = new List<JobImages>();
            BidAudio audio = new BidAudio();
            try
            {
                custDetailVM.Video = new TradesmanViewModels.VideoVM();
                custDetailVM.AudioMessage = new TradesmanViewModels.AudioVM();

                JobDetail job = JsonConvert.DeserializeObject<JobDetail>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsByJobQuotationId}?jobQuotationId={jobDetailId}", "")
                );

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={job.CustomerId}", "")
                );

                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={job.JobQuotationId}", "")
                );

                City city = JsonConvert.DeserializeObject<City>(
                    await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}")
                );

                Bids bids = JsonConvert.DeserializeObject<Bids>(
                    await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidJobQuotaionId}?jobQuotationId={job.JobQuotationId}&tradesmanId={tradesmanId}", "")
                );

                if (bids != null)
                {
                    audio = JsonConvert.DeserializeObject<BidAudio>
                       (await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetByJobQuotationId}?jobQuotationId={bids.BidsId}", ""));
                }

                if (job.JobQuotationId > 0)
                {
                    video = JsonConvert.DeserializeObject<JobQuotationVideo>(
                        await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={job.JobQuotationId}", "")
                    );

                    images = JsonConvert.DeserializeObject<List<JobImages>>(
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByJobDetailId}?jobQuotationId={job.JobQuotationId}", "")
                    );
                }

                custDetailVM.LatLong = jobAddress?.GpsCoordinates;
                custDetailVM.JobQuotationId = job.JobQuotationId;
                custDetailVM.UserId = customer?.UserId;
                custDetailVM.CustomerName = customer?.FirstName + customer?.LastName;
                custDetailVM.CustomerId = customer?.CustomerId ?? 0;
                custDetailVM.JobTitle = job?.Title;
                custDetailVM.JobDescription = job?.Description;
                custDetailVM.Budget = Math.Round(job.Budget, 2);
                custDetailVM.TradesmanBid = decimal.Round(job.TradesmanBudget.HasValue ? job.TradesmanBudget.Value : 0, 2, MidpointRounding.AwayFromZero);
                custDetailVM.ExpectedJobStartDate = job?.StartDate.ToString("dd MMM yyyy");
                custDetailVM.ExpectedJobStartTime = job?.StartDate.ToString("hh:mm tt");
                custDetailVM.TradesmanMessage = bids?.Comments;
                custDetailVM.JobDetailId = job?.JobDetailId ?? 0;
                custDetailVM.BidId = bids?.BidsId ?? 0;
                custDetailVM.UserAddress = $"{jobAddress.StreetAddress.Trim()} {jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerPhoneNumber = customer?.MobileNumber;
                custDetailVM.TradesmanBid = Math.Round(bids?.Amount ?? 0, 2);
                custDetailVM.PostedOn = job.CreatedOn;
                custDetailVM.StatusId = job.StatusId;
                custDetailVM.IsFinished = job.IsFinished;

                custDetailVM.JobImages = images?.Select(img => new TradesmanViewModels.ImageVM()
                {
                    FilePath = img?.FileName,
                    Id = img?.BidImageId ?? 0,
                    IsMain = img?.IsMain ?? false

                }).ToList();

                custDetailVM.Video.FilePath = video?.FileName;
                custDetailVM.AudioMessage.FileName = audio?.FileName;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            return custDetailVM;
        }

        public async Task<BidDetailVM> GetJobDetailsByIdWeb(long jobDetailId, long tradesmanId)
        {
            BidDetailVM custDetailVM = new BidDetailVM();
            JobQuotationVideo video = new JobQuotationVideo();
            List<JobImages> images = new List<JobImages>();
            BidAudio audio = new BidAudio();
            try
            {
                //custDetailVM.Video = new TradesmanViewModels.VideoVM();
                custDetailVM.AudioMessage = new TradesmanViewModels.AudioVM();
                JobDetail job = JsonConvert.DeserializeObject<JobDetail>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobDetailsById}?jobDetailId={jobDetailId}", ""));
                Customer customer = JsonConvert.DeserializeObject<Customer>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={job.CustomerId}", ""));
                JobAddress jobAddress = JsonConvert.DeserializeObject<JobAddress>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobAddress}?jobQuotationId={job.JobQuotationId}", ""));
                City city = JsonConvert.DeserializeObject<City>(await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={jobAddress.CityId}"));
                Bids bids = JsonConvert.DeserializeObject<Bids>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetBidJobQuotaionId}?jobQuotationId={job.JobQuotationId}&tradesmanId={tradesmanId}", ""));
                if (bids != null)
                {
                    audio = JsonConvert.DeserializeObject<BidAudio>
                       (await httpClient.GetAsync($"{_apiConfig.AudioApiUrl}{ApiRoutes.Audio.GetByJobQuotationId}?jobQuotationId={bids.BidsId}", ""));
                }

                if (job.JobQuotationId > 0)
                {
                    //video = JsonConvert.DeserializeObject<JobQuotationVideo>(await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.GetByJobQuotationId}?jobQuotationId={job.JobQuotationId}", ""));
                    images = JsonConvert.DeserializeObject<List<JobImages>>
                        (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByJobDetailId}?jobQuotationId={job.JobQuotationId}", ""));
                }

                custDetailVM.LatLong = jobAddress?.GpsCoordinates;
                custDetailVM.JobQuotationId = job.JobQuotationId;
                custDetailVM.CustomerName = $"{customer?.FirstName} {customer?.LastName}";
                custDetailVM.CustomerId = customer?.CustomerId ?? 0;
                custDetailVM.JobTitle = job?.Title;
                custDetailVM.JobDescription = job?.Description;
                custDetailVM.Budget = Math.Round(job.Budget, 2);
                custDetailVM.TradesmanBid = decimal.Round(job.TradesmanBudget.HasValue ? job.TradesmanBudget.Value : 0, 2, MidpointRounding.AwayFromZero);
                custDetailVM.ExpectedJobStartDate = job?.StartDate.ToString("dd MMM yyyy");
                custDetailVM.ExpectedJobStartTime = job?.StartDate.ToString("hh:mm tt");
                custDetailVM.TradesmanMessage = bids?.Comments;
                custDetailVM.JobDetailId = job?.JobDetailId ?? 0;
                custDetailVM.BidId = bids?.BidsId ?? 0;
                custDetailVM.UserAddress = $"{jobAddress.StreetAddress.Trim()} {jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.CustomerAddress = $"{jobAddress.Area.Trim()}, {city.Name}";
                custDetailVM.IsFinished = job?.IsFinished;
                custDetailVM.TradesmanBid = Math.Round(bids?.Amount ?? 0, 2);

                custDetailVM.JobImages = images?.Select(img => new TradesmanViewModels.ImageVM()
                {
                    FilePath = img?.FileName,
                    Id = img?.BidImageId ?? 0,
                    IsMain = img?.IsMain ?? false,
                    ImageContent = img?.BidImage

                }).ToList();

                //custDetailVM.Video.FilePath = video?.FileName;
                custDetailVM.AudioMessage.FileName = audio?.FileName;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }


            return custDetailVM;
        }

        public async Task<List<IdValueVM>> GetCityList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<IdValueVM>>(
                       await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetAllCities}", "")
                   );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<IdValueVM>();
            }
        }

        public async Task<JobQuotationCallInfoVM> GetJobQuotationCallInfo(long tradesmanId, long bidId, long customerId)
        {
            try
            {
                Tradesman tradesman = JsonConvert.DeserializeObject<Tradesman>(await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanById}?tradesmanId={tradesmanId}", ""));
                List<TradesmanCallLog> callLogs = JsonConvert.DeserializeObject<List<TradesmanCallLog>>(await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetCallLogByBidId}?tradesmanId={tradesmanId}&bidId={bidId}&customerId={customerId}", ""));
                TradesmanProfileImage tradesmanImage = JsonConvert.DeserializeObject<TradesmanProfileImage>(await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanImageById}?tradesmanId={tradesmanId}", ""));
                List<CallType> allCallTypes = JsonConvert.DeserializeObject<List<CallType>>(await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}", ""));

                JobQuotationCallInfoVM jobQuotationCallInfoVM = new JobQuotationCallInfoVM()
                {
                    TradesmanID = tradesman.TradesmanId,
                    TradesmanImage = tradesmanImage?.ProfileImage,
                    TradesmanName = tradesman.FirstName + " " + tradesman.LastName,

                    CallLogs = callLogs.Select(c => new UserViewModels.CallLogVM
                    {
                        CallDuration = c.Duration,
                        CallType = allCallTypes.Where(x => x.CallTypeId == c.CallType).FirstOrDefault().Name,
                    }).ToList()
                };
                return jobQuotationCallInfoVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotationCallInfoVM();
            }
        }

        public async Task<List<FavoritesVM>> GetCustomerFavoriteByCustomerId(long customerId)
        {
            List<FavoritesVM> favoritesVMs = new List<FavoritesVM>();

            try
            {
                List<CustomerFavorites> customerFavoriteContacts = JsonConvert.DeserializeObject<List<CustomerFavorites>>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerFavoriteByCustomerId}?customerId={customerId}", ""));

                List<long?> tradesmanIds = customerFavoriteContacts.Select(x => x.TradesmanId).Distinct().ToList();

                List<Tradesman> tradesmanList = JsonConvert.DeserializeObject<List<Tradesman>>(await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", tradesmanIds));
                List<TradesmanProfileImage> tradesmanImages = JsonConvert.DeserializeObject<List<TradesmanProfileImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImages}", tradesmanIds));

                foreach (CustomerFavorites contact in customerFavoriteContacts)
                {
                    Tradesman tradesman = tradesmanList.FirstOrDefault(x => x.TradesmanId == contact.TradesmanId);

                    FavoritesVM favoriteContacts = new FavoritesVM()
                    {
                        TradesmanId = contact.TradesmanId.HasValue ? contact.TradesmanId.Value : 0,
                        IsFavorite = contact.IsFavorite,
                        TradesmanName = $"{tradesman.FirstName} {tradesman.LastName}",
                        TradesmanImage = tradesmanImages.FirstOrDefault(x => x.TradesmanId == contact.TradesmanId)?.ProfileImage
                    };

                    favoritesVMs.Add(favoriteContacts);
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return favoritesVMs;
        }

        public async Task<List<CallHistoryVM>> GetJobQuotationCallLogs(long jobQuotationId)
        {
            try
            {
                List<TradesmanCallLog> callHistoryList = JsonConvert.DeserializeObject<List<TradesmanCallLog>>(await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetJobQuotationCallLogs}?jobQuotationId={jobQuotationId}", ""));

                List<long> tradesmanIds = callHistoryList.Select(c => c.TradesmanId).Distinct().ToList();

                List<Tradesman> tradesmanList = JsonConvert.DeserializeObject<List<Tradesman>>(await httpClient.PostAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanDetailsByTradesmanIds}", tradesmanIds));

                List<TradesmanProfileImage> tradesmanImageList = JsonConvert.DeserializeObject<List<TradesmanProfileImage>>(await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetTradesmanProfileImages}", tradesmanIds));

                List<CallType> calltypeList = JsonConvert.DeserializeObject<List<CallType>>(await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}"));

                List<CallHistoryVM> callHistoryVMs = new List<CallHistoryVM>();

                byte[] vs = tradesmanImageList.FirstOrDefault(ct => ct.TradesmanId == 22)?.ProfileImage;

                callHistoryVMs = callHistoryList.Select(c => new CallHistoryVM
                {
                    CallerName = tradesmanName(c.TradesmanId),
                    CallerImage = tradesmanImageList.FirstOrDefault(ct => ct.TradesmanId == c.TradesmanId)?.ProfileImage,
                    CallDuration = c.Duration,
                    TradesmanId = c.TradesmanId,
                    CallType = calltypeList.FirstOrDefault(ct => ct.CallTypeId == c.CallType)?.Name,
                    CallTime = c.CreatedOn,
                    CallLogId = c.TradesmanCallLogId

                }).ToList();

                return callHistoryVMs;

                string tradesmanName(long id)
                {
                    string name = tradesmanList.FirstOrDefault(t => t.TradesmanId == id).FirstName + " " + tradesmanList.FirstOrDefault(t => t.TradesmanId == id).LastName;
                    return name;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<CallHistoryVM>();
            }

        }

        public async Task<Response> SetTradesmanToCustomerFavorite(long tradesmanId, long customerId, string createdBy, bool isFavorite)
        {
            try
            {
                CustomerFavorites customerFavorites = new CustomerFavorites()
                {
                    CustomerId = customerId,
                    TradesmanId = tradesmanId,
                    IsFavorite = isFavorite,
                    IsActive = isFavorite,
                    CreatedBy = createdBy,
                    CreatedOn = DateTime.Now
                };

                return JsonConvert.DeserializeObject<Response>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.SetTradesmanToCustomerFavorite}", customerFavorites));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<Response> AddDeleteCustomerSavedAd(bool isSaved, long supplierAdId, long customerId, string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddDeleteCustomerSavedAd}?isSaved={isSaved}&supplierAdId={supplierAdId}&customerId={customerId}&userId={userId}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> AddDeleteCustomerLikedAd(bool isLiked, long supplierAdId, long customerId, string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddDeleteCustomerLikedAd}?isLiked={isLiked}&supplierAdId={supplierAdId}&customerId={customerId}&userId={userId}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> AddCustomerProductRating(int rating, long supplierAdId, long customerId, string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddCustomerProductRating}?rating={rating}&supplierAdId={supplierAdId}&customerId={customerId}&userId={userId}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        public async Task<Response> AddDeleteCustomerSavedTradesman(bool isLiked, long tradesmanId, long customerId, string userId)
        {
            try
            {
                return JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddDeleteCustomerSavedTradesman}?isLiked={isLiked}&tradesmanId={tradesmanId}&customerId={customerId}&userId={userId}", ""));

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        public async Task<List<GetPaymentRecordVM>> getPaymentRecords(long customerId)
        {
            List<GetPaymentRecordVM> getPaymentRecordVMs = new List<GetPaymentRecordVM>();
            try
            {
                List<TradesmanJobReceipts> tradesmanJobReceipts = new List<TradesmanJobReceipts>();

                List<JobDetail> jobDetail = JsonConvert.DeserializeObject<List<JobDetail>>
                   (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.GetJobStatusByCustomerId}?customerId={customerId}&statusId={(int)BidStatus.Completed}", ""));

                tradesmanJobReceipts = JsonConvert.DeserializeObject<List<TradesmanJobReceipts>>
                      (await httpClient.PostAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.getPaymentRecords}", jobDetail.Select(j => j.JobDetailId).ToList()));

                List<PaymentMethod> paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethod>>(await httpClient.GetAsync($"{_apiConfig.PackagesAndPaymentsApi}{ApiRoutes.PackagesAndPayments.GetAllPaymentMethods}", ""));

                getPaymentRecordVMs = tradesmanJobReceipts.Select(x => new GetPaymentRecordVM
                {
                    Amount = x?.Amount ?? 0,
                    CreatedOn = x.PaymentDate,
                    DirectPayment = x.DirectPayment,
                    JobDetailId = jobDetail.Where(a => a.JobDetailId == x.JobDetailId).Select(a => a.JobQuotationId).FirstOrDefault(),
                    Title = jobDetail.Where(s => s.JobDetailId == x.JobDetailId).Select(d => d.Title).FirstOrDefault(),
                    TransactionType = paymentMethods.Where(s => s.PaymentMethodId == x.PaymentMethodId).Select(s => s.Name).FirstOrDefault(),
                    DiscountedAmount = x?.DiscountedAmount ?? 0,
                    PaymentStatus = x?.PaymentStatus ?? 0,
                    PaidViaWallet = x?.PaidViaWallet ?? 0,
                    PayableAmount = x?.PayableAmount ?? x.Amount,


                }).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return getPaymentRecordVMs;
        }

        public async Task<Response> UpdateJobQuotation(JobQuotationDetailVM quotationDetailVM, string createdBy)
        {
            Response response = new Response();
            try
            {
                JobQuotation jobQuotation = new JobQuotation
                {
                    JobQuotationId = quotationDetailVM.JobQuotationId,
                    SubSkillId = quotationDetailVM.SubSkillId,
                    WorkTitle = quotationDetailVM.Title,
                    WorkDescription = quotationDetailVM.JobDescription,
                    WorkStartDate = quotationDetailVM.StartingDateTime,
                    WorkBudget = quotationDetailVM.Budget,
                    DesiredBids = quotationDetailVM.QuotesQuantity,
                    SelectiveTradesman = quotationDetailVM?.SelectiveTradesman ?? false,
                    ModifiedBy = createdBy,
                    ModifiedOn = DateTime.Now,
                    StatusId = quotationDetailVM.StatusId,
                    WorkStartTime = quotationDetailVM.WorkStartTime
                };
                Response jobQuotationUpdateResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobQuotation}", jobQuotation));

                if (jobQuotationUpdateResponse.Status == ResponseStatus.OK)
                {
                    JobAddress jobAddress = new JobAddress
                    {
                        CityId = quotationDetailVM.CityId,
                        //jobAddress.GpsCoordinates = OptimizedString(quotationDetailVM.LocationCoordinates != null ? quotationDetailVM.LocationCoordinates : "");
                        JobQuotationId = quotationDetailVM.JobQuotationId,
                        Area = quotationDetailVM.Area,
                        StreetAddress = quotationDetailVM.Address,
                        ModifiedBy = createdBy,
                        ModifiedOn = DateTime.Now
                    };
                    Response AddressResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                    if (AddressResponse.Status == ResponseStatus.OK)
                    {

                        if (quotationDetailVM?.Images?.Count > 0 && !string.IsNullOrEmpty(quotationDetailVM?.Images?.FirstOrDefault()?.ImageBase64))
                        {
                            for (int i = 0; i < quotationDetailVM.Images.Count; i++)
                            {
                                var checkFormatList = quotationDetailVM.Images[i].ImageBase64.Split(',');
                                if (checkFormatList[i] == "data:image/jpeg;base64")
                                {
                                    string convert = quotationDetailVM.Images[i].ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                                    quotationDetailVM.Images[i].ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                }
                            }

                        }

                        if (quotationDetailVM?.Images?.Count > 0)
                        {
                            await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.DeleteJobImages}?jobQuotationId={quotationDetailVM.JobQuotationId}", "");

                            List<JobImages> jobImages = new List<JobImages>();

                            jobImages = quotationDetailVM?.Images.Select(x => new JobImages
                            {
                                JobQuotationId = quotationDetailVM.JobQuotationId,
                                IsMain = x.IsMain,
                                BidImage = x.ImageContent,
                                FileName = x.FilePath,
                                CreatedOn = DateTime.Now,
                                CreatedBy = createdBy
                            }).ToList();

                            await ListOfJobImages(jobImages, quotationDetailVM.JobQuotationId);
                        }

                        if (quotationDetailVM?.VideoUpdated ?? false)
                        {
                            await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.DeleteJobQuotationVideo}?jobQuotationId={quotationDetailVM.JobQuotationId}", "");
                        }

                        if (!string.IsNullOrWhiteSpace(quotationDetailVM?.VideoFileName) && quotationDetailVM?.video?.Length > 0)
                        {

                            JobQuotationVideo jobQuotationVideo = new JobQuotationVideo
                            {
                                FileName = quotationDetailVM.VideoFileName,
                                Video = quotationDetailVM?.video,
                                JobQuotationId = quotationDetailVM.JobQuotationId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = createdBy
                            };
                            await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.AddVideo}", jobQuotationVideo);
                        }

                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Task Failed..";
                    }


                    response.Status = ResponseStatus.OK;
                    response.Message = "Job Updated Succesfully!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Task Failed..";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public async Task<Response> DeleteJobQuotation(long jobQuotationId)
        {
            Response response = new Response();
            try
            {

                Response jobQuotationUpdateResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.DeleteJobQuotation}?jobQuotationId={jobQuotationId}", ""));

                if (jobQuotationUpdateResponse.Status == ResponseStatus.OK)
                {

                    Response AddressResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.DeleteJobAddressByJobQuotationId}?jobQuotationId={jobQuotationId}", ""));

                    if (AddressResponse.Status == ResponseStatus.OK)
                    {
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.DeleteJobImages}?jobQuotationId={jobQuotationId}", "");
                        await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.DeleteJobQuotationVideo}?jobQuotationId={jobQuotationId}", "");
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Task Failed..";
                    }
                    response.Status = ResponseStatus.OK;
                    response.Message = "Job Quotation removed successfully!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Task Failed..";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);

            }
            return response;
        }

        public async Task<Response> AddAdViews(long supplierAdId, long customerId, string userId)
        {
            Response response = new Response();
            try
            {
                AdViews adViews = new AdViews
                {
                    SupplierAdsId = supplierAdId,
                    CustomerId = customerId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now
                };
                Response AddressResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.AddAdViews}", adViews));
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);

            }
            return response;
        }
        public async Task<Response> JobQuotations(QuotationVM quotationVM)
        {
            Response response = new Response();
            long jobId = 0;

            try
            {
                if (quotationVM.StatusId == (int)BidStatus.Pending)
                {
                    JobQuotation jobQuotation = new JobQuotation
                    {
                        JobQuotationId = quotationVM.JobQuotationId,
                        SkillId = quotationVM.CategoryId,
                        SubSkillId = quotationVM.SubCategoryId,
                        CustomerId = quotationVM.UserId,
                        WorkTitle = quotationVM.WorkTitle,
                        WorkDescription = quotationVM.JobDescription,
                        WorkStartDate = quotationVM.JobstartDateTime,
                        WorkBudget = quotationVM.Budget,
                        DesiredBids = quotationVM.NumberOfBids,
                        VisitCharges = quotationVM?.VisitCharges,
                        ServiceCharges = quotationVM?.ServiceCharges,
                        SelectiveTradesman = quotationVM.SelectiveTradesman,
                        CreatedBy = quotationVM.CreatedBy,
                        StatusId = quotationVM.StatusId,
                        CreatedOn = DateTime.Now,
                        WorkStartTime = quotationVM.WorkStartTime
                    };

                    if (jobQuotation.CustomerId > 0)
                    {
                        string jobQuotationId = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobQuotation}", jobQuotation);
                        bool jobQuotationIds = long.TryParse(jobQuotationId, out jobId);
                    }

                    JobAddress jobAddress = new JobAddress
                    {
                        CityId = quotationVM.CityId,
                        //GpsCoordinates = quotationVM?.LocationCoordinates,
                        GpsCoordinates = quotationVM?.LocationCoordinates + "",
                        JobQuotationId = jobId,
                        Area = quotationVM.Town,
                        StreetAddress = quotationVM.StreetAddress,
                        CreatedBy = quotationVM.CreatedBy,
                        AddressLine = quotationVM.AddressLine,
                        CreatedOn = DateTime.Now
                    };

                    Response AddressResponse = JsonConvert.DeserializeObject<Response>
                        (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                    if (quotationVM.FavoriteIds?.Count > 0)
                    {
                        List<FavoriteTradesman> favoriteTradesmen = new List<FavoriteTradesman>();

                        foreach (long item in quotationVM.FavoriteIds)
                        {
                            FavoriteTradesman favorite = new FavoriteTradesman
                            {
                                JobQuotationId = jobId,
                                CustomerFavoritesId = item,
                                CreatedBy = quotationVM.CreatedBy,
                                CreatedOn = DateTime.Now
                            };

                            favoriteTradesmen.Add(favorite);
                        }

                        await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobQuotationFavoriteTradesmen}", favoriteTradesmen);
                    }

                    if (AddressResponse.Status == ResponseStatus.OK)
                    {
                        response.Status = ResponseStatus.OK;
                        response.Message = "Job Posted Succesfully!";
                        response.ResultData = jobId;
                    }
                }
                else if (quotationVM.StatusId == (int)BidStatus.Active)
                {
                    Response jobStatusResponse = JsonConvert.DeserializeObject<Response>
                        (await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobQuotationStatus}?jobQuotationId={quotationVM.JobQuotationId}&statusId={quotationVM.StatusId}"));

                    if (jobStatusResponse.Status == ResponseStatus.OK)
                    {
                        List<JobImages> jobImages = new List<JobImages>();
                        foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                        {
                            JobImages jobimage = new JobImages
                            {
                                JobQuotationId = quotationVM.JobQuotationId,
                                IsMain = item.IsMain,
                                BidImage = item.ImageContent,
                                FileName = item.FilePath,
                                CreatedOn = DateTime.Now,
                                CreatedBy = quotationVM.CreatedBy
                            };

                            jobImages.Add(jobimage);
                        }
                        await ListOfJobImages(jobImages, quotationVM.JobQuotationId);

                        if (!string.IsNullOrWhiteSpace(quotationVM?.VideoVM?.FilePath))
                        {
                            await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.UpdateJobVideoStatus}?jobQuotationId={quotationVM.JobQuotationId}&isActive={quotationVM?.VideoVM.IsActive}");
                        }

                        Customer customer = JsonConvert.DeserializeObject<Customer>(
                            await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={quotationVM?.CreatedBy}")
                        );

                        City city = JsonConvert.DeserializeObject<City>(
                            await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={quotationVM.CityId}")
                        );

                        string cityName = "";

                        List<string> tradesmenList = JsonConvert.DeserializeObject<List<string>>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmenListBySkillIdAndCityId}?_skillId={quotationVM.CategoryId}&_cityName={cityName}")
                        );

                        // post notification here.
                        // calling notificatin api 
                        // request returen type is bool to check whether notification is posted successfully 
                        // CityName = cityName, SkillName = quotationVM?.SkillName

                        PostNotificationVM postNotificationVM = new PostNotificationVM()
                        {
                            SenderUserId = quotationVM?.CreatedBy,
                            Body = $"{customer.FirstName} {customer.LastName} posted a new Job {quotationVM?.WorkTitle}",
                            Title = NotificationTitles.NewJobPost,
                            TargetActivity = "Home",
                            To = $"'{quotationVM?.SkillName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                            TargetDatabase = TargetDatabase.Tradesman,
                            TradesmenList = tradesmenList,
                            IsRead = false

                        };

                        bool result = JsonConvert.DeserializeObject<bool>
                            (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                        );



                        response.Status = ResponseStatus.OK;
                        response.Message = "Job Posted Succesfully!";

                        try
                        {
                            PersonalDetailsVM customerDetail = await GetPersonalDetails(quotationVM.UserId);
                            if (response.Status == ResponseStatus.OK)
                            {
                                var postJobEmailVM = new PostJobEmailVM
                                {
                                    name = customerDetail.FirstName + " " + customerDetail.LastName,
                                    email_ = customerDetail.Email,
                                    jobTitle = quotationVM?.WorkTitle,
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                };

                                var responses = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendEmailJobPost}", postJobEmailVM)
                                    );


                                //var categoryName = JsonConvert.DeserializeObject<string>(
                                //        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={quotationVM.CategoryId}", "")
                                //);

                                List<TradesmanEmailVM> tradesmanEmails = JsonConvert.DeserializeObject<List<TradesmanEmailVM>>(
                                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SP_GetTradesmanWithSkillIdAndCityId}?skillId={quotationVM.CategoryId}&cityName={cityName}", "")
                                );

                                NewJobPosted newJobPosted = new NewJobPosted()
                                {
                                    UserName = $"{customer.FirstName} {customer.LastName}",
                                    TradeCategory = quotationVM?.SkillName,
                                    JobDescription = quotationVM?.JobDescription,
                                    Distance = $"{8} Kilometers away",
                                    JobCity = cityName,
                                    TradesmanEmail = tradesmanEmails?.Select(x => x?.EmailAddress).ToList(),
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        IsSend = false,
                                        Retries = 0,
                                    }
                                };

                                var emailId = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.NewJobPosted}", newJobPosted)
                                    );

                            }
                        }
                        catch (Exception ex)
                        {
                            Exc.AddErrorLog(ex);
                        }

                    }
                    else
                    {
                        response.Message = jobStatusResponse.Message;
                        response.Status = ResponseStatus.Error;
                    }

                }
                else if (quotationVM.StatusId == (int)BidStatus.Completed)
                {
                    JobQuotation jobQuotation = new JobQuotation
                    {
                        JobQuotationId = quotationVM.JobQuotationId,
                        SkillId = quotationVM.CategoryId,
                        SubSkillId = quotationVM.SubCategoryId,
                        CustomerId = quotationVM.UserId,
                        WorkTitle = quotationVM.WorkTitle,
                        WorkDescription = quotationVM.JobDescription,
                        WorkStartDate = quotationVM.JobstartDateTime,
                        WorkBudget = quotationVM.Budget,
                        VisitCharges = quotationVM.VisitCharges,
                        ServiceCharges = quotationVM.ServiceCharges,
                        DesiredBids = quotationVM.NumberOfBids,
                        SelectiveTradesman = quotationVM.SelectiveTradesman,
                        CreatedBy = quotationVM.CreatedBy,
                        StatusId = (int)BidStatus.Active,
                        CreatedOn = DateTime.Now,
                        WorkStartTime = quotationVM.WorkStartTime,
                        GpsCoordinates = quotationVM.GpsCoordinates
                    };

                    if (jobQuotation.CustomerId > 0)
                    {
                        string jobQuotationId = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobQuotation}", jobQuotation);
                        bool jobQuotationIds = long.TryParse(jobQuotationId, out jobId);
                    }

                    quotationVM.JobQuotationId = jobId;

                    JobAddress jobAddress = new JobAddress
                    {
                        CityId = quotationVM.CityId,
                        GpsCoordinates = quotationVM?.LocationCoordinates,
                        JobQuotationId = quotationVM.JobQuotationId,
                        Area = quotationVM.Town,
                        StreetAddress = quotationVM.StreetAddress,
                        CreatedBy = quotationVM.CreatedBy,
                        AddressLine = quotationVM.AddressLine,
                        CreatedOn = DateTime.Now,
                        TownId = quotationVM.TownId
                    };

                    Response AddressResponse = JsonConvert.DeserializeObject<Response>
                        (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                    if (quotationVM.FavoriteIds?.Count > 0)
                    {
                        List<FavoriteTradesman> favoriteTradesmen = new List<FavoriteTradesman>();

                        foreach (long item in quotationVM.FavoriteIds)
                        {
                            FavoriteTradesman favorite = new FavoriteTradesman
                            {
                                JobQuotationId = quotationVM.JobQuotationId,
                                CustomerFavoritesId = item,
                                CreatedBy = quotationVM.CreatedBy,
                                CreatedOn = DateTime.Now
                            };

                            favoriteTradesmen.Add(favorite);
                        }

                        await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobQuotationFavoriteTradesmen}", favoriteTradesmen);
                    }

                    if (AddressResponse.Status == ResponseStatus.OK)
                    {

                        List<JobImages> jobImages = new List<JobImages>();

                        foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                        {
                            JobImages jobimage = new JobImages
                            {
                                JobQuotationId = quotationVM.JobQuotationId,
                                IsMain = item.IsMain,
                                BidImage = item.ImageContent,
                                FileName = item.FilePath,
                                CreatedOn = DateTime.Now,
                                CreatedBy = quotationVM.CreatedBy
                            };

                            jobImages.Add(jobimage);
                        }
                        await ListOfJobImages(jobImages, quotationVM.JobQuotationId);

                        if (!string.IsNullOrWhiteSpace(quotationVM?.VideoVM?.FilePath))
                        {
                            await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.UpdateJobVideoStatus}?jobQuotationId={quotationVM.JobQuotationId}&isActive={quotationVM?.VideoVM.IsActive}");
                        }
                        if (jobId > 0)
                        {
                            var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                            List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                            SmsVM numList = new SmsVM();

                            authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                            numList.Message = $"New Job Posted Kindly Approve The Job '{quotationVM.WorkTitle}' with '{jobId}'";

                            await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);

                        }



                        response.Status = ResponseStatus.OK;
                        response.Message = "Job Posted Succesfully!";
                        //response.ResultData = jobId;


                        #region NotificationEmail

                        Customer customer = JsonConvert.DeserializeObject<Customer>(
                            await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={quotationVM?.CreatedBy}")
                        );

                        City city = JsonConvert.DeserializeObject<City>(
                            await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={quotationVM.CityId}")
                        );

                        string cityName = city.Name;

                        List<string> tradesmenList = JsonConvert.DeserializeObject<List<string>>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmenListBySkillIdAndCityId}?_skillId={quotationVM.CategoryId}&_cityName={cityName}")
                        );

                        // post notification here.
                        // calling notificatin api 
                        // request returen type is bool to check whether notification is posted successfully 
                        // CityName = cityName, SkillName = quotationVM?.SkillName

                        //PostNotificationVM postNotificationVM = new PostNotificationVM()
                        //{
                        //    SenderUserId = quotationVM?.CreatedBy,
                        //    Body = $"{customer.FirstName} {customer.LastName} posted a new Job {quotationVM?.WorkTitle}. After approval it will be shown in user liveleads as soon as possible.",
                        //    Title = NotificationTitles.NewJobPost,
                        //    TargetActivity = "Home",
                        //    To = $"'{quotationVM?.SkillName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                        //    TargetDatabase = TargetDatabase.Tradesman,
                        //    TradesmenList = tradesmenList
                        //};

                        //bool result = JsonConvert.DeserializeObject<bool>
                        //    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                        //);



                        //response.Status = ResponseStatus.OK;
                        //response.Message = "Job Posted Succesfully!";

                        try
                        {
                            PersonalDetailsVM customerDetail = await GetPersonalDetails(quotationVM.UserId);
                            if (response.Status == ResponseStatus.OK)
                            {
                                var postJobEmailVM = new PostJobEmailVM
                                {
                                    name = customerDetail.FirstName + " " + customerDetail.LastName,
                                    email_ = customerDetail.Email,
                                    jobTitle = quotationVM?.WorkTitle,
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                };

                                var responses = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendEmailJobPost}", postJobEmailVM)
                                    );


                                //var categoryName = JsonConvert.DeserializeObject<string>(
                                //        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={quotationVM.CategoryId}", "")
                                //);

                                List<TradesmanEmailVM> tradesmanEmails = JsonConvert.DeserializeObject<List<TradesmanEmailVM>>(
                                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SP_GetTradesmanWithSkillIdAndCityId}?skillId={quotationVM.CategoryId}&cityName={cityName}", "")
                                );

                                NewJobPosted newJobPosted = new NewJobPosted()
                                {
                                    UserName = $"{customer.FirstName} {customer.LastName}",
                                    TradeCategory = quotationVM?.SkillName,
                                    JobDescription = quotationVM?.JobDescription,
                                    Distance = $"{8} Kilometers away",
                                    JobCity = cityName,
                                    TradesmanEmail = tradesmanEmails?.Select(x => x?.EmailAddress).ToList(),
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        IsSend = false,
                                        Retries = 0,
                                    }
                                };

                                var emailId = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.NewJobPosted}", newJobPosted)
                                    );

                            }
                        }
                        catch (Exception ex)
                        {
                            Exc.AddErrorLog(ex);
                        }

                        #endregion

                    }
                    else
                    {
                        response.Message = AddressResponse.Message;
                        response.Status = ResponseStatus.Error;
                    }
                }
                else if (quotationVM.StatusId == (int)BidStatus.Urgent)
                {
                    JobQuotation jobQuotation = new JobQuotation
                    {
                        JobQuotationId = quotationVM.JobQuotationId,
                        SkillId = quotationVM.CategoryId,
                        SubSkillId = quotationVM.SubCategoryId,
                        CustomerId = quotationVM.UserId,
                        WorkTitle = quotationVM.WorkTitle,
                        WorkDescription = quotationVM.JobDescription,
                        WorkStartDate = quotationVM.JobstartDateTime,
                        WorkBudget = quotationVM.Budget,
                        DesiredBids = quotationVM.NumberOfBids,
                        SelectiveTradesman = quotationVM.SelectiveTradesman,
                        CreatedBy = quotationVM.CreatedBy,
                        StatusId = (int)BidStatus.Active,
                        CreatedOn = DateTime.Now,
                        AuthorizeJob = false,
                        WorkStartTime = quotationVM.WorkStartTime
                    };

                    if (jobQuotation.CustomerId > 0)
                    {
                        string jobQuotationId = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobQuotation}", jobQuotation);
                        bool jobQuotationIds = long.TryParse(jobQuotationId, out jobId);
                    }

                    quotationVM.JobQuotationId = jobId;

                    JobAddress jobAddress = new JobAddress
                    {
                        CityId = quotationVM.CityId,
                        GpsCoordinates = quotationVM?.LocationCoordinates,
                        JobQuotationId = quotationVM.JobQuotationId,
                        Area = quotationVM.Town,
                        StreetAddress = quotationVM.StreetAddress,
                        CreatedBy = quotationVM.CreatedBy,
                        AddressLine = quotationVM.AddressLine,
                        CreatedOn = DateTime.Now
                    };

                    Response AddressResponse = JsonConvert.DeserializeObject<Response>
                        (await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                    if (quotationVM.FavoriteIds?.Count > 0)
                    {
                        List<FavoriteTradesman> favoriteTradesmen = new List<FavoriteTradesman>();

                        foreach (long item in quotationVM.FavoriteIds)
                        {
                            FavoriteTradesman favorite = new FavoriteTradesman
                            {
                                JobQuotationId = quotationVM.JobQuotationId,
                                CustomerFavoritesId = item,
                                CreatedBy = quotationVM.CreatedBy,
                                CreatedOn = DateTime.Now
                            };

                            favoriteTradesmen.Add(favorite);
                        }

                        await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobQuotationFavoriteTradesmen}", favoriteTradesmen);
                    }

                    if (AddressResponse.Status == ResponseStatus.OK)
                    {

                        List<JobImages> jobImages = new List<JobImages>();
                        foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                        {
                            if (!string.IsNullOrWhiteSpace(item.ImageBase64) || jobQuotation.CustomerId > 0)
                            {
                                var checkFormatList = item.ImageBase64.Split(',');
                                //For Angular Web UserProfile Image
                                if (checkFormatList[0] == "data:image/png;base64")
                                {
                                    string convert = item.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                                    item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                }
                                else if (checkFormatList[0] == "data:image/jpg;base64")
                                {
                                    string convert = item.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                                    item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                }
                                else if (checkFormatList[0] == "data:image/jpeg;base64")
                                {
                                    string convert = item.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                                    item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                                }

                                JobImages jobimage = new JobImages
                                {
                                    JobQuotationId = jobId,
                                    IsMain = item.IsMain,
                                    BidImage = item.ImageContent,
                                    FileName = $"img-{DateTime.Now}",
                                    CreatedOn = DateTime.Now,
                                    CreatedBy = quotationVM.CreatedBy
                                };

                                jobImages.Add(jobimage);
                            }
                        }

                        //foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                        //{
                        //    JobImages jobimage = new JobImages
                        //    {
                        //        JobQuotationId = quotationVM.JobQuotationId,
                        //        IsMain = item.IsMain,
                        //        BidImage = item.ImageContent,
                        //        FileName = item.FilePath,
                        //        CreatedOn = DateTime.Now,
                        //        CreatedBy = quotationVM.CreatedBy
                        //    };

                        //    jobImages.Add(jobimage);
                        //}
                        await ListOfJobImages(jobImages, quotationVM.JobQuotationId);

                        if (!string.IsNullOrWhiteSpace(quotationVM?.VideoVM?.FilePath))
                        {
                            await httpClient.GetAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.UpdateJobVideoStatus}?jobQuotationId={quotationVM.JobQuotationId}&isActive={quotationVM?.VideoVM.IsActive}");
                        }

                        var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                        List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                        SmsVM numList = new SmsVM();

                        authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                        numList.Message = $"New Job Posted Kindly Approve The Job '{quotationVM.WorkTitle}' with '{jobId}'";

                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);


                        response.Status = ResponseStatus.OK;
                        response.Message = "Job Posted Succesfully!";
                        //response.ResultData = jobId;


                        #region NotificationEmail

                        Customer customer = JsonConvert.DeserializeObject<Customer>(
                            await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={quotationVM?.CreatedBy}")
                        );

                        City city = JsonConvert.DeserializeObject<City>(
                            await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={quotationVM.CityId}")
                        );

                        string cityName = city.Name;

                        List<string> tradesmenList = JsonConvert.DeserializeObject<List<string>>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmenListBySkillIdAndCityId}?_skillId={quotationVM.CategoryId}&_cityName={cityName}")
                        );

                        // post notification here.
                        // calling notificatin api 
                        // request returen type is bool to check whether notification is posted successfully 
                        // CityName = cityName, SkillName = quotationVM?.SkillName

                        //PostNotificationVM postNotificationVM = new PostNotificationVM()
                        //{
                        //    SenderUserId = quotationVM?.CreatedBy,
                        //    Body = $"{customer.FirstName} {customer.LastName} posted a new Job {quotationVM?.WorkTitle}. After approval it will be shown in user liveleads as soon as possible.",
                        //    Title = NotificationTitles.NewJobPost,
                        //    TargetActivity = "Home",
                        //    To = $"'{quotationVM?.SkillName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                        //    TargetDatabase = TargetDatabase.Tradesman,
                        //    TradesmenList = tradesmenList
                        //};

                        //bool result = JsonConvert.DeserializeObject<bool>
                        //    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                        //);



                        //response.Status = ResponseStatus.OK;
                        //response.Message = "Job Posted Succesfully!";

                        try
                        {
                            PersonalDetailsVM customerDetail = await GetPersonalDetails(quotationVM.UserId);
                            if (response.Status == ResponseStatus.OK)
                            {
                                var postJobEmailVM = new PostJobEmailVM
                                {
                                    name = customerDetail.FirstName + " " + customerDetail.LastName,
                                    email_ = customerDetail.Email,
                                    jobTitle = quotationVM?.WorkTitle,
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                };

                                var responses = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendEmailJobPost}", postJobEmailVM)
                                    );


                                //var categoryName = JsonConvert.DeserializeObject<string>(
                                //        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={quotationVM.CategoryId}", "")
                                //);

                                List<TradesmanEmailVM> tradesmanEmails = JsonConvert.DeserializeObject<List<TradesmanEmailVM>>(
                                    await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SP_GetTradesmanWithSkillIdAndCityId}?skillId={quotationVM.CategoryId}&cityName={cityName}", "")
                                );

                                NewJobPosted newJobPosted = new NewJobPosted()
                                {
                                    UserName = $"{customer.FirstName} {customer.LastName}",
                                    TradeCategory = quotationVM?.SkillName,
                                    JobDescription = quotationVM?.JobDescription,
                                    Distance = $"{8} Kilometers away",
                                    JobCity = cityName,
                                    TradesmanEmail = tradesmanEmails?.Select(x => x?.EmailAddress).ToList(),
                                    Email = new Email
                                    {
                                        CreatedBy = customerDetail.UserId,
                                        IsSend = false,
                                        Retries = 0,
                                    }
                                };

                                var emailId = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.NewJobPosted}", newJobPosted)
                                    );

                            }
                        }
                        catch (Exception ex)
                        {
                            Exc.AddErrorLog(ex);
                        }

                        #endregion

                    }
                    else
                    {
                        response.Message = AddressResponse.Message;
                        response.Status = ResponseStatus.Error;
                    }
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message + ex.InnerException;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> PostJobQuotationWeb(QuotationVM quotationVM)
        {
            Response response = new Response();

            try
            {
                long jobId = 0;
                JobQuotation jobQuotation = new JobQuotation
                {
                    SkillId = quotationVM.CategoryId,
                    SubSkillId = quotationVM.SubCategoryId,
                    CustomerId = quotationVM.UserId,
                    WorkTitle = quotationVM.WorkTitle,
                    WorkDescription = quotationVM.JobDescription,
                    WorkStartDate = quotationVM.JobstartDateTime,
                    WorkBudget = quotationVM.Budget,
                    DesiredBids = quotationVM.NumberOfBids,
                    SelectiveTradesman = quotationVM.SelectiveTradesman,
                    CreatedBy = quotationVM.CreatedBy,
                    StatusId = (int)BidStatus.Active,
                    CreatedOn = DateTime.Now
                };
                if (jobQuotation.CustomerId > 0)
                {
                    string jobQuotationId = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobQuotation}", jobQuotation);
                    bool jobQuotationIds = long.TryParse(jobQuotationId, out jobId);
                }


                if (quotationVM.FavoriteIds?.Count > 0)
                {
                    List<FavoriteTradesman> favoriteTradesmen = new List<FavoriteTradesman>();

                    foreach (long item in quotationVM.FavoriteIds)
                    {
                        FavoriteTradesman favorite = new FavoriteTradesman
                        {
                            JobQuotationId = jobId,
                            CustomerFavoritesId = item,
                            CreatedBy = quotationVM.CreatedBy,
                            CreatedOn = DateTime.Now
                        };

                        favoriteTradesmen.Add(favorite);
                    }
                    if (jobId > 0)
                    {
                        await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobQuotationFavoriteTradesmen}", favoriteTradesmen);
                    }
                }

                List<JobImages> jobImages = new List<JobImages>();
                foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                {
                    JobImages jobimage = new JobImages
                    {
                        JobQuotationId = jobId,
                        IsMain = item.IsMain,
                        BidImage = item.ImageContent,
                        FileName = item.FilePath,
                        CreatedOn = DateTime.Now,
                        CreatedBy = quotationVM.CreatedBy
                    };

                    jobImages.Add(jobimage);
                }
                await ListOfJobImages(jobImages, jobId);


                JobAddress jobAddress = new JobAddress
                {
                    CityId = quotationVM.CityId,
                    GpsCoordinates = quotationVM?.LocationCoordinates,
                    JobQuotationId = jobId,
                    Area = quotationVM.Town,
                    StreetAddress = quotationVM.StreetAddress,
                    CreatedBy = quotationVM.CreatedBy,
                    AddressLine = quotationVM.AddressLine,
                    CreatedOn = DateTime.Now
                };
                Response AddressResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                if (!string.IsNullOrWhiteSpace(quotationVM?.VideoVM?.FilePath) && quotationVM?.VideoVM?.VideoContent?.Length > 0)
                {
                    JobQuotationVideo jobQuotationVideo = new JobQuotationVideo
                    {
                        FileName = quotationVM.VideoVM?.FilePath,
                        Video = quotationVM.VideoVM?.VideoContent,
                        JobQuotationId = jobId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = quotationVM?.CreatedBy
                    };
                    var videoJson = await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.AddVideo}", jobQuotationVideo);
                }

                Customer customer = JsonConvert.DeserializeObject<Customer>(
                    await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={quotationVM.CreatedBy}")
                );

                City city = JsonConvert.DeserializeObject<City>(
                    await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={quotationVM.CityId}")
                );

                string cityName = city.Name;

                var categoryName = JsonConvert.DeserializeObject<string>(
                        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={quotationVM.CategoryId}", "")
                );

                // post notification here.
                // calling notificatin api 
                // request returen type is bool to check whether notification is posted successfully

                //PostNotificationVM postNotificationVM = new PostNotificationVM()
                //{
                //    SenderUserId = quotationVM?.CreatedBy,
                //    Body = $"{customer?.FirstName} {customer?.LastName} posted a new Job {jobQuotation?.WorkTitle}",
                //    Title = NotificationTitles.NewJobPost,
                //    TargetActivity = "Home",
                //    To = $"'{categoryName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                //    TargetDatabase = TargetDatabase.Tradesman
                //};

                //bool result = JsonConvert.DeserializeObject<bool>
                //    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                //);

                var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                SmsVM numList = new SmsVM();

                authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                numList.Message = $"New Job Posted Kindly Approve The Job '{quotationVM.WorkTitle}' by '{quotationVM.JobQuotationId}'";

                await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);

                response.Status = ResponseStatus.OK;
                response.Message = "Job Posted Succesfully!";
                response.ResultData = jobId;

                #region Send Email
                try
                {
                    PersonalDetailsVM customerDetail = await GetPersonalDetails(quotationVM.UserId);
                    if (response.Status == ResponseStatus.OK)
                    {
                        var postJobEmailVM = new PostJobEmailVM
                        {
                            name = customerDetail.FirstName + " " + customerDetail.LastName,
                            email_ = customerDetail.Email,
                            jobTitle = quotationVM.WorkTitle,
                            Email = new Email
                            {
                                CreatedBy = customerDetail.UserId,
                                Retries = 0,
                                IsSend = false
                            }
                        };

                        var responses = JsonConvert.DeserializeObject<long>(
                                await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendEmailJobPost}", postJobEmailVM)
                            );


                        //var categoryName = JsonConvert.DeserializeObject<string>(
                        //        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={jobQuotation.SkillId}", "")
                        //);

                        List<TradesmanEmailVM> tradesmanEmails = JsonConvert.DeserializeObject<List<TradesmanEmailVM>>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SP_GetTradesmanWithSkillIdAndCityId}?skillId={quotationVM.CategoryId}&cityName={cityName}", "")
                        );
                        //Measure Distance
                        #region
                        //var gpsCoordinatesTradesman = tradesmanEmails?.Select(x => x.GpsCoordinates).ToList();
                        //List<string> dist = new List<string>();
                        //foreach (var item in gpsCoordinatesTradesman)
                        //{
                        //    var latlongEndPoint = item.Split(',');
                        //    var latlongStartPoint = jobAddress.GpsCoordinates.Split(",");
                        //    DistanceTo(Convert.ToDouble(latlongStartPoint[0]), Convert.ToDouble(latlongStartPoint[1]), Convert.ToDouble(latlongEndPoint[0]), Convert.ToDouble(latlongEndPoint[1]));
                        //}
                        #endregion

                        NewJobPosted newJobPosted = new NewJobPosted()
                        {
                            UserName = $"{customer.FirstName} {customer.LastName}",
                            TradeCategory = categoryName,
                            JobDescription = quotationVM.JobDescription,
                            Distance = $"{8} Kilometers away",
                            JobCity = cityName,
                            TradesmanEmail = tradesmanEmails?.Select(x => x?.EmailAddress).ToList(),
                            Email = new Email
                            {
                                CreatedBy = customerDetail.UserId,
                                IsSend = false,
                                Retries = 0,
                            }
                        };

                        var emailId = JsonConvert.DeserializeObject<long>(
                                await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.NewJobPosted}", newJobPosted)
                            );
                    }
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);

                }
                #endregion
            }
            catch (Exception ex)
            {
                response.Message = ex.Message + ex.InnerException;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> UpdateJobQuotationStatus(long jobQuotationId, int statusId)
        {
            Response response = new Response();
            try
            {
                Response jobQuotationUpdateResponse = JsonConvert.DeserializeObject<Response>(await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.UpdateJobQuotationStatus}?jobQuotationId={jobQuotationId}&statusId={statusId}", ""));
                if (jobQuotationUpdateResponse.Status == ResponseStatus.OK)
                {

                    response.Status = ResponseStatus.OK;
                    response.Message = "Job Quotation removed successfully!";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Task Failed..";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }
            return response;
        }
        public async Task AddVideo(UserViewModels.VideoVM videoVM, string userId)
        {
            try
            {
                JobQuotationVideo jobQuotationVideo = new JobQuotationVideo
                {
                    FileName = videoVM?.FilePath,
                    Video = videoVM?.VideoContent,
                    JobQuotationId = videoVM?.VideoId ?? 0,
                    CreatedOn = DateTime.Now,
                    CreatedBy = userId
                };

                await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.AddVideo}", jobQuotationVideo);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        private Task<string> RenderViewToStringAsync(string v, QuotationVM quotationVM)
        {
            throw new NotImplementedException();
        }

        public async Task ListOfJobImages(List<JobImages> jobImages, long jobId)
        {
            try
            {
                if (jobId > 0)
                {
                    if (jobImages.Count < 1)
                    {

                        JobImages jobImages1 = new JobImages()
                        {
                            JobQuotationId = jobId
                        };
                        jobImages.Add(jobImages1);

                    }


                    string res = await httpClient.PostAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.SaveJobImages}", jobImages);
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task<UserProfileVM> GetCustomerByUserId(string userId)
        {
            UserProfileVM userVM = new UserProfileVM();

            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={userId}")
                    );

                if (customer != null)
                {
                    CustomerProfileImage profileImage = JsonConvert.DeserializeObject<CustomerProfileImage>(
                        await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customer.CustomerId}")
                    );

                    userVM = new UserProfileVM()
                    {
                        UserId = userId,
                        EntityId = customer.CustomerId,
                        UserName = $"{customer.FirstName} {customer.LastName}",
                        //City = customer?.City,
                        CustomerId = customer.CustomerId,
                        PublicId = customer.PublicId,
                        ProfileImage = profileImage?.ProfileImage
                    };
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return userVM;
        }

        public async Task<SupplierViewModels.CallInfoVM> GetCustomerById(long supplierId, long customerId, bool todaysRecordOnly)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>
                       (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerById}?customerId={customerId}", ""));

                List<SupplierCallLog> callLogs = JsonConvert.DeserializeObject<List<SupplierCallLog>>
                    (await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetSuppliersCallLogs}?supplierId={supplierId}&customerId={customerId}&todaysRecordOnly={todaysRecordOnly}", ""));

                CustomerProfileImage customerImage = JsonConvert.DeserializeObject<CustomerProfileImage>
                    (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customerId}", ""));

                List<CallType> allCallTypes = JsonConvert.DeserializeObject<List<CallType>>
                    (await httpClient.GetAsync($"{_apiConfig.CallApiUrl}{ApiRoutes.Call.GetAllCallType}", ""));

                SupplierViewModels.CallInfoVM callInfoVM = new SupplierViewModels.CallInfoVM()
                {
                    CustomerID = customer.CustomerId,
                    CustomerImage = customerImage?.ProfileImage,
                    CustomerName = customer.FirstName + " " + customer.LastName,
                    //Lat = customer.Lat.HasValue ? customer.Lat.Value : 0,
                    //Long = customer.Long.HasValue ? customer.Long.Value : 0,
                    CallLogs = callLogs.Select(c => new SupplierViewModels.CallLogVM
                    {
                        CallDuration = c.Duration,
                        CallType = allCallTypes.Where(x => x.CallTypeId == c.CallType).FirstOrDefault().Name
                    }).ToList()
                };
                return callInfoVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierViewModels.CallInfoVM();
            }
        }

        public async Task<PersonalDetailsVM> GetPersonalDetails(long customerId)
        {
            try
            {
                if (customerId > 0)
                {
                    Customer customer = JsonConvert.DeserializeObject<Customer>
                       (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetPersonalDetails}?customerId={customerId}", ""));

                    Response identityUser = JsonConvert.DeserializeObject<Response>
                       (await httpClient.GetAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.GetUserByUserId}?userId={customer.UserId}", ""));

                    UserRegisterVM userRegister = JsonConvert.DeserializeObject<UserRegisterVM>(identityUser?.ResultData?.ToString());


                    CustomerProfileImage customerProfileImage = JsonConvert.DeserializeObject<CustomerProfileImage>
                        (await httpClient.GetAsync($"{_apiConfig.ImageApiUrl}{ApiRoutes.Image.GetImageByCustomerId}?customerId={customerId}", ""));

                    return new PersonalDetailsVM()
                    {
                        EntityId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Cnic = customer.Cnic,
                        Email = customer.EmailAddress,
                        IsEmailConfirmed = userRegister.IsEmailConfirmed,
                        IsNumberConfirmed = userRegister.IsNumberConfirmed,
                        Gender = customer.Gender.Value,
                        DateOfBirth = customer?.Dob,
                        MobileNumber = customer.MobileNumber,
                        UserId = customer.UserId,
                        ProfileImage = customerProfileImage?.ProfileImage != null ? customerProfileImage.ProfileImage : new byte[0],
                        CityId = customer.CityId.HasValue ? customer.CityId.Value : 0,
                        Town = customer.State

                    };
                }
                else
                {
                    return new PersonalDetailsVM();
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new PersonalDetailsVM();
            }
        }

        public async Task<bool> UpdatePersonalDetails(PersonalDetailsVM personalDetailVM)
        {
            try
            {
                Customer customer = new Customer()
                {
                    CustomerId = personalDetailVM.EntityId,
                    FirstName = personalDetailVM.FirstName,
                    LastName = personalDetailVM.LastName,
                    Dob = personalDetailVM.DateOfBirth,
                    ModifiedOn = DateTime.Now
                };

                return JsonConvert.DeserializeObject<bool>
                    (await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.UpdatePersonalDetails}", customer));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        public static double DistanceTo(double latitudeofStartPoint, double longitudeofStartPoint, double latitudeofEndPoint, double longitudeofEndPoint, char MeasuredUnit = 'K')
        {
            double rlat1 = Math.PI * latitudeofStartPoint / 180;
            double rlat2 = Math.PI * latitudeofEndPoint / 180;
            double theta = longitudeofStartPoint - longitudeofEndPoint;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (MeasuredUnit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            string str = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAllCustomers}", "");
            return JsonConvert.DeserializeObject<List<Customer>>(str);
        }

        public async Task<Response> JobQuotationsWeb(QuotationVM quotationVM)
        {
            Response response = new Response();

            try
            {
                long jobId = 0;
                TimeSpan startTime = TimeSpan.Parse(quotationVM?.JobStartTime);
                
                JobQuotation jobQuotation = new JobQuotation
                {
                    SkillId = quotationVM.CategoryId,
                    JobQuotationId = quotationVM.JobQuotationId,
                    SubSkillId = quotationVM.SubCategoryId,
                    CustomerId = quotationVM.UserId,
                    WorkTitle = quotationVM.WorkTitle,
                    WorkDescription = quotationVM.JobDescription,
                    WorkStartDate = quotationVM.JobstartDateTime,
                    WorkStartTime = startTime,
                    WorkBudget = quotationVM.Budget,
                    DesiredBids = quotationVM.NumberOfBids,
                    SelectiveTradesman = quotationVM.SelectiveTradesman,
                    AuthorizeJob = false,
                    CreatedBy = quotationVM.CreatedBy,
                    StatusId = quotationVM.StatusId,
                    CreatedOn = DateTime.Now,
                    VisitCharges = quotationVM.VisitCharges,
                    ServiceCharges = quotationVM.ServiceCharges,
                };
                if (jobQuotation.CustomerId > 0)
                {
                    string jobQuotationId = await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobQuotation}", jobQuotation);
                    bool jobQuotationIds = long.TryParse(jobQuotationId, out jobId);
                }

                if ((quotationVM.ImageVMs != null || quotationVM?.ImageVMs?.Count > 0) && jobQuotation.CustomerId > 0)
                {
                    List<JobImages> jobImages = new List<JobImages>();
                    foreach (ImageVM item in quotationVM?.ImageVMs ?? new List<ImageVM>())
                    {
                        if (!string.IsNullOrWhiteSpace(item.ImageBase64) || jobQuotation.CustomerId > 0)
                        {
                            var checkFormatList = item.ImageBase64.Split(',');
                            //For Angular Web UserProfile Image
                            if (checkFormatList[0] == "data:image/png;base64")
                            {
                                string convert = item.ImageBase64.Replace("data:image/png;base64,", String.Empty);
                                item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            }
                            else if (checkFormatList[0] == "data:image/jpg;base64")
                            {
                                string convert = item.ImageBase64.Replace("data:image/jpg;base64,", String.Empty);
                                item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            }
                            else if (checkFormatList[0] == "data:image/jpeg;base64")
                            {
                                string convert = item.ImageBase64.Replace("data:image/jpeg;base64,", String.Empty);
                                item.ImageContent = !string.IsNullOrEmpty(convert) ? Convert.FromBase64String(convert) : new byte[0];
                            }

                            JobImages jobimage = new JobImages
                            {
                                JobQuotationId = jobId,
                                IsMain = item.IsMain,
                                BidImage = item.ImageContent,
                                FileName = $"img-{DateTime.Now}",
                                CreatedOn = DateTime.Now,
                                CreatedBy = quotationVM.CreatedBy
                            };

                            jobImages.Add(jobimage);
                        }
                    }
                    await ListOfJobImages(jobImages, jobId);
                }
                if (jobId > 0 && quotationVM.StatusId != (int)BidStatus.Pending)
                {
                    if (quotationVM.FavoriteIds?.Count > 0)
                    {
                        List<FavoriteTradesman> favoriteTradesmen = new List<FavoriteTradesman>();

                        foreach (long item in quotationVM.FavoriteIds)
                        {
                            FavoriteTradesman favorite = new FavoriteTradesman
                            {
                                JobQuotationId = jobId,
                                CustomerFavoritesId = item,
                                CreatedBy = quotationVM.CreatedBy,
                                CreatedOn = DateTime.Now
                            };

                            favoriteTradesmen.Add(favorite);
                        }
                        if (jobId > 0)
                        {
                            await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.AddJobQuotationFavoriteTradesmen}", favoriteTradesmen);
                        }
                    }

                    JobAddress jobAddress = new JobAddress
                    {
                        CityId = quotationVM.CityId,
                        GpsCoordinates = quotationVM?.LocationCoordinates,
                        JobQuotationId = jobId,
                        TownId = quotationVM.TownId,
                        Area = quotationVM.Town,
                        StreetAddress = quotationVM.StreetAddress,
                        AddressLine = quotationVM.AddressLine,
                        CreatedBy = quotationVM.CreatedBy,
                        CreatedOn = DateTime.Now,

                    };
                    Response AddressResponse = JsonConvert.DeserializeObject<Response>(await httpClient.PostAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.SaveJobAddress}", jobAddress));

                    if (!string.IsNullOrWhiteSpace(quotationVM?.VideoVM?.FilePath) && quotationVM?.VideoVM?.VideoContent?.Length > 0)
                    {
                        JobQuotationVideo jobQuotationVideo = new JobQuotationVideo
                        {
                            FileName = quotationVM.VideoVM?.FilePath,
                            Video = quotationVM.VideoVM?.VideoContent,
                            JobQuotationId = jobId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = quotationVM?.CreatedBy
                        };
                        var videoJson = await httpClient.PostAsync($"{_apiConfig.VideoApiUrl}{ApiRoutes.Video.AddVideo}", jobQuotationVideo);
                    }

                    Customer customer = JsonConvert.DeserializeObject<Customer>(
                        await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByUserId}?userId={quotationVM.CreatedBy}")
                    );

                    City city = JsonConvert.DeserializeObject<City>(
                        await httpClient.GetAsync($"{_apiConfig.UserManagementApiUrl}{ApiRoutes.UserManagement.GetCityById}?cityId={quotationVM.CityId}")
                    );

                    string cityName = city.Name;

                    var categoryName = JsonConvert.DeserializeObject<string>(
                            await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={quotationVM.CategoryId}", "")
                    );

                    // post notification here.
                    // calling notificatin api 
                    // request returen type is bool to check whether notification is posted successfully

                    //PostNotificationVM postNotificationVM = new PostNotificationVM()
                    //{
                    //    SenderUserId = quotationVM?.CreatedBy,
                    //    Body = $"{customer?.FirstName} {customer?.LastName} posted a new Job {jobQuotation?.WorkTitle}",
                    //    Title = NotificationTitles.NewJobPost,
                    //    TargetActivity = "Home",
                    //    To = $"'{categoryName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{quotationVM.CityName}'{NotificationTopics.InTopics}",
                    //    TargetDatabase = TargetDatabase.Tradesman,
                    //    IsRead = false
                    //};

                    //bool result = JsonConvert.DeserializeObject<bool>
                    //    (await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.PostTopicNotification}", postNotificationVM)
                    //);

                    response.Status = ResponseStatus.OK;
                    response.Message = "Job Posted Succesfully!";
                    response.ResultData = jobId;
                    PostNotificationVM postNotificationVM = new PostNotificationVM()
                    {
                        SenderUserId = quotationVM?.CreatedBy,
                        Body = $"{customer?.FirstName} {customer?.LastName} posted a new Job {jobQuotation?.WorkTitle}",
                        Title = NotificationTitles.NewJobPost,
                        TargetActivity = "Live Leads",
                        To = $"'{categoryName}'{NotificationTopics.InTopics}{NotificationTopics.AndCondition}'{cityName}'{NotificationTopics.InTopics}",
                        TargetDatabase = TargetDatabase.Tradesman,
                        isFromWeb = true,
                    };
                    //foreach (var item in quotationVM.fireBaseIds)
                    //{
                    //    postNotificationVM.TragetUserId = item.UserId;
                    //    await httpClient.PostAsync($"{_apiConfig.NotificationApiUrl}{ApiRoutes.Notification.SaveNotificationDataWeb}", postNotificationVM);
                    //}
                    #region Send Email
                    try
                    {
                        //PersonalDetailsVM customerDetail = await GetPersonalDetails(quotationVM.UserId);
                        if (response.Status == ResponseStatus.OK)
                        {
                            if (!string.IsNullOrWhiteSpace(customer.EmailAddress))
                            {
                                var postJobEmailVM = new PostJobEmailVM
                                {
                                    name = customer.FirstName + " " + customer.LastName,
                                    email_ = customer.EmailAddress,
                                    jobTitle = quotationVM.WorkTitle,
                                    Email = new Email
                                    {
                                        CreatedBy = customer.UserId,
                                        Retries = 0,
                                        IsSend = false
                                    }
                                };
                                var responses = JsonConvert.DeserializeObject<long>(
                                        await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendEmailJobPost}", postJobEmailVM)
                                    );
                            }
                        }

                        //var categoryName = JsonConvert.DeserializeObject<string>(
                        //        await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.GetTradesmanSkillName}?skillId={jobQuotation.SkillId}", "")
                        //);

                        //Measure Distance
                        #region
                        //var gpsCoordinatesTradesman = tradesmanEmails?.Select(x => x.GpsCoordinates).ToList();
                        //List<string> dist = new List<string>();
                        //foreach (var item in gpsCoordinatesTradesman)
                        //{
                        //    var latlongEndPoint = item.Split(',');
                        //    var latlongStartPoint = jobAddress.GpsCoordinates.Split(",");
                        //    DistanceTo(Convert.ToDouble(latlongStartPoint[0]), Convert.ToDouble(latlongStartPoint[1]), Convert.ToDouble(latlongEndPoint[0]), Convert.ToDouble(latlongEndPoint[1]));
                        //}
                        #endregion
                        #region send email to tradesman for new job post tradesman 
                        List<TradesmanEmailVM> tradesmanEmails = JsonConvert.DeserializeObject<List<TradesmanEmailVM>>(
                                await httpClient.GetAsync($"{_apiConfig.TradesmanApiUrl}{ApiRoutes.Tradesman.SP_GetTradesmanWithSkillIdAndCityId}?skillId={quotationVM.CategoryId}&cityName={quotationVM.CityName}", "")
                            );

                        List<string> tradesmanEmailsAddressList = new List<string>();
                        foreach (var item in tradesmanEmails)
                        {
                            if (!string.IsNullOrWhiteSpace(item.EmailAddress))
                            {
                                tradesmanEmailsAddressList.Add(item.EmailAddress);
                            }
                        }
                        if (tradesmanEmailsAddressList?.Count > 0)
                        {
                            NewJobPosted newJobPosted = new NewJobPosted()
                            {
                                UserName = $"{customer.FirstName} {customer.LastName}",
                                TradeCategory = categoryName,
                                JobDescription = quotationVM.JobDescription,
                                Distance = $"{8} Kilometers away",
                                JobCity = quotationVM.CityName,
                                TradesmanEmail = tradesmanEmailsAddressList,
                                Email = new Email
                                {
                                    CreatedBy = customer.UserId,
                                    IsSend = false,
                                    Retries = 0,
                                }
                            };

                            var emailId = JsonConvert.DeserializeObject<long>(
                                    await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.NewJobPosted}", newJobPosted)
                                );
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        Exc.AddErrorLog(ex);

                    }
                    #endregion
                }

                var jobsJson = await httpClient.GetAsync($"{_apiConfig.JobApiUrl}{ApiRoutes.Job.JobAuthorizerList}");
                List<JobAuthorizerVM> authoroties = JsonConvert.DeserializeObject<List<JobAuthorizerVM>>(jobsJson);

                SmsVM numList = new SmsVM();
                authoroties.ForEach(x => numList.MobileNumberList.Add(x.phoneNumber));
                numList.Message = $"New Job Posted Kindly Approve The Job '{quotationVM.WorkTitle}' with '{jobId}'";
                await httpClient.PostAsync($"{_apiConfig.CommunicationApiUrl}{ApiRoutes.Communication.SendSms}", numList);
                response.Status = ResponseStatus.OK;
                response.Message = "Job Posted Succesfully!";
                response.ResultData = jobId;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message + ex.InnerException;
                response.Status = ResponseStatus.Error;
                Exc.AddErrorLog(ex);
            }

            return response;
        }
        public async Task<Response> Login(LoginVM model)
        {
            System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();

            var response = new Response();

            try
            {
                //var disco = await DiscoveryClient.GetAsync(_apiConfig.IdentityServerApiUrl); only supports .net core 2.2

                var disco = await _httpClient.GetDiscoveryDocumentAsync(_apiConfig.IdentityServerApiUrl); // supports .net core 5.0

                if (disco.IsError)
                {
                    Exc.AddErrorLog(new Exception(disco.Error));
                    response.Message = "LoginFailed";
                    response.ResultData = disco.IsError;
                }
                else
                {
                    //var extra = new Dictionary<string, string> { { "firebaseClientId", model.FirebaseClientId }, { "role", model.Role } };
                    //var tokenClient = new TokenClient(disco.TokenEndpoint, clientCred.ClientId, clientCred.Secret, null, AuthenticationStyle.PostValues); only supports .net core 2.2
                    //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync($"{model.EmailOrPhoneNumber}", model.Password, "api1", extra);

                    var tokenResponse = await HttpClientTokenRequestExtensions.RequestPasswordTokenAsync(_httpClient, new PasswordTokenRequest
                    {
                        Scope = "api1",
                        Address = disco.TokenEndpoint,
                        ClientSecret = clientCred.Secret,
                        ClientId = clientCred.ClientId,
                        UserName = model.EmailOrPhoneNumber,
                        Password = model.Password,
                        Parameters =
                        {
                            { "firebaseClientId", model.FirebaseClientId },
                            { "role", model.Role }
                        }
                    });

                    if (!tokenResponse.IsError)
                    {
                        response.ResultData = tokenResponse.AccessToken;
                        response.Status = ResponseStatus.OK;
                        response.Message = "Login successful.";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Login failed.";
                    }
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }


        public async Task<List<AdViewerVM>> GetAdViewerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<AdViewerVM>>
                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAdViewerList}?supplierAdId={supplierAdId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }

        public async Task<List<AdViewerVM>> GetAdLikerList(long supplierAdId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<AdViewerVM>>
                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAdLikerList}?supplierAdId={supplierAdId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }
        public async Task<List<AdRatingsVm>> GetAdRatedList(long supplierAdId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<AdRatingsVm>>
                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetAdRatedList}?supplierAdId={supplierAdId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }

        public async Task<List<UserFavoriteTradesmenVM>> GetCustomerFavoriteTradesman(long customerId, int pageSize, int pageNumber)
        {
            return JsonConvert.DeserializeObject<List<UserFavoriteTradesmenVM>>
                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerFavoriteTradesman}?customerId={customerId}&pageSize={pageSize}&pageNumber={pageNumber}", ""));
        }

        public async Task<List<TopTradesmanVM>> GetTopTradesman(int pageSize, int pageNumber, long CategoryId, long customerId)
        {
            return JsonConvert.DeserializeObject<List<TopTradesmanVM>>
                (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetTopTradesman}?pageSize={pageSize}&pageNumber={pageNumber}&CategoryId={CategoryId}&customerId={customerId}"));
        }

        public async Task<bool> UpdateCustomerPublicId(long customerId, string publicId)
        {
            var result = JsonConvert.DeserializeObject<bool>(
                                   await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.UpdateCustomerPublicId}?customerId={customerId}&publicId={publicId}"));
            return result;
        }

        public async Task<CustomerDashBoardCountVM> GetCustomerDashBoardCount(long customerId, string userId)
        {
            var result = JsonConvert.DeserializeObject<CustomerDashBoardCountVM>(await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerDashBoardCount}?customerId={customerId}&userId={userId}"));
            return result;
        }

        public async Task<string> SaveAndRemoveProductsInWishlist(string obj)
        {
            var res="";
            try
            {
                Exc.AddErrorLog(JsonConvert.SerializeObject(obj));
                 res = await httpClient.PostAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.SaveAndRemoveProductsInWishlist}", obj);
                
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return res;
        }

        public async Task<Response> CheckProductStatusInWishList(long customerId, long productId)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.CheckProductStatusInWishList}?customerId={customerId}&productId={productId}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task<string> GetCustomerWishListProducts(long customerId, int pageNumber, int pageSize)
        {
            return await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerWishListProducts}?customerId={customerId}&pageNumber={pageNumber}&pageSize={pageSize}", "");
        }
        public async Task<Response> GetCustomerWishListProductsMobile(long customerId, int pageNumber, int pageSize)
        {
            var response = await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerWishListProductsMobile}?customerId={customerId}&pageNumber={pageNumber}&pageSize={pageSize}", "");
            return JsonConvert.DeserializeObject<Response>(response);
        }
        public async Task<string> GetUserDetailsByUserRole(string userId, string userRole)
        {
            return await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetUserDetailsByUserRole}?userId={userId}&userRole={userRole}", "");
        }

    }
}