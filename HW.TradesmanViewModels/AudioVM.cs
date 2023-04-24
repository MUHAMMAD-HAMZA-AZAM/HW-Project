using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class AudioVM
    {
        public long AudioId { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public string Base64String { get; set; }
        public byte[] AudioContent { get; set; }
    }
}
