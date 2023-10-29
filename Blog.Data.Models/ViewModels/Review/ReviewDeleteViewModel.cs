namespace Blog.Data.Models.ViewModels.Review
{

    public class ReviewDeleteViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Creator { get; set; }
        public string? CreationDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? ExternalArticleUrl { get; set; }
        public bool? TopPick { get; set; }
        public bool? SpecialOffer { get; set; }
    }
}
