using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("Buying")]
        public IActionResult Buying(BuyingSellingDTO buying)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _walletService.Buying(userId, buying);
            return Ok(result);
        }


        [HttpPost("Selling")]
        public IActionResult Selling(BuyingSellingDTO selling)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _walletService.Selling(userId, selling);
            return Ok(result);
        }


        [HttpGet("GetMyWallet")]
        public IActionResult GetMyPortfolio()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _walletService.GetWallet(userId);
            return Ok(result);
        }
    }
}
