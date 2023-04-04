using HS4_BlogProject.Application.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);

        Task<SignInResult> Login(LoginDTO model);

        Task<UpdateProfileDTO> GetByUserName(string username);


        Task UpdateUser(UpdateProfileDTO model);
    }
}
