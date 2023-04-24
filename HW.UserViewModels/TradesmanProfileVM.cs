using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
   public class TradesmanProfileVM
    {
        public long TradesmanId { get; set; }
        public string TradesmanName { get; set; }
        public byte[] TradesmanProfileImg { get; set; }
        public List<string> SkillsSet { get; set; }
        public string GpsCoordinates { get; set; }
        public string TradesmanAddress { get; set; }
        public string TradesmanUserId { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public string MobileNumber { get; set; }
        public int TotalDoneJob { get; set; }
        public DateTime MemberSince { get; set; }
        public int Rating { get; set; }
        public int? Value { get; set; }
        public int? Relaiability { get; set; }
        public string Email { get; set; }
        public string MarkerOptionsAddress { get; set; }
        public int? Reviews { get; set; }
    }

    public class Feedback
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int? Rating { get; set; }
        public string CustomerComment { get; set; }
    }
}
