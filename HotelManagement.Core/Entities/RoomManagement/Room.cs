using HotelManagement.App.Core.Entities;

namespace HotelManagement.Core.Models.RoomManagement;
public class Room : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

}
