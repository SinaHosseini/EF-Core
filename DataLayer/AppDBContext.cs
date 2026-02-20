using DataLayer.Mapping;
using DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDBContext : DbContext
    {
        //public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        //{
        //}

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ILVS7D3\\SQLEXPRESS;Database=TestDB;User Id=sa;Password=123; Trusted_Connection=True; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("DBO");
            //foreach(var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //modelBuilder.Entity<User>(config =>
            //{
            //    config.HasKey(c => c.UserId);
            //    config.Property(c => c.FullName)
            //    .HasDefaultValue("[Name] + ' ' + [Family]");
            //});

            //modelBuilder.Entity<User>()
            //    .Property(b => b.Name).IsRequired();

            //modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
