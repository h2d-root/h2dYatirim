using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;

namespace h2dYatirim.Application.Classes
{
    public class AccountMovementManager : IAccountMovementService
    {
        IAccountMovementDal _accountMovementDal;

        public AccountMovementManager(IAccountMovementDal accountMovementDal)
        {
            _accountMovementDal = accountMovementDal;
        }

        public List<AccountMovement> GetAccountMovement(Guid id)
        {
            var result = _accountMovementDal.GetAll(u=>u.UserId == id);
            return result;
        }
    }
}
