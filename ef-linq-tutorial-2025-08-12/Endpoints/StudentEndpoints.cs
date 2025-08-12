using ef_linq_tutorial_2025_08_12.Data;
using ef_linq_tutorial_2025_08_12.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;

namespace ef_linq_tutorial_2025_08_12.Endpoints
{
    public static class StudentEndpoints
    {
        public static IEndpointRouteBuilder MapStudentsEndpoints(this IEndpointRouteBuilder app)
        {
            var students = app.MapGroup("/api/students").WithTags("Students");

            students.MapGet("", async Task<Ok<List<StudentBasicDto>>>(
                AppDbContext db, string? lastStarts, int page =1, int size=5
                ) =>
            {
                var q = db.Students.AsNoTracking().AsQueryable();


                if (!string.IsNullOrEmpty(lastStarts))
                {
                    q = q.Where(s => EF.Functions.Like(s.Name.Last, $"{lastStarts}%"));
                }

                var data = await q
                .OrderBy(s => s.Name.Last).ThenBy(s => s.Name.First)
                .Skip((page - 1) * size).Take(size)
                .Select(s=>new StudentBasicDto(s.Id, s.Name.Full, s.Address.City))
                .ToListAsync();

                return TypedResults.Ok(data);
            }).WithName("GetStudents").WithSummary("Filter and sort students").WithOpenApi();

            return app;
        }
    }
}
