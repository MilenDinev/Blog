namespace Blog.Data.Entities.Shared
{
    public class NewsLettersSubscribers
    {
        public string NewsLetterId { get; set; }
        public virtual NewsLetter NewsLetter { get; set; }
        public string SubscriberId { get; set; }
        public virtual Subscriber Subscriber { get; set; }
    }
}
