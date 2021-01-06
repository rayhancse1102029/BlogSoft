using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSoft.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogAPIController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogAPIController(IBlogService _blogService)
        {
            this._blogService = _blogService;
        }

        [AllowAnonymous]
        // api/BlogAPI/GetAllBlogPostWithComment
        [HttpGet]
        public async Task<IEnumerable<BlogViewModel>> GetAllBlogPostWithComment()
        {
            return await _blogService.GetAllBlogPostWithComment();
        }
    }
}