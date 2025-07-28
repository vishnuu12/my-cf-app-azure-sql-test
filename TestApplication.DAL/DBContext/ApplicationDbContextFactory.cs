using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.DAL.DBContext
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Put your real connection string here or load from environment/config if you want
            optionsBuilder.UseSqlServer("Server=tcp:btsteamserver.database.windows.net,1433;Initial Catalog=myfirsttestdb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=''Active Directory Default';");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
