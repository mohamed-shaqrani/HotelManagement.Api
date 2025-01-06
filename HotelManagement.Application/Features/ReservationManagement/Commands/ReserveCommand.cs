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
            };
            await repo.AddAsync(reservation);

            await unitOfWork.SaveChangesAsync();

           await mediator.Publish(new Reserved() { ReservationId = reservation.Id, RoomsId = command.RoomsId  });

            return true;


            
            
            

        }

        public async Task<bool> Validate(ReserveCommand command) 
        {
            if (command.GuestId <= 0 || command.NumberOfGuests <= 0 || command.Cost <= 0 || command.RoomsId.Count <= 0)
            {
                return false;
            }
            var repo = unitOfWork.GetRepository<Room>();

            var query = repo.GetAll(r => command.RoomsId.Contains(r.Id)).Select(r => new {r.Id,r.RoomStatus});

            

            var rooms = await query.ToListAsync();

            bool today = command.StartDay.Date == DateTime.Today;

            if(!(command.RoomsId.Count == rooms.Count)) 
            {
                return false;
            }

            if (today) 
            {
                foreach (var room in rooms) 
                {
                    if(room.RoomStatus == RoomSatus.RoomBooked || room.RoomStatus == RoomSatus.RoomNotActive) 
                    {
                        return false ;
                    }
                }

                return true;
            }

            return true ;
        }
    }
}
