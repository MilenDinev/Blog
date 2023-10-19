namespace Blog.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ArticleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
    }
}
