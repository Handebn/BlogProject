using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.Postservice
{
    public interface IPostService
    {
        Task Create(CreatePostDTO model);

        Task Update(UpdatePostDTO model);

        Task Delete(int id);

        Task<UpdatePostDTO> GetById(int id);

        Task<List<PostVM>> GetPosts();

        Task<PostDetailVM> GetPostDetailsVM(int id);

        Task<CreatePostDTO> CreatePost();

        Task <List<GetPostsVM>> GetPostsForMembers(); // Posstları getirecek
    }
}
