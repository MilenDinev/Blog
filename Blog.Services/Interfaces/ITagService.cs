﻿namespace Blog.Services.Interfaces
{
    using Data.ViewModels.Tag;
    using Data.Models.RequestModels.Tag;

    public interface ITagService
    {
        Task CreateAsync(TagCreateModel tagModel, string userId);
        Task<ICollection<TagViewModel>> GetTagViewModelBundleAsync();
    }
}
