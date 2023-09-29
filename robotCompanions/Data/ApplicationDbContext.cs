using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using robotCompanions.Models;

namespace robotCompanions.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Robots> Robots { get; set; }
        public DbSet<shoppingCart> shoppingCart { get; set; }
       
        public DbSet<cartDetails> cartDetails { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<orderDetails> orderDetails { get; set; }

        public DbSet<orderStatus> orderStatus { get; set; }
        public DbSet<robotSize> robotSize { get; set; }



    }
}