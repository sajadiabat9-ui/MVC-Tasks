using UniversityCafeteria2.Models;

namespace UniversityCafeteria2.CodeFirst.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
            = new List<PurchaseOrderItem>();
    }
}