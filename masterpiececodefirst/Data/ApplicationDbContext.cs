
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using masterpiececodefirst.Models;

namespace masterpiececodefirst.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Major> Majors { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<TawjihiResult> TawjihiResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<University>()
                .HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.City)
                .WithMany()
                .HasForeignKey(s => s.CityID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.ChosenMajor)
                .WithMany()
                .HasForeignKey(s => s.ChosenMajorID);

            modelBuilder.Entity<TawjihiResult>()
                .HasOne(t => t.Student)
                .WithOne()
                .HasForeignKey<TawjihiResult>(t => t.StudentID);
        }
    }
}
