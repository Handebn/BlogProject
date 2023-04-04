using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Repositories;

namespace HS4_BlogProject.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
