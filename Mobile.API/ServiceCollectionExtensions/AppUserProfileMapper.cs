using AutoMapper;
using CodigoShopping.Domain.Model;
using Mobile.API.Model;

namespace Mobile.API.ServiceCollectionExtensions
{
    public class AppUserProfileMapper :Profile
    {
        public AppUserProfileMapper() {

            CreateMap<RegisterConfimRequest, AppUser>();
            CreateMap<RegisterConfimRequest, AppUser>().ReverseMap();
            CreateMap<RegisterConfirmResponse, AppUser>();
            CreateMap<RegisterConfirmResponse, AppUser>().ReverseMap();
            CreateMap<RegisterRequest,AppUser>();
            CreateMap<RegisterRequest, AppUser>().ReverseMap();
            CreateMap<RegisterResponse, AppUser>();
            CreateMap<RegisterResponse, AppUser>().ReverseMap();
            CreateMap<AppUserResponse, AppUser>();
            CreateMap<AppUserResponse, AppUser>().ReverseMap();
        }
    }
}
