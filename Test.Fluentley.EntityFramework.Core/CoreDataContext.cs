using Microsoft.EntityFrameworkCore;
using Test.Fluentley.EntityFramework.Core.Entity;

namespace Test.Fluentley.EntityFramework.Core
{
    public class CoreDataContext : DbContext
    {
        public CoreDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}