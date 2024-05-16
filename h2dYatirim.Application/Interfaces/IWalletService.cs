using Core.Utilities.Results;
using h2dYatirim.Application.DTOs;
using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface IWalletService
    {
        IDataResult<bool> Buying(Guid id, BuyingSellingDTO dto);
        IDataResult<bool> Selling(Guid id, BuyingSellingDTO dto);
        IDataResult<List<Wallet>> GetWallet(Guid id);
        IDataResult<List<Wallet>> Refresh(Guid id);
        decimal Value(Guid id);

    }
}
