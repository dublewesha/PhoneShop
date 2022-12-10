using Microsoft.AspNetCore.Identity;
using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneShop.Services
{
    public class SQLConfirmRepository : IConfirmOrdersRepository
    {
        private readonly ConfOrdersDbContext context;
        public SQLConfirmRepository(ConfOrdersDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CurrOrdersModel> GetAllConfirmOrders()
        {
            throw new NotImplementedException();
        }
        //username, email, phonenumber
        /*public IEnumerable<CurrOrdersModel> GetAllConfirmOrders()
        {
            /*IEnumerable<CartItem> confirmorders = context.ConfirmOrders.OrderBy(x => x.UserName);
            List<CurrOrdersModel> currOrderslist = new List<CurrOrdersModel>();
            for (int i = 0; i < confirmorders.Count(); i++)
            {
                if (currOrderslist.Contains())
                currOrderslist.Add(new CurrOrdersModel
                {

                });
            }
            
            
        }*/
    }
}
