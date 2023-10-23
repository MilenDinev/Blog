namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Video;
    using Data.Models.RequestModels.Video;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

    public class VideoService : Repository<Video>, IVideoService
    {
        private readonly IMapper mapper;

        public VideoService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task CreateAsync(VideoCreateModel videoModel, string userId)
        {
            await this.ValidateCreateInputAsync(videoModel);

            var video = mapper.Map<Video>(videoModel);

            await CreateEntityAsync(video, userId);
        }

        public async Task EditAsync(VideoEditModel videoModel, string videoId, string modifierId)
        {
            var video = await this.GetByIdAsync(videoId);

            video.Title = videoModel.Title ?? video.Title;
            video.Url = videoModel.Url ?? video.Url;
            video.ImageUrl = videoModel.ImageUrl ?? video.ImageUrl;

            await SaveModificationAsync(video, modifierId);
        }

        public async Task DeleteAsync(string videoId, string modifierId)
        {
            var video = await this.GetByIdAsync(videoId);

            await DeleteEntityAsync(video, modifierId);
        }

        private async Task ValidateCreateInputAsync(VideoCreateModel videoModel)
        {
            var isAnyVideo = await this.AnyByStringAsync(videoModel.Url);
            if (isAnyVideo)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Video).Name, videoModel.Url));
        }

        public async Task<VideoViewModel> GetVideoViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoViewModel = await this.dbContext.Videos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new VideoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    UploadDate = x.CreationDate.ToString("dd/MM/yyyy"),
                    Url = x.Url
                }).SingleOrDefaultAsync();

            return videoViewModel;
        }

        public async Task<ICollection<VideoPreviewModel>> GetVideoPreviewModelBundleAsync()
        {

            var videoPreviewModelBundle = await this.dbContext.Videos
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new VideoPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    UploadDate = x.CreationDate.ToString("dd/MM/yyyy"),
                    Url = x.Url
                }).ToListAsync();

            return videoPreviewModelBundle;
        }

        public async Task<VideoEditViewModel> GetVideoEditViewModelByIdAsync(string id)
        {

            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoEditViewModel = await this.dbContext.Videos
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new VideoEditViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Url = x.Url
            }).SingleOrDefaultAsync();

            return videoEditViewModel;
        }

        public async Task<VideoDeleteViewModel> GetVideoDeleteViewModelByIdAsync(string id)
        {
            var isAny = await this.AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoDeleteViewModel = await this.dbContext.Videos
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new VideoDeleteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Url = x.Url
            }).SingleOrDefaultAsync();

            return videoDeleteViewModel;
        }
    }
}
