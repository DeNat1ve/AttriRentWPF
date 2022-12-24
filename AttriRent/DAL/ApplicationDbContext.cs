using AttriRent.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.DAL
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Models.Attribute> attributes { get; set; }
        public DbSet<UserPassword> passwords { get; set; }
        public DbSet<UserRole> roles { get; set; }

        public ApplicationDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("User Id=postgres;Password=12345;Host=localhost;Port=5432;Database=AttriRent");
            optionsBuilder.UseSqlServer("Server=USER-PC\\MSSQLSERVER01;Database=AttriRent;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
