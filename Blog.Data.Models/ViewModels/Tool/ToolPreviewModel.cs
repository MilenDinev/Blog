namespace Blog.Data.Models.ViewModels.Tool
{
    public class ToolPreviewModel
    {
        public ToolPreviewModel()
        {
            this.Tags = new HashSet<string>();
            this.PricingStrategies = new HashSet<string>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int UpVotes { get; set; }
        public bool TopPick { get; set; }
        public bool SpecialOffer { get; set; }
        public string CreationDate { get; set; }
        public ICollection<string> Tags { get; set; }
        public ICollection<string> PricingStrategies { get; set; }
    }
}
