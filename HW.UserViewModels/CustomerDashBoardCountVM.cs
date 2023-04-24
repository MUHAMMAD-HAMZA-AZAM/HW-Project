using System;
using System.Collections.Generic;
using System.Text;

namespace HW.UserViewModels
{
    public class CustomerDashBoardCountVM
    {
        public long ActiveJobsCount { get; set; }
        public long CompletedJobsCount { get; set; }
        public long BlogsCount { get; set; }
        public long NotificationsCount { get; set; }

    }
}
