using UniversityCafeteria2.Models;

namespace UniversityCafeteria2.CodeFirst.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}