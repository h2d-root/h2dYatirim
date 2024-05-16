using Core.Utilities.Results;
using h2dYatirim.Domain.Entity;

namespace h2dYatirim.Application.Interfaces
{
    public interface IInvestmentAccountService
    {
        IDataResult<bool> AddAccount(Guid id);
        IDataResult<bool> RemoveAccount();
        IDataResult<InvestmentAccount> GetAccount(Guid userId);
    }

}
