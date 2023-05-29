namespace GiftSuggester.Web.Users.Models;

public class UserCreationRequest
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
}