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
        public List<User> GetAll();
        public User GetById(Guid id);
        public bool Register(User user);
        public string Login(LoginDto dto);
    }
}
