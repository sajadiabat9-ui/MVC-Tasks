
namespace masterpiececodefirst.Models
{
    public class TawjihiResult
    {
        public int TawjihiResultID { get; set; }
        public int StudentID { get; set; }

        public decimal Average { get; set; }

        public string StreamName { get; set; }

        public DateTime SavedAt { get; set; }

        public Student Student { get; set; }
    }
}
