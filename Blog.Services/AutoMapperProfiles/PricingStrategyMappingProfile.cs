namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.RequestModels.PricingStrategy;

    public class PricingStrategyMappingProfile : Profile
    {
        public PricingStrategyMappingProfile()
        {
            this.CreateMap<PricingStrategyCreateModel, PricingStrategy>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Strategy.ToUpper()));
        }
    }
}
