using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Models;
using PhoneShop.Services;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminCurrentsOrdersModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICartRepository cartRepository;
        private readonly IPhoneRepository phoneRepository;
        public AdminCurrentsOrdersModel(UserManager<IdentityUser> userManager, ICartRepository cartRepository, IPhoneRepository phoneRepository)
        {
            this.userManager = userManager;
            this.cartRepository = cartRepository;
            this.phoneRepository = phoneRepository;
        }
        public CurrOrdersModel CurrOrdersModel { get; set; }
        public Phone Phone { get; set; }

        public void OnGet()
        {
            
        }
    }
}
