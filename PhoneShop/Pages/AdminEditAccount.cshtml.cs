using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class UserRolesViewModel
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
    [Authorize(Roles = "admin")]
    public class AdminEditAccountModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminEditAccountModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IdentityUser IdentityUser { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public string Message { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        public List<UserRolesViewModel> RolesModel { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            IdentityUser = await userManager.FindByIdAsync(id);
            AllRoles = roleManager.Roles.ToList();
            List<UserRolesViewModel> p1 = new List<UserRolesViewModel>();
            for (int i = 0; i < AllRoles.Count; i++)
            {
                p1.Add(new UserRolesViewModel
                {
                    RoleName = AllRoles[i].ToString(),
                    IsSelected = await userManager.IsInRoleAsync(IdentityUser, AllRoles[i].ToString())
                });
            }
            RolesModel = p1;
            return Page();
        }

        public async Task<IActionResult> OnPostAdd(string id, string rolename)
        {
            IdentityUser = await userManager.FindByIdAsync(id);
            try
            {
                var result = await userManager.AddToRoleAsync(IdentityUser, rolename);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return Page();
                }

                IdentityUser = await userManager.FindByIdAsync(id);
                AllRoles = roleManager.Roles.ToList();
                List<UserRolesViewModel> p1 = new List<UserRolesViewModel>();
                for (int i = 0; i < AllRoles.Count; i++)
                {
                    p1.Add(new UserRolesViewModel
                    {
                        RoleName = AllRoles[i].ToString(),
                        IsSelected = await userManager.IsInRoleAsync(IdentityUser, AllRoles[i].ToString())
                    });
                }
                RolesModel = p1;
                ViewData["sm"] = "Роль успешно добавлена";
                return Page();
            }
            catch
            {
                IdentityUser = await userManager.FindByIdAsync(id);
                AllRoles = roleManager.Roles.ToList();
                List<UserRolesViewModel> p1 = new List<UserRolesViewModel>();

                for (int i = 0; i < AllRoles.Count; i++)
                {
                    p1.Add(new UserRolesViewModel
                    {
                        RoleName = AllRoles[i].ToString(),
                        IsSelected = await userManager.IsInRoleAsync(IdentityUser, AllRoles[i].ToString())
                    });
                }
                RolesModel = p1;
                ViewData["dm"] = "Ошибка при добавлении роли, проверьте правильность написания роли и наличия её у пользователя";
                return Page();
            }
            //return RedirectToPage("/AdminEditAccount", new { id = id });
        }
        public async Task<IActionResult> OnPostDelete(string id, string rolename)
        {
            IdentityUser = await userManager.FindByIdAsync(id);
            try
            {
                var result = await userManager.RemoveFromRoleAsync(IdentityUser, rolename);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return Page();
                }
                IdentityUser = await userManager.FindByIdAsync(id);
                AllRoles = roleManager.Roles.ToList();
                List<UserRolesViewModel> p1 = new List<UserRolesViewModel>();
                for (int i = 0; i < AllRoles.Count; i++)
                {
                    p1.Add(new UserRolesViewModel
                    {
                        RoleName = AllRoles[i].ToString(),
                        IsSelected = await userManager.IsInRoleAsync(IdentityUser, AllRoles[i].ToString())
                    });
                }
                RolesModel = p1;
                ViewData["sm"] = "Роль успешно удалена";
                return Page();
                //return RedirectToPage("/AdminEditAccount", new { id = id });
            }
            catch
            {
                IdentityUser = await userManager.FindByIdAsync(id);
                AllRoles = roleManager.Roles.ToList();
                List<UserRolesViewModel> p1 = new List<UserRolesViewModel>();
                for (int i = 0; i < AllRoles.Count; i++)
                {
                    p1.Add(new UserRolesViewModel
                    {
                        RoleName = AllRoles[i].ToString(),
                        IsSelected = await userManager.IsInRoleAsync(IdentityUser, AllRoles[i].ToString())
                    });
                }
                RolesModel = p1;
                ViewData["dm"] = "Ошибка при удалении роли, проверьте правильность написания роли и наличия её у пользователя";
                return Page();
                //return RedirectToPage("/AdminEditAccount" , new {id = id});
            }
        }
    }
}
