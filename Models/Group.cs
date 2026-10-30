using System.Collections.Generic;
using System.Linq;

namespace MyWebSite.Models
{
    public class Group
    {
        public string Title { get; set; }
        public int Course { get; set; }

        // Закрытый список студентов
        private readonly List<Student> students = new();

        // Доступ только для чтения снаружи
        public IReadOnlyCollection<Student> Students => students.AsReadOnly();

        public Group(string title, int course)
        {
            Title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("Название группы обязательно.", nameof(title))
                : title.Trim();
            if (course is < 1 or > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(course), "Курс должен быть от 1 до 6.");
            }
            Course = course;
        }

        public void AddStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            if (!students.Any(s => s.StudentId == student.StudentId))
            {
                students.Add(student);
            }
        }

        public void RemoveStudent(string studentId)
        {
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student != null)
            {
                students.Remove(student);
            }
        }
    }
}