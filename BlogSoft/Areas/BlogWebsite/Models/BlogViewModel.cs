using BlogSoft.Data.Entity.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSoft.Areas.BlogWebsite.Models
{
    public class BlogViewModel
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public string commentText { get; set; }
        public int? likes { get; set; }
        public int? disLikes { get; set; }

        public int? blogPostId { get; set; }
        public string comment { get; set; }

        public BlogPost BlogPost { get; set; }
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<BlogViewModel> BlogViewModels { get; set; }
    }
}
