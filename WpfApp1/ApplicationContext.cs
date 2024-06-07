using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;
using ClassLibrary;

namespace WpfApp1
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=students.db");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne<Class>(d => d.StClass)
                .WithMany(dm => dm.Students)
                .HasForeignKey(dkey => dkey.ClassId);
            modelBuilder.Entity<Class>()
               .HasMany(dm => dm.Students)
               .WithOne(d => d.StClass)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Class>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
