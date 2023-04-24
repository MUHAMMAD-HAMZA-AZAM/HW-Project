using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class EditBidVM
    {
        public long BidsId { get; set; }
        public long JobQuotationId { get; set; }
        public AudioVM Audio { get; set; }
        public long TradesmanId { get; set; }
        public long CustomerId { get; set; }
        public string Comments { get; set; }

        [Display(Name = "Bid Amount")]
        public decimal Amount { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string JobTitle { get; set; }
    }
}
