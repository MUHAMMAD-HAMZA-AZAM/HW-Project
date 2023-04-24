using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class SpPrimaryUserVM
    {
        public string Id { get; set; }
        public long CustomerId { get; set; }
        public bool isActive { get; set; }
        public string CustomerName { get; set; }
        public string UserId { get; set; }
        public string SkillName { get; set; }
        public string Email { get; set; }
        public bool IsTestUser { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerCNIC { get; set; }
        public string SalesmanName { get; set; }
        public int JobsPosted { get; set; }
        public string SourceOfReg { get; set; }
        public long noOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastActive { get; set; }
        
    }
}
