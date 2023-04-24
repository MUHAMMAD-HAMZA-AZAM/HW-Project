using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class CheckUserExistenceVM
    {
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? TradesmanId { get; set; }
        public long? SupplierId { get; set; }
    }
}
