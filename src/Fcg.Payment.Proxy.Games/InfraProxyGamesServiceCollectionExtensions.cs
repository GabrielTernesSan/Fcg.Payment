using Fcg.Payment.Proxy.Games.Client.Interfaces;
using Fcg.Payment.Proxy.Games.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Fcg.Payment.Proxy.Games
{
    public static class InfraProxyGamesServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraProxyGames(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientGames, ClientGames>();

            services.AddHttpClient<IClientGames, ClientGames>();

            services.Configure<GamesConfiguration>(configuration.GetSection("GamesConfiguration"));
            services.TryAddSingleton<IValidateOptions<GamesConfiguration>, GamesConfigurationValidation>();

            return services;
        }
    }
}
