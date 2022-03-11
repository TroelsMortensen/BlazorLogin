using BlazorLoginApp.Models;

namespace BlazorLoginApp.Services.Impls;

// dummy database 
public class InMemoryUserService : IUserService
{
    public Task<User?> GetUserAsync(string username)
    {
        User? find = users.Find(user => user.Name.Equals(username));
        return Task.FromResult(find);
    }

    private List<User> users = new()
    {
        new User
        {
            Name = "Troels", Password = "Troels1234", Role = "Teacher", SecurityLevel = 3, BirthYear = 1986, Domain = "via"
        },
        new User { Name = "Maria", Password = "oneTwo3FOUR", Role = "Student", SecurityLevel = 1, BirthYear = 2001, Domain = "google" },
        new User { Name = "Anne", Password = "password", Role = "HeadOfDepartment", SecurityLevel = 5, BirthYear = 1975, Domain = "via" }
    };
}