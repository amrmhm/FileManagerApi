using FileManager.Api.Presistence;
using FileManager.Api.Services;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace FileManager.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies (this IServiceCollection services , IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services.AddConnectionString(configuration)
            .AddFluentValidation();

        // Register Services 
        services.AddScoped<IFileServices, FileServices>();
        return services;
    }
    public static IServiceCollection AddConnectionString (this IServiceCollection services , IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(option =>
        option.UseSqlServer(connectionString));


        return services;
    }
    public static IServiceCollection AddFluentValidation (this IServiceCollection services )
    {
        services.AddFluentValidationAutoValidation()
              .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        return services;
    }
}
