using Microsoft.Extensions.Options;

namespace Fcg.Payment.Proxy.User.Configurations
{
    public class UserConfigurationValidation : IValidateOptions<UserConfiguration>
    {
        public ValidateOptionsResult Validate(string? name, UserConfiguration options)
        {
            if (string.IsNullOrWhiteSpace(options.Url))
            {
                return ValidateOptionsResult.Fail("Undefined 'Url' in configuration section UserUrl");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
