using AutoMapper;
using HotelManagement.Core.Entities.UserManagement;
using HotelManagement.Core.ViewModels;
using HotelManagement.Core.ViewModels.Users;

namespace HotelManagement.Core.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>();
        CreateMap<User, UserDetailsViewModel>();
        CreateMap<UserCreateViewModel, User>();
    }
}
