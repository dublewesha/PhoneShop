using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.Services
{
    public interface IPhoneRepository
    {
        IEnumerable<Phone> GetPhoneByAttributes(string name, int capacity, string color);
        Phone GetPhoneById(int? id);
        Phone Update(Phone updatedPhone);
        IEnumerable<Phone> GetAllTopPhones();
        IEnumerable<Phone> Search(string searchTerm);
        Phone Delete(int id);
    }
}
