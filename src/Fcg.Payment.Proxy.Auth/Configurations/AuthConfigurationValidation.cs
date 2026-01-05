using Microsoft.Extensions.Options;

namespace Fcg.Payment.Proxy.Auth.Configurations
{
    public class AuthConfigurationValidation : IValidateOptions<AuthConfiguration>
    {
        public ValidateOptionsResult Validate(string? name, AuthConfiguration options)
        {
            if (string.IsNullOrWhiteSpace(options.Url))
            {
                return ValidateOptionsResult.Fail("Undefined 'Url' in configuration section UserUrl");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
