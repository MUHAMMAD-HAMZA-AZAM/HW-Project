using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class PromaotionPaymentHistoryVM
    {
        public long PaymentHistoryId { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public decimal? CreditedAmount { get; set; }
        public decimal? DebitedAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public decimal? RemainingAmount { get; set; }
    }
}
