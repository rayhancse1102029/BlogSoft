using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSoft.Data.Entity.Blog
{
    public class Comment : Base
    {
        public int? blogPostId { get; set; }
        public BlogPost blogPost { get; set; }

        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public string commentText { get; set; }
        public int? likes { get; set; }
        public int? disLikes { get; set; }

    }
}
