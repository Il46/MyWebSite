namespace MyWebSite.Models;

public class Student : Person
{
    public string StudentId { get; init; }
    public string Group { get; set; }

    public string Name
    {
        get => name;
        set => name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Имя не может быть пустым.", nameof(value))
            : value.Trim();
    }

    public string Surname
    {
        get => surname;
        set => surname = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Фамилия не может быть пустой.", nameof(value))
            : value.Trim();
    }

    public int Age
    {
        get => age;
        set => SetAge(value);
    }

    public Student(string studentId, string name, string surname, int age, string group)
        : base(name, surname, age)
    {
        Name = name;
        Surname = surname;
        StudentId = string.IsNullOrWhiteSpace(studentId)
            ? throw new ArgumentException("Идентификатор студента обязателен.", nameof(studentId))
            : studentId;
        Group = string.IsNullOrWhiteSpace(group)
            ? throw new ArgumentException("Группа обязательна.", nameof(group))
            : group.Trim();
        SetAge(age);
        ValidateStudentData();
    }

    public void SetAge(int newAge)
    {
        if (newAge is < 15 or > 70)
        {
            throw new ArgumentOutOfRangeException(nameof(newAge), "Возраст студента должен быть от 15 до 70 лет.");
        }

        age = newAge;
    }

    private void ValidateStudentData()
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
        {
            throw new ArgumentException("Имя и фамилия не могут быть пустыми.");
        }
    }

    public string GetStudentInfo()
    {
        return $"{DisplayBasicInfo()}, ID: {StudentId}, Группа: {Group}";
    }
}
