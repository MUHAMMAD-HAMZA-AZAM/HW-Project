using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
   public class SalesmanVM
    {
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? noOfRecoards { get; set; }
        public long? SalesmanId { get; set; }
        public string CustomerId { get; set; }
        public long? CampaignId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string CampaignName { get; set; }
        public string ShortName { get; set; }
        public string MobileNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string MobifiedBy { get; set; }
        public bool? IsActive { get; set; }
      
    }
}
