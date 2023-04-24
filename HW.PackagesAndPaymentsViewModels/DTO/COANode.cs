using System;
using System.Collections.Generic;
using System.Text;

namespace HW.PackagesAndPaymentsViewModels.DTO
{
    public class COANode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsControlAccount { get; set; }
        public List<COANode> Children { get; set; }
    }
}
