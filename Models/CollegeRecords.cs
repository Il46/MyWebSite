namespace MyWebSite.Models;

public sealed class StudentRecord
{
    public string StudentId { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int Age { get; set; }
    public string Group { get; set; } = "";
}

public sealed class TeacherRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int Age { get; set; }
    public string Subject { get; set; } = "";
    public int Experience { get; set; }
}

public sealed class GroupRecord
{
    public string Title { get; set; } = "";
    public int Course { get; set; }
}

public sealed class GradeRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string StudentId { get; set; } = "";
    public string Subject { get; set; } = "";
    public int Value { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
}

public sealed class AttendanceRecord
{
    public string StudentId { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today;
    public bool Present { get; set; }
}

public sealed class CollegeProfileRecord
{
    public int Id { get; set; } = 1;
    public string CollegeName { get; set; } = "Колледж цифровых технологий";
    public string AcademicYear { get; set; } = "2026-2027";
}

public sealed class CollegePageViewModel
{
    public string Section { get; set; } = "dashboard";
    public string CollegeName { get; set; } = "Колледж";
    public string AcademicYear { get; set; } = "";
    public IReadOnlyList<StudentRecord> Students { get; set; } = [];
    public IReadOnlyList<TeacherRecord> Teachers { get; set; } = [];
    public IReadOnlyList<GroupRecord> Groups { get; set; } = [];
    public IReadOnlyList<GradeRecord> Grades { get; set; } = [];
    public IReadOnlyList<AttendanceRecord> Attendance { get; set; } = [];
    public string? Report { get; set; }
    public string? Message { get; set; }
}
