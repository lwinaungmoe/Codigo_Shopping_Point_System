using CodigoShopping.Infrastructure.DomainRepository;
using Mobile.API.Caching;
using Mobile.API.Helper;
using Mobile.API.Services;
using System.Runtime.CompilerServices;

namespace Mobile.API.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICatelogTypeServices, CatelogTypeService>();
            services.AddScoped<ICatelogItemsRepository, CatelogItemsRepository>();
            services.AddScoped<ICatelogTypeRepository, CatelogTypeRepository>();
            services.AddScoped<ICatelogItemsService, CatelogItemsService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IHelperClass, HelperClass>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ICheckOutService, CheckOutService>();
            services.AddScoped<IShoppingTransactionDetailsRepository, ShoppingTransactionDetailsRepository>();
            services.AddScoped<IShoppingTransactionRepository, ShoppingTransactionRepository>();
            services.AddScoped<IPointDataRepository, PointDataRepository>();
            services.AddScoped<IPointSettingRepository, PointSettingRepository>();
            services.AddScoped<ICacheService,CacheService>();
            services.AddScoped<IPointSystemService, PointSystemService>();
            return services;

        }

        
    }
}
