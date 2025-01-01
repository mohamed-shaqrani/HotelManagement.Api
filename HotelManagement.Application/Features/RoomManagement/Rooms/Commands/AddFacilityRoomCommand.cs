using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Commands
{
    public record AddFacilityRoomCommand(int roomid, int facilityid) : IRequest<bool>;
    public class AddFacilityRoomCommandHandler : IRequestHandler<AddFacilityRoomCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFacilityRoomCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Handle (AddFacilityRoomCommand command, CancellationToken token) 
        {
            if (command.facilityid <=0 || command.roomid <= 0) 
            {
                return false;
            }

           await _unitOfWork.GetRepository<FacilityRoom>().AddAsync(new FacilityRoom() { FacilityId = command.facilityid,
                RoomId = command.roomid });
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
