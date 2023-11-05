namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.PricingStrategy;
    using Data.Models.RequestModels.PricingStrategy;
    using Interfaces;
    using Repository;

    public class PricingStrategyService : Repository<PricingStrategy> , IPricingStrategyService
    {
        private readonly IMapper _mapper;

        public PricingStrategyService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(PricingStrategyCreateModel pricingStrategyModel, string userId)
        {
            await ValidateCreateInputAsync(pricingStrategyModel.Strategy);

            var pricingStrategy = _mapper.Map<PricingStrategy>(pricingStrategyModel);

            await CreateEntityAsync(pricingStrategy, userId);
        }

        public async Task EditAsync(PricingStrategyEditModel pricingStrategyModel, string pricingStrategyId, string modifierId)
        {
            var pricingStrategy = await GetByIdAsync(pricingStrategyId);

            pricingStrategy.Strategy = pricingStrategyModel.Model ?? pricingStrategy.Strategy;

            await SaveModificationAsync(pricingStrategy, modifierId);
        }

        public async Task DeleteAsync(string pricingStrategyId, string modifierId)
        {
            var pricingStrategy = await GetByIdAsync(pricingStrategyId);

            await DeleteEntityAsync(pricingStrategy, modifierId);
        }

        public async Task<ICollection<PricingStrategyViewModel>> GetPricingStrategyViewModelBundleAsync()
        {
            var pricingStrategyViewModelBundle = await _dbContext.PricingStrategies
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new PricingStrategyViewModel
                {
                    Id = x.Id,
                    Strategy = x.Strategy,
                })
                .OrderBy(x => x.Strategy)
                .ToListAsync();

            return pricingStrategyViewModelBundle;
        }
    }
}
