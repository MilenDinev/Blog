namespace Blog.Data.Entities
{
    public class ContactGroup
    {
        public ContactGroup()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
