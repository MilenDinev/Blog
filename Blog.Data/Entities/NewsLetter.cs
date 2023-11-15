using Blog.Data.Entities.Shared;

namespace Blog.Data.Entities
{
    public class NewsLetter
    {
        public NewsLetter()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Subscribers = new HashSet<NewsLettersSubscribers>();
            this.Tools = new HashSet<ToolsNewsLetters>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ToolsNewsLetters> Tools { get; set; }
        public virtual ICollection<NewsLettersSubscribers> Subscribers { get; set; }
    }
}
