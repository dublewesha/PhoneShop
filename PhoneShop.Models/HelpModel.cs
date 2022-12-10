using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneShop.Models
{
    public class HelpModel
    {
        public int Id { get; set; }
        [Key]
        public string UserName { get; set; }
    }
}
