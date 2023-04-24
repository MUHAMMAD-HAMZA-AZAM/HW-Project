using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class TransactionsVM
    {
        public long? CustomerId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
       
    }
}
