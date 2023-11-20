namespace Blog.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.ViewModels.Tag;
    using Data.ViewModels.Tool;
    using Data.Models.RequestModels.Tool;
    using Data.ViewModels.PricingStrategy;

    public interface IToolService
    {
        Task CreateAsync(ToolCreateModel toolModel, string userId);
        Task EditAsync(ToolEditModel toolModel, string toolId, string modifierId);
        Task DeleteAsync(string toolId, string modifierId);
        Task<ToolViewModel> GetToolViewModelByIdAsync(string tag);
        Task<ToolEditModel> GetToolEditModelByIdAsync(string id);
        Task<ToolDeleteViewModel> GetToolDeleteViewModelByIdAsync(string id);
        Task<ICollection<ToolPreviewModel>> GetToolPreviewModelBundleAsync();
        Task<ICollection<ToolPreviewModel>> FindToolsPreviewModelBundleAsync(string search);
        Task<ICollection<ToolPreviewModel>> GetTodaysToolPreviewModelBundleAsync();
        Task<ICollection<ToolPreviewModel>> FindTodaysToolsPreviewModelBundleAsync(string search);

        Task<ToolsCounterViewModel> GetToolsCounterAsync(string title);
        Task<AssignedTagsViewModel> GetToolAssignedTagsAsync(string toolId);
        Task<AssignedPricingStrategyViewModel> GetToolAssignedPricingStrategiesAsync(string toolId);
    }
}
