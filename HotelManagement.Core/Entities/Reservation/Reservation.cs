using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities.Reservation
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }

        public int NumberOfGuests { get; set; }

        public DateTime LastDay { get; set; }

        public DateTime FirstDay { get; set; }

       public ICollection<RoomsReservation> RoomsReservations { get; set; }

        public int RoomsNumbers { get; set; }
        public decimal Cost { get; set; }


        public decimal Discount { get; set; }

        public decimal CostAfterDiscount { get; set; }
    }
}
