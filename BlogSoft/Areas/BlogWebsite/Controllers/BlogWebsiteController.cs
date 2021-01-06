using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Data.Entity;
using BlogSoft.Data.Entity.Blog;
using BlogSoft.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSoft.Areas.BlogWebsite.Controllers
{
    [Authorize]
    [Area("BlogWebsite")]
    public class BlogWebsiteController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogWebsiteController(IBlogService _blogService, UserManager<ApplicationUser> _userManager)
        {
            this._blogService = _blogService;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            BlogViewModel model = new BlogViewModel
            {
                BlogViewModels = await _blogService.GetAllBlogPostWithComment()
            }; 
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            BlogViewModel model = new BlogViewModel
            {
                BlogPosts = await _blogService.GetAllBlogPostByUser(User.Identity.Name)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(BlogViewModel model)
        {
            string msg = "error";

            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if ( user != null && !string.IsNullOrEmpty(model.title) && !string.IsNullOrEmpty(model.description))
            {
                BlogPost blogPost = new BlogPost
                {
                    Id = (int)model.id,
                    title = model.title,
                    description = model.description,
                    applicationUserId = user.Id,
                    createdAt = DateTime.Now,
                    createdBy = User.Identity.Name
                };
                await _blogService.SaveBlogPost(blogPost);
                msg = "success";
            }

            return Json(msg);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(BlogViewModel model)
        {
            string msg = "error";

            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if ( user != null && !string.IsNullOrEmpty(model.comment))
                {
                    Comment comment= new Comment
                    {
                        Id = (int)model.id,
                        commentText = model.comment,
                        applicationUserId = user.Id,
                        blogPostId = model.blogPostId,
                        createdAt = DateTime.Now,
                        createdBy = User.Identity.Name
                    };
                    await _blogService.SaveComment(comment);
                    msg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(msg);
        }

        [HttpGet]
        public async Task<IActionResult> like(int id)
        {
            string msg = "error";

            try
            {
                Comment comment = await _blogService.GetCommentById(id);
                if (comment != null)
                {
                    comment.likes = Convert.ToInt32(comment.likes) + 1;
                }
                await _blogService.SaveComment(comment);
                msg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(msg);
        }

        [HttpGet]
        public async Task<IActionResult> disLike(int id)
        {
            string msg = "error";

            try
            {
                Comment comment = await _blogService.GetCommentById(id);
                if (comment != null)
                {
                    comment.disLikes = Convert.ToInt32(comment.disLikes) + 1;
                }
                await _blogService.SaveComment(comment);
                msg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(msg);
        }
    }
}