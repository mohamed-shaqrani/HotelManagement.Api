using HotelManagement.Application.Features.RoomManagement.Rooms.Queries;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Enums;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.ViewModels.Response;
using MediatR;

namespace HotelManagement.ication.Models.RoomManagement.Rooms.Commands;
public record AddRoomCommand(string name, string description , decimal Price, RoomSatus RoomStatus) : IRequest<ResponseViewModel<bool>>;
public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, ResponseViewModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public AddRoomCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<ResponseViewModel<bool>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
    {
        var response = await ValidateRequest(request);
        if (!response.IsSuccess) 
        {
            return response;
        }
        var room = new Room
        {
            Name = request.name,
            Description = request.description,
            Price = request.Price,
            RoomStatus = request.RoomStatus
        };

        await _unitOfWork.GetRepository<Room>().AddAsync(room);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }

    private async Task<ResponseViewModel<bool>> ValidateRequest(AddRoomCommand request) 
    {
        if (string.IsNullOrEmpty(request.name)) 
        {
            return new FailureResponseViewModel<bool>(ErrorCode.RoomNameIsRequired);
        }
        if (string.IsNullOrEmpty(request.description))
        {
            return new FailureResponseViewModel<bool>(ErrorCode.RoomDescriptionIsRequired);
        }
        var roomExists = await _mediator.Send(new IsRoomNameExistsQuery(request.name));

        if (roomExists)
        {
            return new FailureResponseViewModel<bool>(ErrorCode.RoomAlreadyExists);
        }

        return new SuccessResponseViewModel<bool>(SuccessCode.None);
    }
}
