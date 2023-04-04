using HS4_BlogProject.Application.Extensions;
using HS4_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Must to type UserName")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must to type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must to type Confirm Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must to type Email")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }

    }
}
