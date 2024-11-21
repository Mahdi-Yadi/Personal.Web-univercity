using Microsoft.EntityFrameworkCore;
using Web.Data.Models.Account;
using Web.Data.Models.Projects;
using Web.Data.Models.Setting;

namespace Web.Data.Contexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}