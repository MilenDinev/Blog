namespace Blog.Data.Models.ResponseModels.Article
{
    public class EditedArticleModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
    }
}
