using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class EditBidsVM
    {
        public long BidsId { get; set; }
        public long JobQuotationId { get; set; }
        public string  FileName { get; set; }
        public byte[] Audio { get; set; }
        public long TradesmanId { get; set; }
        public long CustomerId { get; set; }
        public string Comments { get; set; }
        public decimal Amount { get; set; }
        public int StatusId { get; set; }
    }
}
