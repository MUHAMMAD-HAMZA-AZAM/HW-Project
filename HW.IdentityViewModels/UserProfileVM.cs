using System.Collections.Generic;

namespace HW.IdentityViewModels
{
    public class UserProfileVM
    {
        public long  tradesmanId { get; set; }
        public string UserId { get; set; }
        public long CustomerId { get; set; }
        public long EntityId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] ProfileImage { get; set; }
        public string City { get; set; }
        public List<string> Skills { get; set; }
        public string PublicId { get; set; }
    }
}
