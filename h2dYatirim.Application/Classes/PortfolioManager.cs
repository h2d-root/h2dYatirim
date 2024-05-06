using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;
using h2dYatırım.Services;

namespace h2dYatirim.Application.Classes
{
    public class PortfolioManager : IPortfolioService
    {
        IPortfolioDal _portfolioDal;
        IAccountMovementDal _accountMovementDal;
        ICryptoAccountDal _cryptoAccountDal;

        public PortfolioManager(ICryptoAccountDal cryptoAccountDal, IAccountMovementDal accountMovementDal, IPortfolioDal portfolioDal)
        {
            _cryptoAccountDal = cryptoAccountDal;
            _accountMovementDal = accountMovementDal;
            _portfolioDal = portfolioDal;
        }

        public bool Buying(Guid id, BuyingSellingDTO dto)
        {
            var account = _cryptoAccountDal.Get(u => u.UserId == id);
            if (account != null)
            {
                var coin = CoinService.ServiceGetAsync(dto.CryptoId);
                var coinPrice = Convert.ToDecimal(coin.Result.PriceUsd);
                decimal value = Convert.ToDecimal(dto.Amount) * coinPrice;
                if (account.AmountInAccount >= value)
                {
                    Portfolio portfolio;
                    var cryptoPortfolio = _portfolioDal.Get(c => c.CryptoId == dto.CryptoId);
                    if (cryptoPortfolio != null)
                    {
                        cryptoPortfolio.Amount += dto.Amount;
                        cryptoPortfolio.CurrentValue += value;
                        cryptoPortfolio.ReceivedValue += value;
                        _portfolioDal.Update(cryptoPortfolio);
                        portfolio = cryptoPortfolio;
                    }
                    else
                    {
                        Portfolio newPortfolio = new Portfolio()
                        {
                            CryptoAccountId = account.Id,
                            UserId = id,
                            CryptoId = dto.CryptoId,
                            Amount = dto.Amount,
                            ValueChange = 0,
                            CurrentValue = value,
                            ReceivedValue = value
                        };
                        _portfolioDal.Add(newPortfolio);
                        portfolio = newPortfolio;
                    }
                    var portfolios = _portfolioDal.GetAll(u => u.UserId == id);
                    decimal portfolioTotalValue = 0;
                    foreach (var item in portfolios)
                    {
                        portfolioTotalValue += item.CurrentValue;
                    }
                    account.AmountInAccount -= value;
                    account.ProtfolioValue = portfolioTotalValue;
                    _cryptoAccountDal.Update(account);
                    AccountMovement movement = new AccountMovement()
                    {
                        UserId = id,
                        CryptoAccountId = account.Id,
                        CryptoId = dto.CryptoId,
                        Value = value,
                        TransactionType = "Alış"
                    };
                    _accountMovementDal.Add(movement);
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

        public List<Portfolio> GetPortfolio(Guid id)
        {
            decimal totalPortfolioValue = 0;
            var result = _portfolioDal.GetAll(u => u.UserId == id);
            foreach (var item in result)
            {
                var crypto = CoinService.ServiceGetAsync(item.CryptoId);
                item.CurrentValue = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(crypto.Result.PriceUsd);

                decimal received = item.ReceivedValue;
                decimal current = item.CurrentValue;
                decimal fark = current - received;
                decimal yuzdeFark = (fark / received) * 100;
                item.ValueChange = yuzdeFark;

                totalPortfolioValue += item.CurrentValue;

                _portfolioDal.Update(item);
            }
            var account = _cryptoAccountDal.Get(u => u.UserId == id);
            account.ProtfolioValue = totalPortfolioValue;
            _cryptoAccountDal.Update(account);
            var portfolios = _portfolioDal.GetAll(u => u.UserId == id);
            return portfolios;
        }

        public List<Portfolio> Refresh(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Selling(Guid id, BuyingSellingDTO dto)
        {
            var portfolio = _portfolioDal.Get(u => u.UserId == id && u.CryptoId == dto.CryptoId);
            var account = _cryptoAccountDal.Get(u => u.UserId == id);
            var crypto = CoinService.ServiceGetAsync(dto.CryptoId);
            decimal coinPrice = Convert.ToDecimal(crypto.Result.PriceUsd);
            if (portfolio != null)
            {
                if (portfolio.Amount >= dto.Amount)
                {
                    decimal value = Convert.ToDecimal(dto.Amount) * coinPrice;
                    account.ProtfolioValue -= value;
                    account.AmountInAccount += value;
                    var movement = new AccountMovement()
                    {
                        UserId = id,
                        CryptoAccountId = account.Id,
                        CryptoId = dto.CryptoId,
                        TransactionType = "Satış",
                        Value = value
                    };
                    if (portfolio.Amount > dto.Amount)
                    {
                        portfolio.CurrentValue -= value;
                        decimal ortalama = portfolio.ReceivedValue / Convert.ToDecimal(portfolio.Amount);
                        portfolio.ReceivedValue -= ortalama * Convert.ToDecimal(dto.Amount);
                        portfolio.Amount -= dto.Amount;
                        _portfolioDal.Update(portfolio);
                    }
                    else if (portfolio.Amount == dto.Amount)
                    {
                        _portfolioDal.Delete(portfolio);
                    }
                    _cryptoAccountDal.Update(account);
                    _accountMovementDal.Add(movement);
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
