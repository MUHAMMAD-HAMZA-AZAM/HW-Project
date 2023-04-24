using System.ComponentModel.DataAnnotations;

namespace HW.IdentityViewModels
{
    public class UserRegisterVM
    {
        public string Id { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }
        public bool IsNumberConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public long? CityId { get; set; }
        public string Role { get; set; }
        public int SellerType { get; set; }
        public int? AccountType { get; set; }
        public string subrole { get; set; }
        public string FirebaseClientId { get; set; }
        public string FacebookUserId { get; set; }
        public string GoogleUserId { get; set; }
        public string AppleUserId { get; set; }
        public string VerificationCode { get; set; }
        public string SecurityRole { get; set; }
        public string ShopName { get; set; }
        public string UserName { get; set; }
        public long ClientId { get; set; }
        public bool HasPin { get; set; }
        public bool TermsAndConditions { get; set; }
        public bool FromPersonalDetails { get; set; }
        public string LatLong { get; set; }
        public bool? IsAllGoodStatus { get; set; }

        public string HashKey { get; set; }
    }
}
