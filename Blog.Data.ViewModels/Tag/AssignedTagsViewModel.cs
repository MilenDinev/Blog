namespace Blog.Data.ViewModels.Tag
{
    public class AssignedTagsViewModel
    {
        public AssignedTagsViewModel()
        {
            this.Tags = new HashSet<string>();        
        }

        public ICollection<string> Tags { get; set; }
    }
}
