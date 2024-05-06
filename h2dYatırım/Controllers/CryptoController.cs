using h2dYatırım.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await CoinService.ServiceAsync());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await CoinService.ServiceGetAsync(id));
        }

    }
}
