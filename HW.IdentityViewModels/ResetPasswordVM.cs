using System.ComponentModel.DataAnnotations;

namespace HW.IdentityViewModels
{
    public class ResetPasswordVM
    {
        public string UserId { get; set; }
        public string PasswordResetToken { get; set; }
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
