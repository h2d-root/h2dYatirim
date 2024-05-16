using h2dYatirim.Infrastructure.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace h2dYatirim.Infrastructure
{
    public static class ServiceRegistrationInfrastructure
    {

        public static IServiceCollection InfrastructureRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IPortfolioDal, PortfolioDal>();
            services.AddScoped<IAccountMovementDal, AccountMovementDal>();
            services.AddScoped<ICryptoAccountDal, CryptoAccountDal>();
            services.AddScoped<IWalletDal, WalletDal>();

            return services;
        }
    }
}
