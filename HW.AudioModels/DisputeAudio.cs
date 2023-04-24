using System;
using System.Collections.Generic;

namespace HW.AudioModels
{
    public partial class DisputeAudio
    {
        public long DisputeAudioId { get; set; }
        public string FileName { get; set; }
        public byte[] Audio { get; set; }
        public long DisputeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
