using GiftSuggester.Data.Groups.Models;

namespace GiftSuggester.Data.Users.Models;

public class UserDbModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public virtual List<GroupDbModel> Groups { get; set; } = new();
}