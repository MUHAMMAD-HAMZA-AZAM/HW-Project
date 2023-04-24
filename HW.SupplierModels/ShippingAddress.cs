using System;
using System.Collections.Generic;

namespace HW.SupplierModels
{
    public partial class ShippingAddress
    {
        public int Id { get; set; }
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public long OrderId { get; set; }
        public int? City { get; set; }
    }
}
