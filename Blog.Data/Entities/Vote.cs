namespace Blog.Data.Entities
{
    public class Vote
    {
        public Vote()
        {
            this.VotedOn = DateTime.UtcNow;
            this.ChangedVoteOn = DateTime.UtcNow;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool Type { get; set; }  
        public bool Deleted { get; set; }
        public string ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime VotedOn { get; set; }
        public DateTime ChangedVoteOn { get; set; }
    }
}
