using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatirim.Infrastructure.Entities;
using h2dYatirim.Infrastructure.Services;
using h2dYatırım.Entities;
using Microsoft.Extensions.Configuration;

namespace h2dYatirim.Application.Classes
{

    public class UserManager : IUserService
    {
        IConfiguration _configuration;

        IUserDal _userDal;

        public UserManager(IUserDal userDal, IConfiguration configuration = null)
        {
            _userDal = userDal;
            _configuration = configuration;
        }

        public List<User> GetAll()
        {
            var result = _userDal.GetAll();
            return result;
        }

        public User GetById(Guid id)
        {
            var result = _userDal.Get(u=>u.Id == id);
            return result;
        }

        public string Login(LoginDto dto)
        {
            var result = _userDal.Get(u => u.IdentificationNumber == dto.IdentificationNumber && u.Password == dto.Password);
            if (result == null)
            {
                return "kayıt bulunamadı";
            }
            else
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var token = JwtHelper.GenerateJwtToken(result, jwtSettings);
                var tOken = new { Token = token };
                return token;
            }
        }

        public bool Register(User user)
        {
            var result = _userDal.Get(u=>u.IdentificationNumber == user.IdentificationNumber);
            if (result == null)
            {
                _userDal.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
