using UniversityCafeteria2.Models;

namespace UniversityCafeteria2.CodeFirst.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
            = new List<PurchaseOrderItem>();
    }
}