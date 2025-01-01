using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Queries
{
    public record IsRoomNameExistsQuery(string name):IRequest<bool>;
    public class IsRoomNameExistsQueryHandler : IRequestHandler<IsRoomNameExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsRoomNameExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(IsRoomNameExistsQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.GetRepository<Room>().AnyAsync(x => x.Name == request.name);
            if (isExist)
            {
                return true;
            }
            return false;
        }
    }
}
