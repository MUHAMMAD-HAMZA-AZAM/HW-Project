using System.Collections.Generic;
using System.Linq;
using HW.AudioApi.Services;
using HW.AudioModels;
using Microsoft.AspNetCore.Mvc;
using HW.Utility;

namespace HW.AudioApi.Controllers
{
    [Produces("application/json")]
    public class AudioController : BaseController
    {
        // GET api/values
        private readonly IAudioService audioService;

        public AudioController(IAudioService audioService)
        {
            this.audioService = audioService;
        }

        [HttpGet]
        public string Start()
        {
            return "Audio service is started.";
        }

        [HttpGet]
        public List<BidAudio> GetAllAudio()
        {
            return audioService.GetAllAudio().ToList();
        }
        [HttpGet]
        public BidAudio GetByJobQuotationId(long jobQuotationId)
        {
            return audioService.GetByJobQuotationId(jobQuotationId);
        }

        [HttpGet]
        public BidAudio GetAudioById(long bidId)
        {
            return audioService.GetAudioById(bidId);
        }

        [HttpPost]
        public Response SaveBidAudio([FromBody] BidAudio audio) // how can i pass list of JobQuotationId in long??
        {
            return audioService.SaveBidAudio(audio);
        }

        [HttpPost]
        public bool SaveDisputeAudio([FromBody] DisputeAudio disputeAudio)
        {
            return audioService.SaveDisputeAudio(disputeAudio);
        }

    }
}
