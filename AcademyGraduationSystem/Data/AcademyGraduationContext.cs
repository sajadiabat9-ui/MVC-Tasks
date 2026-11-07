using Microsoft.EntityFrameworkCore;
using AcademyGraduationSystem.Models;

namespace AcademyGraduationSystem.Data
{
    public class AcademyGraduationContext : DbContext
    {
        public AcademyGraduationContext(DbContextOptions<AcademyGraduationContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}