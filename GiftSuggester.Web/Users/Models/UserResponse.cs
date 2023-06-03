namespace GiftSuggester.Web.Users.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public List<Guid> GroupIds { get; set; } = new();
}