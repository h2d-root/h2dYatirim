using Core.Utilities.Results;
using h2dYatirim.Application.DTOs;
using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface IPortfolioService
    {
        IDataResult<bool> Buying(Guid id, BuyingSellingDTO dto);
        IDataResult<bool> Selling(Guid id, BuyingSellingDTO dto);
        IDataResult<List<Portfolio>> GetPortfolio(Guid id);
        IDataResult<List<Portfolio>> Refresh(Guid id);
        decimal Value(Guid id);

    }
}
