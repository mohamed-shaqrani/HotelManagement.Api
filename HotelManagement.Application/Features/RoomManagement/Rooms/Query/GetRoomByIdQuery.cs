using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Enums;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.MappingProfiles;
using HotelManagement.Core.ViewModels.Response;
using HotelManagement.Core.ViewModels.RoomViewModel;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Query;
public record GetRoomByIdQuery(int Id) : IRequest<ResponseViewModel<RoomViewModel>>;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ResponseViewModel<RoomViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel<RoomViewModel>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _unitOfWork.GetRepository<Room>()
                                                .GetByIdAsync(request.Id);
        var vm = room.Map<RoomViewModel>();

        return new SuccessResponseViewModel<RoomViewModel>(SuccessCode.OperationSucceeded, vm);
    }
}
