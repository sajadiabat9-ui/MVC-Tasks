
namespace masterpiececodefirst.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int? CityID { get; set; }

        public int? ChosenMajorID { get; set; }

        public string Role { get; set; }

        public City City { get; set; }

        public Major ChosenMajor { get; set; }
    }
}
