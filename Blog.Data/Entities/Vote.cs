namespace Blog.Data.Entities
{
    public class Vote
    {
        public Vote()
        {
            this.Id = Guid.NewGuid().ToString();
            this.VotedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public bool Type { get; set; }  
        public bool Deleted { get; set; }
        public string ToolId { get; set; }
        public virtual Tool Tool { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime VotedOn { get; set; }
    }
}
