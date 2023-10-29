namespace Blog.Services.Interfaces
{
    using Data.Entities;
    using Data.Models.RequestModels.Tag;

    public interface ITagService
    {
        Task CreateAsync(TagCreateModel tagModel, string userId);
        Task EditAsync(TagEditModel tagModel, string tagId, string modifierId);
        Task DeleteAsync(string tagId, string modifierId);
        Task<Tag> GetTagByIdAsync(string tagId);
        Task<bool> AnyByIdAsync(string id);
    }
}
