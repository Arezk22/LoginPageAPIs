using System.ComponentModel.DataAnnotations;

namespace LoginPageAPIs.DTOs
{
    public class ScoreCreateDto
    {
        [Required]
        public int ChildId { get; set; }

        [Required, StringLength(100)]
        public string ExerciseName { get; set; }

        [Required, Range(0, 100)]
        public int Mark { get; set; }
    }
}
