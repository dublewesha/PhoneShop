using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Models;
using PhoneShop.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PhoneShop.Pages
{
    public class CartModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string PhotoPath { get; set; }
        public string Color { get; set; }
        public string ItemCartId { get; set; }
        public int HiddenId { get; set; }
    }
    [Authorize]
    public class CartShopModel : PageModel
    {

        private readonly IPhoneRepository phoneRepository;
        private readonly ICartRepository cartRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public CartShopModel(IPhoneRepository phoneRepository, ICartRepository cartRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.phoneRepository = phoneRepository;
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [BindProperty]
        public List<CartModel> Model { get; set; }
        [BindProperty]
        public decimal Summary { get; set; }
        public Phone Phone { get; set; }
        [BindProperty]
        public List<CartItem> CartItems { get; set; }
        public IEnumerable<CartModel> CartModels { get; set; }

        public void OnPostDeleteItemFromCart(int id)
        {
            if (id != 0)
            {
                cartRepository.DeleteItemFromCart(userManager.GetUserName(User), id);
            }
            OnGet();
        }
        public async Task<IActionResult> OnPostConfirmOrder()
        {
            try
            {
                MailAddress admin_adress = new MailAddress("dubleweb@gmail.com");
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("dubleweb@gmail.com", "jfqrpdqbrpptbqck");
                smtpClient.Credentials = basicAuthenticationInfo;
                IdentityUser current_user = await userManager.GetUserAsync(User);
                MailAddress to_curr_user = new MailAddress(current_user.Email);
                MailMessage newMail = new MailMessage(admin_adress, to_curr_user);
                newMail.Subject = "Заказ в интернет-магазине PhoneShop, информация о заказе";
                newMail.SubjectEncoding = System.Text.Encoding.UTF8;
                newMail.Body = "" +
                    "<h1>Благодарим за заказ</h1>" +
                    "<p>Добрый день, " + current_user.UserName + "</p>" +
                    "<div style=\"font-size:15px\">" +
                    "Здесь можно будет посмотреть подробную информацию о заказе" +
                    "</div>";
                newMail.BodyEncoding = System.Text.Encoding.UTF8;
                newMail.IsBodyHtml = true;
                smtpClient.Send(newMail);


            }
            catch { }
            return RedirectToAction("OnGet");
        }

        public void OnGet()
        {
            string curr_username = userManager.GetUserName(User);
            CartItems = cartRepository.GetAllItemsFromCart(curr_username).ToList();
            List<CartModel> p = new List<CartModel>();
            decimal summary = 0;
            for (int i = 0; i < CartItems.Count(); i++)
            {
                Phone = phoneRepository.GetPhoneById(Convert.ToInt32(CartItems[i].ItemId));
                p.Add(new CartModel
                {
                    Id = Phone.Id,
                    ItemName = Phone.Name,
                    Price = Phone.Price,
                    Capacity = Phone.Capacity,
                    PhotoPath = Phone.PhotoPath,
                    Color = Phone.Color,
                    ItemCartId = CartItems[i].ItemId,
                    HiddenId = CartItems[i].Id
                });
                summary += Phone.Price;
            }
            Model = p;
            Summary = summary;
        }
    }
}
