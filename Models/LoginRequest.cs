using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LoginPageAPIs.Models
{
    public class LoginRequest
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Child Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Child Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Child Name must start with a capital letter and contain only letters.")]
        public string ChildName { get; set; }


        [Required(ErrorMessage = "Father Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Father Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Father Name must start with a capital letter and contain only letters.")]
        public string FatherName { get; set; }


        [Required]
        [RegularExpression(@"^[^@\s]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address.")]
        public string Email { get; set; }

        [JsonIgnore]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }
        public string ProfileImagePath { get; set; }               
    }
}
