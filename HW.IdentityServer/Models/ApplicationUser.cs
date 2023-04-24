using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;

namespace HW.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirebaseClientId { get; set; }
        public string FacebookUserId { get; set; }
        public string GoogleUserId { get; set; }
        public string AppleUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastActive { get; set; }
        [DefaultValue(false)]
        public bool? IsTestUser { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool? HasPIN { get; set; }
        [DefaultValue(false)]
        public bool IsBlocked { get; set; }
    }
}
