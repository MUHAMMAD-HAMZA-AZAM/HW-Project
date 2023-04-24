using System;
using System.Collections.Generic;

namespace HW.AudioModels
{
    public partial class BidAudio
    {
        public long BidAudioId { get; set; }
        public string FileName { get; set; }
        public byte[] Audio { get; set; }
        public long BidId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
