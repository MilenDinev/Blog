namespace Blog.Data.Seeders
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider applicationServices)
        {
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
