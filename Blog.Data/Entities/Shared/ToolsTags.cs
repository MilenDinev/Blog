namespace Blog.Data.Entities.Shared
{
    public class ToolsTags
    {
        public string ToolId { get; set; }
        public virtual Tool Tool { get; set; }
        public string TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
