using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
  public class DispVM
    {
        public long DisputeId{ get; set; }
        public DateTime CreatedOn { get; set; }
        public string disputeStatus { get; set; }
        public long DisputeStatusID { get; set; }

    }
}
