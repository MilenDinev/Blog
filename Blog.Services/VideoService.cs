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
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public VideoService(
            ITagService tagService,
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _tagService = tagService;
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

        public async Task AssignTagAsync(string videoId, string tagId, string modifierId)
        {
            var video = await GetByIdAsync(videoId);

            var isTagAlreadyAssigned = video.Tags.Any(x => x.Id == tagId);
            if (isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagAlreadyAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);
            video.Tags.Add(tag);

            await SaveModificationAsync(video, modifierId);
        }

        public async Task RemoveTag(string videoId, string tagId, string modifierId)
        {
            var video = await GetByIdAsync(videoId);

            var isTagAlreadyAssigned = video.Tags.Any(x => x.Id == tagId);
            if (!isTagAlreadyAssigned)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.TagNotAssigned, typeof(Tag).Name, tagId));

            var tag = await _tagService.GetTagByIdAsync(tagId);

            video.Tags.Remove(tag);

            await SaveModificationAsync(video, modifierId);
        }

        public async Task<VideoViewModel> GetVideoViewModelByIdAsync(string id)
        {
            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoViewModel = await _dbContext.Videos
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

            var videoPreviewModelBundle = await _dbContext.Videos
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

            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoEditViewModel = await _dbContext.Videos
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
            var isAny = await AnyByIdAsync(id);

            if (!isAny)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Review).Name));

            var videoDeleteViewModel = await _dbContext.Videos
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
