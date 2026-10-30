namespace MyWebSite.Models;

public class Teacher : Person
{
    private int experience;
    private string subject;

    public string Name => name;
    public string Surname => surname;
    public int Age => age;

    public string Subject
    {
        get => subject;
        set => subject = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Предмет обязателен.", nameof(value))
            : value.Trim();
    }

    public int Experience => experience;

    public Teacher(string name, string surname, int age, string subject, int experience)
        : base(name, surname, age)
    {
        this.name = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Имя обязательно.", nameof(name))
            : name.Trim();
        this.surname = string.IsNullOrWhiteSpace(surname)
            ? throw new ArgumentException("Фамилия обязательна.", nameof(surname))
            : surname.Trim();
        if (age is < 18 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "Возраст преподавателя должен быть от 18 до 100 лет.");
        }

        this.age = age;
        this.subject = string.IsNullOrWhiteSpace(subject)
            ? throw new ArgumentException("Предмет обязателен.", nameof(subject))
            : subject.Trim();
        UpdateExperience(experience);
    }

    public void UpdateExperience(int newExperience)
    {
        if (newExperience < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newExperience), "Стаж не может быть отрицательным.");
        }

        experience = newExperience;
    }

    public string GetTeacherInfo()
    {
        DisplayBasicInfo();
        return $"{DisplayBasicInfo()}, Предмет: {Subject}, Стаж: {experience} лет";
    }
}
