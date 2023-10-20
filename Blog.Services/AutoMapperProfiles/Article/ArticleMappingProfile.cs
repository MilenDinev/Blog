namespace Blog.Services.AutoMapperProfiles.Article
{
    using AutoMapper;
    using Data.Models.ResponseModels.Article;
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
            this.CreateMap<Article, ArticleListModel>()
                .ForMember(e => e.Creator, m => m.MapFrom(m => m.Creator.UserName))
                .ForMember(e => e.CreationDate, m => m.MapFrom(m => m.CreationDate.ToString("dddd MMM hh:mm tt")))
                .ForMember(e => e.LastModifiedOn, m => m.MapFrom(m => m.LastModifiedOn.ToString("dddd MMM hh:mm tt")));
        }
    }
}