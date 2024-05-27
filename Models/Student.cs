using System.ComponentModel;
using System.Security.Claims;

namespace SkolaLinq.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [DisplayName("Namn")]
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
