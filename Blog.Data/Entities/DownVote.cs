namespace Blog.Data.Entities
{
    public class DownVote
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }
        public string ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
