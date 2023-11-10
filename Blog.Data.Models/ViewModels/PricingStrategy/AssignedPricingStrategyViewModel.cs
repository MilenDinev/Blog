namespace Blog.Data.Models.ViewModels.PricingStrategy
{
    public class AssignedPricingStrategyViewModel
    {
        public AssignedPricingStrategyViewModel()
        {
            this.Strategies = new HashSet<string>();
        }

        public ICollection<string> Strategies { get; set; }
    }
   
}
