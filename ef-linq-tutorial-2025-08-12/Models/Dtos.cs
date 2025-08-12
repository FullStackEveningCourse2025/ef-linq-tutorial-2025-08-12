namespace ef_linq_tutorial_2025_08_12.Models
{

    public record StudentBasicDto(int Id, string FullName, string City);
    public record StudentSummaryDto(int Id, string FullName, string City, int CourseCount);
    public record CourseGradeDto(int CourseId, string Title, Grade? Grade);
    public record StudentWithCoursesDto(int Id, string FullName, List<CourseGradeDto> Courses);
    public record StudentCourseDto(string Student, string Course);
    public record StudentCourseGradeDto(string Student, string Course, Grade? Grade);
    public record LeftJoinResultDto(string Student, bool IsEnrolled);
    public record CoursePopularityDto(string Title, int StudentCount, double? AvgScore);
    public record DepartmentCreditStatsDto(string Department, int CourseCount, int TotalCredits);
    public record CourseWithInstructorDto(int Id, string Title, string? Instructor, int EnrollmentCount);

}
