﻿namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;
    using Data.Models.RequestModels.PricingStrategy;
    using Repository;

    public class PricingStrategyService : Repository<PricingStrategy> , IPricingStrategyService
    {
        private readonly IMapper mapper;

        public PricingStrategyService(
            IMapper mapper,
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task CreateAsync(PricingStrategyCreateModel pricingStrategyModel, string userId)
        {
            await this.ValidateCreateInputAsync(pricingStrategyModel);

            var pricingStrategy = mapper.Map<PricingStrategy>(pricingStrategyModel);

            await CreateEntityAsync(pricingStrategy, userId);
        }

        public async Task EditAsync(PricingStrategyEditModel pricingStrategyModel, string pricingStrategyId, string modifierId)
        {
            var pricingStrategy = await this.GetByIdAsync(pricingStrategyId);

            pricingStrategy.Model = pricingStrategyModel.Model ?? pricingStrategy.Model;

            await SaveModificationAsync(pricingStrategy, modifierId);
        }

        public async Task DeleteAsync(string pricingStrategyId, string modifierId)
        {
            var pricingStrategy = await this.GetByIdAsync(pricingStrategyId);

            await DeleteEntityAsync(pricingStrategy, modifierId);
        }

        private async Task ValidateCreateInputAsync(PricingStrategyCreateModel pricingStrategyModel)
        {
            var isAnyPricingStrategy = await this.AnyByStringAsync(pricingStrategyModel.Model);
            if (isAnyPricingStrategy)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(PricingStrategy).Name, pricingStrategyModel.Model));
        }
    }
}
