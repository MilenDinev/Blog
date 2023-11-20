namespace Blog.Data.ViewModels.Video
{
    public class VideoViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? UploadDate { get; set; }
        public string? RelatedToolName { get; set; }
        public string? RelatedToolUrl { get; set; }
        public ContactsViewModel Contacts { get; set; }
    }
}
