namespace Blog.Data.Entities.Shared
{
    public class ToolsPricingStrategies
    {
        public string ToolId { get; set; }
        public virtual Tool Tool { get; set; }
        public string PricingStrategyId { get; set; }
        public virtual PricingStrategy PricingStrategy { get; set; }
    }
}
