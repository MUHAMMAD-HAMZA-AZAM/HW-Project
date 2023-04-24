using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class ProfileVerificationVM
    {
        public bool SellerAccountVerification { get; set; }
        public bool BusinessInformationVerification { get; set; }
        public bool BankAccountVerification { get; set; }
        public bool WareHouseAddressVerification { get; set; }
        public bool ReturnAddressVerification { get; set; }
        public bool IsAllGoodStatus { get; set; }
    }
}
