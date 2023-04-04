using HS4_BlogProject.Application.Services.Postservice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postservice)
        {
            _postService = postservice;
        }

        //Üyerlerin postlarını gösteriyorum.
        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetPostsForMembers());
        }
    }
}
