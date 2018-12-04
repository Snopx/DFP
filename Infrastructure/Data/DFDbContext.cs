using System;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DFDbContext:DbContext
    {
        public DFDbContext(DbContextOptions options) :base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
