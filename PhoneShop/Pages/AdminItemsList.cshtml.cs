using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PhoneShop.Models;
using PhoneShop.Services;
using System.Collections.Generic;
using System.Linq;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminItemsListModel : PageModel
    {
        private readonly IPhoneRepository phoneRepository;
        public AdminItemsListModel(IPhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        [BindProperty]
        public List<Phone> Model { get; set; }
        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Search))
            {
                Model = phoneRepository.GetAllTopPhones().ToList();
            }
            else
            {
                Model = phoneRepository.Search(Search).ToList();
            }
        }
    }
}
