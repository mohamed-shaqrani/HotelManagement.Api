using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Application.Features.RoomManagement.Rooms.Queries;
using HotelManagement.Core.Entities.RoomManagement;
using HotelManagement.Core.Enums;
using HotelManagement.Core.Interfaces;
using HotelManagement.Core.ViewModels.Response;
using MediatR;

namespace HotelManagement.Application.Features.RoomManagement.Rooms.Commands
{
    public record DeleteRoomCommand(int id) : IRequest<ResponseViewModel<bool>>;

    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ResponseViewModel<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;


        public DeleteRoomCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;

        }
        public async Task<ResponseViewModel<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var response = await ValidateRequest(request);
            if (!response.IsSuccess)
            {
                return response;
            }
            else
            {
                _unitOfWork.GetRepository<Room>().Delete(new Room { Id = request.id });
                await _unitOfWork.SaveChangesAsync();
                return response;
            }
        }


        private async Task<ResponseViewModel<bool>> ValidateRequest(DeleteRoomCommand request)
        {
            var roomExists = await _mediator.Send(new IsRoomExistsQuery(request.id));

            if (!roomExists)
            {
                return new FailureResponseViewModel<bool>(ErrorCode.RoomIDNotExist);
            }

            return new SuccessResponseViewModel<bool>(SuccessCode.None);
        }
    }
}