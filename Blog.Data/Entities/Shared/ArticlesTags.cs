namespace Blog.Data.Entities.Shared
{
    public class ArticlesTags
    {
        public string ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
