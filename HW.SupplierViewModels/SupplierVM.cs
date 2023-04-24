using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierViewModels
{
    public class SupplierVM
    {
        public long SupplierId { get; set; }
    }

    public class SupplierReportVM:HW.SupplierModels.Supplier
    {
        public string Name  { get; set; }

    }
}
