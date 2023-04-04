using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Repositories;

namespace HS4_BlogProject.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
