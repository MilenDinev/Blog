namespace Blog.Data.Entities
{
    public class Contact
    {
        public Contact()
        {
            this.ContactGroup = new HashSet<ContactGroup>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }

        public virtual ICollection<ContactGroup> ContactGroup { get; set; }
    }
}
