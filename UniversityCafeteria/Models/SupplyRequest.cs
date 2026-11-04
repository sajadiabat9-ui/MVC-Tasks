namespace UniversityCafeteria.Models
{
    public class SupplyRequest
    {
        public int RequestID { get; set; }

        public List<SupplyItem> Products { get; set; }

        public decimal Subtotal { get; set; }

        public decimal PreparationFee { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalTotal { get; set; }
    }
}