using h2dYatirim.Application.DTOs;
using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface IPortfolioService
    {
        public bool Buying(Guid id,BuyingSellingDTO dto);
        public bool Selling(Guid id, BuyingSellingDTO dto);
        public List<Portfolio> GetPortfolio(Guid id);
        public List<Portfolio> Refresh(Guid id);
        

    }
}
