using LuxuryCarsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarsApp.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($@"Server=DESKTOP-AELUB3Q\SQLEXPRESS;Database=Books;Trusted_Connection=True;");
            }
        }
    }
}
