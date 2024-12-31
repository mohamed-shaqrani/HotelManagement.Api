namespace HotelManagement.Core.Entities.RoomManagement;
public class Room : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public RoomSatus RoomStatus { get; set; }
    public ICollection<RoomImage> RoomImages { get; set; } = [];

}
