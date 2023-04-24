using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class AudioVM
    {
        public string FileName { get; set; }
        public byte[] AudioContent { get; set; }
        public long DisputeId { get; set; }
    }
}
