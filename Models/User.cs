namespace LoginPageAPIs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; } // هذا الحقل لتخزين المسار
    }
}
