using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
