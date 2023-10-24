namespace Blog.Data.Entities.Shared
{
    public class UserVideoViews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string VideoId { get; set; }
        public virtual Video Video { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int Counter { get; set; }
    }
}
