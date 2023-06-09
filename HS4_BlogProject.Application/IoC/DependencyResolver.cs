﻿using Autofac;
using AutoMapper;
using HS4_BlogProject.Application.AutoMapper;
using HS4_BlogProject.Application.Services.AppUserService;
using HS4_BlogProject.Application.Services.GenreService;
using HS4_BlogProject.Application.Services.Postservice;
using HS4_BlogProject.Domain.Repositories;
using HS4_BlogProject.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.IoC
{
    // Ioc : Inversion of Control
    // Nuget: Autofac.Extensions.DependencyInjection
    // Autofac ekliyoruz , system.Reflection değil.
    // Asp.Net Core içinde built-in IOC container var.  tane Life Manager 3 sunarlar. AddSingleton, AddScoped, addTransient
    // 3.parti IOC contianer kullacağız. İçiçe life managment yapmak için kullanıyoruz.

    public class DependencyResolver : Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Repository Registration
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();            
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            #endregion

            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            #region Service Registration
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();

            #endregion

            // Bağımlılığa ne sebep oluyorsa burada çözüyoruz.
            // AutoMapper, FluentValidation

            // bu kısmı internetten bulup yapıştırdık. 
            // register ediyoruz. InstancePerLifetimeScope veriyor bize. her request başına bir instance
            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
