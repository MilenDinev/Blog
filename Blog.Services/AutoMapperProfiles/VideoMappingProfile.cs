namespace Blog.Services.AutoMapperProfiles
{
    using AutoMapper;
    using Data.Entities;
    using Data.Models.RequestModels.Video;

    public class VideoMappingProfile : Profile
    {
        public VideoMappingProfile()
        {
            CreateMap<VideoCreateModel, Video>()
                .ForMember(e => e.NormalizedTag, m => m.MapFrom(m => m.Url.ToUpper()));
        }
    }
}
