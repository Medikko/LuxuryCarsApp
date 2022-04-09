using LuxuryCarsApp.Data.Models.UserModels;
using LuxuryCarsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LuxuryCarsApp.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($@"Server=DESKTOP-AELUB3Q\SQLEXPRESS;Database=LuxuryCars;Trusted_Connection=True;");
            }
        }
    }
}
