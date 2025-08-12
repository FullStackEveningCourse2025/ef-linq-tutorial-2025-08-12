using ef_linq_tutorial_2025_08_12.Models;
using Microsoft.EntityFrameworkCore;

namespace ef_linq_tutorial_2025_08_12.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
