using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class SuppliersFeedbackVM
    {
        public long SupplierFeedbackId { get; set; }
        public byte[] CustomerProfileImage { get; set; }
        public string CustomerName { get; set; }
        public long CustomerId { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn  { get; set; }
    }
}
