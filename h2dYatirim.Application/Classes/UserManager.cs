using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatirim.Infrastructure.Entities;
using h2dYatirim.Infrastructure.Services;
using h2dYatırım.Entities;
using Microsoft.Extensions.Configuration;
using Core.Utilities.Results;

namespace h2dYatirim.Application.Classes
{

    public class UserManager : IUserService
    {
        IConfiguration _configuration;

        IUserDal _userDal;

        public UserManager(IUserDal userDal, IConfiguration configuration)
        {
            _userDal = userDal;
            _configuration = configuration;
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(result);
        }

        public IDataResult<User> GetById(Guid id)
        {
            var result = _userDal.Get(u=>u.Id == id);
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<string> Login(LoginDto dto)
        {
            var result = _userDal.Get(u => u.IdentificationNumber == dto.IdentificationNumber && u.Password == dto.Password);
            if (result == null)
            {
                return new ErrorDataResult<string>("kayıt bulunamadı");
            }
            else
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var token = JwtHelper.GenerateJwtToken(result, jwtSettings);
                return new SuccessDataResult<string>(token,"token başarılı bir şekilde oluşturuldu");
            }
        }

        public IDataResult<bool> Register(User user)
        {
            var result = _userDal.Get(u=>u.IdentificationNumber == user.IdentificationNumber);
            if (result == null)
            {
                _userDal.Add(user);
                return new SuccessDataResult<bool>(true,"başarılı bir şekilde kayıt oldunuz");
            }
            else
            {
                return new ErrorDataResult<bool>(false, "Zaten böyle bir kayıt var.");
            }
        }
    }
}
