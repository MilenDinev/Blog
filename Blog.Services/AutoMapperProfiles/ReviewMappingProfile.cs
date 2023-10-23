namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.RequestModels.Review;

    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewCreateModel, Review>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Title.ToUpper()))
                .ForMember(e => e.Tags, m => m.Ignore());
        }
    }
}