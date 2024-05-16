using h2dYatirim.Application.Classes;
using h2dYatirim.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace h2dYatirim.Application
{
    public static class ServiceRegistrationApplication
    {

        public static IServiceCollection ApplicationRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IPortfolioService, PortfolioManager>();
            services.AddScoped<ICryptoAccountService, CryptoAccountManager>();
            services.AddScoped<IAccountMovementService, AccountMovementManager>();
            services.AddScoped<IWalletService, WalletManager>();
            return services;
        }
    }
}
