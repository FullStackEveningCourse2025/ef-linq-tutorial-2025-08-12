namespace ef_linq_tutorial_2025_08_12.Endpoints
{
    public static class AdminEndpoints
    {
        public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
        {
            var admin = app.MapGroup("/api/admin").WithTags("Admin");
            admin.MapPost("/reset", () =>
            {

                return Results.Ok(new { message = "Database reset and reseeded" });


            })
            .WithSummary("Delete, recreate, and reseed the database")
            .WithOpenApi();

            return app;
        }
    }

}
