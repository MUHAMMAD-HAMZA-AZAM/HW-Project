using System;
using System.Collections.Generic;
using System.Text;

namespace HW.CMSViewModel
{
    public partial class PostVM
    {
        public long CategoryId { get; set; }
        public long PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string Summary { get; set; }
        public int PostStatus { get; set; }
        public bool CommentStatus { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string ImageBase64 { get; set; }
        public byte[] HeaderImage { get; set; }
        public string CategoryName { get; set; }
        public string PostStatusName { get; set; }
        public string UserName { get; set; }
        public string PostAction { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string MetaTags { get; set; }
        public string Slug { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int PostsCount { get; set; }


    }
}
