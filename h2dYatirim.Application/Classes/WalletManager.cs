using Core.Utilities.Results;
using h2dYatirim.Application.DTOs;
using h2dYatirim.Application.Interfaces;
using h2dYatırım.Entities;
using h2dYatirim.Infrastructure.Entities;
using h2dYatırım.Services;

namespace h2dYatirim.Application.Classes
{
    public class WalletManager : IWalletService
    {
        IWalletDal _walletDal;
        IAccountMovementDal _accountMovementDal;
        ICryptoAccountDal _cryptoAccountDal;
        IAccountDal _accountDal;

        public WalletManager(IAccountDal accountDal, ICryptoAccountDal cryptoAccountDal, IAccountMovementDal accountMovementDal, IWalletDal walletDal)
        {
            _cryptoAccountDal = cryptoAccountDal;
            _accountMovementDal = accountMovementDal;
            _walletDal = walletDal;
            _accountDal = accountDal;
        }

        public IDataResult<bool> Buying(Guid id, BuyingSellingDTO dto)
        {
            var account = _accountDal.Get(u => u.UserId == id);
            var cryptoAccount = _cryptoAccountDal.Get(u => u.UserId == id);
            if (account != null)
            {
                var coin = CoinService.ServiceGetAsync(dto.ShareorCryptoId);
                var coinPrice = Convert.ToDecimal(coin.Result.PriceUsd);
                decimal value = Convert.ToDecimal(dto.Amount) * coinPrice;
                if (account.AmountInAccount >= value)
                {
                    Wallet Wallet;
                    var cryptoWallet = _walletDal.Get(c => c.CryptoId == dto.ShareorCryptoId);
                    if (cryptoWallet != null)
                    {
                        cryptoWallet.Amount += dto.Amount;
                        cryptoWallet.CurrentValue += value;
                        cryptoWallet.ReceivedValue += value;
                        _walletDal.Update(cryptoWallet);
                        Wallet = cryptoWallet;
                    }
                    else
                    {
                        Wallet newWallet = new Wallet()
                        {
                            CryptoAccountId = account.Id,
                            UserId = id,
                            CryptoId = dto.ShareorCryptoId,
                            Amount = dto.Amount,
                            ValueChange = 0,
                            CurrentValue = value,
                            ReceivedValue = value
                        };
                        _walletDal.Add(newWallet);
                        Wallet = newWallet;
                    }
                    var Wallets = _walletDal.GetAll(u => u.UserId == id);
                    decimal WalletTotalValue = 0;
                    foreach (var item in Wallets)
                    {
                        WalletTotalValue += item.CurrentValue;
                    }
                    account.AmountInAccount -= value;
                    cryptoAccount.WalletValue = WalletTotalValue;
                    _cryptoAccountDal.Update(cryptoAccount);
                    AccountMovement movement = new AccountMovement()
                    {
                        UserId = id,
                        AccountId = account.Id,
                        AssetId = dto.ShareorCryptoId,
                        Value = value,
                        TransactionType = "Alış"
                    };
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

        public decimal Value(Guid id)
        {
            decimal totalWalletValue = 0;
            var result = _walletDal.GetAll(u => u.UserId == id);
            foreach (var item in result)
            {
                var crypto = CoinService.ServiceGetAsync(item.CryptoId);
                item.CurrentValue = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(crypto.Result.PriceUsd);

                decimal received = item.ReceivedValue;
                decimal current = item.CurrentValue;
                decimal fark = current - received;
                decimal yuzdeFark = (fark / received) * 100;
                item.ValueChange = yuzdeFark;

                totalWalletValue += item.CurrentValue;

                _walletDal.Update(item);
            }
            var account = _cryptoAccountDal.Get(u => u.UserId == id);
            account.WalletValue = totalWalletValue;
            _cryptoAccountDal.Update(account);
            return totalWalletValue;
        }

        public IDataResult<List<Wallet>> GetWallet(Guid id)
        {
            decimal totalWalletValue = 0;
            var result = _walletDal.GetAll(u => u.UserId == id);
            foreach (var item in result)
            {
                var crypto = CoinService.ServiceGetAsync(item.CryptoId);
                item.CurrentValue = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(crypto.Result.PriceUsd);

                decimal received = item.ReceivedValue;
                decimal current = item.CurrentValue;
                decimal fark = current - received;
                decimal yuzdeFark = (fark / received) * 100;
                item.ValueChange = yuzdeFark;

                totalWalletValue += item.CurrentValue;

                _walletDal.Update(item);
            }
            var account = _cryptoAccountDal.Get(u => u.UserId == id);
            account.WalletValue = totalWalletValue;
            _cryptoAccountDal.Update(account);
            var Wallets = _walletDal.GetAll(u => u.UserId == id);
            return new SuccessDataResult<List<Wallet>>(Wallets);
        }

        public IDataResult<List<Wallet>> Refresh(Guid id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<bool> Selling(Guid id, BuyingSellingDTO dto)
        {
            var Wallet = _walletDal.Get(u => u.UserId == id && u.CryptoId == dto.ShareorCryptoId);
            var account = _accountDal.Get(u => u.UserId == id);
            var cryptoAccount = _cryptoAccountDal.Get(u => u.UserId == id);
            var crypto = CoinService.ServiceGetAsync(dto.ShareorCryptoId);
            decimal coinPrice = Convert.ToDecimal(crypto.Result.PriceUsd);
            if (account != null)
            {
                if (Wallet != null)
                {
                    if (Wallet.Amount >= dto.Amount)
                    {
                        decimal value = Convert.ToDecimal(dto.Amount) * coinPrice;
                        cryptoAccount.WalletValue -= value;
                        account.AmountInAccount += value;
                        var movement = new AccountMovement()
                        {
                            UserId = id,
                            AccountId = account.Id,
                            AssetId = dto.ShareorCryptoId,
                            TransactionType = "Satış",
                            Value = value
                        };
                        if (Wallet.Amount > dto.Amount)
                        {
                            Wallet.CurrentValue -= value;
                            decimal ortalama = Wallet.ReceivedValue / Convert.ToDecimal(Wallet.Amount);
                            Wallet.ReceivedValue -= ortalama * Convert.ToDecimal(dto.Amount);
                            Wallet.Amount -= dto.Amount;
                            _walletDal.Update(Wallet);
                        }
                        else if (Wallet.Amount == dto.Amount)
                        {
                            Wallet.CurrentValue = 0;
                            decimal ortalama = 0;
                            Wallet.ReceivedValue = 0;
                            Wallet.Amount = 0;
                            _walletDal.Update(Wallet);
                        }
                        _cryptoAccountDal.Update(cryptoAccount);
                        _accountMovementDal.Add(movement);
                        _accountDal.Update(account);
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
