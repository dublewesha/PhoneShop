using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneShop.Services
{
    public class SQLPhoneRepository : IPhoneRepository
    {
        private readonly PhoneDbContext context;
        public SQLPhoneRepository(PhoneDbContext context)
        {
            this.context = context;
        }
        public Phone Delete(int id)
        {
            var phone_to_delete = context.Phone.Find(id);
            if (phone_to_delete != null)
            {
                context.Phone.Remove(phone_to_delete);
                context.SaveChanges();
            }
            return phone_to_delete;
        }
        public IEnumerable<Phone> GetAllTopPhones()
        {
            return context.Phone;
        }
        public Phone GetPhoneById(int? id)
        {
            if (id.HasValue)
                return context.Phone.Find(id.Value);
            return context.Phone.Find(1);
        }
        public IEnumerable<Phone> GetPhoneByAttributes(string name, int capacity, string color)
        {
            return context.Phone.Where(x => x.Name == name & x.Capacity == capacity & x.Color == color);
        }

        public IEnumerable<Phone> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return context.Phone;
            return context.Phone.Where(x => x.Name.Contains(searchTerm));
        }

        public Phone Update(Phone updatedPhone)
        {
            var phone = context.Phone.Attach(updatedPhone);
            phone.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return updatedPhone;
        }
    }
}
