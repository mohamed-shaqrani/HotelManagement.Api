﻿using HotelManagement.Application.Features.ReservationManagement.Events;
using HotelManagement.Core.Entities.Reservation;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
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

       var Save =  await mediator.Send(new RoomsReserveCommand() 
            { ReservationId = reservation.Id, 
                RoomsId = command.RoomsId,
                FirstDay = reservation.FirstDay,
                LastDay = reservation.LastDay,
            });

            if (!Save) 
            {
                repo.ClearedTrackedChanges(reservation);
                return false;
            }

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
            if (command.GuestId <= 0 || command.NumberOfGuests <= 0 || command.Cost <= 0 || command.RoomsId.Count <= 0  )
            {
                return false;
            }

            if (command.LastDay < command.StartDay) 
            {
                return false;
            }
           
            var ReserveRepo = unitOfWork.GetRepository<Reservation>();

            var Any = await ReserveRepo.AnyAsync( r=>  r.FirstDay<=command.LastDay||  r.LastDay>=command.StartDay);

            if (Any) 
            {
                return false;
            }
           

            

            return true ;
        }
    }
}
