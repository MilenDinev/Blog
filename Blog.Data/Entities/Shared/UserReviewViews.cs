namespace Blog.Data.Entities.Shared
{
    public class UserReviewViews
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int Counter { get; set; }
    }
}
