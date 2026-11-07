namespace AcademyGraduationSystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }  // عشان نعرف المورد مرتبط بأي فئة

        public Category Category { get; set; }

        public List<Offer> Offers { get; set; }
            = new List<Offer>();
    }
}