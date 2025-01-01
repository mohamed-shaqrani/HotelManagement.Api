using HotelManagement.Application.Features.RoomManagement.Rooms.Commands;
using HotelManagement.Application.Features.RoomManagement.Rooms.Query;
using HotelManagement.Core.Extensions;
using HotelManagement.Core.Helpers;
using HotelManagement.Core.ViewModels.Response;
using HotelManagement.Core.ViewModels.Rooms;
using HotelManagement.ication.Models.RoomManagement.Rooms.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;
[Route("api/rooms")]
[ApiController]
public class RoomsController : ControllerBase
{

    readonly IMediator _meditor;

    public RoomsController(IMediator meditor)
    {
        _meditor = meditor;
    }
    [HttpPost]
    public async Task<ResponseViewModel<bool>> Create(RoomCreateViewModel viewModel)
    {
        var query = new AddRoomCommand(viewModel.Name, viewModel.Description, viewModel.Price,viewModel.RoomStatus);
        var res = await _meditor.Send(query);
        return res;
        //return await _meditor.Send(new AddRoomCommand(viewModel.Name, viewModel.Description));
    }

    [HttpDelete]
    public async Task<ResponseViewModel<bool>> Delete(int id)
    {
        return await _meditor.Send(new DeleteRoomCommand(id));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllRooms([FromQuery] RoomParams roomParams)
    {
        var query = new GetAllRoomsQuery(roomParams);
        var res = await _meditor.Send(query);
        Response.AddPaginationHeader(res.Data);

        return Ok(res);
    }
}
