namespace Blog.Data.Models.ViewModels.Review
{
    public class ReviewPreviewModel
    {
        public ReviewPreviewModel()
        {
            this.Tags = new HashSet<string>();
            this.PricingStrategies = new HashSet<string>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UpVotes { get; set; }
        public string? ImageUrl { get; set; }
        public bool TopPick { get; set; }
        public bool SpecialOffer { get; set; }
        public string Creator { get; set; }
        public string CreationDate { get; set; }
        public virtual ICollection<string> Tags { get; set; }
        public virtual ICollection<string> PricingStrategies { get; set; }
    }
}
