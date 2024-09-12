using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace BestBrightnesss.Pages.Profile
{
    [Authorize]
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterView Input { get; set; }
        public string Email { get; set; } = string.Empty;
        private readonly IProfileLogic _profile;
        public RegisterModel(IProfileLogic iProfileLogic)
        {
            _profile = iProfileLogic;
            Input = new RegisterView();
        }
        public void OnGet()
        
        {
            Email = User.Identity?.Name ?? string.Empty;

            Input.Email = Email;

        }
        public async Task<IActionResult> OnPost()
        {
            Email = User.Identity?.Name ?? string.Empty;

            Input.Email = Email;
            if (!ModelState.IsValid)
            {
                // If the form data is invalid, re-render the form with validation messages
                return Page();
            }
            await _profile.Register(Input);

            return RedirectToPage("/Index"); // Redirect to the home page or another page upon successful registration
        }
    }
}
