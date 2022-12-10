using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class LogoutFormModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        public LogoutFormModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Shared/Index");
        }
    }
}
