using Microsoft.EntityFrameworkCore;
using PhoneShop.Models;

namespace PhoneShop.Services
{
    public class PhoneDbContext : DbContext
    {
        public PhoneDbContext(DbContextOptions<PhoneDbContext> options) : base(options) { }

        public DbSet<Phone> Phone { get; set; }
    }
    public class CartAppDbContext : DbContext
    {
        public CartAppDbContext(DbContextOptions<CartAppDbContext> options) : base(options) { }
        public DbSet<CartItem> CartItems { get; set; }
    }
    public class ConfOrdersDbContext : DbContext
    {
        public ConfOrdersDbContext(DbContextOptions<ConfOrdersDbContext> options) : base(options) { }

        public DbSet<CartItem> ConfirmOrders { get; set; }
    }
    public class HelpDbContext : DbContext
    {
        public HelpDbContext(DbContextOptions<HelpDbContext> options) : base(options) { }

        public DbSet<HelpModel> Help { get; set; }
    }
}
