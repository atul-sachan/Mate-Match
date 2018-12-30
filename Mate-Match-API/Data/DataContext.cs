using Mate_Match_API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Mate_Match_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}