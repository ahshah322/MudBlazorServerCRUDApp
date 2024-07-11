using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazorServerCRUD.Data;
using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly NavigationManager _navigationManager;

        public StudentService(ApplicationDbContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
            _context.Database.EnsureCreated();
        }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public async Task CreateStudent(Student student)
        {
            student.Gender = null;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var dbStudent = await _context.Students.FindAsync(id);
            if (dbStudent == null)
                throw new Exception("No Student here. :/");

            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
        }

        //public async Task<Student> GetSingleStudent(int id)
        //{
        //    var Student = await _context.Students.FindAsync(id);
        //    if (Student == null)
        //        throw new Exception("No Student here. :/");
        //    return Student;
        //}
        public async Task<Student> GetSingleStudent(int id)
        {
            var Student = await _context.Students
                                .FindAsync(id);

            if (Student == null)
                throw new Exception("No Student here. :/");
            return Student;
        }

        public async Task GetStudents()
        {
            Students = await _context.Students
                            .Include(s => s.Grades)
                            .ToListAsync();
        }
        public async Task GetGrades()
        {
            //Grades = await _context.Grades.ToListAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            var dbStudent = await _context.Students
                                  .Include(s => s.Grades)
                                  .FirstOrDefaultAsync(s => s.Id == student.Id);
            if (dbStudent == null)
                throw new Exception("No Student here. :/");

            dbStudent.Name = student.Name;
            dbStudent.Email = student.Email;
            dbStudent.DateOfBirth = student.DateOfBirth;
            dbStudent.GenderId = student.GenderId;

            await _context.SaveChangesAsync();
        }
    }
}