namespace BlazorLoginApp.Authentication;

public class User
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public int SecurityLevel { get; set; }
}