using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task <IActionResult> Index()
        {
            List<GenreVM> model= await _genreService.GetGenres();
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDTO model)
        {
           if(ModelState.IsValid)
            {
                await _genreService.Create(model);
                RedirectToAction("index");
            }
            return View(model);
        }


    }
}
