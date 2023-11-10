namespace Blog.Data.Entities.Shared
{
    public class ReviewsPricingStrategies
    {
        public string ReviewId { get; set; }
        public virtual Review Review { get; set; }
        public string PricingStrategyId { get; set; }
        public virtual PricingStrategy PricingStrategy { get; set; }
    }
}
