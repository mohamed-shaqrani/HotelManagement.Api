using HotelManagement.Core.Entities;

namespace HotelManagement.Core.Entities.RoomManagement;
public class RoomType : BaseEntity
{
    public decimal Price { get; set; }
    public string Name { get; set; }

}
