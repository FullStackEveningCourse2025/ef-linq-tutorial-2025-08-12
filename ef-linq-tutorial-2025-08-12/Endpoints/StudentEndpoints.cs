using System;

namespace ef_linq_tutorial_2025_08_12.Endpoints
{
    public static class StudentEndpoints
    {
        public static IEndpointRouteBuilder MapStudentsEndpoints(this IEndpointRouteBuilder app)
        {
            var students = app.MapGroup("/api/students").WithTags("Students");

            students.MapGet("", () =>
            {
                return Results.Ok();
            }).WithName("GetStudents").WithOpenApi();

            return app;
        }
    }
}
