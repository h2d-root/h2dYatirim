using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {

        IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost("Buying")]
        public IActionResult Buying(BuyingSellingDTO buying)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _portfolioService.Buying(userId,buying);
            return Ok(result);
        }


        [HttpPost("Selling")]
        public IActionResult Selling(BuyingSellingDTO selling)
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _portfolioService.Selling(userId, selling);
            return Ok(result);
        }


        [HttpGet("GetMyPortfolio")]
        public IActionResult GetMyPortfolio()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(x => x.Type == "id").Value);
            var result = _portfolioService.GetPortfolio(userId);
            return Ok(result);
        }
    }
}
