using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Interfaces;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Commands;
public record AddRoomCommand(string name, string description) : IRequest<bool>;
public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room
        {
            Name = "Test",
            Description = "Test"
        };

        await _unitOfWork.GetRepository<Room>().AddAsync(room);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
