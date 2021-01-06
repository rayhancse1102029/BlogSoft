using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Areas.BlogWebsite.Models;
using BlogSoft.Helpers;
using BlogSoft.Service.Interface;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogSoft.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public ReportController(IBlogService _blogService, IHostingEnvironment _hostingEnvironment, IConverter converter)
        {
            this._blogService = _blogService;

            this._hostingEnvironment = _hostingEnvironment;
            myPDF = new MyPDF(_hostingEnvironment, converter);
            rootPath = _hostingEnvironment.ContentRootPath;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult PdfReport()
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
            url = scheme + "://" + host + "/Account/Report/GeneratePdfReport";

            string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> GeneratePdfReport()
        {
            try
            {
                IEnumerable<BlogViewModel> data = await _blogService.GetAllBlogPostWithComment();

                return PartialView(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}