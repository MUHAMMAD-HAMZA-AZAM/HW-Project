using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
   public class GetTradesmanBySkillAndTownVM
    {
        public long TradesmanId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public string GpsCoordinates { get; set; }
        public string Area { get; set; }
        public string ShopAddress { get; set; }
        public string AddressLine { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        public byte[] ProfileImage { get; set; }

    }
}
