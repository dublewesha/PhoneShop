using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using PhoneShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class KatalogModel : PageModel
    {
        private readonly IPhoneRepository phoneRepository;

        public KatalogModel(IPhoneRepository phoneRepository)
        {
            this.phoneRepository = phoneRepository;
        }
        public IEnumerable<Phone> Phones { get; set; }
        public Phone Phone { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public void OnGet()
        {
            Phones = phoneRepository.Search(SearchTerm);
        }
    }
}
