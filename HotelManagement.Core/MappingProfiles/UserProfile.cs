using AutoMapper;
using HotelManagement.App.Core.Entities;
using HotelManagement.App.Core.ViewModels;
using HotelManagement.App.Core.ViewModels.Users;

namespace HotelManagement.App.Core.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserDetailsViewModel>();
        CreateMap<UserCreateViewModel, User>();
    }
}
