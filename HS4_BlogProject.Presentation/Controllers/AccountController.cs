using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Controllers
{
    /*
     Core identity is a membership
    Create, update , delete user accounts 
    AccountConfiguration
    Authentication & Authorization
    Password Recovery
    Two - factor authentication with sms
    Microsoft, facebook, google login providers
     */
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) // eğer kullanıcı hali hazırda sisteme Authenticate olmuşsa
            {
                return RedirectToAction("Index", ""); // Areas
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // Service 
            // AppUserService.Create(registerDTO);

            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(registerDTO);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "");
                }

                // Identity'nin içerisinde gömülü olarak bulunan Errors listesinin içerisinde dolaşıyoruz. result error ile dolarsa hataları yazdırıyoruz.

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong";
                }
            }

            return View();
           
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO model)
        { 
            if(ModelState.IsValid)
            {
                _appUserService.Login(model);
            }
            return RedirectToAction("Index","");
        }
        /*
        //Profil Güncelleme işini Jira yada Azure vb. platformlar farklı hangi platform kullanıyorsa, iş parçası için kullanılır.
        
         
         */
        public async Task<IActionResult> Edit(string username)
        {
            //Kullanıcı bilgilerimizi edit edeceğiz

            UpdateProfileDTO user = await _appUserService.GetByUserName(username);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            //Validation 
            //service metoduna UpdateProfileDTO
            if(ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                return RedirectToAction("Index", "Home"); //değişecek
            }
            else
            {
                TempData["Error"] = "Your Profile hasnt been updated!";
                return View(model);
            }
            
        }

    }
}
