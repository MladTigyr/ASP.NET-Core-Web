using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Services;
using Notebook.Services.Interfaces;

namespace Notebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? connectionString = builder.Configuration.GetConnectionString("SqlDevConnection") ?? throw new InvalidOperationException("Connection string 'SqlDevConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<INoteService, NoteService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                PasswordIdentity(options, builder);
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
        private static void PasswordIdentity(IdentityOptions options, WebApplicationBuilder builder)
        {
            options.SignIn.RequireConfirmedAccount = 
                builder.Configuration.GetValue<bool>("IdentitySettings:SignIn:RequireConfirmedAccount");

            options.User.RequireUniqueEmail =
                builder.Configuration.GetValue<bool>("IdentitySettings:User:RequireUniqueEmail");

            options.Password.RequiredLength =
                builder.Configuration.GetValue<int>("IdentitySettings:Password:RequiredLength");
            options.Password.RequireDigit =
                builder.Configuration.GetValue<bool>("IdentitySettings:Password:RequireDigit");
            options.Password.RequireNonAlphanumeric =
                builder.Configuration.GetValue<bool>("IdentitySettings:Password:RequireNonAlphanumeric");
            options.Password.RequireUppercase =
                builder.Configuration.GetValue<bool>("IdentitySettings:Password:RequireUppercase");
            options.Password.RequireLowercase =
                builder.Configuration.GetValue<bool>("IdentitySettings:Password:RequireLowercase");
        }
    }
}
