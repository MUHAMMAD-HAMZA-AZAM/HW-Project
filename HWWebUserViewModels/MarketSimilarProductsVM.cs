using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.UserWebViewModels
{
    public class MarketSimilarProductsVM
    {
        public long SupplierAdId { get; set; }
        public string AdTitle { get; set; }
        public byte[] FileName { get; set; }
        public string AdBudget { get; set; }
        public string CategoryName { get; set; }
    }
}
