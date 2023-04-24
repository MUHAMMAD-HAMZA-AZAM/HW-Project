using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class AdminTradesmanListVM
    {

        public string Id { get; set; }
        public long TradesmanId { get; set; }
        public bool isActive { get; set; }
        public long NoOfRecoards { get; set; }
        public long RecordNo { get; set; }
        public long BidsCount { get; set; }
        public string TradesmanName { get; set; }
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public bool IsTestUser { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public string Skills { get; set; }
        public string SalesmanName { get; set; }
        public string SourceOfReg { get; set; }
        public string MobileNo { get; set; }
        public string TradesmanAddress { get; set; }
        public int CompletedJobsCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
