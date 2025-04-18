using CodexGoddess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodexGoddess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Cars> Cars { get; set; }

        public DbSet<UsersDB> UsersDB { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
