using Fcg.Payment.Proxy.User.Client.Interfaces;
using Fcg.Payment.Proxy.User.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Fcg.Payment.Proxy.User
{
    public static class InfraProxyUserServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraProxyUser(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientUser, ClientUser>();

            services.AddHttpClient<IClientUser, ClientUser>();

            services.Configure<UserConfiguration>(configuration.GetSection("UserConfiguration"));
            services.TryAddSingleton<IValidateOptions<UserConfiguration>, UserConfigurationValidation>();

            return services;
        }
    }
}
