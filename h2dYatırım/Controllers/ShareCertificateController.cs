using h2dYatirim.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ShareCertificateController : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ShareService.GetStockDataAsync();
            return Ok(result);
        }
    }
}
