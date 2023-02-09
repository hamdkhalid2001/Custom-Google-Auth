using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Custom_Google_Auth.Models
{
    public class ApplicationDbContext : IdentityUserContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }

        public DbSet<Products> Products { get; set; }
    }
}
