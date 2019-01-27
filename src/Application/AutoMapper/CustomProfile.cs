using System;
using System.Collections.Generic;
using System.Text;
using Application.ArticleApp;
using Application.UserApp;
using AutoMapper;
using Domain.Model;

namespace Application.AutoMapper
{
    public class CustomProfile: Profile,IProfile
    {
        public CustomProfile()
        {
            CreateMap<Article, ArticleDtos>();
            CreateMap<ArticleDtos, Article>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
