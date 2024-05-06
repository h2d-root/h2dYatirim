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

        public bool AddAccount(Guid id)
        {
            var result = _accountDal.Get(c => c.UserId == id);
            if (result == null)
            {
                var account = new CryptoAccount() { UserId = id, AmountInAccount = 0 };
                _accountDal.Add(account);
                var newAccount = _accountDal.Get(u => u.UserId == id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DepositMoney(Guid userId, decimal balance)
        {
            var result = _accountDal.Get(a => a.UserId == userId);
            if (result != null)
            {
                result.AmountInAccount += Convert.ToDecimal(balance);
                _accountDal.Update(result);
                return true;
            }
            else
            {
                return false;
            }
        }

        public CryptoAccount GetAccount(Guid userId)
        {
            var result = _accountDal.Get(c => c.UserId == userId);
            if (result != null)
            {
                return result;
            }
            else
            {
                return new CryptoAccount();
            }
        }

        public bool RemoveAccount()
        {
            throw new NotImplementedException();
        }

        public bool WithdrawMoney(Guid userId, decimal balance)
        {
            var result = _accountDal.Get(a => a.UserId == userId);
            if (result != null)
            {
                if (result.AmountInAccount >= balance)
                {
                    result.AmountInAccount -= Convert.ToDecimal(balance);
                    _accountDal.Update(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
