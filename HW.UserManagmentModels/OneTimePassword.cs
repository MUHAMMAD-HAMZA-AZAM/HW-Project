using System;
using System.Collections.Generic;

namespace HW.UserManagmentModels
{
    public partial class OneTimePassword
    {
        public long OtpId { get; set; }
        public string UserId { get; set; }
        public byte[] SecretKey { get; set; }
        public long? TimeWindowUsed { get; set; }
        public int RequestCount { get; set; }
        public int AttemptCount { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
