using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Models;
using MyWebSite.Services;

namespace MyWebSite.Controllers;

[Authorize(Roles = "Admin")]
public sealed class CollegeController(CollegeDataStore store) : Controller
{
    public IActionResult Index(string section = "dashboard")
    {
        var settings = store.Settings();
        var model = new CollegePageViewModel
        {
            Section = NormalizeSection(section),
            CollegeName = settings.Name,
            AcademicYear = settings.Year,
            Students = store.Students(),
            Teachers = store.Teachers(),
            Groups = store.Groups(),
            Grades = store.Grades(),
            Attendance = store.Attendance()
        };

        if (model.Section == "dashboard")
        {
            var reportManager = new ReportManager();
            model.Report = reportManager.GenerateReport(
                $"Учебный год: {model.AcademicYear}\nСтудентов: {model.Students.Count}\n" +
                $"Преподавателей: {model.Teachers.Count}\nГрупп: {model.Groups.Count}\n" +
                $"Оценок: {model.Grades.Count}\nЗаписей посещаемости: {model.Attendance.Count}");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddStudent(StudentRecord student) =>
        Execute(() =>
        {
            student.StudentId = Guid.NewGuid().ToString("N");
            store.AddStudent(student);
        }, "students");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteStudent(string id) =>
        Execute(() => store.DeleteStudent(id), "students");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddTeacher(TeacherRecord teacher) =>
        Execute(() => store.AddTeacher(teacher), "teachers");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddGroup(GroupRecord group) =>
        Execute(() => store.AddGroup(group), "groups");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddGrade(GradeRecord grade) =>
        Execute(() => store.AddGrade(grade), "grades");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteGrade(string id) =>
        Execute(() => store.DeleteGrade(id), "grades");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetAttendance(AttendanceRecord attendance) =>
        Execute(() => store.SetAttendance(attendance), "attendance");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateSettings(string name, string year) =>
        Execute(() => store.UpdateSettings(name, year), "admin");

    private IActionResult Execute(Action action, string section)
    {
        try
        {
            action();
            TempData["Message"] = "Изменения сохранены.";
        }
        catch (ArgumentException exception)
        {
            TempData["Error"] = exception.Message;
        }
        catch (InvalidOperationException exception)
        {
            TempData["Error"] = exception.Message;
        }

        return RedirectToAction(nameof(Index), new { section });
    }

    private static string NormalizeSection(string section) =>
        section is "students" or "teachers" or "groups" or "grades" or "attendance" or "admin"
            ? section
            : "dashboard";
}
