namespace Blog.Services.Interfaces
{
    using Blog.Data.Models.ViewModels.Article;
    using Blog.Data.Models.ViewModels.Video;
    using Data.Models.RequestModels.Article;

    public interface IArticleService
    {
        Task CreateAsync(ArticleCreateModel articleModel, string userId);
        Task EditAsync(ArticleEditModel articleModel, string articleId, string modifierId);
        Task DeleteAsync(string articleId, string modifierId);
        Task<ICollection<ArticlePreviewModel>> GetArticlePreviewModelBundleAsync();
        Task<ArticleEditViewModel> GetArticleEditViewModelByIdAsync(string id);
        Task<ArticleDeleteViewModel> GetArticleDeleteViewModelByIdAsync(string id);
        Task<bool> AnyByIdAsync(string id);
    }
}
