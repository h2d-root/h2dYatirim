using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface ICryptoAccountService
    {
        public bool AddAccount(Guid id);
        public bool RemoveAccount();
        public CryptoAccount GetAccount(Guid userId);
        public bool DepositMoney(Guid userId, decimal balance);
        public bool WithdrawMoney(Guid userId, decimal balance);
    }
}
