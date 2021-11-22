using Microsoft.EntityFrameworkCore;
using Models;
using WebAPI.Models;

namespace FileData
{
    public class AssDbContext : DbContext
    {
        public DbSet<Adult> Adults  { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource = C:\Users\Nacia\RiderProjects\Assignment3Main\WebAPI\Database.db");
        }

        
    }
}