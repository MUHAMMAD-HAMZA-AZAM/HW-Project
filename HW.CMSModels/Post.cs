using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class Post
    {
        public long PostId { get; set; }
        public long? CategoryId { get; set; }
        public string UserId { get; set; }
        public byte[] HeaderImage { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string Summary { get; set; }
        public int? PostStatus { get; set; }
        public bool? CommentStatus { get; set; }
        public int? MenuOrder { get; set; }
        public int? PostType { get; set; }
        public int? CommentCount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string MetaTags { get; set; }
        public string Slug { get; set; }
    }
}
