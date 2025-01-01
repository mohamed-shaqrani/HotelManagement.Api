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
    public record IsRoomExistsQuery(int id):IRequest<bool>;
    public class IsRoomExistsQueryHandler : IRequestHandler<IsRoomExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public IsRoomExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(IsRoomExistsQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.GetRepository<Room>().AnyAsync(x => x.Id == request.id);
            if (isExist)
            {
                return true;
            }
            return false;
        }
    }
}
