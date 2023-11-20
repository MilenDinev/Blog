namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
    using Data.ViewModels.Tool;
    using Common.Constants;
    using Common.ExceptionHandlers;

    public class ToolsFavoritesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ToolsFavoritesService(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddToolAsync(string userId, string toolId)
        {
            var isFavorite = await _dbContext.Set<UsersFavoriteTools>()
                .AnyAsync(favorite => favorite.UserId == userId && favorite.ToolId == toolId);

            if (!isFavorite)
            {
                var userFavoriteTool = new UsersFavoriteTools
                {
                    UserId = userId,
                    ToolId = toolId
                };

                await _dbContext.AddAsync(userFavoriteTool);
                await _dbContext.SaveChangesAsync();
            }

            else
            {
                var isToolExists = await _dbContext.Tools.AnyAsync(r => r.Id == toolId);
                if (!isToolExists)
                {
                    throw new ResourceNotFoundException(string.Format(
                        ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));
                }
            }
        }

        public async Task RemoveToolAsync(string userId, string toolId)
        {
            var userFavoriteTool = _dbContext.Set<UsersFavoriteTools>().FirstOrDefault(x => x.UserId == userId && x.ToolId == toolId);

            if (userFavoriteTool == null)
                throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));

            _dbContext.Remove(userFavoriteTool);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ToolPreviewModel>> GetFavoriteToolsAsync(string userId)
        {
            var favoriteToolsModel = await _dbContext.Set<UsersFavoriteTools>()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new ToolPreviewModel
                {
                    Id = x.ToolId,
                    Title = x.Tool.Title,
                    ImageUrl = x.Tool.ImageUrl,
                    TopPick = x.Tool.TopPick,
                    SpecialOffer = x.Tool.SpecialOffer,
                    Description = x.Tool.Description,
                    CreationDate = x.Tool.CreationDate.ToString("dd MMMM hh:mm tt"),
                    Tags = x.Tool.Tags
                        .Where(x => !x.Tag.Deleted)
                        .Select(x => x.Tag.Value).ToList(),
                    PricingStrategies = x.Tool.PricingStrategies
                        .Where(x => !x.PricingStrategy.Deleted)
                        .Select(x => x.PricingStrategy.Strategy).ToList(),
                    UpVotes = x.Tool.Votes
                        .Count(x => x.Type == true && !x.Deleted)
                })
                .AsSplitQuery()
                .ToListAsync();

            return favoriteToolsModel;
        }
    }
}
