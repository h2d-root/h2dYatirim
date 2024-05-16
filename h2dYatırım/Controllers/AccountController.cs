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
        IInvestmentAccountService _shareService;
        IAccountService _accountService;
        IPortfolioService _portfolioService;
        IWalletService _walletService;

        public AccountController(ICryptoAccountService cryptoService, IInvestmentAccountService shareService, IAccountService accountService, IPortfolioService portfolioService, IWalletService walletService)
        {
            _cryptoService = cryptoService;
            _shareService = shareService;
            _accountService = accountService;
            _portfolioService = portfolioService;
            _walletService = walletService;
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _accountService.AddAccount(id);
            return Ok(result);
        }
        [HttpPost("AddCryptoAccount")]
        public IActionResult AddCryptoAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.AddAccount(id);
            return Ok(result);
        }
        [HttpPost("AddInvestmentAccount")]
        public IActionResult AddInvestmentAccountAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _shareService.AddAccount(id);
            return Ok(result);
        }

        [HttpGet("GetMyAccount")]
        public IActionResult GetMyAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var account = _accountService.GetAccount(id);
            return Ok(account);
        }

        [HttpGet("GetMyInvestmentAccount")]
        public IActionResult GetMyInvestmentAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _shareService.GetAccount(id);
            return Ok(result);
        }
        [HttpGet("GetMyCryptoAccount")]
        public IActionResult GetMyCryptoAccount()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _cryptoService.GetAccount(id);
            return Ok(result);
        }
        [HttpGet("DepositMoney")]
        public IActionResult InvestmentAccountDepositMoney(decimal balance)
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _accountService.DepositMoney(id,balance);
            return Ok(result);
        }
        [HttpGet("WithdrawMoney")]
        public IActionResult InvestmentAccountWithdrawMoney(decimal balance)
        {
            var id = Guid.Parse(HttpContext.User.FindFirst("id")?.Value);
            var result = _accountService.WithdrawMoney(id, balance);
            return Ok(result);
        }
    }
}
