using AutoMapper;
using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Entities;

namespace HS4_BlogProject.Application.AutoMapper
{
    // DTO - VM ile Entity arasındaki bağlantıları yapan kısım
    public class Mapping : Profile
    {
        public Mapping()
        {
            // Service katmanında Create işlemi yapılırken dışarıdan gelen CreateGenreDTO nesnesini _mapper.Map<Genre>(model) aracılığıyla Genre ya dönüştürüyoruz.
            CreateMap<Genre, CreateGenreDTO>().ReverseMap(); 
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            
            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Author, AuthorDetailVM>().ReverseMap();

            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostsVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();

            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            //CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();

        }
    }
}
