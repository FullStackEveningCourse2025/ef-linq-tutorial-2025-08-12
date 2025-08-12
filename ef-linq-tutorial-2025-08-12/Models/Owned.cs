using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

namespace ef_linq_tutorial_2025_08_12.Models
{
    public record PersonName(string First, string Last)
    {
        public string Full => $"{First} {Last}";
    }

    public record Address(string Street, string City, string Country);

    public class Student
    {
        public int Id { get; set; }
        public PersonName Name { get; set; } = new("", "");
        public Address Address { get; set; } = new("", "", "");
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

    public enum Grade { A, B, C, D, F }
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public DateOnly EnrolledOn { get; set; }
        public Grade? Grade { get; set; }
    }

    public class Instructor
    {
        public int Id { get; set; }
        public PersonName Name { get; set; } = new("", "");
        public Address Address { get; set; } = new("", "", "");
        public DateOnly HiredOn { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Department
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; } = "";
        public decimal Budget { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }



}
