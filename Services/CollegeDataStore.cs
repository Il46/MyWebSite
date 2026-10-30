using Microsoft.EntityFrameworkCore;
using MyWebSite.Models;

namespace MyWebSite.Services;

public sealed class CollegeDataStore(AppDbContext db)
{
    public IReadOnlyList<StudentRecord> Students() =>
        db.Students.AsNoTracking().OrderBy(student => student.Surname).ThenBy(student => student.Name).ToArray();

    public IReadOnlyList<TeacherRecord> Teachers() =>
        db.Teachers.AsNoTracking().OrderBy(teacher => teacher.Surname).ThenBy(teacher => teacher.Name).ToArray();

    public IReadOnlyList<GroupRecord> Groups() =>
        db.Groups.AsNoTracking().OrderBy(group => group.Title).ToArray();

    public IReadOnlyList<GradeRecord> Grades() =>
        db.Grades.AsNoTracking().OrderByDescending(grade => grade.Date).ToArray();

    public IReadOnlyList<AttendanceRecord> Attendance() =>
        db.Attendance.AsNoTracking().OrderByDescending(record => record.Date).ToArray();

    public (string Name, string Year) Settings()
    {
        var profile = db.CollegeProfiles.AsNoTracking().SingleOrDefault(item => item.Id == 1);
        return profile is null
            ? ("Колледж цифровых технологий", "2026-2027")
            : (profile.CollegeName, profile.AcademicYear);
    }

    public void AddStudent(StudentRecord student)
    {
        var validatedStudent = new Student(student.StudentId, student.Name, student.Surname, student.Age, student.Group);
        student.Name = validatedStudent.Name;
        student.Surname = validatedStudent.Surname;
        student.Group = validatedStudent.Group;

        if (!db.Groups.AsNoTracking().Any(group => group.Title == student.Group))
        {
            throw new InvalidOperationException("Сначала создайте выбранную учебную группу.");
        }

        db.Students.Add(student);
        db.SaveChanges();
    }

    public void DeleteStudent(string id)
    {
        var student = db.Students.Find(id);
        if (student is null)
        {
            return;
        }

        db.Students.Remove(student);
        db.SaveChanges();
    }

    public void AddTeacher(TeacherRecord teacher)
    {
        var validatedTeacher = new Teacher(teacher.Name, teacher.Surname, teacher.Age, teacher.Subject, teacher.Experience);
        teacher.Name = validatedTeacher.Name;
        teacher.Surname = validatedTeacher.Surname;
        teacher.Subject = validatedTeacher.Subject;
        db.Teachers.Add(teacher);
        db.SaveChanges();
    }

    public void AddGroup(GroupRecord group)
    {
        var validatedGroup = new Group(group.Title, group.Course);
        group.Title = validatedGroup.Title;

        if (db.Groups.Any(existing => existing.Title.ToLower() == group.Title.ToLower()))
        {
            throw new InvalidOperationException("Такая группа уже существует.");
        }

        db.Groups.Add(group);
        db.SaveChanges();
    }

    public void AddGrade(GradeRecord grade)
    {
        var gradeBook = new GradeBook();
        gradeBook.AddGrade(grade.Value);
        if (string.IsNullOrWhiteSpace(grade.Subject))
        {
            throw new ArgumentException("Предмет обязателен.");
        }
        grade.Subject = grade.Subject.Trim();
        grade.Date = grade.Date.Date;

        if (!db.Students.Any(student => student.StudentId == grade.StudentId))
        {
            throw new InvalidOperationException("Выберите существующего студента.");
        }

        db.Grades.Add(grade);
        db.SaveChanges();
    }

    public void DeleteGrade(string id)
    {
        var grade = db.Grades.Find(id);
        if (grade is null)
        {
            return;
        }

        db.Grades.Remove(grade);
        db.SaveChanges();
    }

    public void SetAttendance(AttendanceRecord record)
    {
        record.Date = record.Date.Date;
        if (!db.Students.Any(student => student.StudentId == record.StudentId))
        {
            throw new InvalidOperationException("Выберите существующего студента.");
        }

        var existing = db.Attendance.Find(record.StudentId, record.Date);
        if (existing is null)
        {
            db.Attendance.Add(record);
        }
        else
        {
            existing.Present = record.Present;
        }
        db.SaveChanges();
    }

    public void UpdateSettings(string name, string year)
    {
        var settings = new CollegeSettings();
        settings.UpdateSettings(name, year);
        var profile = db.CollegeProfiles.Find(1);
        if (profile is null)
        {
            profile = new CollegeProfileRecord { Id = 1 };
            db.CollegeProfiles.Add(profile);
        }

        profile.CollegeName = settings.CollegeName;
        profile.AcademicYear = settings.AcademicYear;
        db.SaveChanges();
    }

    public void EnsureSeedData()
    {
        if (!db.CollegeProfiles.Any())
        {
            db.CollegeProfiles.Add(new CollegeProfileRecord());
        }

        if (!db.Students.Any() && !db.Groups.Any())
        {
            var group = "ИС-21";
            var studentId = Guid.NewGuid().ToString("N");
            db.Groups.Add(new GroupRecord { Title = group, Course = 2 });
            db.Students.Add(new StudentRecord
            {
                StudentId = studentId,
                Name = "Анна",
                Surname = "Иванова",
                Age = 19,
                Group = group
            });
            db.Teachers.Add(new TeacherRecord
            {
                Name = "Сергей",
                Surname = "Петров",
                Age = 42,
                Subject = "Информатика",
                Experience = 16
            });
            db.Grades.Add(new GradeRecord
            {
                StudentId = studentId,
                Subject = "Информатика",
                Value = 5,
                Date = DateTime.Today
            });
            db.Attendance.Add(new AttendanceRecord
            {
                StudentId = studentId,
                Date = DateTime.Today,
                Present = true
            });
        }

        db.SaveChanges();
    }
}
