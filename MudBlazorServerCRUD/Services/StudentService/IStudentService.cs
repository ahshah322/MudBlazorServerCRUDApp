using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Services.StudentService
{
    public interface IStudentService
    {
        List<Student> Students { get; set; }
        List<Grade> Grades { get; set; }
        Task GetStudents();
        Task GetGrades();
        Task<Student> GetSingleStudent(int Id);
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int Id);
    }
}
