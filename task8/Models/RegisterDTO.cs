using System.ComponentModel.DataAnnotations;

namespace task8.Models
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        [Required][Compare("Password")]
[DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
