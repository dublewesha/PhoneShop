using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class InputRegisterModel
    {
        [Required(ErrorMessage = "Логин не может быть пустым")]
        public string Login { get; set; }
        [EmailAddress(ErrorMessage = "Email заполнен некорректно (example@example.com")]
        [Required(ErrorMessage = "Email Не может быть пустым")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [MinLength(8, ErrorMessage = "Минимальная длина пароля: 8 символов"), MaxLength(32, ErrorMessage = "Максимальная длина пароля: 32 символа")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfPassword { get; set; }
        [Phone(ErrorMessage = "Введите корректный номер телефона \nПример: (+7-999-123-45-67)")]
        [Required(ErrorMessage = "Заполните номер телефона")]
        public string PhoneNumber { get; set; }

    }
    public class RegisterFormModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public InputRegisterModel InputModel { get; set; }
        public RegisterFormModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    PhoneNumber = InputModel.PhoneNumber,
                    UserName = InputModel.Login,
                    Email = InputModel.Email
                };
                var result = await userManager.CreateAsync(user, InputModel.Password);
                if (result.Succeeded /*|| result1.Succeeded*/)
                {
                    await signInManager.SignInAsync(user, false);
                    await userManager.AddToRoleAsync(user, "user");
                    return RedirectToPage("/Shared/Index");
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                        ModelState.AddModelError("", "Логин " + user.UserName +" уже используется");
                    else
                        ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
        public void OnGet() { }
    }
}
