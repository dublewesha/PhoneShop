using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.IdentityModel.Tokens;
using PhoneShop.Models;
using PhoneShop.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PhoneShop.Pages
{
    public class PhoneSetupModel : PageModel
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public PhoneSetupModel(IPhoneRepository phoneRepository, ICartRepository cartRepository, UserManager<IdentityUser> userManager)
        {
            _phoneRepository = phoneRepository;
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public IEnumerable<Phone> Phones { get; set; }

        [BindProperty]
        public Phone Phone { get; set; }
        public IActionResult OnGet(int id, string name, int capacity, string color)
        {
            if (id != 0)
            {
                Phone = _phoneRepository.GetPhoneById(id);
                return Page();
            }
            else
            {
                Phones = _phoneRepository.GetPhoneByAttributes(name, capacity, color);
                try { 
                    Phone = Phones.ElementAt(0);
                    return Page();
                }
                catch { 
                    return RedirectToPage("/Katalog");
                }
            }
        }

        public IActionResult OnPost(string name, int capacity, string color)
        {
            
            Phones = _phoneRepository.GetPhoneByAttributes(name, capacity, color);
            Phone = Phones.ElementAt(0);
            string username = _userManager.GetUserName(User);
            int id = Phone.Id;
            _cartRepository.AddItemToCart(username, id);


            ViewData["SM"] = "Товар добавлен в корзину";
            return Page();
        }
    }
}
