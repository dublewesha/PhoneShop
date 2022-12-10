using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class RoleName
    {
        public string Name { get; set; }
    }
    [Authorize(Roles = "admin")]
    public class AdminRoleCreateModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminRoleCreateModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [BindProperty]
        public RoleName RoleName { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var res = await roleManager.CreateAsync(new IdentityRole { Name = RoleName.Name, NormalizedName = RoleName.Name.ToUpper() });
                if (res.Succeeded)
                {
                    TempData["SM"] = "Новая роль успешно создана";
                    return RedirectToPage("/AdminRoleCreate");
                }
                TempData["DM"] = "Новая роль не создана, присутствуют следующие ошибки:";
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }



        public void OnGet()
        {

        }
    }
}
