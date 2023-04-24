using HW.AudioModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.AudioApi.Services
{
    public interface IAudioService
    {
        IQueryable<BidAudio> GetAllAudio();

        BidAudio GetByJobQuotationId(long jobQuotationId);
        Response SaveBidAudio(BidAudio audio);
        bool SaveDisputeAudio(DisputeAudio disputeAudio);
        BidAudio GetAudioById(long bidId);
    }

    public class AudioService : IAudioService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;

        public AudioService(IUnitOfWork uow, IExceptionService Exc)
        {
            this.uow = uow;
            this.Exc = Exc;
        }

        public IQueryable<BidAudio> GetAllAudio()
        {
            try
            {
                return uow.Repository<BidAudio>().GetAll();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new List<BidAudio>().AsQueryable();
            }
        }

        public BidAudio GetByJobQuotationId(long jobQuotationId)
        {
            try
            {
                return uow.Repository<BidAudio>().Get(a => a.BidId == jobQuotationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new BidAudio();
            }
        }

        public BidAudio GetAudioById(long bidId)
        {
            try
            {
                return uow.Repository<BidAudio>().Get(a => a.BidId == bidId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return new BidAudio();
            }
        }
        public Response SaveBidAudio(BidAudio audio)
        {
            Response response = new Response();
            try
            {
                if (audio.BidId > 0)
                {
                    BidAudio bidAudio = uow.Repository<BidAudio>().Get(x => x.BidId == audio.BidId).FirstOrDefault();
                    if (bidAudio != null)
                    {
                        bidAudio.Audio = audio.Audio;
                        bidAudio.FileName = audio.FileName;
                        bidAudio.ModifiedOn = DateTime.Now;
                        bidAudio.ModifiedBy = audio.CreatedBy;
                        uow.Repository<BidAudio>().Update(bidAudio);
                    }
                    else
                    {
                        uow.Repository<BidAudio>().Add(audio);

                    }

                }
               
                uow.Save();

                response.ResultData = audio;
                response.Message = "Information saved successfully.";
                response.Status = ResponseStatus.OK;
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


        public bool SaveDisputeAudio(DisputeAudio disputeAudio)
        {
            try
            {
                disputeAudio.CreatedOn = DateTime.Now;
                disputeAudio.CreatedBy = "Kam@P0#R!aK-i=a[k2a45r650)";
                uow.Repository<DisputeAudio>().Add(disputeAudio);
                uow.Save();
                if (disputeAudio.DisputeAudioId != 0)
                {
                    return true;
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
    }
}
