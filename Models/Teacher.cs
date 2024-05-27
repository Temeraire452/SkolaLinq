using System.Collections.Generic;
using System.ComponentModel;

namespace SkolaLinq.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        [DisplayName("Namn")]
        public string TeacherName { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
