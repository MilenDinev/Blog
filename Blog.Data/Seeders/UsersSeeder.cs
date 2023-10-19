namespace Blog.Data.Seeders
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Entities;

    internal static class UsersSeeder
    {
        internal static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var adminUser = new User()
            {
                UserName = "admin",
                Email = "admin@yahoo.com",
                NormalizedEmail = "admin@yahoo.com".ToUpper(),
                EmailConfirmed = true,
                NormalizedUserName = "admin".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                CreationDate = DateTime.UtcNow,
                LastModifiedOn = DateTime.UtcNow
            };

            await userManager.CreateAsync(adminUser, "adminpass");
            await userManager.AddToRoleAsync(adminUser, "admin");
        }
    }
}
