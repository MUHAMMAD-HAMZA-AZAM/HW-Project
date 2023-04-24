using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
  public class DisputeVM
    {

        public int JobStatusId { get; set; }
        public long JobDetailId { get; set; }
        public long CustomerId { get; set; }
        public int DisputeStatusId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<ImageVM> ImageVMs { get; set; }
        public AudioVM AudioVM { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] TradesmanImage { get; set; }
        public string CreatedBy { get; set; }

    }
}
