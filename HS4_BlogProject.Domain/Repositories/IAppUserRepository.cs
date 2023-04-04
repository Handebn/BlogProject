using HS4_BlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Domain.Repositories
{
    public interface IAppUserRepository: IBaseRepository<AppUser>
    {
        // IAppUserRepository içinde kullanmak durumunda olduğumuz IBaseRepository içerisinde olmayan metot imzalarını buraya yazıyoruz.
        
    }
}
