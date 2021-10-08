using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Entieties
{
    public class LibraryDbContext : DbContext
    {
        private  string _connectionString = "Data Source=DARK\\SQLEXPRESS;Database=LibraryDb;Trusted_Connection=True";

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Library>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Publication>()
               .Property(p => p.Title)
               .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
