﻿using Core.Utilities.Results;
using h2dYatırım.Entities;

namespace h2dYatirim.Application.Interfaces
{
    public interface ICryptoAccountService
    {
        IDataResult<bool> AddAccount(Guid id);
        IDataResult<bool> RemoveAccount();
        IDataResult<CryptoAccount> GetAccount(Guid userId);
    }
}
