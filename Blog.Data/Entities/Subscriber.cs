using Blog.Data.Entities.Shared;

namespace Blog.Data.Entities
{
    public class Subscriber
    {
        public Subscriber()
        {
            this.Id = Guid.NewGuid().ToString();
            this.NewsLetters = new HashSet<NewsLettersSubscribers>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public string NormalizedTag { get; set; }
        public virtual ICollection<NewsLettersSubscribers> NewsLetters { get; set; }


    }
}
