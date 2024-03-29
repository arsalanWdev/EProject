using EProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EProject.Data
{
    public class ApplicationDbContext: IdentityDbContext<DefaultUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
             
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Books> Bookss { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }
        public DbSet<Order> Orders { get; set; }
        
    }
}
