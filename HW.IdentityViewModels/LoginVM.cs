using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW.IdentityViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Enter a valid email or phone number.")]
        public string EmailOrPhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter a valid password.")]
        public string Password { get; set; }

        public string FacebookClientId { get; set; }
        public string GoogleClientId { get; set; }
        public string AppleUserId { get; set; }
        public string FirebaseClientId { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public bool Rememberme { get; set; }
        public long Id { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public string IpAddress { get; set; }
        public string Browser { get; set; }
        public string RequestType { get; set; }
        public int? SellerType { get; set; }
        public int? AccountType { get; set; }
    }
}
