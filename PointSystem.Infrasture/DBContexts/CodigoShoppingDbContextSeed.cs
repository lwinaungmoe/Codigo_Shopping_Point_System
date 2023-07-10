using CodigoShopping.Domain.Model;
using Microsoft.Extensions.Logging;

namespace CodigoShopping.Infrastructure.DBContexts
{
    public class CodigoShoppingDbContextSeed
    {
        public async Task SeedAsync(CodigoShoppingDbContext context, ILogger<CodigoShoppingDbContextSeed> logger)
        {
            context.Database.EnsureCreated();

            if (!context.CatalogTypes.Any())
            {
                await context.CatalogTypes.AddRangeAsync(
                       GetPreconfiguredCatalogTypes());

                await context.SaveChangesAsync();
            }

            if (!context.CatalogItems.Any())
            {
                await context.CatalogItems.AddRangeAsync(

                 GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }

            if (!context.PointSetting.Any())
            {
                await context.PointSetting.AddRangeAsync(

                   GetPointSettings());

                await context.SaveChangesAsync();
            }

            //await context.AppUsers.AddRangeAsync(

            //  GetAppUser());

            //await context.SaveChangesAsync();
        }

        private IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
        {
            new() { Type = "Alcohol"},
            new() { Type = "Water" },
            new() { Type = "Soda" },
        };
        }

        private IEnumerable<PointSetting> GetPointSettings()
        {
            return new List<PointSetting>()
        {
            new() { Description = "NonAlcohol",  Name="NonAlcoholPoint", PointAmount = 20.0M, PointMaxScore=500},
        };
        }

        private IEnumerable<AppUser> GetAppUser()
        {
            return new List<AppUser>()
        {
            new() {
                IsConfirmMobileNumber = true,
                Email="keyjohn1234@gmail.com",
                PasswordHash="$11$96yGE.0Ymp8d7jdRE7hWn.m9OGHv30qBHJqIS9wXafB4VbbvMNa3q" ,// 12345,
                OTPExpirayDateTIme=DateTime.Now.AddDays(1),
                OtpCode="182047",
                DeviceId="1",
                RegisterDateTime=DateTime.Now,
                IsDeleted=false,
                MobileNumber="+959448946017",
                UserName="lwinaungmoe"
            },
        };
        }

        private IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
        {
            new() { CatalogTypeId = 2,  AvailableStock = 100, Description = "Classic Water", Name = "Classic Water", Price = 19.5M, PictureFileName = "1.png" },
            new() { CatalogTypeId = 1,  AvailableStock = 100, Description = "Myanmar Beer", Name = "Myanmar Beer", Price= 8.50M, PictureFileName = "2.png" },
            new() { CatalogTypeId = 2,  AvailableStock = 100, Description = "PMG Water", Name = "PMG Water", Price = 12, PictureFileName = "3.png" },
            new() { CatalogTypeId = 3,  AvailableStock = 100, Description = "Coca Cola", Name = "Coca Cola", Price = 12, PictureFileName = "4.png" },
            new() { CatalogTypeId = 3, AvailableStock = 100, Description = "Red Bull", Name = "Red Bull", Price = 8.5M, PictureFileName = "5.png" },
        };
        }
    }
}