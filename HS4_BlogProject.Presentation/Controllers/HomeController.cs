using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Application.Services.Postservice;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Controllers
{

    // Makaleler listelensin
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService) => _postService = postService;

        public async Task<IActionResult> Index()
        {
            List<PostVM> model = await _postService.GetPosts();
            return View(model);
        }
         
    }
}
