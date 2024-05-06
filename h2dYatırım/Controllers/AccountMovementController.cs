using h2dYatirim.Application.Interfaces;
using h2dYatırım.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountMovementController : ControllerBase
    {
        IAccountMovementService _accountMovementService;

        public AccountMovementController(IAccountMovementService accountMovementService)
        {
            _accountMovementService = accountMovementService;
        }

        [HttpGet("GetMyAccountMovement")]
        public ActionResult GetMyAccountMovement()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _accountMovementService.GetAccountMovement(userId);
            return Ok(result);
        }

    }
}
