using System.ComponentModel.DataAnnotations;

namespace LoginPageAPIs.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string ChildName { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
