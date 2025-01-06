using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Entities.UserManagement;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Entities.ReservationManagement;
public class Reservation : BaseEntity
{
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    [Required]
    public DateTime CheckInDate { get; set; }
    [Required]
    public DateTime CheckOutDate => CheckInDate.AddDays(NumberOfDays);
    public int NumberOfDays { get; set; }

    public decimal Price { get; set; }

}
