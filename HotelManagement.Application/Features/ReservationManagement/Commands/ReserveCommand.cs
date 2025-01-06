using HotelManagement.Application.Features.ReservationManagement.Events;
using HotelManagement.Core.Entities.Reservation;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.ReservationManagement.Commands
{
    public record ReserveCommand(int GuestId, int NumberOfGuests, DateTime StartDay, DateTime LastDay,decimal Cost,decimal Discount,List<int> RoomsId) : IRequest<bool>;
    public class ReserveCommandHandle : IRequestHandler<ReserveCommand,bool>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        public ReserveCommandHandle(IUnitOfWork unitOfWork,IMediator mediator) 
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ReserveCommand command,CancellationToken cancellation) 
        {
         

            if( !await Validate(command)) 
            {
                return false;
            }

            var repo = unitOfWork.GetRepository<Reservation>();

            var reservation = new Reservation()
            {
                Cost = command.Cost,
                FirstDay = command.StartDay,
                LastDay = command.LastDay,
                RoomsNumbers = command.RoomsId.Count(),
                NumberOfGuests = command.NumberOfGuests,
                UserId = command.GuestId,
                Discount = command.Discount,
                CostAfterDiscount = command.Cost - command.Discount,
                Status = Core.Enums.ReservationStatus.Pending
            };
            await repo.AddAsync(reservation);

            await unitOfWork.SaveChangesAsync();
            IEnumerable<RoomsReservation> reservations = CreateRoomsReservation(command.RoomsId, reservation);
           await unitOfWork.GetRepository<RoomsReservation>().AddRangeAsync(reservations);
           
            if(command.StartDay.Date == DateTime.Today) 
            {
                await mediator.Publish(new RoomReserved() { RoomId = command.RoomsId, RoomSatus = RoomSatus.RoomBooked });

            }
           await unitOfWork.SaveChangesAsync();
            return true;


            
            
            

        }

        public IEnumerable<RoomsReservation> CreateRoomsReservation(IEnumerable<int> roomsids,Reservation reservation) 
        {
            var roomsreservations = new List<RoomsReservation>();
            foreach (var roomid in roomsids) 
            {
                roomsreservations.Add(new RoomsReservation() 
                {
                     ReservationId = reservation.Id,
                     RoomId = roomid,
                     StartDate = reservation.FirstDay,
                     EndDate = reservation.LastDay,
                });
            }

            return roomsreservations;
        }


        public async Task<bool> Validate(ReserveCommand command) 
        {
            if (command.GuestId <= 0 || command.NumberOfGuests <= 0 || command.Cost <= 0 || command.RoomsId.Count <= 0)
            {
                return false;
            }
           
            var RoomReserveRepo = unitOfWork.GetRepository<RoomsReservation>();

            var Any = await RoomReserveRepo.AnyAsync(
            r=> command.RoomsId.Contains(r.Id) && 
            r.StartDate<=command.LastDay&&
             r.EndDate>=command.StartDay
            );

            if (Any) 
            {
                return false;
            }
           

            

            return true ;
        }
    }
}
