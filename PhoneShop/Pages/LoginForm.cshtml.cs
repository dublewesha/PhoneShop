using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class InputLoginModel
    {
        [Required(ErrorMessage = "Логин не заполнен")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Пароль не заполнен")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class LoginFormModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        public LoginFormModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputLoginModel InputModel { get; set; }
        public SignInManager<IdentityUser> SignInManager;
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityuser = await signInManager.PasswordSignInAsync(InputModel.Login, InputModel.Password, InputModel.RememberMe, false);
                if (identityuser.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("/Shared/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                TempData["DM"] = "Логин или пароль некорректны";
                ModelState.AddModelError("", "Логин или пароль некорректны");
            }
            TempData["DM"] = "Логин или пароль некорректны";
            return Page();
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Shared/Index");
            }
            return Page();
        }
    }
}
