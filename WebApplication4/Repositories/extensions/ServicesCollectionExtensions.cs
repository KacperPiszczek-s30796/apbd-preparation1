using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories.extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}