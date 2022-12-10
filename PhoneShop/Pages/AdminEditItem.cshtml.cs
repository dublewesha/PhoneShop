using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneShop.Models;
using PhoneShop.Services;
using System;
using System.IO;

namespace PhoneShop.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminEditItemModel : PageModel
    {
        private readonly IPhoneRepository phoneRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminEditItemModel(IPhoneRepository phoneRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.phoneRepository = phoneRepository;
        }
        public IFormFile Photo { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        [BindProperty]
        public Phone Phone { get; set; }
        public void OnGet(int id)
        {
            Phone = phoneRepository.GetPhoneById(id);
        }
        public void OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Phone.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", Phone.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Phone.PhotoPath = ProcessUploadedFile();
                }
                if (Photo1 != null)
                {
                    if (Phone.PhotoPath1 != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", Phone.PhotoPath1);
                        System.IO.File.Delete(filePath);
                    }
                    Phone.PhotoPath1 = ProcessUploadedFile();
                }
                if (Photo2 != null)
                {
                    if (Phone.PhotoPath2 != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", Phone.PhotoPath2);
                        System.IO.File.Delete(filePath);
                    }
                    Phone.PhotoPath2 = ProcessUploadedFile();
                }
                Phone = phoneRepository.Update(Phone);
                ViewData["sm"] = "Изменения успешно сохранены";
                OnGet(id);
            }
            else
            {
                ViewData["dm"] = "Сохранение не удалось, присутствуют следующие ошибки:";
                OnGet(id);
            }
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if(Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }
            return uniqueFileName;
        }
    }
}
