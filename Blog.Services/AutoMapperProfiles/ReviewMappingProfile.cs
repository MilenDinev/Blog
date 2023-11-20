namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.ViewModels.Tool;
    using Data.Models.RequestModels.Tool;

    public class ToolMappingProfile : Profile
    {
        public ToolMappingProfile()
        {
            CreateMap<ToolCreateModel, Tool>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Title.ToUpper()))
                .ForMember(e => e.TopPick, m => m.MapFrom(m => m.TopPick))
                .ForMember(e => e.SpecialOffer, m => m.MapFrom(m => m.SpecialOffer))
                .ForMember(e => e.Tags, m => m.Ignore());
            CreateMap<Tool, ToolPreviewModel>()
                .ForMember(m => m.UpVotes, e => e.MapFrom(e => e.Votes.Count(x => x.Type == true && !x.Deleted)))
                .ForMember(m => m.CreationDate, e => e.MapFrom(e => e.CreationDate.ToString("dd MMMM hh:mm tt")))
                .ForMember(m => m.Tags, e => e.MapFrom(e => e.Tags.Select(t => t.Tag.Value).ToList()))
                .ForMember(m => m.PricingStrategies, e => e.MapFrom(e => e.PricingStrategies.Select(t => t.PricingStrategy.Strategy).ToList()));

        }
    }
}