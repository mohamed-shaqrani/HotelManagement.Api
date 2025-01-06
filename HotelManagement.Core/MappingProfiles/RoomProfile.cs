using AutoMapper;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.ViewModels.RoomViewModel;

namespace HotelManagement.Core.MappingProfiles;
public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, GetAllRoomViewModel>().ReverseMap();
        CreateMap<GetAllRoomViewModel, Room>();
        CreateMap<Room, RoomViewModel>().ReverseMap();

    }
}
