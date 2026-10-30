using System;

namespace MyWebSite.Models
{
    public class Subject
    {
        private string title;
        private int hours;
        private Teacher? teacher;

        public string Title => title;
        public int Hours => hours;
        public Teacher? Teacher => teacher;

        public Subject(string title, int hours)
        {
            this.title = string.IsNullOrWhiteSpace(title)
                ? throw new ArgumentException("Название предмета обязательно.", nameof(title))
                : title.Trim();
            if (hours <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), "Количество часов должно быть больше нуля.");
            }
            this.hours = hours;
        }

        public void AssignTeacher(Teacher newTeacher)
        {
            if (newTeacher == null)
            {
                throw new ArgumentNullException(nameof(newTeacher), "Преподаватель не может быть null");
            }
            teacher = newTeacher;
        }
    }
}