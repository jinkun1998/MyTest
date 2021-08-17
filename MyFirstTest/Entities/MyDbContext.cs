using MyFirstTest.Models;
using System.Data.Entity;

namespace MyFirstTest.Entities
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
