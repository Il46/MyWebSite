namespace MyWebSite.Models;

public abstract class Person
{
    protected string name;
    protected string surname;
    protected int age;

    protected Person(string name, string surname, int age)
    {
        this.name = name;
        this.surname = surname;
        this.age = age;
    }

    protected string DisplayBasicInfo() => $"ФИО: {surname} {name}, Возраст: {age}";

    protected internal string GetFullName() => $"{name} {surname}";

    private protected void CalculateStatistics()
    {
    }
}
