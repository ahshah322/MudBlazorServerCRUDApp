using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazorServerCRUD.Data;
using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var Students = await _context.Students.ToListAsync();
            return Ok(Students);
        }

        // GET api/Student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetSingleStudent(int Id)
        {
            var StudentInDb = await _context.Students
                .SingleOrDefaultAsync(c => c.Id == Id);
            if (StudentInDb == null)
            {
                return NotFound("Sorry, no Student here!");
            }
            return Ok(StudentInDb);
        }

        // POST api/Student
        [HttpPost]
        public async Task<ActionResult<List<Student>>> CreateStudent(Student Student)
        {
            _context.Add(Student);
            await _context.SaveChangesAsync();

            return Ok(await GetStudentsInDb());
        }

        // PUT api/Student/1
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student Student, int Id)
        {
            var StudentInDb = await _context.Students
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (StudentInDb == null)
            {
                return NotFound("Sorry, no Student here!");
            }

            StudentInDb.Name = Student.Name;
            StudentInDb.DateOfBirth = Student.DateOfBirth;
            StudentInDb.Email = Student.Email;

            await _context.SaveChangesAsync();

            return Ok(await GetStudentsInDb());
        }

        // DELETE api/Student/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int Id)
        {
            var StudentInDb = await _context.Students
                .FirstOrDefaultAsync(c => c.Id == Id);
            if (StudentInDb == null)
            {
                return NotFound("Sorry, no Student here!");
            }

            _context.Students.Remove(StudentInDb);
            await _context.SaveChangesAsync();

            return Ok(await GetStudentsInDb());
        }

        private async Task<List<Student>> GetStudentsInDb()
        {
            return await _context.Students.ToListAsync();
        }
    }
}
