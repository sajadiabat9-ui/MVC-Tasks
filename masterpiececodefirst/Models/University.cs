
namespace masterpiececodefirst.Models
{
    public class University
    {
        public int UniversityID { get; set; }

        public string UniversityName { get; set; }

        public string UniversityNameAr { get; set; }

        public int CityID { get; set; }

        public string UniType { get; set; }

        public int FoundedYear { get; set; }

        public City City { get; set; }
    }
}
