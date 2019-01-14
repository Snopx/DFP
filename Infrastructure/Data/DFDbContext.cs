using System;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class DFDbContext:DbContext,IDbContext
    {
        public DFDbContext(DbContextOptions options) :base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
