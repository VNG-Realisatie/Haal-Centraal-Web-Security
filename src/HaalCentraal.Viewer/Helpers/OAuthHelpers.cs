using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Helpers
{
    public static class OAuthHelpers
    {
        public static void AddOpenIdConnect(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.ResponseType = OpenIdConnectResponseType.Code;
                options.UsePkce = Convert.ToBoolean(configuration["OpenIdConnect:usePkce"]);

                options.Authority = configuration["OpenIdConnect:authority"];
                options.ClientId = configuration["OpenIdConnect:clientid"];
                options.ClientSecret = configuration["OpenIdConnect:clientsecret"];

                var scopes = configuration["OpenIdConnect:scopes"];
                foreach (var scope in scopes.Split(' '))
                {
                    options.Scope.Add(scope);
                }

                if (configuration["OpenIdConnect:scopes"].Contains("openid"))
                {
                    // delete unneeded claims
                    options.ClaimActions.DeleteClaims(new[] { "sid", "idp", "s_hash", "auth_time" });

                    options.ClaimActions.MapUniqueJsonKey("bag-permission", "bag-permission");
                    options.ClaimActions.MapUniqueJsonKey("brk-permission", "brk-permission");
                    options.ClaimActions.MapUniqueJsonKey("brp-permission", "brp-permission");
                    options.ClaimActions.MapUniqueJsonKey("gemeente", "gemeente");

                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Events = new OpenIdConnectEvents
                    {
                        OnUserInformationReceived = context =>
                        {
                            var claims = new[] { new Claim("scope", context.ProtocolMessage.Scope) };

                            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(context.Principal.Identity, claims));

                            return Task.CompletedTask;
                        }
                    };
                }

                options.SaveTokens = true;
            });
        }

        public static void AddAttributeBasedAccessControl(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanAccessBAG", policyBuilder =>
                {
                    policyBuilder
                        .RequireAuthenticatedUser()
                        .RequireScope("BAG");
                });
                options.AddPolicy("CanAccessBRK", policyBuilder =>
                {
                    policyBuilder
                        .RequireAuthenticatedUser()
                        .RequireScope("BRK");
                });
                options.AddPolicy("CanAccessBRP", policyBuilder =>
                {
                    policyBuilder
                        .RequireAuthenticatedUser()
                        .RequireScope("BRP")
                        .RequireClaim("gemeente");
                });
            });
        }
    }
}
