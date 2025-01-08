using HotelManagement.Core.Entities;
using HotelManagement.Core.Entities.RoomManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.ReservationManagement.Events
{
    public class RoomsReserveCommand : IRequest<bool>
    {
        public int ReservationId { get; set; }

        public List<int> RoomsId { get; set; } = new();

        public DateTime FirstDay { get; set; }

        public DateTime LastDay { get; set; }
    }

    public class RoomReserved : INotification 
    {
        public List<int> RoomId { get;set; } = new();

        public RoomSatus RoomSatus {  get; set; }
    }
}
