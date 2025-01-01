using HotelManagement.Core.Entities.RoomManagement;

namespace HotelManagement.Core.ViewModels.RoomViewModel;
public class RoomViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public RoomSatus RoomSatus { get; set; }
}
