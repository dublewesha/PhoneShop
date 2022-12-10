using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ItemId { get; set; }
        
    }
}
