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

            students.MapGet("", async Task<Ok<List<Student>>>(AppDbContext db) =>
            {
                var q = db.Students.AsQueryable();

                var data = await q.ToListAsync();

                return TypedResults.Ok(data);
            }).WithName("GetStudents").WithOpenApi();

            return app;
        }
    }
}
