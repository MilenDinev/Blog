namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.RequestModels.Article;

    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<ArticleCreateModel, Article>()
            .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Url.ToUpper()));
        }
    }
}
