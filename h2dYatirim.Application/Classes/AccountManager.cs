using Core.Utilities.Results;
using h2dYatirim.Application.Interfaces;
using h2dYatirim.Infrastructure.Entities;
using h2dYatirim.Domain.Entity;

namespace h2dYatirim.Application.Classes
{
    public class AccountManager : IAccountService
    {
        IAccountDal _accountDal;
        ICryptoAccountDal _cryptoAccountDal;
        IInvestmentAccountDal _investmentAccountDal;

        public AccountManager(IAccountDal accountDal, ICryptoAccountDal cryptoAccountDal, IInvestmentAccountDal investmentAccountDal)
        {
            _cryptoAccountDal = cryptoAccountDal;
            _investmentAccountDal = investmentAccountDal;
            _accountDal = accountDal;
        }

        public IDataResult<bool> AddAccount(Guid id)
        {
            var result = _accountDal.Get(c => c.UserId == id);
            if (result == null)
            {
                var account = new Account() { UserId = id, AmountInAccount = 0 };
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

        public IDataResult<Account> GetAccount(Guid userId)
        {
            var result = _accountDal.Get(c => c.UserId == userId, includes: "CryptoAccount, InvestmentAccount");
            if (result != null)
            {
                var crypto = _cryptoAccountDal.Get(u=>u.UserId == userId);
                var share = _investmentAccountDal.Get(u=> u.UserId == userId);
                result.AssetValue = crypto.WalletValue+share.PortfolioValue;
                _accountDal.Update(result);
                return new SuccessDataResult<Account>(result);
            }
            else
            {
                return new ErrorDataResult<Account>(new Account());
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
