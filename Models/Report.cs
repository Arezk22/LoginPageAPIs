using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginPageAPIs.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ChildName { get; set; }

        public string ExerciseName { get; set; }

        [Required]
        [Range(0, 100)]
        public int Mark { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public int ChildId { get; set; } // المفتاح الأجنبي (بدون [ForeignKey] هنا)

        [ForeignKey("ChildId")] // 👈 هنا تكتبها، تربط الـ Navigation بـ ChildId
        public LoginRequest Child { get; set; }
    }
}
