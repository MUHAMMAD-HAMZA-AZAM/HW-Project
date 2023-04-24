using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.IdentityViewModels
{
    public class PersonalDetailsVM
    {
        public string UserId { get; set; }
        public long EntityId { get; set; }
        public string ShopName { get; set; }
        public long JobQuotationId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "CNIC #")]
        public string Cnic { get; set; }
        public bool IsNumberConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int Gender { get; set; }
        public string Role { get; set; }
        public byte[] ProfileImage { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime ?DateOfBirth { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        public string LatLong { get; set; }
        public string Town { get; set; }
        public long TownId { get; set; }
        public long CityId { get; set; }
        public string Address { get; set; }
        public string Relationship { get; set; }
        public string UserRole { get; set; }
        public long? SupplierId { get; set; }
        public long? CustomerId { get; set; }
        public int? AccountType { get; set; }
        public bool? FeaturedSupplier { get; set; }
        public string City { get; set; }
        public string FirebaseClientId { get; set; }
        public bool? IsAllGoodStatus { get; set; }
    }
}
