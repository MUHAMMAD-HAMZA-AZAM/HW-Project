using System;
using System.Collections.Generic;

namespace HW.PromotionsModels
{
    public partial class Book
    {
        public long BookId { get; set; }
        public string BookCode { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedOn { get; set; }
        public int AssigneeRole { get; set; }
        public int LeafLimit { get; set; }
    }
}
