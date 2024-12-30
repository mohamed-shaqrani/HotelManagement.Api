using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers;
[Route("api/account")]
[ApiController]
public class AccountsController : ControllerBase
{

    public ActionResult Test()
    {
        return Ok("Test Api");
    }
}
