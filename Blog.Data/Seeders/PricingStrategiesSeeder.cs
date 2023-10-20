namespace Blog.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    internal class PricingStrategiesSeeder
    {
        internal static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            var creator = await dbContext.Users.SingleOrDefaultAsync();

            var pricingStrategies = new List<PricingStrategy>()
            {

                new PricingStrategy()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Model = "paid",
                        NormalizedTag = "paid".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Model = "frepremium",
                        NormalizedTag = "frepremium".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Model = "trialFree",
                    NormalizedTag = "trialFree".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Model = "gitHub",
                    NormalizedTag = "gitHub".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Model = "googleColab",
                    NormalizedTag = "googleColab".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Model = "paid",
                    NormalizedTag = "paid".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                }
            };

            await dbContext.PricingStrategies.AddRangeAsync(pricingStrategies);
            await dbContext.SaveChangesAsync();
        }
    }
}
