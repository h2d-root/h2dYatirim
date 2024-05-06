using Core.DataAccess.EntityFramework;
using h2dYatırım.DataAccess;
using h2dYatırım.Entities;

namespace h2dYatirim.Infrastructure.Entities
{
    public class AccountMovementDal : efEntitiyRepositoryBase<AccountMovement, h2dYatirimDBContext>, IAccountMovementDal { }
}
