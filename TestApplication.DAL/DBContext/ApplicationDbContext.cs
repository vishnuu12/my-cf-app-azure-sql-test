using Microsoft.EntityFrameworkCore;
using TestApplication.Models.Entities;
using TestApplication.Models.Models;

namespace TestApplication.DAL.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}