using System.ComponentModel;

namespace SkolaLinq.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        [DisplayName("Klass")]
        public string ClassName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
