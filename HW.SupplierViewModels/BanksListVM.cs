using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
  public  class BanksListVM
    {
       
            public long BankId { get; set; }
        public string UserId { get; set; }

        public string BankName { get; set; }
            public bool? Active { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? ModifiedOn { get; set; }
            public string ModifiedBy { get; set; }
        
    }
}
