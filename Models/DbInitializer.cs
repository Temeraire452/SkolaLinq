using SkolaLinq.Data;

namespace SkolaLinq.Models
{
    public class DbInitializer
    {
        public static void Initialize(SkolaLinqDbContext context)
        {
            context.Database.EnsureCreated();

            // Kolla om det redan finns data.
            if (context.Students.Any())
            {
                return;
            }

            var teachers = new Teacher[]
            {
                new Teacher{TeacherName="Susanne"},
                new Teacher{TeacherName="Angelica"},
                new Teacher{TeacherName="Reidar"},
                new Teacher{TeacherName="Tobias"}
            };
            foreach (Teacher t in teachers)
            {
                context.Teachers.Add(t);
            }
            context.SaveChanges();

            var classes = new Class[]
            {
                new Class{ClassName="3A"},
                new Class{ClassName="3B"},
                new Class{ClassName="3C"},
                new Class{ClassName="3D"}
            };
            foreach (Class c in classes)
            {
                context.Classes.Add(c);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseName="Mattematik", TeacherId=teachers[0].TeacherId},
                new Course{CourseName="Historia", TeacherId=teachers[1].TeacherId},
                new Course{CourseName="Programmering 1", TeacherId=teachers[2].TeacherId},
                new Course{CourseName="Engelska", TeacherId=teachers[3].TeacherId}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{StudentName="Alexander", ClassId=classes[0].ClassId},
                new Student{StudentName="Anna", ClassId=classes[1].ClassId},
                new Student{StudentName="Anton", ClassId=classes[2].ClassId},
                new Student{StudentName="Alice", ClassId=classes[3].ClassId}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            foreach (var course in courses)
            {
                foreach (var student in students)
                {
                    var courseStudent = new CourseStudent
                    {
                        CourseId = course.CourseId,
                        StudentId = student.StudentId
                    };
                    context.CourseStudents.Add(courseStudent);
                }
            }
            context.SaveChanges();
        }
    }
}
    
