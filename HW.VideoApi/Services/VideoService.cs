using HW.VideoModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.VideoApi.Services
{
    public interface IVideoService
    {
        IQueryable<JobQuotationVideo> GetAllVideo();
        JobQuotationVideo GetByJobQuotationId(long jobQuotationId);
        SupplierAdVideos GetSupplierAdVideoByAdId(long supplierAdId);
        string GetSupplierAdVideoNameByAdId(long supplierAdId);
        Task AddVideo(JobQuotationVideo jobQuotationVideo);
        Task<Response> AddJobVideo(JobQuotationVideo jobVideo);
        Task DeleteJobQuotationVideo(long jobQuotationId);
        Task DeleteAdVideo(long supplierAdsId);
        void UpdateSuplierAdVideo(SupplierAdVideos supplierAdVideo);
        void SubmitAndUpdateAdVideo(SupplierAdVideos supplierAdVideos);
        Task<Response> UpdateJobVideoStatus(long jobQuotationId, bool isActive);
    }

    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        public VideoService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public void UpdateSuplierAdVideo(SupplierAdVideos supplierAdVideo)
        {
            try
            {
                var oldVideoId = uow.Repository<SupplierAdVideos>().GetAll().Where(x => x.SupplierAdsId == supplierAdVideo.SupplierAdsId).Select(x => x.AdVideoId).FirstOrDefault();
                if (oldVideoId > 0)
                {
                    var oldVideo = uow.Repository<SupplierAdVideos>().GetById(oldVideoId);
                    oldVideo.VideoName = supplierAdVideo.VideoName;
                    oldVideo.AdVideo = supplierAdVideo.AdVideo;
                    uow.Repository<SupplierAdVideos>().Update(oldVideo);
                }
                else
                    uow.Repository<SupplierAdVideos>().Add(supplierAdVideo);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

        }

        public async Task AddVideo(JobQuotationVideo jobQuotationVideo)
        {
            try
            {
                await uow.Repository<JobQuotationVideo>().AddAsync(jobQuotationVideo);
                await uow.SaveAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

        }

        public async Task<Response> AddJobVideo(JobQuotationVideo jobVideo)
        {
            Response response = new Response();
            try
            {
                JobQuotationVideo video = uow.Repository<JobQuotationVideo>().Get(x => x.JobQuotationId == jobVideo.JobQuotationId).FirstOrDefault();

                if (video != null)
                {
                    video.Video = jobVideo.Video;
                    video.FileName = jobVideo.FileName;
                    video.ModifiedOn = DateTime.Now;
                    video.ModifiedBy = jobVideo.CreatedBy;

                    uow.Repository<JobQuotationVideo>().Update(video);

                    response.Message = "Video has been updated successfully";
                }
                else
                {
                    await uow.Repository<JobQuotationVideo>().AddAsync(jobVideo);

                    response.Message = "Video has been uploaded successfully";
                }

                await uow.SaveAsync();

                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                response.Message = ex.Message;
                response.Status = ResponseStatus.OK;
            }
            return response;
        }


        public async Task DeleteAdVideo(long supplierAdsId)
        {
            try
            {
                var query = uow.Repository<SupplierAdVideos>().Get(x => x.SupplierAdsId == supplierAdsId).Select(s => s.AdVideoId).FirstOrDefault();
                await uow.Repository<SupplierAdVideos>().DeleteAsync(query);
                await uow.SaveAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task DeleteJobQuotationVideo(long jobQuotationId)
        {
            try
            {
                JobQuotationVideo query = uow.Repository<JobQuotationVideo>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();

                await uow.Repository<JobQuotationVideo>().DeleteAsync(query.VideoId);
                await uow.SaveAsync();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

        }

        public IQueryable<JobQuotationVideo> GetAllVideo()
        {
            try
            {
                return uow.Repository<JobQuotationVideo>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<JobQuotationVideo>().AsQueryable();
            }
        }

        public JobQuotationVideo GetByJobQuotationId(long jobQuotationId)
        {
            try
            {
                JobQuotationVideo jobQuotationVideo = uow.Repository<JobQuotationVideo>().Get(a => a.JobQuotationId == jobQuotationId).FirstOrDefault();
                return jobQuotationVideo;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new JobQuotationVideo();
            }
        }

        public SupplierAdVideos GetSupplierAdVideoByAdId(long supplierAdId)
        {
            try
            {
                return uow.Repository<SupplierAdVideos>().GetAll().FirstOrDefault(x => x.SupplierAdsId == supplierAdId);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new SupplierAdVideos();
            }
        }

        public void SubmitAndUpdateAdVideo(SupplierAdVideos supplierAdVideos)
        {
            try
            {
                if (supplierAdVideos.SupplierAdsId > 0)
                {
                    var adVideoId = uow.Repository<SupplierAdVideos>().GetAll().Where(x => x.SupplierAdsId == supplierAdVideos.SupplierAdsId).Select(id => id.AdVideoId).FirstOrDefault();
                    if (adVideoId > 0)
                    {
                        var adVideo = uow.Repository<SupplierAdVideos>().GetById(adVideoId);
                        adVideo.AdVideo = supplierAdVideos.AdVideo;
                        adVideo.VideoName = "vid-" + DateTime.Now.Ticks + ".mp4";
                        adVideo.ModifiedOn = DateTime.Now;
                        uow.Repository<SupplierAdVideos>().Update(adVideo);
                    }
                    else
                    {
                        supplierAdVideos.VideoName = "vid-" + DateTime.Now.Ticks + ".mp4";
                        uow.Repository<SupplierAdVideos>().Add(supplierAdVideos);
                    }
                }
                uow.Save();
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
                var exceptionMessage = e.Message;
            }
        }

        public string GetSupplierAdVideoNameByAdId(long supplierAdId)
        {
            try
            {
                return uow.Repository<SupplierAdVideos>().Get().Where(x => x.SupplierAdsId == supplierAdId).Select(s => s.VideoName).FirstOrDefault();
            }
            catch (Exception e)
            {
                Exc.AddErrorLog(e);
                return "";
            }
        }

        public async Task<Response> UpdateJobVideoStatus(long jobQuotationId, bool isActive)
        {
            Response response = new Response();

            try
            {
                JobQuotationVideo jobVideo = uow.Repository<JobQuotationVideo>().Get(x => x.JobQuotationId == jobQuotationId).FirstOrDefault();

                if (jobVideo != null)
                {
                    jobVideo.IsActive = isActive;

                    uow.Repository<JobQuotationVideo>().Update(jobVideo);
                    await uow.SaveAsync();

                    response.Message = "Job Video status updated successfully";
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Message = "Job Video not found";
                    response.Status = ResponseStatus.Error;
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
    }
}