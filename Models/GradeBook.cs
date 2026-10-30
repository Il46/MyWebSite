using System;
using System.Collections.Generic;

namespace MyWebSite.Models
{
    public class GradeBook
    {
        private readonly List<int> grades = new();

        public IReadOnlyList<int> Grades => grades.AsReadOnly();

        public void AddGrade(int grade)
        {
            if (grade >= 2 && grade <= 5)
            {
                grades.Add(grade);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(grade), "Оценка должна быть от 2 до 5.");
            }
        }

        public bool RemoveGrade(int index)
        {
            if (index < 0 || index >= grades.Count)
            {
                return false;
            }

            grades.RemoveAt(index);
            return true;
        }
    }
}