namespace Blog.Data.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Entities;

    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider applicationServices)
        {
            if (applicationServices == null)
            {
                throw new ArgumentNullException(nameof(applicationServices));
            }

            using (var serviceScope = applicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                await MigrateDatabaseAsync(context);

                if (!await context.Users.AnyAsync())
                {
                    await SeedDatabaseAsync(roleManager, userManager, context);
                }
            }
        }

        private static async Task MigrateDatabaseAsync(ApplicationDbContext context)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }
        }

        private static async Task SeedDatabaseAsync(RoleManager<IdentityRole<string>> roleManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            await RolesSeeder.SeedRolesAsync(roleManager);
            await UsersSeeder.SeedUsersAsync(userManager);
            await PricingStrategiesSeeder.SeedAsync(context);
            await TagsSeeder.SeedAsync(context);
        }
    }
}
