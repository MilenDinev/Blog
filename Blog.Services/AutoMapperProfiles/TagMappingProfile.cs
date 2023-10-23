namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.RequestModels.Tag;

    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            this.CreateMap<TagCreateModel, Tag>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Value.ToUpper()));
        }
    }
}
