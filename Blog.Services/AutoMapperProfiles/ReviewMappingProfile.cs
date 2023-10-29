namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.ViewModels.Review;
    using Data.Models.RequestModels.Review;

    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewCreateModel, Review>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Title.ToUpper()))
                .ForMember(e => e.Tags, m => m.Ignore());
            CreateMap<Review, ReviewPreviewModel>()
                .ForMember(m => m.UpVotes, e => e.MapFrom(e => e.Votes.Count(x => x.Type == true && !x.Deleted)))
                .ForMember(m => m.CreationDate, e => e.MapFrom(e => e.CreationDate.ToString("dd MMMM hh:mm tt")))
                .ForMember(m => m.Tags, e => e.MapFrom(e => e.Tags.Select(t => t.Value).ToList()));

        }
    }
}