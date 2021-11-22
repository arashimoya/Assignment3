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
            optionsBuilder.UseSqlite(@"DataSource = C:\Users\arasi\RiderProjects\DNPAssignments\Assigment3\WebAPI\Assignment.db");
        }

        
    }
}