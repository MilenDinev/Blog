namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.ViewModels;
    using Data.ViewModels.Video;
    using Data.Models.RequestModels.Video;
    using Common.Constants;
    using Common.ExceptionHandlers;
    using Interfaces;
    using Repository;

    public class VideoService : Repository<Video>, IVideoService
    {
        private readonly IMapper _mapper;

        public VideoService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(VideoCreateModel videoModel, string userId)
        {
            await ValidateCreateInputAsync(videoModel);

            var video = _mapper.Map<Video>(videoModel);

            await CreateEntityAsync(video, userId);
        }

        public async Task EditAsync(VideoEditModel videoModel, string videoId, string modifierId)
        {
            var video = await GetByIdAsync(videoId);

            video.Title = videoModel.Title ?? video.Title;
            video.Url = videoModel.Url ?? video.Url;
            video.ImageUrl = videoModel.ImageUrl ?? video.ImageUrl;
            video.RelatedToolName = videoModel.RelatedToolName ?? video.RelatedToolName;
            video.RelatedToolUrl = videoModel.RelatedToolUrl ?? video.RelatedToolUrl;

            await SaveModificationAsync(video, modifierId);
        }

        public async Task DeleteAsync(string videoId, string modifierId)
        {
            var video = await GetByIdAsync(videoId);

            await DeleteEntityAsync(video, modifierId);
        }

        private async Task ValidateCreateInputAsync(VideoCreateModel videoModel)
        {
            var isAnyVideo = await AnyByStringAsync(videoModel.Url);
            if (isAnyVideo)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Video).Name, videoModel.Url));
        }

        public async Task<VideoViewModel> GetVideoViewModelByIdAsync(string id)
        {
            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));

            var videoViewModel = await _dbContext.Videos
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new VideoViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    UploadDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Url = x.Url,
                    Contacts = new ContactsViewModel
                    {
                        Youtube = Contacts.Youtube,
                        X = Contacts.X, 
                        Discord = Contacts.Discord,
                        Email = Contacts.Email,
                    }
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return videoViewModel;
        }

        public async Task<ICollection<VideoPreviewModel>> GetVideoPreviewModelBundleAsync()
        {

            var videoPreviewModelBundle = await _dbContext.Videos
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new VideoPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    UploadDate = x.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Url = x.Url
                })
                .ToListAsync();

            return videoPreviewModelBundle;
        }

        public async Task<VideoEditModel> GetVideoEditViewModelByIdAsync(string id)
        {

            var videoEditViewModel = await _dbContext.Videos
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new VideoEditModel
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Url = x.Url
            })
            .SingleOrDefaultAsync();

            return videoEditViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Video).Name));
        }

        public async Task<VideoDeleteViewModel> GetVideoDeleteViewModelByIdAsync(string id)
        {

            var videoDeleteViewModel = await _dbContext.Videos
            .AsNoTracking()
            .Where(x => x.Id == id && !x.Deleted)
            .Select(x => new VideoDeleteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Url = x.Url
            })
            .SingleOrDefaultAsync();

            return videoDeleteViewModel
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Video).Name));
        }
    }
}
