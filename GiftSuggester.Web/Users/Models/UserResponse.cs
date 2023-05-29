namespace GiftSuggester.Web.Users.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<Guid> GroupIds { get; set; }
}