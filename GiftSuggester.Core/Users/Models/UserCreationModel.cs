namespace GiftSuggester.Core.Users.Models;

public class UserCreationModel
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
}