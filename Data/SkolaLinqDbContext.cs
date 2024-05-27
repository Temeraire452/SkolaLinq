using Microsoft.EntityFrameworkCore;
using SkolaLinq.Models;

namespace SkolaLinq.Data
{
    public class SkolaLinqDbContext : DbContext
    {
        public SkolaLinqDbContext(DbContextOptions<SkolaLinqDbContext> options) : base(options)
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera relationen mellan Teacher och Course
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId);

            // Konfigurera relationen mellan Course och Student
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .UsingEntity<CourseStudent>(
                    j => j
                        .HasOne(cs => cs.Student)
                        .WithMany(s => s.CourseStudents)
                        .HasForeignKey(cs => cs.StudentId),
                    j => j
                        .HasOne(cs => cs.Course)
                        .WithMany(c => c.CourseStudents)
                        .HasForeignKey(cs => cs.CourseId),
                    j =>
                    {
                        j.HasKey(cs => new { cs.CourseId, cs.StudentId });
                        j.ToTable("CourseStudent");
                    });

            // Konfigurera relationen mellan Class och Student
            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);
        }
    }
}