namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
    using Data.ViewModels.Tool;
    using Data.ViewModels.Tag;
    using Data.ViewModels.PricingStrategy;
    using Data.Models.RequestModels.Tool;
    using Common.Constants;
    using Common.ExceptionHandlers;
    using Interfaces;
    using Repository;

    public class ToolService : Repository<Tool>, IToolService
    {
        private readonly IMapper _mapper;

        public ToolService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(ToolCreateModel toolModel, string userId)
        {
            await ValidateCreateInputAsync(toolModel.Title);

            var tool = _mapper.Map<Tool>(toolModel);

            await CreateEntityAsync(tool, userId);

            var selectedTags = _dbContext.Tags.Where(t => toolModel.AssignedTags.Contains(t.Id)).ToList();

            tool.Tags = selectedTags.Select(x => new ToolsTags
            {
                TagId = x.Id,
                ToolId = tool.Id,
            }).ToList();

            var selectedPricingStrategies = _dbContext.PricingStrategies
                .Where(t => toolModel.AssignedPricingStrategies.Contains(t.Id))
                .ToList();

            tool.PricingStrategies = selectedPricingStrategies.Select(x => new ToolsPricingStrategies
            {
                PricingStrategyId = x.Id,
                ToolId = tool.Id
            }).ToList();

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(ToolEditModel toolModel, string toolId, string modifierId)
        {
            var tool = await GetByIdAsync(toolId);

            tool.Title = toolModel.Title ?? tool.Title;
            tool.Description = toolModel.Description ?? tool.Description;
            tool.Content = toolModel.Content ?? tool.Content;
            tool.ImageUrl = toolModel.ImageUrl ?? tool.ImageUrl;
            tool.VideoUrl = toolModel.VideoUrl ?? tool.VideoUrl;
            tool.ExternalArticleUrl = toolModel.ExternalArticleUrl ?? tool.ExternalArticleUrl;
            tool.TopPick = toolModel.TopPick != tool.TopPick ? toolModel.TopPick : tool.TopPick;
            tool.SpecialOffer = toolModel.SpecialOffer != tool.SpecialOffer ? toolModel.SpecialOffer : tool.SpecialOffer;

            await SaveModificationAsync(tool, modifierId);
        }

        public async Task DeleteAsync(string toolId, string modifierId)
        {
            var tool = await GetByIdAsync(toolId);

            await DeleteEntityAsync(tool, modifierId);
        }

        public async Task<ToolViewModel> GetToolViewModelByIdAsync(string id)
        {
            var toolViewModel = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ToolViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    DownVotes = x.Votes.Where(x => !x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    VideoUrl = x.VideoUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    LastModifiedOn = x.LastModifiedOn.ToString(StringFormats.CreationDate),
                    PricingStrategies = x.PricingStrategies
                    .Select(y => y.PricingStrategy.Strategy)
                    .ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return toolViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
        }

        public async Task<ToolEditModel> GetToolEditModelByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ResourceNotFoundException(string.Format(
                                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));

            var toolEditModel = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ToolEditModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    ImageUrl = x.ImageUrl,
                    ExternalArticleUrl = x.ExternalArticleUrl,
                    VideoUrl = x.VideoUrl,
                    SpecialOffer = x.SpecialOffer,
                    TopPick = x.TopPick,
                    AssignedTags = x.Tags.Select(x => x.Tag.Value).ToList(),
                    AssignedPricingStrategies = x.PricingStrategies.Select(x => x.PricingStrategy.Strategy).ToList(),
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return toolEditModel
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
        }

        public async Task<ToolDeleteViewModel> GetToolDeleteViewModelByIdAsync(string id)
        {
            var toolDeleteViewModel = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => x.Id == id && !x.Deleted)
                .Select(x => new ToolDeleteViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Creator = x.Creator.UserName ?? "Anonymous",
                    ImageUrl = x.ImageUrl
                })
                .AsSplitQuery()
                .SingleOrDefaultAsync();

            return toolDeleteViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
        }

        public async Task<ToolsCounterViewModel> GetToolsCounterAsync(string title)
        {
            var createdToolsCount = await _dbContext.Tools.CountAsync(x => !x.Deleted);

            var toolsCountViewModel = new ToolsCounterViewModel
            {
                Title = title,
                Count = createdToolsCount,
            };

            return toolsCountViewModel;
        }

        public async Task<AssignedTagsViewModel> GetToolAssignedTagsAsync(string toolId)
        {
            var assignedTagsViewModel = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => x.Id == toolId && !x.Deleted)
                .Select(x => new AssignedTagsViewModel
                {
                     Tags = x.Tags
                    .Select(y => y.Tag.Value)
                    .ToList(),
                })
                .SingleOrDefaultAsync();

            return assignedTagsViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
        }

        public async Task<AssignedPricingStrategyViewModel> GetToolAssignedPricingStrategiesAsync(string toolId)
        {
            var assignedPricingStrategiesViewModel = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => x.Id == toolId && !x.Deleted)
                .Select(x => new AssignedPricingStrategyViewModel
                {
                    Strategies = x.PricingStrategies
                    .Select(y => y.PricingStrategy.Strategy)
                    .ToList(),
                })
                .SingleOrDefaultAsync();

            return assignedPricingStrategiesViewModel 
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
        }

        public async Task<ICollection<ToolPreviewModel>> GetToolPreviewModelBundleAsync()
        {
            var toolPreviewModelBundle = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ToolPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return toolPreviewModelBundle;
        }

        public async Task<ICollection<ToolPreviewModel>> FindToolsPreviewModelBundleAsync(string search)
        {
            var toolPreviewModelBundle = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => !x.Deleted && (x.Title.Contains(search) || x.Content.Contains(search) || x.Tags.Select(x => x.Tag.Value).Contains(search)))
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ToolPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return toolPreviewModelBundle;
        }

        public async Task<ICollection<ToolPreviewModel>> GetTodaysToolPreviewModelBundleAsync()
        {
            var currentDate = DateTime.UtcNow.Date;

            var toolLatestPreviewModelBundle = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => !x.Deleted && x.CreationDate.Date == currentDate)
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ToolPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Count(x => x.Type == true && !x.Deleted),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return toolLatestPreviewModelBundle;
        }

        public async Task<ICollection<ToolPreviewModel>> FindTodaysToolsPreviewModelBundleAsync(string search)
        {
            var currentDate = DateTime.UtcNow.Date;

            var toolPreviewModelBundle = await _dbContext.Tools
                .AsNoTracking()
                .Where(x => !x.Deleted && x.CreationDate.Date == currentDate && (x.Title.Contains(search) || x.Content.Contains(search) || x.Tags.Select(x => x.Tag.Value).Contains(search)))
                .OrderByDescending(x => x.CreationDate)
                .Select(x => new ToolPreviewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    UpVotes = x.Votes.Where(x => x.Type == true && !x.Deleted).Select(x => x.Id).Count(),
                    ImageUrl = x.ImageUrl,
                    TopPick = x.TopPick,
                    SpecialOffer = x.SpecialOffer,
                    CreationDate = x.CreationDate.ToString(StringFormats.CreationDate),
                    Tags = x.Tags
                        .Select(y => y.Tag.Value)
                        .ToList(),
                    PricingStrategies = x.PricingStrategies
                        .Select(y => y.PricingStrategy.Strategy)
                        .ToList()
                })
                .AsSplitQuery()
                .ToListAsync();

            return toolPreviewModelBundle;
        }
    }
}