namespace MudBlazorServerCRUD.Model
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string Subject { get; set; }
        public int Score { get; set; }

        // Foreign key
        public int StudentId { get; set; }

        // Navigation property
        public Student Student { get; set; }
    }
}
