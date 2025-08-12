using ef_linq_tutorial_2025_08_12.Models;
using System;

namespace ef_linq_tutorial_2025_08_12.Data
{
    public static class DbSeeder
    {
        private static readonly Random _rng = new(42);

        public static async Task SeedAsync(AppDbContext db)
        {
            var cs = new Department { Name = "Computer Science", Budget = 300_000m };
            var math = new Department { Name = "Mathematics", Budget = 180_000m };
            var lit = new Department { Name = "Literature", Budget = 120_000m };

            var instructors = new[]
            {
            new Instructor { Name = new("Ada","Lovelace"), Address = new("1 Analytical St","London","UK"), HiredOn = new DateOnly(2020,9,1) },
            new Instructor { Name = new("Edsger","Dijkstra"), Address = new("2 Graph Ln","Amsterdam","NL"), HiredOn = new DateOnly(2019,2,15) },
            new Instructor { Name = new("Mary","Somerville"), Address = new("5 Algebra Ave","Edinburgh","UK"), HiredOn = new DateOnly(2018,6,10) }
        };

            var courses = new[]
            {
            new Course { Title = "Intro to Programming", Credits = 5, Department = cs, Instructor = instructors[0] },
            new Course { Title = "Data Structures", Credits = 5, Department = cs, Instructor = instructors[1] },
            new Course { Title = "Databases", Credits = 5, Department = cs, Instructor = instructors[0] },
            new Course { Title = "Discrete Math", Credits = 5, Department = math, Instructor = instructors[1] },
            new Course { Title = "Linear Algebra", Credits = 5, Department = math, Instructor = instructors[2] },
            new Course { Title = "World Literature", Credits = 5, Department = lit, Instructor = instructors[2] },
        };

            var students = new List<Student>();
            string[] firsts = { "Bob", "Alice", "Charlie", "Diana", "Eve", "Frank", "Grace", "Heidi", "Ivan", "Judy", "Mallory", "Niaj", "Olivia", "Peggy", "Rupert" };
            string[] lasts = { "Brown", "Smith", "Johnson", "Williams", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Moore", "Jackson" };
            string[] cities = { "Dublin", "Cork", "Galway", "Belfast", "Limerick" };

            for (int i = 0; i < 20; i++)
            {
                var s = new Student
                {
                    Name = new(firsts[_rng.Next(firsts.Length)], lasts[_rng.Next(lasts.Length)]),
                    Address = new($"{_rng.Next(1, 200)} College Rd", cities[_rng.Next(cities.Length)], "Ireland"),
                    DateOfBirth = new DateOnly(_rng.Next(1999, 2006), _rng.Next(1, 13), _rng.Next(1, 28))
                };
                students.Add(s);
            }

            db.AddRange(cs, math, lit);
            db.AddRange(instructors);
            db.AddRange(courses);
            db.AddRange(students);
            await db.SaveChangesAsync();

            var allCourseIds = db.Courses.Select(c => c.Id).ToList();
            foreach (var s in db.Students)
            {
                var taking = _rng.Next(1, 5);
                var picks = allCourseIds.OrderBy(_ => _rng.Next()).Take(taking).ToList();
                foreach (var cid in picks)
                {
                    db.Enrollments.Add(new Enrollment
                    {
                        StudentId = s.Id,
                        CourseId = cid,
                        EnrolledOn = new DateOnly(2025, 9, _rng.Next(1, 28)),
                        Grade = _rng.Next(0, 4) switch
                        {
                            0 => Grade.A,
                            1 => Grade.B,
                            2 => Grade.C,
                            3 => Grade.D,
                            _ => null
                        }
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
