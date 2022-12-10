using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Models;
using PhoneShop.Services;
using System.Runtime.InteropServices;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminDeleteItemModel : PageModel
    {
        private readonly IPhoneRepository phoneRepository;
        public AdminDeleteItemModel(IPhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
        }
        [BindProperty]
        public Phone Phone { get; set; }
        public IActionResult OnGet(int id)
        {
            Phone = phoneRepository.GetPhoneById(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            Phone = phoneRepository.GetPhoneById(Phone.Id);
            return RedirectToPage("/AdminItemsList");
        }
    }
}
