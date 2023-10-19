namespace Blog.Web.ConfigExtensions
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using Services.Interfaces;
    using Services;
    using Constants;
    using Blog.Services.Repository.Interfaces;
    using Blog.Services.Repository;
    using Microsoft.AspNetCore.Identity;
    using Blog.Data.Entities;

    public static class ServicesRegistrator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(AutoMapperAssemblyPath.Assembly));
            services.AddTransient<UserManager<User>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IFinder, Finder>();
        }
    }
}
