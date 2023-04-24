using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels
{
    public partial class SocialMediaLinks
    {
        public long Id { get; set; }
        public long SupplierId { get; set; }
        public string FacebookUrl { get; set; }
        public bool? IsActive { get; set; }
        public string YoutubeUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
