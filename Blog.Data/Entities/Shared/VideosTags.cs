namespace Blog.Data.Entities.Shared
{
    public class VideosTags
    {
        public string VideoId { get; set; }
        public virtual Video Video { get; set; }
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
