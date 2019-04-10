using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enum;
using Domain.Model;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class DFDbContext : DbContext, IDbContext
    {
        public DFDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(b => b.Account).IsUnique();
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                ID = 1,
                Account = "admin",
                Name = "DarrenFang",
                Password = SecurityOfCrypt.Encode("123qwe"),
                Gender = Gender.Male
            });
        }
    }
}
