namespace Blog.Data.Models.ViewModels.Review
{

    public class ReviewEditViewModel
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public bool? TopPick { get; set; }
        public bool? SpecialOffer { get; set; }
    }
}
