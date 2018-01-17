using Microsoft.EntityFrameworkCore;

namespace WebSocketServer.Models
{
    public sealed class AppContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
         
        public AppContext()
        {
            Database.EnsureCreated();
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;User Id=SA;Password=123456;Database=api_dev");
        }
    }
}