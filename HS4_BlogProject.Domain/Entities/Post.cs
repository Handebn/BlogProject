using HS4_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Domain.Entities
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        // Lazy Loading - Veritabanındaki nesneleri doldurup küfeye dolduruyor. Core da padif olarak geliyor.
        // Eager Loading - ihtiyacımız kadarını küfeye dolduruyor.

        //public List<Comment> Comments { get; set; }
        //public List<Like> Likes { get; set; }
    }
}
