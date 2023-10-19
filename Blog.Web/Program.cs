using Blog.Data.Seeders;
using Blog.Web.ConfigExtensions;
using Blog.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity();
builder.Services.AddControllersWithViews();

var app = builder.Build();

DatabaseSeeder.SeedAsync(app.Services).GetAwaiter().GetResult();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ErrorHandler>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
