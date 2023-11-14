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
                        Strategy = "Paid",
                        NormalizedTag = "Paid".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Strategy = "Free",
                        NormalizedTag = "Free".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Strategy = "Freemium",
                        NormalizedTag = "Freemium".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Strategy = "Premium",
                        NormalizedTag = "Premium".ToUpper(),
                        CreatorId = creator.Id,
                        LastModifierId = creator.Id,
                        CreationDate = DateTime.UtcNow,
                        LastModifiedOn = DateTime.UtcNow
                    },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Strategy = "Free Trial",
                    NormalizedTag = "FreeTrial".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Strategy = "GitHub",
                    NormalizedTag = "GitHub".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Strategy = "Google Colab",
                    NormalizedTag = "GoogleColab".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new PricingStrategy()
                {
                    Id = Guid.NewGuid().ToString(),
                    Strategy = "Open Source",
                    NormalizedTag = "OpenSource".ToUpper(),
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
