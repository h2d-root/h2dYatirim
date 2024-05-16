using Core.Utilities.Results;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;

namespace h2dYatirim.Application.Classes
{
    public class CryptoAccountManager : ICryptoAccountService
    {
        ICryptoAccountDal _accountDal;

        public CryptoAccountManager(ICryptoAccountDal accountDal)
        {
            _accountDal = accountDal;
        }

        public IDataResult<bool> AddAccount(Guid id)
        {
            var result = _accountDal.Get(c => c.UserId == id);
            if (result == null)
            {
                var account = new CryptoAccount() { UserId = id, AmountInAccount = 0 };
                _accountDal.Add(account);
                var newAccount = _accountDal.Get(u => u.UserId == id);
                return new SuccessDataResult<bool>(true);
            }
            else
            {
                return new ErrorDataResult<bool>(false);
            }
        }

        public IDataResult<bool> DepositMoney(Guid userId, decimal balance)
        {
            var result = _accountDal.Get(a => a.UserId == userId);
            if (result != null)
            {
                result.AmountInAccount += Convert.ToDecimal(balance);
                _accountDal.Update(result);
                return new SuccessDataResult<bool>(true);
            }
            else
            {
                return new ErrorDataResult<bool>(false);
            }
        }

        public IDataResult<CryptoAccount> GetAccount(Guid userId)
        {
            var result = _accountDal.Get(c => c.UserId == userId);
            if (result != null)
            {
                return new SuccessDataResult<CryptoAccount>(result);
            }
            else
            {
                return new ErrorDataResult<CryptoAccount>(new CryptoAccount());
            }
        }

        public IDataResult<bool> RemoveAccount()
        {
            throw new NotImplementedException();
        }

        public IDataResult<bool> WithdrawMoney(Guid userId, decimal balance)
        {
            var result = _accountDal.Get(a => a.UserId == userId);
            if (result != null)
            {
                if (result.AmountInAccount >= balance)
                {
                    result.AmountInAccount -= balance;
                    _accountDal.Update(result);
                    return new SuccessDataResult<bool>(true);
                }
                else
                {
                    return new ErrorDataResult<bool>(false);
                }
            }
            else
            {
                return new ErrorDataResult<bool>(false);
            }
        }
    }
}
