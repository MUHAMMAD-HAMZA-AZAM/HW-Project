using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class AdminWebUserProfileVM
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string DateofBirth { get; set; }
        public string MobileNo { get; set; }
        public int Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProfileImage { get; set; }
    }
}
