using IdentityServer4.Validation;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HaalCentraal.IdentityServer.Helpers
{
    /// <summary>
    /// In IdentityServer4 is het mogelijk om properties toe te kennen aan een client.
    /// Deze properties worden gebruikt om claims toe te kennen aan clients die gebruik maken van client credentials flow
    /// </summary>
    public class AddCustomClaimsForClientCredentialFlow : ICustomTokenRequestValidator
    {
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var client = context.Result.ValidatedRequest.Client;

            foreach(var key in client.Properties.Keys)
            {
                context.Result.ValidatedRequest.ClientClaims.Add(new Claim(key, client.Properties[key]));
            }

            context.Result.ValidatedRequest.Client.ClientClaimsPrefix = string.Empty;
        }
    }
}
