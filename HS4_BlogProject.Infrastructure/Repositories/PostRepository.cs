using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Repositories;

namespace HS4_BlogProject.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
