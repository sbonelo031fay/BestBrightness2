using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace BestBrightnesss.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            var callbackUrl = Url.Action("Index", "Home", null, Request.Scheme);
            var properties = new AuthenticationProperties
            {
                RedirectUri = callbackUrl
            };
            return SignOut(
                properties,
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
