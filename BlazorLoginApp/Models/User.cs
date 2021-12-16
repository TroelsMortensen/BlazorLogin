namespace BlazorLoginApp.Models;

public class User
{
    public string Name { get;  set; }

    public string Password { get;  set; }

    public string Role { get;  set; }

    public int SecurityLevel { get;  set; }
    public int BirthYear { get;  set; }

    public User(string name, string password, string role, int securityLevel, short birthYear)
    {
        Name = name;
        Password = password;
        Role = role;
        SecurityLevel = securityLevel;
        BirthYear = birthYear;
    }
}