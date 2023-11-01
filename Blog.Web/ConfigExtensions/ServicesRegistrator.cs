namespace Blog.Web.ConfigExtensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using Data.Entities;
    using Services;
    using Services.Interfaces;
    using Constants;

    public static class ServicesRegistrator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(AutoMapperAssemblyPath.Assembly));
            services.AddScoped<UserManager<User>>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPricingStrategyService, PricingStrategyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IArticleService, ArticleService>();
        }
    }
}
