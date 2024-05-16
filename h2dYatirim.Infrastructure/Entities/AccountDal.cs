using Core.DataAccess.EntityFramework;
using h2dYatirim.Domain.Entity;
using h2dYatırım.DataAccess;

namespace h2dYatirim.Infrastructure.Entities
{
    public class AccountDal : efEntitiyRepositoryBase<Account, h2dYatirimDBContext>, IAccountDal { }

}
