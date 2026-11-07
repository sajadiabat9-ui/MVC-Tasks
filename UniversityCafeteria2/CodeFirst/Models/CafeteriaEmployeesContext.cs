using Microsoft.EntityFrameworkCore;
using UniversityCafeteria2.CodeFirst.Models;

namespace UniversityCafeteria2.CodeFirst
{
    public class CafeteriaEmployeesContext : DbContext
    {
        public CafeteriaEmployeesContext(DbContextOptions<CafeteriaEmployeesContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}