using MedTox_WebAPI.models; 
using Microsoft.EntityFrameworkCore;

namespace MedTox_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
