﻿namespace Blog.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Models.ViewModels.Review;
    using Data.Models.RequestModels.Review;

    public interface IReviewService
    {
        Task CreateAsync(ReviewCreateModel reviewModel, string userId);
        Task EditAsync(ReviewEditModel reviewModel, string reviewId, string modifierId);
        Task DeleteAsync(string reviewId, string modifierId);
        Task<ReviewViewModel> GetReviewViewModelByIdAsync(string tag);
        Task<ReviewEditViewModel> GetReviewEditViewModelByIdAsync(string id);
        Task<ReviewDeleteViewModel> GetReviewDeleteViewModelByIdAsync(string id);
        Task<ICollection<ReviewPreviewModel>> GetReviewPreviewModelBundleAsync();
        Task<ICollection<ReviewPreviewModel>> GetTodaysReviewPreviewModelBundleAsync();
    }
}
