using Microsoft.EntityFrameworkCore;
using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasMany(s => s.Grades)
            .WithOne(g => g.Student)
            .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>().HasData(
                    new Grade { GradeId = 1, Subject = "Math", Score = 90, StudentId = 1 },
                    new Grade { GradeId = 2, Subject = "Science", Score = 85, StudentId = 1 },
                    new Grade { GradeId = 3, Subject = "Math", Score = 88, StudentId = 2 },
                    new Grade { GradeId = 4, Subject = "Science", Score = 92, StudentId = 2 }
                );
            modelBuilder.Entity<Student>().HasData(
                    new Student()
                    {
                        Id = 1,
                        Name = "ali@gmail.com",
                        Email = "ali@gmail.com",
                        GenderId = 1,
                        DateOfBirth = new DateTime(1996, 11, 22),
                    },
                    new Student()
                    {
                        Id = 2,
                        Name = "shabi",
                        Email = "shabi@gmail.com",
                        GenderId = 1,
                        DateOfBirth = new DateTime(2000, 9, 9),
                    },
                    new Student()
                    {
                        Id = 3,
                        Name = "zillay",
                        Email = "zillay@gmail.com",
                        GenderId = 1,
                        DateOfBirth = new DateTime(1998, 05, 05),
                    }, 
                    new Student()
                    {
                        Id = 4,
                        Name = "zulqar",
                        Email = "zulqar@gmail.com",
                        GenderId = 1,
                        DateOfBirth = new DateTime(1993, 11, 22),
                    },
                    new Student()
                    {
                        Id = 5,
                        Name = "elisa",
                        Email = "elisa@gmail.com",
                        GenderId = 2,
                        DateOfBirth = new DateTime(2000, 01, 01),
                    },
                    new Student()
                    {
                        Id = 6,
                        Name = "mehrub",
                        Email = "mehrub@gmail.com",
                        GenderId = 3,
                        DateOfBirth = new DateTime(1995, 01, 01),
                    }
                );

            modelBuilder.Entity<Gender>().HasData(
                    new Gender()
                    {
                        Id = 1,
                        Name = "Male",
                        NameA = "Man",
                        NameB = "M"
                    },
                    new Gender()
                    {
                        Id = 2,
                        Name = "Female",
                        NameA = "Woman",
                        NameB = "W"
                    },
                    new Gender()
                    {
                        Id = 3,
                        Name = "Other",
                        NameA = "Not Mentioned",
                        NameB = "O"
                    }
                );
        }
    }
}
