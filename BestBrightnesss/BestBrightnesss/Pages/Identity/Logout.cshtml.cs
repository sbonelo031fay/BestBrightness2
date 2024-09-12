using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BestBrightnesss.Identity
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            var externalProvider = User.FindFirst(ClaimTypes.AuthenticationMethod)?.Value;
            if (externalProvider != null && externalProvider == "AzureAD")
            {
                var postLogoutRedirectUri = Url.Page("/Index", pageHandler: null, values: null, protocol: Request.Scheme);
                var logoutUrl = $"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri={HttpUtility.UrlEncode(postLogoutRedirectUri)}";

                return Redirect(logoutUrl);
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            return LocalRedirect(returnUrl);
        }
    }
}
