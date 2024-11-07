using Microsoft.EntityFrameworkCore;
using Web.Data.Models.Account;

namespace Web.Data.Contexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

    }
}