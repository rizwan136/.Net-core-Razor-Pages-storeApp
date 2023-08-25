using Microsoft.EntityFrameworkCore;

namespace RazorPagesTutorial.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
