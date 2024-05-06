using Core.DataAccess.EntityFramework;
using h2dYatırım.DataAccess;
using h2dYatırım.Entities;

namespace h2dYatirim.Infrastructure.Entities
{
    public class UserDal : efEntitiyRepositoryBase<User, h2dYatirimDBContext>, IUserDal { }
}
