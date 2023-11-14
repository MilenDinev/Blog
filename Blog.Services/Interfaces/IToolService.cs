namespace Blog.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.Models.ViewModels.Tag;
    using Data.Models.ViewModels.Tool;
    using Data.Models.RequestModels.Tool;
    using Blog.Data.Models.ViewModels.PricingStrategy;

    public interface IToolService
    {
        Task CreateAsync(ToolCreateModel toolModel, string userId);
        Task EditAsync(ToolEditModel toolModel, string toolId, string modifierId);
        Task DeleteAsync(string toolId, string modifierId);
        Task<ToolViewModel> GetToolViewModelByIdAsync(string tag);
        Task<ToolEditViewModel> GetToolEditViewModelByIdAsync(string id);
        Task<ToolDeleteViewModel> GetToolDeleteViewModelByIdAsync(string id);
        Task<ICollection<ToolPreviewModel>> GetToolPreviewModelBundleAsync();
        Task<ICollection<ToolPreviewModel>> GetTodaysToolPreviewModelBundleAsync();
        Task<CreatedToolsViewModel> GetCreatedToolsCountAsync(string title);
        Task<AssignedTagsViewModel> GetToolAssignedTagsAsync(string toolId);
        Task<AssignedPricingStrategyViewModel> GetToolAssignedPricingStrategiesAsync(string toolId);
    }
}
