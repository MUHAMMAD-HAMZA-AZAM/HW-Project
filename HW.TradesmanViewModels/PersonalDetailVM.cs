using System;
using System.ComponentModel.DataAnnotations;

namespace HW.TradesmanViewModels
{
    public class PersonalDetailVM
    {
        public long TradesmanId { get; set; }

        public string UserId { get; set; }
        public string FirebaseId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public string Cnic { get; set; }

        public bool IsOrganization { get; set; }
        
        public string CompanyName { get; set; }

        public Byte Gender { get; set; }

        public byte[] ProfileImage { get; set; }
       
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        public int CSJobStatusId { get; set; }

    }
}
