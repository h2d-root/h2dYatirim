using Core.Utilities.Results;
using h2dYatirim.Application.DTOs;
using h2dYatırım.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace h2dYatirim.Application.Interfaces
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(Guid id);
        IDataResult<bool> Register(User user);
        IDataResult<string> Login(LoginDto dto);
    }

}
