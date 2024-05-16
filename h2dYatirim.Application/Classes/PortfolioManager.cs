using Core.Utilities.Results;
using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;
using h2dYatirim.Infrastructure.Services;
using h2dYatırım.Services;

namespace h2dYatirim.Application.Classes
{
    public class PortfolioManager : IPortfolioService
    {
        IPortfolioDal _portfolioDal;
        IAccountMovementDal _accountMovementDal;
        IInvestmentAccountDal _investmentAccountDal;
        IAccountDal _accountDal;

        public PortfolioManager(IAccountMovementDal accountMovementDal, IAccountDal accountDal, IPortfolioDal portfolioDal, IInvestmentAccountDal investmentAccountDal)
        {
            _accountMovementDal = accountMovementDal;
            _portfolioDal = portfolioDal;
            _investmentAccountDal = investmentAccountDal;
            _accountDal = accountDal;
        }

        public IDataResult<bool> Buying(Guid id, BuyingSellingDTO dto)
        {
            var account = _accountDal.Get(u => u.UserId == id);
            var investmentAccount = _investmentAccountDal.Get(u=>u.UserId == id);
            if (account != null)
            {
                var share = ShareService.ServiceGetAsync(dto.ShareorCryptoId);
                decimal value = Convert.ToDecimal(dto.Amount) * share.Result.Price;
                if (account.AmountInAccount >= value)
                {
                    Portfolio portfolio;
                    var cryptoPortfolio = _portfolioDal.Get(c => c.ShareCertificateId == dto.ShareorCryptoId);
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
                            InvestmentAccountId = account.Id,
                            UserId = id,
                            ShareCertificateId = dto.ShareorCryptoId,
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
                    investmentAccount.PortfolioValue = portfolioTotalValue;
                    _investmentAccountDal.Update(investmentAccount);
                    _accountDal.Update(account);
                    AccountMovement movement = new AccountMovement()
                    {
                        UserId = id,
                        AccountId = account.Id,
                        AssetId = dto.ShareorCryptoId,
                        Value = value,
                        TransactionType = "Alış"
                    };
                    _accountMovementDal.Add(movement);
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
        public decimal Value(Guid id)
        {
            decimal totalPortfolioValue = 0;
            var result = _portfolioDal.GetAll(u => u.UserId == id);
            foreach (var item in result)
            {
                var share = ShareService.ServiceGetAsync(item.ShareCertificateId);
                item.CurrentValue = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(share.Result.Price);

                decimal received = item.ReceivedValue;
                decimal current = item.CurrentValue;
                decimal fark = current - received;
                decimal yuzdeFark = (fark / received) * 100;
                item.ValueChange = yuzdeFark;

                totalPortfolioValue += item.CurrentValue;

                _portfolioDal.Update(item);
            }
            var account = _investmentAccountDal.Get(u => u.UserId == id);
            account.PortfolioValue = totalPortfolioValue;
            _investmentAccountDal.Update(account);
            return totalPortfolioValue;
        }
        public IDataResult<List<Portfolio>> GetPortfolio(Guid id)
        {
            decimal totalPortfolioValue = 0;
            var result = _portfolioDal.GetAll(u => u.UserId == id);
            foreach (var item in result)
            {
                var share = ShareService.ServiceGetAsync(item.ShareCertificateId);
                item.CurrentValue = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(share.Result.Price);

                decimal received = item.ReceivedValue;
                decimal current = item.CurrentValue;
                decimal fark = current - received;
                decimal yuzdeFark = (fark / received) * 100;
                item.ValueChange = yuzdeFark;

                totalPortfolioValue += item.CurrentValue;

                _portfolioDal.Update(item);
            }
            var account = _investmentAccountDal.Get(u => u.UserId == id);
            account.PortfolioValue = totalPortfolioValue;
            _investmentAccountDal.Update(account);
            var portfolios = _portfolioDal.GetAll(u => u.UserId == id);
            return new SuccessDataResult<List<Portfolio>>(portfolios);
        }

        public IDataResult<List<Portfolio>> Refresh(Guid id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<bool> Selling(Guid id, BuyingSellingDTO dto)
        {
            var portfolio = _portfolioDal.Get(u => u.UserId == id && u.ShareCertificateId == dto.ShareorCryptoId);
            var account = _accountDal.Get(u => u.UserId == id);
            var investmentAccount = _investmentAccountDal.Get(u => u.UserId == id);
            var share = ShareService.ServiceGetAsync(dto.ShareorCryptoId);
            if (account != null)
            {
                if (portfolio != null)
                {
                    if (portfolio.Amount >= dto.Amount)
                    {
                        decimal value = Convert.ToDecimal(dto.Amount) * share.Result.Price;
                        investmentAccount.PortfolioValue -= value;
                        account.AmountInAccount += value;
                        var movement = new AccountMovement()
                        {
                            UserId = id,
                            AccountId = account.Id,
                            AssetId = dto.ShareorCryptoId,
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
                        _investmentAccountDal.Update(investmentAccount);
                        _accountDal.Update(account);
                        _accountMovementDal.Add(movement);
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
            else
            {
                return new ErrorDataResult<bool>(false); ;
            }

        }
    }

}
