using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
   public class SpBusinessProfileVM
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TradeName { get; set; }
        public string Cnic { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Mobile { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Gender { get; set; }
        public string RegistrationNo { get; set; }
        public string PrimaryTrade { get; set; }
        public string TravellingDistance { get; set; }
        public string Skills { get; set; }
        public string LatLong { get; set; }
        public string BusinessAddress { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public byte[] ProfileImage  { get; set; }
        public List<TrradesmanJobsFeedbackVM> trradesmanJobsFeedbackVMs  { get; set; }
    }
}
