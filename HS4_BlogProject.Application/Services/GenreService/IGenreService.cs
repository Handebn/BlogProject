using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.GenreService
{
    
    public interface IGenreService
    {
        Task <List<GenreVM>>GetGenres();
        Task Create(CreateGenreDTO model);
    }
}
