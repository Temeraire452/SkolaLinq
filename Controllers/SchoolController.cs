using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkolaLinq.Data;
using SkolaLinq.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaLinq.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SkolaLinqDbContext _context;

        public SchoolController(SkolaLinqDbContext context)
        {
            _context = context;
        }

        // Meny
        public IActionResult Index()
        {
            return View();
        }

        // Hämta alla lärare som undervisar i “programmering 1”
        public async Task<IActionResult> TeachersProgramming()
        {
            var teachers = await _context.Courses
                .Where(c => c.CourseName == "programmering 1")
                .Include(c => c.Teacher)
                .Select(c => c.Teacher)
                .ToListAsync();

            return View(teachers);
        }

        // Hämta alla elever med deras lärare
        public async Task<IActionResult> StudentsWithTeachers()
        {
            var students = await _context.Students
                .Include(s => s.Courses)
                .ThenInclude(c => c.Teacher)
                .ToListAsync();

            return View(students);
        }

        // Hämta alla elever som läser “programmering 1” och deras lärare
        public async Task<IActionResult> StudentsProgrammingTeachers()
        {
            var studentTeachers = await _context.CourseStudents
                .Where(cs => cs.Course.CourseName == "programmering 1")
                .Select(cs => new StudentTeacherViewModel
                {
                    StudentName = cs.Student.StudentName,
                    TeacherName = cs.Course.Teacher.TeacherName
                })
                .ToListAsync();

            return View(studentTeachers);
        }

        // Ändra “programmering 2” till “OOP”
        public IActionResult EditCourseName()
        {
            // Visa formuläret för att ändra kursnamnet
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourseName(string newCourseName)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseName == "programmering 1");

            if (course != null)
            {
                // Uppdatera kursnamnet med det nya
                course.CourseName = newCourseName;

                // Spara ändringarna till databasen
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Åtgärd för att ändra läraren i "programmering 1" från "Reidar" till "Tobias"
        public async Task<IActionResult> ChangeTeacher()
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherName == "Tobias");

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTeacher(string newTeacherName)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.CourseName == "programmering 1");

            if (course != null)
            {
                var newTeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherName == newTeacherName);

                if (newTeacher != null)
                {
                    course.Teacher = newTeacher;

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}