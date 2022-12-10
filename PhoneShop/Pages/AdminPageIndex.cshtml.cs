using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminPageModel : PageModel
    {
        
        public void OnGet()
        {

        }
    }
}
