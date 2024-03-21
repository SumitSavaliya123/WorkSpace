using BusinessAccessLayer.Abstraction;
using BusinessAccessLayer.Implementation;
using Common.Constants;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Implementation;
using Entities.DTOs.Request;
using Microsoft.EntityFrameworkCore;

namespace WorkSpaceAPI.Extensions;

public static class ApplicationConfiguration
{
    public static void ConnectDatabase(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString(SystemConstants.CONNECTION_STRING_NAME)!);
        });
    }

    public static void RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtManageService, JwtManageService>();

    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(SystemConstants.CorsPolicy,
                builder => builder.WithOrigins("http://localhost:4200", "http://localhost:60561")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true; // Enable detailed errors for debugging
        }).AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.PropertyNamingPolicy = null; // Customize JSON serialization if needed
        });
    }

    public static void RegisterMail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MailSettingDto>(config.GetSection("MailSettings"));
        services.AddScoped<IMailService, MailService>();
    }

    public static void SetRequestBodySize(this IServiceCollection services)
    {
        services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = int.MaxValue;
        });
    }
    
}
