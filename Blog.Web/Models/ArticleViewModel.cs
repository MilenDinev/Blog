namespace Blog.Web.Models
{
    public class ArticleViewModel
    {

        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string Creator { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public string? CreationDate { get; set; }
        public string? LastModifiedOn { get; set; }
    }
}
