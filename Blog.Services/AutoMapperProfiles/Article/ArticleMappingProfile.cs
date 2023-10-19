namespace Blog.Services.AutoMapperProfiles.Article
{
    using AutoMapper;
    using Blog.Data.Models.ResponseModels.Article;
    using Data.Entities;
    using Data.Models.RequestModels.Article;

    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            this.CreateMap<ArticleCreateModel, Article>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Title.ToUpper()))
                .ForMember(e => e.Tags, m => m.Ignore());
            this.CreateMap<Article, CreatedArticleModel>();
            this.CreateMap<Article, ArticleListModel>();
        }
    }
}