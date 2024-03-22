using EProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
             
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Books> Bookss { get; set; }
    }
}
