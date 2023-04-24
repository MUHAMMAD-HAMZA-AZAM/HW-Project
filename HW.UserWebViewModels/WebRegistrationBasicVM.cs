using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.UserWebViewModels
{
    public class WebRegistrationBasicVM
    {
        [Required(ErrorMessage = "Enter First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name.")]
        public string LastName { get; set; }
        public string ShopName { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter a valid email.")]
        public string EmailAddress { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter a valid password.")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Enter a valid phone number.")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Select Gender")]
        public string Gender { get; set; }
        //[Required(ErrorMessage = "Select City")]
        public string FacebookUserId { get; set; }
        public string GoogleUserId { get; set; }
        public string City { get; set; }
        //[Required(ErrorMessage = "Enter a valid email or phone number.")]
        public bool TermsAndConditions { get; set; }
        public string VerificationCode { get; set; }
        public string Role { get; set; }
        public int SellerType { get; set; }
        public int? AccountType { get; set; }
        public string CNIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsAllGoodStatus { get; set; }

    }
}
