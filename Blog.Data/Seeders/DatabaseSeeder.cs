namespace Blog.Data.Seeders
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Entities;

    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder 
    {
        public async static Task SeedAsync(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
                {
                    await context.Database.MigrateAsync();
                }

            }
        }
    }
}
