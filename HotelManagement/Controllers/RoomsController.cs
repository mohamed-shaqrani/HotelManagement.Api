using HotelManagement.Application.Features.RoomManagement.Rooms.Commands;
using HotelManagement.Application.Features.RoomManagement.Rooms.Query;
using HotelManagement.Core.Extensions;
using HotelManagement.Core.Helpers;
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
    public async Task<ActionResult> Add(string name, string desc)
    {
        var res = await _meditor.Send(new AddRoomCommand(name, desc));
        return Ok(res);
    }
    [HttpGet]
    public async Task<ActionResult> GetAllRooms([FromQuery] RoomParams roomParams)
    {
        var query = new GetAllRoomsQuery(roomParams);
        var res = await _meditor.Send(query);
        Response.AddPaginationHeader(res.Data);

        return Ok(res);

    }

    [HttpPut("edit/room")]

    public async Task<ActionResult> EditRoom([FromBody] EditRoomCommand command) 
    {
        var result = await _meditor.Send(command);

        if (result) 
        {
            return Ok("Room is edited successfully");
        }
        return BadRequest("Something is wrong ");    
    }
}
