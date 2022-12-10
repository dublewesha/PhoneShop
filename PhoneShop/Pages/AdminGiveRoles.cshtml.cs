using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminGiveRolesModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
