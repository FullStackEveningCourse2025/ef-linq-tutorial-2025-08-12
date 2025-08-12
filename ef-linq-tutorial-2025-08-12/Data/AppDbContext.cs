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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(b =>
            {
                b.OwnsOne(s => s.Name, nb =>
                {
                    nb.Property(p => p.First).HasMaxLength(50);
                    nb.Property(p => p.Last).HasMaxLength(50);
                    nb.HasIndex(p => new { p.Last, p.First });
                });
                b.OwnsOne(s => s.Address, ab =>
                {
                    ab.Property(p => p.Street).HasMaxLength(100);
                    ab.Property(p => p.City).HasMaxLength(50);
                    ab.Property(p => p.Country).HasMaxLength(50);
                   
                });

               
            });

            modelBuilder.Entity<Instructor>(b =>
            {
                b.OwnsOne(i => i.Name);
               b.OwnsOne(i => i.Address);
            });

            modelBuilder.Entity<Department>(b =>
            {
                b.Property(d => d.Budget).HasColumnType("decimal(12,2)");
                b.HasIndex(d => d.Name).IsUnique();
            });

            modelBuilder.Entity<Course>(b =>
            {
                b.Property(c => c.Title).HasMaxLength(100);
                b.HasOne(c => c.Department)
                    .WithMany(d => d.Courses)
                    .HasForeignKey(c => c.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(c => c.Instructor)
                    .WithMany(i => i.Courses)
                    .HasForeignKey(c => c.InstructorId);
            });

            modelBuilder.Entity<Enrollment>(b =>
            {
                b.HasKey(e => new { e.StudentId, e.CourseId });
                b.HasOne(e => e.Student)
                    .WithMany(s => s.Enrollments)
                    .HasForeignKey(e => e.StudentId);
                b.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId);
            });

        }
    }
}
