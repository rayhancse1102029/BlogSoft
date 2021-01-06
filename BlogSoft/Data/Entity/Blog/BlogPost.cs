using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSoft.Data.Entity.Blog
{
    public class BlogPost : Base
    {
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public string title { get; set; }
        public string description { get; set; }
    }
}
