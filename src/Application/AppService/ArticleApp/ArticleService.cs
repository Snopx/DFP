﻿using Application.ArticleApp.Dto;
using AutoMapper;
using Domain.Base;
using Domain.Interface;
using Domain.Model;
using Domain.QueryParameterFolder;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Extension;

namespace Application.ArticleApp
{
    public class ArticleService : BaseService<Article, IRepository<Article>>, IArticleService
    {
        private readonly IMapper _mapper;
        public ArticleService(IRepository<Article> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<PaginatedList<ArticleOutputDto>> GetPageEntitys(ArticleParameter queryParameters)
        {
            var query = Table;

            query.WhereIf(!string.IsNullOrWhiteSpace(queryParameters.Title), x => x.Title.Contains(queryParameters.Title, System.StringComparison.OrdinalIgnoreCase));

            query.WhereIf(queryParameters.ArticleType.HasValue, x => x.ArticleType == queryParameters.ArticleType);

            var count = await query.CountAsync();
            var articles = await query.PageBy(queryParameters).ToListAsync();

            var data = _mapper.Map<List<ArticleOutputDto>>(articles);

            return new PaginatedList<ArticleOutputDto>(queryParameters.PageIndex, queryParameters.PageSize, count, data);
        }

        public async Task<List<ArticleOutputDto>> GetTop5()
        {
            var result = await Table.OrderByDescending(x=>x.CreateTime).Take(5).ToListAsync();
            return _mapper.Map<List<ArticleOutputDto>>(result);
        }
    }
}
