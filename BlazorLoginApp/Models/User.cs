namespace BlazorLoginApp.Models;

public class User
{
    public string Name { get; private set; }

    public string Password { get; private set; }

    public string Role { get; private set; }

    public int SecurityLevel { get; private set; }
    public int BirthYear { get; private set; }

    public User(string name, string password, string role, int securityLevel, short birthYear)
    {
        Name = name;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
        BirthYear = birthYear;
    }
}