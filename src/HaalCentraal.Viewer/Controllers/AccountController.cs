using HaalCentraal.Viewer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading.Tasks;

namespace HaalCentraal.Viewer.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AccountViewModel();
            vm.Tokens.Add(OpenIdConnectParameterNames.IdToken, await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken));
            vm.Tokens.Add(OpenIdConnectParameterNames.AccessToken, await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken));
            foreach(var claim in User.Claims)
            {
                vm.Claims.Add(claim.Type, claim.Value);
            }

            return View(vm);
        }

        public async Task LogoutAsync()
        {
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
