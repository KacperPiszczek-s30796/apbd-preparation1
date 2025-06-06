﻿using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories.extensions;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProduct_WarehouseRepository, Product_WarehouseRepository>();

        return services;
    }
}