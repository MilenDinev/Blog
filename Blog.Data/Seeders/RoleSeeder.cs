namespace Blog.Data.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    internal static class RolesSeeder
    {
        internal static async Task SeedRolesAsync(RoleManager<IdentityRole<string>> roleManager)
        {
            var adminRole = new IdentityRole<string>()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            await roleManager.CreateAsync(adminRole);

            var regularRole = new IdentityRole<string>()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "regular",
                NormalizedName = "regular".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };
            await roleManager.CreateAsync(regularRole);
        }
    }
}
