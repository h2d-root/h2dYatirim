using Core.Utilities.Results;
using h2dYatirim.Application.Interfaces;
using h2dYatirim.Infrastructure.Entities;
using h2dYatirim.Domain.Entity;

namespace h2dYatirim.Application.Classes
{
    public class InvestmentAccountManager : IInvestmentAccountService
    {
        IInvestmentAccountDal _investmentAccountDal;
        IAccountDal _accountDal;


        public InvestmentAccountManager(IInvestmentAccountDal investmentAccountDal, IAccountDal accountDal)
        {
            _investmentAccountDal = investmentAccountDal;
            _accountDal = accountDal;
        }

        public IDataResult<bool> AddAccount(Guid id)
        {
            var account = _accountDal.Get(u=>u.UserId==id);
            if (account != null)
            {
                var result = _investmentAccountDal.Get(c => c.UserId == id);
                if (result == null)
                {
                    var investmentAccount = new InvestmentAccount() { UserId = id, AccountId = account.Id };
                    _investmentAccountDal.Add(investmentAccount);
                    var newAccount = _investmentAccountDal.Get(u => u.UserId == id);
                    return new SuccessDataResult<bool>(true);
                }
                else
                {
                    return new ErrorDataResult<bool>(false,"Zaten hesabınız var");
                }
            }
            else
            {
                return new ErrorDataResult<bool>(false, "İlk önce ana hesabınızı oluşturunuz");
            }

        }

        
        public IDataResult<InvestmentAccount> GetAccount(Guid userId)
        {
            var result = _investmentAccountDal.Get(c => c.UserId == userId);
            if (result != null)
            {
                return new SuccessDataResult<InvestmentAccount>(result);
            }
            else
            {
                return new ErrorDataResult<InvestmentAccount>(new InvestmentAccount());
            }
        }

        public IDataResult<bool> RemoveAccount()
        {
            throw new NotImplementedException();
        }

        
    }


}
