using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [HttpPost("Regiter")]
        public IActionResult Register(User user)
        {
            var result = _userService.Register(user);
                if (result)
                {
                    return Ok("Başarılı");
                }
                else
                {
                    return Ok("Zaten mevcut bir kaydınız var");
                }
            
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto user)
        {
            string result = _userService.Login(user);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return Ok(result);
        }
    }
}
