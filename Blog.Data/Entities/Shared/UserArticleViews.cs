namespace Blog.Data.Entities.Shared
{
    public class UserArticleViews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int Counter { get; set; }
    }
}
