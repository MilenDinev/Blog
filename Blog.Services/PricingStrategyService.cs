namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.PricingStrategy;
    using Data.Models.RequestModels.PricingStrategy;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

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
            await ValidateCreateInputAsync(pricingStrategyModel);

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

        private async Task ValidateCreateInputAsync(PricingStrategyCreateModel pricingStrategyModel)
        {
            var isAnyPricingStrategy = await AnyByStringAsync(pricingStrategyModel.Model);
            if (isAnyPricingStrategy)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(PricingStrategy).Name, pricingStrategyModel.Model));
        }
    }
}
