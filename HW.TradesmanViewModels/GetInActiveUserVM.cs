using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
   public class GetInActiveUserVM
    {
        public long UserIds { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Cnic { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string BusinessAddress { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public DateTime ?CreatedOn { get; set; }
        public DateTime ?LastActive { get; set; }
    }
}
