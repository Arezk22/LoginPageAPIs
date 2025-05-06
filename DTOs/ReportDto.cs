using System.ComponentModel.DataAnnotations;

namespace LoginPageAPIs.DTOs
{
    public class ReportDto
    {
        public string ChildName { get; set; }

        public string ExerciseName { get; set; }
       
        public int Mark { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}

