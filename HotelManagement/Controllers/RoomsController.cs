using HotelManagement.Application.Models.RoomManagement.Rooms.Commands;
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
    public async Task<ActionResult> Add(string name, string desc)
    {
        var res = await _meditor.Send(new AddRoomCommand(name, desc));
        return Ok(res);
    }
}
