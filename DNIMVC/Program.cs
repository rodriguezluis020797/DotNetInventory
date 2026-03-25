using DNIMVC.Models.Identity;
using DNIMVC.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DNIMVC;

public class Program
{
    // Base path used for configuration file resolution.
    // AppContext.BaseDirectory ensures the correct path is used
    // regardless of the working directory at runtime.
    private static readonly string basePath = AppContext.BaseDirectory;

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureConfiguration(builder);
        ConfigureServices(builder);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }

    /// <summary>
    ///     Configures the application's configuration sources.
    ///     Loads appsettings.json first, then the environment-specific override file
    ///     (e.g. appsettings.Development.json), then environment variables.
    ///     Each source overrides the previous, allowing environment-specific
    ///     values to override defaults without modifying the base config file.
    /// </summary>
    private static void ConfigureConfiguration(WebApplicationBuilder builder)
    {
        builder.Configuration
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
            .AddEnvironmentVariables();
        Console.WriteLine(builder.Configuration.GetConnectionString("IdentityConnectionString"));
    }

    /// <summary>
    ///     Registers all application services with the dependency injection container.
    /// </summary>
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();

        // Register the Identity database context using SQL Server.
        // MigrationsAssembly specifies where EF Core should look for
        // migration files, which must match the project name.
        builder.Services.AddDbContext<IdentityDataService>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("IdentityConnectionString"),
                sql => sql.MigrationsAssembly("DNIMVC")
            ));

        // Register ASP.NET Core Identity with custom password and lockout policies.
        // Identity handles password hashing, user management, and role management.
        // AddEntityFrameworkStores wires Identity to the IdentityDataService DbContext.
        // AddDefaultTokenProviders enables email confirmation and password reset tokens.
        builder.Services.AddIdentity<ApplicationUserModel, IdentityRole<Guid>>(options =>
            {
                // Password requirements
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;

                // Lock the account for 15 minutes after 5 consecutive failed attempts
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                // Prevent duplicate accounts by enforcing unique emails
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<IdentityDataService>()
            .AddDefaultTokenProviders();
    }
}