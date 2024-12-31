using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Enums;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.MappingProfiles;
using HotelManagement.Core.ViewModels.Response;
using HotelManagement.Core.ViewModels.RoomViewModel;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Query;
public record GetAllRoomsQuery(RoomParams RoomParams) : IRequest<ResponseViewModel<PageList<GetAllRoomViewModel>>>;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, ResponseViewModel<PageList<GetAllRoomViewModel>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRoomsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel<PageList<GetAllRoomViewModel>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = _unitOfWork.GetRepository<Room>()
                                                .GetAll()
                                                .ProjectTo<GetAllRoomViewModel>();
        var paginatedResult = await PageList<GetAllRoomViewModel>.CreateAsync(rooms, request.RoomParams.PageNumber, request.RoomParams.PageSize);


        return new SuccessResponseViewModel<PageList<GetAllRoomViewModel>>(SuccessCode.AdminCreated, paginatedResult);
    }
}
