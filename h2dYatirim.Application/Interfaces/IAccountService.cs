using Core.Utilities.Results;
using h2dYatirim.Domain.Entity;

namespace h2dYatirim.Application.Interfaces
{
    public interface IAccountService
    {
        IDataResult<bool> AddAccount(Guid id);
        IDataResult<bool> RemoveAccount();
        IDataResult<Account> GetAccount(Guid userId);
        IDataResult<bool> DepositMoney(Guid userId, decimal balance);
        IDataResult<bool> WithdrawMoney(Guid userId, decimal balance);
    }
}
