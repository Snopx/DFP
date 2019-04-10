using Application.AppService.ArticleApp.Dto;
using Application.AppService.UserApp.dto;
using Application.ArticleApp.Dto;
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
                .ForMember(x => x.Author, m => m.Ignore());
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
            CreateMap<UserRegisterDto, UserModel>();
        }
    }
}
