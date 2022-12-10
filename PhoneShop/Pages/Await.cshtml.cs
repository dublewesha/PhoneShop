using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class AwaitModel : PageModel
    {
        public IActionResult OnGet()
        {
            Task.Delay(2500);

            return RedirectToPage("Index");
        }
    }
}
