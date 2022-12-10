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
        [Required(ErrorMessage = "����� �� ����� ���� ������")]
        public string Login { get; set; }
        [EmailAddress(ErrorMessage = "Email �������� ����������� (example@example.com")]
        [Required(ErrorMessage = "Email �� ����� ���� ������")]
        public string Email { get; set; }
        [Required(ErrorMessage = "������ �� ����� ���� ������")]
        [MinLength(8, ErrorMessage = "����������� ����� ������: 8 ��������"), MaxLength(32, ErrorMessage = "������������ ����� ������: 32 �������")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "������ �� ���������")]
        public string ConfPassword { get; set; }
        [Phone(ErrorMessage = "������� ���������� ����� �������� \n������: (+7-999-123-45-67)")]
        [Required(ErrorMessage = "��������� ����� ��������")]
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
                        ModelState.AddModelError("", "����� " + user.UserName +" ��� ������������");
                    else
                        ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
        public void OnGet() { }
    }
}
