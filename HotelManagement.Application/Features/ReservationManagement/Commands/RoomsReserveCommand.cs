using HotelManagement.Application.Features.ReservationManagement.Events;
using HotelManagement.Core.Entities.Reservation;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.ReservationManagement.Commands
{
    public class RoomReserveCommand : IRequestHandler<RoomsReserveCommand,bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public RoomReserveCommand(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(RoomsReserveCommand command, CancellationToken token) 
        {
            if (!await Validate(command)) 
            {
                return false;
            }

            var repo = unitOfWork.GetRepository<RoomsReservation>();

            if(command.RoomsId.Count == 1) 
            {
                await repo.AddAsync(new RoomsReservation()
                {
                    ReservationId = command.ReservationId,
                    RoomId = command.RoomsId[0],
                    StartDate = command.FirstDay,
                    EndDate = command.LastDay,
                });
            }

          await  repo.AddRangeAsync(CreateRooms(command));
            return true;
        }

        public IEnumerable<RoomsReservation> CreateRooms(RoomsReserveCommand command) 
        {
            var rooms = new List<RoomsReservation>();
            foreach (var roomid in command.RoomsId) 
            {
                rooms.Add(new RoomsReservation() { ReservationId = command.ReservationId, 
                    RoomId = roomid,
                    StartDate = command.FirstDay,
                    EndDate = command.LastDay,
                });
            }
            return rooms;
        }

        public async Task<bool> Validate(RoomsReserveCommand command) 
        {
            var roomsrepo = unitOfWork.GetRepository<Room>();

            var AnyRooms = await roomsrepo.AnyAsync( r=> command.RoomsId.Contains(r.Id));

            if (!AnyRooms) 
            {
                return false;
            }

            var RoomsReserve = unitOfWork.GetRepository<RoomsReservation>();
            var AnyReserve = await RoomsReserve.AnyAsync(r=>command.RoomsId.Contains(r.RoomId) && 
            (r.StartDate>=command.FirstDay && r.EndDate<=command.LastDay) || 
            (r.StartDate<command.FirstDay && r.EndDate> command.LastDay   ) ||
            (r.StartDate > command.FirstDay && r.EndDate > command.LastDay));
            if (AnyReserve) 
            {
                return false;
            }
            return true;
        }
    }
}
