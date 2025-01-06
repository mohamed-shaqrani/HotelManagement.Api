using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Commands
{
    public record EditRoomCommand(int id, string name, string description) : IRequest<bool>;
    internal class EditRoomCommandHandler : IRequestHandler<EditRoomCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditRoomCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(EditRoomCommand command, CancellationToken cancellationToken)
        {
            if (command.id <= 0 || command.name is null || command.description is null)
            {
                return false;
            }

            var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(command.id);

            if (room is null)
            {
                return false;
            }

            room.Name = command.name;
            room.Description = command.description;

            await _unitOfWork.SaveChangesAsync();

            return true;

        }
    }
}
