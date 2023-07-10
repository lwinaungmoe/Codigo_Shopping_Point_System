using CodigoShopping.Infrastructure.DomainRepository;

namespace PointSystem.API.Services.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IShoppingTransactionRepository, ShoppingTransactionRepository>();
            services.AddScoped<IPointDataRepository, PointDataRepository>();
            services.AddScoped<ICalcuatePointSystemService, CalculatePointSystemService>();

            return services;
        }
    }
}