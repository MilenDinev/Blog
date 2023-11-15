namespace Blog.Data.Entities.Shared
{
    public class ToolsNewsLetters
    {
        public string ToolId { get; set; }
        public virtual Tool Tool { get; set; }
        public string NewsLetterId { get; set; }
        public virtual NewsLetter NewsLetter { get; set; }
    }
}
