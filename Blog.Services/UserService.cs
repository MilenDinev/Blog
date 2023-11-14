namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Data.Entities;
    using Data.Entities.Shared;
    using Data.Models.ViewModels.Vote;
    using Data.Models.ViewModels.Tool;
    using Managers;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;

    public class UserService : IUserService
    {
        private readonly ToolsFavoritesManager _favoriteToolsManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(ToolsFavoritesManager favoriteToolsManager, ApplicationDbContext context)
        {
            _favoriteToolsManager = favoriteToolsManager;
            _dbContext = context;
        }

        public async Task AddFavoriteToolAsync(string userId, string toolId)
        {
            await _favoriteToolsManager.AddToolAsync(userId, toolId);
        }

        public async Task RemoveFavoritesToolAsync(string userId, string toolId)
        {
            await _favoriteToolsManager.RemoveToolAsync(userId, toolId);
        }

        public async Task<bool> IsFavoriteToolAsync(string userId, string toolId)
        {
            var isFavorite = await _dbContext.Set<UsersFavoriteTools>()
                .AnyAsync(favorite => favorite.UserId == userId && favorite.ToolId == toolId);

            return isFavorite;
        }


        public async Task<ICollection<ToolPreviewModel>> GetFavoriteToolsAsync(string userId)
        {
            return await _favoriteToolsManager.GetFavoriteToolsAsync(userId);
        }

        public async Task<VoteViewModel> VoteAsync(bool type, string toolId, string userId)
        {
            // Refactoring is needed 
            var tool = await _dbContext.Tools
                .Include(r => r.Votes)
                .FirstOrDefaultAsync(r => r.Id == toolId && !r.Deleted)
                ?? throw new ResourceNotFoundException(string.Format(
                    ErrorMessages.EntityDoesNotExist, typeof(Tool).Name));

            var existingVote = tool.Votes.FirstOrDefault(v => v.UserId == userId);

            if (existingVote != null)
            {
                if (existingVote.Type == type)
                {
                    // User clicked on the same vote type again, so remove their vote
                    tool.Votes.Remove(existingVote);
                }
                else
                {
                    // User is changing their vote type
                    existingVote.Type = type;
                }
            }
            else
            {
                // The user hasn't voted before, so create a new vote
                var newVote = new Vote
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = type,
                    ToolId = tool.Id,
                    UserId = userId,
                };

                tool.Votes.Add(newVote);
            }

            await _dbContext.SaveChangesAsync();

            var upVotes = tool.Votes.Count(v => v.Type && !v.Deleted);
            var downVotes = tool.Votes.Count(v => !v.Type && !v.Deleted);

            return new VoteViewModel
            {
                UpVotes = upVotes,
                DownVotes = downVotes,
            };
        }
    }
}
