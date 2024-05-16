using Core.Utilities.Results;
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
        public IDataResult<List<AccountMovement>> GetAccountMovement(Guid userId)
        {
            return new SuccessDataResult<List<AccountMovement>>(_accountMovementDal.GetAll(u => u.UserId == userId));
        }
    }
}
