using h2dYatirim.Application.Interfaces;
using h2dYatırım.DataAccess;
using h2dYatırım.Entities;
using h2dYatırım.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ICryptoAccountService _cryptoService;

        public AccountController(ICryptoAccountService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet("AddAccount")]
        public IActionResult AddAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.AddAccount(id);
            return Ok(result);
        }
        [HttpGet("GetMyAccount")]
        public IActionResult GetMyAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.GetAccount(id);
            return Ok(result);
        }
        [HttpGet("DepositMoney")]
        public IActionResult DepositMoney(decimal balance)
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.DepositMoney(id,balance);
            return Ok(result);
        }
        [HttpGet("WithdrawMoney")]
        public IActionResult WithdrawMoney(decimal balance)
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.WithdrawMoney(id, balance);
            return Ok(result);
        }
    }
}
