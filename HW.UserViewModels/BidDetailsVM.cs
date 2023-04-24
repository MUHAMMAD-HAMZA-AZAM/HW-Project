using System;

namespace HW.UserViewModels
{
    public class BidDetailsVM
    {
        public long BidId { get; set; }
        public string TradesmanUserId { get; set; }
        public long TradesmanId { get; set; }
        public long? CustomerId { get; set; }
        public string TradesmanName { get; set; }
        public byte[] TradesmanProfileImage { get; set; }
        public byte[] JobImage { get; set; }
        public string JobTitle { get; set; }
        public decimal CustomerBudget { get; set; }
        public decimal TradesmanOffer { get; set; }
        public string JobDescription { get; set; }
        public byte[] BidAudioMessage { get; set; }
        public string BidAudioFileName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public long JobQuotationId { get; set; }
        public DateTime BidPostedOn { get; set; }
        public string TradesmanAddress { get; set; }
        public string CustomerName { get; set; }
        public int BidStatusId { get; set; }
        public long? JobDetailsId { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public decimal? OtherCharges { get; set; }
        public long? SkillId { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public bool? IsStarted { get; set; }
        public bool? IsFinished { get; set; }
        public string Action { get; set; }
        public int? PaymentMethod { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }
}
