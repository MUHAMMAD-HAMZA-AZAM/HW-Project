using System;
using System.Collections.Generic;
using System.Text;

namespace HW.TradesmanViewModels
{
    public class TrradesmanJobsFeedbackVM
    {
        public long JdId { get; set; }
        public string Title { get; set; }
        public long TradesmanRating { get; set; }
        public string Comments { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
