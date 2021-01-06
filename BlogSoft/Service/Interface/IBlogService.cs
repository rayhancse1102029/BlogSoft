using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Data.Entity.Blog;

namespace BlogSoft.Service.Interface
{
    public interface IBlogService
    {
        #region BlogPost

        Task<bool> SaveBlogPost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPost();
        Task<BlogPost> GetBlogPostById(int id);
        Task<bool> DeleteBlogPostById(int id);

        Task<IEnumerable<BlogPost>> GetAllBlogPostByUser(string username);
        Task<IEnumerable<BlogViewModel>> GetAllBlogPostWithComment();
        Task<IQueryable<BlogViewModel>> Search(string srctxt);
        Task<IQueryable<BlogViewModel>> ListOfDataForAdmin();
        #endregion

        #region Comment

        Task<bool> SaveComment(Comment comment);
        Task<IEnumerable<Comment>> GetAllComment();
        Task<Comment> GetCommentById(int id);
        Task<bool> DeleteCommentById(int id);
        
        #endregion
    }
}
