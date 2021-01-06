using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Data;
using BlogSoft.Data.Entity.Blog;
using BlogSoft.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogSoft.Service
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;


        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        #region BlogPost
        public async Task<bool> SaveBlogPost(BlogPost blogPost)
        {
            if (blogPost.Id != 0)
            {
                _context.BlogPosts.Update(blogPost);

                await _context.SaveChangesAsync();

                return true;
            }

            await _context.BlogPosts.AddAsync(blogPost);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPost()
        {
            return await _context.BlogPosts.Include(x=>x.applicationUser).AsNoTracking().ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostById(int id)
        {
            return await _context.BlogPosts.Include(x => x.applicationUser).Where(x=>x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBlogPostById(int id)
        {
            _context.BlogPosts.Remove(_context.BlogPosts.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostByUser(string username)
        {

            return await _context.BlogPosts.Where(x=>x.createdBy == username).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogPostWithComment()
        {
            List<BlogPost> blogPosts = await _context.BlogPosts.Include(x => x.applicationUser).AsNoTracking().ToListAsync();
            List<BlogViewModel> model = new List<BlogViewModel>();
            if (blogPosts.Count > 0)
            {
                foreach (var blog in blogPosts)
                {
                    BlogViewModel blogPost = new BlogViewModel
                    {
                         BlogPost = blog,
                         Comments = await _context.Comments.Include(x => x.applicationUser).Where(x=>x.blogPostId == blog.Id).ToListAsync()
                    };
                    model.Add(blogPost);
                }
            }

            return model;
        }

        public async Task<IQueryable<BlogViewModel>> Search(string srctxt)
        {
            srctxt = srctxt.ToUpper();

            List<BlogPost> blogPosts = await _context.BlogPosts.Where(x=>x.title.Contains(srctxt) || x.description.Contains(srctxt) || x.createdAt.ToString().Contains(srctxt) || x.createdBy.Contains(srctxt)).Include(x => x.applicationUser).AsNoTracking().ToListAsync();
            List<BlogViewModel> model = new List<BlogViewModel>();

            if (blogPosts.Count > 0)
            {
                foreach (var blog in blogPosts)
                {
                    BlogViewModel blogPost = new BlogViewModel
                    {
                        BlogPost = blog,
                        Comments = await _context.Comments.Include(x => x.applicationUser).Where(x => x.blogPostId == blog.Id).ToListAsync()
                    };
                    
                    model.Add(blogPost);
                }
            }

            return model.AsQueryable();
        }

        public async Task<IQueryable<BlogViewModel>> ListOfDataForAdmin()
        {

            List<BlogPost> blogPosts = await _context.BlogPosts.Include(x => x.applicationUser).AsNoTracking().ToListAsync();
            List<BlogViewModel> model = new List<BlogViewModel>();

            if (blogPosts.Count > 0)
            {
                foreach (var blog in blogPosts)
                {
                    BlogViewModel blogPost = new BlogViewModel
                    {
                        BlogPost = blog,
                        Comments = await _context.Comments.Include(x => x.applicationUser).Where(x => x.blogPostId == blog.Id).ToListAsync()
                    };

                    model.Add(blogPost);
                }
            }

            return model.AsQueryable();
        }

        #endregion

        #region Comment
        public async Task<bool> SaveComment(Comment comment)
        {
            try
            {
                if (comment.Id != 0)
                {
                    _context.Comments.Update(comment);
                }
                else
                {
                    await _context.Comments.AddAsync(comment);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {

            return await _context.Comments.Include(x => x.applicationUser).AsNoTracking().ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.Include(x => x.applicationUser).Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCommentById(int id)
        {
            _context.Comments.Remove(_context.Comments.Find(id));

            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

    }
}
