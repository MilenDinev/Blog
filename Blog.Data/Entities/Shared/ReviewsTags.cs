namespace Blog.Data.Entities.Shared
{
    public class ReviewsTags
    {
        public string ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
