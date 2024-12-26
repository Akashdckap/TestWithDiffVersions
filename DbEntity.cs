using Assess2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assess2
{
    public class DbEntity:DbContext
    {
        public DbEntity(DbContextOptions<DbEntity>options) : base(options)
        {
        }
        public DbSet<Directors> Directors { get; set; }
    }
}
