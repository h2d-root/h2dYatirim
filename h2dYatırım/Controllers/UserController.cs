using h2dYatırım.DataAccess;
using h2dYatırım.DTOs;
using h2dYatırım.Entities;
using h2dYatırım.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace h2dYatırım.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserDal _userDal = new UserDal();
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        JwtHelper jwtHelper = new JwtHelper();

        [HttpPost("Regiter")]
        public IActionResult Register(User user)
        {
            try
            {
                var result = _userDal.Get(u=>u.IdentificationNumber == user.IdentificationNumber);
                if (result == null)
                {
                    user.Id = Guid.NewGuid();
                    _userDal.Add(user);
                    return Ok("Başarılı");
                }
                else
                {
                    return Ok("Zaten mevcut bir kaydınız var ["+result.FirstName+"]");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDTO user)
        {
            var result = _userDal.Get(u=>u.IdentificationNumber == user.IdentificationNumber && u.Password == user.Password);
            if (result == null)
            {
                return Ok("kayıt bulunamadı");
            }
            else
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var token = jwtHelper.GenerateJwtToken(result, jwtSettings);

                return Ok(new { Token = token });
            }
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _userDal.GetAll();
            return Ok(result);
        }
    }
}
