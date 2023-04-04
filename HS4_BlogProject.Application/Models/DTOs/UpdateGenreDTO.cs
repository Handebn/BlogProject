using HS4_BlogProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Models.DTOs
{
    public class UpdateGenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must to type Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Name { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;

    }
}
