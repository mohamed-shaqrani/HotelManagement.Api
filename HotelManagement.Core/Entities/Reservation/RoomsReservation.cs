using HotelManagement.Core.Entities.RoomManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Reservation
{
    public class RoomsReservation: BaseEntity
    {
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        public Reservation Reservation { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
