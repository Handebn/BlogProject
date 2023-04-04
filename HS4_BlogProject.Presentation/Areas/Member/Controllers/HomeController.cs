using HS4_BlogProject.Application.Services.Postservice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }
        //Üyerlerin postlarını gösteriyorum
        public IActionResult Index()
        {
            
            return View(_postService.GetPostsForMembers());
        }
    }
}
