namespace MudBlazorServerCRUD.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Marks { get; set; } 
        public DateTime? DateOfBirth { get; set; } = DateTime.Today;

        // Navigation property
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public Gender Gender { get; set; }
        public int GenderId { get; set; } = 1;
    }
}
