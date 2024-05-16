﻿using Core.DataAccess.EntityFramework;
using h2dYatırım.DataAccess;
using h2dYatırım.Entities;

namespace h2dYatirim.Infrastructure.Entities
{
    public class WalletDal : efEntitiyRepositoryBase<Wallet, h2dYatirimDBContext>, IWalletDal { }
}
