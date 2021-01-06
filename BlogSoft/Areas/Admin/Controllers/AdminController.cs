using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Data.Entity;
using BlogSoft.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSoft.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IBlogService _blogService, UserManager<ApplicationUser> _userManager)
        {
            this._blogService = _blogService;
            this._userManager = _userManager;
        }


        public async Task<IActionResult> Index(string srctext, int page = 1)
        {
            ViewBag.srctext = srctext;

            if (!string.IsNullOrEmpty(srctext))
            {
                IQueryable<BlogViewModel> db = await _blogService.Search(srctext);

                if (page <= 0) { page = 1; }
                int pageSize = 5;
                return View(PaginatedList<BlogViewModel>.Create(db, page, pageSize));

            }
            else
            {
                IQueryable<BlogViewModel> db = await _blogService.ListOfDataForAdmin();

                if (page <= 0) { page = 1; }
                int pageSize = 5;
                return View(PaginatedList<BlogViewModel>.Create(db, page, pageSize));
            }
           
        }

      

    }
}