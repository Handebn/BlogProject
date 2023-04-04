using AutoMapper;
using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Enums;
using HS4_BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
            {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);
            await _genreRepository.Create(genre);
        }

        public async Task<List<GenreVM>>GetGenres()
        {
            var genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,

                },
                where: x => x.Status != Status.Passive);
            return genres;
        }
    }
}
