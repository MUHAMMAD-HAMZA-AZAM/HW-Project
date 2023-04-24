using System;
using System.Collections.Generic;

#nullable disable

namespace HW.CMSModels
{
    public partial class Comment
    {
        public long CommentId { get; set; }
        public long? CommentPostId { get; set; }
        public string UserId { get; set; }
        public string CommentContent { get; set; }
        public int? CommentStatus { get; set; }
        public long CommentparentId { get; set; }
        public int? CommentNo { get; set; }
        public string Createdy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
