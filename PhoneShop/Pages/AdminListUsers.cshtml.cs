using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminListUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        public AdminListUsersModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        [BindProperty]
        public List<IdentityUser> UsersList { get; set; }
        public List<IdentityUser> RolesList { get; set; }
        public List<string> RolesOfUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                List<IdentityUser> p = userManager.Users.ToList();
                UsersList = p.Where(x => x.UserName.Contains(Search)).ToList();
            }
            else
            {
                UsersList = userManager.Users.ToList();
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var curr_us = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(curr_us);
            return RedirectToPage("/AdminListUsers");
        }
    }
}
