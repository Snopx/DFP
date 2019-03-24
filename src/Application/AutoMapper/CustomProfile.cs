using System;
using System.Collections.Generic;
using System.Text;
using Application.AppService.ArticleApp.Dto;
using Application.ArticleApp;
using Application.ArticleApp.Dto;
using Application.UserApp;
using AutoMapper;
using Domain.Model;

namespace Application.AutoMapper
{
    public class CustomProfile: Profile,IProfile
    {
        public CustomProfile()
        {
            CreateMap<Article, ArticleOutputDto>();
            CreateMap<ArticleInputDto, Article>()
                .ForMember(x => x.CreateTime, m => m.Ignore())
                .ForMember(x => x.UpdateTime, m => m.Ignore())
                .ForMember(x => x.Auther, m => m.Ignore())
                .ForMember(x => x.ID, m => m.Ignore());
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
