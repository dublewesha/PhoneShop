using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.Services
{
    public interface IConfirmOrdersRepository
    {
        public IEnumerable<CurrOrdersModel> GetAllConfirmOrders();
    }
}
