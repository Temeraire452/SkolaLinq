using System.ComponentModel;

namespace SkolaLinq.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [DisplayName("Kurs")]
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
