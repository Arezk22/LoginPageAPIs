using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoginPageAPIs.Models
{
    public class Score
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("ChildId")]
        public int ChildId { get; set; } // Foreign key من جدول LoginRequest

        [Required]
        [StringLength(100)]
        public string ExerciseName { get; set; }

        [Required]
        [Range(0, 100)]
        public int Mark { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        
        [BindNever]
        public LoginRequest Child { get; set; }
    }
}
