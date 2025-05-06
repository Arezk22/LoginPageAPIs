using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginPageAPIs.DTOs
{
    public class ScoreReadDto
    {
        
        public string ChildName { get; set; }      
        public string FatherName { get; set; }        
        public string ExerciseName { get; set; }        
        public int Mark { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
               
    }
}
