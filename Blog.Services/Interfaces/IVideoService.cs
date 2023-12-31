﻿namespace Blog.Services.Interfaces
{
    using Data.ViewModels.Video;
    using Data.Models.RequestModels.Video;

    public interface IVideoService
    {
        Task CreateAsync(VideoCreateModel videoModel, string userId);
        Task EditAsync(VideoEditModel videoModel, string videoId, string modifierId);
        Task DeleteAsync(string videoId, string modifierId);
        Task<VideoViewModel> GetVideoViewModelByIdAsync(string id);
        Task<VideoEditModel> GetVideoEditViewModelByIdAsync(string id);
        Task<VideoDeleteViewModel> GetVideoDeleteViewModelByIdAsync(string id);
        Task<ICollection<VideoPreviewModel>> GetVideoPreviewModelBundleAsync();
    }
}
