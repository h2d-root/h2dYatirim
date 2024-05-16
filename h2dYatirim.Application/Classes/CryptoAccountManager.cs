using Core.Utilities.Results;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;

namespace h2dYatirim.Application.Classes
{
    public class CryptoAccountManager : ICryptoAccountService
    {
        ICryptoAccountDal cryptoAccountDal;
        IAccountDal accountDal;

        public CryptoAccountManager(ICryptoAccountDal cryptoAccountDal, IAccountDal accountDal)
        {
            this.cryptoAccountDal = cryptoAccountDal;
            this.accountDal = accountDal;
        }

        public IDataResult<bool> AddAccount(Guid id)
        {
            var account = accountDal.Get(u=>u.UserId == id);
            
            if (account != null)
            {
                var result = cryptoAccountDal.Get(c => c.UserId == id);
                if (result == null)
                {
                    var cryptoAccount = new CryptoAccount() { UserId = id, AccountId = account.Id };
                    cryptoAccountDal.Add(cryptoAccount);
                    var newAccount = cryptoAccountDal.Get(u => u.UserId == id);
                    account.CryptoAccountId = newAccount.Id;
                    accountDal.Update(account);
                    return new SuccessDataResult<bool>(true);
                }
                else
                {
                    return new ErrorDataResult<bool>(false,"Zaten hesabınız var");
                }
            }
            else
            {
                return new ErrorDataResult<bool>(false, "ilk önce hesap oluşturmanız gerekmektedir");
            }


        }

        
        public IDataResult<CryptoAccount> GetAccount(Guid userId)
        {
            var result = cryptoAccountDal.Get(c => c.UserId == userId);
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

    }
}
