namespace MyWebSite.Models;

public class AttendanceManager
{
    private readonly Dictionary<(string StudentId, DateTime Date), bool> attendance = new();

    public void AddAttendance(string studentId, DateTime date, bool present)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentId);
        attendance.Add((studentId, date.Date), present);
    }

    public void UpdateAttendance(string studentId, DateTime date, bool present)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentId);
        attendance[(studentId, date.Date)] = present;
    }

    public (int Present, int Absent) GetStatistics(string studentId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentId);
        var records = attendance.Where(entry => entry.Key.StudentId == studentId);
        var present = records.Count(entry => entry.Value);
        return (present, records.Count() - present);
    }
}
