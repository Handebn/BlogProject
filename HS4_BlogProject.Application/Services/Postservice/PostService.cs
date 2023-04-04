using AutoMapper;
using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Enums;
using HS4_BlogProject.Domain.Repositories;
using HS4_BlogProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.Postservice
{
    // Nuget: SixLabors
    // using SixLabors.ImageSharp;
    // using SixLabors.ImageSharp.Processing;

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        // Dependency Injection
        public PostService(IPostRepository postRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        //_mapper.Map<Post>(model) ile Service.Create metoduna gönderilen CreatePostDTO nesnesini Post nesnesine çeviriyor. Mapping sınıfı içine bağlantılarını yazdık.
        // View'dan gelen nesnesi değiştirip veritabanına gönderiyoruz
        public async Task Create(CreatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = $"/images/defaultpost.jpg";
            }

            await _postRepository.Create(post);

            //Post post = new Post();
            //post.AuthorId = 3;
            //post.Content = "asd as dasd asd asd";
            //post.GenreId = 2;
            //post.Title = "malakem";

            //postRepository.Create(model);
        }


        // id ile Post nesnesini veritabanında pasife alma işlemi yapıyoruz.
        /// <summary>
        /// Status'u Pasife alırız
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);
            post.Status = Status.Passive;
            post.DeleteDate = DateTime.Now;
            await _postRepository.Delete(post);
        }

        // Veritabanında çekilen veriyi PostVM ile View'a gönderiyoruz.
        // Veritabanından tüm verileri küfeye yükleyip merdivenlerin sonuna gelince azaltıp, vermiyoruz. En başında veritabanından küfemize ihtiyacımız olanı yüklüyoruz.
        public async Task<UpdatePostDTO> GetById(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
               select: x => new PostVM
               {
                   Title = x.Title,
                   Content = x.Content,
                   ImagePath = x.ImagePath,
                   GenreId = x.GenreId,
                   AuthorId = x.AuthorId
               },
               where: x => x.Id == id);

            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName));

            model.Genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));

            return model;
        }

        // veritabanından PostDetailsVM aracılığyla View'a veri taşıyoruz.
        public async Task<PostDetailVM> GetPostDetailsVM(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailVM
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorImagePath = x.Author.ImagePath,
                    AuthorLastName = x.Author.LastName,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title,
                },
                where: x => x.Id == id,
                orderBy: null,
                include: x => x.Include(x => x.Author));

            return post;

        }

        // veritabanından PostVM aracılığyla View'a veri taşıyoruz. Genre ve Author'ı eagerloading ile küfeye yüklüyoruz.
        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre)
                                .Include(x => x.Author)
                );

            return posts;
        }

        // View'da kullanıcının değiştirdiği alanlar UpdatePostDTO nesnesi aracılığıyla service gönderilir. _mapper.Map<Post>(model) ile eşleme yapılır. Veritabanında güncelleme yapılır.
        public async Task Update(UpdatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            //Post post1 = new Post();
            //post1.AuthorId = model.AuthorId;
            //post1.Content = model.Content;
            //post1.GenreId = model.GenreId;
            //post1.ImagePath = model.ImagePath;
            //post1.Status = model.Status;
            //post1.Title = model.Title;
            //post1.UploadPath = model.UploadPath;

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = model.ImagePath;
            }

            await _postRepository.Update(post);
        }


        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM
                    {
                        Id = x.Id,
                        Name = x.Name
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)),

                Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName                      
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName).ThenBy(x => x.LastName))
            };

            return model;     
        }

        public Task <List<GetPostsVM>>GetPostsForMembers()
        {
            var posts = _postRepository.GetFilteredList(
                select: x => new GetPostsVM
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title,
                    Id = x.Id,
                    AuthorImagePath=x.Author.ImagePath
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate)
                //include: x => x.Include(x=>x.Author).Include(x=> x.CreateDate)
                );

            return posts;
        }
    }
}
