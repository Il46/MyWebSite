namespace MyWebSite.Models;

public abstract class User
{
    protected string Name { get; }
    protected string Surname { get; }
    private string Email { get; }

    protected User(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }

    public string GetDisplayName() => $"{Name} {Surname}";
}

public sealed class Admin(string name, string surname, string email) : User(name, surname, email);

public sealed class TeacherUser(string name, string surname, string email) : User(name, surname, email);

public sealed class StudentUser(string name, string surname, string email) : User(name, surname, email);
